using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Tavenem.Mathematics;

/// <summary>
/// Provides information about the properties of a single point as a shape.
/// </summary>
[DebuggerDisplay("{Position}")]
public readonly struct SinglePoint<TScalar> : IShape<SinglePoint<TScalar>, TScalar>
    where TScalar : IFloatingPoint<TScalar>
{
    /// <summary>
    /// A <see cref="SinglePoint{TScalar}"/> representing the origin: a point at <see
    /// cref="Vector3{TScalar}.Zero"/>.
    /// </summary>
    public static SinglePoint<TScalar> Origin { get; } = new();

    /// <summary>
    /// A circular radius which fully contains the shape. Always 0 for a <see cref="SinglePoint{TScalar}"/>.
    /// </summary>
    [JsonIgnore]
    public TScalar ContainingRadius => TScalar.Zero;

    /// <summary>
    /// The point on this shape highest on the Y axis. The same as <see cref="Position"/> for a
    /// <see cref="SinglePoint{TScalar}"/>.
    /// </summary>
    [JsonIgnore]
    public Vector3<TScalar> HighestPoint => Position;

    /// <summary>
    /// The point on this shape lowest on the Y axis. The same as <see cref="Position"/> for a
    /// <see cref="SinglePoint{TScalar}"/>.
    /// </summary>
    [JsonIgnore]
    public Vector3<TScalar> LowestPoint => Position;

    /// <summary>
    /// The position of the shape in 3D space.
    /// </summary>
    public Vector3<TScalar> Position { get; init; }

    /// <summary>
    /// The rotation of this shape in 3D space. Always <see cref="Quaternion{TScalar}.MultiplicativeIdentity"/>
    /// for <see cref="SinglePoint{TScalar}"/>, which can have no meaningful rotation.
    /// </summary>
    [JsonIgnore]
    public Quaternion<TScalar> Rotation => Quaternion<TScalar>.MultiplicativeIdentity;

    /// <summary>
    /// Identifies the type of this <see cref="IShape{TSelf, TScalar}"/>.
    /// </summary>
    public ShapeType ShapeType => ShapeType.SinglePoint;

    /// <summary>
    /// The length of the shortest of the three dimensions of this shape. Always 0 for a <see
    /// cref="SinglePoint{TScalar}"/>.
    /// </summary>
    /// <remarks>
    /// Note: this is not the length of smallest possible cross-section, but of the total length
    /// of the shape along any of the three primary axes of 3D space, prior to any rotation.
    /// </remarks>
    [JsonIgnore]
    public TScalar SmallestDimension => TScalar.Zero;

    /// <summary>
    /// The total volume of the shape. Always 0 for a <see cref="SinglePoint{TScalar}"/>.
    /// </summary>
    [JsonIgnore]
    public TScalar Volume => TScalar.Zero;

    /// <summary>
    /// Initializes a new instance of <see cref="SinglePoint{TScalar}"/>.
    /// </summary>
    /// <param name="position">The position of the shape in 3D space.</param>
    public SinglePoint(Vector3<TScalar> position) => Position = position;

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="Position"/> set to the given
    /// value.
    /// </summary>
    /// <param name="position">The new <see cref="Position"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="Position"/> set to the given value.
    /// </returns>
    public IShape<TScalar> GetCloneAtPosition(Vector3<TScalar> position) => GetTypedCloneAtPosition(position);

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="Rotation"/> set to the given
    /// value.
    /// </summary>
    /// <param name="rotation">The new <see cref="Rotation"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="Rotation"/> set to the given value.
    /// </returns>
    public IShape<TScalar> GetCloneWithRotation(Quaternion<TScalar> rotation) => this;

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
    public IShape<TScalar> GetScaledByDimension(TScalar factor) => this;

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
    public IShape<TScalar> GetScaledByVolume(TScalar factor) => this;

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="IShape{TScalar}.Position"/> set to the given
    /// value.
    /// </summary>
    /// <param name="position">The new <see cref="IShape{TScalar}.Position"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="IShape{TScalar}.Position"/> set to the given value.
    /// </returns>
    public SinglePoint<TScalar> GetTypedCloneAtPosition(Vector3<TScalar> position) => new() { Position = position };

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="IShape{TScalar}.Rotation"/> set to the given
    /// value.
    /// </summary>
    /// <param name="rotation">The new <see cref="IShape{TScalar}.Rotation"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="IShape{TScalar}.Rotation"/> set to the given value.
    /// </returns>
    public SinglePoint<TScalar> GetTypedCloneWithRotation(Quaternion<TScalar> rotation) => this;

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
    public SinglePoint<TScalar> GetTypedScaledByDimension(TScalar factor) => this;

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
    public SinglePoint<TScalar> GetTypedScaledByVolume(TScalar factor) => this;

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
    public bool Intersects(IShape<TScalar> shape)
    {
        if (shape is SinglePoint<TScalar> point)
        {
            return point.Position == Position;
        }
        return shape.IsPointWithin(Position);
    }

    /// <summary>
    /// Determines if a given point lies within this shape.
    /// </summary>
    /// <param name="point">A point in the same 3D space as this shape.</param>
    /// <returns>
    /// <see langword="true"/> if the point is within (or tangent to) this shape; otherwise <see langword="false"/>.
    /// </returns>
    public bool IsPointWithin(Vector3<TScalar> point) => point == Position;

    /// <summary>
    /// Determines whether the specified <see cref="SinglePoint{TScalar}"/> is equivalent to the current
    /// object.
    /// </summary>
    /// <param name="other">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true"/> if the specified point is at the same position as this instance;
    /// otherwise <see langword="false"/>.
    /// </returns>
    public bool Equals(SinglePoint<TScalar> other) => other.Position == Position;

    /// <summary>
    /// Determines whether the specified <see cref="IShape{TScalar}"/> is equivalent to the current
    /// object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true"/> if the specified shape is a point at the same position as this instance;
    /// otherwise <see langword="false"/>.
    /// </returns>
    public bool Equals(IShape<TScalar>? obj) => obj is SinglePoint<TScalar> shape && Equals(shape);

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    /// <see langword="true"/> if obj and this instance are the same type and represent
    /// the same value; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(object? obj) => obj is SinglePoint<TScalar> shape && Equals(shape);

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    public override int GetHashCode() => Position.GetHashCode();

    /// <summary>
    /// Returns a boolean indicating whether the two given shapes are equal.
    /// </summary>
    /// <param name="left">The first shape to compare.</param>
    /// <param name="right">The second shape to compare.</param>
    /// <returns>True if the shapes are equal; False otherwise.</returns>
    public static bool operator ==(SinglePoint<TScalar> left, IShape<TScalar> right)
        => left.Equals(right);

    /// <summary>
    /// Returns a boolean indicating whether the two given shapes are not equal.
    /// </summary>
    /// <param name="left">The first shape to compare.</param>
    /// <param name="right">The second shape to compare.</param>
    /// <returns>True if the shapes are not equal; False if they are equal.</returns>
    public static bool operator !=(SinglePoint<TScalar> left, IShape<TScalar> right)
        => !(left == right);
}
