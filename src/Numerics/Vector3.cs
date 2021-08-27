using System.Diagnostics;
using System.Numerics;

namespace Tavenem.Mathematics;

/// <summary>
/// A structure encapsulating three values.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public readonly struct Vector3<TScalar> : ISpatialVector<Vector3<TScalar>, TScalar>
    where TScalar : IFloatingPoint<TScalar>
{
    /// <summary>
    /// The number of values represented by this vector.
    /// </summary>
#pragma warning disable RCS1158 // Static member in generic type should use a type parameter.
    public static int Count => 3;
#pragma warning restore RCS1158 // Static member in generic type should use a type parameter.

    /// <summary>
    /// Returns a vector with 1 in all positions.
    /// </summary>
    public static Vector3<TScalar> One => new() { X = TScalar.One, Y = TScalar.One, Z = TScalar.One };
    /// <summary>
    /// Returns a vector with 1 in all positions.
    /// </summary>
    public static Vector3<TScalar> MultiplicativeIdentity => One;

    /// <summary>
    /// Returns the vector (1,0,0).
    /// </summary>
    public static Vector3<TScalar> UnitX { get; } = new() { X = TScalar.One, Y = TScalar.Zero, Z = TScalar.Zero };

    /// <summary>
    /// Returns the vector (0,1,0).
    /// </summary>
    public static Vector3<TScalar> UnitY { get; } = new() { X = TScalar.Zero, Y = TScalar.One, Z = TScalar.Zero };

    /// <summary>
    /// Returns the vector (0,0,1).
    /// </summary>
    public static Vector3<TScalar> UnitZ { get; } = new() { X = TScalar.Zero, Y = TScalar.Zero, Z = TScalar.One };

    /// <summary>
    /// Returns a vector with 0 in all positions.
    /// </summary>
    public static Vector3<TScalar> Zero => new();
    /// <summary>
    /// Returns a vector with 0 in all positions.
    /// </summary>
    public static Vector3<TScalar> AdditiveIdentity => Zero;

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
            throw new IndexOutOfRangeException();
        }
    }

    /// <summary>
    /// Creates a new vector with the given values.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    /// <param name="z">The Z component.</param>
    public Vector3(TScalar x, TScalar y, TScalar z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    /// <summary>
    /// Creates a new vector with the given value for all components.
    /// </summary>
    /// <param name="value">The value to use for all components.</param>
    public static Vector3<TScalar> Create(TScalar value) => new() { X = value, Y = value, Z = value };

    /// <summary>
    /// Creates a new vector with the given values.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    /// <param name="z">The Z component.</param>
    public static Vector3<TScalar> Create(TScalar x, TScalar y, TScalar z) => new() { X = x, Y = y, Z = z };

    /// <summary>
    /// Creates a new vector with the given values.
    /// </summary>
    /// <param name="vector2">
    /// A <see cref="Vector2{TScalar}"/> representing the X and Y components.
    /// </param>
    /// <param name="z">The Z component.</param>
    public static Vector3<TScalar> Create(Vector2<TScalar> vector2, TScalar z)
        => new() { X = vector2.X, Y = vector2.Y, Z = z };

    /// <summary>
    /// Creates a new vector with the given values.
    /// </summary>
    /// <param name="values">The values to use for all components.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="values"/> does not contain enough elements
    /// </exception>
    public static Vector3<TScalar> Create(TScalar[] values)
    {
        if (values.Length < Count)
        {
            throw new ArgumentException($"{nameof(values)} does not contain enough elements", nameof(values));
        }
        return new() { X = values[0], Y = values[1], Z = values[2] };
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
    public static Vector3<TScalar> Create(TScalar[] values, int startIndex)
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
        return new() { X = values[startIndex], Y = values[startIndex + 1], Z = values[startIndex + 2] };
    }

    /// <summary>
    /// Creates a new vector with the given values.
    /// </summary>
    /// <param name="values">The values to use for all components.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="values"/> does not contain enough elements
    /// </exception>
    public static Vector3<TScalar> Create(ReadOnlySpan<TScalar> values)
    {
        if (values.Length < Count)
        {
            throw new ArgumentException($"{nameof(values)} does not contain enough elements", nameof(values));
        }
        return new() { X = values[0], Y = values[1], Z = values[2] };
    }

    /// <summary>
    /// Returns a vector whose elements are the absolute values of each of the source vector's elements.
    /// </summary>
    /// <param name="value">The source vector.</param>
    /// <returns>The absolute value vector.</returns>
    public static Vector3<TScalar> Abs(Vector3<TScalar> value)
        => VectorCommon.Abs<Vector3<TScalar>, TScalar>(value);

    /// <summary>
    /// Computes the angle between two vectors.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <returns>The angle between the vectors, in radians.</returns>
    public static TScalar Angle(Vector3<TScalar> value1, Vector3<TScalar> value2) => TScalar.Atan2(
        VectorCommon.Length<Vector3<TScalar>, TScalar>(Cross(value1, value2)),
        VectorCommon.Dot<Vector3<TScalar>, TScalar>(value1, value2));

    /// <summary>
    /// Determines if the given vectors are parallel.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <param name="allowSmallError">If <see langword="true"/>, a small amount of error is
    /// disregarded, to account for floating point errors.</param>
    /// <returns><see langword="true"/> if the vectors are parallel; otherwise <see
    /// langword="false"/>.</returns>
    public static bool AreParallel(Vector3<TScalar> value1, Vector3<TScalar> value2, bool allowSmallError = true)
    {
        var cross = Cross(value1, value2);
        return allowSmallError
            ? VectorCommon.IsNearlyZero<Vector3<TScalar>, TScalar>(cross)
            : cross == Zero;
    }

    /// <summary>
    /// Restricts a vector between a min and max value.
    /// </summary>
    /// <param name="value1">The source vector.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    public static Vector3<TScalar> Clamp(Vector3<TScalar> value1, Vector3<TScalar> min, Vector3<TScalar> max)
        => VectorCommon.Clamp<Vector3<TScalar>, TScalar>(value1, min, max);

    /// <summary>
    /// Computes the cross product of two vectors.
    /// </summary>
    /// <param name="vector1">The first vector.</param>
    /// <param name="vector2">The second vector.</param>
    /// <returns>The cross product.</returns>
    public static Vector3<TScalar> Cross(Vector3<TScalar> vector1, Vector3<TScalar> vector2) => new()
    {
        X = (vector1.Y * vector2.Z) - (vector1.Z * vector2.Y),
        Y = (vector1.Z * vector2.X) - (vector1.X * vector2.Z),
        Z = (vector1.X * vector2.Y) - (vector1.Y * vector2.X),
    };

    /// <summary>
    /// Returns the Euclidean distance between the two given points.
    /// </summary>
    /// <param name="value1">The first point.</param>
    /// <param name="value2">The second point.</param>
    /// <returns>The distance.</returns>
    public static TScalar Distance(Vector3<TScalar> value1, Vector3<TScalar> value2)
        => VectorCommon.Distance<Vector3<TScalar>, TScalar>(value1, value2);

    /// <summary>
    /// Returns the Euclidean distance squared between the two given points.
    /// </summary>
    /// <param name="value1">The first point.</param>
    /// <param name="value2">The second point.</param>
    /// <returns>The distance squared.</returns>
    public static TScalar DistanceSquared(Vector3<TScalar> value1, Vector3<TScalar> value2)
        => VectorCommon.DistanceSquared<Vector3<TScalar>, TScalar>(value1, value2);

    /// <summary>
    /// Returns the dot product of two vectors.
    /// </summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <returns>The dot product.</returns>
    public static TScalar Dot(Vector3<TScalar> left, Vector3<TScalar> right)
        => VectorCommon.Dot<Vector3<TScalar>, TScalar>(left, right);

    /// <summary>
    /// Determines if a vector is nearly zero (all elements closer to zero than <see
    /// cref="NumberValues.NearlyZeroDouble"/>).
    /// </summary>
    /// <param name="value">A vector to test.</param>
    /// <returns>
    /// <see langword="true"/> if the vector is close to zero; otherwise <see langword="false"/>.
    /// </returns>
    public static bool IsNearlyZero(Vector3<TScalar> value)
        => VectorCommon.IsNearlyZero<Vector3<TScalar>, TScalar>(value);

    /// <summary>
    /// Returns the length of the vector.
    /// </summary>
    /// <returns>The vector's length.</returns>
    public static TScalar Length(Vector3<TScalar> value)
        => VectorCommon.Length<Vector3<TScalar>, TScalar>(value);

    /// <summary>
    /// Returns the length of the vector squared.
    /// </summary>
    /// <returns>The vector's length squared.</returns>
    public static TScalar LengthSquared(Vector3<TScalar> value)
        => VectorCommon.LengthSquared<Vector3<TScalar>, TScalar>(value);

    /// <summary>
    /// Linearly interpolates between two vectors based on the given weighting.
    /// </summary>
    /// <param name="value1">The first source vector.</param>
    /// <param name="value2">The second source vector.</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of the second source
    /// vector.</param>
    /// <returns>The interpolated vector.</returns>
    public static Vector3<TScalar> Lerp(Vector3<TScalar> value1, Vector3<TScalar> value2, TScalar amount)
        => VectorCommon.Lerp<Vector3<TScalar>, TScalar>(value1, value2, amount);

    /// <summary>
    /// Returns a vector whose elements are the maximum of each of the pairs of elements in the
    /// two source vectors.
    /// </summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <returns>The maximized vector.</returns>
    public static Vector3<TScalar> Max(Vector3<TScalar> left, Vector3<TScalar> right)
        => VectorCommon.Max<Vector3<TScalar>, TScalar>(left, right);

    /// <summary>
    /// Returns a vector whose elements are the minimum of each of the pairs of elements in the
    /// two source vectors.
    /// </summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <returns>The minimized vector.</returns>
    public static Vector3<TScalar> Min(Vector3<TScalar> left, Vector3<TScalar> right)
        => VectorCommon.Min<Vector3<TScalar>, TScalar>(left, right);

    /// <summary>
    /// Returns a vector with the same direction as the given vector, but with a length of 1.
    /// </summary>
    /// <param name="value">The vector to normalize.</param>
    /// <returns>The normalized vector.</returns>
    public static Vector3<TScalar> Normalize(Vector3<TScalar> value)
        => VectorCommon.Normalize<Vector3<TScalar>, TScalar>(value);

    /// <summary>
    /// Returns the reflection of a vector off a surface that has the specified normal.
    /// </summary>
    /// <param name="vector">The source vector.</param>
    /// <param name="normal">The normal of the surface being reflected off.</param>
    /// <returns>The reflected vector.</returns>
    public static Vector3<TScalar> Reflect(Vector3<TScalar> vector, Vector3<TScalar> normal)
        => VectorCommon.Reflect<Vector3<TScalar>, TScalar>(vector, normal);

    /// <summary>
    /// Calculates the quaternion which represents the rotation from one vector to another.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    public static Quaternion<TScalar> RotationTo(Vector3<TScalar> value1, Vector3<TScalar> value2)
    {
        if (AreParallel(value1, value2))
        {
            if (value1 != value2)
            {
                var inter = AreParallel(value1, UnitX) ? UnitY : UnitX;
                return RotationTo(inter, value2) * RotationTo(value1, inter);
            }
            else
            {
                return Quaternion<TScalar>.MultiplicativeIdentity;
            }
        }
        else
        {
            return Quaternion<TScalar>.Normalize(
                Quaternion<TScalar>.Create(
                    Cross(value1, value2),
                    TScalar.Sqrt(VectorCommon.LengthSquared<Vector3<TScalar>, TScalar>(value1)
                    * VectorCommon.LengthSquared<Vector3<TScalar>, TScalar>(value2))
                    + VectorCommon.Dot<Vector3<TScalar>, TScalar>(value1, value2)));
        }
    }

    /// <summary>
    /// Returns a vector whose elements are the square root of each of the source vector's
    /// elements.
    /// </summary>
    /// <param name="value">The source vector.</param>
    /// <returns>The square root vector.</returns>
    public static Vector3<TScalar> SquareRoot(Vector3<TScalar> value)
        => VectorCommon.SquareRoot<Vector3<TScalar>, TScalar>(value);

    /// <summary>
    /// Transforms a vector by the given matrix.
    /// </summary>
    /// <param name="position">The source vector.</param>
    /// <param name="matrix">The transformation matrix.</param>
    /// <returns>The transformed vector.</returns>
    public static Vector3<TScalar> Transform(Vector3<TScalar> position, Matrix4x4<TScalar> matrix) => new()
    {
        X = (position.X * matrix.M11) + (position.Y * matrix.M21) + (position.Z * matrix.M31) + matrix.M41,
        Y = (position.X * matrix.M12) + (position.Y * matrix.M22) + (position.Z * matrix.M32) + matrix.M42,
        Z = (position.X * matrix.M13) + (position.Y * matrix.M23) + (position.Z * matrix.M33) + matrix.M43,
    };

    /// <summary>
    /// Transforms a vector by the given Quaternion rotation value.
    /// </summary>
    /// <param name="value">The source vector to be rotated.</param>
    /// <param name="rotation">The rotation to apply.</param>
    /// <returns>The transformed vector.</returns>
    public static Vector3<TScalar> Transform(Vector3<TScalar> value, Quaternion<TScalar> rotation)
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
        };
    }

    /// <summary>
    /// Transforms a vector normal by the given matrix.
    /// </summary>
    /// <param name="normal">The source vector.</param>
    /// <param name="matrix">The transformation matrix.</param>
    /// <returns>The transformed vector.</returns>
    public static Vector3<TScalar> TransformNormal(Vector3<TScalar> normal, Matrix4x4<TScalar> matrix) => new()
    {
        X = (normal.X * matrix.M11) + (normal.Y * matrix.M21) + (normal.Z * matrix.M31),
        Y = (normal.X * matrix.M12) + (normal.Y * matrix.M22) + (normal.Z * matrix.M32),
        Z = (normal.X * matrix.M13) + (normal.Y * matrix.M23) + (normal.Z * matrix.M33),
    };

    /// <summary>
    /// Adds two vectors together.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The summed vector.</returns>
    public static Vector3<TScalar> operator +(Vector3<TScalar> left, Vector3<TScalar> right) => new()
    {
        X = left.X + right.X,
        Y = left.Y + right.Y,
        Z = left.Z + right.Z,
    };

    /// <summary>
    /// Subtracts the second vector from the first.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The difference vector.</returns>
    public static Vector3<TScalar> operator -(Vector3<TScalar> left, Vector3<TScalar> right) => new()
    {
        X = left.X - right.X,
        Y = left.Y - right.Y,
        Z = left.Z - right.Z,
    };

    /// <summary>
    /// Multiplies two vectors together.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The product vector.</returns>
    public static Vector3<TScalar> operator *(Vector3<TScalar> left, Vector3<TScalar> right) => new()
    {
        X = left.X * right.X,
        Y = left.Y * right.Y,
        Z = left.Z * right.Z,
    };

    /// <summary>
    /// Multiplies a vector by the given scalar.
    /// </summary>
    /// <param name="left">The source vector.</param>
    /// <param name="right">The scalar value.</param>
    /// <returns>The scaled vector.</returns>
    public static Vector3<TScalar> operator *(Vector3<TScalar> left, TScalar right) => new()
    {
        X = left.X * right,
        Y = left.Y * right,
        Z = left.Z * right,
    };

    /// <summary>
    /// Multiplies a vector by the given scalar.
    /// </summary>
    /// <param name="left">The scalar value.</param>
    /// <param name="right">The source vector.</param>
    /// <returns>The scaled vector.</returns>
    public static Vector3<TScalar> operator *(TScalar left, Vector3<TScalar> right) => new()
    {
        X = left * right.X,
        Y = left * right.Y,
        Z = left * right.Z,
    };

    /// <summary>
    /// Divides the first vector by the second.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The vector resulting from the division.</returns>
    public static Vector3<TScalar> operator /(Vector3<TScalar> left, Vector3<TScalar> right) => new()
    {
        X = left.X / right.X,
        Y = left.Y / right.Y,
        Z = left.Z / right.Z,
    };

    /// <summary>
    /// Divides the vector by the given scalar.
    /// </summary>
    /// <param name="value1">The source vector.</param>
    /// <param name="value2">The scalar value.</param>
    /// <returns>The result of the division.</returns>
    public static Vector3<TScalar> operator /(Vector3<TScalar> value1, TScalar value2) => new()
    {
        X = value1.X / value2,
        Y = value1.Y / value2,
        Z = value1.Z / value2,
    };

    /// <summary>
    /// Negates a given vector.
    /// </summary>
    /// <param name="value">The source vector.</param>
    /// <returns>The negated vector.</returns>
    public static Vector3<TScalar> operator -(Vector3<TScalar> value) => new()
    {
        X = -value.X,
        Y = -value.Y,
        Z = -value.Z,
    };

    /// <summary>
    /// Returns a boolean indicating whether the two given vectors are equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns>True if the vectors are equal; False otherwise.</returns>
    public static bool operator ==(Vector3<TScalar> left, Vector3<TScalar> right)
        => left.X == right.X && left.Y == right.Y && left.Z == right.Z;

    /// <summary>
    /// Returns a boolean indicating whether the two given vectors are not equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns>True if the vectors are not equal; False if they are equal.</returns>
    public static bool operator !=(Vector3<TScalar> left, Vector3<TScalar> right)
        => left.X != right.X || left.Y != right.Y || left.Z != right.Z;

    /// <summary>
    /// Returns a boolean indicating whether the two given vectors are equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns>True if the vectors are equal; False otherwise.</returns>
    public static bool operator >(Vector3<TScalar> left, Vector3<TScalar> right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Returns a boolean indicating whether the two given vectors are equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns>True if the vectors are equal; False otherwise.</returns>
    public static bool operator >=(Vector3<TScalar> left, Vector3<TScalar> right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Returns a boolean indicating whether the two given vectors are not equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns>True if the vectors are not equal; False if they are equal.</returns>
    public static bool operator <(Vector3<TScalar> left, Vector3<TScalar> right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Returns a boolean indicating whether the two given vectors are not equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns>True if the vectors are not equal; False if they are equal.</returns>
    public static bool operator <=(Vector3<TScalar> left, Vector3<TScalar> right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Converts between implementations of vectors.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator Vector3<TScalar>(Vector3 value) => new()
    {
        X = TScalar.Create(value.X),
        Y = TScalar.Create(value.Y),
        Z = TScalar.Create(value.Z),
    };

    /// <summary>
    /// Converts between implementations of vectors.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static explicit operator Vector3(Vector3<TScalar> value) => new(
        value.X.Create<TScalar, float>(),
        value.Y.Create<TScalar, float>(),
        value.Z.Create<TScalar, float>());

    /// <summary>
    /// Returns a vector whose elements are the absolute values of each of this instance's
    /// elements.
    /// </summary>
    /// <returns>The absolute value vector.</returns>
    public Vector3<TScalar> Abs() => Abs(this);

    /// <summary>
    /// Returns the angle between this instance and the given vector.
    /// </summary>
    /// <param name="other">The other vector.</param>
    /// <returns>The angle between the vectors, in radians.</returns>
    public TScalar Angle(Vector3<TScalar> other) => Angle(this, other);

    /// <summary>
    /// Determines if this instance and the given vector are parallel.
    /// </summary>
    /// <param name="other">The other vector.</param>
    /// <param name="allowSmallError">If <see langword="true"/>, a small amount of error is
    /// disregarded, to account for floating point errors.</param>
    /// <returns><see langword="true"/> if the vectors are parallel; otherwise <see
    /// langword="false"/>.</returns>
    public bool AreParallel(Vector3<TScalar> other, bool allowSmallError = true)
        => AreParallel(this, other, allowSmallError);

    /// <summary>
    /// Restricts this instance between a min and max value.
    /// </summary>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    public Vector3<TScalar> Clamp(Vector3<TScalar> min, Vector3<TScalar> max)
        => Clamp(this, min, max);

    /// <summary>
    /// Computes the cross product of this instance and the given vector.
    /// </summary>
    /// <param name="other">The other vector.</param>
    /// <returns>The cross product.</returns>
    public Vector3<TScalar> Cross(Vector3<TScalar> other) => Cross(this, other);

    /// <summary>
    /// Compares the current instance with another object of the same type and returns an
    /// integer that indicates whether the current instance precedes, follows, or occurs in the
    /// same position in the sort order as the other object.
    /// </summary>
    /// <param name="other">An object to compare with this instance.</param>
    /// <returns>A value that indicates the relative order of the objects being
    /// compared.</returns>
    public int CompareTo(Vector3<TScalar> other)
    {
        if (Equals(other))
        {
            return 0;
        }
        if (VectorCommon.LengthSquared<Vector3<TScalar>, TScalar>(this)
            >= VectorCommon.LengthSquared<Vector3<TScalar>, TScalar>(other))
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
        if (obj is Vector3<TScalar> other)
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
        => ((ISpatialVector<Vector3<TScalar>, TScalar>)this).CopyTo(destination);

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
        => ((ISpatialVector<Vector3<TScalar>, TScalar>)this).CopyTo(destination, startIndex);

    /// <summary>
    /// Copies the values of this vector to the given span.
    /// </summary>
    /// <param name="destination">A <see cref="Span{T}"/> of <typeparamref name="TScalar"/>.</param>
    /// <exception cref="ArgumentException"></exception>
    public void CopyTo(Span<TScalar> destination)
        => ((ISpatialVector<Vector3<TScalar>, TScalar>)this).CopyTo(destination);

    /// <summary>
    /// Returns the Euclidean distance between this instance and another point.
    /// </summary>
    /// <param name="other">The other point.</param>
    /// <returns>The distance.</returns>
    public TScalar Distance(Vector3<TScalar> other) => Distance(this, other);

    /// <summary>
    /// Returns the Euclidean distance squared between this instance and another point.
    /// </summary>
    /// <param name="other">The other point.</param>
    /// <returns>The distance squared.</returns>
    public TScalar DistanceSquared(Vector3<TScalar> other) => DistanceSquared(this, other);

    /// <summary>
    /// Returns the dot product of this instance and another vector.
    /// </summary>
    /// <param name="other">The other vector.</param>
    /// <returns>The dot product.</returns>
    public TScalar Dot(Vector3<TScalar> other) => Dot(this, other);

    /// <summary>
    /// Returns a boolean indicating whether the given vector is equal to this instance.
    /// </summary>
    /// <param name="other">The vector to compare with this instance.</param>
    /// <returns>
    /// <see langword="true"/> if the other vector3 is equal to this instance; otherwise <see langword="false"/>.
    /// </returns>
    public bool Equals(Vector3<TScalar> other) => X == other.X && Y == other.Y && Z == other.Z;

    /// <summary>
    /// Returns a boolean indicating whether the given Object is equal to this instance.
    /// </summary>
    /// <param name="obj">The Object to compare against.</param>
    /// <returns>
    /// <see langword="true"/> if the Object is equal to this instance; otherwise <see langword="false"/>.
    /// </returns>
    public override bool Equals(object? obj) => obj is Vector3<TScalar> other && Equals(other);

    /// <summary>Returns the hash code for this instance.</summary>
    /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    public override int GetHashCode() => HashCode.Combine(X, Y, Z);

    /// <summary>
    /// Determines if this instance is nearly zero (all elements closer to zero than <see
    /// cref="NumberValues.NearlyZeroDouble"/>).
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if this instance is close to zero; otherwise <see langword="false"/>.
    /// </returns>
    public bool IsNearlyZero() => IsNearlyZero(this);

    /// <summary>
    /// Returns the length of this instance.
    /// </summary>
    /// <returns>This instance's length.</returns>
    public TScalar Length() => Length(this);

    /// <summary>
    /// Returns the length of this instance squared.
    /// </summary>
    /// <returns>This instance's length squared.</returns>
    public TScalar LengthSquared() => LengthSquared(this);

    /// <summary>
    /// Linearly interpolates between this instance and another vector based on the given weighting.
    /// </summary>
    /// <param name="other">The other source vector.</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of the second source
    /// vector.</param>
    /// <returns>The interpolated vector.</returns>
    public Vector3<TScalar> Lerp(Vector3<TScalar> other, TScalar amount)
        => Lerp(this, other, amount);

    /// <summary>
    /// Returns a vector whose elements are the maximum of each of the pairs of elements in the
    /// two source vectors.
    /// </summary>
    /// <param name="other">The other vector.</param>
    /// <returns>The maximized vector.</returns>
    public Vector3<TScalar> Max(Vector3<TScalar> other) => Max(this, other);

    /// <summary>
    /// Returns a vector whose elements are the minimum of each of the pairs of elements in the
    /// two source vectors.
    /// </summary>
    /// <param name="other">The other vector.</param>
    /// <returns>The minimized vector.</returns>
    public Vector3<TScalar> Min(Vector3<TScalar> other) => Min(this, other);

    /// <summary>
    /// Returns a vector with the same direction as this instance, but with a length of 1.
    /// </summary>
    /// <returns>The normalized vector.</returns>
    public Vector3<TScalar> Normalize() => Normalize(this);

    /// <summary>
    /// Returns the reflection of this instance off a surface that has the specified normal.
    /// </summary>
    /// <param name="normal">The normal of the surface being reflected off.</param>
    /// <returns>The reflected vector.</returns>
    public Vector3<TScalar> Reflect(Vector3<TScalar> normal) => Reflect(this, normal);

    /// <summary>
    /// Calculates the quaternion which represents the rotation from this instance to another vector.
    /// </summary>
    /// <param name="other">The other vector.</param>
    public Quaternion<TScalar> RotationTo(Vector3<TScalar> other) => RotationTo(this, other);

    /// <summary>
    /// Returns a vector whose elements are the square root of each of this instance's
    /// elements.
    /// </summary>
    /// <returns>The square root vector.</returns>
    public Vector3<TScalar> SquareRoot() => SquareRoot(this);

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
        => VectorCommon.ToString<Vector3<TScalar>, TScalar>(this, format, formatProvider);

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
    /// Transforms this instance by the given matrix.
    /// </summary>
    /// <param name="matrix">The transformation matrix.</param>
    /// <returns>The transformed vector.</returns>
    public Vector3<TScalar> Transform(Matrix4x4<TScalar> matrix) => Transform(this, matrix);

    /// <summary>
    /// Transforms this instance by the given Quaternion rotation value.
    /// </summary>
    /// <param name="rotation">The rotation to apply.</param>
    /// <returns>The transformed vector.</returns>
    public Vector3<TScalar> Transform(Quaternion<TScalar> rotation) => Transform(this, rotation);

    /// <summary>
    /// Transforms this instance as a normal vector by the given matrix.
    /// </summary>
    /// <param name="matrix">The transformation matrix.</param>
    /// <returns>The transformed vector.</returns>
    public Vector3<TScalar> TransformNormal(Matrix4x4<TScalar> matrix)
        => TransformNormal(this, matrix);

    /// <summary>
    /// Attempts to copy the values of this vector to the given span.
    /// </summary>
    /// <param name="destination">An array.</param>
    public bool TryCopyTo(Span<TScalar> destination)
        => ((ISpatialVector<Vector3<TScalar>, TScalar>)this).TryCopyTo(destination);

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
        => ((ISpatialVector<Vector3<TScalar>, TScalar>)this).TryFormat(destination, out charsWritten, format, provider);
}
