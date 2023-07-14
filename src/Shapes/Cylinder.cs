using System.Numerics;
using System.Text.Json.Serialization;

namespace Tavenem.Mathematics;

/// <summary>
/// Provides information about the properties of a cylinder.
/// </summary>
public readonly struct Cylinder<TScalar> : IShape<Cylinder<TScalar>, TScalar>
    where TScalar : IFloatingPointIeee754<TScalar>
{
    internal readonly Vector3<TScalar> _start;
    internal readonly Vector3<TScalar> _end;

    /// <summary>
    /// The axis of the cylinder.
    /// </summary>
    public Vector3<TScalar> Axis { get; }

    /// <summary>
    /// A circular radius which fully contains the shape.
    /// </summary>
    [JsonIgnore]
    public TScalar ContainingRadius { get; }

    /// <summary>
    /// The point on this shape highest on the Y axis.
    /// </summary>
    [JsonIgnore]
    public Vector3<TScalar> HighestPoint { get; }

    /// <summary>
    /// The point on this shape lowest on the Y axis.
    /// </summary>
    [JsonIgnore]
    public Vector3<TScalar> LowestPoint { get; }

    /// <summary>
    /// The position of the shape in 3D space.
    /// </summary>
    public Vector3<TScalar> Position { get; }

    /// <summary>
    /// The radius of the cylinder.
    /// </summary>
    public TScalar Radius { get; }

    /// <summary>
    /// The rotation of this shape in 3D space. Always <see cref="Quaternion{TScalar}.MultiplicativeIdentity"/> for
    /// cylinder, whose <see cref="Axis"/> defines its orientation.
    /// </summary>
    [JsonIgnore]
    public Quaternion<TScalar> Rotation => Quaternion<TScalar>.MultiplicativeIdentity;

    /// <summary>
    /// Identifies the type of this <see cref="IShape{TScalar}"/>.
    /// </summary>
    [JsonPropertyOrder(-1)]
    public ShapeType ShapeType => ShapeType.Cylinder;

    /// <summary>
    /// The length of the shortest of the three dimensions of this shape.
    /// </summary>
    /// <remarks>
    /// Note: this is not the length of smallest possible cross-section, but of the total length
    /// of the shape along any of the three primary axes of 3D space, prior to any rotation.
    /// </remarks>
    [JsonIgnore]
    public TScalar SmallestDimension { get; }

    /// <summary>
    /// The total volume of the shape.
    /// </summary>
    [JsonIgnore]
    public TScalar Volume { get; }

    /// <summary>
    /// Initializes a new instance of cylinder with the given parameters.
    /// </summary>
    /// <param name="axis">The axis of the cylinder.</param>
    /// <param name="radius">The radius of the cylinder.</param>
    /// <param name="position">The position of the shape in 3D space.</param>
    [JsonConstructor]
    public Cylinder(Vector3<TScalar> axis, TScalar radius, Vector3<TScalar> position)
    {
        Axis = axis;
        Position = position;
        Radius = radius;

        var two = NumberValues.Two<TScalar>();
        var axisLength = Vector3<TScalar>.Length(axis);
        var halfHeight = axisLength / two;
        var r2 = Radius * Radius;
        ContainingRadius = TScalar.Sqrt((halfHeight * halfHeight) + r2);

        var _halfPath = Vector3<TScalar>.Normalize(axis) * halfHeight;
        _start = Position - _halfPath;
        _end = _start + axis;

        var pl = Vector3<TScalar>.Normalize(Axis);
        var pr = Vector3<TScalar>.UnitY - (pl * Vector3<TScalar>.Dot(Vector3<TScalar>.UnitY, pl));
        var h = Vector3<TScalar>.Normalize(pr) * Radius;
        if (_end.Y >= _start.Y)
        {
            HighestPoint = _end + h;
            LowestPoint = _start - h;
        }
        else
        {
            HighestPoint = _start + h;
            LowestPoint = _end - h;
        }

        SmallestDimension = TScalar.Min(axisLength, Radius * two);

        Volume = TScalar.Pi * r2 * axisLength;
    }

    /// <summary>
    /// Initializes a new instance of cylinder with the given parameters.
    /// </summary>
    /// <param name="axis">The axis of the cylinder.</param>
    /// <param name="radius">The radius of the cylinder.</param>
    public Cylinder(Vector3<TScalar> axis, TScalar radius) : this(axis, radius, Vector3<TScalar>.Zero) { }

    /// <summary>
    /// Initializes a new instance of cylinder.
    /// </summary>
    public Cylinder() : this(Vector3<TScalar>.Zero, TScalar.Zero) { }

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
    public IShape<TScalar> GetCloneWithRotation(Quaternion<TScalar> rotation) => GetTypedCloneWithRotation(rotation);

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
    public IShape<TScalar> GetScaledByDimension(TScalar factor) => GetTypedScaledByDimension(factor);

    /// <summary>
    /// Gets a copy of this instance whose dimensions have been scaled such that
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
    public IShape<TScalar> GetScaledByVolume(TScalar factor) => GetTypedScaledByVolume(factor);

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="IShape{TScalar}.Position"/> set to the given
    /// value.
    /// </summary>
    /// <param name="position">The new <see cref="IShape{TScalar}.Position"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="IShape{TScalar}.Position"/> set to the given value.
    /// </returns>
    public Cylinder<TScalar> GetTypedCloneAtPosition(Vector3<TScalar> position)
        => new(Axis, Radius, position);

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="IShape{TScalar}.Rotation"/> set to the given
    /// value.
    /// </summary>
    /// <param name="rotation">The new <see cref="IShape{TScalar}.Rotation"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="IShape{TScalar}.Rotation"/> set to the given value.
    /// </returns>
    public Cylinder<TScalar> GetTypedCloneWithRotation(Quaternion<TScalar> rotation)
        => new(Vector3<TScalar>.Transform(Axis, rotation), Radius, Position);

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
    public Cylinder<TScalar> GetTypedScaledByDimension(TScalar factor)
    {
        if (factor < TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
        }

        return new Cylinder<TScalar>(Axis * factor, Radius * factor, Position);
    }

    /// <summary>
    /// Gets a copy of this instance whose dimensions have been scaled such that
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
    public Cylinder<TScalar> GetTypedScaledByVolume(TScalar factor)
    {
        if (factor < TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
        }

        if (factor == TScalar.Zero)
        {
            return new Cylinder<TScalar>(Vector3<TScalar>.Zero, TScalar.Zero, Position);
        }
        else
        {
            var hMultiplier = TScalar.Sqrt(factor);
            return new Cylinder<TScalar>(Axis * hMultiplier, Radius * TScalar.Sqrt(hMultiplier), Position);
        }
    }

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
            return IsPointWithin(point.Position);
        }
        // Approximate with capsule.
        return new Capsule<TScalar>(Axis, Radius, Position)
            .Intersects(shape);
    }

    /// <summary>
    /// Determines if a given point lies within this shape.
    /// </summary>
    /// <param name="point">A point in the same 3D space as this shape.</param>
    /// <returns>
    /// <see langword="true"/> if the point is within (or tangent to) this shape; otherwise <see langword="false"/>.
    /// </returns>
    public bool IsPointWithin(Vector3<TScalar> point)
    {
        var p1 = Position - (Axis / NumberValues.Two<TScalar>());

        var dP = point - p1;

        var lengthSq = Vector3<TScalar>.LengthSquared(Axis);

        var dot = Vector3<TScalar>.Dot(dP, Axis);

        if (dot < TScalar.Zero || dot > lengthSq)
        {
            return false;
        }

        var dSq = Vector3<TScalar>.Dot(dP, dP) - (dot * dot / lengthSq);

        return dSq <= Radius * Radius;
    }

    /// <summary>
    /// Determines whether the specified cylinder is equivalent to the current object.
    /// </summary>
    /// <param name="other">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true"/> if the specified cylinder is equivalent to the
    /// current object; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(Cylinder<TScalar> other)
        => other.Axis == Axis && other.Radius == Radius && other.Position == Position;

    /// <summary>
    /// Determines whether the specified <see cref="IShape{TScalar}"/> is equivalent to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true"/> if the specified <see cref="IShape{TScalar}"/> is equivalent to the
    /// current object; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(IShape<TScalar>? obj) => obj is Cylinder<TScalar> other && Equals(other);

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    /// <see langword="true"/> if obj and this instance are the same type and represent
    /// the same value; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(object? obj) => obj is Cylinder<TScalar> shape && Equals(shape);

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    public override int GetHashCode() => HashCode.Combine(Axis, Radius, Position);

    /// <summary>
    /// Returns a boolean indicating whether the two given shapes are equal.
    /// </summary>
    /// <param name="left">The first shapes to compare.</param>
    /// <param name="right">The second shapes to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the shapes are equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool operator ==(Cylinder<TScalar> left, IShape<TScalar> right)
        => left.Equals(right);

    /// <summary>
    /// Returns a boolean indicating whether the two given shapes are not equal.
    /// </summary>
    /// <param name="left">The first shapes to compare.</param>
    /// <param name="right">The second shapes to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the shapes are not equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool operator !=(Cylinder<TScalar> left, IShape<TScalar> right)
        => !(left == right);
}
