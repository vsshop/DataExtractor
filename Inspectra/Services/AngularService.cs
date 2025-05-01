using Inspectra.Attributes;
using Inspectra.Extensions;
using System.Reflection;
using System.Text.Json;

namespace Inspectra.Services
{
    public class AngularService
    {
        private readonly Dictionary<string, (object instance, MethodInfo method)> _handlers = new();
        public AngularService(IServiceProvider services)
        {
            var assembly = typeof(Program).Assembly;

            foreach (var type in assembly.GetTypes())
            {
                foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                {
                    var attr = method.GetCustomAttribute<AngularAttribute>();
                    if (attr == null) continue;
                    if (method.IsStatic) continue;

                    string name = attr?.Name ?? method.Name;
                    var instance = services.GetService(type)!;
                    _handlers[name] = (instance, method);
                }
            }
        }

        public async Task<object?> InvokeAsync(string name, string? json = null)
        {
            if (!_handlers.TryGetValue(name, out var handler)) return null;

            var (instance, method) = handler;
            var args = PrepareArguments(method, json);
            var result = method.Invoke(instance, args);
            return await ResultAsync(result);
        }

        private object?[] PrepareArguments(MethodInfo method, string? json)
        {
            var parameters = method.GetParameters();

            if (json == null) return Array.Empty<object?>();
            if (parameters.Length == 0) return Array.Empty<object?>();

            if (parameters.Length == 1)
            {
                var paramType = parameters[0].ParameterType;
                var deserialized = JsonSerializer.Deserialize(json, paramType);
                return new object?[] { deserialized };
            }
            else
            {
                var payloadArray = JsonSerializer.Deserialize<JsonElement[]>(json);

                var args = new object?[parameters.Length];
                for (int i = 0; i < parameters.Length; i++)
                {
                    args[i] = payloadArray[i].Deserialize(parameters[i].ParameterType);
                }
                return args;
            }
        }

        private async Task<object?> ResultAsync(object? invocationResult)
        {
            if (invocationResult is Task task)
            {
                return task.ResultAsync();
            }

            return invocationResult;
        }
    }
}
