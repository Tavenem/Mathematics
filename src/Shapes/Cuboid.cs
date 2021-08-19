using System.Text.Json.Serialization;

namespace Tavenem.Mathematics;

/// <summary>
/// Provides information about the properties of an cuboid.
/// </summary>
public readonly struct Cuboid<TScalar> : IShape<Cuboid<TScalar>, TScalar>
    where TScalar : IFloatingPoint<TScalar>
{
    /// <summary>
    /// The length of the cuboid in the X dimension.
    /// </summary>
    public TScalar AxisX { get; }

    /// <summary>
    /// The length of the cuboid in the Y dimension.
    /// </summary>
    public TScalar AxisY { get; }

    /// <summary>
    /// The length of the cuboid in the Z dimension.
    /// </summary>
    public TScalar AxisZ { get; }

    /// <summary>
    /// A circular radius which fully contains the shape.
    /// </summary>
    [JsonIgnore]
    public TScalar ContainingRadius { get; }

    /// <summary>
    /// <para>
    /// The positions of the eight corners which form this cuboid.
    /// </para>
    /// <para>
    /// The corners are given in an order such that each pair of consecutive corners (as well as
    /// the first and last) form an edge.
    /// </para>
    /// </summary>
    [JsonIgnore]
    public Vector3<TScalar>[] Corners { get; }

    /// <summary>
    /// The point on this shape highest on the Y axis.
    /// </summary>
    [JsonIgnore]
    public Vector3<TScalar> HighestPoint { get; }

    /// <summary>
    /// The point on this shape lowest on the Y axis.
    /// </summary>
    [JsonIgnore]
    public Vector3<TScalar> LowestPoint { get; }

    /// <summary>
    /// The position of the shape in 3D space.
    /// </summary>
    public Vector3<TScalar> Position { get; }

    /// <summary>
    /// The rotation of this shape in 3D space.
    /// </summary>
    public Quaternion<TScalar> Rotation { get; }

    /// <summary>
    /// Identifies the type of this <see cref="IShape{TScalar}"/>.
    /// </summary>
    public ShapeType ShapeType => ShapeType.Cuboid;

    /// <summary>
    /// The length of the shortest of the three dimensions of this shape.
    /// </summary>
    /// <remarks>
    /// Note: this is not the length of smallest possible cross-section, but of the total length
    /// of the shape along any of the three primary axes of 3D space, prior to any rotation.
    /// </remarks>
    [JsonIgnore]
    public TScalar SmallestDimension { get; }

    /// <summary>
    /// The total volume of the shape.
    /// </summary>
    [JsonIgnore]
    public TScalar Volume { get; }

    /// <summary>
    /// Initializes a new instance of cuboid with the given parameters.
    /// </summary>
    /// <param name="axisX">The length of the cuboid in the X dimension.</param>
    /// <param name="axisY">The length of the cuboid in the Y dimension.</param>
    /// <param name="axisZ">The length of the cuboid in the Z dimension.</param>
    /// <param name="position">The position of the shape in 3D space.</param>
    /// <param name="rotation">The rotation of this shape in 3D space.</param>
    [JsonConstructor]
    public Cuboid(TScalar axisX, TScalar axisY, TScalar axisZ, Vector3<TScalar> position, Quaternion<TScalar> rotation)
    {
        AxisX = axisX;
        AxisY = axisY;
        AxisZ = axisZ;
        Position = position;
        Rotation = rotation;

        var two = NumberValues.Two<TScalar>();
        ContainingRadius = TScalar.Sqrt((AxisX * AxisX) + (AxisY * AxisY) + (AxisZ * AxisZ)) / two;
        SmallestDimension = TScalar.Min(AxisX, TScalar.Min(AxisY, AxisZ));
        Volume = AxisX * AxisY * AxisZ;

        var x = Vector3<TScalar>.Transform(Vector3<TScalar>.UnitX * (AxisX / two), Rotation);
        var y = Vector3<TScalar>.Transform(Vector3<TScalar>.UnitY * (AxisY / two), Rotation);
        var z = Vector3<TScalar>.Transform(Vector3<TScalar>.UnitZ * (AxisZ / two), Rotation);

        Corners = new Vector3<TScalar>[8]
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

    /// <summary>
    /// Initializes a new instance of cuboid with the given parameters.
    /// </summary>
    /// <param name="axisX">The length of the cuboid in the X dimension.</param>
    /// <param name="axisY">The length of the cuboid in the Y dimension.</param>
    /// <param name="axisZ">The length of the cuboid in the Z dimension.</param>
    /// <param name="position">The position of the shape in 3D space.</param>
    public Cuboid(TScalar axisX, TScalar axisY, TScalar axisZ, Vector3<TScalar> position)
        : this(axisX, axisY, axisZ, position, Quaternion<TScalar>.MultiplicativeIdentity) { }

    /// <summary>
    /// Initializes a new instance of cuboid with the given parameters.
    /// </summary>
    /// <param name="axisX">The length of the cuboid in the X dimension.</param>
    /// <param name="axisY">The length of the cuboid in the Y dimension.</param>
    /// <param name="axisZ">The length of the cuboid in the Z dimension.</param>
    public Cuboid(TScalar axisX, TScalar axisY, TScalar axisZ)
        : this(axisX, axisY, axisZ, Vector3<TScalar>.Zero, Quaternion<TScalar>.MultiplicativeIdentity) { }

    /// <summary>
    /// Initializes a new instance of cuboid.
    /// </summary>
    public Cuboid() : this(TScalar.Zero, TScalar.Zero, TScalar.Zero) { }

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="Position"/> set to the given
    /// value.
    /// </summary>
    /// <param name="position">The new <see cref="Position"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="Position"/> set to the given value.
    /// </returns>
    public IShape<TScalar> GetCloneAtPosition(Vector3<TScalar> position) => GetTypedCloneAtPosition(position);

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="Rotation"/> set to the given
    /// value.
    /// </summary>
    /// <param name="rotation">The new <see cref="Rotation"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="Rotation"/> set to the given value.
    /// </returns>
    public IShape<TScalar> GetCloneWithRotation(Quaternion<TScalar> rotation) => GetTypedCloneWithRotation(rotation);

    /// <summary>
    /// Gets a copy of this instance whose dimensions have been multiplied by the
    /// given factor.
    /// </summary>
    /// <param name="factor">The amount by which to scale this instance's size.</param>
    /// <returns>
    /// A copy of this instance whose dimensions have been multiplied by the given factor.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="factor"/> must be &gt;= 0.
    /// </exception>
    public IShape<TScalar> GetScaledByDimension(TScalar factor) => GetTypedScaledByDimension(factor);

    /// <summary>
    /// Gets a copy of this instance whose dimensions have beens scaled such that
    /// its volume will be multiplied by the given factor.
    /// </summary>
    /// <param name="factor">The amount by which to scale this instance's volume.</param>
    /// <returns>
    /// A copy of this instance whose dimensions have been scaled such that
    /// its volume will be multiplied by the given factor.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="factor"/> must be &gt;= 0.
    /// </exception>
    public IShape<TScalar> GetScaledByVolume(TScalar factor) => GetTypedScaledByVolume(factor);

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="IShape{TScalar}.Position"/> set to the given
    /// value.
    /// </summary>
    /// <param name="position">The new <see cref="IShape{TScalar}.Position"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="IShape{TScalar}.Position"/> set to the given value.
    /// </returns>
    public Cuboid<TScalar> GetTypedCloneAtPosition(Vector3<TScalar> position)
        => new(AxisX, AxisY, AxisZ, position, Rotation);

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="IShape{TScalar}.Rotation"/> set to the given
    /// value.
    /// </summary>
    /// <param name="rotation">The new <see cref="IShape{TScalar}.Rotation"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="IShape{TScalar}.Rotation"/> set to the given value.
    /// </returns>
    public Cuboid<TScalar> GetTypedCloneWithRotation(Quaternion<TScalar> rotation)
        => new(AxisX, AxisY, AxisZ, Position, rotation);

    /// <summary>
    /// Gets a copy of this instance whose dimensions have been multiplied by the
    /// given factor.
    /// </summary>
    /// <param name="factor">The amount by which to scale this instance's size.</param>
    /// <returns>
    /// A copy of this instance whose dimensions have been multiplied by the given factor.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="factor"/> must be &gt;= 0.
    /// </exception>
    public Cuboid<TScalar> GetTypedScaledByDimension(TScalar factor)
    {
        if (factor < TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
        }

        return new Cuboid<TScalar>(
            AxisX * factor,
            AxisY * factor,
            AxisZ * factor,
            Position,
            Rotation);
    }

    /// <summary>
    /// Gets a copy of this instance whose dimensions have beens scaled such that
    /// its volume will be multiplied by the given factor.
    /// </summary>
    /// <param name="factor">The amount by which to scale this instance's volume.</param>
    /// <returns>
    /// A copy of this instance whose dimensions have been scaled such that
    /// its volume will be multiplied by the given factor.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="factor"/> must be &gt;= 0.
    /// </exception>
    public Cuboid<TScalar> GetTypedScaledByVolume(TScalar factor)
    {
        if (factor < TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
        }

        if (factor == TScalar.Zero)
        {
            return new Cuboid<TScalar>(TScalar.Zero, TScalar.Zero, TScalar.Zero, Position);
        }
        else
        {
            var multiplier = TScalar.Cbrt(factor);
            return new Cuboid<TScalar>(
                AxisX * multiplier,
                AxisY * multiplier,
                AxisZ * multiplier,
                Position,
                Rotation);
        }
    }

    /// <summary>
    /// Determines whether the specified cuboid is equivalent to the current object.
    /// </summary>
    /// <param name="other">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true"/> if the specified cuboid is equivalent to the
    /// current object; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(Cuboid<TScalar> other) => other.AxisX == AxisX
        && other.AxisY == AxisY
        && other.AxisZ == AxisZ
        && other.Position == Position
        && other.Rotation == Rotation;

    /// <summary>
    /// Determines whether the specified <see cref="IShape{TScalar}"/> is equivalent to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true"/> if the specified <see cref="IShape{TScalar}"/> is equivalent to the
    /// current object; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(IShape<TScalar>? obj) => obj is Cuboid<TScalar> other && Equals(other);

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    /// <see langword="true"/> if obj and this instance are the same type and represent
    /// the same value; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(object? obj) => obj is Cuboid<TScalar> shape && Equals(shape);

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    public override int GetHashCode() => HashCode.Combine(AxisX, AxisY, AxisZ, Position, Rotation);

    /// <summary>
    /// Determines if this instance intersects with the given <see cref="IShape{TScalar}"/>.
    /// </summary>
    /// <param name="shape">
    /// The <see cref="IShape{TScalar}"/> to check for intersection with this instance.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if this instance intersects with the given <see
    /// cref="IShape{TScalar}"/>; otherwise <see langword="false"/>.
    /// </returns>
    public bool Intersects(IShape<TScalar> shape)
    {
        if (shape is SinglePoint<TScalar> point)
        {
            return IsPointWithin(point.Position);
        }
        // First-pass exclusion based on containing radii.
        if (Vector3<TScalar>.Distance(Position, shape.Position) > shape.ContainingRadius + ContainingRadius)
        {
            return false;
        }
        if (shape is Cone<TScalar> cone)
        {
            return Intersects(cone);
        }
        if (shape is Cuboid<TScalar> cuboid)
        {
            return Intersects(cuboid);
        }
        if (shape is Line<TScalar> line)
        {
            return Intersects(line);
        }
        if (shape is Sphere<TScalar> sphere)
        {
            return Intersects(sphere);
        }
        return shape.Intersects(this);
    }

    /// <summary>
    /// Determines if a given point lies within this shape.
    /// </summary>
    /// <param name="point">A point in the same 3D space as this shape.</param>
    /// <returns>
    /// <see langword="true"/> if the point is within (or tangent to) this shape; otherwise <see langword="false"/>.
    /// </returns>
    public bool IsPointWithin(Vector3<TScalar> point)
    {
        var diff = point - Position;
        var orthonormalBasis = new Vector3<TScalar>[]
        {
            Vector3<TScalar>.Transform(Vector3<TScalar>.UnitX, Rotation),
            Vector3<TScalar>.Transform(Vector3<TScalar>.UnitY, Rotation),
            Vector3<TScalar>.Transform(Vector3<TScalar>.UnitZ, Rotation)
        };
        var closest = new TScalar[3];
        for (var i = 0; i < 3; i++)
        {
            closest[i] = Vector3<TScalar>.Dot(diff, orthonormalBasis[i]);
        }

        var sqrDist = TScalar.Zero;
        var two = NumberValues.Two<TScalar>();
        for (var i = 0; i < 3; i++)
        {
            TScalar a;
            if (i == 0)
            {
                a = AxisX / two;
            }
            else if (i == 1)
            {
                a = AxisY / two;
            }
            else
            {
                a = AxisZ / two;
            }

            TScalar p;
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
        return sqrDist <= TScalar.Zero;
    }

    private bool Intersects(Cone<TScalar> cone)
    {
        var orthonormalBasis = new Vector3<TScalar>[]
        {
            Vector3<TScalar>.Transform(Vector3<TScalar>.UnitX, Rotation),
            Vector3<TScalar>.Transform(Vector3<TScalar>.UnitY, Rotation),
            Vector3<TScalar>.Transform(Vector3<TScalar>.UnitZ, Rotation)
        };
        var CmV = Position - cone.Position;
        var coneDir = Vector3<TScalar>.Normalize(cone.Axis);
        var DdU = new Vector3<TScalar>(
            Vector3<TScalar>.Dot(coneDir, orthonormalBasis[0]),
            Vector3<TScalar>.Dot(coneDir, orthonormalBasis[1]),
            Vector3<TScalar>.Dot(coneDir, orthonormalBasis[2]));
        var DdCmV = Vector3<TScalar>.Dot(coneDir, CmV);
        var two = NumberValues.Two<TScalar>();
        var eX = AxisX / two;
        var eY = AxisY / two;
        var eZ = AxisZ / two;
        var radius = (eX * TScalar.Abs(DdU.X))
            + (eY * TScalar.Abs(DdU.Y))
            + (eZ * TScalar.Abs(DdU.Z));
        if (DdCmV + radius <= TScalar.Zero)
        {
            return false;
        }
        if (DdCmV - radius >= cone.Length)
        {
            return false;
        }
        var UdCmV = new Vector3<TScalar>(
            Vector3<TScalar>.Dot(orthonormalBasis[0], CmV),
            Vector3<TScalar>.Dot(orthonormalBasis[1], CmV),
            Vector3<TScalar>.Dot(orthonormalBasis[1], CmV));
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

        var cosAngle = TScalar.Cos(TScalar.Atan(cone.Radius / cone.Length));
        var cosSqr = cosAngle * cosAngle;
        Vector3<TScalar>[] X = new Vector3<TScalar>[8], PmV = new Vector3<TScalar>[8];
        TScalar[] DdPmV = new TScalar[8], sqrDdPmV = new TScalar[8], sqrLenPmV = new TScalar[8];
        int iMax = -1, jMax = -1;
        for (var i = 0; i < polygon.Length; ++i)
        {
            var j = polygon[i];
            X[j] = new Vector3<TScalar>(
                (j & 1) != 0 ? eX : -eX,
                (j & 2) != 0 ? eY : -eY,
                (j & 4) != 0 ? eZ : -eZ);
            DdPmV[j] = Vector3<TScalar>.Dot(DdU, X[j]) + DdCmV;
            if (DdPmV[j] > TScalar.Zero)
            {
                PmV[j] = X[j] + CmV;
                sqrDdPmV[j] = DdPmV[j] * DdPmV[j];
                sqrLenPmV[j] = Vector3<TScalar>.Dot(PmV[j], PmV[j]);
                if (sqrDdPmV[j] - (cosSqr * sqrLenPmV[j]) > TScalar.Zero)
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
        var DdUA = new TScalar[3]
        {
            DdU.X,
            DdU.Y,
            DdU.Z,
        };
        var maxPmVA = new TScalar[3]
        {
            PmV[jMax].X,
            PmV[jMax].Y,
            PmV[jMax].Z,
        };
        var fder = TScalar.Create(s) * ((DdUA[k0] * maxSqrLenPmV) - (maxDdPmV * maxPmVA[k0]));
        var mMod3 = new int[] { 0, 1, 2, 0 };

        if (fder <= TScalar.Zero)
        {
            jDiff = jMax - polygon[iMax > 0 ? iMax - 1 : polygon.Length - 1];
            s = jDiff > 0 ? 1 : -1;
            k0 = Math.Abs(jDiff) >> 1;
            fder = TScalar.Create(-s) * ((DdUA[k0] * maxSqrLenPmV) - (maxDdPmV * maxPmVA[k0]));
        }
        if (fder > TScalar.Zero)
        {
            var k1 = mMod3[k0 + 1];
            var k2 = mMod3[k1 + 1];
            var denom = (DdUA[k1] * maxPmVA[k1]) + (DdUA[k2] * maxPmVA[k2]);
            var MmVA = new TScalar[3] { TScalar.Zero, TScalar.Zero, TScalar.Zero };
            var maxXA = new TScalar[3]
            {
                X[jMax].X,
                X[jMax].Y,
                X[jMax].Z,
            };
            var CmVA = new TScalar[3]
            {
                CmV.X,
                CmV.Y,
                CmV.Z,
            };
            MmVA[k0] = ((maxPmVA[k1] * maxPmVA[k1]) + (maxPmVA[k2] * maxPmVA[k2])) * DdUA[k0];
            MmVA[k1] = denom * (maxXA[k1] + CmVA[k1]);
            MmVA[k2] = denom * (maxXA[k2] + CmVA[k2]);
            var MmV = new Vector3<TScalar>(MmVA[0], MmVA[1], MmVA[2]);

            var DdMmV = Vector3<TScalar>.Dot(DdU, MmV);
            if ((DdMmV * DdMmV) - (cosSqr * Vector3<TScalar>.Dot(MmV, MmV)) > TScalar.Zero)
            {
                return true;
            }
            return TScalar.Create(s) * ((DdUA[k1] * maxPmVA[k2]) - (DdUA[k2] * maxPmVA[k1])) <= TScalar.Zero;
        }
        return false;
    }

    private bool Intersects(Cuboid<TScalar> cuboid)
    {
        // First-pass check against overlapping containing radii.
        if (Vector3<TScalar>.Distance(Position, cuboid.Position) > cuboid.ContainingRadius + ContainingRadius)
        {
            return false;
        }

        // Separating axis test

        var orthonormalBasis = new Vector3<TScalar>[]
        {
            Vector3<TScalar>.Transform(Vector3<TScalar>.UnitX, Rotation),
            Vector3<TScalar>.Transform(Vector3<TScalar>.UnitY, Rotation),
            Vector3<TScalar>.Transform(Vector3<TScalar>.UnitZ, Rotation)
        };
        var otherOrthonormalBasis = new Vector3<TScalar>[]
        {
            Vector3<TScalar>.Transform(Vector3<TScalar>.UnitX, cuboid.Rotation),
            Vector3<TScalar>.Transform(Vector3<TScalar>.UnitY, cuboid.Rotation),
            Vector3<TScalar>.Transform(Vector3<TScalar>.UnitZ, cuboid.Rotation)
        };

        var v = cuboid.Position - Position; // translation in parent space

        // translation in this box's space
        var T = new Vector3<TScalar>(
            Vector3<TScalar>.Dot(v, orthonormalBasis[0]),
            Vector3<TScalar>.Dot(v, orthonormalBasis[1]),
            Vector3<TScalar>.Dot(v, orthonormalBasis[2]));

        // other's basis with respect to this box's local space
        var R = new TScalar[3, 3];
        for (var i = 0; i < 3; i++)
        {
            for (var k = 0; k < 3; k++)
            {
                R[i, k] = Vector3<TScalar>.Dot(orthonormalBasis[i], otherOrthonormalBasis[k]);
            }
        }

        TScalar ra, rb, t;

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
            rb = (cuboid.AxisX * TScalar.Abs(R[i, 0]))
                + (cuboid.AxisY * TScalar.Abs(R[i, 1]))
                + (cuboid.AxisZ * TScalar.Abs(R[i, 2]));
            if (i == 0)
            {
                t = TScalar.Abs(T.X);
            }
            else if (i == 1)
            {
                t = TScalar.Abs(T.Y);
            }
            else
            {
                t = TScalar.Abs(T.Z);
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
            ra = (AxisX * TScalar.Abs(R[0, k]))
                + (AxisY * TScalar.Abs(R[1, k]))
                + (AxisZ * TScalar.Abs(R[2, k]));
            t = TScalar.Abs((T.X * R[0, k]) + (T.Y * R[1, k]) + (T.Z * R[2, k]));
            if (t > ra + rb)
            {
                return false;
            }
        }

        // 9 cross products

        // L = A0 x B0
        ra = (AxisY * TScalar.Abs(R[2, 0])) + (AxisZ * TScalar.Abs(R[1, 0]));
        rb = (cuboid.AxisY * TScalar.Abs(R[0, 2])) + (AxisZ * TScalar.Abs(R[0, 1]));
        t = TScalar.Abs((T.Z * R[1, 0]) - (T.Y * R[2, 0]));
        if (t > ra + rb)
        {
            return false;
        }

        // L = A0 x B1
        ra = (AxisY * TScalar.Abs(R[2, 1])) + (AxisZ * TScalar.Abs(R[1, 1]));
        rb = (cuboid.AxisX * TScalar.Abs(R[0, 2])) + (AxisZ * TScalar.Abs(R[0, 0]));
        t = TScalar.Abs((T.Z * R[1, 1]) - (T.Y * R[2, 1]));
        if (t > ra + rb)
        {
            return false;
        }

        // L = A0 x B2
        ra = (AxisY * TScalar.Abs(R[2, 2])) + (AxisZ * TScalar.Abs(R[1, 2]));
        rb = (cuboid.AxisX * TScalar.Abs(R[0, 1])) + (AxisY * TScalar.Abs(R[0, 0]));
        t = TScalar.Abs((T.Z * R[1, 2]) - (T.Y * R[2, 2]));
        if (t > ra + rb)
        {
            return false;
        }

        // L = A1 x B0
        ra = (AxisX * TScalar.Abs(R[2, 0])) + (AxisZ * TScalar.Abs(R[0, 0]));
        rb = (cuboid.AxisY * TScalar.Abs(R[1, 2])) + (AxisZ * TScalar.Abs(R[1, 1]));
        t = TScalar.Abs((T.X * R[2, 0]) - (T.Z * R[0, 0]));
        if (t > ra + rb)
        {
            return false;
        }

        // L = A1 x B1
        ra = (AxisX * TScalar.Abs(R[2, 1])) + (AxisZ * TScalar.Abs(R[0, 1]));
        rb = (cuboid.AxisX * TScalar.Abs(R[1, 2])) + (AxisZ * TScalar.Abs(R[1, 0]));
        t = TScalar.Abs((T.X * R[2, 1]) - (T.Z * R[0, 1]));
        if (t > ra + rb)
        {
            return false;
        }

        // L = A1 x B2
        ra = (AxisX * TScalar.Abs(R[2, 2])) + (AxisZ * TScalar.Abs(R[0, 2]));
        rb = (cuboid.AxisX * TScalar.Abs(R[1, 1])) + (AxisY * TScalar.Abs(R[1, 0]));
        t = TScalar.Abs((T.X * R[2, 2]) - (T.Z * R[0, 2]));
        if (t > ra + rb)
        {
            return false;
        }

        // L = A2 x B0
        ra = (AxisX * TScalar.Abs(R[1, 0])) + (AxisY * TScalar.Abs(R[0, 0]));
        rb = (cuboid.AxisY * TScalar.Abs(R[2, 2])) + (AxisZ * TScalar.Abs(R[2, 1]));
        t = TScalar.Abs((T.Y * R[0, 0]) - (T.X * R[1, 0]));
        if (t > ra + rb)
        {
            return false;
        }

        // L = A2 x B1
        ra = (AxisX * TScalar.Abs(R[1, 1])) + (AxisY * TScalar.Abs(R[0, 1]));
        rb = (cuboid.AxisX * TScalar.Abs(R[2, 2])) + (AxisZ * TScalar.Abs(R[2, 0]));
        t = TScalar.Abs((T.Y * R[0, 1]) - (T.X * R[1, 1]));
        if (t > ra + rb)
        {
            return false;
        }

        // L = A2 x B2
        ra = (AxisX * TScalar.Abs(R[1, 2])) + (AxisY * TScalar.Abs(R[0, 2]));
        rb = (cuboid.AxisX * TScalar.Abs(R[2, 1])) + (AxisY * TScalar.Abs(R[2, 0]));
        t = TScalar.Abs((T.Y * R[0, 2]) - (T.X * R[1, 2]));

        return t <= ra + rb;
    }

    private bool Intersects(Line<TScalar> line)
    {
        var diff = line.Position - Position;
        var orthonormalBasis = new Vector3<TScalar>[]
        {
            Vector3<TScalar>.Transform(Vector3<TScalar>.UnitX, Rotation),
            Vector3<TScalar>.Transform(Vector3<TScalar>.UnitY, Rotation),
            Vector3<TScalar>.Transform(Vector3<TScalar>.UnitZ, Rotation)
        };
        var offsetLineCenter = new Vector3<TScalar>(
            Vector3<TScalar>.Dot(diff, orthonormalBasis[0]),
            Vector3<TScalar>.Dot(diff, orthonormalBasis[1]),
            Vector3<TScalar>.Dot(diff, orthonormalBasis[2]));
        var lineDirection = Vector3<TScalar>.Normalize(line.Path);
        var offsetLineDirection = new Vector3<TScalar>(
            Vector3<TScalar>.Dot(lineDirection, orthonormalBasis[0]),
            Vector3<TScalar>.Dot(lineDirection, orthonormalBasis[1]),
            Vector3<TScalar>.Dot(lineDirection, orthonormalBasis[2]));

        if (TScalar.Abs(offsetLineCenter.X) > AxisX + (line.ContainingRadius * TScalar.Abs(offsetLineDirection.X)))
        {
            return false;
        }
        if (TScalar.Abs(offsetLineCenter.Y) > AxisY + (line.ContainingRadius * TScalar.Abs(offsetLineDirection.Y)))
        {
            return false;
        }
        if (TScalar.Abs(offsetLineCenter.Z) > AxisZ + (line.ContainingRadius * TScalar.Abs(offsetLineDirection.Z)))
        {
            return false;
        }

        var WxD = Vector3<TScalar>.Cross(offsetLineDirection, offsetLineCenter);
        var absWdU = new TScalar[]
        {
            TScalar.Abs(offsetLineDirection.X),
            TScalar.Abs(offsetLineDirection.Y),
            TScalar.Abs(offsetLineDirection.Z)
        };
        return (TScalar.Abs(WxD.X) <= (AxisY * absWdU[2]) + (AxisZ * absWdU[1]))
            && (TScalar.Abs(WxD.Y) <= (AxisX * absWdU[2]) + (AxisZ * absWdU[0]))
            && (TScalar.Abs(WxD.Z) <= (AxisX * absWdU[1]) + (AxisY * absWdU[0]));
    }

    private bool Intersects(Sphere<TScalar> sphere)
    {
        // First-pass check against overlapping containing radii.
        if (Vector3<TScalar>.Distance(Position, sphere.Position) > sphere.Radius + ContainingRadius)
        {
            return false;
        }

        var corner = Vector3<TScalar>.Transform(Corners[0], Rotation);
        var max = Position + corner;
        var min = Position + (-corner);

        // Convert sphere to local coordinates
        var spherePosition = Position - sphere.Position;

        // Arvo's algorithm
        TScalar s;
        var d = TScalar.Zero;
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
    /// <param name="left">The first shapes to compare.</param>
    /// <param name="right">The second shapes to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the shapes are equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool operator ==(Cuboid<TScalar> left, IShape<TScalar> right)
        => left.Equals(right);

    /// <summary>
    /// Returns a boolean indicating whether the two given shapes are not equal.
    /// </summary>
    /// <param name="left">The first shapes to compare.</param>
    /// <param name="right">The second shapes to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the shapes are not equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool operator !=(Cuboid<TScalar> left, IShape<TScalar> right)
        => !(left == right);
}
