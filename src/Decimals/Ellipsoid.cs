using System;
using System.Runtime.Serialization;

namespace Tavenem.Mathematics.Decimals
{
    /// <summary>
    /// Provides information about the properties of an ellipsoid.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Ellipsoid : IShape, IEquatable<Ellipsoid>, ISerializable
    {
        /// <summary>
        /// The length of the X half-axis of the <see cref="Ellipsoid"/> extending from its center
        /// to its surface.
        /// </summary>
        [DataMember(Order = 1)]
        public decimal AxisX { get; }

        /// <summary>
        /// The length of the Y half-axis of the <see cref="Ellipsoid"/> extending from its center
        /// to its surface.
        /// </summary>
        [DataMember(Order = 2)]
        public decimal AxisY { get; }

        /// <summary>
        /// The length of the Z half-axis of the <see cref="Ellipsoid"/> extending from its center
        /// to its surface.
        /// </summary>
        [DataMember(Order = 3)]
        public decimal AxisZ { get; }

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
        [DataMember(Order = 4)]
        public Vector3 Position { get; }

        /// <summary>
        /// The rotation of this shape in 3D space.
        /// </summary>
        [DataMember(Order = 5)]
        public Quaternion Rotation { get; }

        /// <summary>
        /// Identifies the type of this <see cref="IShape"/>.
        /// </summary>
        [DataMember(Order = 6)]
        public ShapeType ShapeType => ShapeType.Ellipsoid;

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
        /// The total volume of the shape. Read-only; set by setting the axes.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public decimal Volume { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Ellipsoid"/>.
        /// </summary>
        public Ellipsoid() : this(0, 0, 0) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Ellipsoid"/> with the given parameters.
        /// </summary>
        /// <param name="axisX">The length of the first radius of the <see cref="Ellipsoid"/>.</param>
        /// <param name="axisY">The length of the second radius of the <see cref="Ellipsoid"/>.</param>
        /// <param name="axisZ">The length of the third radius of the <see cref="Ellipsoid"/>.</param>
        public Ellipsoid(decimal axisX, decimal axisY, decimal axisZ)
            : this(axisX, axisY, axisZ, Vector3.Zero, Quaternion.Identity) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Ellipsoid"/> with the given parameters.
        /// </summary>
        /// <param name="axisX">The length of the first radius of the <see cref="Ellipsoid"/>.</param>
        /// <param name="axisY">The length of the second radius of the <see cref="Ellipsoid"/>.</param>
        /// <param name="axisZ">The length of the third radius of the <see cref="Ellipsoid"/>.</param>
        /// <param name="position">The position of the shape in 3D space.</param>
        public Ellipsoid(decimal axisX, decimal axisY, decimal axisZ, Vector3 position)
            : this(axisX, axisY, axisZ, position, Quaternion.Identity) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Ellipsoid"/> with the given parameters.
        /// </summary>
        /// <param name="axisX">The length of the first radius of the <see cref="Ellipsoid"/>.</param>
        /// <param name="axisY">The length of the second radius of the <see cref="Ellipsoid"/>.</param>
        /// <param name="axisZ">The length of the third radius of the <see cref="Ellipsoid"/>.</param>
        /// <param name="position">The position of the shape in 3D space.</param>
        /// <param name="rotation">The rotation of this shape in 3D space.</param>
        [System.Text.Json.Serialization.JsonConstructor]
        public Ellipsoid(decimal axisX, decimal axisY, decimal axisZ, Vector3 position, Quaternion rotation)
        {
            AxisX = axisX;
            AxisY = axisY;
            AxisZ = axisZ;
            Position = position;
            Rotation = rotation;

            ContainingRadius = Math.Max(AxisX, Math.Max(AxisY, AxisZ));

            var x = Vector3.Transform(Vector3.UnitX * AxisX, Rotation);
            var y = Vector3.Transform(Vector3.UnitY * AxisY, Rotation);
            var z = Vector3.Transform(Vector3.UnitZ * AxisZ, Rotation);
            var points = new Vector3[6]
            {
                Position + x,
                Position + y,
                Position + z,
                Position - x,
                Position - y,
                Position - z,
            };
            HighestPoint = points[0];
            LowestPoint = points[0];
            for (var i = 0; i < 6; i++)
            {
                if (points[i].Y > HighestPoint.Y)
                {
                    HighestPoint = points[i];
                }
                if (points[i].Y < LowestPoint.Y)
                {
                    LowestPoint = points[i];
                }
            }

            SmallestDimension = Math.Min(AxisX, Math.Min(AxisY, AxisZ));
            Volume = DecimalConstants.FourThirdsPI * AxisX * AxisY * AxisZ;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="Ellipsoid"/> as an oblate spheroid with the given parameters.
        /// </summary>
        /// <param name="radius">The equatorial radius of the oblate spheroid.</param>
        /// <param name="axis">The axis length of the oblate spheroid, from the center to the
        /// perimeter.</param>
        public Ellipsoid(decimal radius, decimal axis)
            : this(radius, axis, radius, Vector3.Zero, Quaternion.Identity) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Ellipsoid"/> as an oblate spheroid with the
        /// given parameters.
        /// </summary>
        /// <param name="radius">The equatorial radius of the oblate spheroid.</param>
        /// <param name="axis">The axis length of the oblate spheroid, from the center to the
        /// perimeter.</param>
        /// <param name="position">The position of the shape in 3D space.</param>
        public Ellipsoid(decimal radius, decimal axis, Vector3 position)
            : this(radius, axis, radius, position, Quaternion.Identity) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Ellipsoid"/> as an oblate spheroid with the given parameters.
        /// </summary>
        /// <param name="radius">The equatorial radius of the oblate spheroid.</param>
        /// <param name="axis">The axis length of the oblate spheroid, from the center to the
        /// perimeter.</param>
        /// <param name="position">The position of the shape in 3D space.</param>
        /// <param name="rotation">The rotation of this shape in 3D space.</param>
        public Ellipsoid(decimal radius, decimal axis, Vector3 position, Quaternion rotation)
            : this(radius, axis, radius, position, rotation) { }

        private Ellipsoid(SerializationInfo info, StreamingContext context) : this(
            (decimal?)info.GetValue(nameof(AxisX), typeof(decimal)) ?? default,
            (decimal?)info.GetValue(nameof(AxisY), typeof(decimal)) ?? default,
            (decimal?)info.GetValue(nameof(AxisZ), typeof(decimal)) ?? default,
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
        public Ellipsoid GetCopyAtPosition(Vector3 position) => new(AxisX, AxisY, AxisZ, position, Rotation);

        /// <summary>
        /// Gets a deep clone of this instance with its <see cref="Rotation"/> set to the given
        /// value.
        /// </summary>
        /// <param name="rotation">The new <see cref="Rotation"/> for the clone.</param>
        /// <returns>A deep clone of this instance with its <see cref="Rotation"/> set to the given
        /// value.</returns>
        public Ellipsoid GetCopyWithRotation(Quaternion rotation) => new(AxisX, AxisY, AxisZ, Position, rotation);

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

            // First-pass check against overlapping containing radii.
            if (Vector3.Distance(Position, shape.Position) > shape.ContainingRadius + ContainingRadius)
            {
                return false;
            }

            if (shape is not Line and not Cuboid)
            {
                // For all other shapes rely on containing radii overlap only.
                return true;
            }

            var s = new Matrix4x4(
                AxisX, 0, 0, 0,
                0, AxisY, 0, 0,
                0, 0, AxisZ, 0,
                0, 0, 0, 1);
            var r = Matrix4x4.CreateFromQuaternion(Rotation);
            var t = Matrix4x4.CreateTranslation(Position);
            Matrix4x4.Invert(t * r * s, out var invK);

            var iSphere = new Sphere(1);

            if (shape is Line line)
            {
                return iSphere.Intersects(new Line(Vector3.Transform(line.Path, invK), Vector3.Transform(line.Position, invK)));
            }
            else if (shape is Cuboid cuboid)
            {
                for (var i = 0; i < 8; i += 2)
                {
                    var l = Line.From(cuboid.Corners[i], cuboid.Corners[i + 1]);
                    var intersect = iSphere.Intersects(new Line(Vector3.Transform(l.Path, invK), Vector3.Transform(l.Position, invK)));
                    if (intersect)
                    {
                        return true;
                    }
                }
                return false;
            }

            return false;
        }

        /// <summary>
        /// Determines if a given point lies within this shape.
        /// </summary>
        /// <param name="point">A point in the same 3-D space as this shape.</param>
        /// <returns>True if the point is within (or tangent to) this shape.</returns>
        public bool IsPointWithin(Vector3 point) =>
            (decimal)Math.Pow((double)((point.X - Position.X) / AxisX), 2) + (decimal)Math.Pow((double)((point.Y - Position.Y) / AxisY), 2)
            + (decimal)Math.Pow((double)((point.Z - Position.Z) / AxisZ), 2) <= 1;

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if obj and this instance are the same type and represent
        /// the same value; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object? obj) => obj is Ellipsoid shape && Equals(shape);

        /// <summary>
        /// Determines whether the specified <see cref="IShape"/> is equivalent to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="IShape"/> is equivalent to the
        /// current object; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(IShape? obj) => obj is Ellipsoid other && Equals(other);

        /// <summary>
        /// Determines whether the specified <see cref="Ellipsoid"/> is equivalent to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="Ellipsoid"/> is equivalent to the
        /// current object; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(Ellipsoid? other)
            => other is not null && other.AxisX == AxisX && other.AxisY == AxisY && other.AxisZ == AxisZ && other.Position == Position && other.Rotation == Rotation;

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            var hash = (17 * 23) + AxisX.GetHashCode();
            hash = (hash * 23) + AxisY.GetHashCode();
            hash = (hash * 23) + AxisZ.GetHashCode();
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
            info.AddValue(nameof(AxisX), AxisX);
            info.AddValue(nameof(AxisY), AxisY);
            info.AddValue(nameof(AxisZ), AxisZ);
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

            return new Ellipsoid(AxisX * factor, AxisY * factor, AxisZ * factor, Position, Rotation);
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
                return new Ellipsoid(0, 0, 0, Position);
            }
            else
            {
                var multiplier = (decimal)Math.Pow((double)factor, 1.0 / 3.0);
                return new Ellipsoid(AxisX * multiplier, AxisY * multiplier, AxisZ * multiplier, Position, Rotation);
            }
        }

        /// <summary>
        /// Returns a boolean indicating whether the two given shapes are equal.
        /// </summary>
        /// <param name="left">The first shape to compare.</param>
        /// <param name="right">The second shape to compare.</param>
        /// <returns>True if the shapes are equal; False otherwise.</returns>
        public static bool operator ==(Ellipsoid left, IShape right)
            => left.Equals(right);

        /// <summary>
        /// Returns a boolean indicating whether the two given shapes are not equal.
        /// </summary>
        /// <param name="left">The first shape to compare.</param>
        /// <param name="right">The second shape to compare.</param>
        /// <returns>True if the shapes are not equal; False if they are equal.</returns>
        public static bool operator !=(Ellipsoid left, IShape right)
            => !(left == right);
    }
}
