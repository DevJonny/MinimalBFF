using Paramore.Darker.Attributes;

namespace MinimalBFF.Ports.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public sealed class ResultConverterAttribute : QueryHandlerAttribute
{
    public ResultConverterAttribute(int step) : base(step) { }

    public override object[] GetAttributeParams() => Array.Empty<object>();

    public override Type GetDecoratorType() => typeof(ResultConverterDecorator<,>);
}