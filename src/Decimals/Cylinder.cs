using System;
using System.Runtime.Serialization;

namespace Tavenem.Mathematics.Decimals
{
    /// <summary>
    /// Provides information about the properties of a cylinder.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Cylinder : IShape, IEquatable<Cylinder>, ISerializable
    {
        internal readonly Vector3 _start;
        internal readonly Vector3 _end;

        /// <summary>
        /// The axis of the <see cref="Cylinder"/>.
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
        /// The radius of the <see cref="Cylinder"/>.
        /// </summary>
        [DataMember(Order = 3)]
        public decimal Radius { get; }

        /// <summary>
        /// The rotation of this shape in 3D space. Always <see cref="Quaternion.Identity"/> for
        /// <see cref="Cylinder"/>, whose <see cref="Axis"/> defines its orientation.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Quaternion Rotation => Quaternion.Identity;

        /// <summary>
        /// Identifies the type of this <see cref="IShape"/>.
        /// </summary>
        [DataMember(Order = 4)]
        public ShapeType ShapeType => ShapeType.Cylinder;

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
        /// Initializes a new instance of <see cref="Cylinder"/>.
        /// </summary>
        public Cylinder() : this(Vector3.Zero, 0) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Cylinder"/> with the given parameters.
        /// </summary>
        /// <param name="axis">The axis of the <see cref="Cylinder"/>.</param>
        /// <param name="radius">The radius of the <see cref="Cylinder"/>.</param>
        public Cylinder(Vector3 axis, decimal radius) : this(axis, radius, Vector3.Zero) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Cylinder"/> with the given parameters.
        /// </summary>
        /// <param name="axis">The axis of the <see cref="Cylinder"/>.</param>
        /// <param name="radius">The radius of the <see cref="Cylinder"/>.</param>
        /// <param name="position">The position of the shape in 3D space.</param>
        [System.Text.Json.Serialization.JsonConstructor]
        public Cylinder(Vector3 axis, decimal radius, Vector3 position)
        {
            Axis = axis;
            Position = position;
            Radius = radius;

            var axisLength = axis.Length();
            var halfHeight = axisLength / 2;
            ContainingRadius = ((halfHeight * halfHeight) + (Radius * Radius)).Sqrt();

            var _halfPath = Vector3.Normalize(axis) * halfHeight;
            _start = Position - _halfPath;
            _end = _start + axis;

            var pl = Vector3.Normalize(Axis);
            var pr = Vector3.UnitY - (pl * Vector3.Dot(Vector3.UnitY, pl));
            var h = Vector3.Normalize(pr) * Radius;
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

            SmallestDimension = Math.Min(axisLength, Radius * 2);

            Volume = DecimalConstants.PI * Radius * Radius * axisLength;
        }

        private Cylinder(SerializationInfo info, StreamingContext context) : this(
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
        public Cylinder GetCopyAtPosition(Vector3 position) => new(Axis, Radius, position);

        /// <summary>
        /// Gets a deep clone of this instance with its <see cref="Rotation"/> set to the given
        /// value.
        /// </summary>
        /// <param name="rotation">The new <see cref="Rotation"/> for the clone.</param>
        /// <returns>A deep clone of this instance with its <see cref="Rotation"/> set to the given
        /// value.</returns>
        public Cylinder GetCopyWithRotation(Quaternion rotation) => new(Vector3.Transform(Axis, rotation), Radius, Position);

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
            // Approximate with capsule.
            return new Capsule(Axis, Radius, Position).Intersects(shape);
        }

        /// <summary>
        /// Determines if a given point lies within this shape.
        /// </summary>
        /// <param name="point">A point in the same 3-D space as this shape.</param>
        /// <returns>True if the point is within (or tangent to) this shape.</returns>
        public bool IsPointWithin(Vector3 point)
        {
            var p1 = Position - (Axis / 2);

            var dP = point - p1;

            var lengthSq = Axis.LengthSquared();

            var dot = Vector3.Dot(dP, Axis);

            if (dot < 0 || dot > lengthSq)
            {
                return false;
            }

            var dSq = Vector3.Dot(dP, dP) - (dot * dot / lengthSq);

            return dSq <= Radius * Radius;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if obj and this instance are the same type and represent
        /// the same value; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object? obj) => obj is Cylinder shape && Equals(shape);

        /// <summary>
        /// Determines whether the specified <see cref="IShape"/> is equivalent to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="IShape"/> is equivalent to the
        /// current object; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(IShape? obj) => obj is Cylinder other && Equals(other);

        /// <summary>
        /// Determines whether the specified <see cref="Cylinder"/> is equivalent to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="Cylinder"/> is equivalent to the
        /// current object; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(Cylinder? other)
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

            return new Cylinder(Axis * factor, Radius * factor, Position);
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
                return new Cylinder(Axis * 0, 0, Position);
            }
            else
            {
                var hMultiplier = factor.Sqrt();
                return new Cylinder(Axis * hMultiplier, Radius * hMultiplier.Sqrt(), Position);
            }
        }

        /// <summary>
        /// Returns a boolean indicating whether the two given shapes are equal.
        /// </summary>
        /// <param name="left">The first shape to compare.</param>
        /// <param name="right">The second shape to compare.</param>
        /// <returns>True if the shapes are equal; False otherwise.</returns>
        public static bool operator ==(Cylinder left, IShape right)
            => left.Equals(right);

        /// <summary>
        /// Returns a boolean indicating whether the two given shapes are not equal.
        /// </summary>
        /// <param name="left">The first shape to compare.</param>
        /// <param name="right">The second shape to compare.</param>
        /// <returns>True if the shapes are not equal; False if they are equal.</returns>
        public static bool operator !=(Cylinder left, IShape right)
            => !(left == right);
    }
}
