using System;
using System.Runtime.Serialization;

namespace Tavenem.Mathematics.Decimals
{
    /// <summary>
    /// Provides information about the properties of a capsule (swept sphere).
    /// </summary>
    [Serializable]
    [DataContract]
    public class Capsule : IShape, IEquatable<Capsule>, ISerializable
    {
        internal readonly Vector3 _start;
        internal readonly Vector3 _end;
        private readonly decimal _pathLength;

        /// <summary>
        /// The axis of the <see cref="Capsule"/>.
        /// </summary>
        [DataMember(Order = 1)]
        public Vector3 Axis { get; }

        /// <summary>
        /// A circular radius which fully contains the shape.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public decimal ContainingRadius { get; }

        /// <summary>
        /// The point on this shape highest on the Y axis.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Vector3 HighestPoint { get; }

        /// <summary>
        /// The total length of this <see cref="Capsule"/>.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public decimal Length { get; }

        /// <summary>
        /// The point on this shape lowest on the Y axis.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Vector3 LowestPoint { get; }

        /// <summary>
        /// The position of the shape in 3D space.
        /// </summary>
        [DataMember(Order = 2)]
        public Vector3 Position { get; }

        /// <summary>
        /// The radius of the <see cref="Capsule"/>.
        /// </summary>
        [DataMember(Order = 3)]
        public decimal Radius { get; }

        /// <summary>
        /// The rotation of this shape in 3D space. Always <see cref="Quaternion.Identity"/> for
        /// <see cref="Capsule"/>, whose <see cref="Axis"/> defines its orientation.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Quaternion Rotation => Quaternion.Identity;

        /// <summary>
        /// Identifies the type of this <see cref="IShape"/>.
        /// </summary>
        [DataMember(Order = 4)]
        public ShapeType ShapeType => ShapeType.Capsule;

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
        /// The total volume of the shape. Read-only; set by setting the axis and radius.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public decimal Volume { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Capsule"/>.
        /// </summary>
        public Capsule() : this(Vector3.Zero, 0) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Capsule"/> with the given parameters.
        /// </summary>
        /// <param name="axis">The axis of the <see cref="Capsule"/>.</param>
        /// <param name="radius">The radius of the <see cref="Capsule"/>.</param>
        public Capsule(Vector3 axis, decimal radius) : this(axis, radius, Vector3.Zero) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Capsule"/> with the given parameters.
        /// </summary>
        /// <param name="axis">The axis of the <see cref="Capsule"/>.</param>
        /// <param name="radius">The radius of the <see cref="Capsule"/>.</param>
        /// <param name="position">The position of the shape in 3D space.</param>
        [System.Text.Json.Serialization.JsonConstructor]
        public Capsule(Vector3 axis, decimal radius, Vector3 position)
        {
            Axis = axis;
            Position = position;
            Radius = radius;

            _pathLength = axis.Length();
            Length = _pathLength + (Radius * 2);

            var halfHeight = _pathLength / 2;
            ContainingRadius = halfHeight + Radius;

            var _halfPath = Vector3.Normalize(axis) * halfHeight;
            _start = Position - _halfPath;
            _end = _start + axis;

            var y = Vector3.UnitY * Radius;
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

            SmallestDimension = Math.Min(Length, Radius * 2);

            Volume = (DecimalConstants.PI * Radius * Radius * _pathLength) + (DecimalConstants.FourThirdsPI * (decimal)Math.Pow((double)Radius, 3));
        }

        private Capsule(SerializationInfo info, StreamingContext context) : this(
            (Vector3?)info.GetValue(nameof(Axis), typeof(Vector3)) ?? default,
            (decimal?)info.GetValue(nameof(Radius), typeof(decimal)) ?? default,
            (Vector3?)info.GetValue(nameof(Position), typeof(Vector3)) ?? default)
        { }

