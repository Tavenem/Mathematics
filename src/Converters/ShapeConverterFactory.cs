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
            while (readerCopy.Read())
            {
                if (readerCopy.TokenType == JsonTokenType.PropertyName)
                {
                    var prop = readerCopy.GetString();
                    if (string.Equals(
                        prop,
                        nameof(IShape<TScalar>.ShapeType),
                        options.PropertyNameCaseInsensitive
                            ? StringComparison.OrdinalIgnoreCase
                            : StringComparison.Ordinal))
                    {
                        if (!readerCopy.Read()
                            || !readerCopy.TryGetInt32(out var classTypeInt)
                            || !Enum.IsDefined(typeof(ShapeType), classTypeInt))
                        {
                            throw new JsonException("Type discriminator missing or invalid");
                        }
                        return (ShapeType)classTypeInt switch
                        {
                            ShapeType.Capsule => JsonSerializer.Deserialize(ref reader, typeof(Capsule<TScalar>), options) as IShape<TScalar>,
                            ShapeType.Cone => JsonSerializer.Deserialize(ref reader, typeof(Cone<TScalar>), options) as IShape<TScalar>,
                            ShapeType.Cuboid => JsonSerializer.Deserialize(ref reader, typeof(Cuboid<TScalar>), options) as IShape<TScalar>,
                            ShapeType.Cylinder => JsonSerializer.Deserialize(ref reader, typeof(Cylinder<TScalar>), options) as IShape<TScalar>,
                            ShapeType.Ellipsoid => JsonSerializer.Deserialize(ref reader, typeof(Ellipsoid<TScalar>), options) as IShape<TScalar>,
                            ShapeType.Frustum => JsonSerializer.Deserialize(ref reader, typeof(Frustum<TScalar>), options) as IShape<TScalar>,
                            ShapeType.HollowSphere => JsonSerializer.Deserialize(ref reader, typeof(HollowSphere<TScalar>), options) as IShape<TScalar>,
                            ShapeType.Line => JsonSerializer.Deserialize(ref reader, typeof(Line<TScalar>), options) as IShape<TScalar>,
                            ShapeType.SinglePoint => JsonSerializer.Deserialize(ref reader, typeof(SinglePoint<TScalar>), options) as IShape<TScalar>,
                            ShapeType.Sphere => JsonSerializer.Deserialize(ref reader, typeof(Sphere<TScalar>), options) as IShape<TScalar>,
                            ShapeType.Torus => JsonSerializer.Deserialize(ref reader, typeof(Torus<TScalar>), options) as IShape<TScalar>,
                            _ => throw new JsonException("Type discriminator invalid"),
                        };
                    }
                }
            }
            throw new JsonException("Type discriminator missing");
        }

        /// <summary>Writes an <see cref="IShape{TScalar}"/> as JSON.</summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="value">The value to convert to JSON.</param>
        /// <param name="options">An object that specifies serialization options to use.</param>
        public override void Write(Utf8JsonWriter writer, IShape<TScalar> value, JsonSerializerOptions options)
            => JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
