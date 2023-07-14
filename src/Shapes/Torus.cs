using System.Numerics;
using System.Text.Json.Serialization;

namespace Tavenem.Mathematics;

/// <summary>
/// Provides information about the properties of a torus.
/// </summary>
public readonly struct Torus<TScalar> : IShape<Torus<TScalar>, TScalar>
    where TScalar : IFloatingPointIeee754<TScalar>
{
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
    /// The distance from the center of the tube to the center of the torus.
    /// </summary>
    public TScalar MajorRadius { get; }

    /// <summary>
    /// The radius of the tube, in meters.
    /// </summary>
    public TScalar MinorRadius { get; }

    /// <summary>
    /// The position of the shape in 3D space.
    /// </summary>
    public Vector3<TScalar> Position { get; }

    /// <summary>
    /// The rotation of this shape in 3D space.
    /// </summary>
    public Quaternion<TScalar> Rotation { get; }

    /// <summary>
    /// Identifies the type of this <see cref="IShape{TScalar}"/>.
    /// </summary>
    [JsonPropertyOrder(-1)]
    public ShapeType ShapeType => ShapeType.Torus;

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
    /// Initializes a new instance of torus.
    /// </summary>
    /// <param name="majorRadius">The length of the major radius of the torus.</param>
    /// <param name="minorRadius">The length of the minor radius of the torus.</param>
    /// <param name="position">The position of the shape in 3D space.</param>
    /// <param name="rotation">The rotation of this shape in 3D space.</param>
    [JsonConstructor]
    public Torus(TScalar majorRadius, TScalar minorRadius, Vector3<TScalar> position, Quaternion<TScalar> rotation)
    {
        if (majorRadius < minorRadius)
        {
            throw new ArgumentException("majorRadius cannot be smaller than minorRadius", nameof(majorRadius));
        }
        MajorRadius = majorRadius;
        MinorRadius = minorRadius;
        Position = position;
        Rotation = rotation;

        ContainingRadius = MajorRadius + MinorRadius;

        var x = Vector3<TScalar>.Transform(Vector3<TScalar>.UnitX, Rotation);
        var z = Vector3<TScalar>.Transform(Vector3<TScalar>.UnitZ, Rotation);
        var pl = Vector3<TScalar>.Normalize(Vector3<TScalar>.Cross(x, z));
        var pr = Vector3<TScalar>.UnitY - (pl * Vector3<TScalar>.Dot(Vector3<TScalar>.UnitY, pl));
        var h = Vector3<TScalar>.Normalize(pr) * MajorRadius;
        var y = Vector3<TScalar>.UnitY * MinorRadius;
        HighestPoint = Position + h + y;
        LowestPoint = Position - h - y;

        SmallestDimension = TScalar.Min(MajorRadius, MinorRadius);
        Volume = NumberValues.TwoPiSquared<TScalar>() * MajorRadius * MinorRadius.Square();
    }

    /// <summary>
    /// Initializes a new instance of torus.
    /// </summary>
    /// <param name="majorRadius">The length of the major radius of the torus.</param>
    /// <param name="minorRadius">The length of the minor radius of the torus.</param>
    /// <param name="position">The position of the shape in 3D space.</param>
    public Torus(TScalar majorRadius, TScalar minorRadius, Vector3<TScalar> position)
        : this(majorRadius, minorRadius, position, Quaternion<TScalar>.MultiplicativeIdentity) { }

    /// <summary>
    /// Initializes a new instance of torus.
    /// </summary>
    /// <param name="majorRadius">The length of the major radius of the torus.</param>
    /// <param name="minorRadius">The length of the minor radius of the torus.</param>
    public Torus(TScalar majorRadius, TScalar minorRadius) : this(
        majorRadius,
        minorRadius,
        Vector3<TScalar>.Zero,
        Quaternion<TScalar>.MultiplicativeIdentity)
    { }

    /// <summary>
    /// Initializes a new instance of torus.
    /// </summary>
    public Torus() : this(TScalar.Zero, TScalar.Zero) { }

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
    public Torus<TScalar> GetTypedCloneAtPosition(Vector3<TScalar> position)
        => new(MajorRadius, MinorRadius, position, Rotation);

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="IShape{TScalar}.Rotation"/> set to the given
    /// value.
    /// </summary>
    /// <param name="rotation">The new <see cref="IShape{TScalar}.Rotation"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="IShape{TScalar}.Rotation"/> set to the given value.
    /// </returns>
    public Torus<TScalar> GetTypedCloneWithRotation(Quaternion<TScalar> rotation)
        => new(MajorRadius, MinorRadius, Position, rotation);

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
    public Torus<TScalar> GetTypedScaledByDimension(TScalar factor)
    {
        if (factor < TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
        }

        return new Torus<TScalar>(
            MajorRadius * factor,
            MinorRadius * factor,
            Position,
            Rotation);
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
    public Torus<TScalar> GetTypedScaledByVolume(TScalar factor)
    {
        if (factor < TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
        }

        if (factor == TScalar.Zero)
        {
            return new Torus<TScalar>(TScalar.Zero, TScalar.Zero, Position);
        }
        else
        {
            var majMultiplier = TScalar.Sqrt(factor);
            return new Torus<TScalar>(
                MajorRadius * majMultiplier,
                MinorRadius * TScalar.Sqrt(majMultiplier),
                Position,
                Rotation);
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
        // First-pass exclusion based on containing radii.
        if (Vector3<TScalar>.Distance(Position, shape.Position) > shape.ContainingRadius + ContainingRadius)
        {
            return false;
        }
        // Approximate with cylinder
        return new Cylinder<TScalar>(
            Vector3<TScalar>.Transform(Vector3<TScalar>.UnitY * MinorRadius, Rotation),
            MajorRadius + MinorRadius,
            Position)
            .Intersects(shape);
    }

    /// <summary>
    /// Determines if a given point lies within this shape.
    /// </summary>
    /// <param name="point">A point in the same 3D space as this shape.</param>
    /// <returns>True if the point is within (or tangent to) this shape.</returns>
    public bool IsPointWithin(Vector3<TScalar> point)
    {
        var two = TScalar.One + TScalar.One;
        return TScalar.Pow(TScalar.Pow(point.X - Position.X, two)
            + TScalar.Pow(point.Y - Position.Y, two)
            + TScalar.Pow(point.Z - Position.Z, two)
            + TScalar.Pow(MajorRadius, two)
            - TScalar.Pow(MinorRadius, two),
            two)
            - (two * two
            * TScalar.Pow(MajorRadius, two)
            * (TScalar.Pow(point.X - Position.X, two)
            + TScalar.Pow(point.Y - Position.Y, two))) <= TScalar.Zero;
    }

    /// <summary>
    /// Determines whether the specified torus is equivalent to the current object.
    /// </summary>
    /// <param name="other">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true"/> if the specified torus is equivalent to the
    /// current object; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(Torus<TScalar> other) => other.MajorRadius == MajorRadius
        && other.MinorRadius == MinorRadius
        && other.Position == Position
        && other.Rotation == Rotation;

    /// <summary>
    /// Determines whether the specified <see cref="IShape{TScalar}"/> is equivalent to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true"/> if the specified <see cref="IShape{TScalar}"/> is equivalent to the
    /// current object; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(IShape<TScalar>? obj) => obj is Torus<TScalar> other && Equals(other);

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    /// <see langword="true"/> if obj and this instance are the same type and represent
    /// the same value; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(object? obj) => obj is Torus<TScalar> shape && Equals(shape);

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    public override int GetHashCode() => HashCode.Combine(MajorRadius, MinorRadius, Position, Rotation);

    /// <summary>
    /// Returns a boolean indicating whether the two given shapes are equal.
    /// </summary>
    /// <param name="left">The first shapes to compare.</param>
    /// <param name="right">The second shapes to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the shapes are equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool operator ==(Torus<TScalar> left, IShape<TScalar> right)
        => left.Equals(right);

    /// <summary>
    /// Returns a boolean indicating whether the two given shapes are not equal.
    /// </summary>
    /// <param name="left">The first shapes to compare.</param>
    /// <param name="right">The second shapes to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the shapes are not equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool operator !=(Torus<TScalar> left, IShape<TScalar> right)
        => !(left == right);
}
