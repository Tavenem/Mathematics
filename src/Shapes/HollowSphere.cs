using System.Numerics;
using System.Text.Json.Serialization;

namespace Tavenem.Mathematics;

/// <summary>
/// Provides information about the properties of a hollow sphere (shell).
/// </summary>
public readonly struct HollowSphere<TScalar> : IShape<HollowSphere<TScalar>, TScalar>
    where TScalar : IFloatingPointIeee754<TScalar>
{
    /// <summary>
    /// A circular radius which fully contains the shape.
    /// </summary>
    [JsonIgnore]
    public TScalar ContainingRadius => OuterRadius;

    /// <summary>
    /// The point on this shape highest on the Y axis.
    /// </summary>
    [JsonIgnore]
    public Vector3<TScalar> HighestPoint { get; }

    /// <summary>
    /// The inner radius of the hollow sphere.
    /// </summary>
    public TScalar InnerRadius { get; }

    /// <summary>
    /// The point on this shape lowest on the Y axis.
    /// </summary>
    [JsonIgnore]
    public Vector3<TScalar> LowestPoint { get; }

    /// <summary>
    /// The outer radius of the hollow sphere.
    /// </summary>
    public TScalar OuterRadius { get; }

    /// <summary>
    /// The position of the shape in 3D space.
    /// </summary>
    public Vector3<TScalar> Position { get; }

    /// <summary>
    /// The rotation of this shape in 3D space. Always <see cref="Quaternion{TScalar}.MultiplicativeIdentity"/>
    /// for hollow sphere, which can have no meaningful rotation.
    /// </summary>
    [JsonIgnore]
    public Quaternion<TScalar> Rotation => Quaternion<TScalar>.MultiplicativeIdentity;

    /// <summary>
    /// Identifies the type of this <see cref="IShape{TScalar}"/>.
    /// </summary>
    [JsonPropertyOrder(-1)]
    public ShapeType ShapeType => ShapeType.HollowSphere;

    /// <summary>
    /// The length of the shortest of the three dimensions of this shape.
    /// </summary>
    /// <remarks>
    /// Note: this is not the length of smallest possible cross-section, but of the total length
    /// of the shape along any of the three primary axes of 3D space, prior to any rotation.
    /// </remarks>
    [JsonIgnore]
    public TScalar SmallestDimension => OuterRadius;

    /// <summary>
    /// The total volume of the shape.
    /// </summary>
    [JsonIgnore]
    public TScalar Volume { get; }

    /// <summary>
    /// Initializes a new instance of hollow sphere with the given parameters.
    /// </summary>
    /// <param name="innerRadius">The inner radius of the hollow sphere.</param>
    /// <param name="outerRadius">The outer radius of the hollow sphere.</param>
    /// <param name="position">The position of the shape in 3D space.</param>
    [JsonConstructor]
    public HollowSphere(TScalar innerRadius, TScalar outerRadius, Vector3<TScalar> position)
    {
        InnerRadius = innerRadius;
        OuterRadius = outerRadius;
        Position = position;

        HighestPoint = Position + (Vector3<TScalar>.UnitY * outerRadius);
        LowestPoint = Position + (-Vector3<TScalar>.UnitY * outerRadius);
        var fourThirdsPi = NumberValues.FourThirdsPi<TScalar>();
        var three = NumberValues.Three<TScalar>();
        Volume = (fourThirdsPi * TScalar.Pow(OuterRadius, three))
            - (fourThirdsPi * TScalar.Pow(InnerRadius, three));
    }

    /// <summary>
    /// Initializes a new instance of hollow sphere with the given parameters.
    /// </summary>
    /// <param name="innerRadius">The inner radius of the hollow sphere.</param>
    /// <param name="outerRadius">The outer radius of the hollow sphere.</param>
    public HollowSphere(TScalar innerRadius, TScalar outerRadius)
        : this(innerRadius, outerRadius, Vector3<TScalar>.Zero) { }

    /// <summary>
    /// Initializes a new instance of hollow sphere.
    /// </summary>
    public HollowSphere() : this(TScalar.Zero, TScalar.Zero) { }

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
    public HollowSphere<TScalar> GetTypedCloneAtPosition(Vector3<TScalar> position)
        => new(InnerRadius, OuterRadius, position);

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="IShape{TScalar}.Rotation"/> set to the given
    /// value.
    /// </summary>
    /// <param name="rotation">The new <see cref="IShape{TScalar}.Rotation"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="IShape{TScalar}.Rotation"/> set to the given value.
    /// </returns>
    public HollowSphere<TScalar> GetTypedCloneWithRotation(Quaternion<TScalar> rotation) => this;

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
    public HollowSphere<TScalar> GetTypedScaledByDimension(TScalar factor)
    {
        if (factor < TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
        }

        return new HollowSphere<TScalar>(InnerRadius * factor, OuterRadius * factor, Position);
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
    public HollowSphere<TScalar> GetTypedScaledByVolume(TScalar factor)
    {
        if (factor < TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
        }

        if (factor == TScalar.Zero)
        {
            return new HollowSphere<TScalar>(TScalar.Zero, TScalar.Zero, Position);
        }
        else
        {
            var multiplier = TScalar.Cbrt(factor);
            return new HollowSphere<TScalar>(InnerRadius * multiplier, OuterRadius * multiplier, Position);
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
        else if (shape is Line<TScalar> line)
        {
            return Intersects(line);
        }
        else
        {
            var d = Vector3<TScalar>.Distance(Position, shape.Position);
            return d - shape.ContainingRadius <= OuterRadius
                && d + shape.ContainingRadius < InnerRadius;
        }
    }

    /// <summary>
    /// Determines if a given point lies within this shape.
    /// </summary>
    /// <param name="point">A point in the same 3D space as this shape.</param>
    /// <returns>
    /// <see langword="true"/> if the point is within (or tangent to) this shape; otherwise <see langword="false"/>.
    /// </returns>
    public bool IsPointWithin(Vector3<TScalar> point)
        => Vector3<TScalar>.Length(point - Position) <= OuterRadius
        && Vector3<TScalar>.Length(point - Position) >= InnerRadius;

    /// <summary>
    /// Determines whether the specified hollow sphere is equivalent to the current object.
    /// </summary>
    /// <param name="other">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true"/> if the specified hollow sphere is equivalent to the
    /// current object; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(HollowSphere<TScalar> other)
        => other.InnerRadius == InnerRadius && other.OuterRadius == OuterRadius && other.Position == Position;

    /// <summary>
    /// Determines whether the specified <see cref="IShape{TScalar}"/> is equivalent to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true"/> if the specified <see cref="IShape{TScalar}"/> is equivalent to the
    /// current object; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(IShape<TScalar>? obj) => obj is HollowSphere<TScalar> other && Equals(other);

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    /// <see langword="true"/> if obj and this instance are the same type and represent
    /// the same value; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(object? obj) => obj is HollowSphere<TScalar> shape && Equals(shape);

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    public override int GetHashCode() => HashCode.Combine(InnerRadius, OuterRadius, Position);

    private bool Intersects(Line<TScalar> line)
    {
        var diff = line.Position - Position;
        var a0 = Vector3<TScalar>.Dot(diff, diff) - (OuterRadius * OuterRadius);
        var a1 = Vector3<TScalar>.Dot(Vector3<TScalar>.Normalize(line.Path), diff);
        var discriminant = (a1 * a1) - a0;
        if (discriminant < TScalar.Zero)
        {
            return false;
        }
        else
        {
            var extent = line.ContainingRadius;
            var tmp0 = (extent * extent) + a0;
            var tmp1 = (TScalar.One + TScalar.One) * a1 * extent;
            var qm = tmp0 - tmp1;
            var qp = tmp0 + tmp1;
            var intersect = qm * qp <= TScalar.Zero
                || (qm > TScalar.Zero
                && TScalar.Abs(a1) < extent);
            if (!intersect)
            {
                return false;
            }

            // Both endpoints within inner sphere?
            return Vector3<TScalar>.Length(line.HighestPoint - Position) >= InnerRadius
                || Vector3<TScalar>.Length(line.LowestPoint - Position) >= InnerRadius;
        }
    }

    /// <summary>
    /// Returns a boolean indicating whether the two given shapes are equal.
    /// </summary>
    /// <param name="left">The first shapes to compare.</param>
    /// <param name="right">The second shapes to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the shapes are equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool operator ==(HollowSphere<TScalar> left, IShape<TScalar> right)
        => left.Equals(right);

    /// <summary>
    /// Returns a boolean indicating whether the two given shapes are not equal.
    /// </summary>
    /// <param name="left">The first shapes to compare.</param>
    /// <param name="right">The second shapes to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the shapes are not equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool operator !=(HollowSphere<TScalar> left, IShape<TScalar> right)
        => !(left == right);
}
