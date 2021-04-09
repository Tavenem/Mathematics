using System;
using System.Runtime.Serialization;

namespace Tavenem.Mathematics.Decimals
{
    /// <summary>
    /// Provides information about the properties of a torus.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Torus : IShape, IEquatable<Torus>, ISerializable
    {
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
        /// The distance from the center of the tube to the center of the torus.
        /// </summary>
        [DataMember(Order = 1)]
        public decimal MajorRadius { get; }

        /// <summary>
        /// The radius of the tube, in meters.
        /// </summary>
        [DataMember(Order = 2)]        public decimal MinorRadius { get; }

        /// <summary>
        /// The position of the shape in 3D space.
        /// </summary>
        [DataMember(Order = 3)]
        public Vector3 Position { get; }

        /// <summary>
        /// The rotation of this shape in 3D space.
        /// </summary>
        [DataMember(Order = 4)]
        public Quaternion Rotation { get; }

        /// <summary>
        /// Identifies the type of this <see cref="IShape"/>.
        /// </summary>
        [DataMember(Order = 5)]
        public ShapeType ShapeType => ShapeType.Torus;

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
        /// The total volume of the shape. Read-only; set by setting the radii.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public decimal Volume { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Torus"/>.
        /// </summary>
        public Torus() : this(0, 0) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Torus"/>.
        /// </summary>
        /// <param name="majorRadius">The length of the major radius of the <see cref="Torus"/>.</param>
        /// <param name="minorRadius">The length of the minor radius of the <see cref="Torus"/>.</param>
        public Torus(decimal majorRadius, decimal minorRadius)
            : this(majorRadius, minorRadius, Vector3.Zero, Quaternion.Identity) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Torus"/>.
        /// </summary>
        /// <param name="majorRadius">The length of the major radius of the <see cref="Torus"/>.</param>
        /// <param name="minorRadius">The length of the minor radius of the <see cref="Torus"/>.</param>
        /// <param name="position">The position of the shape in 3D space.</param>
        public Torus(decimal majorRadius, decimal minorRadius, Vector3 position)
            : this(majorRadius, minorRadius, position, Quaternion.Identity) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Torus"/>.
        /// </summary>
        /// <param name="majorRadius">The length of the major radius of the <see cref="Torus"/>.</param>
        /// <param name="minorRadius">The length of the minor radius of the <see cref="Torus"/>.</param>
        /// <param name="position">The position of the shape in 3D space.</param>
        /// <param name="rotation">The rotation of this shape in 3D space.</param>
        [System.Text.Json.Serialization.JsonConstructor]
        public Torus(decimal majorRadius, decimal minorRadius, Vector3 position, Quaternion rotation)
        {
            if (majorRadius < minorRadius)
            {
                throw new ArgumentException("majorRadius cannot be smaller than minorRadius", nameof(majorRadius));
            }
            MajorRadius = majorRadius;
            MinorRadius = minorRadius;
            Position = position;
            Rotation = rotation;

            ContainingRadius = MajorRadius + MinorRadius;

            var x = Vector3.Transform(Vector3.UnitX, Rotation);
            var z = Vector3.Transform(Vector3.UnitZ, Rotation);
            var pl = Vector3.Normalize(Vector3.Cross(x, z));
            var pr = Vector3.UnitY - (pl * Vector3.Dot(Vector3.UnitY, pl));
            var h = Vector3.Normalize(pr) * MajorRadius;
            var y = Vector3.UnitY * MinorRadius;
            HighestPoint = Position + h + y;
            LowestPoint = Position - h - y;

            SmallestDimension = Math.Min(MajorRadius, MinorRadius);
            Volume = DecimalConstants.TwoPISquared * MajorRadius * (decimal)Math.Pow((double)MinorRadius, 2);
        }

        private Torus(SerializationInfo info, StreamingContext context) : this(
            (decimal?)info.GetValue(nameof(MajorRadius), typeof(decimal)) ?? default,
            (decimal?)info.GetValue(nameof(MinorRadius), typeof(decimal)) ?? default,
            (Vector3?)info.GetValue(nameof(Position), typeof(Vector3)) ?? default,
            (Quaternion?)info.GetValue(nameof(Rotation), typeof(Quaternion)) ?? default)
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
        public Torus GetCopyAtPosition(Vector3 position) => new(MajorRadius, MinorRadius, position, Rotation);

        /// <summary>
        /// Gets a deep clone of this instance with its <see cref="Rotation"/> set to the given
        /// value.
        /// </summary>
        /// <param name="rotation">The new <see cref="Rotation"/> for the clone.</param>
        /// <returns>A deep clone of this instance with its <see cref="Rotation"/> set to the given
        /// value.</returns>
        public Torus GetCopyWithRotation(Quaternion rotation) => new(MajorRadius, MinorRadius, Position, rotation);

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
            // Approximate with cylinder
            return new Cylinder(Vector3.Transform(Vector3.UnitY * MinorRadius, Rotation), MajorRadius + MinorRadius, Position).Intersects(shape);
        }

