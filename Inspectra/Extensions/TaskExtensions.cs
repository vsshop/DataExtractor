namespace Inspectra.Extensions
{
    public static class TaskExtensions
    {
        public static object? Result(this Task task)
        {
            var type = task.GetType();
            var property = type.GetProperty("Result");
            return property?.GetValue(task);
        }

        public static async Task<object?> ResultAsync(this Task task)
        {
            await task.ConfigureAwait(false);
            return task.Result();
        }
    }
}
