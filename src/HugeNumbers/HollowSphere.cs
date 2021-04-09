using System;
using System.Runtime.Serialization;
using Tavenem.HugeNumbers;

namespace Tavenem.Mathematics.HugeNumbers
{
    /// <summary>
    /// Provides information about the properties of a hollow sphere.
    /// </summary>
    [Serializable]
    [DataContract]
    public class HollowSphere : IShape, IEquatable<HollowSphere>, ISerializable
    {
        /// <summary>
        /// A circular radius which fully contains the shape.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public HugeNumber ContainingRadius => OuterRadius;

        /// <summary>
        /// The point on this shape highest on the Y axis.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Vector3 HighestPoint { get; }

        /// <summary>
        /// The inner radius of the <see cref="Sphere"/>.
        /// </summary>
        [DataMember(Order = 1)]
        public HugeNumber InnerRadius { get; }

        /// <summary>
        /// The point on this shape lowest on the Y axis.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Vector3 LowestPoint { get; }

        /// <summary>
        /// The outer radius of the <see cref="Sphere"/>.
        /// </summary>
        [DataMember(Order = 2)]
        public HugeNumber OuterRadius { get; }

        /// <summary>
        /// The position of the shape in 3D space.
        /// </summary>
        [DataMember(Order = 3)]
        public Vector3 Position { get; }

        /// <summary>
        /// The rotation of this shape in 3D space. Always <see cref="Quaternion.Identity"/> for
        /// <see cref="HollowSphere"/>, which can have no meaningful rotation.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Quaternion Rotation => Quaternion.Identity;

        /// <summary>
        /// Identifies the type of this <see cref="IShape"/>.
        /// </summary>
        [DataMember(Order = 4)]
        public ShapeType ShapeType => ShapeType.HollowSphere;

        /// <summary>
        /// The length of the shortest of the three dimensions of this shape.
        /// </summary>
        /// <remarks>
        /// Note: this is not the length of smallest possible cross-section, but of the total length
        /// of the shape along any of the three primary axes of 3D space, prior to any rotation.
        /// </remarks>
        [System.Text.Json.Serialization.JsonIgnore]
        public HugeNumber SmallestDimension => OuterRadius;

        /// <summary>
        /// The total volume of the shape. Read-only; set by setting the radii.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public HugeNumber Volume { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="HollowSphere"/>.
        /// </summary>
        public HollowSphere() : this(HugeNumber.Zero, HugeNumber.Zero) { }

        /// <summary>
        /// Initializes a new instance of <see cref="HollowSphere"/> with the given parameters.
        /// </summary>
        /// <param name="innerRadius">The inner radius of the <see cref="HollowSphere"/>.</param>
        /// <param name="outerRadius">The outer radius of the <see cref="HollowSphere"/>.</param>
        public HollowSphere(HugeNumber innerRadius, HugeNumber outerRadius)
            : this(innerRadius, outerRadius, Vector3.Zero) { }

        /// <summary>
        /// Initializes a new instance of <see cref="HollowSphere"/> with the given parameters.
        /// </summary>
        /// <param name="innerRadius">The inner radius of the <see cref="HollowSphere"/>.</param>
        /// <param name="outerRadius">The outer radius of the <see cref="HollowSphere"/>.</param>
        /// <param name="position">The position of the shape in 3D space.</param>
        [System.Text.Json.Serialization.JsonConstructor]
        public HollowSphere(HugeNumber innerRadius, HugeNumber outerRadius, Vector3 position)
        {
            InnerRadius = innerRadius;
            OuterRadius = outerRadius;
            Position = position;

            HighestPoint = Position + (Vector3.UnitY * outerRadius);
            LowestPoint = Position + (-Vector3.UnitY * outerRadius);
            Volume = (HugeNumber.FourThirdsPI * OuterRadius.Cube()) - (HugeNumber.FourThirdsPI * InnerRadius.Cube());
        }

        private HollowSphere(SerializationInfo info, StreamingContext context) : this(
            (HugeNumber?)info.GetValue(nameof(InnerRadius), typeof(HugeNumber)) ?? default,
            (HugeNumber?)info.GetValue(nameof(OuterRadius), typeof(HugeNumber)) ?? default,
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
        /// <remarks>Returns the same instance, since rotation does not affect spheres.</remarks>
        public IShape GetCloneWithRotation(Quaternion rotation) => this;

        /// <summary>
        /// Gets a deep clone of this instance with its <see cref="Position"/> set to the given
        /// value.
        /// </summary>
        /// <param name="position">The new <see cref="Position"/> for the clone.</param>
        /// <returns>A deep clone of this instance with its <see cref="Position"/> set to the given
        /// value.</returns>
        public HollowSphere GetCopyAtPosition(Vector3 position) => new(InnerRadius, OuterRadius, position);

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
            else if (shape is Line line)
            {
                return Intersects(line);
            }
            else
            {
                var d = Vector3.Distance(Position, shape.Position);
                return d - shape.ContainingRadius <= OuterRadius
                    && d + shape.ContainingRadius < InnerRadius;
            }
        }

