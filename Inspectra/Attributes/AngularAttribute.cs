namespace Inspectra.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class AngularAttribute(string? method = null) : Attribute
    {
        public string? Name { get => method; }
    }
}
