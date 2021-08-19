using System.Diagnostics;
using System.Numerics;

namespace Tavenem.Mathematics;

/// <summary>
/// A structure encapsulating four values.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public readonly struct Vector4<TScalar> : ISpatialVector<Vector4<TScalar>, TScalar>
    where TScalar : IFloatingPoint<TScalar>
{
    /// <summary>
    /// The number of values represented by this vector.
    /// </summary>
#pragma warning disable RCS1158 // Static member in generic type should use a type parameter.
    public static int Count => 4;
#pragma warning restore RCS1158 // Static member in generic type should use a type parameter.

    /// <summary>
    /// Returns a vector with 1 in all positions.
    /// </summary>
    public static Vector4<TScalar> One => new() { X = TScalar.One, Y = TScalar.One, Z = TScalar.One, W = TScalar.One };
    /// <summary>
    /// Returns a vector with 1 in all positions.
    /// </summary>
    public static Vector4<TScalar> MultiplicativeIdentity => One;

    /// <summary>
    /// Returns the vector (1,0,0).
    /// </summary>
    public static Vector4<TScalar> UnitX { get; } = new() { X = TScalar.One, Y = TScalar.Zero, Z = TScalar.Zero, W = TScalar.Zero };

    /// <summary>
    /// Returns the vector (0,1,0).
    /// </summary>
    public static Vector4<TScalar> UnitY { get; } = new() { X = TScalar.Zero, Y = TScalar.One, Z = TScalar.Zero, W = TScalar.Zero };

    /// <summary>
    /// Returns the vector (0,0,1).
    /// </summary>
    public static Vector4<TScalar> UnitZ { get; } = new() { X = TScalar.Zero, Y = TScalar.Zero, Z = TScalar.One, W = TScalar.Zero };

    /// <summary>
    /// Returns a vector with 0 in all positions.
    /// </summary>
    public static Vector4<TScalar> Zero => new();
    /// <summary>
    /// Returns a vector with 0 in all positions.
    /// </summary>
    public static Vector4<TScalar> AdditiveIdentity => Zero;

    /// <summary>
    /// Returns the vector (0,0,0,1).
    /// </summary>
    public static Vector4<TScalar> UnitW { get; } = new() { X = TScalar.Zero, Y = TScalar.Zero, Z = TScalar.Zero, W = TScalar.One };

    /// <summary>
    /// The X component of the vector.
    /// </summary>
    public TScalar X { get; init; }

    /// <summary>
    /// The Y component of the vector.
    /// </summary>
    public TScalar Y { get; init; }

    /// <summary>
    /// The Z component of the vector.
    /// </summary>
    public TScalar Z { get; init; }

    /// <summary>
    /// The W component of the vector.
    /// </summary>
    public TScalar W { get; init; }

    /// <summary>
    /// Provides access to the values of this vector.
    /// </summary>
    /// <param name="index">The index of the value to retrieve.</param>
    /// <returns>The value at the given <paramref name="index"/>.</returns>
    /// <exception cref="IndexOutOfRangeException" />
    public TScalar this[int index]
    {
        get
        {
            if (index == 0)
            {
                return X;
            }
            if (index == 1)
            {
                return Y;
            }
            if (index == 2)
            {
                return Z;
            }
            if (index == 3)
            {
                return W;
            }
            throw new IndexOutOfRangeException();
        }
    }

    /// <summary>
    /// Creates a new vector with the given values.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    /// <param name="z">The Z component.</param>
    /// <param name="w">The W component.</param>
    public Vector4(TScalar x, TScalar y, TScalar z, TScalar w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    /// <summary>
    /// Creates a new vector with the given value for all components.
    /// </summary>
    /// <param name="value">The value to use for all components.</param>
    public static Vector4<TScalar> Create(TScalar value) => new() { X = value, Y = value, Z = value, W = value };

    /// <summary>
    /// Creates a new vector with the given values.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    /// <param name="z">The Z component.</param>
    /// <param name="w">The W component.</param>
    public static Vector4<TScalar> Create(TScalar x, TScalar y, TScalar z, TScalar w) => new() { X = x, Y = y, Z = z, W = w };

    /// <summary>
    /// Creates a new vector with the given values.
    /// </summary>
    /// <param name="vector3">
    /// A <see cref="Vector3{TScalar}"/> representing the X, Y, and Z components.
    /// </param>
    /// <param name="w">The W component.</param>
    public static Vector4<TScalar> Create(Vector3<TScalar> vector3, TScalar w)
        => new() { X = vector3.X, Y = vector3.Y, Z = vector3.Z, W = w };

    /// <summary>
    /// Creates a new vector with the given values.
    /// </summary>
    /// <param name="values">The values to use for all components.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="values"/> does not contain enough elements
    /// </exception>
    public static Vector4<TScalar> Create(TScalar[] values)
    {
        if (values.Length < Count)
        {
            throw new ArgumentException($"{nameof(values)} does not contain enough elements", nameof(values));
        }
        return new() { X = values[0], Y = values[1], Z = values[2], W = values[3] };
    }

    /// <summary>
    /// Creates a new vector with the given values.
    /// </summary>
    /// <param name="values">The values to use for all components.</param>
    /// <param name="startIndex"></param>
    /// <exception cref="ArgumentException">
    /// <paramref name="values"/> does not contain enough elements
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// the value of <paramref name="startIndex"/> does not leave enough elements in <paramref name="values"/>
    /// </exception>
    public static Vector4<TScalar> Create(TScalar[] values, int startIndex)
    {
        if (values.Length < Count)
        {
            throw new ArgumentException($"{nameof(values)} does not contain enough elements");
        }
        if (startIndex + values.Length < Count)
        {
            throw new ArgumentOutOfRangeException(
                nameof(startIndex),
                $"the value of {nameof(startIndex)} does not leave enough elements in {nameof(values)}");
        }
        return new() { X = values[startIndex], Y = values[startIndex + 1], Z = values[startIndex + 2], W = values[startIndex + 3] };
    }

    /// <summary>
    /// Creates a new vector with the given values.
    /// </summary>
    /// <param name="values">The values to use for all components.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="values"/> does not contain enough elements
    /// </exception>
    public static Vector4<TScalar> Create(ReadOnlySpan<TScalar> values)
    {
        if (values.Length < Count)
        {
            throw new ArgumentException($"{nameof(values)} does not contain enough elements", nameof(values));
        }
        return new() { X = values[0], Y = values[1], Z = values[2], W = values[3] };
    }

    /// <summary>
    /// Returns a vector whose elements are the absolute values of each of the source vector's elements.
    /// </summary>
    /// <param name="value">The source vector.</param>
    /// <returns>The absolute value vector.</returns>
    public static Vector4<TScalar> Abs(Vector4<TScalar> value)
        => VectorCommon.Abs<Vector4<TScalar>, TScalar>(value);

    /// <summary>
    /// Restricts a vector between a min and max value.
    /// </summary>
    /// <param name="value1">The source vector.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    public static Vector4<TScalar> Clamp(Vector4<TScalar> value1, Vector4<TScalar> min, Vector4<TScalar> max)
        => VectorCommon.Clamp<Vector4<TScalar>, TScalar>(value1, min, max);

    /// <summary>
    /// Returns the Euclidean distance between the two given points.
    /// </summary>
    /// <param name="value1">The first point.</param>
    /// <param name="value2">The second point.</param>
    /// <returns>The distance.</returns>
    public static TScalar Distance(Vector4<TScalar> value1, Vector4<TScalar> value2)
        => VectorCommon.Distance<Vector4<TScalar>, TScalar>(value1, value2);

    /// <summary>
    /// Returns the Euclidean distance squared between the two given points.
    /// </summary>
    /// <param name="value1">The first point.</param>
    /// <param name="value2">The second point.</param>
    /// <returns>The distance squared.</returns>
    public static TScalar DistanceSquared(Vector4<TScalar> value1, Vector4<TScalar> value2)
        => VectorCommon.DistanceSquared<Vector4<TScalar>, TScalar>(value1, value2);

    /// <summary>
    /// Returns the dot product of two vectors.
    /// </summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <returns>The dot product.</returns>
    public static TScalar Dot(Vector4<TScalar> left, Vector4<TScalar> right)
        => VectorCommon.Dot<Vector4<TScalar>, TScalar>(left, right);

    /// <summary>
    /// Determines if a vector is nearly zero (all elements closer to zero than <see
    /// cref="NumberValues.NearlyZeroDouble"/>).
    /// </summary>
    /// <param name="value">A vector to test.</param>
    /// <returns>
    /// <see langword="true"/> if the vector is close to zero; otherwise <see langword="false"/>.
    /// </returns>
    public static bool IsNearlyZero(Vector4<TScalar> value)
        => VectorCommon.IsNearlyZero<Vector4<TScalar>, TScalar>(value);

    /// <summary>
    /// Returns the length of the vector.
    /// </summary>
    /// <returns>The vector's length.</returns>
    public static TScalar Length(Vector4<TScalar> value)
        => VectorCommon.Length<Vector4<TScalar>, TScalar>(value);

    /// <summary>
    /// Returns the length of the vector squared.
    /// </summary>
    /// <returns>The vector's length squared.</returns>
    public static TScalar LengthSquared(Vector4<TScalar> value)
        => VectorCommon.LengthSquared<Vector4<TScalar>, TScalar>(value);

    /// <summary>
    /// Linearly interpolates between two vectors based on the given weighting.
    /// </summary>
    /// <param name="value1">The first source vector.</param>
    /// <param name="value2">The second source vector.</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of the second source
    /// vector.</param>
    /// <returns>The interpolated vector.</returns>
    public static Vector4<TScalar> Lerp(Vector4<TScalar> value1, Vector4<TScalar> value2, TScalar amount)
        => VectorCommon.Lerp<Vector4<TScalar>, TScalar>(value1, value2, amount);

    /// <summary>
    /// Returns a vector whose elements are the maximum of each of the pairs of elements in the
    /// two source vectors
    /// </summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <returns>The maximized vector</returns>
    public static Vector4<TScalar> Max(Vector4<TScalar> left, Vector4<TScalar> right)
        => VectorCommon.Max<Vector4<TScalar>, TScalar>(left, right);

    /// <summary>
    /// Returns a vector whose elements are the minimum of each of the pairs of elements in the
    /// two source vectors.
    /// </summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <returns>The minimized vector.</returns>
    public static Vector4<TScalar> Min(Vector4<TScalar> left, Vector4<TScalar> right)
        => VectorCommon.Min<Vector4<TScalar>, TScalar>(left, right);

    /// <summary>
    /// Returns a vector with the same direction as the given vector, but with a length of 1.
    /// </summary>
    /// <param name="value">The vector to normalize.</param>
    /// <returns>The normalized vector.</returns>
    public static Vector4<TScalar> Normalize(Vector4<TScalar> value)
        => VectorCommon.Normalize<Vector4<TScalar>, TScalar>(value);

    /// <summary>
    /// Returns the reflection of a vector off a surface that has the specified normal.
    /// </summary>
    /// <param name="vector">The source vector.</param>
    /// <param name="normal">The normal of the surface being reflected off.</param>
    /// <returns>The reflected vector.</returns>
    public static Vector4<TScalar> Reflect(Vector4<TScalar> vector, Vector4<TScalar> normal)
        => VectorCommon.Reflect<Vector4<TScalar>, TScalar>(vector, normal);

    /// <summary>
    /// Returns a vector whose elements are the square root of each of the source vector's
    /// elements.
    /// </summary>
    /// <param name="value">The source vector.</param>
    /// <returns>The square root vector.</returns>
    public static Vector4<TScalar> SquareRoot(Vector4<TScalar> value)
        => VectorCommon.SquareRoot<Vector4<TScalar>, TScalar>(value);

    /// <summary>
    /// Transforms a vector by the given matrix.
    /// </summary>
    /// <param name="position">The source vector.</param>
    /// <param name="matrix">The transformation matrix.</param>
    /// <returns>The transformed vector.</returns>
    public static Vector4<TScalar> Transform(Vector2<TScalar> position, Matrix4x4<TScalar> matrix) => new()
    {
        X = (position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41,
        Y = (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42,
        Z = (position.X * matrix.M13) + (position.Y * matrix.M23) + matrix.M43,
        W = (position.X * matrix.M14) + (position.Y * matrix.M24) + matrix.M44,
    };

    /// <summary>
    /// Transforms a vector by the given matrix.
    /// </summary>
    /// <param name="position">The source vector.</param>
    /// <param name="matrix">The transformation matrix.</param>
    /// <returns>The transformed vector.</returns>
    public static Vector4<TScalar> Transform(Vector3<TScalar> position, Matrix4x4<TScalar> matrix) => new()
    {
        X = (position.X * matrix.M11) + (position.Y * matrix.M21) + (position.Z * matrix.M31) + matrix.M41,
        Y = (position.X * matrix.M12) + (position.Y * matrix.M22) + (position.Z * matrix.M32) + matrix.M42,
        Z = (position.X * matrix.M13) + (position.Y * matrix.M23) + (position.Z * matrix.M33) + matrix.M43,
        W = (position.X * matrix.M14) + (position.Y * matrix.M24) + (position.Z * matrix.M34) + matrix.M44,
    };

    /// <summary>
    /// Transforms a vector by the given matrix.
    /// </summary>
    /// <param name="vector">The source vector.</param>
    /// <param name="matrix">The transformation matrix.</param>
    /// <returns>The transformed vector.</returns>
    public static Vector4<TScalar> Transform(Vector4<TScalar> vector, Matrix4x4<TScalar> matrix) => new()
    {
        X = (vector.X * matrix.M11) + (vector.Y * matrix.M21) + (vector.Z * matrix.M31) + (vector.W * matrix.M41),
        Y = (vector.X * matrix.M12) + (vector.Y * matrix.M22) + (vector.Z * matrix.M32) + (vector.W * matrix.M42),
        Z = (vector.X * matrix.M13) + (vector.Y * matrix.M23) + (vector.Z * matrix.M33) + (vector.W * matrix.M43),
        W = (vector.X * matrix.M14) + (vector.Y * matrix.M24) + (vector.Z * matrix.M34) + (vector.W * matrix.M44),
    };

    /// <summary>
    /// Transforms a vector by the given Quaternion rotation value.
    /// </summary>
    /// <param name="value">The source vector to be rotated.</param>
    /// <param name="rotation">The rotation to apply.</param>
    /// <returns>The transformed vector.</returns>
    public static Vector4<TScalar> Transform(Vector2<TScalar> value, Quaternion<TScalar> rotation)
    {
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

        return new()
        {
            X = (value.X * (TScalar.One - yy2 - zz2)) + (value.Y * (xy2 - wz2)),
            Y = (value.X * (xy2 + wz2)) + (value.Y * (TScalar.One - xx2 - zz2)),
            Z = (value.X * (xz2 - wy2)) + (value.Y * (yz2 + wx2)),
            W = TScalar.One,
        };
    }

    /// <summary>
    /// Transforms a vector by the given Quaternion rotation value.
    /// </summary>
    /// <param name="value">The source vector to be rotated.</param>
    /// <param name="rotation">The rotation to apply.</param>
    /// <returns>The transformed vector.</returns>
    public static Vector4<TScalar> Transform(Vector3<TScalar> value, Quaternion<TScalar> rotation)
    {
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

        return new()
        {
            X = (value.X * (TScalar.One - yy2 - zz2)) + (value.Y * (xy2 - wz2)) + (value.Z * (xz2 + wy2)),
            Y = (value.X * (xy2 + wz2)) + (value.Y * (TScalar.One - xx2 - zz2)) + (value.Z * (yz2 - wx2)),
            Z = (value.X * (xz2 - wy2)) + (value.Y * (yz2 + wx2)) + (value.Z * (TScalar.One - xx2 - yy2)),
            W = TScalar.One,
        };
    }

    /// <summary>
    /// Transforms a vector by the given Quaternion rotation value.
    /// </summary>
    /// <param name="value">The source vector to be rotated.</param>
    /// <param name="rotation">The rotation to apply.</param>
    /// <returns>The transformed vector.</returns>
    public static Vector4<TScalar> Transform(Vector4<TScalar> value, Quaternion<TScalar> rotation)
    {
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

        return new()
        {
            X = (value.X * (TScalar.One - yy2 - zz2)) + (value.Y * (xy2 - wz2)) + (value.Z * (xz2 + wy2)),
            Y = (value.X * (xy2 + wz2)) + (value.Y * (TScalar.One - xx2 - zz2)) + (value.Z * (yz2 - wx2)),
            Z = (value.X * (xz2 - wy2)) + (value.Y * (yz2 + wx2)) + (value.Z * (TScalar.One - xx2 - yy2)),
            W = value.W,
        };
    }

    /// <summary>
    /// Adds two vectors together.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The summed vector.</returns>
    public static Vector4<TScalar> operator +(Vector4<TScalar> left, Vector4<TScalar> right) => new()
    {
        X = left.X + right.X,
        Y = left.Y + right.Y,
        Z = left.Z + right.Z,
        W = left.W + right.W,
    };

    /// <summary>
    /// Subtracts the second vector from the first.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The difference vector.</returns>
    public static Vector4<TScalar> operator -(Vector4<TScalar> left, Vector4<TScalar> right) => new()
    {
        X = left.X - right.X,
        Y = left.Y - right.Y,
        Z = left.Z - right.Z,
        W = left.W - right.W,
    };

    /// <summary>
    /// Multiplies two vectors together.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The product vector.</returns>
    public static Vector4<TScalar> operator *(Vector4<TScalar> left, Vector4<TScalar> right) => new()
    {
        X = left.X * right.X,
        Y = left.Y * right.Y,
        Z = left.Z * right.Z,
        W = left.W * right.W,
    };

    /// <summary>
    /// Multiplies a vector by the given scalar.
    /// </summary>
    /// <param name="left">The source vector.</param>
    /// <param name="right">The scalar value.</param>
    /// <returns>The scaled vector.</returns>
    public static Vector4<TScalar> operator *(Vector4<TScalar> left, TScalar right) => new()
    {
        X = left.X * right,
        Y = left.Y * right,
        Z = left.Z * right,
        W = left.W * right,
    };

    /// <summary>
    /// Multiplies a vector by the given scalar.
    /// </summary>
    /// <param name="left">The scalar value.</param>
    /// <param name="right">The source vector.</param>
    /// <returns>The scaled vector.</returns>
    public static Vector4<TScalar> operator *(TScalar left, Vector4<TScalar> right) => new()
    {
        X = left * right.X,
        Y = left * right.Y,
        Z = left * right.Z,
        W = left * right.W,
    };

    /// <summary>
    /// Divides the first vector by the second.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The vector resulting from the division.</returns>
    public static Vector4<TScalar> operator /(Vector4<TScalar> left, Vector4<TScalar> right) => new()
    {
        X = left.X / right.X,
        Y = left.Y / right.Y,
        Z = left.Z / right.Z,
        W = left.W / right.W,
    };

    /// <summary>
    /// Divides the vector by the given scalar.
    /// </summary>
    /// <param name="value1">The source vector.</param>
    /// <param name="value2">The scalar value.</param>
    /// <returns>The result of the division.</returns>
    public static Vector4<TScalar> operator /(Vector4<TScalar> value1, TScalar value2) => new()
    {
        X = value1.X / value2,
        Y = value1.Y / value2,
        Z = value1.Z / value2,
        W = value1.W / value2,
    };

    /// <summary>
    /// Negates a given vector.
    /// </summary>
    /// <param name="value">The source vector.</param>
    /// <returns>The negated vector.</returns>
    public static Vector4<TScalar> operator -(Vector4<TScalar> value) => new()
    {
        X = -value.X,
        Y = -value.Y,
        Z = -value.Z,
        W = -value.W,
    };

    /// <summary>
    /// Returns a boolean indicating whether the two given vectors are equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns>True if the vectors are equal; False otherwise.</returns>
    public static bool operator ==(Vector4<TScalar> left, Vector4<TScalar> right) => left.Equals(right);

    /// <summary>
    /// Returns a boolean indicating whether the two given vectors are not equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns>True if the vectors are not equal; False if they are equal.</returns>
    public static bool operator !=(Vector4<TScalar> left, Vector4<TScalar> right) => !left.Equals(right);

    /// <summary>
    /// Returns a boolean indicating whether the two given vectors are equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns>True if the vectors are equal; False otherwise.</returns>
    public static bool operator >(Vector4<TScalar> left, Vector4<TScalar> right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Returns a boolean indicating whether the two given vectors are equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns>True if the vectors are equal; False otherwise.</returns>
    public static bool operator >=(Vector4<TScalar> left, Vector4<TScalar> right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Returns a boolean indicating whether the two given vectors are not equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns>True if the vectors are not equal; False if they are equal.</returns>
    public static bool operator <(Vector4<TScalar> left, Vector4<TScalar> right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Returns a boolean indicating whether the two given vectors are not equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns>True if the vectors are not equal; False if they are equal.</returns>
    public static bool operator <=(Vector4<TScalar> left, Vector4<TScalar> right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Converts between implementations of vectors.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator Vector4<TScalar>(Vector4 value) => new()
    {
        X = TScalar.Create(value.X),
        Y = TScalar.Create(value.Y),
        Z = TScalar.Create(value.Z),
        W = TScalar.Create(value.W),
    };

    /// <summary>
    /// Converts between implementations of vectors.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static explicit operator Vector4(Vector4<TScalar> value) => new(
        Extensions.TypeConvert<float, TScalar>(value.X),
        Extensions.TypeConvert<float, TScalar>(value.Y),
        Extensions.TypeConvert<float, TScalar>(value.Z),
        Extensions.TypeConvert<float, TScalar>(value.W));

    /// <summary>
    /// Compares the current instance with another object of the same type and returns an
    /// integer that indicates whether the current instance precedes, follows, or occurs in the
    /// same position in the sort order as the other object.
    /// </summary>
    /// <param name="other">An object to compare with this instance.</param>
    /// <returns>A value that indicates the relative order of the objects being
    /// compared.</returns>
    public int CompareTo(Vector4<TScalar> other)
    {
        if (Equals(other))
        {
            return 0;
        }
        if (VectorCommon.LengthSquared<Vector4<TScalar>, TScalar>(this)
            >= VectorCommon.LengthSquared<Vector4<TScalar>, TScalar>(other))
        {
            return 1;
        }
        return -1;
    }

    /// <summary>Compares the current instance with another object of the same type and returns
    /// an integer that indicates whether the current instance precedes, follows, or occurs in
    /// the same position in the sort order as the other object.</summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>
    /// A value that indicates the relative order of the objects being compared. The return
    /// value has these meanings: Value Meaning Less than zero This instance precedes <paramref
    /// name="obj" /> in the sort order. Zero This instance occurs in the same position in the
    /// sort order as <paramref name="obj" />. Greater than zero This instance follows <paramref
    /// name="obj" /> in the sort order.</returns>
    /// <exception cref="ArgumentException"><paramref name="obj" /> is not the same type as this
    /// instance.</exception>
    public int CompareTo(object? obj)
    {
        if (obj is Vector4<TScalar> other)
        {
            return CompareTo(other);
        }
        throw new ArgumentException($"{nameof(obj)} is not the same type as this instance.", nameof(obj));
    }

    /// <summary>
    /// Copies the values of this vector to the given array.
    /// </summary>
    /// <param name="destination">An array.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="destination"/> is not long enough
    /// </exception>
    public void CopyTo(TScalar[] destination)
        => ((ISpatialVector<Vector4<TScalar>, TScalar>)this).CopyTo(destination);

    /// <summary>
    /// Copies the values of this vector to the given array, starting at the given position.
    /// </summary>
    /// <param name="destination">An array.</param>
    /// <param name="startIndex">
    /// The position in <paramref name="destination"/> at which to begin writing values.
    /// </param>
    /// <exception cref="ArgumentException">
    /// <paramref name="destination"/> is not long enough
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// the value of <paramref name="startIndex"/> would cause elements to be written beyond the end of <paramref name="destination"/>
    /// </exception>
    public void CopyTo(TScalar[] destination, int startIndex)
        => ((ISpatialVector<Vector4<TScalar>, TScalar>)this).CopyTo(destination, startIndex);

    /// <summary>
    /// Copies the values of this vector to the given span.
    /// </summary>
    /// <param name="destination">A <see cref="Span{T}"/> of <typeparamref name="TScalar"/>.</param>
    /// <exception cref="ArgumentException"></exception>
    public void CopyTo(Span<TScalar> destination)
        => ((ISpatialVector<Vector4<TScalar>, TScalar>)this).CopyTo(destination);

    /// <summary>
    /// Returns a boolean indicating whether the given vector is equal to this instance.
    /// </summary>
    /// <param name="other">The vector to compare with this instance.</param>
    /// <returns>
    /// <see langword="true"/> if the other vector3 is equal to this instance; otherwise <see langword="false"/>.
    /// </returns>
    public bool Equals(Vector4<TScalar> other) => X == other.X && Y == other.Y && Z == other.Z && W == other.W;

    /// <summary>
    /// Returns a boolean indicating whether the given Object is equal to this instance.
    /// </summary>
    /// <param name="obj">The Object to compare against.</param>
    /// <returns>
    /// <see langword="true"/> if the Object is equal to this instance; otherwise <see langword="false"/>.
    /// </returns>
    public override bool Equals(object? obj) => obj is Vector4<TScalar> other && Equals(other);

    /// <summary>Returns the hash code for this instance.</summary>
    /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
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
    public string ToString(string? format, IFormatProvider? formatProvider)
        => ((ISpatialVector<Vector4<TScalar>, TScalar>)this).ToString(format, formatProvider);

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
    /// Attempts to copy the values of this vector to the given span.
    /// </summary>
    /// <param name="destination">An array.</param>
    public bool TryCopyTo(Span<TScalar> destination)
        => ((ISpatialVector<Vector4<TScalar>, TScalar>)this).TryCopyTo(destination);

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
        => ((ISpatialVector<Vector4<TScalar>, TScalar>)this).TryFormat(destination, out charsWritten, format, provider);
}
