using System;
using System.Runtime.Serialization;

namespace Tavenem.Mathematics.Decimals
{
    /// <summary>
    /// Provides information about the properties of a cone.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Cone : IShape, IEquatable<Cone>, ISerializable
    {
        private readonly Vector3 _start;
        private readonly Vector3 _end;

        /// <summary>
        /// The axis of the <see cref="Cone"/>, starting from the point and ending at the base.
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
        /// THe length of the <see cref="Cone"/>, from point to base.
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
        /// The radius of the <see cref="Cone"/>'s base.
        /// </summary>
        [DataMember(Order = 3)]
        public decimal Radius { get; }

        /// <summary>
        /// The rotation of this shape in 3D space. Always <see cref="Quaternion.Identity"/> for
        /// <see cref="Cone"/>, whose <see cref="Axis"/> defines its orientation.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Quaternion Rotation => Quaternion.Identity;

        /// <summary>
        /// Identifies the type of this <see cref="IShape"/>.
        /// </summary>
        [DataMember(Order = 4)]
        public ShapeType ShapeType => ShapeType.Cone;

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
        /// Initializes a new instance of <see cref="Cone"/>.
        /// </summary>
        public Cone() : this(Vector3.Zero, 0) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Cone"/> with the given parameters.
        /// </summary>
        /// <param name="axis">The axis of the <see cref="Cone"/>, starting from the point and
        /// ending at the base.</param>
        /// <param name="radius">The radius of the <see cref="Cone"/>'s base.</param>
        public Cone(Vector3 axis, decimal radius) : this(axis, radius, Vector3.Zero) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Cone"/> with the given parameters.
        /// </summary>
        /// <param name="axis">The axis of the <see cref="Cone"/>, starting from the point and
        /// ending at the base.</param>
        /// <param name="radius">The radius of the <see cref="Cone"/>'s base.</param>
        /// <param name="position">The position of the shape in 3D space.</param>
        [System.Text.Json.Serialization.JsonConstructor]
        public Cone(Vector3 axis, decimal radius, Vector3 position)
        {
            Axis = axis;
            Position = position;
            Radius = radius;

            Length = axis.Length();
            var halfHeight = Length / 2;
            ContainingRadius = ((halfHeight * halfHeight) + (Radius * Radius)).Sqrt();

            var _halfPath = Vector3.Normalize(axis) * halfHeight;
            _start = Position - _halfPath;
            _end = _start + axis;

            var pl = Vector3.Normalize(Axis);
            var pr = Vector3.UnitY - (pl * Vector3.Dot(Vector3.UnitY, pl));
            var h = Vector3.Normalize(pr) * Radius;
            var endTop = _end + h;
            var endBottom = _end - h;
            HighestPoint = endTop.Y >= _start.Y ? endTop : _start;
            LowestPoint = endBottom.Y < _start.Y ? endBottom : _start;

            SmallestDimension = Math.Min(Length, Radius * 2);

            Volume = DecimalConstants.PI * Radius * Radius * (Length / 3);
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Cone"/> with the given parameters.
        /// </summary>
        /// <param name="origin">The position of the apex of the <see cref="Cone"/>.</param>
        /// <param name="orientation">The direction of the <see cref="Cone"/>'s axis, and its length.</param>
        /// <param name="angle">The angle of the cone.</param>
        public Cone(Vector3 origin, Vector3 orientation, decimal angle)
        {
            Axis = orientation;

            _start = origin;
            _end = origin + orientation;

            Length = orientation.Length();
            var halfHeight = Length / 2;

            var normalAxis = Vector3.Normalize(orientation);
            Position = origin + (normalAxis * halfHeight);

            Radius = (decimal)Math.Tan((double)angle / 2) * Length;
            ContainingRadius = (decimal)Math.Sqrt((double)((halfHeight * halfHeight) + (Radius * Radius)));

            var pr = Vector3.UnitY - (normalAxis * Vector3.Dot(Vector3.UnitY, normalAxis));
            var h = Vector3.Normalize(pr) * Radius;
            var endTop = _end + h;
            var endBottom = _end - h;
            HighestPoint = endTop.Y >= _start.Y ? endTop : _start;
            LowestPoint = endBottom.Y < _start.Y ? endBottom : _start;

            SmallestDimension = Math.Min(Length, Radius * 2);

            Volume = DecimalConstants.PI * Radius * Radius * (Length / 3);
        }

        private Cone(SerializationInfo info, StreamingContext context) : this(
            (Vector3?)info.GetValue(nameof(Axis), typeof(Vector3)) ?? default,
            (decimal?)info.GetValue(nameof(Radius), typeof(decimal)) ?? default,
            (Vector3?)info.GetValue(nameof(Position), typeof(Vector3)) ?? default)
        { }

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
        public Cone GetCopyAtPosition(Vector3 position) => new(Axis, Radius, position);

        /// <summary>
        /// Gets a deep clone of this instance with its <see cref="Rotation"/> set to the given
        /// value.
        /// </summary>
        /// <param name="rotation">The new <see cref="Rotation"/> for the clone.</param>
        /// <returns>A deep clone of this instance with its <see cref="Rotation"/> set to the given
        /// value.</returns>
        public Cone GetCopyWithRotation(Quaternion rotation) => new(Vector3.Transform(Axis, rotation), Radius, Position);

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
            if (shape is Line line)
            {
                return Intersects(line);
            }
            if (shape is Sphere sphere)
            {
                return Intersects(sphere);
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
            var p1 = point - Position;
            var dot = Vector3.Dot(p1, Axis);

            return dot / p1.Length() / Length > (decimal)Math.Cos(Math.Atan((double)(Radius / Length)) / 2)
                && dot / Length < Length;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if obj and this instance are the same type and represent
        /// the same value; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object? obj) => obj is Cone shape && Equals(shape);

        /// <summary>
        /// Determines whether the specified <see cref="IShape"/> is equivalent to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="IShape"/> is equivalent to the
        /// current object; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(IShape? obj) => obj is Cone other && Equals(other);

        /// <summary>
        /// Determines whether the specified <see cref="Cone"/> is equivalent to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="Cone"/> is equivalent to the
        /// current object; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(Cone? other)
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

            return new Cone(Axis * factor, Radius * factor, Position);
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
                return new Cone(Axis * 0, 0, Position);
            }
            else
            {
                var hMultiplier = factor.Sqrt();
                return new Cone(Axis * hMultiplier, Radius * hMultiplier.Sqrt());
            }
        }

        internal decimal GetRadiusAtPoint(Vector3 point) => Radius / Length * (point - Position).Length();

        private bool Intersects(Capsule capsule)
        {
            var axis = new Line(Axis, Position);
            var otherAxis = new Line(capsule.Axis, capsule.Position);
            axis.GetDistanceTo(otherAxis, out var closestPoint, out var otherClosestPoint);
            var radiusAtClosestPoint = GetRadiusAtPoint(closestPoint);
            var closestSphere = new Sphere(radiusAtClosestPoint, closestPoint);
            var otherClosestSphere = new Sphere(capsule.Radius, otherClosestPoint);
            return closestSphere.Intersects(otherClosestSphere);
        }

        private bool Intersects(Cone cone)
        {
            var axis = new Line(Axis, Position);
            var otherAxis = new Line(cone.Axis, cone.Position);
            axis.GetDistanceTo(otherAxis, out var closestPoint, out var closestPointOther);
            var radiusAtClosestPoint = GetRadiusAtPoint(closestPoint);
            var otherRadiusAtClosestPoint = cone.GetRadiusAtPoint(closestPointOther);
            var closestSphere = new Sphere(radiusAtClosestPoint, closestPoint);
            var otherClosestSphere = new Sphere(otherRadiusAtClosestPoint, closestPointOther);
            return closestSphere.Intersects(otherClosestSphere);
        }

        private bool Intersects(Line line)
        {
            var PmV = line.Position - Position;
            var dir = Vector3.Normalize(Axis);
            var lineDir = Vector3.Normalize(line.Path);
            var DdU = Vector3.Dot(dir, lineDir);
            var DdPmV = Vector3.Dot(dir, PmV);
            var cosAngle = (decimal)Math.Cos(Math.Atan((double)(Radius / Length)));
            var cosSqr = cosAngle * cosAngle;
            var c2 = (DdU * DdU) - cosSqr;
            var c1 = (DdU * DdPmV) - (cosSqr * Vector3.Dot(lineDir, PmV));
            var c0 = (DdPmV * DdPmV) - (cosSqr * Vector3.Dot(PmV, PmV));
            decimal t;
            var intersects = new decimal[2];

            if (c2 != 0)
            {
                var discr = (c1 * c1) - (c0 * c2);
                if (discr < 0)
                {
                    return false;
                }

                if (discr > 0)
                {
                    var root = discr.Sqrt();
                    var invC2 = 1 / c2;
                    var numIntersects = 0;

                    t = (-c1 - root) * invC2;
                    if ((DdU * t) + DdPmV >= 0)
                    {
                        intersects[numIntersects++] = t;
                    }

                    t = (-c1 + root) * invC2;
                    if ((DdU * t) + DdPmV >= 0)
                    {
                        intersects[numIntersects++] = t;
                    }

                    if (numIntersects == 2 && intersects[0] > intersects[1])
                    {
                        var tmp = intersects[0];
                        intersects[0] = intersects[1];
                        intersects[1] = tmp;
                    }
                    else if (numIntersects == 1)
                    {
                        if (DdU > 0)
                        {
                            intersects[1] = decimal.MaxValue;
                        }
                        else
                        {
                            intersects[1] = intersects[0];
                            intersects[0] = decimal.MaxValue;
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
                    if ((DdU * t) + DdPmV >= 0)
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
            else if (c1 != 0)
            {
                t = -0.5m * c0 / c1;
                if ((DdU * t) + DdPmV >= 0)
                {
                    if (DdU > 0)
                    {
                        intersects[0] = t;
                        intersects[1] = decimal.MaxValue;
                    }
                    else
                    {
                        intersects[0] = decimal.MinValue;
                        intersects[1] = t;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (c0 != 0)
            {
                return false;
            }
            else // the line is along the cone
            {
                intersects[0] = decimal.MinValue;
                intersects[1] = decimal.MaxValue;
            }

            if (DdU != 0)
            {
                var invAD = 1 / DdU;
                var hInterval = new decimal[2];
                if (DdU > 0)
                {
                    hInterval[0] = -DdPmV * invAD;
                    hInterval[1] = (Axis.Length() - DdPmV) * invAD;
                }
                else
                {
                    hInterval[0] = (Axis.Length() - DdPmV) * invAD;
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

        private bool Intersects(Sphere sphere)
        {
            var angle = Math.Atan((double)(Radius / Length));
            var sinAngle = (decimal)Math.Sin(angle);
            var cosAngle = (decimal)Math.Cos(angle);
            var CmV = sphere.Position - Position;
            var dir = Vector3.Normalize(Axis);
            var D = CmV + (sphere.Radius / sinAngle * dir);
            var lenSqr = Vector3.Dot(D, D);
            var e = Vector3.Dot(D, dir);
            if (e <= 0 || e * e < lenSqr * cosAngle * cosAngle)
            {
                return false;
            }

            lenSqr = Vector3.Dot(CmV, CmV);
            e = -Vector3.Dot(CmV, dir);

            return e <= 0 || e * e < lenSqr * sinAngle * sinAngle
                || lenSqr <= sphere.Radius * sphere.Radius;
        }

        /// <summary>
        /// Returns a boolean indicating whether the two given shapes are equal.
        /// </summary>
        /// <param name="left">The first shape to compare.</param>
        /// <param name="right">The second shape to compare.</param>
        /// <returns>True if the shapes are equal; False otherwise.</returns>
        public static bool operator ==(Cone left, IShape right)
            => left.Equals(right);

        /// <summary>
        /// Returns a boolean indicating whether the two given shapes are not equal.
        /// </summary>
        /// <param name="left">The first shape to compare.</param>
        /// <param name="right">The second shape to compare.</param>
        /// <returns>True if the shapes are not equal; False if they are equal.</returns>
        public static bool operator !=(Cone left, IShape right)
            => !(left == right);
    }
}
