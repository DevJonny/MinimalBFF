using Paramore.Darker.Attributes;

namespace MinimalBFF.Ports.Attributes;

public class HttpStatusToResultConverter
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class HttpStatusToResultConverterAttribute : QueryHandlerAttribute
    {
        public HttpStatusToResultConverterAttribute(int step) : base(step)
        {
        }

        public override object[] GetAttributeParams()
        {
            return new object[0];
        }

        public override Type GetDecoratorType()
        {
            return typeof(HttpStatusToResultConverterDecorator<,>);
        }
    }
}