        /// <summary>
        /// Determines if a given point lies within this shape.
        /// </summary>
        /// <param name="point">A point in the same 3D space as this shape.</param>
        /// <returns>True if the point is within (or tangent to) this shape.</returns>
        public bool IsPointWithin(Vector3 point) =>
            (decimal)Math.Pow(Math.Pow((double)(point.X - Position.X), 2) + Math.Pow((double)(point.Y - Position.Y), 2)
            + Math.Pow((double)(point.Z - Position.Z), 2) + Math.Pow((double)MajorRadius, 2) - Math.Pow((double)MinorRadius, 2), 2)
            - (4 * (decimal)Math.Pow((double)MajorRadius, 2) * ((decimal)Math.Pow((double)(point.X - Position.X), 2) + (decimal)Math.Pow((double)(point.Y - Position.Y), 2))) <= 0;

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if obj and this instance are the same type and represent
        /// the same value; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object? obj) => obj is Torus shape && Equals(shape);

        /// <summary>
        /// Determines whether the specified <see cref="IShape"/> is equivalent to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="IShape"/> is equivalent to the
        /// current object; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(IShape? obj) => obj is Torus other && Equals(other);

        /// <summary>
        /// Determines whether the specified <see cref="Torus"/> is equivalent to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="Torus"/> is equivalent to the
        /// current object; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(Torus? other)
            => other is not null && other.MajorRadius == MajorRadius && other.MinorRadius == MinorRadius && other.Position == Position && other.Rotation == Rotation;

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            var hash = (17 * 23) + MajorRadius.GetHashCode();
            hash = (hash * 23) + MinorRadius.GetHashCode();
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
            info.AddValue(nameof(MajorRadius), MajorRadius);
            info.AddValue(nameof(MinorRadius), MinorRadius);
            info.AddValue(nameof(Position), Position);
            info.AddValue(nameof(Rotation), Rotation);
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

            return new Torus(MajorRadius * factor, MinorRadius * factor, Position, Rotation);
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
                return new Torus(0, 0, Position);
            }
            else
            {
                var majMultiplier = factor.Sqrt();
                return new Torus(MajorRadius * majMultiplier, MinorRadius * majMultiplier.Sqrt(), Position, Rotation);
            }
        }

        /// <summary>
        /// Returns a boolean indicating whether the two given shapes are equal.
        /// </summary>
        /// <param name="left">The first shape to compare.</param>
        /// <param name="right">The second shape to compare.</param>
        /// <returns>True if the shapes are equal; False otherwise.</returns>
        public static bool operator ==(Torus left, IShape right)
            => left.Equals(right);

        /// <summary>
        /// Returns a boolean indicating whether the two given shapes are not equal.
        /// </summary>
        /// <param name="left">The first shape to compare.</param>
        /// <param name="right">The second shape to compare.</param>
        /// <returns>True if the shapes are not equal; False if they are equal.</returns>
        public static bool operator !=(Torus left, IShape right)
            => !(left == right);
    }
}
