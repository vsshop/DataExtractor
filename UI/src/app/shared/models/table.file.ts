import { CSVFile } from "./csv.file";
import { XMLFile } from "./xml.file";

export class TableFile {
  name: string;
  columnsOriginal: string[];
  columnsModified: string[];
  dataOriginal: string[][];
  dataModified: string[][];
  modifiers: boolean[] = [];

  constructor(xml: XMLFile, csv: CSVFile) {
    this.name = csv.name;
    this.columnsOriginal = xml.columns.map(col => col);
    this.columnsModified = xml.columns.map(col => col);
    this.modifiers = xml.columns.map(_ => false);
    this.dataOriginal = csv.columns.map(col => [...col]);
    this.dataModified = csv.columns.map(col => [...col]);
  }

  get count() {
    return this.columnsOriginal;
  }

  async save() {
    const rowCount = Math.max(...this.dataModified.map(col => col.length));
    const rows = Array.from({ length: rowCount }, (_, rowIndex) =>
      this.dataModified.map(col => col[rowIndex] ?? '')
    );

    const csv = [this.columnsModified, ...rows].map(row =>
      row.map(cell => cell.replace(/"/g, '""')).join(';')
    ).join('\r\n');

    const bomCsv = '\uFEFF' + csv; // добавим BOM для Excel

    const blob = new Blob([bomCsv], { type: 'text/csv;charset=utf-8;' });
    const url = URL.createObjectURL(blob);

    if (this.picker) {
      const handle = await this.picker.call(window, {
        suggestedName: `${this.name}.csv`,
        types: [{
          description: 'CSV file',
          accept: { 'text/csv': ['.csv'] },
        }]
      });
      const writable = await handle.createWritable();
      await writable.write(bomCsv); // важно
      await writable.close();
    } else {
      const a = document.createElement('a');
      a.href = url;
      a.download = `${this.name}.csv`;
      a.click();
      URL.revokeObjectURL(url);
    }
  }

  private get picker() {
    return (window as any).showSaveFilePicker ?? null;
  }

}
