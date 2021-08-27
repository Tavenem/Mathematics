using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace Tavenem.Mathematics;

/// <summary>
/// A structure encapsulating a four-dimensional vector (x,y,z,w), which is used to efficiently
/// rotate an object about the (x,y,z) vector by the angle theta, where w = cos(theta/2).
/// </summary>
[DebuggerDisplay("{ToString()}")]
public readonly struct Quaternion<TScalar> :
    IAdditionOperators<Quaternion<TScalar>, Quaternion<TScalar>, Quaternion<TScalar>>,
    IEqualityOperators<Quaternion<TScalar>, Quaternion<TScalar>>,
    IMultiplicativeIdentity<Quaternion<TScalar>, Quaternion<TScalar>>,
    IMultiplyOperators<Quaternion<TScalar>, Quaternion<TScalar>, Quaternion<TScalar>>,
    IMultiplyOperators<Quaternion<TScalar>, TScalar, Quaternion<TScalar>>,
    ISpanFormattable,
    ISubtractionOperators<Quaternion<TScalar>, Quaternion<TScalar>, Quaternion<TScalar>>,
    IUnaryNegationOperators<Quaternion<TScalar>, Quaternion<TScalar>>
    where TScalar : IFloatingPoint<TScalar>
{
    /// <summary>
    /// Returns a quaternion representing no rotation.
    /// </summary>
    public static Quaternion<TScalar> MultiplicativeIdentity => new()
    {
        X = TScalar.Zero,
        Y = TScalar.Zero,
        Z = TScalar.Zero,
        W = TScalar.One,
    };

    /// <summary>
    /// Specifies the X-value of the vector component of the quaternion.
    /// </summary>
    public TScalar X { get; init; }

    /// <summary>
    /// Specifies the Y-value of the vector component of the quaternion.
    /// </summary>
    public TScalar Y { get; init; }

    /// <summary>
    /// Specifies the Z-value of the vector component of the quaternion.
    /// </summary>
    public TScalar Z { get; init; }

    /// <summary>
    /// Specifies the rotation component of the quaternion.
    /// </summary>
    public TScalar W { get; init; }

    /// <summary>
    /// Creates a new quaternion with the given values.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    /// <param name="z">The Z component.</param>
    /// <param name="w">The W component.</param>
    public Quaternion(TScalar x, TScalar y, TScalar z, TScalar w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    /// <summary>
    /// Creates a new quaternion with the given value for all components.
    /// </summary>
    /// <param name="value">The value to use for all components.</param>
    public static Quaternion<TScalar> Create(TScalar value) => new() { X = value, Y = value, Z = value, W = value };

    /// <summary>
    /// Creates a new quaternion with the given values.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    /// <param name="z">The Z component.</param>
    /// <param name="w">The W component.</param>
    public static Quaternion<TScalar> Create(TScalar x, TScalar y, TScalar z, TScalar w) => new() { X = x, Y = y, Z = z, W = w };

    /// <summary>
    /// Creates a new quaternion with the given vector and rotation parts.
    /// </summary>
    /// <param name="vector3">
    /// A <see cref="Vector3{TScalar}"/> representing the vector part of the quaternion.
    /// </param>
    /// <param name="w">The rotation part of the quaternion.</param>
    public static Quaternion<TScalar> Create(Vector3<TScalar> vector3, TScalar w)
        => new() { X = vector3.X, Y = vector3.Y, Z = vector3.Z, W = w };

    /// <summary>
    /// Creates a new quaternion with the given values.
    /// </summary>
    /// <param name="values">The values to use for all components.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="values"/> does not contain enough elements
    /// </exception>
    public static Quaternion<TScalar> Create(TScalar[] values)
    {
        if (values.Length < 4)
        {
            throw new ArgumentException($"{nameof(values)} does not contain enough elements", nameof(values));
        }
        return new() { X = values[0], Y = values[1], Z = values[2], W = values[3] };
    }

    /// <summary>
    /// Creates a new quaternion with the given values.
    /// </summary>
    /// <param name="values">The values to use for all components.</param>
    /// <param name="startIndex"></param>
    /// <exception cref="ArgumentException">
    /// <paramref name="values"/> does not contain enough elements
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// the value of <paramref name="startIndex"/> does not leave enough elements in <paramref name="values"/>
    /// </exception>
    public static Quaternion<TScalar> Create(TScalar[] values, int startIndex)
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
        return new() { X = values[startIndex], Y = values[startIndex + 1], Z = values[startIndex + 2], W = values[startIndex + 3] };
    }

    /// <summary>
    /// Creates a new quaternion with the given values.
    /// </summary>
    /// <param name="values">The values to use for all components.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="values"/> does not contain enough elements
    /// </exception>
    public static Quaternion<TScalar> Create(ReadOnlySpan<TScalar> values)
    {
        if (values.Length < 4)
        {
            throw new ArgumentException($"{nameof(values)} does not contain enough elements", nameof(values));
        }
        return new() { X = values[0], Y = values[1], Z = values[2], W = values[3] };
    }

    /// <summary>
    /// Concatenates two quaternions; the result represents the <paramref name="value1"/>
    /// rotation followed by the <paramref name="value2"/> rotation.
    /// </summary>
    /// <param name="value1">The first quaternion rotation in the series.</param>
    /// <param name="value2">The second quaternion rotation in the series.</param>
    /// <returns>
    /// A new quaternion representing the concatenation of the <paramref name="value1"/>
    /// rotation followed by the <paramref name="value2"/> rotation.
    /// </returns>
    public static Quaternion<TScalar> Concatenate(Quaternion<TScalar> value1, Quaternion<TScalar> value2)
    {
        // Concatenate rotation is actually q2 * q1 instead of q1 * q2.
        // So that's why value2 goes q1 and value1 goes q2.
        var q1x = value2.X;
        var q1y = value2.Y;
        var q1z = value2.Z;
        var q1w = value2.W;

        var q2x = value1.X;
        var q2y = value1.Y;
        var q2z = value1.Z;
        var q2w = value1.W;

        // cross(av, bv)
        var cx = (q1y * q2z) - (q1z * q2y);
        var cy = (q1z * q2x) - (q1x * q2z);
        var cz = (q1x * q2y) - (q1y * q2x);

        var dot = (q1x * q2x) + (q1y * q2y) + (q1z * q2z);

        return new()
        {
            X = (q1x * q2w) + (q2x * q1w) + cx,
            Y = (q1y * q2w) + (q2y * q1w) + cy,
            Z = (q1z * q2w) + (q2z * q1w) + cz,
            W = (q1w * q2w) - dot,
        };
    }

    /// <summary>
    /// Concatenates this instance with another quaternion; the result represents this instance
    /// followed by the <paramref name="other"/> rotation.
    /// </summary>
    /// <param name="other">The second quaternion rotation in the series.</param>
    /// <returns>
    /// A new quaternion representing the concatenation of this instance
    /// rotation followed by the <paramref name="other"/> rotation.
    /// </returns>
    public Quaternion<TScalar> Concatenate(Quaternion<TScalar> other)
        => Concatenate(this, other);

    /// <summary>
    /// Creates the conjugate of a specified quaternion.
    /// </summary>
    /// <param name="value">The quaternion of which to return the conjugate.</param>
    /// <returns>A new quaternion that is the conjugate of the specified one.</returns>
    public static Quaternion<TScalar> Conjugate(Quaternion<TScalar> value) => new()
    {
        X = -value.X,
        Y = -value.Y,
        Z = -value.Z,
        W = value.W,
    };

    /// <summary>
    /// Creates the conjugate of this instance.
    /// </summary>
    /// <returns>A new quaternion that is the conjugate of this instance.</returns>
    public Quaternion<TScalar> Conjugate() => Conjugate(this);

    /// <summary>
    /// Creates a quaternion from a normalized vector axis and an angle to rotate about the vector.
    /// </summary>
    /// <param name="axis">
    /// <para>
    /// The unit vector to rotate around.
    /// </para>
    /// <para>
    /// This vector must be normalized before calling this function or the resulting quaternion will be incorrect.
    /// </para>
    /// </param>
    /// <param name="angle">The angle, in radians, to rotate around the vector.</param>
    /// <returns>The created quaternion.</returns>
    public static Quaternion<TScalar> CreateFromAxisAngle(Vector3<TScalar> axis, TScalar angle)
    {
        var halfAngle = angle / NumberValues.Two<TScalar>();
        var s = TScalar.Sin(halfAngle);
        var c = TScalar.Cos(halfAngle);

        return new()
        {
            X = axis.X * s,
            Y = axis.Y * s,
            Z = axis.Z * s,
            W = c,
        };
    }

    /// <summary>
    /// Creates a new quaternion from the given yaw, pitch, and roll, in radians.
    /// </summary>
    /// <param name="yaw">The yaw angle, in radians, around the Y-axis.</param>
    /// <param name="pitch">The pitch angle, in radians, around the X-axis.</param>
    /// <param name="roll">The roll angle, in radians, around the Z-axis.</param>
    /// <returns></returns>
    public static Quaternion<TScalar> CreateFromYawPitchRoll(TScalar yaw, TScalar pitch, TScalar roll)
    {
        TScalar sr, cr, sp, cp, sy, cy;

        var halfRoll = roll / NumberValues.Two<TScalar>();
        sr = TScalar.Sin(halfRoll);
        cr = TScalar.Cos(halfRoll);

        var halfPitch = pitch / NumberValues.Two<TScalar>();
        sp = TScalar.Sin(halfPitch);
        cp = TScalar.Cos(halfPitch);

        var halfYaw = yaw / NumberValues.Two<TScalar>();
        sy = TScalar.Sin(halfYaw);
        cy = TScalar.Cos(halfYaw);

        return new()
        {
            X = (cy * sp * cr) + (sy * cp * sr),
            Y = (sy * cp * cr) - (cy * sp * sr),
            Z = (cy * cp * sr) - (sy * sp * cr),
            W = (cy * cp * cr) + (sy * sp * sr),
        };
    }

    /// <summary>
    /// Creates a quaternion from the given rotation matrix.
    /// </summary>
    /// <param name="matrix">The rotation matrix.</param>
    /// <returns>The created quaternion.</returns>
    public static Quaternion<TScalar> CreateFromRotationMatrix(Matrix4x4<TScalar> matrix)
    {
        var trace = matrix.M11 + matrix.M22 + matrix.M33;

        if (trace > TScalar.Zero)
        {
            var s = TScalar.Sqrt(trace + TScalar.One);
            var invS = TScalar.One / (s * NumberValues.Two<TScalar>());
            return new()
            {
                X = s / NumberValues.Two<TScalar>(),
                Y = (matrix.M23 - matrix.M32) * invS,
                Z = (matrix.M31 - matrix.M13) * invS,
                W = (matrix.M12 - matrix.M21) * invS,
            };
        }
        else if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
        {
            var s = TScalar.Sqrt(TScalar.One + matrix.M11 - matrix.M22 - matrix.M33);
            var invS = TScalar.One / (s * NumberValues.Two<TScalar>());
            return new()
            {
                X = s / NumberValues.Two<TScalar>(),
                Y = (matrix.M12 + matrix.M21) * invS,
                Z = (matrix.M13 + matrix.M31) * invS,
                W = (matrix.M23 - matrix.M32) * invS,
            };
        }
        else if (matrix.M22 > matrix.M33)
        {
            var s = TScalar.Sqrt(TScalar.One + matrix.M22 - matrix.M11 - matrix.M33);
            var invS = TScalar.One / (s * NumberValues.Two<TScalar>());
            return new()
            {
                X = (matrix.M21 + matrix.M12) * invS,
                Y = s / NumberValues.Two<TScalar>(),
                Z = (matrix.M32 + matrix.M23) * invS,
                W = (matrix.M31 - matrix.M13) * invS,
            };
        }
        else
        {
            var s = TScalar.Sqrt(TScalar.One + matrix.M33 - matrix.M11 - matrix.M22);
            var invS = TScalar.One / (s * NumberValues.Two<TScalar>());
            return new()
            {
                X = (matrix.M31 + matrix.M13) * invS,
                Y = (matrix.M32 + matrix.M23) * invS,
                Z = s / NumberValues.Two<TScalar>(),
                W = (matrix.M12 - matrix.M21) * invS,
            };
        }
    }

    /// <summary>
    /// Calculates the dot product of two quaternions.
    /// </summary>
    /// <param name="quaternion1">The first source quaternion.</param>
    /// <param name="quaternion2">The second source quaternion.</param>
    /// <returns>The dot product of the Quaternions.</returns>
    public static TScalar Dot(Quaternion<TScalar> quaternion1, Quaternion<TScalar> quaternion2)
        => (quaternion1.X * quaternion2.X)
        + (quaternion1.Y * quaternion2.Y)
        + (quaternion1.Z * quaternion2.Z)
        + (quaternion1.W * quaternion2.W);

    /// <summary>
    /// Calculates the dot product of this instance and another quaternion.
    /// </summary>
    /// <param name="other">The other source quaternion.</param>
    /// <returns>The dot product of the quaternions.</returns>
    public TScalar Dot(Quaternion<TScalar> other) => Dot(this, other);

    /// <summary>
    /// Returns the inverse of a quaternion.
    /// </summary>
    /// <param name="value">The source quaternion.</param>
    /// <returns>The inverted quaternion.</returns>
    public static Quaternion<TScalar> Inverse(Quaternion<TScalar> value)
    {
        var ls = (value.X * value.X) + (value.Y * value.Y) + (value.Z * value.Z) + (value.W * value.W);
        var invNorm = TScalar.One / ls;

        return new()
        {
            X = -value.X * invNorm,
            Y = -value.Y * invNorm,
            Z = -value.Z * invNorm,
            W = value.W * invNorm,
        };
    }

    /// <summary>
    /// Returns the inverse of this instance.
    /// </summary>
    /// <returns>The inverted quaternion.</returns>
    public Quaternion<TScalar> Inverse() => Inverse(this);

    /// <summary>
    /// Calculates the length of the quaternion.
    /// </summary>
    /// <returns>The computed length of the quaternion.</returns>
    public static TScalar Length(Quaternion<TScalar> value) => TScalar.Sqrt(LengthSquared(value));

    /// <summary>
    /// Calculates the length of this instance.
    /// </summary>
    /// <returns>The computed length of this instance.</returns>
    public TScalar Length() => Length(this);

    /// <summary>
    /// Calculates the length squared of the quaternion. This operation is cheaper than Length().
    /// </summary>
    /// <returns>The length squared of the quaternion.</returns>
    public static TScalar LengthSquared(Quaternion<TScalar> value)
        => (value.X * value.X) + (value.Y * value.Y) + (value.Z * value.Z) + (value.W * value.W);

    /// <summary>
    /// Calculates the length squared of this instance. This operation is cheaper than Length().
    /// </summary>
    /// <returns>The length squared of this instance.</returns>
    public TScalar LengthSquared() => LengthSquared(this);

    /// <summary>
    ///  Linearly interpolates between two quaternions.
    /// </summary>
    /// <param name="quaternion1">The first source quaternion.</param>
    /// <param name="quaternion2">The second source quaternion.</param>
    /// <param name="amount">The relative weight of the second source quaternion in the interpolation.</param>
    /// <returns>The interpolated quaternion.</returns>
    public static Quaternion<TScalar> Lerp(Quaternion<TScalar> quaternion1, Quaternion<TScalar> quaternion2, TScalar amount)
    {
        var t = amount;
        var t1 = TScalar.One - t;

        Quaternion<TScalar> r;

        var dot = (quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)
                    + (quaternion1.Z * quaternion2.Z) + (quaternion1.W * quaternion2.W);

        if (dot >= TScalar.Zero)
        {
            r = new()
            {
                X = (t1 * quaternion1.X) + (t * quaternion2.X),
                Y = (t1 * quaternion1.Y) + (t * quaternion2.Y),
                Z = (t1 * quaternion1.Z) + (t * quaternion2.Z),
                W = (t1 * quaternion1.W) + (t * quaternion2.W),
            };
        }
        else
        {
            r = new()
            {
                X = (t1 * quaternion1.X) - (t * quaternion2.X),
                Y = (t1 * quaternion1.Y) - (t * quaternion2.Y),
                Z = (t1 * quaternion1.Z) - (t * quaternion2.Z),
                W = (t1 * quaternion1.W) - (t * quaternion2.W),
            };
        }

        // Normalize it.
        var ls = (r.X * r.X) + (r.Y * r.Y) + (r.Z * r.Z) + (r.W * r.W);
        var invNorm = TScalar.One / TScalar.Sqrt(ls);

        return r * invNorm;
    }

    /// <summary>
    ///  Linearly interpolates between this instance and another quaternion.
    /// </summary>
    /// <param name="other">The other source quaternion.</param>
    /// <param name="amount">The relative weight of the second source quaternion in the interpolation.</param>
    /// <returns>The interpolated quaternion.</returns>
    public Quaternion<TScalar> Lerp(Quaternion<TScalar> other, TScalar amount)
        => Lerp(this, other, amount);

    /// <summary>
    /// Divides each component of the quaternion by the length of the quaternion.
    /// </summary>
    /// <param name="value">The source quaternion.</param>
    /// <returns>The normalized quaternion.</returns>
    public static Quaternion<TScalar> Normalize(Quaternion<TScalar> value)
    {
        var ls = (value.X * value.X) + (value.Y * value.Y) + (value.Z * value.Z) + (value.W * value.W);

        var invNorm = TScalar.One / TScalar.Sqrt(ls);

        return new()
        {
            X = value.X * invNorm,
            Y = value.Y * invNorm,
            Z = value.Z * invNorm,
            W = value.W * invNorm,
        };
    }

    /// <summary>
    /// Divides each component of this instance by the length of this instance.
    /// </summary>
    /// <returns>The normalized quaternion.</returns>
    public Quaternion<TScalar> Normalize() => Normalize(this);

    /// <summary>
    /// Interpolates between two quaternions, using spherical linear interpolation.
    /// </summary>
    /// <param name="quaternion1">The first source quaternion.</param>
    /// <param name="quaternion2">The second source quaternion.</param>
    /// <param name="amount">The relative weight of the second source quaternion in the interpolation.</param>
    /// <returns>The interpolated quaternion.</returns>
    public static Quaternion<TScalar> Slerp(Quaternion<TScalar> quaternion1, Quaternion<TScalar> quaternion2, TScalar amount)
    {
        var t = amount;

        var cosOmega = (quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)
            + (quaternion1.Z * quaternion2.Z) + (quaternion1.W * quaternion2.W);

        var flip = false;

        if (cosOmega < TScalar.Zero)
        {
            flip = true;
            cosOmega = -cosOmega;
        }

        TScalar s1, s2;

        if (cosOmega > TScalar.One - (TScalar.One / (NumberValues.TenThousand<TScalar>() * NumberValues.Hundred<TScalar>()))) // 1-(1e-6)
        {
            // Too close, do straight linear interpolation.
            s1 = TScalar.One - t;
            s2 = flip ? -t : t;
        }
        else
        {
            var omega = TScalar.Acos(cosOmega);
            var invSinOmega = TScalar.One / TScalar.Sin(omega);

            s1 = TScalar.Sin((TScalar.One - t) * omega) * invSinOmega;
            s2 = flip
                ? -TScalar.Sin(t * omega) * invSinOmega
                : TScalar.Sin(t * omega) * invSinOmega;
        }

        return new()
        {
            X = (s1 * quaternion1.X) + (s2 * quaternion2.X),
            Y = (s1 * quaternion1.Y) + (s2 * quaternion2.Y),
            Z = (s1 * quaternion1.Z) + (s2 * quaternion2.Z),
            W = (s1 * quaternion1.W) + (s2 * quaternion2.W),
        };
    }

    /// <summary>
    /// Interpolates between this instance and another quaternion, using spherical linear interpolation.
    /// </summary>
    /// <param name="other">The other source quaternion.</param>
    /// <param name="amount">The relative weight of the second source quaternion in the interpolation.</param>
    /// <returns>The interpolated quaternion.</returns>
    public Quaternion<TScalar> Slerp(Quaternion<TScalar> other, TScalar amount)
        => Slerp(this, other, amount);

    /// <summary>
    /// Flips the sign of each component of the quaternion.
    /// </summary>
    /// <param name="value">The source quaternion.</param>
    /// <returns>The negated quaternion.</returns>
    public static Quaternion<TScalar> operator -(Quaternion<TScalar> value) => new()
    {
        X = -value.X,
        Y = -value.Y,
        Z = -value.Z,
        W = -value.W,
    };

    /// <summary>
    /// Adds two Quaternions element-by-element.
    /// </summary>
    /// <param name="value1">The first source quaternion.</param>
    /// <param name="value2">The second source quaternion.</param>
    /// <returns>The result of adding the Quaternions.</returns>
    public static Quaternion<TScalar> operator +(Quaternion<TScalar> value1, Quaternion<TScalar> value2) => new()
    {
        X = value1.X + value2.X,
        Y = value1.Y + value2.Y,
        Z = value1.Z + value2.Z,
        W = value1.W + value2.W,
    };

    /// <summary>
    /// Subtracts one quaternion from another.
    /// </summary>
    /// <param name="value1">The first source quaternion.</param>
    /// <param name="value2">The second Quaternion, to be subtracted from the first.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Quaternion<TScalar> operator -(Quaternion<TScalar> value1, Quaternion<TScalar> value2) => new()
    {
        X = value1.X - value2.X,
        Y = value1.Y - value2.Y,
        Z = value1.Z - value2.Z,
        W = value1.W - value2.W,
    };

    /// <summary>
    /// Multiplies two Quaternions together.
    /// </summary>
    /// <param name="value1">The quaternion on the left side of the multiplication.</param>
    /// <param name="value2">The quaternion on the right side of the multiplication.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Quaternion<TScalar> operator *(Quaternion<TScalar> value1, Quaternion<TScalar> value2)
    {
        var q1x = value1.X;
        var q1y = value1.Y;
        var q1z = value1.Z;
        var q1w = value1.W;

        var q2x = value2.X;
        var q2y = value2.Y;
        var q2z = value2.Z;
        var q2w = value2.W;

        // cross(av, bv)
        var cx = (q1y * q2z) - (q1z * q2y);
        var cy = (q1z * q2x) - (q1x * q2z);
        var cz = (q1x * q2y) - (q1y * q2x);

        var dot = (q1x * q2x) + (q1y * q2y) + (q1z * q2z);

        return new()
        {
            X = (q1x * q2w) + (q2x * q1w) + cx,
            Y = (q1y * q2w) + (q2y * q1w) + cy,
            Z = (q1z * q2w) + (q2z * q1w) + cz,
            W = (q1w * q2w) - dot,
        };
    }

    /// <summary>
    /// Multiplies a quaternion by a scalar value.
    /// </summary>
    /// <param name="value1">The source quaternion.</param>
    /// <param name="value2">The scalar value.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Quaternion<TScalar> operator *(Quaternion<TScalar> value1, TScalar value2) => new()
    {
        X = value1.X * value2,
        Y = value1.Y * value2,
        Z = value1.Z * value2,
        W = value1.W * value2,
    };

    /// <summary>
    /// Divides a quaternion by another quaternion.
    /// </summary>
    /// <param name="value1">The source quaternion.</param>
    /// <param name="value2">The divisor.</param>
    /// <returns>The result of the division.</returns>
    public static Quaternion<TScalar> operator /(Quaternion<TScalar> value1, Quaternion<TScalar> value2)
    {
        var q1x = value1.X;
        var q1y = value1.Y;
        var q1z = value1.Z;
        var q1w = value1.W;

        // Inverse part.
        var ls = (value2.X * value2.X) + (value2.Y * value2.Y)
                    + (value2.Z * value2.Z) + (value2.W * value2.W);
        var invNorm = TScalar.One / ls;

        var q2x = -value2.X * invNorm;
        var q2y = -value2.Y * invNorm;
        var q2z = -value2.Z * invNorm;
        var q2w = value2.W * invNorm;

        // Multiply part.

        // cross(av, bv)
        var cx = (q1y * q2z) - (q1z * q2y);
        var cy = (q1z * q2x) - (q1x * q2z);
        var cz = (q1x * q2y) - (q1y * q2x);

        var dot = (q1x * q2x) + (q1y * q2y) + (q1z * q2z);

        return new()
        {
            X = (q1x * q2w) + (q2x * q1w) + cx,
            Y = (q1y * q2w) + (q2y * q1w) + cy,
            Z = (q1z * q2w) + (q2z * q1w) + cz,
            W = (q1w * q2w) - dot,
        };
    }

    /// <summary>
    /// Returns a boolean indicating whether the two given Quaternions are equal.
    /// </summary>
    /// <param name="value1">The first quaternion to compare.</param>
    /// <param name="value2">The second quaternion to compare.</param>
    /// <returns>True if the Quaternions are equal; False otherwise.</returns>
    public static bool operator ==(Quaternion<TScalar> value1, Quaternion<TScalar> value2)
        => value1.X == value2.X
        && value1.Y == value2.Y
        && value1.Z == value2.Z
        && value1.W == value2.W;

    /// <summary>
    /// Returns a boolean indicating whether the two given Quaternions are not equal.
    /// </summary>
    /// <param name="value1">The first quaternion to compare.</param>
    /// <param name="value2">The second quaternion to compare.</param>
    /// <returns>True if the Quaternions are not equal; False if they are equal.</returns>
    public static bool operator !=(Quaternion<TScalar> value1, Quaternion<TScalar> value2)
        => value1.X != value2.X
        || value1.Y != value2.Y
        || value1.Z != value2.Z
        || value1.W != value2.W;

    /// <summary>
    /// Converts between implementations of quaternions.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator Quaternion<TScalar>(Quaternion value) => new()
    {
        X = TScalar.Create(value.X),
        Y = TScalar.Create(value.Y),
        Z = TScalar.Create(value.Z),
        W = TScalar.Create(value.W),
    };

    /// <summary>
    /// Converts between implementations of quaternions.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static explicit operator Quaternion(Quaternion<TScalar> value) => new(
        value.X.Create<TScalar, float>(),
        value.Y.Create<TScalar, float>(),
        value.Z.Create<TScalar, float>(),
        value.W.Create<TScalar, float>());

    /// <summary>
    /// Returns a boolean indicating whether the given quaternion is equal to this quaternion instance.
    /// </summary>
    /// <param name="other">The quaternion to compare this instance to.</param>
    /// <returns>True if the other quaternion is equal to this instance; False otherwise.</returns>
    public bool Equals(Quaternion<TScalar> other)
        => X == other.X && Y == other.Y && Z == other.Z && W == other.W;

    /// <summary>
    /// Returns a boolean indicating whether the given Object is equal to this quaternion instance.
    /// </summary>
    /// <param name="obj">The Object to compare against.</param>
    /// <returns>True if the Object is equal to this Quaternion; False otherwise.</returns>
    public override bool Equals(object? obj) => obj is Quaternion<TScalar> other && Equals(other);

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);

    /// <summary>
    /// Returns a <see cref="string"/> representing this instance, using the specified
    /// <paramref name="format"/> to format individual elements and the given <see cref="IFormatProvider"/>.
    /// </summary>
    /// <param name="format">The format of individual elements.</param>
    /// <param name="formatProvider">
    /// The format provider to use when formatting elements.
    /// </param>
    /// <returns>The <see cref="string"/> representation.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider) => new StringBuilder("{X:")
        .Append(X.ToString(format, formatProvider))
        .Append(" Y:")
        .Append(Y.ToString(format, formatProvider))
        .Append(" Z:")
        .Append(Z.ToString(format, formatProvider))
        .Append(" W:")
        .Append(W.ToString(format, formatProvider))
        .Append('}')
        .ToString();

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
        if (destination.Length < 13
            || !"{X:".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 3;
        destination = destination[3..];

        if (!X.TryFormat(destination, out var c, format, provider))
        {
            return false;
        }
        charsWritten += c;
        destination = destination[c..];

        if (destination.Length < 10
            || !" Y:".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 3;
        destination = destination[3..];

        if (!Y.TryFormat(destination, out c, format, provider))
        {
            return false;
        }
        charsWritten += c;
        destination = destination[c..];

        if (destination.Length < 7
            || !" Z:".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 3;
        destination = destination[3..];

        if (!Z.TryFormat(destination, out c, format, provider))
        {
            return false;
        }
        charsWritten += c;
        destination = destination[c..];

        if (destination.Length < 4
            || !" W:".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 3;
        destination = destination[3..];

        if (!W.TryFormat(destination, out c, format, provider))
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
