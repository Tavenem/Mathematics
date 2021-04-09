using System;
using System.Runtime.Serialization;

namespace Tavenem.Mathematics.Doubles
{
    /// <summary>
    /// Provides information about the properties of a sphere.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Sphere : IShape, IEquatable<Sphere>, ISerializable
    {
        /// <summary>
        /// A circular radius which fully contains the shape.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public double ContainingRadius => Radius;

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
        [DataMember(Order = 1)]
        public Vector3 Position { get; }

        /// <summary>
        /// The radius of the <see cref="Sphere"/>.
        /// </summary>
        [DataMember(Order = 2)]
        public double Radius { get; }

        /// <summary>
        /// The rotation of this shape in 3D space. Always <see cref="Quaternion.Identity"/> for
        /// <see cref="Sphere"/>, which can have no meaningful rotation.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Quaternion Rotation => Quaternion.Identity;

        /// <summary>
        /// Identifies the type of this <see cref="IShape"/>.
        /// </summary>
        [DataMember(Order = 3)]
        public ShapeType ShapeType => ShapeType.Sphere;

        /// <summary>
        /// The length of the shortest of the three dimensions of this shape.
        /// </summary>
        /// <remarks>
        /// Note: this is not the length of smallest possible cross-section, but of the total length
        /// of the shape along any of the three primary axes of 3D space, prior to any rotation.
        /// </remarks>
        [System.Text.Json.Serialization.JsonIgnore]
        public double SmallestDimension => Radius;

        /// <summary>
        /// The total volume of the shape.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public double Volume { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Sphere"/>.
        /// </summary>
        public Sphere() : this(0) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Sphere"/>.
        /// </summary>
        /// <param name="radius">The radius of the <see cref="Sphere"/>.</param>
        public Sphere(double radius) : this(radius, Vector3.Zero) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Sphere"/>.
        /// </summary>
        /// <param name="radius">The radius of the <see cref="Sphere"/>.</param>
        /// <param name="position">The position of the shape in 3D space.</param>
        [System.Text.Json.Serialization.JsonConstructor]
        public Sphere(double radius, Vector3 position)
        {
            Position = position;
            Radius = radius;

            HighestPoint = Position + (Vector3.UnitY * radius);
            LowestPoint = Position + (-Vector3.UnitY * radius);
            Volume = DoubleConstants.FourThirdsPI * Math.Pow(Radius, 3);
        }

        private Sphere(SerializationInfo info, StreamingContext context) : this(
            (double?)info.GetValue(nameof(Radius), typeof(double)) ?? default,
            (Vector3?)info.GetValue(nameof(Position), typeof(Vector3)) ?? default)
        { }

        /// <summary>
        /// Finds the point at which this instance makes contact with the given
        /// <paramref name="shape"/> when swept along the given <paramref name="path"/>, if any.
        /// </summary>
        /// <param name="path">The path along which this instance is swept.</param>
        /// <param name="shape">The <see cref="IShape"/> to check for collision with the swept
        /// sphere represented by this instance.</param>
        /// <returns>The point along the given <paramref name="path"/> at which this instance makes
        /// contact with the given <paramref name="shape"/>; or
        /// <see langword="null"/> if they do not collide.</returns>
        public Vector3? GetCollisionPoint(Vector3 path, IShape shape)
            => new Capsule(path, Radius, Position + (path / 2)).GetCollisionPoint(shape);

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
        public Sphere GetCopyAtPosition(Vector3 position) => new(Radius, position);

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
            else if (shape is Sphere sphere)
            {
                return Vector3.Distance(Position, sphere.Position) <= sphere.Radius + Radius;
            }
            else
            {
                return shape.Intersects(this);
            }
        }

        /// <summary>
        /// Determines if a given point lies within this shape.
        /// </summary>
        /// <param name="point">A point in the same 3D space as this shape.</param>
        /// <returns>True if the point is within (or tangent to) this shape.</returns>
        public bool IsPointWithin(Vector3 point)
            => (point - Position).Length() <= Radius;

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if obj and this instance are the same type and represent
        /// the same value; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object? obj) => obj is Sphere shape && Equals(shape);

        /// <summary>
        /// Determines whether the specified <see cref="IShape"/> is equivalent to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="IShape"/> is equivalent to the
        /// current object; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(IShape? obj) => obj is Sphere other && Equals(other);

        /// <summary>
        /// Determines whether the specified <see cref="Sphere"/> is equivalent to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="Sphere"/> is equivalent to the
        /// current object; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(Sphere? other) => other is not null && other.Radius == Radius && other.Position == Position;

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            var hash = (17 * 23) + Radius.GetHashCode();
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
        public IShape ScaleByDimension(double factor)
        {
            if (factor < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
            }

            return factor == 0
                ? new Sphere(0, Position)
                : new Sphere(Radius * factor, Position);
        }

        /// <summary>
        /// Scales this <see cref="IShape"/>'s dimensions such that its volume will be multiplied by
        /// the given factor.
        /// </summary>
        /// <param name="factor">The amount by which to scale this <see cref="IShape"/>'s volume.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="factor"/> must be &gt;= 0.
        /// </exception>
        public IShape ScaleVolume(double factor)
        {
            if (factor < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
            }

            return factor == 0
                ? new Sphere(0, Position)
                : new Sphere(Radius * Math.Pow(factor, 1.0 / 3.0), Position);
        }

        private bool Intersects(Line line)
        {
            var diff = line.Position - Position;
            var a0 = Vector3.Dot(diff, diff) - (Radius * Radius);
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
                return qm * qp <= 0 || (qm > 0 && Math.Abs(a1) < extent);
            }
        }

        /// <summary>
        /// Returns a boolean indicating whether the two given shapes are equal.
        /// </summary>
        /// <param name="left">The first shape to compare.</param>
        /// <param name="right">The second shape to compare.</param>
        /// <returns>True if the shapes are equal; False otherwise.</returns>
        public static bool operator ==(Sphere left, IShape right)
            => left.Equals(right);

        /// <summary>
        /// Returns a boolean indicating whether the two given shapes are not equal.
        /// </summary>
        /// <param name="left">The first shape to compare.</param>
        /// <param name="right">The second shape to compare.</param>
        /// <returns>True if the shapes are not equal; False if they are equal.</returns>
        public static bool operator !=(Sphere left, IShape right)
            => !(left == right);
    }
}
