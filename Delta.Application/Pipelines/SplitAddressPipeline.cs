using System.Text.RegularExpressions;
using Delta.Domain.Models.MTable;
using Delta.Pipeline.Abstracts;
using Delta.Pipeline.Enums;
using Delta.Pipeline.Models;

namespace Delta.Application.Pipelines;

public partial class SplitAddressPipeline : BasePipeline
{
    public override string Title => "Розділення адрес";
    public override string Icon => "spell-check";

    [GeneratedRegex(@"^(.+?)\s+(\d+\s*[a-zA-Z]*)$", RegexOptions.Compiled)]
    private static partial Regex AddressPattern();

    public override async Task Apply(PipelineContext context)
    {
        var addressColumns = FindAddressColumns(context.Table.Columns).ToList();

        for (int i = addressColumns.Count - 1; i >= 0; i--)
        {
            var addressColumn = addressColumns[i];
            var insertIndex = addressColumn.Index + 1;

            var streetNumbers = $"{addressColumn.Title}_Номер";
            var streetTitles = $"{addressColumn.Title}_Вулиця";

            var numberColumn = context.Table.AddColumn(streetNumbers);
            var titleColumn = context.Table.AddColumn(streetTitles);

            foreach (var row in context.Table.Rows)
            {
                var address = row.Cells[addressColumn.Index]?.Trim();
                if (string.IsNullOrWhiteSpace(address)) continue;

                var match = AddressPattern().Match(address);
                if (match.Success)
                {
                    var street = match.Groups[1].Value.Trim();
                    var number = match.Groups[2].Value.Trim();

                    row.Cells[numberColumn.Index] = number;
                    row.Cells[titleColumn.Index] = street;
                }
                else
                {
                    row.Cells[titleColumn.Index] = address;
                }

                var level = match.Success ? LevelCode.Info : LevelCode.Warning;

                var message = $"Split address '{address}'";
                context.Log.Add(new()
                {
                    Row = row.Id,
                    Column = numberColumn.Id,
                    Level = level,
                    Message = message
                });

                context.Log.Add(new()
                {
                    Row = row.Id,
                    Column = titleColumn.Id,
                    Level = level,
                    Message = message
                });
            }

            context.Log.Add(new()
            {
                Column = numberColumn.Id,
                Level = LevelCode.Info,
                Message = $"Created columns: '{streetNumbers}'"
            });

            context.Log.Add(new()
            {
                Column = titleColumn.Id,
                Level = LevelCode.Info,
                Message = $"Created columns: '{streetTitles}'"
            });

            context.Table.RemoveColumn(addressColumn);
            context.Table.MoveColumn(numberColumn, addressColumn.Index);
            context.Table.MoveColumn(titleColumn, addressColumn.Index);
        }
    }

    private static List<Column> FindAddressColumns(List<Column> columns)
    {
        var titles = new[]
        {
            "AN_Strasse_Nr_HW",

        };
        return columns.Where(c => titles.Contains(c.Title)).ToList();
    }
}
