using System;
using System.Runtime.Serialization;

namespace Tavenem.Mathematics.Decimals
{
    /// <summary>
    /// Provides information about the properties of a rectangular (view) frustum.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Frustum : IShape, IEquatable<Frustum>, ISerializable
    {
        /// <summary>
        /// The ratio of the horizontal (x-axis) angle to the vertical (y-axis) angle.
        /// </summary>
        [DataMember(Order = 1)]
        public decimal AspectRatio { get; }

        /// <summary>
        /// The direction and distance of the frustum, from the apex of the pyramid (camera) to the
        /// far clipping plane.
        /// </summary>
        [DataMember(Order = 2)]
        public Vector3 Axis { get; }

        /// <summary>
        /// A circular radius which fully contains the shape.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public decimal ContainingRadius { get; }

        /// <summary>
        /// <para>
        /// The positions of the eight corners which form this frustum.
        /// </para>
        /// <para>
        /// The corners are given in an order such that each pair of consecutive corners (as well as
        /// the first and last) form an edge.
        /// </para>
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Vector3[] Corners { get; }

        /// <summary>
        /// The vertical (y-axis) angle which defines the pyramidal shape of the frustum.
        /// </summary>
        [DataMember(Order = 3)]
        public decimal FieldOfViewAngle { get; }

        /// <summary>
        /// The distance from the apex of the pyramid (origin of the view) at which the far
        /// clipping plane occurs.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public decimal FarPlaneDistance { get; }

        /// <summary>
        /// The point on this shape highest on the Y axis.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Vector3 HighestPoint { get; }

        /// <summary>
        /// The point on this shape lowest on the Y axis.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Vector3 LowestPoint { get; }

        /// <summary>
        /// The distance from the apex of the pyramid (origin of the view) at which the near
        /// clipping plane occurs.
        /// </summary>
        [DataMember(Order = 4)]
        public decimal NearPlaneDistance { get; }

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
        [System.Text.Json.Serialization.JsonIgnore]
        public Plane[] Planes { get; }

        /// <summary>
        /// The position of the shape in 3D space.
        /// </summary>
        [DataMember(Order = 5)]
        public Vector3 Position { get; }

        /// <summary>
        /// The rotation of this shape in 3D space.
        /// </summary>
        [DataMember(Order = 6)]
        public Quaternion Rotation { get; }

        /// <summary>
        /// Identifies the type of this <see cref="IShape"/>.
        /// </summary>
        [DataMember(Order = 7)]
        public ShapeType ShapeType => ShapeType.Frustum;

        /// <summary>
        /// The length of the shortest of the three dimensions of this shape.
        /// </summary>
        /// <remarks>
        /// Note: this is not the length of smallest possible cross-section, but of the total length
        /// of the shape along any of the three primary axes of 3D space, prior to any rotation.
        /// </remarks>
        [System.Text.Json.Serialization.JsonIgnore]
        public decimal SmallestDimension { get; }

        /// <summary>
        /// The total volume of the shape. Read-only; set by setting the aspect ratio, axis, field
        /// of view, and near plane distance.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public decimal Volume { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Frustum"/>.
        /// </summary>
        public Frustum() : this(0, Vector3.Zero, 0, 0) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Frustum"/> with the given parameters.
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
        public Frustum(decimal aspectRatio, Vector3 axis, decimal fieldOfViewAngle, decimal nearPlaneDistance)
            : this(aspectRatio, axis, fieldOfViewAngle, nearPlaneDistance, Vector3.Zero, Quaternion.Identity) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Frustum"/> with the given parameters.
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
        public Frustum(decimal aspectRatio, Vector3 axis, decimal fieldOfViewAngle, decimal nearPlaneDistance, Vector3 position)
            : this(aspectRatio, axis, fieldOfViewAngle, nearPlaneDistance, position, Quaternion.Identity) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Frustum"/> with the given parameters.
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
        [System.Text.Json.Serialization.JsonConstructor]
        public Frustum(decimal aspectRatio, Vector3 axis, decimal fieldOfViewAngle, decimal nearPlaneDistance, Vector3 position, Quaternion rotation)
        {
            AspectRatio = aspectRatio;
            Axis = axis;
            FieldOfViewAngle = fieldOfViewAngle;
            NearPlaneDistance = nearPlaneDistance;
            Position = position;
            Rotation = rotation;

            var farPlaneDistSq = Axis.LengthSquared();
            FarPlaneDistance = Axis.Length();

            var tan = (decimal)Math.Tan((double)fieldOfViewAngle);
            var tanSq4 = tan.Square() * 4;
            var tanAR = tan * aspectRatio;

            Volume = tanSq4 * aspectRatio * ((farPlaneDistSq * FarPlaneDistance) - nearPlaneDistance.Cube()) / 3;

            SmallestDimension = aspectRatio <= 1
                ? 2 * tanAR * nearPlaneDistance
                : 2 * tan * nearPlaneDistance;

            ContainingRadius = (decimal)Math.Sqrt((double)((FarPlaneDistance - nearPlaneDistance).Square() + (tanSq4 * farPlaneDistSq * (1 + aspectRatio.Square())))) / 2;

            axis = position - (axis / 2);
            var basis1 = axis.Normalize().Transform(rotation);
            Vector3 basis2, basis3;
            if (basis1.Z == -1)
            {
                basis2 = new Vector3(0, -1, 0);
                basis3 = new Vector3(-1, 0, 0);
            }
            else
            {
                var a = 1.0m / (1.0m + basis1.Z);
                var b = -basis1.X * basis1.Y * a;
                basis2 = new Vector3(1 - (basis1.X.Square() * a), b, -basis1.X);
                basis3 = new Vector3(b, 1 - (basis1.Y.Square() * a), -basis1.Y);
            }
            var farY = basis2 * farPlaneDistSq * tanAR;
            var farZ = basis3 * tan * FarPlaneDistance;
            var nearX = basis1 * nearPlaneDistance;
            var nearY = basis2 * nearPlaneDistance.Square() * tanAR;
            var nearZ = basis3 * tan * nearPlaneDistance;
            Corners = new Vector3[8]
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

            Planes = new Plane[6]
            {
                Plane.CreateFromVertices(Corners[0], Corners[1], Corners[2]),
                Plane.CreateFromVertices(Corners[0], Corners[1], Corners[4]),
                Plane.CreateFromVertices(Corners[0], Corners[1], Corners[5]),
                Plane.CreateFromVertices(Corners[0], Corners[1], Corners[6]),
                Plane.CreateFromVertices(Corners[0], Corners[1], Corners[7]),
                Plane.CreateFromVertices(Corners[4], Corners[5], Corners[6]),
            };
        }

        private Frustum(SerializationInfo info, StreamingContext context) : this(
            (decimal?)info.GetValue(nameof(AspectRatio), typeof(decimal)) ?? default,
            (Vector3?)info.GetValue(nameof(Axis), typeof(Vector3)) ?? default,
            (decimal?)info.GetValue(nameof(FieldOfViewAngle), typeof(decimal)) ?? default,
            (decimal?)info.GetValue(nameof(NearPlaneDistance), typeof(decimal)) ?? default,
            (Vector3?)info.GetValue(nameof(Position), typeof(Vector3)) ?? default,
            (Quaternion?)info.GetValue(nameof(Rotation), typeof(Quaternion)) ?? default)
        { }

        private static decimal GetDistance(Frustum frustum, Vector3 cornerA, Vector3 cornerB, Vector3 cornerC, Vector3 cornerD)
        {
            if (Vector3.AreParallel(cornerA - cornerB, cornerC - cornerD))
            {
                return decimal.MaxValue;
            }
            var normal = (cornerA - cornerB).Cross(cornerC - cornerD).Normalize();
            if (normal.Dot(cornerA - frustum.Position) < 0)
            {
                normal = -normal;
            }
            return normal.Dot(cornerC - cornerA);
        }

        private static decimal GetEdgeDistance(Frustum frustum, Cuboid cuboid)
        {
            var distance = decimal.MaxValue;
            for (var i = 1; i < frustum.Corners.Length; i++)
            {
                var planeA = Plane.CreateFromVertices(
                    frustum.Corners[i - 1],
                    frustum.Corners[i],
                    frustum.Corners[i == frustum.Corners.Length - 1 ? 0 : i + 1]);
                var planeB = Plane.CreateFromVertices(
                    frustum.Corners[i],
                    frustum.Corners[i == frustum.Corners.Length - 1 ? 0 : i + 1],
                    frustum.Corners[i == frustum.Corners.Length - 1 ? 1 : i + 2]);
                for (var j = 1; j < cuboid.Corners.Length; j++)
                {
                    var planeC = Plane.CreateFromVertices(
                        cuboid.Corners[i - 1],
                        cuboid.Corners[i],
                        cuboid.Corners[i == cuboid.Corners.Length - 1 ? 0 : i + 1]);
                    var planeD = Plane.CreateFromVertices(
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

        private static decimal GetEdgeDistance(Frustum frustumA, Frustum frustumB)
        {
            var distance = decimal.MaxValue;
            for (var i = 1; i < frustumA.Corners.Length; i++)
            {
                var planeA = Plane.CreateFromVertices(
                    frustumA.Corners[i - 1],
                    frustumA.Corners[i],
                    frustumA.Corners[i == frustumA.Corners.Length - 1 ? 0 : i + 1]);
                var planeB = Plane.CreateFromVertices(
                    frustumA.Corners[i],
                    frustumA.Corners[i == frustumA.Corners.Length - 1 ? 0 : i + 1],
                    frustumA.Corners[i == frustumA.Corners.Length - 1 ? 1 : i + 2]);
                for (var j = 1; j < frustumB.Corners.Length; j++)
                {
                    var planeC = Plane.CreateFromVertices(
                        frustumB.Corners[i - 1],
                        frustumB.Corners[i],
                        frustumB.Corners[i == frustumB.Corners.Length - 1 ? 0 : i + 1]);
                    var planeD = Plane.CreateFromVertices(
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

        private static decimal GetFaceDistance(Plane[] planes, Vector3[] corners)
        {
            var distance = decimal.MinValue;
            for (var i = 0; i < planes.Length; i++)
            {
                distance = Math.Max(distance, planes[i].DotCoordinate(GetSupport(corners, -planes[i].Normal)));
            }
            return distance;
        }

        private static Vector3 GetSupport(Vector3[] corners, Vector3 normal)
        {
            var maxProjection = decimal.MinValue;
            var index = -1;
            for (var i = 0; i < corners.Length; i++)
            {
                var projection = corners[i].Dot(normal);
                if (projection > maxProjection)
                {
                    maxProjection = projection;
                    index = i;
                }
            }
            return corners[index];
        }

        private static bool IsMinkowskiFace(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
        {
            var bxa = b.Cross(a);
            var dxc = d.Cross(c);
            var cba = c.Dot(bxa);
            var dba = d.Dot(bxa);
            var adc = a.Dot(dxc);
            var bdc = b.Dot(dxc);
            return cba * dba < 0
                && adc * bdc < 0
                && cba * bdc > 0;
        }

        /// <summary>
        /// Gets a deep clone of this instance with its <see cref="Position"/> set to the given
        /// value.
        /// </summary>
        /// <param name="position">The new <see cref="Position"/> for the clone.</param>
        /// <returns>A deep clone of this instance with its <see cref="Position"/> set to the given
        /// value.</returns>
        public IShape GetCloneAtPosition(Vector3 position) => GetCopyAtPosition(position);

        /// <summary>
        /// Gets a deep clone of this instance with its <see cref="Rotation"/> set to the given
        /// value.
        /// </summary>
        /// <param name="rotation">The new <see cref="Rotation"/> for the clone.</param>
        /// <returns>A deep clone of this instance with its <see cref="Rotation"/> set to the given
        /// value.</returns>
        public IShape GetCloneWithRotation(Quaternion rotation) => GetCopyWithRotation(rotation);

        /// <summary>
        /// Gets a deep clone of this instance with its <see cref="Position"/> set to the given
        /// value.
        /// </summary>
        /// <param name="position">The new <see cref="Position"/> for the clone.</param>
        /// <returns>A deep clone of this instance with its <see cref="Position"/> set to the given
        /// value.</returns>
        public Frustum GetCopyAtPosition(Vector3 position) => new(AspectRatio, Axis, FieldOfViewAngle, NearPlaneDistance, position, Rotation);

        /// <summary>
        /// Gets a deep clone of this instance with its <see cref="Rotation"/> set to the given
        /// value.
        /// </summary>
        /// <param name="rotation">The new <see cref="Rotation"/> for the clone.</param>
        /// <returns>A deep clone of this instance with its <see cref="Rotation"/> set to the given
        /// value.</returns>
        public Frustum GetCopyWithRotation(Quaternion rotation) => new(AspectRatio, Axis, FieldOfViewAngle, NearPlaneDistance, Position, rotation);

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if obj and this instance are the same type and represent
        /// the same value; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object? obj) => obj is Frustum shape && Equals(shape);

        /// <summary>
        /// Determines whether the specified <see cref="IShape"/> is equivalent to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="IShape"/> is equivalent to the
        /// current object; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(IShape? obj) => obj is Frustum other && Equals(other);

        /// <summary>
        /// Determines whether the specified <see cref="Frustum"/> is equivalent to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="Frustum"/> is equivalent to the
        /// current object; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(Frustum? other) => other is not null
            && AspectRatio == other.AspectRatio
            && Axis.Equals(other.Axis)
            && FieldOfViewAngle == other.FieldOfViewAngle
            && NearPlaneDistance == other.NearPlaneDistance
            && Position.Equals(other.Position)
            && Rotation.Equals(other.Rotation);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode() => HashCode.Combine(AspectRatio, Axis, FieldOfViewAngle, NearPlaneDistance, Position, Rotation);

        /// <summary>Populates a <see cref="SerializationInfo"></see> with the data needed to
        /// serialize the target object.</summary>
        /// <param name="info">The <see cref="SerializationInfo"></see> to populate with
        /// data.</param>
        /// <param name="context">The destination (see <see cref="StreamingContext"></see>) for this
        /// serialization.</param>
        /// <exception cref="System.Security.SecurityException">The caller does not have the
        /// required permission.</exception>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(AspectRatio), AspectRatio);
            info.AddValue(nameof(Axis), Axis);
            info.AddValue(nameof(FieldOfViewAngle), FieldOfViewAngle);
            info.AddValue(nameof(NearPlaneDistance), NearPlaneDistance);
            info.AddValue(nameof(Position), Position);
            info.AddValue(nameof(Rotation), Rotation);
        }

        /// <summary>
        /// Determines if this instance intersects with the given <see cref="IShape"/>.
        /// </summary>
        /// <param name="shape">The <see cref="IShape"/> to check for intersection with this
        /// instance.</param>
        /// <returns><see langword="true"/> if this instance intersects with the given <see
        /// cref="IShape"/>; otherwise <see langword="false"/>.</returns>
        public bool Intersects(IShape shape)
        {
            if (shape is SinglePoint point)
            {
                return IsPointWithin(point.Position);
            }
            // First-pass exclusion based on containing radii.
            if (Vector3.Distance(Position, shape.Position) > shape.ContainingRadius + ContainingRadius)
            {
                return false;
            }
            if (shape is Capsule capsule)
            {
                return Intersects(capsule);
            }
            if (shape is Cone cone)
            {
                return Intersects(cone);
            }
            if (shape is Cuboid cuboid)
            {
                return Intersects(cuboid);
            }
            if (shape is Cylinder cylinder)
            {
                return Intersects(cylinder);
            }
            if (shape is Frustum frustum)
            {
                return Intersects(frustum);
            }
            if (shape is HollowSphere hollowSphere)
            {
                var dist = GetPointDistance(hollowSphere.Position);
                return dist <= hollowSphere.ContainingRadius
                    && dist + hollowSphere.InnerRadius < ContainingRadius;
            }
            if (shape is Line line)
            {
                return Intersects(line);
            }
            if (shape is Sphere sphere)
            {
                return GetPointDistance(sphere.Position) <= sphere.ContainingRadius;
            }
            return shape.Intersects(this);
        }

        /// <summary>
        /// Determines if a given point lies within this shape.
        /// </summary>
        /// <param name="point">A point in the same 3-D space as this shape.</param>
        /// <returns>True if the point is within (or tangent to) this shape.</returns>
        public bool IsPointWithin(Vector3 point)
        {
            if (!new Sphere(ContainingRadius, Position).IsPointWithin(point))
            {
                return false;
            }
            for (var i = 0; i < 6; i++)
            {
                if (Math.Sign(Planes[i].DotCoordinate(point)) != Math.Sign(Planes[i].DotCoordinate(Position)))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Scales this <see cref="IShape"/>'s dimensions such that its dimensions will be
        /// multiplied by the given factor.
        /// </summary>
        /// <param name="factor">The amount by which to scale this <see cref="IShape"/>'s
        /// size.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="factor"/> must be &gt;= 0.
        /// </exception>
        public IShape ScaleByDimension(decimal factor)
        {
            if (factor < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
            }

            return new Frustum(AspectRatio, Axis * factor, FieldOfViewAngle, NearPlaneDistance * factor, Position, Rotation);
        }

        /// <summary>
        /// Scales this <see cref="IShape"/>'s dimensions such that its volume will be multiplied by
        /// the given factor.
        /// </summary>
        /// <param name="factor">The amount by which to scale this <see cref="IShape"/>'s volume.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="factor"/> must be &gt;= 0.
        /// </exception>
        public IShape ScaleVolume(decimal factor)
        {
            if (factor < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
            }

            if (factor == 0)
            {
                return new Frustum(0, Vector3.Zero, 0, 0, Position);
            }
            else
            {
                var multiplier = (decimal)Math.Pow((double)factor, 1.0 / 3.0);
                return new Frustum(AspectRatio, Axis * multiplier, FieldOfViewAngle, NearPlaneDistance * multiplier, Position, Rotation);
            }
        }

        private decimal GetPointDistance(Vector3 point)
        {
            var maxDistance = decimal.MinValue;
            for (var i = 0; i < 6; i++)
            {
                var distance = Planes[i].DotCoordinate(point);
                distance = Math.Sign(distance) == Math.Sign(Planes[i].DotCoordinate(Position))
                    ? -Math.Abs(distance)
                    : Math.Abs(distance);
                maxDistance = Math.Max(maxDistance, distance);
            }
            return maxDistance;
        }

        private bool Intersects(Cone cone)
        {
            var line = new Line(cone.Axis);
            if (Intersects(line))
            {
                return true;
            }
            for (var i = 1; i < Corners.Length; i++)
            {
                var edge = new Line(Corners[i - 1], Corners[i]);
                var distance = line.GetDistanceTo(edge, out var closestPoint, out _);
                if (distance <= cone.GetRadiusAtPoint(closestPoint))
                {
                    return true;
                }
            }
            return false;
        }

        private bool Intersects(Capsule capsule)
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

        private bool Intersects(Cylinder cylinder)
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

        private bool Intersects(Line line)
        {
            var pointA = line.LowestPoint;
            var pointB = line.HighestPoint;
            for (var i = 0; i < 6; i++)
            {
                var distA = GetPointDistance(pointA);
                var distB = GetPointDistance(pointB);
                if (distA < 0 && distB < 0)
                {
                    return false;
                }
                if (distA * distB > 0)
                {
                    continue;
                }
                var ratio = Math.Abs(distA) / Math.Abs(distB);
                var intersect = Vector3.Lerp(pointA, pointB, ratio);
                if (distA < 0)
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

        private bool Intersects(Cuboid cuboid)
        {
            for (var i = 0; i < 8; i++)
            {
                if (IsPointWithin(cuboid.Corners[i]))
                {
                    return true;
                }
            }

            var distance = GetFaceDistance(Planes, cuboid.Corners);
            if (distance > 0)
            {
                return false;
            }

            var planes = new Plane[6]
            {
                Plane.CreateFromVertices(cuboid.Corners[0], cuboid.Corners[1], cuboid.Corners[2]),
                Plane.CreateFromVertices(cuboid.Corners[1], cuboid.Corners[2], cuboid.Corners[3]),
                Plane.CreateFromVertices(cuboid.Corners[2], cuboid.Corners[3], cuboid.Corners[4]),
                Plane.CreateFromVertices(cuboid.Corners[3], cuboid.Corners[4], cuboid.Corners[5]),
                Plane.CreateFromVertices(cuboid.Corners[4], cuboid.Corners[5], cuboid.Corners[0]),
                Plane.CreateFromVertices(cuboid.Corners[5], cuboid.Corners[0], cuboid.Corners[1]),
            };
            distance = GetFaceDistance(planes, Corners);
            if (distance > 0)
            {
                return false;
            }

            distance = GetEdgeDistance(this, cuboid);
            return distance <= 0;
        }

        private bool Intersects(Frustum frustum)
        {
            for (var i = 0; i < 8; i++)
            {
                if (IsPointWithin(frustum.Corners[i]))
                {
                    return true;
                }
            }

            var distance = GetFaceDistance(Planes, frustum.Corners);
            if (distance > 0)
            {
                return false;
            }

            distance = GetFaceDistance(frustum.Planes, Corners);
            if (distance > 0)
            {
                return false;
            }

            distance = GetEdgeDistance(this, frustum);
            return distance <= 0;
        }

        /// <summary>
        /// Returns a boolean indicating whether the two given shapes are equal.
        /// </summary>
        /// <param name="left">The first shape to compare.</param>
        /// <param name="right">The second shape to compare.</param>
        /// <returns>True if the shapes are equal; False otherwise.</returns>
        public static bool operator ==(Frustum left, Frustum right) => left.Equals(right);

        /// <summary>
        /// Returns a boolean indicating whether the two given shapes are not equal.
        /// </summary>
        /// <param name="left">The first shape to compare.</param>
        /// <param name="right">The second shape to compare.</param>
        /// <returns>True if the shapes are not equal; False if they are equal.</returns>
        public static bool operator !=(Frustum left, Frustum right) => !(left == right);
    }
}
