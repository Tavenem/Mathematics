using System;
using System.Runtime.Serialization;
using Tavenem.HugeNumbers;

namespace Tavenem.Mathematics.HugeNumbers
{
    /// <summary>
    /// Provides information about the properties of a line as a shape.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Line : IShape, IEquatable<Line>, ISerializable
    {
        private readonly Vector3 _start;
        private readonly Vector3 _end;

        /// <summary>
        /// A circular radius which fully contains the shape.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public HugeNumber ContainingRadius { get; }

        /// <summary>
        /// The point on this shape highest on the Y axis.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Vector3 HighestPoint { get; }

        /// <summary>
        /// The length of the <see cref="Line"/>.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public HugeNumber Length { get; }

        /// <summary>
        /// The point on this shape lowest on the Y axis.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Vector3 LowestPoint { get; }

        /// <summary>
        /// The vector which the <see cref="Line"/> follows.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Note that <see cref="Path"/> does not match the position and extent of the <see
        /// cref="Line"/> itself, since <see cref="Position"/> is the center of the line, while <see
        /// cref="Path"/> has the same direction and the full length of the <see cref="Line"/>.
        /// </para>
        /// <para>
        /// The <see cref="Line"/>, by contrast, has the same direction as <see cref="Path"/>, but
        /// ends half its length away from <see cref="Position"/>, while extending in the reverse
        /// direction from <see cref="Position"/> for the same distance.
        /// </para>
        /// </remarks>
        [DataMember(Order = 1)]
        public Vector3 Path { get; }

        /// <summary>
        /// The position of the shape in 3D space.
        /// </summary>
        [DataMember(Order = 2)]
        public Vector3 Position { get; }

        /// <summary>
        /// The rotation of this shape in 3D space. Always <see cref="Quaternion.Identity"/> for
        /// <see cref="Line"/>, whose <see cref="Path"/> defines its orientation.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Quaternion Rotation => Quaternion.Identity;

        /// <summary>
        /// Identifies the type of this <see cref="IShape"/>.
        /// </summary>
        [DataMember(Order = 3)]
        public ShapeType ShapeType => ShapeType.Line;

        /// <summary>
        /// The length of the shortest of the three dimensions of this shape. Always 0 for <see
        /// cref="Line"/>, which has a 0-length cross-section.
        /// </summary>
        /// <remarks>
        /// Note: this is not the length of smallest possible cross-section, but of the total length
        /// of the shape along any of the three primary axes of 3D space, prior to any rotation.
        /// </remarks>
        [System.Text.Json.Serialization.JsonIgnore]
        public HugeNumber SmallestDimension => 0;

        /// <summary>
        /// The total volume of the shape. Always 0 for <see cref="Line"/>, which has a 0-length
        /// cross-section.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public HugeNumber Volume => 0;

        /// <summary>
        /// Initializes a new instance of <see cref="Line"/>.
        /// </summary>
        public Line() : this(Vector3.Zero) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Line"/>.
        /// </summary>
        /// <param name="path">The vector which the <see cref="Line"/> follows.</param>
        public Line(Vector3 path) : this(path, Vector3.Zero) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Line"/>.
        /// </summary>
        /// <param name="path">The vector which the <see cref="Line"/> follows.</param>
        /// <param name="position">The position of the shape in 3D space. Note that as with all <see
        /// cref="IShape"/> instances, this is the center of the <see cref="Line"/>, not either
        /// endpoint. See <see cref="From(Vector3, Vector3)"/> to construct a <see cref="Line"/>
        /// from an endpoint.</param>
        [System.Text.Json.Serialization.JsonConstructor]
        public Line(Vector3 path, Vector3 position)
        {
            Path = path;
            Position = position;

            Length = path.Length();

            var _halfPath = Vector3.Normalize(path) * (Length / 2);
            _start = Position - _halfPath;
            _end = _start + path;

            ContainingRadius = Length;
            if (_end.Y >= _start.Y)
            {
                HighestPoint = _end;
                LowestPoint = _start;
            }
            else
            {
                HighestPoint = _start;
                LowestPoint = _end;
            }
        }

        private Line(SerializationInfo info, StreamingContext context) : this(
            (Vector3?)info.GetValue(nameof(Path), typeof(Vector3)) ?? default,
            (Vector3?)info.GetValue(nameof(Position), typeof(Vector3)) ?? default)
        { }

        /// <summary>
        /// Gets a new <see cref="Line"/> which originates from the given <paramref name="start"/>
        /// position.
        /// </summary>
        /// <param name="start">The origin point of the <see cref="Line"/>.</param>
        /// <param name="path">The vector which the <see cref="Line"/> follows.</param>
        /// <returns></returns>
        public static Line From(Vector3 start, Vector3 path)
            => new(path, start + (Vector3.Normalize(path) * (path.Length() / 2)));

        private static void ComputeIntersection(HugeNumber mF00, HugeNumber mF10, HugeNumber mB, HugeNumber[] sValue, int[] classify, out int[] edge, out HugeNumber[,] end)
        {
            edge = new int[2];
            end = new HugeNumber[2, 2];

            if (classify[0] < 0)
            {
                edge[0] = 0;
                end[0, 0] = 0;
                end[0, 1] = mF00 / mB;
                if (end[0, 1] < 0 || end[0, 1] > 1)
                {
                    end[0, 1] = new HugeNumber(5, -1);
                }
                if (classify[1] == 0)
                {
                    edge[1] = 3;
                    end[1, 0] = sValue[1];
                    end[1, 1] = 1;
                }
                else
                {
                    edge[1] = 1;
                    end[1, 0] = 1;
                    end[1, 1] = mF10 / mB;
                    if (end[1, 1] < 0 || end[1, 1] > 1)
                    {
                        end[1, 1] = new HugeNumber(5, -1);
                    }
                }
            }
            else if (classify[0] == 0)
            {
                edge[0] = 2;
                end[0, 0] = sValue[0];
                end[0, 1] = 0;
                if (classify[1] < 0)
                {
                    edge[1] = 0;
                    end[1, 0] = 0;
                    end[1, 1] = mF00 / mB;
                    if (end[1, 1] < 0 || end[1, 1] > 1)
                    {
                        end[1, 1] = new HugeNumber(5, -1);
                    }
                }
                else if (classify[1] == 0)
                {
                    edge[1] = 3;
                    end[1, 0] = sValue[1];
                    end[1, 1] = 1;
                }
                else
                {
                    edge[1] = 1;
                    end[1, 0] = 1;
                    end[1, 1] = mF10 / mB;
                    if (end[1, 1] < 0 || end[1, 1] > 1)
                    {
                        end[1, 1] = new HugeNumber(5, -1);
                    }
                }
            }
            else if (classify[0] > 0)
            {
                edge[0] = 1;
                end[0, 0] = 1;
                end[0, 1] = mF10 / mB;
                if (end[0, 1] < 0 || end[0, 1] > 1)
                {
                    end[0, 1] = new HugeNumber(5, -1);
                }
                if (classify[1] == 0)
                {
                    edge[1] = 3;
                    end[1, 0] = sValue[1];
                    end[1, 1] = 1;
                }
                else
                {
                    edge[1] = 0;
                    end[1, 0] = 0;
                    end[1, 1] = mF00 / mB;
                    if (end[1, 1] < 0 || end[1, 1] > 1)
                    {
                        end[1, 1] = new HugeNumber(5, -1);
                    }
                }
            }
        }

        private static void ComputeMinimumParameters(HugeNumber mG00, HugeNumber mG01, HugeNumber mG10, HugeNumber mG11, HugeNumber mB, HugeNumber mC, HugeNumber mE, int[] edge, HugeNumber[,] end, out HugeNumber[] parameters)
        {
            parameters = new HugeNumber[2];
            var delta = end[1, 1] - end[0, 1];
            var h0 = delta * ((-mB * end[0, 0]) + (mC * end[0, 1]) - mE);
            if (h0 >= 0)
            {
                if (edge[0] == 0)
                {
                    parameters[0] = 0;
                    parameters[1] = GetClampedRoot(mC, mG00, mG01);
                }
                else if (edge[0] == 1)
                {
                    parameters[0] = 1;
                    parameters[1] = GetClampedRoot(mC, mG10, mG11);
                }
                else
                {
                    parameters[0] = end[0, 0];
                    parameters[1] = end[0, 1];
                }
            }
            else
            {
                var h1 = delta * ((-mB * end[1, 0]) + (mC * end[1, 1]) - mE);
                if (h1 <= 0)
                {
                    if (edge[1] == 0)
                    {
                        parameters[0] = 0;
                        parameters[1] = GetClampedRoot(mC, mG00, mG01);
                    }
                    else if (edge[1] == 1)
                    {
                        parameters[0] = 1;
                        parameters[1] = GetClampedRoot(mC, mG10, mG11);
                    }
                    else
                    {
                        parameters[0] = end[1, 0];
                        parameters[1] = end[1, 1];
                    }
                }
                else
                {
                    var z = HugeNumber.Min(HugeNumber.Max(h0 / (h0 - h1), 0), 1);
                    var omz = 1 - z;
                    parameters[0] = (omz * end[0, 0]) + (z * end[1, 0]);
                    parameters[1] = (omz * end[0, 1]) + (z * end[1, 1]);
                }
            }
        }

        private static HugeNumber GetClampedRoot(HugeNumber slope, HugeNumber h0, HugeNumber h1)
        {
            if (h0 < 0)
            {
                if (h1 > 0)
                {
                    var result = -h0 / slope;
                    return result > 1 ? new HugeNumber(5, -1) : result;
                }
                return 1;
            }
            return 0;
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
        public Line GetCopyAtPosition(Vector3 position) => new(Path, position);

        /// <summary>
        /// Gets a deep clone of this instance with its <see cref="Rotation"/> set to the given
        /// value.
        /// </summary>
        /// <param name="rotation">The new <see cref="Rotation"/> for the clone.</param>
        /// <returns>A deep clone of this instance with its <see cref="Rotation"/> set to the given
        /// value.</returns>
        public Line GetCopyWithRotation(Quaternion rotation) => new(Vector3.Transform(Path, rotation), Position);

        /// <summary>
        /// Calculates the distance to the given <paramref name="point"/>.
        /// </summary>
        /// <param name="point">A <see cref="Vector3"/> to which the distance is to be
        /// calculated.</param>
        /// <param name="closestPoint">When the method returns, will be set to the closest point on
        /// this instance to the given <paramref name="point"/>.</param>
        /// <returns>The distance to the given <paramref name="point"/>.</returns>
        public HugeNumber GetDistanceTo(Vector3 point, out Vector3 closestPoint)
        {
            var diff = point - _end;
            closestPoint = _start;
            var t = Vector3.Dot(Path, diff);
            if (t >= 0)
            {
                closestPoint = _end;
            }
            else
            {
                var sqrLn = Vector3.Dot(Path, Path);
                if (sqrLn > 0)
                {
                    t /= sqrLn;
                    closestPoint = _start + (t * Path);
                }
            }
            diff = point - closestPoint;
            return diff.Length();
        }

        /// <summary>
        /// Calculates the distance to the given <paramref name="point"/>.
        /// </summary>
        /// <param name="point">The <see cref="SinglePoint"/> to which the distance is to be
        /// calculated.</param>
        /// <param name="closestPoint">When the method returns, will be set to the closest point on
        /// this instance to the given <paramref name="point"/>.</param>
        /// <returns>The distance to the given <paramref name="point"/>.</returns>
        public HugeNumber GetDistanceTo(SinglePoint point, out Vector3 closestPoint)
            => GetDistanceTo(point.Position, out closestPoint);

        /// <summary>
        /// Calculates the distance to the given <paramref name="line"/>.
        /// </summary>
        /// <param name="line">The <see cref="Line"/> to which the distance is to be
        /// calculated.</param>
        /// <param name="closestPoint">When the method returns, will be set to the closest point on
        /// this instance to the given <paramref name="line"/>.</param>
        /// <param name="closestPointOther">When the method returns, will be set to the closest
        /// point on the given <paramref name="line"/> to this instance.</param>
        /// <returns>The distance to the given <paramref name="line"/>.</returns>
        public HugeNumber GetDistanceTo(Line line, out Vector3 closestPoint, out Vector3 closestPointOther)
        {
            var startDiff = _start - line._start;
            var mA = Vector3.Dot(Path, Path);
            var mB = Vector3.Dot(Path, line.Path);
            var mC = Vector3.Dot(line.Path, line.Path);
            var mD = Vector3.Dot(Path, startDiff);
            var mE = Vector3.Dot(line.Path, startDiff);
            var mF00 = mD;
            var mF10 = mF00 + mA;
            var mF01 = mF00 - mB;
            var mF11 = mF10 - mB;
            var mG00 = -mE;
            var mG10 = mG00 - mB;
            var mG01 = mG00 + mC;
            var mG11 = mG10 + mC;
            var parameters = new HugeNumber[2];

            if (mA > 0 && mC > 0)
            {
                var sValue = new HugeNumber[2];
                sValue[0] = GetClampedRoot(mA, mF00, mF10);
                sValue[1] = GetClampedRoot(mA, mF01, mF11);
                var classify = new int[2];
                for (var i = 0; i < 2; ++i)
                {
                    if (sValue[i] <= 0)
                    {
                        classify[i] = -1;
                    }
                    else if (sValue[i] >= 1)
                    {
                        classify[i] = 1;
                    }
                    else
                    {
                        classify[i] = 0;
                    }
                }
                if (classify[0] == -1 && classify[1] == -1)
                {
                    parameters[0] = 0;
                    parameters[1] = GetClampedRoot(mC, mG00, mG01);
                }
                else if (classify[0] == 1 && classify[1] == 1)
                {
                    parameters[0] = 1;
                    parameters[1] = GetClampedRoot(mC, mG10, mG11);
                }
                else
                {
                    ComputeIntersection(mF00, mF10, mB, sValue, classify, out var edge, out var end);
                    ComputeMinimumParameters(mG00, mG01, mG10, mG11, mB, mC, mE, edge, end, out parameters);
                }
            }
            else if (mA > 0)
            {
                parameters[0] = GetClampedRoot(mA, mF00, mF10);
                parameters[1] = 0;
            }
            else if (mC > 0)
            {
                parameters[0] = 0;
                parameters[1] = GetClampedRoot(mC, mG00, mG01);
            }
            else
            {
                parameters[0] = 0;
                parameters[1] = 0;
            }

            closestPoint = ((1 - parameters[0]) * _start) + (parameters[0] * _end);
            closestPointOther = ((1 - parameters[1]) * line._start) + (parameters[1] * line._end);
            var diff = closestPoint - closestPointOther;
            return diff.Length();
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
                return GetDistanceTo(point, out _).IsZero;
            }
            else if (shape is Line line)
            {
                return Intersects(line);
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
        public bool IsPointWithin(Vector3 point) => GetDistanceTo(point, out var _).IsZero;

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if obj and this instance are the same type and represent
        /// the same value; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object? obj) => obj is Line shape && Equals(shape);

        /// <summary>
        /// Determines whether the specified <see cref="IShape"/> is equivalent to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="IShape"/> is equivalent to the
        /// current object; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(IShape? obj) => obj is Line other && Equals(other);

        /// <summary>
        /// Determines whether the specified <see cref="Line"/> is equivalent to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="Line"/> is equivalent to the
        /// current object; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(Line? other) => other is not null && other.Path == Path && other.Position == Position;

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            var hash = (17 * 23) + Path.GetHashCode();
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
            info.AddValue(nameof(Path), Path);
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

            return factor == 0
                ? new Line(Vector3.Zero, Position)
                : new Line(Path * factor, Position);
        }

        /// <summary>
        /// <para>
        /// Scales this <see cref="IShape"/>'s dimensions such that its volume will be multiplied by
        /// the given factor.
        /// </para>
        /// <para>
        /// Always returns the original instance for <see cref="Line"/>, which has no <see
        /// cref="Volume"/>.
        /// </para>
        /// </summary>
        /// <param name="factor">The amount by which to scale this <see cref="IShape"/>'s volume.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="factor"/> must be &gt;= 0.
        /// </exception>
        public IShape ScaleVolume(HugeNumber factor) => this;

        private bool Intersects(Line line)
        {
            var startDiff = line._start - _start;
            var crossPaths = Vector3.Cross(Path, line.Path);

            if (!Vector3.Dot(startDiff, crossPaths).IsZero)
            {
                return false;
            }

            var s = Vector3.Dot(Vector3.Cross(startDiff, line.Path), crossPaths) / Vector3.Dot(crossPaths, crossPaths);
            return s >= 0 && s <= 1;
        }

        /// <summary>
        /// Returns a boolean indicating whether the two given shapes are equal.
        /// </summary>
        /// <param name="left">The first shape to compare.</param>
        /// <param name="right">The second shape to compare.</param>
        /// <returns>True if the shapes are equal; False otherwise.</returns>
        public static bool operator ==(Line left, IShape right)
            => left.Equals(right);

        /// <summary>
        /// Returns a boolean indicating whether the two given shapes are not equal.
        /// </summary>
        /// <param name="left">The first shape to compare.</param>
        /// <param name="right">The second shape to compare.</param>
        /// <returns>True if the shapes are not equal; False if they are equal.</returns>
        public static bool operator !=(Line left, IShape right)
            => !(left == right);
    }
}
