using System.Text.Json.Serialization;

namespace Tavenem.Mathematics;

/// <summary>
/// Provides information about the properties of a geometric shape.
/// </summary>
[JsonConverter(typeof(ShapeConverterFactory))]
public interface IShape<TScalar> : IEquatable<IShape<TScalar>> where TScalar : IFloatingPoint<TScalar>
{
    /// <summary>
    /// A circular radius which fully contains the shape.
    /// </summary>
    TScalar ContainingRadius { get; }

    /// <summary>
    /// The point on this shape highest on the Y axis.
    /// </summary>
    Vector3<TScalar> HighestPoint { get; }

    /// <summary>
    /// The point on this shape lowest on the Y axis.
    /// </summary>
    Vector3<TScalar> LowestPoint { get; }

    /// <summary>
    /// The position of the shape in 3D space.
    /// </summary>
    Vector3<TScalar> Position { get; }

    /// <summary>
    /// The rotation of this shape in 3D space.
    /// </summary>
    Quaternion<TScalar> Rotation { get; }

    /// <summary>
    /// Identifies the type of this <see cref="IShape{TScalar}"/>.
    /// </summary>
    ShapeType ShapeType { get; }

    /// <summary>
    /// The length of the shortest of the three dimensions of this shape.
    /// </summary>
    /// <remarks>
    /// Note: this is not the length of smallest possible cross-section, but of the total length
    /// of the shape along any of the three primary axes of 3D space, prior to any rotation.
    /// </remarks>
    TScalar SmallestDimension { get; }

    /// <summary>
    /// The total volume of the shape. Read-only.
    /// </summary>
    TScalar Volume { get; }

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="Position"/> set to the given
    /// value.
    /// </summary>
    /// <param name="position">The new <see cref="Position"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="Position"/> set to the given value.
    /// </returns>
    IShape<TScalar> GetCloneAtPosition(Vector3<TScalar> position);

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="Rotation"/> set to the given
    /// value.
    /// </summary>
    /// <param name="rotation">The new <see cref="Rotation"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="Rotation"/> set to the given value.
    /// </returns>
    IShape<TScalar> GetCloneWithRotation(Quaternion<TScalar> rotation);

    /// <summary>
    /// Gets a copy of this instance whose dimensions have been multiplied by the
    /// given factor.
    /// </summary>
    /// <param name="factor">The amount by which to scale this instance's size.</param>
    /// <returns>
    /// A copy of this instance whose dimensions have been multiplied by the given factor.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="factor"/> must be &gt;= 0.
    /// </exception>
    IShape<TScalar> GetScaledByDimension(TScalar factor);

    /// <summary>
    /// Gets a copy of this instance whose dimensions have beens scaled such that
    /// its volume will be multiplied by the given factor.
    /// </summary>
    /// <param name="factor">The amount by which to scale this instance's volume.</param>
    /// <returns>
    /// A copy of this instance whose dimensions have been scaled such that
    /// its volume will be multiplied by the given factor.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="factor"/> must be &gt;= 0.
    /// </exception>
    IShape<TScalar> GetScaledByVolume(TScalar factor);

    /// <summary>
    /// Determines if this instance intersects with the given <see cref="IShape{TScalar}"/>.
    /// </summary>
    /// <param name="shape">
    /// The <see cref="IShape{TScalar}"/> to check for intersection with this instance.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if this instance intersects with the given <see
    /// cref="IShape{TScalar}"/>; otherwise <see langword="false"/>.
    /// </returns>
    bool Intersects(IShape<TScalar> shape);

    /// <summary>
    /// Determines if a given point lies within this shape.
    /// </summary>
    /// <param name="point">A point in the same 3D space as this shape.</param>
    /// <returns>
    /// <see langword="true"/> if the point is within (or tangent to) this shape; otherwise <see langword="false"/>.
    /// </returns>
    bool IsPointWithin(Vector3<TScalar> point);
}
