using System;
using System.Text.Json.Serialization;

namespace Tavenem.Mathematics.Converters
{
    /// <summary>
    /// When placed on an interface, specifies the converter type to use.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
    public class JsonInterfaceConverterAttribute : JsonConverterAttribute
    {
        /// <summary>Initializes a new instance of <see cref="JsonConverter" /> with the specified converter type.</summary>
        /// <param name="converterType">The type of the converter.</param>
        public JsonInterfaceConverterAttribute(Type converterType)
            : base(converterType)
        {
        }
    }
}
