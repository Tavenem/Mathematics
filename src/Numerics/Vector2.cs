using System.Diagnostics;
using System.Numerics;

namespace Tavenem.Mathematics;

/// <summary>
/// A structure encapsulating two values.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public readonly struct Vector2<TScalar> : ISpatialVector<Vector2<TScalar>, TScalar>
    where TScalar : IFloatingPoint<TScalar>
{
    /// <summary>
    /// The number of values represented by this vector.
    /// </summary>
#pragma warning disable RCS1158 // Static member in generic type should use a type parameter.
    public static int Count => 2;
#pragma warning restore RCS1158 // Static member in generic type should use a type parameter.

    /// <summary>
    /// Returns a vector with 1 in all positions.
    /// </summary>
    public static Vector2<TScalar> One { get; } = new() { X = TScalar.One, Y = TScalar.One };
    /// <summary>
    /// Returns a vector with 1 in all positions.
    /// </summary>
    public static Vector2<TScalar> MultiplicativeIdentity => One;

    /// <summary>
    /// Returns the vector (1,0).
    /// </summary>
    public static Vector2<TScalar> UnitX { get; } = new() { X = TScalar.One, Y = TScalar.Zero };

    /// <summary>
    /// Returns the vector (0,1).
    /// </summary>
    public static Vector2<TScalar> UnitY { get; } = new() { X = TScalar.Zero, Y = TScalar.One };

    /// <summary>
    /// Returns a vector with 0 in all positions.
    /// </summary>
    public static Vector2<TScalar> Zero { get; } = new();
    /// <summary>
    /// Returns a vector with 0 in all positions.
    /// </summary>
    public static Vector2<TScalar> AdditiveIdentity => Zero;

    /// <summary>
    /// The X component of the vector.
    /// </summary>
    public TScalar X { get; init; }

    /// <summary>
    /// The Y component of the vector.
    /// </summary>
    public TScalar Y { get; init; }

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
            throw new IndexOutOfRangeException();
        }
    }

    /// <summary>
    /// Creates a new vector with the given values.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    public Vector2(TScalar x, TScalar y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Creates a new vector with the given value for all components.
    /// </summary>
    /// <param name="value">The value to use for all components.</param>
    public static Vector2<TScalar> Create(TScalar value) => new() { X = value, Y = value };

    /// <summary>
    /// Creates a new vector with the given values.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    public static Vector2<TScalar> Create(TScalar x, TScalar y) => new() { X = x, Y = y };

    /// <summary>
    /// Creates a new vector with the given values.
    /// </summary>
    /// <param name="values">The values to use for all components.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="values"/> does not contain enough elements
    /// </exception>
    public static Vector2<TScalar> Create(TScalar[] values)
    {
        if (values.Length < Count)
        {
            throw new ArgumentException($"{nameof(values)} does not contain enough elements", nameof(values));
        }
        return new() { X = values[0], Y = values[1] };
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
    public static Vector2<TScalar> Create(TScalar[] values, int startIndex)
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
        return new() { X = values[startIndex], Y = values[startIndex + 1] };
    }

    /// <summary>
    /// Creates a new vector with the given values.
    /// </summary>
    /// <param name="values">The values to use for all components.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="values"/> does not contain enough elements
    /// </exception>
    public static Vector2<TScalar> Create(ReadOnlySpan<TScalar> values)
    {
        if (values.Length < Count)
        {
            throw new ArgumentException($"{nameof(values)} does not contain enough elements", nameof(values));
        }
        return new() { X = values[0], Y = values[1] };
    }

    /// <summary>
    /// Returns a vector whose elements are the absolute values of each of the source vector's
    /// elements.
    /// </summary>
    /// <param name="value">The source vector.</param>
    /// <returns>The absolute value vector.</returns>
    public static Vector2<TScalar> Abs(Vector2<TScalar> value)
        => VectorCommon.Abs<Vector2<TScalar>, TScalar>(value);

    /// <summary>
    /// Returns the angle between the given vectors.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <returns>The angle between the vectors, in radians.</returns>
    public static TScalar Angle(Vector2<TScalar> value1, Vector2<TScalar> value2) => TScalar.Atan2(
        (value1.X * value2.Y) - (value1.Y * value2.X),
        VectorCommon.Dot<Vector2<TScalar>, TScalar>(value1, value2));

    /// <summary>
    /// Determines if the given vectors are parallel.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <param name="allowSmallError">If <see langword="true"/>, a small amount of error is
    /// disregarded, to account for floating point errors.</param>
    /// <returns><see langword="true"/> if the vectors are parallel; otherwise <see
    /// langword="false"/>.</returns>
    public static bool AreParallel(Vector2<TScalar> value1, Vector2<TScalar> value2, bool allowSmallError = true)
    {
        var cross = Vector3<TScalar>.Cross(
            Vector3<TScalar>.Create(value1, TScalar.Zero),
            Vector3<TScalar>.Create(value2, TScalar.Zero));
        return allowSmallError
            ? VectorCommon.IsNearlyZero<Vector3<TScalar>, TScalar>(cross)
            : cross == Vector3<TScalar>.Zero;
    }

    /// <summary>
    /// Restricts a vector between a min and max value.
    /// </summary>
    /// <param name="value1">The source vector.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    public static Vector2<TScalar> Clamp(Vector2<TScalar> value1, Vector2<TScalar> min, Vector2<TScalar> max)
        => VectorCommon.Clamp<Vector2<TScalar>, TScalar>(value1, min, max);

    /// <summary>
    /// Returns the Euclidean distance between the two given points.
    /// </summary>
    /// <param name="value1">The first point.</param>
    /// <param name="value2">The second point.</param>
    /// <returns>The distance.</returns>
    public static TScalar Distance(Vector2<TScalar> value1, Vector2<TScalar> value2)
        => VectorCommon.Distance<Vector2<TScalar>, TScalar>(value1, value2);

    /// <summary>
    /// Returns the Euclidean distance squared between the two given points.
    /// </summary>
    /// <param name="value1">The first point.</param>
    /// <param name="value2">The second point.</param>
    /// <returns>The distance squared.</returns>
    public static TScalar DistanceSquared(Vector2<TScalar> value1, Vector2<TScalar> value2)
        => VectorCommon.DistanceSquared<Vector2<TScalar>, TScalar>(value1, value2);

    /// <summary>
    /// Returns the dot product of two vectors.
    /// </summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <returns>The dot product.</returns>
    public static TScalar Dot(Vector2<TScalar> left, Vector2<TScalar> right)
        => VectorCommon.Dot<Vector2<TScalar>, TScalar>(left, right);

    /// <summary>
    /// Determines if a vector is nearly zero (all elements closer to zero than <see
    /// cref="NumberValues.NearlyZeroDouble"/>).
    /// </summary>
    /// <param name="value">A vector to test.</param>
    /// <returns>
    /// <see langword="true"/> if the vector is close to zero; otherwise <see langword="false"/>.
    /// </returns>
    public static bool IsNearlyZero(Vector2<TScalar> value)
        => VectorCommon.IsNearlyZero<Vector2<TScalar>, TScalar>(value);

    /// <summary>
    /// Returns the length of the vector.
    /// </summary>
    /// <returns>The vector's length.</returns>
    public static TScalar Length(Vector2<TScalar> value)
        => VectorCommon.Length<Vector2<TScalar>, TScalar>(value);

    /// <summary>
    /// Returns the length of the vector squared.
    /// </summary>
    /// <returns>The vector's length squared.</returns>
    public static TScalar LengthSquared(Vector2<TScalar> value)
        => VectorCommon.LengthSquared<Vector2<TScalar>, TScalar>(value);

    /// <summary>
    /// Linearly interpolates between two vectors based on the given weighting.
    /// </summary>
    /// <param name="value1">The first source vector.</param>
    /// <param name="value2">The second source vector.</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of the second source
    /// vector.</param>
    /// <returns>The interpolated vector.</returns>
    public static Vector2<TScalar> Lerp(Vector2<TScalar> value1, Vector2<TScalar> value2, TScalar amount)
        => VectorCommon.Lerp<Vector2<TScalar>, TScalar>(value1, value2, amount);

    /// <summary>
    /// Returns a vector whose elements are the maximum of each of the pairs of elements in the
    /// two source vectors
    /// </summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <returns>The maximized vector</returns>
    public static Vector2<TScalar> Max(Vector2<TScalar> left, Vector2<TScalar> right)
        => VectorCommon.Max<Vector2<TScalar>, TScalar>(left, right);

    /// <summary>
    /// Returns a vector whose elements are the minimum of each of the pairs of elements in the
    /// two source vectors.
    /// </summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <returns>The minimized vector.</returns>
    public static Vector2<TScalar> Min(Vector2<TScalar> left, Vector2<TScalar> right)
        => VectorCommon.Min<Vector2<TScalar>, TScalar>(left, right);

    /// <summary>
    /// Returns a vector with the same direction as the given vector, but with a length of 1.
    /// </summary>
    /// <param name="value">The vector to normalize.</param>
    /// <returns>The normalized vector.</returns>
    public static Vector2<TScalar> Normalize(Vector2<TScalar> value)
        => VectorCommon.Normalize<Vector2<TScalar>, TScalar>(value);

    /// <summary>
    /// Returns the reflection of a vector off a surface that has the specified normal.
    /// </summary>
    /// <param name="vector">The source vector.</param>
    /// <param name="normal">The normal of the surface being reflected off.</param>
    /// <returns>The reflected vector.</returns>
    public static Vector2<TScalar> Reflect(Vector2<TScalar> vector, Vector2<TScalar> normal)
        => VectorCommon.Reflect<Vector2<TScalar>, TScalar>(vector, normal);

    /// <summary>
    /// Returns the result of rotating the given vector by the given angle.
    /// </summary>
    /// <param name="vector">A vector.</param>
    /// <param name="angle">An angle, in radians.</param>
    public static Vector2<TScalar> Rotate(Vector2<TScalar> vector, TScalar angle)
    {
        var sin = TScalar.Sin(angle);
        var cos = TScalar.Cos(angle);
        return new()
        {
            X = (vector.X * cos) + (vector.Y * -sin),
            Y = (vector.X * sin) + (vector.Y * cos),
        };
    }

    /// <summary>
    /// Returns a vector whose elements are the square root of each of the source vector's
    /// elements.
    /// </summary>
    /// <param name="value">The source vector.</param>
    /// <returns>The square root vector.</returns>
    public static Vector2<TScalar> SquareRoot(Vector2<TScalar> value)
        => VectorCommon.SquareRoot<Vector2<TScalar>, TScalar>(value);

    /// <summary>
    /// Transforms a vector by the given matrix.
    /// </summary>
    /// <param name="position">The source vector.</param>
    /// <param name="matrix">The transformation matrix.</param>
    /// <returns>The transformed vector.</returns>
    public static Vector2<TScalar> Transform(Vector2<TScalar> position, Matrix3x2<TScalar> matrix) => new()
    {
        X = (position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M31,
        Y = (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M32,
    };

    /// <summary>
    /// Transforms a vector by the given matrix.
    /// </summary>
    /// <param name="position">The source vector.</param>
    /// <param name="matrix">The transformation matrix.</param>
    /// <returns>The transformed vector.</returns>
    public static Vector2<TScalar> Transform(Vector2<TScalar> position, Matrix4x4<TScalar> matrix) => new()
    {
        X = (position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41,
        Y = (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42,
    };

    /// <summary>
    /// Transforms a vector normal by the given matrix.
    /// </summary>
    /// <param name="normal">The source vector.</param>
    /// <param name="matrix">The transformation matrix.</param>
    /// <returns>The transformed vector.</returns>
    public static Vector2<TScalar> TransformNormal(Vector2<TScalar> normal, Matrix3x2<TScalar> matrix) => new()
    {
        X = (normal.X * matrix.M11) + (normal.Y * matrix.M21),
        Y = (normal.X * matrix.M12) + (normal.Y * matrix.M22),
    };

    /// <summary>
    /// Transforms a vector normal by the given matrix.
    /// </summary>
    /// <param name="normal">The source vector.</param>
    /// <param name="matrix">The transformation matrix.</param>
    /// <returns>The transformed vector.</returns>
    public static Vector2<TScalar> TransformNormal(Vector2<TScalar> normal, Matrix4x4<TScalar> matrix) => new()
    {
        X = (normal.X * matrix.M11) + (normal.Y * matrix.M21),
        Y = (normal.X * matrix.M12) + (normal.Y * matrix.M22),
    };

    /// <summary>
    /// Transforms a vector by the given Quaternion rotation value.
    /// </summary>
    /// <param name="value">The source vector to be rotated.</param>
    /// <param name="rotation">The rotation to apply.</param>
    /// <returns>The transformed vector.</returns>
    public static Vector2<TScalar> Transform(Vector2<TScalar> value, Quaternion<TScalar> rotation)
    {
        var x2 = rotation.X + rotation.X;
        var y2 = rotation.Y + rotation.Y;
        var z2 = rotation.Z + rotation.Z;

        var wz2 = rotation.W * z2;
        var xx2 = rotation.X * x2;
        var xy2 = rotation.X * y2;
        var yy2 = rotation.Y * y2;
        var zz2 = rotation.Z * z2;

        return new()
        {
            X = (value.X * (TScalar.One - yy2 - zz2)) + (value.Y * (xy2 - wz2)),
            Y = (value.X * (xy2 + wz2)) + (value.Y * (TScalar.One - xx2 - zz2)),
        };
    }

    /// <summary>
    /// Adds two vectors together.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The summed vector.</returns>
    public static Vector2<TScalar> operator +(Vector2<TScalar> left, Vector2<TScalar> right)
        => new() { X = left.X + right.X, Y = left.Y + right.Y };

    /// <summary>
    /// Subtracts the second vector from the first.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The difference vector.</returns>
    public static Vector2<TScalar> operator -(Vector2<TScalar> left, Vector2<TScalar> right)
        => new() { X = left.X - right.X, Y = left.Y - right.Y };

    /// <summary>
    /// Multiplies two vectors together.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The product vector.</returns>
    public static Vector2<TScalar> operator *(Vector2<TScalar> left, Vector2<TScalar> right)
        => new() { X = left.X * right.X, Y = left.Y * right.Y };

    /// <summary>
    /// Multiplies a vector by the given scalar.
    /// </summary>
    /// <param name="left">The scalar value.</param>
    /// <param name="right">The source vector.</param>
    /// <returns>The scaled vector.</returns>
    public static Vector2<TScalar> operator *(TScalar left, Vector2<TScalar> right)
        => new() { X = left * right.X, Y = left * right.Y };

    /// <summary>
    /// Multiplies a vector by the given scalar.
    /// </summary>
    /// <param name="left">The source vector.</param>
    /// <param name="right">The scalar value.</param>
    /// <returns>The scaled vector.</returns>
    public static Vector2<TScalar> operator *(Vector2<TScalar> left, TScalar right)
        => new() { X = left.X * right, Y = left.Y * right };

    /// <summary>
    /// Divides the first vector by the second.
    /// </summary>
    /// <param name="left">The first source vector.</param>
    /// <param name="right">The second source vector.</param>
    /// <returns>The vector resulting from the division.</returns>
    public static Vector2<TScalar> operator /(Vector2<TScalar> left, Vector2<TScalar> right)
        => new() { X = left.X / right.X, Y = left.Y / right.Y };

    /// <summary>
    /// Divides the vector by the given scalar.
    /// </summary>
    /// <param name="value1">The source vector.</param>
    /// <param name="value2">The scalar value.</param>
    /// <returns>The result of the division.</returns>
    public static Vector2<TScalar> operator /(Vector2<TScalar> value1, TScalar value2)
        => new() { X = value1.X / value2, Y = value1.Y / value2 };

    /// <summary>
    /// Negates a given vector.
    /// </summary>
    /// <param name="value">The source vector.</param>
    /// <returns>The negated vector.</returns>
    public static Vector2<TScalar> operator -(Vector2<TScalar> value)
        => new() { X = -value.X, Y = -value.Y };

    /// <summary>
    /// Returns a boolean indicating whether the two given vectors are equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns>True if the vectors are equal; False otherwise.</returns>
    public static bool operator ==(Vector2<TScalar> left, Vector2<TScalar> right)
        => left.Equals(right);

    /// <summary>
    /// Returns a boolean indicating whether the two given vectors are not equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns>True if the vectors are not equal; False if they are equal.</returns>
    public static bool operator !=(Vector2<TScalar> left, Vector2<TScalar> right)
        => !left.Equals(right);

    /// <summary>
    /// Returns a boolean indicating whether the two given vectors are equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns>True if the vectors are equal; False otherwise.</returns>
    public static bool operator >(Vector2<TScalar> left, Vector2<TScalar> right)
        => left.CompareTo(right) > 0;

    /// <summary>
    /// Returns a boolean indicating whether the two given vectors are equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns>True if the vectors are equal; False otherwise.</returns>
    public static bool operator >=(Vector2<TScalar> left, Vector2<TScalar> right)
        => left.CompareTo(right) >= 0;

    /// <summary>
    /// Returns a boolean indicating whether the two given vectors are not equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns>True if the vectors are not equal; False if they are equal.</returns>
    public static bool operator <(Vector2<TScalar> left, Vector2<TScalar> right)
        => left.CompareTo(right) < 0;

    /// <summary>
    /// Returns a boolean indicating whether the two given vectors are not equal.
    /// </summary>
    /// <param name="left">The first vector to compare.</param>
    /// <param name="right">The second vector to compare.</param>
    /// <returns>True if the vectors are not equal; False if they are equal.</returns>
    public static bool operator <=(Vector2<TScalar> left, Vector2<TScalar> right)
        => left.CompareTo(right) <= 0;

    /// <summary>
    /// Converts between implementations of vectors.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator Vector2<TScalar>(Vector2 value) => new()
    {
        X = TScalar.Create(value.X),
        Y = TScalar.Create(value.Y),
    };

    /// <summary>
    /// Converts between implementations of vectors.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static explicit operator Vector2(Vector2<TScalar> value) => new(
        Extensions.TypeConvert<float, TScalar>(value.X),
        Extensions.TypeConvert<float, TScalar>(value.Y));

    /// <summary>
    /// Compares the current instance with another object of the same type and returns an
    /// integer that indicates whether the current instance precedes, follows, or occurs in the
    /// same position in the sort order as the other object.
    /// </summary>
    /// <param name="other">An object to compare with this instance.</param>
    /// <returns>
    /// A value that indicates the relative order of the objects being compared.
    /// </returns>
    public int CompareTo(Vector2<TScalar> other)
    {
        if (Equals(other))
        {
            return 0;
        }
        if (VectorCommon.LengthSquared<Vector2<TScalar>, TScalar>(this)
            >= VectorCommon.LengthSquared<Vector2<TScalar>, TScalar>(other))
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
    /// <exception cref="ArgumentException">
    /// <paramref name="obj" /> is not the same type as this instance.
    /// </exception>
    public int CompareTo(object? obj)
    {
        if (obj is Vector2<TScalar> other)
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
        => ((ISpatialVector<Vector2<TScalar>, TScalar>)this).CopyTo(destination);

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
        => ((ISpatialVector<Vector2<TScalar>, TScalar>)this).CopyTo(destination, startIndex);

    /// <summary>
    /// Copies the values of this vector to the given span.
    /// </summary>
    /// <param name="destination">A <see cref="Span{T}"/> of <typeparamref name="TScalar"/>.</param>
    /// <exception cref="ArgumentException"></exception>
    public void CopyTo(Span<TScalar> destination)
        => ((ISpatialVector<Vector2<TScalar>, TScalar>)this).CopyTo(destination);

    /// <summary>
    /// Returns a boolean indicating whether the given vector is equal to this instance.
    /// </summary>
    /// <param name="other">The vector to compare with this instance.</param>
    /// <returns>
    /// <see langword="true"/> if the other vector is equal to this instance; otherwise <see langword="false"/>.
    /// </returns>
    public bool Equals(Vector2<TScalar> other) => X == other.X && Y == other.Y;

    /// <summary>
    /// Returns a boolean indicating whether the given Object is equal to this Vector2 instance.
    /// </summary>
    /// <param name="obj">The Object to compare against.</param>
    /// <returns>
    /// <see langword="true"/> if the other Object is equal to this instance; otherwise <see langword="false"/>.
    /// </returns>
    public override bool Equals(object? obj) => obj is Vector2<TScalar> other && Equals(other);

    /// <summary>Returns the hash code for this instance.</summary>
    /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    public override int GetHashCode() => HashCode.Combine(X, Y);

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
        => ((ISpatialVector<Vector2<TScalar>, TScalar>)this).ToString(format, formatProvider);

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
        => ((ISpatialVector<Vector2<TScalar>, TScalar>)this).TryCopyTo(destination);

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
        => ((ISpatialVector<Vector2<TScalar>, TScalar>)this).TryFormat(destination, out charsWritten, format, provider);
}
