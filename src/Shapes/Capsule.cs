using System.Numerics;
using System.Text.Json.Serialization;

namespace Tavenem.Mathematics;

/// <summary>
/// Provides information about the properties of a capsule (swept sphere).
/// </summary>
public readonly struct Capsule<TScalar> : IShape<Capsule<TScalar>, TScalar>
    where TScalar : IFloatingPointIeee754<TScalar>
{
    internal readonly Vector3<TScalar> _start;
    internal readonly Vector3<TScalar> _end;
    private readonly TScalar _pathLength;

    /// <summary>
    /// The axis of the capsule.
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
    /// The total length of this capsule.
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
    /// The radius of the capsule.
    /// </summary>
    public TScalar Radius { get; }

    /// <summary>
    /// The rotation of this shape in 3D space. Always <see cref="Quaternion{TScalar}.MultiplicativeIdentity"/> for
    /// capsule, whose <see cref="Axis"/> defines its orientation.
    /// </summary>
    [JsonIgnore]
    public Quaternion<TScalar> Rotation => Quaternion<TScalar>.MultiplicativeIdentity;

    /// <summary>
    /// Identifies the type of this <see cref="IShape{TScalar}"/>.
    /// </summary>
    [JsonPropertyOrder(-1)]
    public ShapeType ShapeType => ShapeType.Capsule;

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
    /// Initializes a new instance of capsule with the given parameters.
    /// </summary>
    /// <param name="axis">The axis of the capsule.</param>
    /// <param name="radius">The radius of the capsule.</param>
    /// <param name="position">The position of the shape in 3D space.</param>
    [JsonConstructor]
    public Capsule(Vector3<TScalar> axis, TScalar radius, Vector3<TScalar> position)
    {
        Axis = axis;
        Position = position;
        Radius = radius;

        var two = NumberValues.Two<TScalar>();
        _pathLength = Vector3<TScalar>.Length(axis);
        var twoR = Radius * two;
        Length = _pathLength + twoR;

        var halfHeight = _pathLength / two;
        ContainingRadius = halfHeight + Radius;

        var _halfPath = Vector3<TScalar>.Normalize(axis) * halfHeight;
        _start = Position - _halfPath;
        _end = _start + axis;

        var y = Vector3<TScalar>.UnitY * Radius;
        if (_end.Y >= _start.Y)
        {
            HighestPoint = _end + y;
            LowestPoint = _start - y;
        }
        else
        {
            HighestPoint = _start + y;
            LowestPoint = _end - y;
        }

        SmallestDimension = TScalar.Min(Length, twoR);

        Volume = (TScalar.Pi * Radius * Radius * _pathLength)
            + (NumberValues.FourThirdsPi<TScalar>() * Radius.Cube());
    }

    /// <summary>
    /// Initializes a new instance of capsule with the given parameters.
    /// </summary>
    /// <param name="axis">The axis of the capsule.</param>
    /// <param name="radius">The radius of the capsule.</param>
    public Capsule(Vector3<TScalar> axis, TScalar radius) : this(axis, radius, Vector3<TScalar>.Zero) { }

    /// <summary>
    /// Initializes a new instance of capsule.
    /// </summary>
    public Capsule() : this(Vector3<TScalar>.Zero, TScalar.Zero) { }

    private static int FindBoxEdgeIntersection(
        TScalar ay,
        TScalar ey,
        TScalar dx,
        TScalar dz,
        TScalar bx,
        TScalar by,
        TScalar bz,
        TScalar radius,
        out TScalar distance)
    {
        distance = TScalar.Zero;

        var rSqr = radius * radius;
        TScalar dy, crossZ, crossX;

        if (by >= TScalar.Zero)
        {
            dy = ay - ey;
            crossZ = (dx * by) - (dy * bx);
            crossX = (dz * by) - (dy * bz);
        }
        else
        {
            dy = ay + ey;
            crossZ = (dy * bx) - (dx * by);
            crossX = (dy * bz) - (dz * by);
        }

        if (crossZ >= TScalar.Zero && crossX >= TScalar.Zero && (crossX * crossX) + (crossZ * crossZ) > rSqr * by * by)
        {
            var endPoint = new Vector3<TScalar>(bx, by, bz);
            var cross = Vector3<TScalar>.Cross(new Vector3<TScalar>(dx, dy, dz), endPoint);
            var crossSqrLen = Vector3<TScalar>.Dot(cross, cross);
            if (crossSqrLen > rSqr * Vector3<TScalar>.Dot(endPoint, endPoint))
            {
                return 0; // misses on vertex
            }

            distance = GetBoxVertexIntersection(dx, dy, dz, bx, by, bz, rSqr);
        }
        else
        {
            distance = GetBoxEdgeIntersection(dx, dz, bx, bz, (bz * bz) + (bx * bx), rSqr);
        }
        return 1;
    }

    private static int FindBoxEdgeRegionIntersection(
        TScalar ex, TScalar ey, TScalar ez,
        TScalar ax, TScalar ay, TScalar az,
        TScalar bx, TScalar by, TScalar bz,
        bool isAboveEdge, TScalar radius, out TScalar distance)
    {
        distance = TScalar.Zero;

        var dx = ax - ex;
        var dz = az - ez;

        if (isAboveEdge && (dx * dx) + (dz * dz) - (radius * radius) <= TScalar.Zero)
        {
            return -1; // already intersecting
        }

        if ((bx * dx) + (bz * dz) >= TScalar.Zero)
        {
            return 0; // not moving towards box
        }

        var dotPerp = (bz * dx) - (bx * dz);
        if (dotPerp >= TScalar.Zero)
        {
            if (bx >= TScalar.Zero)
            {
                return 0; // missed corner
            }

            // check intersection with x-z edge
            if (dotPerp <= -radius * bx)
            {
                return FindBoxEdgeIntersection(ay, ey, dz, dx, bz, by, bx, radius, out distance);
            }

            // check intersection with face on Z axis
            return FindBoxFaceRegionIntersection(ex, ey, ez, ax, ay, az, bx, by, bz, false, radius, out distance);
        }
        if (bz >= TScalar.Zero)
        {
            return 0; // missed corner
        }
        // check intersection with x-z edge
        return dotPerp >= radius * bz
            ? FindBoxEdgeIntersection(ay, ey, dx, dz, bx, by, bz, radius, out distance)
            : FindBoxFaceRegionIntersection(ez, ey, ex, az, ay, ax, bz, by, bx, false, radius, out distance);
    }

    private static int FindBoxFaceRegionIntersection(
        TScalar ex, TScalar ey, TScalar ez,
        TScalar ax, TScalar ay, TScalar az,
        TScalar bx, TScalar by, TScalar bz,
        bool isAboveFace, TScalar radius, out TScalar distance)
    {
        distance = TScalar.Zero;

        if (az <= ez + radius && isAboveFace)
        {
            return -1; // already intersecting
        }
        if (bz >= TScalar.Zero)
        {
            return 0; // moving away on z axis
        }
        var rSqr = radius * radius;
        var bSqrX = (bz * bz) + (bx * bx);
        var bSqrY = (bz * bz) + (by * by);
        TScalar dx, dy;
        var dz = az - ez;
        TScalar crossX, crossY;
        TScalar signX, signY;

        if (bx >= TScalar.Zero)
        {
            signX = TScalar.One;
            dx = ax - ex;
            crossX = (bx * dz) - (bz * dx);
        }
        else
        {
            signX = TScalar.NegativeOne;
            dx = ax + ex;
            crossX = (bz * dx) - (bx * dz);
        }

        if (by >= TScalar.Zero)
        {
            signY = TScalar.One;
            dy = ay - ey;
            crossY = (by * dz) - (bz * dy);
        }
        else
        {
            signY = TScalar.NegativeOne;
            dy = ay + ey;
            crossY = (bz * dy) - (by * dz);
        }

        if (crossX > radius * bx * signX)
        {
            if (crossX * crossX > rSqr * bSqrX)
            {
                return 0; // sphere misses on x axis
            }
            if (crossY > radius * by * signY)
            {
                if (crossY * crossY > rSqr * bSqrY)
                {
                    return 0; // sphere misses on y axis
                }

                var endPoint = new Vector3<TScalar>(bx, by, bz);
                var cross = Vector3<TScalar>.Cross(new Vector3<TScalar>(dx, dy, dz), endPoint);
                if (Vector3<TScalar>.Dot(cross, cross) > rSqr * Vector3<TScalar>.Dot(endPoint, endPoint))
                {
                    return 0; // sphere misses the corner
                }

                // intersection with y edge
                distance = GetBoxVertexIntersection(dx, dy, dz, bx, by, bz, rSqr);
            }
            else
            {
                distance = GetBoxEdgeIntersection(dx, dz, bx, bz, bSqrX, rSqr); // intersection with x edge
            }
        }
        else if (crossY > radius * by * signY)
        {
            if (crossY * crossY > rSqr * bSqrY)
            {
                return 0; // sphere misses on y axis
            }
            distance = GetBoxEdgeIntersection(dy, dz, by, bz, bSqrY, rSqr); // intersection with y edge
        }
        else
        {
            distance = (-dz + radius) / bz; // intersection with face
        }

        return 1;
    }

    private static int FindBoxVertexRegionIntersection(
        TScalar ex, TScalar ey, TScalar ez,
        TScalar ax, TScalar ay, TScalar az,
        TScalar bx, TScalar by, TScalar bz,
        TScalar radius, out TScalar distance)
    {
        distance = TScalar.Zero;

        var dx = ax - ex;
        var dy = ay - ey;
        var dz = az - ez;
        var rSqr = radius * radius;

        if ((dx * dx) + (dy * dy) + (dz * dz) - rSqr <= TScalar.Zero)
        {
            return -1; // sphere is already intersecting box
        }
        if ((bx * dx) + (by * dy) + (bz * dz) >= TScalar.Zero)
        {
            return 0; // sphere is not moving towards the box
        }

        var crossX = (by * dz) - (bz * dy);
        var crossY = (bx * dz) - (bz * dx);
        var crossZ = (by * dx) - (bx * dy);
        var crossXSqr = crossX * crossX;
        var crossYSqr = crossY * crossY;
        var crossZSqr = crossZ * crossZ;

        if ((crossY < TScalar.Zero && crossZ >= TScalar.Zero && crossYSqr + crossZSqr <= rSqr * bx * bx)
            || (crossZ < TScalar.Zero && crossX < TScalar.Zero && crossXSqr + crossZSqr <= rSqr * by * by)
            || (crossY >= TScalar.Zero && crossX >= TScalar.Zero && crossXSqr + crossYSqr <= rSqr * bz * bz))
        {
            // intersection with the vertex
            distance = GetBoxVertexIntersection(dx, dy, dz, bx, by, bz, rSqr);
            return 1;
        }

        // check Y and Z planes
        if (crossY < TScalar.Zero && crossZ >= TScalar.Zero)
        {
            return FindBoxEdgeRegionIntersection(ey, ex, ez, ay, ax, az, by, bx, bz, false, radius, out distance);
        }

        // check X and Z planes
        if (crossZ < TScalar.Zero && crossX < TScalar.Zero)
        {
            return FindBoxEdgeRegionIntersection(ex, ey, ez, ax, ay, az, bx, by, bz, false, radius, out distance);
        }

        // check X and Y planes
        return FindBoxEdgeRegionIntersection(ex, ez, ey, ax, az, ay, bx, bz, by, false, radius, out distance);
    }

    private static TScalar GetBoxEdgeIntersection(TScalar dx, TScalar dz, TScalar bx, TScalar bz, TScalar bSqr, TScalar rSqr)
    {
        var dot = (bx * dx) + (bz * dz);
        var diff = (dx * dx) + (dz * dz) - rSqr;
        var inv = TScalar.One / TScalar.Sqrt(TScalar.Abs((dot * dot) - (bSqr * diff)));
        return diff * inv / (TScalar.One - (dot * inv));
    }

    private static TScalar GetBoxVertexIntersection(
        TScalar dx, TScalar dy, TScalar dz,
        TScalar bx, TScalar by, TScalar bz,
        TScalar rSqr)
    {
        var bSqr = (bx * bx) + (by * by) + (bz * bz);
        var dot = (dx * bx) + (dy * by) + (dz * bz);
        var diff = (dx * dx) + (dy * dy) + (dz * dz) - rSqr;
        var inv = TScalar.One / TScalar.Sqrt(TScalar.Abs((dot * dot) - (bSqr * diff)));
        return diff * inv / (TScalar.One - (dot * inv));
    }

    private static bool Quadratic(TScalar a, TScalar b, TScalar c, out TScalar root1, out TScalar root2)
    {
        var q = (b * b) - (NumberValues.Four<TScalar>() * a * c);
        if (q >= TScalar.Zero)
        {
            var sq = TScalar.Sqrt(q);
            var d = TScalar.One / (NumberValues.Two<TScalar>() * a);
            root1 = (-b + sq) * d;
            root2 = (-b - sq) * d;
            return true;
        }
        else
        {
            root1 = TScalar.NaN;
            root2 = TScalar.NaN;
            return false;
        }
    }

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
    public IShape<TScalar> GetScaledByVolume(TScalar factor) => GetTypedScaledByVolume(factor);

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="IShape{TScalar}.Position"/> set to the given
    /// value.
    /// </summary>
    /// <param name="position">The new <see cref="IShape{TScalar}.Position"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="IShape{TScalar}.Position"/> set to the given value.
    /// </returns>
    public Capsule<TScalar> GetTypedCloneAtPosition(Vector3<TScalar> position)
        => new(Axis, Radius, position);

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="IShape{TScalar}.Rotation"/> set to the given
    /// value.
    /// </summary>
    /// <param name="rotation">The new <see cref="IShape{TScalar}.Rotation"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="IShape{TScalar}.Rotation"/> set to the given value.
    /// </returns>
    public Capsule<TScalar> GetTypedCloneWithRotation(Quaternion<TScalar> rotation)
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
    public Capsule<TScalar> GetTypedScaledByDimension(TScalar factor)
    {
        if (factor < TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
        }

        return new Capsule<TScalar>(Axis * factor, Radius * factor, Position);
    }

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
    public Capsule<TScalar> GetTypedScaledByVolume(TScalar factor)
    {
        if (factor < TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
        }

        if (factor == TScalar.Zero)
        {
            return new Capsule<TScalar>(Vector3<TScalar>.Zero, TScalar.Zero, Position);
        }
        else
        {
            var hMultiplier = TScalar.Sqrt(factor);
            return new Capsule<TScalar>(Axis * hMultiplier, Radius * TScalar.Sqrt(hMultiplier), Position);
        }
    }

    /// <summary>
    /// Treating this capsule as a swept sphere (along the path of its <see
    /// cref="Axis"/>), finds the point at which that sphere makes contact with the given
    /// <paramref name="shape"/>, if any.
    /// </summary>
    /// <param name="shape">The <see cref="IShape{TScalar}"/> to check for collision with the swept
    /// sphere represented by this instance.</param>
    /// <returns>
    /// The point along this instance's <see cref="Axis"/> at which the swept sphere
    /// represented by this instance makes contact with the given <paramref name="shape"/>; or
    /// <see langword="null"/> if they do not collide.
    /// </returns>
    public Vector3<TScalar>? GetCollisionPoint(IShape<TScalar> shape)
    {
        if (!TryGetDistanceToCollisionPoint(shape, out var dist))
        {
            return null;
        }

        return _start + (Vector3<TScalar>.Normalize(Axis) * dist);
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
        if (shape is Line<TScalar> line)
        {
            return Intersects(line, out var _);
        }
        if (shape is Sphere<TScalar> sphere)
        {
            return Intersects(sphere.Radius, sphere.Position, sphere.Position, out var _);
        }
        if (shape is Capsule<TScalar> capsule)
        {
            return Intersects(capsule.Radius, capsule._start, capsule._end, out var _);
        }
        if (shape is Cuboid<TScalar> cuboid)
        {
            return Intersects(cuboid, out var _);
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
        if (Vector3<TScalar>.Distance(point, _start) < Radius)
        {
            return true;
        }
        else if (Vector3<TScalar>.Distance(point, _end) < Radius)
        {
            return true;
        }

        var axisNormal = Vector3<TScalar>.Normalize(Axis);
        var localRight = Vector3<TScalar>.Cross(axisNormal, Vector3<TScalar>.UnitY);
        var orthonormalBasis = new Vector3<TScalar>[]
        {
                axisNormal,
                localRight,
                Vector3<TScalar>.Cross(localRight, axisNormal)
        };
        var delta = point - Position;
        var P = new Vector3<TScalar>(
            Vector3<TScalar>.Dot(orthonormalBasis[1], delta),
            Vector3<TScalar>.Dot(orthonormalBasis[2], delta),
            Vector3<TScalar>.Dot(orthonormalBasis[0], delta));
        var closest = P;
        var sqrRadius = Radius * Radius;
        var sqrDist = (P.X * P.X) + (P.Y * P.Y);
        if (sqrDist > sqrRadius)
        {
            return false;
        }
        var distance = sqrDist.IsNearlyEqualTo(sqrRadius) ? TScalar.Sqrt(sqrDist) - Radius : TScalar.Zero;
        if (sqrDist.IsNearlyEqualTo(sqrRadius))
        {
            var tmp = Radius / distance;
            closest = new Vector3<TScalar>(closest.X * tmp, closest.Y * tmp, closest.Z);
        }
        var two = NumberValues.Two<TScalar>();
        if (P.Z > _pathLength / two || P.Z < -_pathLength / two)
        {
            distance = Vector3<TScalar>.Length(closest - P);
        }
        return distance <= TScalar.Zero;
    }

    /// <summary>
    /// Determines whether the specified capsule is equivalent to the current object.
    /// </summary>
    /// <param name="other">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true"/> if the specified capsule is equivalent to the
    /// current object; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(Capsule<TScalar> other)
        => other.Axis == Axis && other.Radius == Radius && other.Position == Position;

    /// <summary>
    /// Determines whether the specified <see cref="IShape{TScalar}"/> is equivalent to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true"/> if the specified <see cref="IShape{TScalar}"/> is equivalent to the
    /// current object; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(IShape<TScalar>? obj) => obj is Capsule<TScalar> other && Equals(other);

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    /// <see langword="true"/> if obj and this instance are the same type and represent
    /// the same value; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(object? obj) => obj is Capsule<TScalar> shape && Equals(shape);

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    public override int GetHashCode() => HashCode.Combine(Axis, Radius, Position);

    private bool TryGetDistanceToCollisionPoint(IShape<TScalar> shape, out TScalar value)
    {
        value = TScalar.Zero;
        if (shape is SinglePoint<TScalar> point)
        {
            if (new Line<TScalar>(Axis, Position).GetDistanceTo(point, out var closestPoint).IsNearlyZero())
            {
                value = Vector3<TScalar>.Length(closestPoint - _start);
                return true;
            }
            return false;
        }
        if (shape is Line<TScalar> line)
        {
            if (Intersects(line, out var d))
            {
                value = d;
                return true;
            }
            return false;
        }
        if (shape is Sphere<TScalar> sphere)
        {
            if (Intersects(sphere.Radius, sphere.Position, sphere.Position, out var d))
            {
                value = d;
                return true;
            }
            return false;
        }
        if (shape is Capsule<TScalar> capsule)
        {
            if (Intersects(capsule.Radius, capsule._start, capsule._end, out var d))
            {
                value = d;
                return true;
            }
            return false;
        }
        if (shape is Cuboid<TScalar> cuboid)
        {
            if (Intersects(cuboid, out var d))
            {
                value = d;
                return true;
            }
            return false;
        }
        // Substitute the sphere formed by the containing radius.
        if (Intersects(shape.ContainingRadius, shape.Position, shape.Position, out var dist))
        {
            value = dist;
            return true;
        }
        return false;
    }

    private bool Intersects(Cuboid<TScalar> cuboid, out TScalar distance)
    {
        distance = TScalar.Zero;

        // Get starting sphere in the local coordinates of the box
        var cdiff = _start - cuboid.Position;
        var orthonormalBasis = new Vector3<TScalar>[]
        {
            Vector3<TScalar>.Transform(Vector3<TScalar>.UnitX, cuboid.Rotation),
            Vector3<TScalar>.Transform(Vector3<TScalar>.UnitY, cuboid.Rotation),
            Vector3<TScalar>.Transform(Vector3<TScalar>.UnitZ, cuboid.Rotation)
        };
        var ax = Vector3<TScalar>.Dot(cdiff, orthonormalBasis[0]);
        var ay = Vector3<TScalar>.Dot(cdiff, orthonormalBasis[1]);
        var az = Vector3<TScalar>.Dot(cdiff, orthonormalBasis[2]);
        var bx = Vector3<TScalar>.Dot(Axis, orthonormalBasis[0]);
        var by = Vector3<TScalar>.Dot(Axis, orthonormalBasis[1]);
        var bz = Vector3<TScalar>.Dot(Axis, orthonormalBasis[2]);

        if (ax < TScalar.Zero)
        {
            ax = -ax;
            bx = -bx;
        }
        if (ay < TScalar.Zero)
        {
            ay = -ay;
            by = -by;
        }
        if (az < TScalar.Zero)
        {
            az = -az;
            bz = -bz;
        }

        int retVal;

        var two = NumberValues.Two<TScalar>();
        var extent0 = cuboid.AxisX / two;
        var extent1 = cuboid.AxisY / two;
        var extent2 = cuboid.AxisZ / two;
        if (ax <= extent0)
        {
            if (ay <= extent1)
            {
                if (az <= extent2)
                {
                    return true; // starting sphere is inside box
                }

                retVal = FindBoxFaceRegionIntersection(extent0, extent1, extent2, ax, ay, az, bx, by, bz, true, Radius, out distance);
            }
            else
            {
                retVal = az <= extent2
                ? FindBoxFaceRegionIntersection(extent0, extent2, extent1, ax, az, ay, bx, bz, by, true, Radius, out distance)
                : FindBoxEdgeRegionIntersection(extent1, extent0, extent2, ay, ax, az, by, bx, bz, true, Radius, out distance);
            }
        }
        else if (ay <= extent1)
        {
            retVal = az <= extent2
                ? FindBoxFaceRegionIntersection(extent1, extent2, extent0, ay, az, ax, by, bz, bx, true, Radius, out distance)
                : FindBoxEdgeRegionIntersection(extent0, extent1, extent2, ax, ay, az, bx, by, bz, true, Radius, out distance);
        }
        else
        {
            retVal = az <= extent2
                ? FindBoxEdgeRegionIntersection(extent0, extent2, extent1, ax, az, ay, bx, bz, by, true, Radius, out distance)
                : FindBoxVertexRegionIntersection(extent0, extent1, extent2, ax, ay, az, bx, by, bz, Radius, out distance);
        }

        return retVal != 0 && distance <= _pathLength;
    }

    private bool Intersects(Line<TScalar> line, out TScalar distance)
    {
        distance = TScalar.Zero;
        var orthonormalBasis = new Vector3<TScalar>[]
        {
            Vector3<TScalar>.Transform(Vector3<TScalar>.UnitX, Rotation),
            Vector3<TScalar>.Transform(Vector3<TScalar>.UnitY, Rotation),
            Vector3<TScalar>.Transform(Vector3<TScalar>.UnitZ, Rotation)
        };
        var rSqr = Radius * Radius;
        var diff = line.Position - Position;
        var P = new Vector3<TScalar>(
            Vector3<TScalar>.Dot(orthonormalBasis[1], diff),
            Vector3<TScalar>.Dot(orthonormalBasis[2], diff),
            Vector3<TScalar>.Dot(orthonormalBasis[0], diff));
        var lineDirection = Vector3<TScalar>.Normalize(line.Path);
        var dz = Vector3<TScalar>.Dot(orthonormalBasis[0], lineDirection);

        var hasIntersect1 = false;
        var hasIntersect2 = false;
        if (TScalar.Abs(dz).IsNearlyEqualTo(TScalar.One))
        {
            // the line intersects with both spheres
            return rSqr - (P.X * P.X) - (P.Y * P.Y) >= TScalar.Zero;
        }

        var D = new Vector3<TScalar>(
            Vector3<TScalar>.Dot(orthonormalBasis[1], lineDirection),
            Vector3<TScalar>.Dot(orthonormalBasis[2], lineDirection),
            dz);
        var a0 = (P.X * P.X) + (P.Y * P.Y) - rSqr;
        var a1 = (P.X * D.X) + (P.Y * D.Y);
        var a2 = (D.X * D.X) + (D.Y * D.Y);
        var discr = (a1 * a1) - (a0 * a2);

        if (discr < TScalar.Zero)
        {
            return false;
        }

        if (discr > TScalar.Zero)
        {
            var root = TScalar.Sqrt(discr);
            var inv = TScalar.One / a2;
            var tValue = (-a1 - root) * inv;
            var zValue = P.Z + (tValue * D.Z);
            if (TScalar.Abs(zValue) <= line.Length)
            {
                hasIntersect1 = true;
            }

            tValue = (-a1 + root) * inv;
            zValue = P.Z + (tValue * D.Z);
            if (TScalar.Abs(zValue) <= line.Length)
            {
                if (hasIntersect1)
                {
                    hasIntersect2 = true;
                }
                else
                {
                    hasIntersect1 = true;
                }
            }

            if (hasIntersect2) // the line intersects with the cylinder in two places
            {
                // calculate the distance the starting sphere must travel to encounter the line
                var a = line.GetDistanceTo(new Line<TScalar>(Axis, Position), out _, out _);
                var h = line.GetDistanceTo(_start, out _);
                var totalDistance = TScalar.Sqrt((a * a) - (h * h));
                distance = totalDistance - TScalar.Sqrt((a * a) - rSqr);
                return true;
            }
        }
        else
        {
            var tValue = -a1 / a2;
            var zValue = P.Z + (tValue * D.Z);
            if (TScalar.Abs(zValue) <= line.Length) // the line intersects the cylinder at a single point
            {
                // calculate the distance the starting sphere must travel to encounter the line
                var a = line.GetDistanceTo(new Line<TScalar>(Axis, Position), out _, out _);
                var distance1 = TScalar.Sqrt((a * a) - rSqr);
                var h = line.GetDistanceTo(_start, out _);
                var totalDistance = TScalar.Sqrt((a * a) - (h * h));
                distance = totalDistance - distance1;
                return true;
            }
        }

        // test for intersection with start sphere
        var PZpE = P.Z + line.Length;
        a1 += PZpE * D.Z;
        a0 += PZpE * PZpE;
        discr = (a1 * a1) - a0;
        if (discr > TScalar.Zero)
        {
            var root = TScalar.Sqrt(discr);
            var tValue = -a1 - root;
            var zValue = P.Z + (tValue * D.Z);
            if (zValue <= -line.Length)
            {
                return true;
            }
            tValue = -a1 + root;
            zValue = P.Z + (tValue * D.Z);
            if (zValue <= -line.Length)
            {
                return true;
            }
        }
        else if (discr.IsNearlyZero())
        {
            var tValue = -a1;
            var zValue = P.Z + (tValue * D.Z);
            if (zValue <= -line.Length)
            {
                return true;
            }
        }

        if (hasIntersect1) // line already intersected with the cylinder, no need to check end sphere
        {
            // calculate the distance the starting sphere must travel to encounter the line
            var a = line.GetDistanceTo(new Line<TScalar>(Axis, Position), out _, out _);
            var h = line.GetDistanceTo(_start, out _);
            var totalDistance = TScalar.Sqrt((a * a) - (h * h));
            distance = totalDistance - TScalar.Sqrt((a * a) - rSqr);
            return true;
        }

        // test for intersection with end sphere
        a1 -= NumberValues.Two<TScalar>() * line.Length * D.Z;
        a0 -= NumberValues.Four<TScalar>() * line.Length * P.Z;
        discr = (a1 * a1) - a0;
        if (discr > TScalar.Zero)
        {
            var root = TScalar.Sqrt(discr);
            var tValue = -a1 - root;
            var zValue = P.Z + (tValue * D.Z);
            if (zValue >= line.Length)
            {
                distance = Length;
                return true;
            }

            tValue = -a1 + root;
            zValue = P.Z + (tValue * D.Z);
            if (zValue >= line.Length)
            {
                distance = Length;
                return true;
            }
        }
        else if (discr.IsNearlyZero())
        {
            var tValue = -a1;
            var zValue = P.Z + (tValue * D.Z);
            if (zValue >= line.Length)
            {
                distance = Length;
                return true;
            }
        }

        return false; // no intersections were detected
    }

    private bool Intersects(TScalar otherRadius, Vector3<TScalar> B0, Vector3<TScalar> B1, out TScalar distance)
    {
        var AB = B0 - _start;
        var vab = B1 - B0 - Axis;
        var rab = Radius + otherRadius;
        distance = TScalar.Zero;

        // first check for overlap at the spheres' starting positions
        if (Vector3<TScalar>.Dot(AB, AB) <= rab * rab)
        {
            return true;
        }

        if (Quadratic(
            Vector3<TScalar>.Dot(vab, vab),
            NumberValues.Two<TScalar>() * Vector3<TScalar>.Dot(vab, AB),
            Vector3<TScalar>.Dot(AB, AB) - (rab * rab),
            out distance,
            out var root2))
        {
            if (distance > root2)
            {
                distance = root2;
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Returns a boolean indicating whether the two given shapes are equal.
    /// </summary>
    /// <param name="left">The first shapes to compare.</param>
    /// <param name="right">The second shapes to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the shapes are equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool operator ==(Capsule<TScalar> left, IShape<TScalar> right)
        => left.Equals(right);

    /// <summary>
    /// Returns a boolean indicating whether the two given shapes are not equal.
    /// </summary>
    /// <param name="left">The first shapes to compare.</param>
    /// <param name="right">The second shapes to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the shapes are not equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool operator !=(Capsule<TScalar> left, IShape<TScalar> right)
        => !(left == right);
}