        private static int FindBoxEdgeIntersection(decimal ay, decimal ey, decimal dx, decimal dz, decimal bx, decimal by, decimal bz, decimal radius, out decimal distance)
        {
            distance = 0;

            var rSqr = radius * radius;
            decimal dy, crossZ, crossX;

            if (by >= 0)
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

            if (crossZ >= 0 && crossX >= 0 && (crossX * crossX) + (crossZ * crossZ) > rSqr * by * by)
            {
                var endPoint = new Vector3(bx, by, bz);
                var cross = Vector3.Cross(new Vector3(dx, dy, dz), endPoint);
                var crossSqrLen = Vector3.Dot(cross, cross);
                if (crossSqrLen > rSqr * Vector3.Dot(endPoint, endPoint))
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
            decimal ex, decimal ey, decimal ez,
            decimal ax, decimal ay, decimal az,
            decimal bx, decimal by, decimal bz,
            bool isAboveEdge, decimal radius, out decimal distance)
        {
            distance = 0;

            var dx = ax - ex;
            var dz = az - ez;

            if (isAboveEdge && (dx * dx) + (dz * dz) - (radius * radius) <= 0)
            {
                return -1; // already intersecting
            }

            if ((bx * dx) + (bz * dz) >= 0)
            {
                return 0; // not moving towards box
            }

            var dotPerp = (bz * dx) - (bx * dz);
            if (dotPerp >= 0)
            {
                if (bx >= 0)
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
            if (bz >= 0)
            {
                return 0; // missed corner
            }
            // check intersection with x-z edge
            return dotPerp >= radius * bz
                ? FindBoxEdgeIntersection(ay, ey, dx, dz, bx, by, bz, radius, out distance)
                : FindBoxFaceRegionIntersection(ez, ey, ex, az, ay, ax, bz, by, bx, false, radius, out distance);
        }

        private static int FindBoxFaceRegionIntersection(
            decimal ex, decimal ey, decimal ez,
            decimal ax, decimal ay, decimal az,
            decimal bx, decimal by, decimal bz,
            bool isAboveFace, decimal radius, out decimal distance)
        {
            distance = 0;

            if (az <= ez + radius && isAboveFace)
            {
                return -1; // already intersecting
            }
            if (bz >= 0)
            {
                return 0; // moving away on z axis
            }
            var rSqr = radius * radius;
            var bSqrX = (bz * bz) + (bx * bx);
            var bSqrY = (bz * bz) + (by * by);
            decimal dx, dy;
            var dz = az - ez;
            decimal crossX, crossY;
            int signX, signY;

            if (bx >= 0)
            {
                signX = 1;
                dx = ax - ex;
                crossX = (bx * dz) - (bz * dx);
            }
            else
            {
                signX = -1;
                dx = ax + ex;
                crossX = (bz * dx) - (bx * dz);
            }

            if (by >= 0)
            {
                signY = 1;
                dy = ay - ey;
                crossY = (by * dz) - (bz * dy);
            }
            else
            {
                signY = -1;
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

                    var endPoint = new Vector3(bx, by, bz);
                    var cross = Vector3.Cross(new Vector3(dx, dy, dz), endPoint);
                    if (Vector3.Dot(cross, cross) > rSqr * Vector3.Dot(endPoint, endPoint))
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
            decimal ex, decimal ey, decimal ez,
            decimal ax, decimal ay, decimal az,
            decimal bx, decimal by, decimal bz,
            decimal radius, out decimal distance)
        {
            distance = 0;

            var dx = ax - ex;
            var dy = ay - ey;
            var dz = az - ez;
            var rSqr = radius * radius;

            if ((dx * dx) + (dy * dy) + (dz * dz) - rSqr <= 0)
            {
                return -1; // sphere is already intersecting box
            }
            if ((bx * dx) + (by * dy) + (bz * dz) >= 0)
            {
                return 0; // sphere is not moving towards the box
            }

            var crossX = (by * dz) - (bz * dy);
            var crossY = (bx * dz) - (bz * dx);
            var crossZ = (by * dx) - (bx * dy);
            var crossXSqr = crossX * crossX;
            var crossYSqr = crossY * crossY;
            var crossZSqr = crossZ * crossZ;

            if ((crossY < 0 && crossZ >= 0 && crossYSqr + crossZSqr <= rSqr * bx * bx)
                || (crossZ < 0 && crossX < 0 && crossXSqr + crossZSqr <= rSqr * by * by)
                || (crossY >= 0 && crossX >= 0 && crossXSqr + crossYSqr <= rSqr * bz * bz))
            {
                // intersection with the vertex
                distance = GetBoxVertexIntersection(dx, dy, dz, bx, by, bz, rSqr);
                return 1;
            }

            // check Y and Z planes
            if (crossY < 0 && crossZ >= 0)
            {
                return FindBoxEdgeRegionIntersection(ey, ex, ez, ay, ax, az, by, bx, bz, false, radius, out distance);
            }

            // check X and Z planes
            if (crossZ < 0 && crossX < 0)
            {
                return FindBoxEdgeRegionIntersection(ex, ey, ez, ax, ay, az, bx, by, bz, false, radius, out distance);
            }

            // check X and Y planes
            return FindBoxEdgeRegionIntersection(ex, ez, ey, ax, az, ay, bx, bz, by, false, radius, out distance);
        }

        private static decimal GetBoxEdgeIntersection(decimal dx, decimal dz, decimal bx, decimal bz, decimal bSqr, decimal rSqr)
        {
            var dot = (bx * dx) + (bz * dz);
            var diff = (dx * dx) + (dz * dz) - rSqr;
            var inv = 1 / Math.Abs((dot * dot) - (bSqr * diff)).Sqrt();
            return diff * inv / (1 - (dot * inv));
        }

        private static decimal GetBoxVertexIntersection(
            decimal dx, decimal dy, decimal dz,
            decimal bx, decimal by, decimal bz,
            decimal rSqr)
        {
            var bSqr = (bx * bx) + (by * by) + (bz * bz);
            var dot = (dx * bx) + (dy * by) + (dz * bz);
            var diff = (dx * dx) + (dy * dy) + (dz * dz) - rSqr;
            var inv = 1 / Math.Abs((dot * dot) - (bSqr * diff)).Sqrt();
            return diff * inv / (1 - (dot * inv));
        }

        private static bool Quadratic(decimal a, decimal b, decimal c, out decimal root1, out decimal root2)
        {
            var q = (b * b) - (4 * a * c);
            if (q >= 0)
            {
                var sq = q.Sqrt();
                var d = 1 / (2 * a);
                root1 = (-b + sq) * d;
                root2 = (-b - sq) * d;
                return true;
            }
            else
            {
                root1 = 0;
                root2 = 0;
                return false;
            }
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
        public Capsule GetCopyAtPosition(Vector3 position) => new(Axis, Radius, position);

        /// <summary>
        /// Gets a deep clone of this instance with its <see cref="Rotation"/> set to the given
        /// value.
        /// </summary>
        /// <param name="rotation">The new <see cref="Rotation"/> for the clone.</param>
        /// <returns>A deep clone of this instance with its <see cref="Rotation"/> set to the given
        /// value.</returns>
        public Capsule GetCopyWithRotation(Quaternion rotation) => new(Vector3.Transform(Axis, rotation), Radius, Position);

        /// <summary>
        /// Treating this <see cref="Capsule"/> as a swept sphere (along the path of its <see
        /// cref="Axis"/>), finds the point at which that sphere makes contact with the given
        /// <paramref name="shape"/>, if any.
        /// </summary>
        /// <param name="shape">The <see cref="IShape"/> to check for collision with the swept
        /// sphere represented by this instance.</param>
        /// <returns>The point along this instance's <see cref="Axis"/> at which the swept sphere
        /// represented by this instance makes contact with the given <paramref name="shape"/>; or
        /// <see langword="null"/> if they do not collide.</returns>
        public Vector3? GetCollisionPoint(IShape shape)
        {
            var dist = GetDistanceToCollisionPoint(shape);
            if (!dist.HasValue)
            {
                return null;
            }

            return _start + (Vector3.Normalize(Axis) * dist);
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
            if (shape is Line line)
            {
                return Intersects(line, out var _);
            }
            if (shape is Sphere sphere)
            {
                return Intersects(sphere.Radius, sphere.Position, sphere.Position, out var _);
            }
            if (shape is Capsule capsule)
            {
                return Intersects(capsule.Radius, capsule._start, capsule._end, out var _);
            }
            if (shape is Cuboid cuboid)
            {
                return Intersects(cuboid, out var _);
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
            if (Vector3.Distance(point, _start) < Radius)
            {
                return true;
            }
            else if (Vector3.Distance(point, _end) < Radius)
            {
                return true;
            }

            var axisNormal = Vector3.Normalize(Axis);
            var localRight = Vector3.Cross(axisNormal, Vector3.UnitY);
            var orthonormalBasis = new Vector3[]
            {
                    axisNormal,
                    localRight,
                    Vector3.Cross(localRight, axisNormal)
            };
            var delta = point - Position;
            var P = new Vector3(
                Vector3.Dot(orthonormalBasis[1], delta),
                Vector3.Dot(orthonormalBasis[2], delta),
                Vector3.Dot(orthonormalBasis[0], delta));
            var closest = P;
            var sqrRadius = Radius * Radius;
            var sqrDist = (P.X * P.X) + (P.Y * P.Y);
            if (sqrDist > sqrRadius)
            {
                return false;
            }
            var distance = sqrDist == sqrRadius ? sqrDist.Sqrt() - Radius : 0;
            if (sqrDist == sqrRadius)
            {
                var tmp = Radius / distance;
                closest = new Vector3(closest.X * tmp, closest.Y * tmp, closest.Z);
            }
            if (P.Z > _pathLength / 2 || P.Z < -_pathLength / 2)
            {
                distance = (closest - P).Length();
            }
            return distance <= 0;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if obj and this instance are the same type and represent
        /// the same value; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object? obj) => obj is Capsule shape && Equals(shape);

        /// <summary>
        /// Determines whether the specified <see cref="IShape"/> is equivalent to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="IShape"/> is equivalent to the
        /// current object; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(IShape? obj) => obj is Capsule other && Equals(other);

        /// <summary>
        /// Determines whether the specified <see cref="Capsule"/> is equivalent to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="Capsule"/> is equivalent to the
        /// current object; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(Capsule? other)
            => other is not null && other.Axis == Axis && other.Radius == Radius && other.Position == Position;

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            var hash = (17 * 23) + Axis.GetHashCode();
            hash = (hash * 23) + Position.GetHashCode();
            return (hash * 23) + Radius.GetHashCode();
        }

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
            info.AddValue(nameof(Axis), Axis);
            info.AddValue(nameof(Position), Position);
            info.AddValue(nameof(Radius), Radius);
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

            return new Capsule(Axis * factor, Radius * factor, Position);
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
                return new Capsule(Axis * 0, 0, Position);
            }
            else
            {
                var hMultiplier = factor.Sqrt();
                return new Capsule(Axis * hMultiplier, Radius * hMultiplier.Sqrt(), Position);
            }
        }

        private decimal? GetDistanceToCollisionPoint(IShape shape)
        {
            if (shape is SinglePoint point)
            {
                return new Line(Axis, Position).GetDistanceTo(point, out var closestPoint) == 0
                    ? (closestPoint - _start).Length()
                    : (decimal?)null;
            }
            if (shape is Line line)
            {
                return Intersects(line, out var d) ? d : (decimal?)null;
            }
            if (shape is Sphere sphere)
            {
                return Intersects(sphere.Radius, sphere.Position, sphere.Position, out var d) ? d : (decimal?)null;
            }
            if (shape is Capsule capsule)
            {
                return Intersects(capsule.Radius, capsule._start, capsule._end, out var d) ? d : (decimal?)null;
            }
            if (shape is Cuboid cuboid)
            {
                return !Intersects(cuboid, out var d) ? null : (decimal?)d;
            }
            // Substitute the sphere formed by the containing radius.
            return Intersects(shape.ContainingRadius, shape.Position, shape.Position, out var dist) ? dist : (decimal?)null;
        }

        private bool Intersects(Cuboid cuboid, out decimal distance)
        {
            distance = 0;

            // Get starting sphere in the local coordinates of the box
            var cdiff = _start - cuboid.Position;
            var orthonormalBasis = new Vector3[]
            {
                Vector3.Transform(Vector3.UnitX, cuboid.Rotation),
                Vector3.Transform(Vector3.UnitY, cuboid.Rotation),
                Vector3.Transform(Vector3.UnitZ, cuboid.Rotation)
            };
            var ax = Vector3.Dot(cdiff, orthonormalBasis[0]);
            var ay = Vector3.Dot(cdiff, orthonormalBasis[1]);
            var az = Vector3.Dot(cdiff, orthonormalBasis[2]);
            var bx = Vector3.Dot(Axis, orthonormalBasis[0]);
            var by = Vector3.Dot(Axis, orthonormalBasis[1]);
            var bz = Vector3.Dot(Axis, orthonormalBasis[2]);

            if (ax < 0)
            {
                ax = -ax;
                bx = -bx;
            }
            if (ay < 0)
            {
                ay = -ay;
                by = -by;
            }
            if (az < 0)
            {
                az = -az;
                bz = -bz;
            }

            int retVal;

            var extent0 = cuboid.AxisX / 2;
            var extent1 = cuboid.AxisY / 2;
            var extent2 = cuboid.AxisZ / 2;
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

        private bool Intersects(Line line, out decimal distance)
        {
            distance = 0;
            var orthonormalBasis = new Vector3[]
            {
                Vector3.Transform(Vector3.UnitX, Rotation),
                Vector3.Transform(Vector3.UnitY, Rotation),
                Vector3.Transform(Vector3.UnitZ, Rotation)
            };
            var rSqr = Radius * Radius;
            var diff = line.Position - Position;
            var P = new Vector3(
                Vector3.Dot(orthonormalBasis[1], diff),
                Vector3.Dot(orthonormalBasis[2], diff),
                Vector3.Dot(orthonormalBasis[0], diff));
            var lineDirection = Vector3.Normalize(line.Path);
            var dz = Vector3.Dot(orthonormalBasis[0], lineDirection);

            decimal? intersect1 = null, intersect2 = null;
            if (Math.Abs(dz) == 1)
            {
                // the line intersects with both spheres
                return rSqr - (P.X * P.X) - (P.Y * P.Y) >= 0;
            }

            var D = new Vector3(
                Vector3.Dot(orthonormalBasis[1], lineDirection),
                Vector3.Dot(orthonormalBasis[2], lineDirection),
                dz);
            var a0 = (P.X * P.X) + (P.Y * P.Y) - rSqr;
            var a1 = (P.X * D.X) + (P.Y * D.Y);
            var a2 = (D.X * D.X) + (D.Y * D.Y);
            var discr = (a1 * a1) - (a0 * a2);

            if (discr < 0)
            {
                return false;
            }

            if (discr > 0)
            {
                var root = discr.Sqrt();
                var inv = 1 / a2;
                var tValue = (-a1 - root) * inv;
                var zValue = P.Z + (tValue * D.Z);
                if (Math.Abs(zValue) <= line.Length)
                {
                    intersect1 = tValue;
                }

                tValue = (-a1 + root) * inv;
                zValue = P.Z + (tValue * D.Z);
                if (Math.Abs(zValue) <= line.Length)
                {
                    if (intersect1.HasValue)
                    {
                        intersect2 = tValue;
                    }
                    else
                    {
                        intersect1 = tValue;
                    }
                }

                if (intersect2.HasValue) // the line intersects with the cylinder in two places
                {
                    // calculate the distance the starting sphere must travel to encounter the line
                    var a = line.GetDistanceTo(new Line(Axis, Position), out _, out _);
                    intersect1 = ((a * a) - rSqr).Sqrt();
                    var h = line.GetDistanceTo(_start, out _);
                    var totalDistance = ((a * a) - (h * h)).Sqrt();
                    distance = totalDistance - intersect1.Value;
                    return true;
                }
            }
            else
            {
                var tValue = -a1 / a2;
                var zValue = P.Z + (tValue * D.Z);
                if (Math.Abs(zValue) <= line.Length) // the line intersects the cylinder at a single point
                {
                    // calculate the distance the starting sphere must travel to encounter the line
                    var a = line.GetDistanceTo(new Line(Axis, Position), out _, out _);
                    var distance1 = ((a * a) - rSqr).Sqrt();
                    var h = line.GetDistanceTo(_start, out _);
                    var totalDistance = ((a * a) - (h * h)).Sqrt();
                    distance = totalDistance - distance1;
                    return true;
                }
            }

            // test for intersection with start sphere
            var PZpE = P.Z + line.Length;
            a1 += PZpE * D.Z;
            a0 += PZpE * PZpE;
            discr = (a1 * a1) - a0;
            if (discr > 0)
            {
                var root = discr.Sqrt();
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
            else if (discr == 0)
            {
                var tValue = -a1;
                var zValue = P.Z + (tValue * D.Z);
                if (zValue <= -line.Length)
                {
                    return true;
                }
            }

            if (intersect1.HasValue) // line already intersected with the cylinder, no need to check end sphere
            {
                // calculate the distance the starting sphere must travel to encounter the line
                var a = line.GetDistanceTo(new Line(Axis, Position), out _, out _);
                intersect1 = ((a * a) - rSqr).Sqrt();
                var h = line.GetDistanceTo(_start, out _);
                var totalDistance = ((a * a) - (h * h)).Sqrt();
                distance = totalDistance - intersect1.Value;
                return true;
            }

            // test for intersection with end sphere
            a1 -= 2 * line.Length * D.Z;
            a0 -= 4 * line.Length * P.Z;
            discr = (a1 * a1) - a0;
            if (discr > 0)
            {
                var root = discr.Sqrt();
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
            else if (discr == 0)
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

        private bool Intersects(decimal otherRadius, Vector3 B0, Vector3 B1, out decimal distance)
        {
            var AB = B0 - _start;
            var vab = B1 - B0 - Axis;
            var rab = Radius + otherRadius;
            distance = 0;

            // first check for overlap at the spheres' starting positions
            if (Vector3.Dot(AB, AB) <= rab * rab)
            {
                return true;
            }

            if (Quadratic(
                Vector3.Dot(vab, vab),
                2 * Vector3.Dot(vab, AB),
                Vector3.Dot(AB, AB) - (rab * rab),
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
        /// <param name="left">The first shape to compare.</param>
        /// <param name="right">The second shape to compare.</param>
        /// <returns>True if the shapes are equal; False otherwise.</returns>
        public static bool operator ==(Capsule left, IShape right)
            => left.Equals(right);

        /// <summary>
        /// Returns a boolean indicating whether the two given shapes are not equal.
        /// </summary>
        /// <param name="left">The first shape to compare.</param>
        /// <param name="right">The second shape to compare.</param>
        /// <returns>True if the shapes are not equal; False if they are equal.</returns>
        public static bool operator !=(Capsule left, IShape right)
            => !(left == right);
    }
}
