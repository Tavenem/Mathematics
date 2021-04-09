using System;
using System.Runtime.Serialization;
using Tavenem.HugeNumbers;

namespace Tavenem.Mathematics.HugeNumbers
{
    /// <summary>
    /// Provides information about the properties of an cuboid.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Cuboid : IShape, IEquatable<Cuboid>, ISerializable
    {
        /// <summary>
        /// The length of the <see cref="Cuboid"/> in the X dimension.
        /// </summary>
        [DataMember(Order = 1)]
        public HugeNumber AxisX { get; }

        /// <summary>
        /// The length of the <see cref="Cuboid"/> in the Y dimension.
        /// </summary>
        [DataMember(Order = 2)]
        public HugeNumber AxisY { get; }

        /// <summary>
        /// The length of the <see cref="Cuboid"/> in the Z dimension.
        /// </summary>
        [DataMember(Order = 3)]
        public HugeNumber AxisZ { get; }

        /// <summary>
        /// A circular radius which fully contains the shape.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public HugeNumber ContainingRadius { get; }

        /// <summary>
        /// <para>
        /// The positions of the eight corners which form this cuboid.
        /// </para>
        /// <para>
        /// The corners are given in an order such that each pair of consecutive corners (as well as
        /// the first and last) form an edge.
        /// </para>
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Vector3[] Corners { get; }

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
        public ShapeType ShapeType => ShapeType.Cuboid;

        /// <summary>
        /// The length of the shortest of the three dimensions of this shape.
        /// </summary>
        /// <remarks>
        /// Note: this is not the length of smallest possible cross-section, but of the total length
        /// of the shape along any of the three primary axes of 3D space, prior to any rotation.
        /// </remarks>
        [System.Text.Json.Serialization.JsonIgnore]
        public HugeNumber SmallestDimension { get; }

        /// <summary>
        /// The total volume of the shape. Read-only; set by setting the axes.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public HugeNumber Volume { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="Cuboid"/>.
        /// </summary>
        public Cuboid() : this(HugeNumber.Zero, HugeNumber.Zero, HugeNumber.Zero) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Cuboid"/> with the given parameters.
        /// </summary>
        /// <param name="axisX">The length of the <see cref="Cuboid"/> in the X dimension.</param>
        /// <param name="axisY">The length of the <see cref="Cuboid"/> in the Y dimension.</param>
        /// <param name="axisZ">The length of the <see cref="Cuboid"/> in the Z dimension.</param>
        public Cuboid(HugeNumber axisX, HugeNumber axisY, HugeNumber axisZ)
            : this(axisX, axisY, axisZ, Vector3.Zero, Quaternion.Identity) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Cuboid"/> with the given parameters.
        /// </summary>
        /// <param name="axisX">The length of the <see cref="Cuboid"/> in the X dimension.</param>
        /// <param name="axisY">The length of the <see cref="Cuboid"/> in the Y dimension.</param>
        /// <param name="axisZ">The length of the <see cref="Cuboid"/> in the Z dimension.</param>
        /// <param name="position">The position of the shape in 3D space.</param>
        public Cuboid(HugeNumber axisX, HugeNumber axisY, HugeNumber axisZ, Vector3 position)
            : this(axisX, axisY, axisZ, position, Quaternion.Identity) { }

        /// <summary>
        /// Initializes a new instance of <see cref="Cuboid"/> with the given parameters.
        /// </summary>
        /// <param name="axisX">The length of the <see cref="Cuboid"/> in the X dimension.</param>
        /// <param name="axisY">The length of the <see cref="Cuboid"/> in the Y dimension.</param>
        /// <param name="axisZ">The length of the <see cref="Cuboid"/> in the Z dimension.</param>
        /// <param name="position">The position of the shape in 3D space.</param>
        /// <param name="rotation">The rotation of this shape in 3D space.</param>
        [System.Text.Json.Serialization.JsonConstructor]
        public Cuboid(HugeNumber axisX, HugeNumber axisY, HugeNumber axisZ, Vector3 position, Quaternion rotation)
        {
            AxisX = axisX;
            AxisY = axisY;
            AxisZ = axisZ;
            Position = position;
            Rotation = rotation;

            ContainingRadius = HugeNumber.Sqrt((AxisX * AxisX) + (AxisY * AxisY) + (AxisZ * AxisZ)) / 2;
            SmallestDimension = HugeNumber.Min(AxisX, HugeNumber.Min(AxisY, AxisZ));
            Volume = AxisX * AxisY * AxisZ;

            var x = Vector3.Transform(Vector3.UnitX * (AxisX / 2), Rotation);
            var y = Vector3.Transform(Vector3.UnitY * (AxisY / 2), Rotation);
            var z = Vector3.Transform(Vector3.UnitZ * (AxisZ / 2), Rotation);

            Corners = new Vector3[8]
            {
                Position + x + y + z,
                Position + x - y + z,
                Position + x - y - z,
                Position + x + y - z,
                Position - x + y - z,
                Position - x - y - z,
                Position - x - y + z,
                Position - x + y + z
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
        }

        private Cuboid(SerializationInfo info, StreamingContext context) : this(
            (HugeNumber?)info.GetValue(nameof(AxisX), typeof(HugeNumber)) ?? default,
            (HugeNumber?)info.GetValue(nameof(AxisY), typeof(HugeNumber)) ?? default,
            (HugeNumber?)info.GetValue(nameof(AxisZ), typeof(HugeNumber)) ?? default,
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
        public Cuboid GetCopyAtPosition(Vector3 position) => new(AxisX, AxisY, AxisZ, position, Rotation);

        /// <summary>
        /// Gets a deep clone of this instance with its <see cref="Rotation"/> set to the given
        /// value.
        /// </summary>
        /// <param name="rotation">The new <see cref="Rotation"/> for the clone.</param>
        /// <returns>A deep clone of this instance with its <see cref="Rotation"/> set to the given
        /// value.</returns>
        public Cuboid GetCopyWithRotation(Quaternion rotation) => new(AxisX, AxisY, AxisZ, Position, rotation);

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
            if (shape is Cone cone)
            {
                return Intersects(cone);
            }
            if (shape is Cuboid cuboid)
            {
                return Intersects(cuboid);
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
            var diff = point - Position;
            var orthonormalBasis = new Vector3[]
            {
                Vector3.Transform(Vector3.UnitX, Rotation),
                Vector3.Transform(Vector3.UnitY, Rotation),
                Vector3.Transform(Vector3.UnitZ, Rotation)
            };
            var closest = new HugeNumber[3];
            for (var i = 0; i < 3; i++)
            {
                closest[i] = Vector3.Dot(diff, orthonormalBasis[i]);
            }

            var sqrDist = HugeNumber.Zero;
            for (var i = 0; i < 3; i++)
            {
                HugeNumber a;
                if (i == 0)
                {
                    a = AxisX / 2;
                }
                else if (i == 1)
                {
                    a = AxisY / 2;
                }
                else
                {
                    a = AxisZ / 2;
                }

                HugeNumber p;
                if (i == 0)
                {
                    p = point.X;
                }
                else if (i == 1)
                {
                    p = point.Y;
                }
                else
                {
                    p = point.Z;
                }

                if (closest[i] < -a)
                {
                    var delta = p + a;
                    sqrDist += delta * delta;
                    closest[i] = -a;
                }
                else if (p > a)
                {
                    var delta = p - a;
                    sqrDist += delta * delta;
                    closest[i] = a;
                }
            }
            return sqrDist <= 0;
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><see langword="true"/> if obj and this instance are the same type and represent
        /// the same value; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object? obj) => obj is Cuboid shape && Equals(shape);

        /// <summary>
        /// Determines whether the specified <see cref="IShape"/> is equivalent to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="IShape"/> is equivalent to the
        /// current object; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(IShape? obj) => obj is Cuboid other && Equals(other);

        /// <summary>
        /// Determines whether the specified <see cref="Cuboid"/> is equivalent to the current object.
        /// </summary>
        /// <param name="other">The object to compare with the current object.</param>
        /// <returns>
        /// <see langword="true"/> if the specified <see cref="Cuboid"/> is equivalent to the
        /// current object; otherwise, <see langword="false"/>.
        /// </returns>
        public bool Equals(Cuboid? other)
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
        public IShape ScaleByDimension(HugeNumber factor)
        {
            if (factor < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
            }

            return new Cuboid(AxisX * factor, AxisY * factor, AxisZ * factor, Position, Rotation);
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
                return new Cuboid(0, 0, 0, Position);
            }
            else
            {
                var multiplier = HugeNumber.Pow(factor, HugeNumber.Third);
                return new Cuboid(AxisX * multiplier, AxisY * multiplier, AxisZ * multiplier, Position, Rotation);
            }
        }

        private bool Intersects(Cone cone)
        {
            var orthonormalBasis = new Vector3[]
            {
                Vector3.Transform(Vector3.UnitX, Rotation),
                Vector3.Transform(Vector3.UnitY, Rotation),
                Vector3.Transform(Vector3.UnitZ, Rotation)
            };
            var CmV = Position - cone.Position;
            var coneDir = Vector3.Normalize(cone.Axis);
            var DdU = new Vector3(
                Vector3.Dot(coneDir, orthonormalBasis[0]),
                Vector3.Dot(coneDir, orthonormalBasis[1]),
                Vector3.Dot(coneDir, orthonormalBasis[2]));
            var DdCmV = Vector3.Dot(coneDir, CmV);
            var eX = AxisX / 2;
            var eY = AxisY / 2;
            var eZ = AxisZ / 2;
            var radius = (eX * HugeNumber.Abs(DdU.X))
                + (eY * HugeNumber.Abs(DdU.Y))
                + (eZ * HugeNumber.Abs(DdU.Z));
            if (DdCmV + radius <= 0)
            {
                return false;
            }
            if (DdCmV - radius >= cone.Length)
            {
                return false;
            }
            var UdCmV = new Vector3(
                Vector3.Dot(orthonormalBasis[0], CmV),
                Vector3.Dot(orthonormalBasis[1], CmV),
                Vector3.Dot(orthonormalBasis[1], CmV));
            var index = new int[3];
            if (UdCmV.X < -eX)
            {
                index[0] = 2;
            }
            else if (UdCmV.X > eX)
            {
                index[0] = 0;
            }
            else
            {
                index[0] = 1;
            }

            if (UdCmV.Y < -eY)
            {
                index[1] = 2;
            }
            else if (UdCmV.Y > eY)
            {
                index[1] = 0;
            }
            else
            {
                index[1] = 1;
            }

            if (UdCmV.Z < -eZ)
            {
                index[2] = 2;
            }
            else if (UdCmV.Z > eZ)
            {
                index[2] = 0;
            }
            else
            {
                index[2] = 1;
            }

            var lookup = index[0] + (3 * index[1]) + (9 * index[2]);
            if (lookup == 13)
            {
                return true;
            }
            var polygon = lookup switch
            {
                0 => new int[] { 1, 5, 4, 6, 2, 3 },
                1 => new int[] { 0, 2, 3, 1, 5, 4 },
                2 => new int[] { 0, 2, 3, 7, 5, 4 },
                3 => new int[] { 0, 4, 6, 2, 3, 1 },
                4 => new int[] { 0, 2, 3, 1 },
                5 => new int[] { 0, 2, 3, 7, 5, 1 },
                6 => new int[] { 0, 4, 6, 7, 3, 1 },
                7 => new int[] { 0, 2, 6, 7, 3, 1 },
                8 => new int[] { 0, 2, 6, 7, 5, 1 },
                9 => new int[] { 0, 1, 5, 4, 6, 2 },
                10 => new int[] { 0, 1, 5, 4 },
                11 => new int[] { 0, 1, 3, 7, 5, 4 },
                12 => new int[] { 0, 4, 6, 2 },
                14 => new int[] { 1, 3, 7, 5 },
                15 => new int[] { 0, 4, 6, 7, 3, 2 },
                16 => new int[] { 2, 6, 7, 3 },
                17 => new int[] { 1, 3, 2, 6, 7, 5 },
                18 => new int[] { 0, 1, 5, 7, 6, 2 },
                19 => new int[] { 0, 1, 5, 7, 6, 4 },
                20 => new int[] { 0, 1, 3, 7, 6, 4 },
                21 => new int[] { 0, 4, 5, 7, 6, 2 },
                22 => new int[] { 4, 5, 7, 6 },
                23 => new int[] { 1, 3, 7, 6, 4, 5 },
                24 => new int[] { 0, 4, 5, 7, 3, 2 },
                25 => new int[] { 2, 6, 4, 5, 7, 3 },
                26 => new int[] { 1, 3, 2, 6, 4, 5 },
                _ => Array.Empty<int>(),
            };

            var cosAngle = HugeNumber.Cos(HugeNumber.Atan(cone.Radius / cone.Length));
            var cosSqr = cosAngle * cosAngle;
            Vector3[] X = new Vector3[8], PmV = new Vector3[8];
            HugeNumber[] DdPmV = new HugeNumber[8], sqrDdPmV = new HugeNumber[8], sqrLenPmV = new HugeNumber[8];
            int iMax = -1, jMax = -1;
            for (var i = 0; i < polygon.Length; ++i)
            {
                var j = polygon[i];
                X[j] = new Vector3(
                    (j & 1) != 0 ? eX : -eX,
                    (j & 2) != 0 ? eY : -eY,
                    (j & 4) != 0 ? eZ : -eZ);
                DdPmV[j] = Vector3.Dot(DdU, X[j]) + DdCmV;
                if (DdPmV[j] > 0)
                {
                    PmV[j] = X[j] + CmV;
                    sqrDdPmV[j] = DdPmV[j] * DdPmV[j];
                    sqrLenPmV[j] = Vector3.Dot(PmV[j], PmV[j]);
                    if (sqrDdPmV[j] - (cosSqr * sqrLenPmV[j]) > 0)
                    {
                        return true;
                    }
                    if (iMax == -1 || sqrDdPmV[j] * sqrLenPmV[jMax] > sqrDdPmV[jMax] * sqrLenPmV[j])
                    {
                        iMax = i;
                        jMax = j;
                    }
                }
            }

            if (iMax == -1)
            {
                return false;
            }

            var maxSqrLenPmV = sqrLenPmV[jMax];
            var maxDdPmV = DdPmV[jMax];
            var jDiff = polygon[iMax < polygon.Length - 1 ? iMax + 1 : 0] - jMax;
            var s = jDiff > 0 ? 1 : -1;
            var k0 = Math.Abs(jDiff) >> 1;
            var DdUA = new HugeNumber[3]
            {
                DdU.X,
                DdU.Y,
                DdU.Z,
            };
            var maxPmVA = new HugeNumber[3]
            {
                PmV[jMax].X,
                PmV[jMax].Y,
                PmV[jMax].Z,
            };
            var fder = s * ((DdUA[k0] * maxSqrLenPmV) - (maxDdPmV * maxPmVA[k0]));
            var mMod3 = new int[] { 0, 1, 2, 0 };

            if (fder <= 0)
            {
                jDiff = jMax - polygon[iMax > 0 ? iMax - 1 : polygon.Length - 1];
                s = jDiff > 0 ? 1 : -1;
                k0 = Math.Abs(jDiff) >> 1;
                fder = -s * ((DdUA[k0] * maxSqrLenPmV) - (maxDdPmV * maxPmVA[k0]));
            }
            if (fder > 0)
            {
                var k1 = mMod3[k0 + 1];
                var k2 = mMod3[k1 + 1];
                var denom = (DdUA[k1] * maxPmVA[k1]) + (DdUA[k2] * maxPmVA[k2]);
                var MmVA = new HugeNumber[3] { 0, 0, 0, };
                var maxXA = new HugeNumber[3]
                {
                    X[jMax].X,
                    X[jMax].Y,
                    X[jMax].Z,
                };
                var CmVA = new HugeNumber[3]
                {
                    CmV.X,
                    CmV.Y,
                    CmV.Z,
                };
                MmVA[k0] = ((maxPmVA[k1] * maxPmVA[k1]) + (maxPmVA[k2] * maxPmVA[k2])) * DdUA[k0];
                MmVA[k1] = denom * (maxXA[k1] + CmVA[k1]);
                MmVA[k2] = denom * (maxXA[k2] + CmVA[k2]);
                var MmV = new Vector3(MmVA[0], MmVA[1], MmVA[2]);

                var DdMmV = Vector3.Dot(DdU, MmV);
                if ((DdMmV * DdMmV) - (cosSqr * Vector3.Dot(MmV, MmV)) > 0)
                {
                    return true;
                }
                return s * ((DdUA[k1] * maxPmVA[k2]) - (DdUA[k2] * maxPmVA[k1])) <= 0;
            }
            return false;
        }

        private bool Intersects(Cuboid cuboid)
        {
            // First-pass check against overlapping containing radii.
            if (Vector3.Distance(Position, cuboid.Position) > cuboid.ContainingRadius + ContainingRadius)
            {
                return false;
            }

            // Separating axis test

            var orthonormalBasis = new Vector3[]
            {
                Vector3.Transform(Vector3.UnitX, Rotation),
                Vector3.Transform(Vector3.UnitY, Rotation),
                Vector3.Transform(Vector3.UnitZ, Rotation)
            };
            var otherOrthonormalBasis = new Vector3[]
            {
                Vector3.Transform(Vector3.UnitX, cuboid.Rotation),
                Vector3.Transform(Vector3.UnitY, cuboid.Rotation),
                Vector3.Transform(Vector3.UnitZ, cuboid.Rotation)
            };

            var v = cuboid.Position - Position; // translation in parent space

            // translation in this box's space
            var T = new Vector3(
                Vector3.Dot(v, orthonormalBasis[0]),
                Vector3.Dot(v, orthonormalBasis[1]),
                Vector3.Dot(v, orthonormalBasis[2]));

            // other's basis with respect to this box's local space
            var R = new HugeNumber[3, 3];
            for (var i = 0; i < 3; i++)
            {
                for (var k = 0; k < 3; k++)
                {
                    R[i, k] = Vector3.Dot(orthonormalBasis[i], otherOrthonormalBasis[k]);
                }
            }

            HugeNumber ra, rb, t;

            // this box's basis vectors
            for (var i = 0; i < 3; i++)
            {
                if (i == 0)
                {
                    ra = AxisX;
                }
                else if (i == 1)
                {
                    ra = AxisY;
                }
                else
                {
                    ra = AxisZ;
                }
                rb = (cuboid.AxisX * HugeNumber.Abs(R[i, 0]))
                    + (cuboid.AxisY * HugeNumber.Abs(R[i, 1]))
                    + (cuboid.AxisZ * HugeNumber.Abs(R[i, 2]));
                if (i == 0)
                {
                    t = HugeNumber.Abs(T.X);
                }
                else if (i == 1)
                {
                    t = HugeNumber.Abs(T.Y);
                }
                else
                {
                    t = HugeNumber.Abs(T.Z);
                }
                if (t > ra + rb)
                {
                    return false;
                }
            }

            // other's basis vectors
            for (var k = 0; k < 3; k++)
            {
                if (k == 0)
                {
                    rb = cuboid.AxisX;
                }
                else if (k == 1)
                {
                    rb = cuboid.AxisY;
                }
                else
                {
                    rb = cuboid.AxisZ;
                }
                ra = (AxisX * HugeNumber.Abs(R[0, k]))
                    + (AxisY * HugeNumber.Abs(R[1, k]))
                    + (AxisZ * HugeNumber.Abs(R[2, k]));
                t = HugeNumber.Abs((T.X * R[0, k]) + (T.Y * R[1, k]) + (T.Z * R[2, k]));
                if (t > ra + rb)
                {
                    return false;
                }
            }

            // 9 cross products

            // L = A0 x B0
            ra = (AxisY * HugeNumber.Abs(R[2, 0])) + (AxisZ * HugeNumber.Abs(R[1, 0]));
            rb = (cuboid.AxisY * HugeNumber.Abs(R[0, 2])) + (AxisZ * HugeNumber.Abs(R[0, 1]));
            t = HugeNumber.Abs((T.Z * R[1, 0]) - (T.Y * R[2, 0]));
            if (t > ra + rb)
            {
                return false;
            }

            // L = A0 x B1
            ra = (AxisY * HugeNumber.Abs(R[2, 1])) + (AxisZ * HugeNumber.Abs(R[1, 1]));
            rb = (cuboid.AxisX * HugeNumber.Abs(R[0, 2])) + (AxisZ * HugeNumber.Abs(R[0, 0]));
            t = HugeNumber.Abs((T.Z * R[1, 1]) - (T.Y * R[2, 1]));
            if (t > ra + rb)
            {
                return false;
            }

            // L = A0 x B2
            ra = (AxisY * HugeNumber.Abs(R[2, 2])) + (AxisZ * HugeNumber.Abs(R[1, 2]));
            rb = (cuboid.AxisX * HugeNumber.Abs(R[0, 1])) + (AxisY * HugeNumber.Abs(R[0, 0]));
            t = HugeNumber.Abs((T.Z * R[1, 2]) - (T.Y * R[2, 2]));
            if (t > ra + rb)
            {
                return false;
            }

            // L = A1 x B0
            ra = (AxisX * HugeNumber.Abs(R[2, 0])) + (AxisZ * HugeNumber.Abs(R[0, 0]));
            rb = (cuboid.AxisY * HugeNumber.Abs(R[1, 2])) + (AxisZ * HugeNumber.Abs(R[1, 1]));
            t = HugeNumber.Abs((T.X * R[2, 0]) - (T.Z * R[0, 0]));
            if (t > ra + rb)
            {
                return false;
            }

            // L = A1 x B1
            ra = (AxisX * HugeNumber.Abs(R[2, 1])) + (AxisZ * HugeNumber.Abs(R[0, 1]));
            rb = (cuboid.AxisX * HugeNumber.Abs(R[1, 2])) + (AxisZ * HugeNumber.Abs(R[1, 0]));
            t = HugeNumber.Abs((T.X * R[2, 1]) - (T.Z * R[0, 1]));
            if (t > ra + rb)
            {
                return false;
            }

            // L = A1 x B2
            ra = (AxisX * HugeNumber.Abs(R[2, 2])) + (AxisZ * HugeNumber.Abs(R[0, 2]));
            rb = (cuboid.AxisX * HugeNumber.Abs(R[1, 1])) + (AxisY * HugeNumber.Abs(R[1, 0]));
            t = HugeNumber.Abs((T.X * R[2, 2]) - (T.Z * R[0, 2]));
            if (t > ra + rb)
            {
                return false;
            }

            // L = A2 x B0
            ra = (AxisX * HugeNumber.Abs(R[1, 0])) + (AxisY * HugeNumber.Abs(R[0, 0]));
            rb = (cuboid.AxisY * HugeNumber.Abs(R[2, 2])) + (AxisZ * HugeNumber.Abs(R[2, 1]));
            t = HugeNumber.Abs((T.Y * R[0, 0]) - (T.X * R[1, 0]));
            if (t > ra + rb)
            {
                return false;
            }

            // L = A2 x B1
            ra = (AxisX * HugeNumber.Abs(R[1, 1])) + (AxisY * HugeNumber.Abs(R[0, 1]));
            rb = (cuboid.AxisX * HugeNumber.Abs(R[2, 2])) + (AxisZ * HugeNumber.Abs(R[2, 0]));
            t = HugeNumber.Abs((T.Y * R[0, 1]) - (T.X * R[1, 1]));
            if (t > ra + rb)
            {
                return false;
            }

            // L = A2 x B2
            ra = (AxisX * HugeNumber.Abs(R[1, 2])) + (AxisY * HugeNumber.Abs(R[0, 2]));
            rb = (cuboid.AxisX * HugeNumber.Abs(R[2, 1])) + (AxisY * HugeNumber.Abs(R[2, 0]));
            t = HugeNumber.Abs((T.Y * R[0, 2]) - (T.X * R[1, 2]));

            return t <= ra + rb;
        }

        private bool Intersects(Line line)
        {
            var diff = line.Position - Position;
            var orthonormalBasis = new Vector3[]
            {
                Vector3.Transform(Vector3.UnitX, Rotation),
                Vector3.Transform(Vector3.UnitY, Rotation),
                Vector3.Transform(Vector3.UnitZ, Rotation)
            };
            var offsetLineCenter = new Vector3(
                Vector3.Dot(diff, orthonormalBasis[0]),
                Vector3.Dot(diff, orthonormalBasis[1]),
                Vector3.Dot(diff, orthonormalBasis[2]));
            var lineDirection = Vector3.Normalize(line.Path);
            var offsetLineDirection = new Vector3(
                Vector3.Dot(lineDirection, orthonormalBasis[0]),
                Vector3.Dot(lineDirection, orthonormalBasis[1]),
                Vector3.Dot(lineDirection, orthonormalBasis[2]));

            if (HugeNumber.Abs(offsetLineCenter.X) > AxisX + (line.ContainingRadius * HugeNumber.Abs(offsetLineDirection.X)))
            {
                return false;
            }
            if (HugeNumber.Abs(offsetLineCenter.Y) > AxisY + (line.ContainingRadius * HugeNumber.Abs(offsetLineDirection.Y)))
            {
                return false;
            }
            if (HugeNumber.Abs(offsetLineCenter.Z) > AxisZ + (line.ContainingRadius * HugeNumber.Abs(offsetLineDirection.Z)))
            {
                return false;
            }

            var WxD = Vector3.Cross(offsetLineDirection, offsetLineCenter);
            var absWdU = new HugeNumber[]
            {
                HugeNumber.Abs(offsetLineDirection.X),
                HugeNumber.Abs(offsetLineDirection.Y),
                HugeNumber.Abs(offsetLineDirection.Z)
            };
            return (HugeNumber.Abs(WxD.X) <= (AxisY * absWdU[2]) + (AxisZ * absWdU[1]))
                && (HugeNumber.Abs(WxD.Y) <= (AxisX * absWdU[2]) + (AxisZ * absWdU[0]))
                && (HugeNumber.Abs(WxD.Z) <= (AxisX * absWdU[1]) + (AxisY * absWdU[0]));
        }

        private bool Intersects(Sphere sphere)
        {
            // First-pass check against overlapping containing radii.
            if (Vector3.Distance(Position, sphere.Position) > sphere.Radius + ContainingRadius)
            {
                return false;
            }

            var corner = Vector3.Transform(Corners[0], Rotation);
            var max = Position + corner;
            var min = Position + Vector3.Negate(corner);

            // Convert sphere to local coordinates
            var spherePosition = Position - sphere.Position;

            // Arvo's algorithm
            HugeNumber s;
            var d = HugeNumber.Zero;
            if (spherePosition.X < min.X)
            {
                s = spherePosition.X - min.X;
                d += s * s;
            }
            else if (spherePosition.X > max.X)
            {
                s = spherePosition.X - max.X;
                d += s * s;
            }
            if (spherePosition.Y < min.Y)
            {
                s = spherePosition.Y - min.Y;
                d += s * s;
            }
            else if (spherePosition.Y > max.Y)
            {
                s = spherePosition.Y - max.Y;
                d += s * s;
            }
            if (spherePosition.Z < min.Z)
            {
                s = spherePosition.Z - min.Z;
                d += s * s;
            }
            else if (spherePosition.Z > max.Z)
            {
                s = spherePosition.Z - max.Z;
                d += s * s;
            }
            return d <= sphere.Radius * sphere.Radius;
        }

        /// <summary>
        /// Returns a boolean indicating whether the two given shapes are equal.
        /// </summary>
        /// <param name="left">The first shape to compare.</param>
        /// <param name="right">The second shape to compare.</param>
        /// <returns>True if the shapes are equal; False otherwise.</returns>
        public static bool operator ==(Cuboid left, IShape right)
            => left.Equals(right);

        /// <summary>
        /// Returns a boolean indicating whether the two given shapes are not equal.
        /// </summary>
        /// <param name="left">The first shape to compare.</param>
        /// <param name="right">The second shape to compare.</param>
        /// <returns>True if the shapes are not equal; False if they are equal.</returns>
        public static bool operator !=(Cuboid left, IShape right)
            => !(left == right);
    }
}
