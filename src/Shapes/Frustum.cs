using System.Numerics;
using System.Text.Json.Serialization;

namespace Tavenem.Mathematics;

/// <summary>
/// Provides information about the properties of a rectangular (view) frustum.
/// </summary>
public readonly struct Frustum<TScalar> : IShape<Frustum<TScalar>, TScalar>
    where TScalar : IFloatingPointIeee754<TScalar>
{
    /// <summary>
    /// The ratio of the horizontal (x-axis) angle to the vertical (y-axis) angle.
    /// </summary>
    public TScalar AspectRatio { get; }

    /// <summary>
    /// The direction and distance of the frustum, from the apex of the pyramid (camera) to the
    /// far clipping plane.
    /// </summary>
    public Vector3<TScalar> Axis { get; }

    /// <summary>
    /// A circular radius which fully contains the shape.
    /// </summary>
    [JsonIgnore]
    public TScalar ContainingRadius { get; }

    /// <summary>
    /// <para>
    /// The positions of the eight corners which form this frustum.
    /// </para>
    /// <para>
    /// The corners are given in an order such that each pair of consecutive corners (as well as
    /// the first and last) form an edge.
    /// </para>
    /// </summary>
    [JsonIgnore]
    public Vector3<TScalar>[] Corners { get; }

    /// <summary>
    /// The vertical (y-axis) angle which defines the pyramidal shape of the frustum.
    /// </summary>
    public TScalar FieldOfViewAngle { get; }

    /// <summary>
    /// The distance from the apex of the pyramid (origin of the view) at which the far
    /// clipping plane occurs.
    /// </summary>
    [JsonIgnore]
    public TScalar FarPlaneDistance { get; }

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
    /// The distance from the apex of the pyramid (origin of the view) at which the near
    /// clipping plane occurs.
    /// </summary>
    public TScalar NearPlaneDistance { get; }

    /// <summary>
    /// <para>
    /// The set of planes which define this frustum.
    /// </para>
    /// <para>
    /// Their order is such that each pair of consecutive planes (as well as the first and last)
    /// form an edge, and every three consecutive planes form a corner (including the first two
    /// with the last, and the last two with the first).
    /// </para>
    /// </summary>
    [JsonIgnore]
    public Plane<TScalar>[] Planes { get; }

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
    public ShapeType ShapeType => ShapeType.Frustum;

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
    /// Initializes a new instance of frustum with the given parameters.
    /// </summary>
    /// <param name="aspectRatio">
    /// The ratio of the horizontal (x-axis) angle to the vertical (y-axis) angle.
    /// </param>
    /// <param name="axis">
    /// The direction and distance of the frustum, from the apex of the pyramid (camera) to the
    /// far clipping plane.
    /// </param>
    /// <param name="fieldOfViewAngle">
    /// The vertical (y-axis) angle which defines the pyramidal shape of the frustum.
    /// </param>
    /// <param name="nearPlaneDistance">
    /// The distance from the apex of the pyramid (origin of the view) at which the near
    /// clipping plane occurs.
    /// </param>
    /// <param name="position">The position of the shape in 3D space.</param>
    /// <param name="rotation">The rotation of this shape in 3D space.</param>
    [JsonConstructor]
    public Frustum(
        TScalar aspectRatio,
        Vector3<TScalar> axis,
        TScalar fieldOfViewAngle,
        TScalar nearPlaneDistance,
        Vector3<TScalar> position,
        Quaternion<TScalar> rotation)
    {
        AspectRatio = aspectRatio;
        Axis = axis;
        FieldOfViewAngle = fieldOfViewAngle;
        NearPlaneDistance = nearPlaneDistance;
        Position = position;
        Rotation = rotation;

        var farPlaneDistSq = Vector3<TScalar>.LengthSquared(Axis);
        FarPlaneDistance = TScalar.Sqrt(farPlaneDistSq);

        var two = NumberValues.Two<TScalar>();
        var tan = TScalar.Tan(fieldOfViewAngle);
        var tanSq4 = tan.Square() * NumberValues.Four<TScalar>();
        var tanAR = tan * aspectRatio;

        Volume = tanSq4
            * aspectRatio
            * ((farPlaneDistSq * FarPlaneDistance) - nearPlaneDistance.Cube())
            / NumberValues.Three<TScalar>();

        SmallestDimension = aspectRatio <= TScalar.One
            ? two * tanAR * nearPlaneDistance
            : two * tan * nearPlaneDistance;

        ContainingRadius = TScalar.Sqrt(
            (FarPlaneDistance - nearPlaneDistance).Square()
            + (tanSq4 * farPlaneDistSq * (TScalar.One + aspectRatio.Square())))
            / two;

        axis = position - (axis / two);
        var basis1 = Vector3<TScalar>.Transform(Vector3<TScalar>.Normalize(axis), rotation);
        Vector3<TScalar> basis2, basis3;
        if (basis1.Z.IsNearlyEqualTo(TScalar.NegativeOne))
        {
            basis2 = new Vector3<TScalar>(TScalar.Zero, TScalar.NegativeOne, TScalar.Zero);
            basis3 = new Vector3<TScalar>(TScalar.NegativeOne, TScalar.Zero, TScalar.Zero);
        }
        else
        {
            var a = TScalar.One / (TScalar.One + basis1.Z);
            var b = -basis1.X * basis1.Y * a;
            basis2 = new Vector3<TScalar>(TScalar.One - (basis1.X.Square() * a), b, -basis1.X);
            basis3 = new Vector3<TScalar>(b, TScalar.One - (basis1.Y.Square() * a), -basis1.Y);
        }
        var farY = basis2 * farPlaneDistSq * tanAR;
        var farZ = basis3 * tan * FarPlaneDistance;
        var nearX = basis1 * nearPlaneDistance;
        var nearY = basis2 * nearPlaneDistance.Square() * tanAR;
        var nearZ = basis3 * tan * nearPlaneDistance;
        Corners = new Vector3<TScalar>[8]
        {
            Position + axis - farY + farZ,
            Position + axis + farY + farZ,
            Position + axis - farY - farZ,
            Position + axis + farY - farZ,
            Position + nearX + nearY - nearZ,
            Position + nearX - nearY - nearZ,
            Position + nearX - nearY + nearZ,
            Position + nearX + nearY + nearZ
        };

        HighestPoint = Corners[0];
        LowestPoint = Corners[0];
        for (var i = 1; i < 8; i++)
        {
            if (Corners[i].Y > HighestPoint.Y)
            {
                HighestPoint = Corners[i];
            }
            if (Corners[i].Y < LowestPoint.Y)
            {
                LowestPoint = Corners[i];
            }
        }

        Planes = new Plane<TScalar>[6]
        {
            Plane<TScalar>.CreateFromVertices(Corners[0], Corners[1], Corners[2]),
            Plane<TScalar>.CreateFromVertices(Corners[0], Corners[1], Corners[4]),
            Plane<TScalar>.CreateFromVertices(Corners[0], Corners[1], Corners[5]),
            Plane<TScalar>.CreateFromVertices(Corners[0], Corners[1], Corners[6]),
            Plane<TScalar>.CreateFromVertices(Corners[0], Corners[1], Corners[7]),
            Plane<TScalar>.CreateFromVertices(Corners[4], Corners[5], Corners[6]),
        };
    }

    /// <summary>
    /// Initializes a new instance of frustum with the given parameters.
    /// </summary>
    /// <param name="aspectRatio">
    /// The ratio of the horizontal (x-axis) angle to the vertical (y-axis) angle.
    /// </param>
    /// <param name="axis">
    /// The direction and distance of the frustum, from the apex of the pyramid (camera) to the
    /// far clipping plane.
    /// </param>
    /// <param name="fieldOfViewAngle">
    /// The vertical (y-axis) angle which defines the pyramidal shape of the frustum.
    /// </param>
    /// <param name="nearPlaneDistance">
    /// The distance from the apex of the pyramid (origin of the view) at which the near
    /// clipping plane occurs.
    /// </param>
    /// <param name="position">The position of the shape in 3D space.</param>
    public Frustum(
        TScalar aspectRatio,
        Vector3<TScalar> axis,
        TScalar fieldOfViewAngle,
        TScalar nearPlaneDistance,
        Vector3<TScalar> position)
        : this(
            aspectRatio,
            axis,
            fieldOfViewAngle,
            nearPlaneDistance,
            position,
            Quaternion<TScalar>.MultiplicativeIdentity)
    { }

    /// <summary>
    /// Initializes a new instance of frustum with the given parameters.
    /// </summary>
    /// <param name="aspectRatio">
    /// The ratio of the horizontal (x-axis) angle to the vertical (y-axis) angle.
    /// </param>
    /// <param name="axis">
    /// The direction and distance of the frustum, from the apex of the pyramid (camera) to the
    /// far clipping plane.
    /// </param>
    /// <param name="fieldOfViewAngle">
    /// The vertical (y-axis) angle which defines the pyramidal shape of the frustum.
    /// </param>
    /// <param name="nearPlaneDistance">
    /// The distance from the apex of the pyramid (origin of the view) at which the near
    /// clipping plane occurs.
    /// </param>
    public Frustum(
        TScalar aspectRatio,
        Vector3<TScalar> axis,
        TScalar fieldOfViewAngle,
        TScalar nearPlaneDistance)
        : this(
            aspectRatio,
            axis,
            fieldOfViewAngle,
            nearPlaneDistance,
            Vector3<TScalar>.Zero,
            Quaternion<TScalar>.MultiplicativeIdentity)
    { }

    /// <summary>
    /// Initializes a new instance of frustum.
    /// </summary>
    public Frustum() : this(
        TScalar.Zero,
        Vector3<TScalar>.Zero,
        TScalar.Zero,
        TScalar.Zero)
    { }

    private static TScalar GetDistance(
        Frustum<TScalar> frustum,
        Vector3<TScalar> cornerA,
        Vector3<TScalar> cornerB,
        Vector3<TScalar> cornerC,
        Vector3<TScalar> cornerD)
    {
        if (Vector3<TScalar>.AreParallel(cornerA - cornerB, cornerC - cornerD))
        {
            return TScalar.PositiveInfinity;
        }
        var normal = Vector3<TScalar>.Normalize(Vector3<TScalar>.Cross(cornerA - cornerB, cornerC - cornerD));
        if (Vector3<TScalar>.Dot(normal, cornerA - frustum.Position) < TScalar.Zero)
        {
            normal = -normal;
        }
        return Vector3<TScalar>.Dot(normal, cornerC - cornerA);
    }

    private static TScalar GetEdgeDistance(Frustum<TScalar> frustum, Cuboid<TScalar> cuboid)
    {
        var distance = TScalar.PositiveInfinity;
        for (var i = 1; i < frustum.Corners.Length; i++)
        {
            var planeA = Plane<TScalar>.CreateFromVertices(
                frustum.Corners[i - 1],
                frustum.Corners[i],
                frustum.Corners[i == frustum.Corners.Length - 1 ? 0 : i + 1]);
            var planeB = Plane<TScalar>.CreateFromVertices(
                frustum.Corners[i],
                frustum.Corners[i == frustum.Corners.Length - 1 ? 0 : i + 1],
                frustum.Corners[i == frustum.Corners.Length - 1 ? 1 : i + 2]);
            for (var j = 1; j < cuboid.Corners.Length; j++)
            {
                var planeC = Plane<TScalar>.CreateFromVertices(
                    cuboid.Corners[i - 1],
                    cuboid.Corners[i],
                    cuboid.Corners[i == cuboid.Corners.Length - 1 ? 0 : i + 1]);
                var planeD = Plane<TScalar>.CreateFromVertices(
                    cuboid.Corners[i],
                    cuboid.Corners[i == cuboid.Corners.Length - 1 ? 0 : i + 1],
                    cuboid.Corners[i == cuboid.Corners.Length - 1 ? 1 : i + 2]);

                if (IsMinkowskiFace(planeA.Normal, planeB.Normal, -planeC.Normal, -planeD.Normal))
                {
                    var separation = GetDistance(
                        frustum,
                        frustum.Corners[i],
                        frustum.Corners[i == frustum.Corners.Length - 1 ? 0 : i + 1],
                        cuboid.Corners[i],
                        cuboid.Corners[i == cuboid.Corners.Length - 1 ? 0 : i + 1]);
                    if (separation < distance)
                    {
                        distance = separation;
                    }
                }
            }
        }
        return distance;
    }

    private static TScalar GetEdgeDistance(Frustum<TScalar> frustumA, Frustum<TScalar> frustumB)
    {
        var distance = TScalar.PositiveInfinity;
        for (var i = 1; i < frustumA.Corners.Length; i++)
        {
            var planeA = Plane<TScalar>.CreateFromVertices(
                frustumA.Corners[i - 1],
                frustumA.Corners[i],
                frustumA.Corners[i == frustumA.Corners.Length - 1 ? 0 : i + 1]);
            var planeB = Plane<TScalar>.CreateFromVertices(
                frustumA.Corners[i],
                frustumA.Corners[i == frustumA.Corners.Length - 1 ? 0 : i + 1],
                frustumA.Corners[i == frustumA.Corners.Length - 1 ? 1 : i + 2]);
            for (var j = 1; j < frustumB.Corners.Length; j++)
            {
                var planeC = Plane<TScalar>.CreateFromVertices(
                    frustumB.Corners[i - 1],
                    frustumB.Corners[i],
                    frustumB.Corners[i == frustumB.Corners.Length - 1 ? 0 : i + 1]);
                var planeD = Plane<TScalar>.CreateFromVertices(
                    frustumB.Corners[i],
                    frustumB.Corners[i == frustumB.Corners.Length - 1 ? 0 : i + 1],
                    frustumB.Corners[i == frustumB.Corners.Length - 1 ? 1 : i + 2]);

                if (IsMinkowskiFace(planeA.Normal, planeB.Normal, -planeC.Normal, -planeD.Normal))
                {
                    var separation = GetDistance(
                        frustumA,
                        frustumA.Corners[i],
                        frustumA.Corners[i == frustumA.Corners.Length - 1 ? 0 : i + 1],
                        frustumB.Corners[i],
                        frustumB.Corners[i == frustumB.Corners.Length - 1 ? 0 : i + 1]);
                    if (separation < distance)
                    {
                        distance = separation;
                    }
                }
            }
        }
        return distance;
    }

    private static TScalar GetFaceDistance(Plane<TScalar>[] planes, Vector3<TScalar>[] corners)
    {
        var distance = TScalar.NegativeInfinity;
        for (var i = 0; i < planes.Length; i++)
        {
            distance = TScalar.Max(
                distance,
                Plane<TScalar>.DotCoordinate(
                    planes[i],
                    GetSupport(corners, -planes[i].Normal)));
        }
        return distance;
    }

    private static Vector3<TScalar> GetSupport(Vector3<TScalar>[] corners, Vector3<TScalar> normal)
    {
        var maxProjection = TScalar.NegativeInfinity;
        var index = -1;
        for (var i = 0; i < corners.Length; i++)
        {
            var projection = Vector3<TScalar>.Dot(corners[i], normal);
            if (projection > maxProjection)
            {
                maxProjection = projection;
                index = i;
            }
        }
        return corners[index];
    }

    private static bool IsMinkowskiFace(Vector3<TScalar> a, Vector3<TScalar> b, Vector3<TScalar> c, Vector3<TScalar> d)
    {
        var bxa = Vector3<TScalar>.Cross(b, a);
        var dxc = Vector3<TScalar>.Cross(d, c);
        var cba = Vector3<TScalar>.Dot(c, bxa);
        var dba = Vector3<TScalar>.Dot(d, bxa);
        var adc = Vector3<TScalar>.Dot(a, dxc);
        var bdc = Vector3<TScalar>.Dot(b, dxc);
        return cba * dba < TScalar.Zero
            && adc * bdc < TScalar.Zero
            && cba * bdc > TScalar.Zero;
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
    public Frustum<TScalar> GetTypedCloneAtPosition(Vector3<TScalar> position) => new(
        AspectRatio,
        Axis,
        FieldOfViewAngle,
        NearPlaneDistance,
        position,
        Rotation);

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="IShape{TScalar}.Rotation"/> set to the given
    /// value.
    /// </summary>
    /// <param name="rotation">The new <see cref="IShape{TScalar}.Rotation"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="IShape{TScalar}.Rotation"/> set to the given value.
    /// </returns>
    public Frustum<TScalar> GetTypedCloneWithRotation(Quaternion<TScalar> rotation) => new(
        AspectRatio,
        Axis,
        FieldOfViewAngle,
        NearPlaneDistance,
        Position,
        rotation);

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
    public Frustum<TScalar> GetTypedScaledByDimension(TScalar factor)
    {
        if (factor < TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
        }

        return new Frustum<TScalar>(
            AspectRatio,
            Axis * factor,
            FieldOfViewAngle,
            NearPlaneDistance * factor,
            Position,
            Rotation);
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
    public Frustum<TScalar> GetTypedScaledByVolume(TScalar factor)
    {
        if (factor < TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
        }

        if (factor == TScalar.Zero)
        {
            return new Frustum<TScalar>(
                TScalar.Zero,
                Vector3<TScalar>.Zero,
                TScalar.Zero,
                TScalar.Zero,
                Position);
        }
        else
        {
            var multiplier = TScalar.Cbrt(factor);
            return new Frustum<TScalar>(
                AspectRatio,
                Axis * multiplier,
                FieldOfViewAngle,
                NearPlaneDistance * multiplier,
                Position,
                Rotation);
        }
    }

    /// <summary>
    /// Determines whether the specified frustum is equivalent to the current object.
    /// </summary>
    /// <param name="other">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true"/> if the specified frustum is equivalent to the
    /// current object; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(Frustum<TScalar> other) => AspectRatio == other.AspectRatio
        && Axis.Equals(other.Axis)
        && FieldOfViewAngle == other.FieldOfViewAngle
        && NearPlaneDistance == other.NearPlaneDistance
        && Position.Equals(other.Position)
        && Rotation.Equals(other.Rotation);

    /// <summary>
    /// Determines whether the specified <see cref="IShape{TScalar}"/> is equivalent to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true"/> if the specified <see cref="IShape{TScalar}"/> is equivalent to the
    /// current object; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(IShape<TScalar>? obj) => obj is Frustum<TScalar> other && Equals(other);

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    /// <see langword="true"/> if obj and this instance are the same type and represent
    /// the same value; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(object? obj) => obj is Frustum<TScalar> shape && Equals(shape);

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    public override int GetHashCode()
        => HashCode.Combine(AspectRatio, Axis, FieldOfViewAngle, NearPlaneDistance, Position, Rotation);

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
        if (shape is Cuboid<TScalar> cuboid)
        {
            return Intersects(cuboid);
        }
        if (shape is Cylinder<TScalar> cylinder)
        {
            return Intersects(cylinder);
        }
        if (shape is Frustum<TScalar> frustum)
        {
            return Intersects(frustum);
        }
        if (shape is HollowSphere<TScalar> hollowSphere)
        {
            var dist = GetPointDistance(hollowSphere.Position);
            return dist <= hollowSphere.ContainingRadius
                && dist + hollowSphere.InnerRadius < ContainingRadius;
        }
        if (shape is Line<TScalar> line)
        {
            return Intersects(line);
        }
        if (shape is Sphere<TScalar> sphere)
        {
            return GetPointDistance(sphere.Position) <= sphere.ContainingRadius;
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
        if (!new Sphere<TScalar>(ContainingRadius, Position)
            .IsPointWithin(point))
        {
            return false;
        }
        for (var i = 0; i < 6; i++)
        {
            if (TScalar.Sign(Plane<TScalar>.DotCoordinate(Planes[i], point))
                != TScalar.Sign(Plane<TScalar>.DotCoordinate(Planes[i], Position)))
            {
                return false;
            }
        }
        return true;
    }

    private TScalar GetPointDistance(Vector3<TScalar> point)
    {
        var maxDistance = TScalar.NegativeInfinity;
        for (var i = 0; i < 6; i++)
        {
            var distance = Plane<TScalar>.DotCoordinate(Planes[i], point);
            distance = TScalar.Sign(distance) == TScalar.Sign(Plane<TScalar>.DotCoordinate(Planes[i], Position))
                ? -TScalar.Abs(distance)
                : TScalar.Abs(distance);
            maxDistance = TScalar.Max(maxDistance, distance);
        }
        return maxDistance;
    }

    private bool Intersects(Cone<TScalar> cone)
    {
        var line = new Line<TScalar>(cone.Axis);
        if (Intersects(line))
        {
            return true;
        }
        for (var i = 1; i < Corners.Length; i++)
        {
            var edge = new Line<TScalar>(Corners[i - 1], Corners[i]);
            var distance = line.GetDistanceTo(edge, out var closestPoint, out _);
            if (distance <= cone.GetRadiusAtPoint(closestPoint))
            {
                return true;
            }
        }
        return false;
    }

    private bool Intersects(Capsule<TScalar> capsule)
    {
        for (var i = 1; i < Corners.Length; i++)
        {
            var separation = GetDistance(
                this,
                Corners[i],
                Corners[i == Corners.Length - 1 ? 0 : i + 1],
                capsule._start,
                capsule._end);
            if (separation <= capsule.Radius)
            {
                return true;
            }
        }
        return false;
    }

    private bool Intersects(Cylinder<TScalar> cylinder)
    {
        for (var i = 1; i < Corners.Length; i++)
        {
            var separation = GetDistance(
                this,
                Corners[i],
                Corners[i == Corners.Length - 1 ? 0 : i + 1],
                cylinder._start,
                cylinder._end);
            if (separation <= cylinder.Radius)
            {
                return true;
            }
        }
        return false;
    }

    private bool Intersects(Line<TScalar> line)
    {
        var pointA = line.LowestPoint;
        var pointB = line.HighestPoint;
        for (var i = 0; i < 6; i++)
        {
            var distA = GetPointDistance(pointA);
            var distB = GetPointDistance(pointB);
            if (distA < TScalar.Zero && distB < TScalar.Zero)
            {
                return false;
            }
            if (distA * distB > TScalar.Zero)
            {
                continue;
            }
            var ratio = TScalar.Abs(distA) / TScalar.Abs(distB);
            var intersect = Vector3<TScalar>.Lerp(pointA, pointB, ratio);
            if (distA < TScalar.Zero)
            {
                pointA = intersect;
            }
            else
            {
                pointB = intersect;
            }
        }
        return true;
    }

    private bool Intersects(Cuboid<TScalar> cuboid)
    {
        for (var i = 0; i < 8; i++)
        {
            if (IsPointWithin(cuboid.Corners[i]))
            {
                return true;
            }
        }

        if (GetFaceDistance(Planes, cuboid.Corners) > TScalar.Zero)
        {
            return false;
        }

        var planes = new Plane<TScalar>[6]
        {
            Plane<TScalar>.CreateFromVertices(cuboid.Corners[0], cuboid.Corners[1], cuboid.Corners[2]),
            Plane<TScalar>.CreateFromVertices(cuboid.Corners[1], cuboid.Corners[2], cuboid.Corners[3]),
            Plane<TScalar>.CreateFromVertices(cuboid.Corners[2], cuboid.Corners[3], cuboid.Corners[4]),
            Plane<TScalar>.CreateFromVertices(cuboid.Corners[3], cuboid.Corners[4], cuboid.Corners[5]),
            Plane<TScalar>.CreateFromVertices(cuboid.Corners[4], cuboid.Corners[5], cuboid.Corners[0]),
            Plane<TScalar>.CreateFromVertices(cuboid.Corners[5], cuboid.Corners[0], cuboid.Corners[1]),
        };
        return GetFaceDistance(planes, Corners) <= TScalar.Zero
            && GetEdgeDistance(this, cuboid) <= TScalar.Zero;
    }

    private bool Intersects(Frustum<TScalar> frustum)
    {
        for (var i = 0; i < 8; i++)
        {
            if (IsPointWithin(frustum.Corners[i]))
            {
                return true;
            }
        }

        return GetFaceDistance(Planes, frustum.Corners) <= TScalar.Zero
            && GetFaceDistance(frustum.Planes, Corners) <= TScalar.Zero
            && GetEdgeDistance(this, frustum) <= TScalar.Zero;
    }

    /// <summary>
    /// Returns a boolean indicating whether the two given shapes are equal.
    /// </summary>
    /// <param name="left">The first shapes to compare.</param>
    /// <param name="right">The second shapes to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the shapes are equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool operator ==(Frustum<TScalar> left, Frustum<TScalar> right) => left.Equals(right);

    /// <summary>
    /// Returns a boolean indicating whether the two given shapes are not equal.
    /// </summary>
    /// <param name="left">The first shapes to compare.</param>
    /// <param name="right">The second shapes to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the shapes are not equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool operator !=(Frustum<TScalar> left, Frustum<TScalar> right) => !(left == right);
}
