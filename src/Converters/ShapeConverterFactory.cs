using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tavenem.Mathematics;

/// <summary>
/// Converts an <see cref="IShape{TScalar}"/> to or from JSON.
/// </summary>
public class ShapeConverterFactory : JsonConverterFactory
{
    /// <summary>Determines whether the specified type can be converted.</summary>
    /// <param name="typeToConvert">The type to compare against.</param>
    /// <returns>
    /// <see langword="true" /> if the type can be converted; otherwise, <see langword="false" />.
    /// </returns>
    public override bool CanConvert(Type typeToConvert)
    {
        if (!typeToConvert.IsGenericType)
        {
            return false;
        }

        return typeToConvert.GetGenericTypeDefinition().IsAssignableFrom(typeof(IShape<>));
    }

    /// <summary>
    /// Creates a converter for a specified type.
    /// </summary>
    /// <param name="typeToConvert">The type handled by the converter.</param>
    /// <param name="options">The serialization options to use.</param>
    /// <returns>
    /// A converter for which T is compatible with <paramref name="typeToConvert" />.
    /// </returns>
    public override JsonConverter CreateConverter(
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        var type = typeToConvert.GetGenericArguments()[0];

        return (JsonConverter)Activator.CreateInstance(
            typeof(ShapeConverter<>).MakeGenericType(
                new Type[] { type }),
            BindingFlags.Instance | BindingFlags.Public,
            binder: null,
            args: null,
            culture: null)!;
    }

    private class ShapeConverter<TScalar> : JsonConverter<IShape<TScalar>>
         where TScalar : IFloatingPoint<TScalar>
    {
        /// <summary>Reads and converts the JSON to an <see cref="IShape{TScalar}"/>.</summary>
        /// <param name="reader">The reader.</param>
        /// <param name="typeToConvert">The type to convert.</param>
        /// <param name="options">An object that specifies serialization options to use.</param>
        /// <returns>The converted value.</returns>
        public override IShape<TScalar>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var readerCopy = reader;
            readerCopy.Read();
            if (readerCopy.TokenType != JsonTokenType.PropertyName
                || !readerCopy.Read()
                || !readerCopy.TryGetInt32(out var classTypeInt)
                || !Enum.IsDefined(typeof(ShapeType), classTypeInt))
            {
                throw new JsonException("Type discriminator missing or invalid");
            }
            var type = (ShapeType)classTypeInt switch
            {
                ShapeType.Capsule => typeof(Capsule<TScalar>),
                ShapeType.Cone => typeof(Cone<TScalar>),
                ShapeType.Cuboid => typeof(Cuboid<TScalar>),
                ShapeType.Cylinder => typeof(Cylinder<TScalar>),
                ShapeType.Ellipsoid => typeof(Ellipsoid<TScalar>),
                ShapeType.Frustum => typeof(Frustum<TScalar>),
                ShapeType.HollowSphere => typeof(HollowSphere<TScalar>),
                ShapeType.Line => typeof(Line<TScalar>),
                ShapeType.SinglePoint => typeof(SinglePoint<TScalar>),
                ShapeType.Sphere => typeof(Sphere<TScalar>),
                ShapeType.Torus => typeof(Torus<TScalar>),
                _ => throw new JsonException("Type discriminator invalid"),
            };
            return JsonSerializer.Deserialize(ref reader, type, options) as IShape<TScalar>;
        }

        /// <summary>Writes an <see cref="IShape{TScalar}"/> as JSON.</summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="value">The value to convert to JSON.</param>
        /// <param name="options">An object that specifies serialization options to use.</param>
        public override void Write(Utf8JsonWriter writer, IShape<TScalar> value, JsonSerializerOptions options)
            => JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