        /// <summary>
        /// Determines if a given point lies within this shape.
        /// </summary>
        /// <param name="point">A point in the same 3D space as this shape.</param>
        /// <returns>True if the point is within (or tangent to) this shape.</returns>
        public bool IsPointWithin(Vector3 point)
            => (point - Position).Length() <= OuterRadius && (point - Position).Length() >= InnerRadius;

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if obj and this instance are the same type and represent
        /// the same value; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object? obj) => obj is HollowSphere shape && Equals(shape);

        /// <summary>
        /// Determines whether the specified <see cref="IShape"/> is equivalent to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="IShape"/> is equivalent to the
        /// current object; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(IShape? obj) => obj is HollowSphere other && Equals(other);

        /// <summary>
        /// Determines whether the specified <see cref="HollowSphere"/> is equivalent to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="HollowSphere"/> is equivalent to the
        /// current object; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(HollowSphere? other)
            => other is not null && other.InnerRadius == InnerRadius && other.OuterRadius == OuterRadius && other.Position == Position;

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            var hash = (17 * 23) + InnerRadius.GetHashCode();
            hash = (hash * 23) + OuterRadius.GetHashCode();
            return (hash * 23) + Position.GetHashCode();
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
            info.AddValue(nameof(InnerRadius), InnerRadius);
            info.AddValue(nameof(OuterRadius), OuterRadius);
            info.AddValue(nameof(Position), Position);
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
        public IShape ScaleByDimension(HugeNumber factor)
        {
            if (factor < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
            }

            return new HollowSphere(InnerRadius * factor, OuterRadius * factor, Position);
        }

        /// <summary>
        /// Scales this <see cref="IShape"/>'s dimensions such that its volume will be multiplied by
        /// the given factor.
        /// </summary>
        /// <param name="factor">The amount by which to scale this <see cref="IShape"/>'s volume.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="factor"/> must be &gt;= 0.
        /// </exception>
        public IShape ScaleVolume(HugeNumber factor)
        {
            if (factor < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
            }

            if (factor == 0)
            {
                return new HollowSphere(0, 0, Position);
            }
            else
            {
                var multiplier = HugeNumber.Pow(factor, HugeNumber.Third);
                return new HollowSphere(InnerRadius * multiplier, OuterRadius * multiplier, Position);
            }
        }

        private bool Intersects(Line line)
        {
            var diff = line.Position - Position;
            var a0 = Vector3.Dot(diff, diff) - (OuterRadius * OuterRadius);
            var a1 = Vector3.Dot(Vector3.Normalize(line.Path), diff);
            var discr = (a1 * a1) - a0;
            if (discr < 0)
            {
                return false;
            }
            else
            {
                var extent = line.ContainingRadius;
                var tmp0 = (extent * extent) + a0;
                var tmp1 = 2 * a1 * extent;
                var qm = tmp0 - tmp1;
                var qp = tmp0 + tmp1;
                var intersect = qm * qp <= 0 || (qm > 0 && HugeNumber.Abs(a1) < extent);
                if (!intersect)
                {
                    return false;
                }

                // Both endpoints within inner sphere?
                return (line.HighestPoint - Position).Length() >= InnerRadius
                    || (line.LowestPoint - Position).Length() >= InnerRadius;
            }
        }

        /// <summary>
        /// Returns a boolean indicating whether the two given shapes are equal.
        /// </summary>
        /// <param name="left">The first shape to compare.</param>
        /// <param name="right">The second shape to compare.</param>
        /// <returns>True if the shapes are equal; False otherwise.</returns>
        public static bool operator ==(HollowSphere left, IShape right)
            => left.Equals(right);

        /// <summary>
        /// Returns a boolean indicating whether the two given shapes are not equal.
        /// </summary>
        /// <param name="left">The first shape to compare.</param>
        /// <param name="right">The second shape to compare.</param>
        /// <returns>True if the shapes are not equal; False if they are equal.</returns>
        public static bool operator !=(HollowSphere left, IShape right)
            => !(left == right);
    }
}
