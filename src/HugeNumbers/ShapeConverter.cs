using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tavenem.Mathematics.HugeNumbers
{
    /// <summary>
    /// Converts an <see cref="IShape"/> to or from JSON.
    /// </summary>
    public class ShapeConverter : JsonConverter<IShape>
    {
        /// <summary>Determines whether the specified type can be converted.</summary>
        /// <param name="typeToConvert">The type to compare against.</param>
        /// <returns>
        /// <see langword="true" /> if the type can be converted; otherwise, <see langword="false"
        /// />.
        /// </returns>
        public override bool CanConvert(Type typeToConvert) => typeof(IShape).IsAssignableFrom(typeToConvert);

        /// <summary>Reads and converts the JSON to an <see cref="IShape"/>.</summary>
        /// <param name="reader">The reader.</param>
        /// <param name="typeToConvert">The type to convert.</param>
        /// <param name="options">An object that specifies serialization options to use.</param>
        /// <returns>The converted value.</returns>
        public override IShape? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                        nameof(IShape.ShapeType),
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
                            ShapeType.Capsule => JsonSerializer.Deserialize(ref reader, typeof(Capsule), options) as IShape,
                            ShapeType.Cone => JsonSerializer.Deserialize(ref reader, typeof(Cone), options) as IShape,
                            ShapeType.Cuboid => JsonSerializer.Deserialize(ref reader, typeof(Cuboid), options) as IShape,
                            ShapeType.Cylinder => JsonSerializer.Deserialize(ref reader, typeof(Cylinder), options) as IShape,
                            ShapeType.Ellipsoid => JsonSerializer.Deserialize(ref reader, typeof(Ellipsoid), options) as IShape,
                            ShapeType.Frustum => JsonSerializer.Deserialize(ref reader, typeof(Frustum), options) as IShape,
                            ShapeType.HollowSphere => JsonSerializer.Deserialize(ref reader, typeof(HollowSphere), options) as IShape,
                            ShapeType.Line => JsonSerializer.Deserialize(ref reader, typeof(Line), options) as IShape,
                            ShapeType.SinglePoint => JsonSerializer.Deserialize(ref reader, typeof(SinglePoint), options) as IShape,
                            ShapeType.Sphere => JsonSerializer.Deserialize(ref reader, typeof(Sphere), options) as IShape,
                            ShapeType.Torus => JsonSerializer.Deserialize(ref reader, typeof(Torus), options) as IShape,
                            _ => throw new JsonException("Type discriminator invalid"),
                        };
                    }
                }
            }
            throw new JsonException("Type discriminator missing");
        }

        /// <summary>Writes an <see cref="IShape"/> as JSON.</summary>
        /// <param name="writer">The writer to write to.</param>
        /// <param name="value">The value to convert to JSON.</param>
        /// <param name="options">An object that specifies serialization options to use.</param>
        public override void Write(Utf8JsonWriter writer, IShape value, JsonSerializerOptions options)
            => JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}
