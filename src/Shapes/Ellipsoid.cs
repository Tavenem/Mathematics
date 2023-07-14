using System.Numerics;
using System.Text.Json.Serialization;

namespace Tavenem.Mathematics;

/// <summary>
/// Provides information about the properties of an ellipsoid.
/// </summary>
public readonly struct Ellipsoid<TScalar> : IShape<Ellipsoid<TScalar>, TScalar>
    where TScalar : IFloatingPointIeee754<TScalar>
{
    /// <summary>
    /// The length of the X half-axis of the ellipsoid extending from its center
    /// to its surface.
    /// </summary>
    public TScalar AxisX { get; }

    /// <summary>
    /// The length of the Y half-axis of the ellipsoid extending from its center
    /// to its surface.
    /// </summary>
    public TScalar AxisY { get; }

    /// <summary>
    /// The length of the Z half-axis of the ellipsoid extending from its center
    /// to its surface.
    /// </summary>
    public TScalar AxisZ { get; }

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
    /// The rotation of this shape in 3D space.
    /// </summary>
    public Quaternion<TScalar> Rotation { get; }

    /// <summary>
    /// Identifies the type of this <see cref="IShape{TScalar}"/>.
    /// </summary>
    [JsonPropertyOrder(-1)]
    public ShapeType ShapeType => ShapeType.Ellipsoid;

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
    /// Initializes a new instance of ellipsoid with the given parameters.
    /// </summary>
    /// <param name="axisX">The length of the first radius of the ellipsoid.</param>
    /// <param name="axisY">The length of the second radius of the ellipsoid.</param>
    /// <param name="axisZ">The length of the third radius of the ellipsoid.</param>
    /// <param name="position">The position of the shape in 3D space.</param>
    /// <param name="rotation">The rotation of this shape in 3D space.</param>
    [JsonConstructor]
    public Ellipsoid(TScalar axisX, TScalar axisY, TScalar axisZ, Vector3<TScalar> position, Quaternion<TScalar> rotation)
    {
        AxisX = axisX;
        AxisY = axisY;
        AxisZ = axisZ;
        Position = position;
        Rotation = rotation;

        ContainingRadius = TScalar.Max(AxisX, TScalar.Max(AxisY, AxisZ));

        var x = Vector3<TScalar>.Transform(Vector3<TScalar>.UnitX * AxisX, Rotation);
        var y = Vector3<TScalar>.Transform(Vector3<TScalar>.UnitY * AxisY, Rotation);
        var z = Vector3<TScalar>.Transform(Vector3<TScalar>.UnitZ * AxisZ, Rotation);
        var points = new Vector3<TScalar>[6]
        {
            Position + x,
            Position + y,
            Position + z,
            Position - x,
            Position - y,
            Position - z,
        };
        HighestPoint = points[0];
        LowestPoint = points[0];
        for (var i = 0; i < 6; i++)
        {
            if (points[i].Y > HighestPoint.Y)
            {
                HighestPoint = points[i];
            }
            if (points[i].Y < LowestPoint.Y)
            {
                LowestPoint = points[i];
            }
        }

        SmallestDimension = TScalar.Min(AxisX, TScalar.Min(AxisY, AxisZ));
        Volume = NumberValues.FourThirdsPi<TScalar>() * AxisX * AxisY * AxisZ;
    }

    /// <summary>
    /// Initializes a new instance of ellipsoid with the given parameters.
    /// </summary>
    /// <param name="axisX">The length of the first radius of the ellipsoid.</param>
    /// <param name="axisY">The length of the second radius of the ellipsoid.</param>
    /// <param name="axisZ">The length of the third radius of the ellipsoid.</param>
    /// <param name="position">The position of the shape in 3D space.</param>
    public Ellipsoid(TScalar axisX, TScalar axisY, TScalar axisZ, Vector3<TScalar> position)
        : this(axisX, axisY, axisZ, position, Quaternion<TScalar>.MultiplicativeIdentity) { }

    /// <summary>
    /// Initializes a new instance of ellipsoid with the given parameters.
    /// </summary>
    /// <param name="axisX">The length of the first radius of the ellipsoid.</param>
    /// <param name="axisY">The length of the second radius of the ellipsoid.</param>
    /// <param name="axisZ">The length of the third radius of the ellipsoid.</param>
    public Ellipsoid(TScalar axisX, TScalar axisY, TScalar axisZ)
        : this(axisX, axisY, axisZ, Vector3<TScalar>.Zero, Quaternion<TScalar>.MultiplicativeIdentity) { }

    /// <summary>
    /// Initializes a new instance of ellipsoid as an oblate spheroid with the given parameters.
    /// </summary>
    /// <param name="radius">The equatorial radius of the oblate spheroid.</param>
    /// <param name="axis">The axis length of the oblate spheroid, from the center to the
    /// perimeter.</param>
    /// <param name="position">The position of the shape in 3D space.</param>
    /// <param name="rotation">The rotation of this shape in 3D space.</param>
    public Ellipsoid(TScalar radius, TScalar axis, Vector3<TScalar> position, Quaternion<TScalar> rotation)
        : this(radius, axis, radius, position, rotation) { }

    /// <summary>
    /// Initializes a new instance of ellipsoid as an oblate spheroid with the
    /// given parameters.
    /// </summary>
    /// <param name="radius">The equatorial radius of the oblate spheroid.</param>
    /// <param name="axis">The axis length of the oblate spheroid, from the center to the
    /// perimeter.</param>
    /// <param name="position">The position of the shape in 3D space.</param>
    public Ellipsoid(TScalar radius, TScalar axis, Vector3<TScalar> position)
        : this(radius, axis, radius, position, Quaternion<TScalar>.MultiplicativeIdentity) { }

    /// <summary>
    /// Initializes a new instance of ellipsoid as an oblate spheroid with the given parameters.
    /// </summary>
    /// <param name="radius">The equatorial radius of the oblate spheroid.</param>
    /// <param name="axis">The axis length of the oblate spheroid, from the center to the
    /// perimeter.</param>
    public Ellipsoid(TScalar radius, TScalar axis)
        : this(radius, axis, radius, Vector3<TScalar>.Zero, Quaternion<TScalar>.MultiplicativeIdentity) { }

    /// <summary>
    /// Initializes a new instance of ellipsoid.
    /// </summary>
    public Ellipsoid() : this(TScalar.Zero, TScalar.Zero, TScalar.Zero) { }

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
    public Ellipsoid<TScalar> GetTypedCloneAtPosition(Vector3<TScalar> position)
        => new(AxisX, AxisY, AxisZ, position, Rotation);

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="IShape{TScalar}.Rotation"/> set to the given
    /// value.
    /// </summary>
    /// <param name="rotation">The new <see cref="IShape{TScalar}.Rotation"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="IShape{TScalar}.Rotation"/> set to the given value.
    /// </returns>
    public Ellipsoid<TScalar> GetTypedCloneWithRotation(Quaternion<TScalar> rotation)
        => new(AxisX, AxisY, AxisZ, Position, rotation);

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
    public Ellipsoid<TScalar> GetTypedScaledByDimension(TScalar factor)
    {
        if (factor < TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
        }

        return new Ellipsoid<TScalar>(
            AxisX * factor,
            AxisY * factor,
            AxisZ * factor,
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
    public Ellipsoid<TScalar> GetTypedScaledByVolume(TScalar factor)
    {
        if (factor < TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
        }

        if (factor == TScalar.Zero)
        {
            return new Ellipsoid<TScalar>(TScalar.Zero, TScalar.Zero, TScalar.Zero, Position);
        }
        else
        {
            var multiplier = TScalar.Cbrt(factor);
            return new Ellipsoid<TScalar>(
                AxisX * multiplier,
                AxisY * multiplier,
                AxisZ * multiplier,
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

        // First-pass check against overlapping containing radii.
        if (Vector3<TScalar>.Distance(Position, shape.Position) > shape.ContainingRadius + ContainingRadius)
        {
            return false;
        }

        if (shape is not Line<TScalar> and not Cuboid<TScalar>)
        {
            // For all other shapes rely on containing radii overlap only.
            return true;
        }

        var s = new Matrix4x4<TScalar>(
            AxisX, TScalar.Zero, TScalar.Zero, TScalar.Zero,
            TScalar.Zero, AxisY, TScalar.Zero, TScalar.Zero,
            TScalar.Zero, TScalar.Zero, AxisZ, TScalar.Zero,
            TScalar.Zero, TScalar.Zero, TScalar.Zero, TScalar.One);
        var r = Matrix4x4<TScalar>.CreateFromQuaternion(Rotation);
        var t = Matrix4x4<TScalar>.CreateTranslation(Position);
        Matrix4x4<TScalar>.Invert(t * r * s, out var invK);

        var iSphere = new Sphere<TScalar>(TScalar.One);

        if (shape is Line<TScalar> line)
        {
            return iSphere.Intersects(new Line<TScalar>(Vector3<TScalar>.Transform(line.Path, invK), Vector3<TScalar>.Transform(line.Position, invK)));
        }
        else if (shape is Cuboid<TScalar> cuboid)
        {
            for (var i = 0; i < 8; i += 2)
            {
                var l = Line<TScalar>.Create(cuboid.Corners[i], cuboid.Corners[i + 1]);
                if (iSphere.Intersects(new Line<TScalar>(
                    Vector3<TScalar>.Transform(l.Path, invK),
                    Vector3<TScalar>.Transform(l.Position, invK))))
                {
                    return true;
                }
            }
            return false;
        }

        return false;
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
        var two = NumberValues.Two<TScalar>();
        return TScalar.Pow((point.X - Position.X) / AxisX, two)
            + TScalar.Pow((point.Y - Position.Y) / AxisY, two)
            + TScalar.Pow((point.Z - Position.Z) / AxisZ, two) <= TScalar.One;
    }

    /// <summary>
    /// Determines whether the specified ellipsoid is equivalent to the current object.
    /// </summary>
    /// <param name="other">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true"/> if the specified ellipsoid is equivalent to the
    /// current object; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(Ellipsoid<TScalar> other) => other.AxisX == AxisX
        && other.AxisY == AxisY
        && other.AxisZ == AxisZ
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
    public bool Equals(IShape<TScalar>? obj) => obj is Ellipsoid<TScalar> other && Equals(other);

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    /// <see langword="true"/> if obj and this instance are the same type and represent
    /// the same value; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(object? obj) => obj is Ellipsoid<TScalar> shape && Equals(shape);

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    public override int GetHashCode() => HashCode.Combine(AxisX, AxisY, AxisZ, Position, Rotation);

    /// <summary>
    /// Returns a boolean indicating whether the two given shapes are equal.
    /// </summary>
    /// <param name="left">The first shapes to compare.</param>
    /// <param name="right">The second shapes to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the shapes are equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool operator ==(Ellipsoid<TScalar> left, IShape<TScalar> right)
        => left.Equals(right);

    /// <summary>
    /// Returns a boolean indicating whether the two given shapes are not equal.
    /// </summary>
    /// <param name="left">The first shapes to compare.</param>
    /// <param name="right">The second shapes to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the shapes are not equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool operator !=(Ellipsoid<TScalar> left, IShape<TScalar> right)
        => !(left == right);
}
