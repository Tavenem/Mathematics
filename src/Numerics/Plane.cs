using System.Diagnostics;

namespace Tavenem.Mathematics;

/// <summary>
/// A structure encapsulating a 3D Plane
/// </summary>
[DebuggerDisplay("{ToString()}")]
public readonly struct Plane<TScalar> :
    IEqualityOperators<Plane<TScalar>, Plane<TScalar>>,
    ISpanFormattable
    where TScalar : IFloatingPoint<TScalar>
{
    /// <summary>
    /// The normal vector of the plane.
    /// </summary>
    public Vector3<TScalar> Normal { get; init; }

    /// <summary>
    /// The distance of the plane along its normal from the origin.
    /// </summary>
    public TScalar D { get; init; }

    /// <summary>
    /// Creates a new plane with the given values.
    /// </summary>
    /// <param name="x">The X component of the normal vector of the plane.</param>
    /// <param name="y">The Y component of the normal vector of the plane.</param>
    /// <param name="z">The Z component of the normal vector of the plane.</param>
    /// <param name="d">
    /// The distance of the plane along its normal from the origin.
    /// </param>
    public Plane(TScalar x, TScalar y, TScalar z, TScalar d)
    {
        Normal = new() { X = x, Y = y, Z = z };
        D = d;
    }

    /// <summary>
    /// Creates a new plane with the given normal and distance parts.
    /// </summary>
    /// <param name="normal">
    /// The normal vector of the plane.
    /// </param>
    /// <param name="d">
    /// The distance of the plane along its normal from the origin.
    /// </param>
    public Plane(Vector3<TScalar> normal, TScalar d)
    {
        Normal = normal;
        D = d;
    }

    /// <summary>
    /// Creates a new plane from the given vector.
    /// </summary>
    /// <param name="value">
    /// A vector whose first 3 elements describe the normal vector of the plane,
    /// and whose W component defines the distance along that normal from the origin.
    /// </param>
    public Plane(Vector4<TScalar> value)
    {
        Normal = new() { X = value.X, Y = value.Y, Z = value.Z };
        D = value.W;
    }

    /// <summary>
    /// Creates a new plane with the given value for all components.
    /// </summary>
    /// <param name="value">The value to use for all components.</param>
    public static Plane<TScalar> Create(TScalar value) => new()
    {
        Normal = new() { X = value, Y = value, Z = value },
        D = value,
    };

    /// <summary>
    /// Creates a new plane with the given values.
    /// </summary>
    /// <param name="x">The X component of the normal vector of the plane.</param>
    /// <param name="y">The Y component of the normal vector of the plane.</param>
    /// <param name="z">The Z component of the normal vector of the plane.</param>
    /// <param name="d">
    /// The distance of the plane along its normal from the origin.
    /// </param>
    public static Plane<TScalar> Create(TScalar x, TScalar y, TScalar z, TScalar d) => new()
    {
        Normal = new() { X = x, Y = y, Z = z },
        D = d,
    };

    /// <summary>
    /// Creates a new plane with the given normal and distance parts.
    /// </summary>
    /// <param name="normal">
    /// The normal vector of the plane.
    /// </param>
    /// <param name="d">
    /// The distance of the plane along its normal from the origin.
    /// </param>
    public static Plane<TScalar> Create(Vector3<TScalar> normal, TScalar d) => new()
    {
        Normal = normal,
        D = d,
    };

    /// <summary>
    /// Creates a new plane from the given vector.
    /// </summary>
    /// <param name="value">
    /// A vector whose first 3 elements describe the normal vector of the plane,
    /// and whose W component defines the distance along that normal from the origin.
    /// </param>
    public static Plane<TScalar> Create(Vector4<TScalar> value) => new()
    {
        Normal = new() { X = value.X, Y = value.Y, Z = value.Z },
        D = value.W,
    };

    /// <summary>
    /// Creates a new plane with the given values.
    /// </summary>
    /// <param name="values">The values to use for all components.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="values"/> does not contain enough elements
    /// </exception>
    public static Plane<TScalar> Create(TScalar[] values)
    {
        if (values.Length < 4)
        {
            throw new ArgumentException($"{nameof(values)} does not contain enough elements", nameof(values));
        }
        return new()
        {
            Normal = new() { X = values[0], Y = values[1], Z = values[2] },
            D = values[3],
        };
    }

    /// <summary>
    /// Creates a new plane with the given values.
    /// </summary>
    /// <param name="values">The values to use for all components.</param>
    /// <param name="startIndex"></param>
    /// <exception cref="ArgumentException">
    /// <paramref name="values"/> does not contain enough elements
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// the value of <paramref name="startIndex"/> does not leave enough elements in <paramref name="values"/>
    /// </exception>
    public static Plane<TScalar> Create(TScalar[] values, int startIndex)
    {
        if (values.Length < 4)
        {
            throw new ArgumentException($"{nameof(values)} does not contain enough elements");
        }
        if (startIndex + values.Length < 4)
        {
            throw new ArgumentOutOfRangeException(
                nameof(startIndex),
                $"the value of {nameof(startIndex)} does not leave enough elements in {nameof(values)}");
        }
        return new()
        {
            Normal = new() { X = values[startIndex], Y = values[startIndex + 1], Z = values[startIndex + 2] },
            D = values[startIndex + 3],
        };
    }

    /// <summary>
    /// Creates a new plane with the given values.
    /// </summary>
    /// <param name="values">The values to use for all components.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="values"/> does not contain enough elements
    /// </exception>
    public static Plane<TScalar> Create(ReadOnlySpan<TScalar> values)
    {
        if (values.Length < 4)
        {
            throw new ArgumentException($"{nameof(values)} does not contain enough elements", nameof(values));
        }
        return new()
        {
            Normal = new() { X = values[0], Y = values[1], Z = values[2] },
            D = values[3],
        };
    }

    /// <summary>
    /// Creates a plane that contains the three given points.
    /// </summary>
    /// <param name="point1">The first point defining the Plane.</param>
    /// <param name="point2">The second point defining the Plane.</param>
    /// <param name="point3">The third point defining the Plane.</param>
    /// <returns>The plane containing the three points.</returns>
    public static Plane<TScalar> CreateFromVertices(Vector3<TScalar> point1, Vector3<TScalar> point2, Vector3<TScalar> point3)
    {
        var ax = point2.X - point1.X;
        var ay = point2.Y - point1.Y;
        var az = point2.Z - point1.Z;

        var bx = point3.X - point1.X;
        var by = point3.Y - point1.Y;
        var bz = point3.Z - point1.Z;

        // N=Cross(a,b)
        var nx = (ay * bz) - (az * by);
        var ny = (az * bx) - (ax * bz);
        var nz = (ax * by) - (ay * bx);

        // Normalize(N)
        var ls = (nx * nx) + (ny * ny) + (nz * nz);
        var invNorm = TScalar.One / TScalar.Sqrt(ls);

        var normal = new Vector3<TScalar>()
        {
            X = nx * invNorm,
            Y = ny * invNorm,
            Z = nz * invNorm,
        };

        return new()
        {
            Normal = normal,
            D = -((normal.X * point1.X) + (normal.Y * point1.Y) + (normal.Z * point1.Z)),
        };
    }

    /// <summary>
    /// Calculates the dot product of a plane and <see cref="Vector4{TScalar}"/>.
    /// </summary>
    /// <param name="plane">The plane.</param>
    /// <param name="value">The vector.</param>
    /// <returns>The dot product.</returns>
    public static TScalar Dot(Plane<TScalar> plane, Vector4<TScalar> value)
        => (plane.Normal.X * value.X)
        + (plane.Normal.Y * value.Y)
        + (plane.Normal.Z * value.Z)
        + (plane.D * value.W);

    /// <summary>
    /// Returns the dot product of a specified <see cref="Vector3{TScalar}"/> and the
    /// normal vector of this plane plus the distance value of the plane.
    /// </summary>
    /// <param name="plane">The plane.</param>
    /// <param name="value">The vector.</param>
    /// <returns>The resulting value.</returns>
    public static TScalar DotCoordinate(Plane<TScalar> plane, Vector3<TScalar> value)
        => (plane.Normal.X * value.X)
        + (plane.Normal.Y * value.Y)
        + (plane.Normal.Z * value.Z)
        + plane.D;

    /// <summary>
    /// Returns the dot product of a specified <see cref="Vector3{TScalar}"/>
    /// and the normal vector of this plane.
    /// </summary>
    /// <param name="plane">The plane.</param>
    /// <param name="value">The vector.</param>
    /// <returns>The resulting dot product.</returns>
    public static TScalar DotNormal(Plane<TScalar> plane, Vector3<TScalar> value)
        => (plane.Normal.X * value.X)
        + (plane.Normal.Y * value.Y)
        + (plane.Normal.Z * value.Z);

    /// <summary>
    /// Creates a new plane whose normal vector is the source plane's normal vector normalized.
    /// </summary>
    /// <param name="value">The source plane.</param>
    /// <returns>The normalized plane.</returns>
    public static Plane<TScalar> Normalize(Plane<TScalar> value)
    {
        var f = (value.Normal.X * value.Normal.X) + (value.Normal.Y * value.Normal.Y) + (value.Normal.Z * value.Normal.Z);

        if (TScalar.Abs(f - TScalar.One).IsNearlyZero())
        {
            return value; // It already normalized, so we don't need to further process.
        }

        var fInv = TScalar.One / TScalar.Sqrt(f);

        return new()
        {
            Normal = new()
            {
                X = value.Normal.X * fInv,
                Y = value.Normal.Y * fInv,
                Z = value.Normal.Z * fInv,
            },
            D = value.D * fInv,
        };
    }

    /// <summary>
    /// Transforms a normalized plane by a matrix.
    /// </summary>
    /// <param name="plane">
    /// <para>
    /// The normalized plane to transform.
    /// </para>
    /// <para>
    /// This plane must already be normalized, so that its Normal vector is of unit length, before this method is called.
    /// </para>
    /// </param>
    /// <param name="matrix">The transformation matrix to apply to the plane.</param>
    /// <returns>The transformed plane.</returns>
    public static Plane<TScalar> Transform(Plane<TScalar> plane, Matrix4x4<TScalar> matrix)
    {
        Matrix4x4<TScalar>.Invert(matrix, out var m);

        TScalar x = plane.Normal.X, y = plane.Normal.Y, z = plane.Normal.Z, w = plane.D;

        return new()
        {
            Normal = new()
            {
                X = (x * m.M11) + (y * m.M12) + (z * m.M13) + (w * m.M14),
                Y = (x * m.M21) + (y * m.M22) + (z * m.M23) + (w * m.M24),
                Z = (x * m.M31) + (y * m.M32) + (z * m.M33) + (w * m.M34),
            },
            D = (x * m.M41) + (y * m.M42) + (z * m.M43) + (w * m.M44),
        };
    }

    /// <summary>
    ///  Transforms a normalized plane by a quaternion rotation.
    /// </summary>
    /// <param name="plane">
    /// <para>
    /// The normalized plane to transform.
    /// </para>
    /// <para>
    /// This plane must already be normalized, so that its normal vector is of unit length, before this method is called.
    /// </para>
    /// </param>
    /// <param name="rotation">The wuaternion rotation to apply to the Plane.</param>
    /// <returns>A new plane that results from applying the rotation.</returns>
    public static Plane<TScalar> Transform(Plane<TScalar> plane, Quaternion<TScalar> rotation)
    {
        // Compute rotation matrix.
        var x2 = rotation.X + rotation.X;
        var y2 = rotation.Y + rotation.Y;
        var z2 = rotation.Z + rotation.Z;

        var wx2 = rotation.W * x2;
        var wy2 = rotation.W * y2;
        var wz2 = rotation.W * z2;
        var xx2 = rotation.X * x2;
        var xy2 = rotation.X * y2;
        var xz2 = rotation.X * z2;
        var yy2 = rotation.Y * y2;
        var yz2 = rotation.Y * z2;
        var zz2 = rotation.Z * z2;

        var m11 = TScalar.One - yy2 - zz2;
        var m21 = xy2 - wz2;
        var m31 = xz2 + wy2;

        var m12 = xy2 + wz2;
        var m22 = TScalar.One - xx2 - zz2;
        var m32 = yz2 - wx2;

        var m13 = xz2 - wy2;
        var m23 = yz2 + wx2;
        var m33 = TScalar.One - xx2 - yy2;

        TScalar x = plane.Normal.X, y = plane.Normal.Y, z = plane.Normal.Z;

        return new()
        {
            Normal = new()
            {
                X = (x * m11) + (y * m21) + (z * m31),
                Y = (x * m12) + (y * m22) + (z * m32),
                Z = (x * m13) + (y * m23) + (z * m33),
            },
            D = plane.D,
        };
    }

    /// <summary>
    /// Returns a boolean indicating whether the two given planes are equal.
    /// </summary>
    /// <param name="value1">The first plane to compare.</param>
    /// <param name="value2">The second plane to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the planes are equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool operator ==(Plane<TScalar> value1, Plane<TScalar> value2)
        => value1.Equals(value2);

    /// <summary>
    /// Returns a boolean indicating whether the two given Planes are not equal.
    /// </summary>
    /// <param name="value1">The first plane to compare.</param>
    /// <param name="value2">The second plane to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the planes are not equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool operator !=(Plane<TScalar> value1, Plane<TScalar> value2)
        => !value1.Equals(value2);

    /// <summary>
    /// Converts between implementations of planes.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator Plane<TScalar>(System.Numerics.Plane value) => new()
    {
        Normal = new()
        {
            X = TScalar.Create(value.Normal.X),
            Y = TScalar.Create(value.Normal.Y),
            Z = TScalar.Create(value.Normal.Z),
        },
        D = TScalar.Create(value.D),
    };

    /// <summary>
    /// Converts between implementations of planes.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static explicit operator System.Numerics.Plane(Plane<TScalar> value) => new(
        value.Normal.X.Create<TScalar, float>(),
        value.Normal.Y.Create<TScalar, float>(),
        value.Normal.Z.Create<TScalar, float>(),
        value.D.Create<TScalar, float>());

    /// <summary>
    /// Returns a boolean indicating whether the given plane is equal to this plane instance.
    /// </summary>
    /// <param name="other">The plane to compare with this instance.</param>
    /// <returns>
    /// <see langword="true"/> if the other plane is equal to this instance; otherwise <see langword="false"/>.
    /// </returns>
    public bool Equals(Plane<TScalar> other)
        => Normal.X == other.Normal.X
        && Normal.Y == other.Normal.Y
        && Normal.Z == other.Normal.Z
        && D == other.D;

    /// <summary>
    /// Returns a boolean indicating whether the given Object is equal to this plane instance.
    /// </summary>
    /// <param name="obj">The Object to compare against.</param>
    /// <returns>
    /// <see langword="true"/> if the Object is equal to this plane; otherwise <see langword="false"/>.
    /// </returns>
    public override bool Equals(object? obj)
        => obj is Plane<TScalar> other && Equals(other);

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(Normal, D);

    /// <summary>
    /// Returns a <see cref="string"/> representing this instance, using the specified
    /// <paramref name="format"/> to format individual elements and the given <see cref="IFormatProvider"/>.
    /// </summary>
    /// <param name="format">The format of individual elements.</param>
    /// <param name="formatProvider">
    /// The format provider to use when formatting elements.
    /// </param>
    /// <returns>The <see cref="string"/> representation.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider)
        => $"{{Normal:{Normal.ToString(format, formatProvider)} D:{D.ToString(format, formatProvider)}}}";

    /// <summary>
    /// Returns a <see cref="string"/> representing this instance, using the specified
    /// <paramref name="format"/> to format individual elements.
    /// </summary>
    /// <param name="format">The format of individual elements.</param>
    /// <returns>The <see cref="string"/> representation.</returns>
    public string ToString(string format) => ToString(format, null);

    /// <summary>
    /// Returns a <see cref="string"/> representing this instance.
    /// </summary>
    /// <returns>The <see cref="string"/> representation.</returns>
    public override string ToString() => ToString(null, null);

    /// <summary>
    /// Attempts to write this instance to the given <see cref="Span{T}"/>.
    /// </summary>
    /// <param name="destination">The <see cref="Span{T}"/> to write to.</param>
    /// <param name="charsWritten">
    /// When this method returns, this will contains the number of characters written to <paramref name="destination"/>.
    /// </param>
    /// <param name="format">A format string containing formatting specifications.</param>
    /// <param name="provider">
    /// An object that supplies format information about the current instance.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if this instance was successfully written to the
    /// <paramref name="destination"/>; otherwise <see langword="false"/>.
    /// </returns>
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        charsWritten = 0;
        if (destination.Length < 12
            || !"{Normal:".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 8;
        destination = destination[8..];

        if (!Normal.TryFormat(destination, out var c, format, provider))
        {
            return false;
        }
        charsWritten += c;
        destination = destination[c..];

        if (destination.Length < 4
            || !" D:".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 3;
        destination = destination[3..];

        if (!D.TryFormat(destination, out c, format, provider))
        {
            return false;
        }
        charsWritten += c;
        destination = destination[c..];

        if (destination.Length < 1)
        {
            return false;
        }
        destination[0] = '}';
        charsWritten++;
        return true;
    }
}
