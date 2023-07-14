using System.Numerics;
using System.Text.Json.Serialization;

namespace Tavenem.Mathematics;

/// <summary>
/// Provides information about the properties of a cone.
/// </summary>
public readonly struct Cone<TScalar> : IShape<Cone<TScalar>, TScalar>
    where TScalar : IFloatingPointIeee754<TScalar>
{
    private readonly Vector3<TScalar> _start;
    private readonly Vector3<TScalar> _end;

    /// <summary>
    /// The axis of the cone, starting from the point and ending at the base.
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
    /// THe length of the cone, from point to base.
    /// </summary>
    [JsonIgnore]
    public TScalar Length { get; }

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
    /// The radius of the cone's base.
    /// </summary>
    public TScalar Radius { get; }

    /// <summary>
    /// The rotation of this shape in 3D space. Always <see cref="Quaternion{TScalar}.MultiplicativeIdentity"/> for
    /// cone, whose <see cref="Axis"/> defines its orientation.
    /// </summary>
    [JsonIgnore]
    public Quaternion<TScalar> Rotation => Quaternion<TScalar>.MultiplicativeIdentity;

    /// <summary>
    /// Identifies the type of this <see cref="IShape{TScalar}"/>.
    /// </summary>
    [JsonPropertyOrder(-1)]
    public ShapeType ShapeType => ShapeType.Cone;

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
    /// Initializes a new instance of cone with the given parameters.
    /// </summary>
    /// <param name="axis">The axis of the cone, starting from the point and
    /// ending at the base.</param>
    /// <param name="radius">The radius of the cone's base.</param>
    /// <param name="position">The position of the shape in 3D space.</param>
    [JsonConstructor]
    public Cone(Vector3<TScalar> axis, TScalar radius, Vector3<TScalar> position)
    {
        Axis = axis;
        Position = position;
        Radius = radius;

        Length = Vector3<TScalar>.Length(axis);
        var two = NumberValues.Two<TScalar>();
        var halfHeight = Length / two;
        var r2 = Radius * Radius;
        ContainingRadius = TScalar.Sqrt((halfHeight * halfHeight) + r2);

        var _halfPath = Vector3<TScalar>.Normalize(axis) * halfHeight;
        _start = Position - _halfPath;
        _end = _start + axis;

        var pl = Vector3<TScalar>.Normalize(Axis);
        var pr = Vector3<TScalar>.UnitY - (pl * Vector3<TScalar>.Dot(Vector3<TScalar>.UnitY, pl));
        var h = Vector3<TScalar>.Normalize(pr) * Radius;
        var endTop = _end + h;
        var endBottom = _end - h;
        HighestPoint = endTop.Y >= _start.Y ? endTop : _start;
        LowestPoint = endBottom.Y < _start.Y ? endBottom : _start;

        SmallestDimension = TScalar.Min(Length, Radius * two);

        Volume = TScalar.Pi * r2 * (Length / NumberValues.Three<TScalar>());
    }

    /// <summary>
    /// Initializes a new instance of cone with the given parameters.
    /// </summary>
    /// <param name="origin">The position of the apex of the cone.</param>
    /// <param name="orientation">The direction of the cone's axis, and its length.</param>
    /// <param name="angle">The angle of the cone.</param>
    public Cone(Vector3<TScalar> origin, Vector3<TScalar> orientation, TScalar angle)
    {
        Axis = orientation;

        _start = origin;
        _end = origin + orientation;

        Length = Vector3<TScalar>.Length(orientation);
        var two = NumberValues.Two<TScalar>();
        var halfHeight = Length / two;

        var normalAxis = Vector3<TScalar>.Normalize(orientation);
        Position = origin + (normalAxis * halfHeight);

        Radius = TScalar.Tan(angle / two) * Length;
        var r2 = Radius * Radius;
        ContainingRadius = TScalar.Sqrt((halfHeight * halfHeight) + r2);

        var pr = Vector3<TScalar>.UnitY - (normalAxis * Vector3<TScalar>.Dot(Vector3<TScalar>.UnitY, normalAxis));
        var h = Vector3<TScalar>.Normalize(pr) * Radius;
        var endTop = _end + h;
        var endBottom = _end - h;
        HighestPoint = endTop.Y >= _start.Y ? endTop : _start;
        LowestPoint = endBottom.Y < _start.Y ? endBottom : _start;

        SmallestDimension = TScalar.Min(Length, Radius * two);

        Volume = TScalar.Pi * r2 * (Length / NumberValues.Three<TScalar>());
    }

    /// <summary>
    /// Initializes a new instance of cone with the given parameters.
    /// </summary>
    /// <param name="axis">The axis of the cone, starting from the point and
    /// ending at the base.</param>
    /// <param name="radius">The radius of the cone's base.</param>
    public Cone(Vector3<TScalar> axis, TScalar radius) : this(axis, radius, Vector3<TScalar>.Zero) { }

    /// <summary>
    /// Initializes a new instance of cone.
    /// </summary>
    public Cone() : this(Vector3<TScalar>.Zero, TScalar.Zero) { }

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
    public Cone<TScalar> GetTypedCloneAtPosition(Vector3<TScalar> position)
        => new(Axis, Radius, position);

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="IShape{TScalar}.Rotation"/> set to the given
    /// value.
    /// </summary>
    /// <param name="rotation">The new <see cref="IShape{TScalar}.Rotation"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="IShape{TScalar}.Rotation"/> set to the given value.
    /// </returns>
    public Cone<TScalar> GetTypedCloneWithRotation(Quaternion<TScalar> rotation)
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
    public Cone<TScalar> GetTypedScaledByDimension(TScalar factor)
    {
        if (factor < TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
        }

        return new Cone<TScalar>(Axis * factor, Radius * factor, Position);
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
    public Cone<TScalar> GetTypedScaledByVolume(TScalar factor)
    {
        if (factor < TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
        }

        if (factor == TScalar.Zero)
        {
            return new Cone<TScalar>(Vector3<TScalar>.Zero, TScalar.Zero, Position);
        }
        else
        {
            var hMultiplier = TScalar.Sqrt(factor);
            return new Cone<TScalar>(Axis * hMultiplier, Radius * TScalar.Sqrt(hMultiplier));
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
        if (shape is Capsule<TScalar> capsule)
        {
            return Intersects(capsule);
        }
        if (shape is Cone<TScalar> cone)
        {
            return Intersects(cone);
        }
        if (shape is Line<TScalar> line)
        {
            return Intersects(line);
        }
        if (shape is Sphere<TScalar> sphere)
        {
            return Intersects(sphere);
        }
        return shape.Intersects(this);
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
        var p1 = point - Position;
        var dot = Vector3<TScalar>.Dot(p1, Axis);

        return dot / Vector3<TScalar>.Length(p1) / Length
            > TScalar.Cos(TScalar.Atan(Radius / Length) / NumberValues.Two<TScalar>())
            && dot / Length < Length;
    }

    /// <summary>
    /// Determines whether the specified cone is equivalent to the current object.
    /// </summary>
    /// <param name="other">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true"/> if the specified cone is equivalent to the
    /// current object; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(Cone<TScalar> other)
        => other.Axis == Axis && other.Radius == Radius && other.Position == Position;

    /// <summary>
    /// Determines whether the specified <see cref="IShape{TScalar}"/> is equivalent to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true"/> if the specified <see cref="IShape{TScalar}"/> is equivalent to the
    /// current object; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(IShape<TScalar>? obj) => obj is Cone<TScalar> other && Equals(other);

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    /// <see langword="true"/> if obj and this instance are the same type and represent
    /// the same value; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(object? obj) => obj is Cone<TScalar> shape && Equals(shape);

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    public override int GetHashCode() => HashCode.Combine(Axis, Radius, Position);

    internal TScalar GetRadiusAtPoint(Vector3<TScalar> point) => Radius / Length * Vector3<TScalar>.Length(point - Position);

    private bool Intersects(Capsule<TScalar> capsule)
    {
        var axis = new Line<TScalar>(Axis, Position);
        var otherAxis = new Line<TScalar>(capsule.Axis, capsule.Position);
        axis.GetDistanceTo(otherAxis, out var closestPoint, out var otherClosestPoint);
        var radiusAtClosestPoint = GetRadiusAtPoint(closestPoint);
        var closestSphere = new Sphere<TScalar>(radiusAtClosestPoint, closestPoint);
        var otherClosestSphere = new Sphere<TScalar>(capsule.Radius, otherClosestPoint);
        return closestSphere.Intersects(otherClosestSphere);
    }

    private bool Intersects(Cone<TScalar> cone)
    {
        var axis = new Line<TScalar>(Axis, Position);
        var otherAxis = new Line<TScalar>(cone.Axis, cone.Position);
        axis.GetDistanceTo(otherAxis, out var closestPoint, out var closestPointOther);
        var radiusAtClosestPoint = GetRadiusAtPoint(closestPoint);
        var otherRadiusAtClosestPoint = cone.GetRadiusAtPoint(closestPointOther);
        var closestSphere = new Sphere<TScalar>(radiusAtClosestPoint, closestPoint);
        var otherClosestSphere = new Sphere<TScalar>(otherRadiusAtClosestPoint, closestPointOther);
        return closestSphere.Intersects(otherClosestSphere);
    }

    private bool Intersects(Line<TScalar> line)
    {
        var PmV = line.Position - Position;
        var dir = Vector3<TScalar>.Normalize(Axis);
        var lineDir = Vector3<TScalar>.Normalize(line.Path);
        var DdU = Vector3<TScalar>.Dot(dir, lineDir);
        var DdPmV = Vector3<TScalar>.Dot(dir, PmV);
        var cosAngle = TScalar.Cos(TScalar.Atan(Radius / Length));
        var cosSqr = cosAngle * cosAngle;
        var c2 = (DdU * DdU) - cosSqr;
        var c1 = (DdU * DdPmV) - (cosSqr * Vector3<TScalar>.Dot(lineDir, PmV));
        var c0 = (DdPmV * DdPmV) - (cosSqr * Vector3<TScalar>.Dot(PmV, PmV));
        TScalar t;
        var intersects = new TScalar[2];

        if (!c2.IsNearlyZero())
        {
            var discriminant = (c1 * c1) - (c0 * c2);
            if (discriminant < TScalar.Zero)
            {
                return false;
            }

            if (discriminant > TScalar.Zero)
            {
                var root = TScalar.Sqrt(discriminant);
                var invC2 = TScalar.One / c2;
                var numIntersects = 0;

                t = (-c1 - root) * invC2;
                if ((DdU * t) + DdPmV >= TScalar.Zero)
                {
                    intersects[numIntersects++] = t;
                }

                t = (-c1 + root) * invC2;
                if ((DdU * t) + DdPmV >= TScalar.Zero)
                {
                    intersects[numIntersects++] = t;
                }

                if (numIntersects == 2 && intersects[0] > intersects[1])
                {
                    (intersects[1], intersects[0]) = (intersects[0], intersects[1]);
                }
                else if (numIntersects == 1)
                {
                    if (DdU > TScalar.Zero)
                    {
                        intersects[1] = TScalar.PositiveInfinity;
                    }
                    else
                    {
                        intersects[1] = intersects[0];
                        intersects[0] = TScalar.PositiveInfinity;
                    }
                }
                else if (numIntersects == 0)
                {
                    return false;
                }
            }
            else
            {
                t = -c1 / c2;
                if ((DdU * t) + DdPmV >= TScalar.Zero)
                {
                    intersects[0] = t;
                    intersects[1] = t;
                }
                else
                {
                    return false;
                }
            }
        }
        else if (!c1.IsNearlyZero())
        {
            t = -NumberValues.Half<TScalar>() * c0 / c1;
            if ((DdU * t) + DdPmV >= TScalar.Zero)
            {
                if (DdU > TScalar.Zero)
                {
                    intersects[0] = t;
                    intersects[1] = TScalar.PositiveInfinity;
                }
                else
                {
                    intersects[0] = TScalar.NegativeInfinity;
                    intersects[1] = t;
                }
            }
            else
            {
                return false;
            }
        }
        else if (!c0.IsNearlyZero())
        {
            return false;
        }
        else // the line is along the cone
        {
            intersects[0] = TScalar.NegativeInfinity;
            intersects[1] = TScalar.PositiveInfinity;
        }

        if (!DdU.IsNearlyZero())
        {
            var invAD = TScalar.One / DdU;
            var hInterval = new TScalar[2];
            if (DdU > TScalar.Zero)
            {
                hInterval[0] = -DdPmV * invAD;
                hInterval[1] = (Vector3<TScalar>.Length(Axis) - DdPmV) * invAD;
            }
            else
            {
                hInterval[0] = (Vector3<TScalar>.Length(Axis) - DdPmV) * invAD;
                hInterval[1] = -DdPmV * invAD;
            }

            if (intersects[1] < hInterval[0] || intersects[0] > hInterval[1])
            {
                return false;
            }
            else if (intersects[1] > hInterval[0])
            {
                if (intersects[0] < hInterval[1])
                {
                    intersects[0] = intersects[0] < hInterval[0] ? hInterval[0] : intersects[0];
                    intersects[1] = intersects[1] < hInterval[1] ? hInterval[1] : intersects[1];
                }
                else
                {
                    intersects[1] = intersects[0];
                }
            }
            else
            {
                intersects[0] = intersects[1];
            }
        }

        return intersects[1] >= -line.Length && intersects[0] <= line.Length;
    }

    private bool Intersects(Sphere<TScalar> sphere)
    {
        var angle = TScalar.Atan(Radius / Length);
        var sinAngle = TScalar.Sin(angle);
        var cosAngle = TScalar.Cos(angle);
        var CmV = sphere.Position - Position;
        var dir = Vector3<TScalar>.Normalize(Axis);
        var D = CmV + (sphere.Radius / sinAngle * dir);
        var lenSqr = Vector3<TScalar>.Dot(D, D);
        var e = Vector3<TScalar>.Dot(D, dir);
        if (e <= TScalar.Zero || e * e < lenSqr * cosAngle * cosAngle)
        {
            return false;
        }

        lenSqr = Vector3<TScalar>.Dot(CmV, CmV);
        e = -Vector3<TScalar>.Dot(CmV, dir);

        return e <= TScalar.Zero || e * e < lenSqr * sinAngle * sinAngle
            || lenSqr <= sphere.Radius * sphere.Radius;
    }

    /// <summary>
    /// Returns a boolean indicating whether the two given shapes are equal.
    /// </summary>
    /// <param name="left">The first shapes to compare.</param>
    /// <param name="right">The second shapes to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the shapes are equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool operator ==(Cone<TScalar> left, IShape<TScalar> right)
        => left.Equals(right);

    /// <summary>
    /// Returns a boolean indicating whether the two given shapes are not equal.
    /// </summary>
    /// <param name="left">The first shapes to compare.</param>
    /// <param name="right">The second shapes to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the shapes are not equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool operator !=(Cone<TScalar> left, IShape<TScalar> right)
        => !(left == right);
}
