using System;
using System.Runtime.Serialization;

namespace Tavenem.Mathematics.Decimals
{
    /// <summary>
    /// Provides information about the properties of a single point as a shape.
    /// </summary>
    [Serializable]
    [DataContract]
    public class SinglePoint : IShape, IEquatable<SinglePoint>, ISerializable
    {
        /// <summary>
        /// A <see cref="SinglePoint"/> representing the origin: a point at <see
        /// cref="Vector3.Zero"/>.
        /// </summary>
        public static readonly SinglePoint Origin = new();

        /// <summary>
        /// A circular radius which fully contains the shape. Always 0 for a <see cref="SinglePoint"/>.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public decimal ContainingRadius => 0;

        /// <summary>
        /// The point on this shape highest on the Y axis. The same as <see cref="Position"/> for a
        /// <see cref="SinglePoint"/>.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Vector3 HighestPoint => Position;

        /// <summary>
        /// The point on this shape lowest on the Y axis. The same as <see cref="Position"/> for a
        /// <see cref="SinglePoint"/>.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Vector3 LowestPoint => Position;

        /// <summary>
        /// The position of the shape in 3D space.
        /// </summary>
        [DataMember(Order = 1)]
        public Vector3 Position { get; }

        /// <summary>
        /// The rotation of this shape in 3D space. Always <see cref="Quaternion.Identity"/> for
        /// <see cref="SinglePoint"/>, which can have no meaningful rotation.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Quaternion Rotation => Quaternion.Identity;

        /// <summary>
        /// Identifies the type of this <see cref="IShape"/>.
        /// </summary>
        [DataMember(Order = 2)]
        public ShapeType ShapeType => ShapeType.SinglePoint;

        /// <summary>
        /// The length of the shortest of the three dimensions of this shape. Always 0 for a <see
        /// cref="SinglePoint"/>.
        /// </summary>
        /// <remarks>
        /// Note: this is not the length of smallest possible cross-section, but of the total length
        /// of the shape along any of the three primary axes of 3D space, prior to any rotation.
        /// </remarks>
        [System.Text.Json.Serialization.JsonIgnore]
        public decimal SmallestDimension => 0;

        /// <summary>
        /// The total volume of the shape. Always 0 for a <see cref="SinglePoint"/>.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public decimal Volume => 0;

        /// <summary>
        /// Initializes a new instance of <see cref="SinglePoint"/>.
        /// </summary>
        public SinglePoint() : this(Vector3.Zero) { }

        /// <summary>
        /// Initializes a new instance of <see cref="SinglePoint"/>.
        /// </summary>
        /// <param name="position">The position of the shape in 3D space.</param>
        [System.Text.Json.Serialization.JsonConstructor]
        public SinglePoint(Vector3 position) => Position = position;

        private SinglePoint(SerializationInfo info, StreamingContext context) : this(
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
        /// <remarks>Returns the same instance, since rotation does not affect points.</remarks>
        public IShape GetCloneWithRotation(Quaternion rotation) => this;

        /// <summary>
        /// Gets a deep clone of this instance with its <see cref="Position"/> set to the given
        /// value.
        /// </summary>
        /// <param name="position">The new <see cref="Position"/> for the clone.</param>
        /// <returns>A deep clone of this instance with its <see cref="Position"/> set to the given
        /// value.</returns>
#pragma warning disable CA1822 // Mark members as static: Conformity with other IShape classes
        public SinglePoint GetCopyAtPosition(Vector3 position) => new(position);
#pragma warning restore CA1822 // Mark members as static

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
                return point.Position == Position;
            }
            return shape.IsPointWithin(Position);
        }

        /// <summary>
        /// Determines if a given point lies within this shape.
        /// </summary>
        /// <param name="point">A point in the same 3D space as this shape.</param>
        /// <returns>True if the point is within (or tangent to) this shape.</returns>
        public bool IsPointWithin(Vector3 point) => point == Position;

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if obj and this instance are the same type and represent
        /// the same value; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object? obj) => obj is SinglePoint shape && Equals(shape);

        /// <summary>
        /// Determines whether the specified <see cref="IShape"/> is equivalent to the current
        /// object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="IShape"/> is a <see
        /// cref="SinglePoint"/>, since all single points are mathematically identical; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        public bool Equals(IShape? obj) => obj is SinglePoint shape && Equals(shape);

        /// <summary>
        /// <para>
        /// Determines whether the specified <see cref="SinglePoint"/> is equivalent to the current
        /// object.
        /// </para>
        /// <para>
        /// Always <see langword="true"/>, since single points are all mathematically equivalent.
        /// </para>
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>
        /// Always <see langword="true"/>, since single points are all mathematically equivalent.
        /// </returns>
        public bool Equals(SinglePoint? other) => other is not null && other.Position == Position;

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode() => Position.GetHashCode();

        /// <summary>Populates a <see cref="SerializationInfo"></see> with the data needed to
        /// serialize the target object.</summary>
        /// <param name="info">The <see cref="SerializationInfo"></see> to populate with
        /// data.</param>
        /// <param name="context">The destination (see <see cref="StreamingContext"></see>) for this
        /// serialization.</param>
        /// <exception cref="System.Security.SecurityException">The caller does not have the
        /// required permission.</exception>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
            => info.AddValue(nameof(Position), Position);

        /// <summary>
        /// <para>
        /// Scales this <see cref="IShape"/>'s dimensions such that its dimensions will be
        /// multiplied by the given factor.
        /// </para>
        /// <para>
        /// Always returns the original instance for <see cref="SinglePoint"/>, which has no
        /// dimensions.
        /// </para>
        /// </summary>
        /// <param name="factor">The amount by which to scale this <see cref="IShape"/>'s
        /// size.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="factor"/> must be &gt;= 0.
        /// </exception>
        public IShape ScaleByDimension(decimal factor) => this;

        /// <summary>
        /// <para>
        /// Scales this <see cref="IShape"/>'s dimensions such that its volume will be multiplied by
        /// the given factor.
        /// </para>
        /// <para>
        /// Always returns the original instance for <see cref="SinglePoint"/>, which has no <see
        /// cref="Volume"/>.
        /// </para>
        /// </summary>
        /// <param name="factor">The amount by which to scale this <see cref="IShape"/>'s
        /// volume.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="factor"/> must be &gt;= 0.
        /// </exception>
        public IShape ScaleVolume(decimal factor) => this;

        /// <summary>
        /// Returns a boolean indicating whether the two given shapes are equal.
        /// </summary>
        /// <param name="left">The first shape to compare.</param>
        /// <param name="right">The second shape to compare.</param>
        /// <returns>True if the shapes are equal; False otherwise.</returns>
        public static bool operator ==(SinglePoint left, IShape right)
            => left.Equals(right);

        /// <summary>
        /// Returns a boolean indicating whether the two given shapes are not equal.
        /// </summary>
        /// <param name="left">The first shape to compare.</param>
        /// <param name="right">The second shape to compare.</param>
        /// <returns>True if the shapes are not equal; False if they are equal.</returns>
        public static bool operator !=(SinglePoint left, IShape right)
            => !(left == right);
    }
}
