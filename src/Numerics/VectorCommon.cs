using System.Globalization;
using System.Numerics;
using System.Text;

namespace Tavenem.Mathematics;

internal static class VectorCommon
{
    /// <summary>
    /// Returns a vector whose elements are the absolute values of each of the source vector's
    /// elements.
    /// </summary>
    /// <param name="value">The source vector.</param>
    /// <returns>The absolute value vector.</returns>
    public static TSelf Abs<TSelf, TScalar>(TSelf value)
        where TSelf : ISpatialVector<TSelf, TScalar>
        where TScalar : IFloatingPointIeee754<TScalar>
    {
        var elements = new TScalar[TSelf.Count];
        for (var i = 0; i < TSelf.Count; i++)
        {
            elements[i] = TScalar.Abs(value[i]);
        }
        return TSelf.Create(elements);
    }

    /// <summary>
    /// Restricts a vector between a min and max value.
    /// </summary>
    /// <param name="value1">The source vector.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    public static TSelf Clamp<TSelf, TScalar>(TSelf value1, TSelf min, TSelf max)
        where TSelf : ISpatialVector<TSelf, TScalar>
        where TScalar : IFloatingPointIeee754<TScalar>
    {
        var elements = new TScalar[TSelf.Count];
        for (var i = 0; i < TSelf.Count; i++)
        {
            var v = (min[i] > value1[i]) ? min[i] : value1[i];
            v = (max[i] < v) ? max[i] : v;
            elements[i] = v;
        }
        return TSelf.Create(elements);
    }

    /// <summary>
    /// Returns the Euclidean distance between the two given points.
    /// </summary>
    /// <param name="value1">The first point.</param>
    /// <param name="value2">The second point.</param>
    /// <returns>The distance.</returns>
    public static TScalar Distance<TSelf, TScalar>(TSelf value1, TSelf value2)
        where TSelf : ISpatialVector<TSelf, TScalar>
        where TScalar : IFloatingPointIeee754<TScalar>
        => TScalar.Sqrt(DistanceSquared<TSelf, TScalar>(value1, value2));

    /// <summary>
    /// Returns the Euclidean distance squared between the two given points.
    /// </summary>
    /// <param name="value1">The first point.</param>
    /// <param name="value2">The second point.</param>
    /// <returns>The distance squared.</returns>
    public static TScalar DistanceSquared<TSelf, TScalar>(TSelf value1, TSelf value2)
        where TSelf : ISpatialVector<TSelf, TScalar>
        where TScalar : IFloatingPointIeee754<TScalar>
    {
        var sum = TScalar.Zero;
        for (var i = 0; i < TSelf.Count; i++)
        {
            sum += (value1[i] - value2[i]).Square();
        }
        return sum;
    }

    /// <summary>
    /// Returns the dot product of two vectors.
    /// </summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <returns>The dot product.</returns>
    public static TScalar Dot<TSelf, TScalar>(TSelf left, TSelf right)
        where TSelf : ISpatialVector<TSelf, TScalar>
        where TScalar : IFloatingPointIeee754<TScalar>
    {
        var sum = TScalar.Zero;
        for (var i = 0; i < TSelf.Count; i++)
        {
            sum += left[i] * right[i];
        }
        return sum;
    }

    /// <summary>
    /// Determines if a vector is nearly zero (all elements closer to zero than <see
    /// cref="NumberValues.NearlyZeroDouble"/>).
    /// </summary>
    /// <param name="value">A vector to test.</param>
    /// <returns>
    /// <see langword="true"/> if the vector is close to zero; otherwise <see langword="false"/>.
    /// </returns>
    public static bool IsNearlyZero<TSelf, TScalar>(TSelf value)
        where TSelf : ISpatialVector<TSelf, TScalar>
        where TScalar : IFloatingPointIeee754<TScalar>
    {
        for (var i = 0; i < TSelf.Count; i++)
        {
            if (!value[i].IsNearlyZero())
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Returns the length of the vector.
    /// </summary>
    /// <returns>The vector's length.</returns>
    public static TScalar Length<TSelf, TScalar>(TSelf value)
        where TSelf : ISpatialVector<TSelf, TScalar>
        where TScalar : IFloatingPointIeee754<TScalar> => TScalar.Sqrt(LengthSquared<TSelf, TScalar>(value));

    /// <summary>
    /// Returns the length of the vector squared.
    /// </summary>
    /// <returns>The vector's length squared.</returns>
    public static TScalar LengthSquared<TSelf, TScalar>(TSelf value)
        where TSelf : ISpatialVector<TSelf, TScalar>
        where TScalar : IFloatingPointIeee754<TScalar> => Dot<TSelf, TScalar>(value, value);

    /// <summary>
    /// Linearly interpolates between two vectors based on the given weighting.
    /// </summary>
    /// <param name="value1">The first source vector.</param>
    /// <param name="value2">The second source vector.</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of the second source
    /// vector.</param>
    /// <returns>The interpolated vector.</returns>
    public static TSelf Lerp<TSelf, TScalar>(TSelf value1, TSelf value2, TScalar amount)
        where TSelf : ISpatialVector<TSelf, TScalar>
        where TScalar : IFloatingPointIeee754<TScalar>
    {
        var elements = new TScalar[TSelf.Count];
        for (var i = 0; i < TSelf.Count; i++)
        {
            elements[i] = value1[i].Lerp(value2[i], amount);
        }
        return TSelf.Create(elements);
    }

    /// <summary>
    /// Returns a vector whose elements are the maximum of each of the pairs of elements in the
    /// two source vectors
    /// </summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <returns>The maximized vector</returns>
    public static TSelf Max<TSelf, TScalar>(TSelf left, TSelf right)
        where TSelf : ISpatialVector<TSelf, TScalar>
        where TScalar : IFloatingPointIeee754<TScalar>
    {
        var elements = new TScalar[TSelf.Count];
        for (var i = 0; i < TSelf.Count; i++)
        {
            elements[i] = TScalar.Max(left[i], right[i]);
        }
        return TSelf.Create(elements);
    }

    /// <summary>
    /// Returns a vector whose elements are the minimum of each of the pairs of elements in the
    /// two source vectors.
    /// </summary>
    /// <param name="left">The first vector.</param>
    /// <param name="right">The second vector.</param>
    /// <returns>The minimized vector.</returns>
    public static TSelf Min<TSelf, TScalar>(TSelf left, TSelf right)
        where TSelf : ISpatialVector<TSelf, TScalar>
        where TScalar : IFloatingPointIeee754<TScalar>
    {
        var elements = new TScalar[TSelf.Count];
        for (var i = 0; i < TSelf.Count; i++)
        {
            elements[i] = TScalar.Min(left[i], right[i]);
        }
        return TSelf.Create(elements);
    }

    /// <summary>
    /// Returns a vector with the same direction as the given vector, but with a length of 1.
    /// </summary>
    /// <param name="value">The vector to normalize.</param>
    /// <returns>The normalized vector.</returns>
    public static TSelf Normalize<TSelf, TScalar>(TSelf value)
        where TSelf : ISpatialVector<TSelf, TScalar>
        where TScalar : IFloatingPointIeee754<TScalar>
    {
        var ls = TSelf.Dot(value, value);
        var norm = TScalar.Sqrt(ls);

        var elements = new TScalar[TSelf.Count];
        for (var i = 0; i < TSelf.Count; i++)
        {
            elements[i] = value[i] / norm;
        }
        return TSelf.Create(elements);
    }

    /// <summary>
    /// Returns the reflection of a vector off a surface that has the specified normal.
    /// </summary>
    /// <param name="vector">The source vector.</param>
    /// <param name="normal">The normal of the surface being reflected off.</param>
    /// <returns>The reflected vector.</returns>
    public static TSelf Reflect<TSelf, TScalar>(TSelf vector, TSelf normal)
        where TSelf : ISpatialVector<TSelf, TScalar>
        where TScalar : IFloatingPointIeee754<TScalar>
    {
        var dot = Dot<TSelf, TScalar>(vector, normal);

        var two = TScalar.One + TScalar.One;
        var elements = new TScalar[TSelf.Count];
        for (var i = 0; i < TSelf.Count; i++)
        {
            elements[i] = vector[i] - (two * dot * normal[i]);
        }
        return TSelf.Create(elements);
    }

    /// <summary>
    /// Returns a vector whose elements are the square root of each of the source vector's
    /// elements.
    /// </summary>
    /// <param name="value">The source vector.</param>
    /// <returns>The square root vector.</returns>
    public static TSelf SquareRoot<TSelf, TScalar>(TSelf value)
        where TSelf : ISpatialVector<TSelf, TScalar>
        where TScalar : IFloatingPointIeee754<TScalar>
    {
        var elements = new TScalar[TSelf.Count];
        for (var i = 0; i < TSelf.Count; i++)
        {
            elements[i] = TScalar.Sqrt(value[i]);
        }
        return TSelf.Create(elements);
    }

    /// <summary>
    /// Copies the values of the given vector to the given array.
    /// </summary>
    /// <param name="value">The vector</param>
    /// <param name="destination">An array.</param>
    /// <exception cref="ArgumentException">
    /// <paramref name="destination"/> is not long enough
    /// </exception>
    public static void CopyTo<TSelf, TScalar>(TSelf value, TScalar[] destination)
        where TSelf : ISpatialVector<TSelf, TScalar>
        where TScalar : IFloatingPointIeee754<TScalar>
    {
        if (destination.Length < TSelf.Count)
        {
            throw new ArgumentException($"{nameof(destination)} is not long enough.", nameof(destination));
        }
        for (var i = 0; i < TSelf.Count; i++)
        {
            destination[i] = value[i];
        }
    }

    /// <summary>
    /// Copies the values of the given vector to the given array, starting at the given position.
    /// </summary>
    /// <param name="value">The vector</param>
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
    public static void CopyTo<TSelf, TScalar>(TSelf value, TScalar[] destination, int startIndex)
        where TSelf : ISpatialVector<TSelf, TScalar>
        where TScalar : IFloatingPointIeee754<TScalar>
    {
        if (destination.Length < TSelf.Count)
        {
            throw new ArgumentException($"{nameof(destination)} is not long enough.", nameof(destination));
        }
        if (destination.Length < startIndex + TSelf.Count)
        {
            throw new ArgumentOutOfRangeException(
                nameof(startIndex),
                $"{nameof(startIndex)} would cause elements to be written beyond the length of {nameof(destination)}.");
        }
        for (var i = 0; i < TSelf.Count; i++)
        {
            destination[startIndex + i] = value[i];
        }
    }

    /// <summary>
    /// Copies the values of the given vector to the given span.
    /// </summary>
    /// <param name="value">The vector</param>
    /// <param name="destination">A <see cref="Span{T}"/> of <typeparamref name="TScalar"/>.</param>
    /// <exception cref="ArgumentException"></exception>
    public static void CopyTo<TSelf, TScalar>(TSelf value, Span<TScalar> destination)
        where TSelf : ISpatialVector<TSelf, TScalar>
        where TScalar : IFloatingPointIeee754<TScalar>
    {
        if (!TryCopyTo(value, destination))
        {
            throw new ArgumentException(
                $"{nameof(destination)} is not large enough to contain the values of this vector.",
                nameof(destination));
        }
    }

    /// <summary>
    /// Returns a <see cref="string"/> representing the given vector, using the specified
    /// <paramref name="format"/> to format individual elements and the given <see cref="IFormatProvider"/>.
    /// </summary>
    /// <param name="value">The vector</param>
    /// <param name="format">The format of individual elements.</param>
    /// <param name="formatProvider">
    /// The format provider to use when formatting elements.
    /// </param>
    /// <returns>The <see cref="string"/> representation.</returns>
    public static string ToString<TSelf, TScalar>(TSelf value, string? format, IFormatProvider? formatProvider)
        where TSelf : ISpatialVector<TSelf, TScalar>
        where TScalar : IFloatingPointIeee754<TScalar>
    {
        var sb = new StringBuilder()
          .Append('<');
        for (var i = 0; i < TSelf.Count; i++)
        {
            sb.Append(value[i].ToString(format, formatProvider));
            if (i < TSelf.Count - 1)
            {
                sb.Append(NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator)
                    .Append(' ');
            }
        }
        return sb.Append('>')
            .ToString();
    }

    /// <summary>
    /// Attempts to copy the values of the given vector to the given span.
    /// </summary>
    /// <param name="value">The vector</param>
    /// <param name="destination">An array.</param>
    public static bool TryCopyTo<TSelf, TScalar>(TSelf value, Span<TScalar> destination)
        where TSelf : ISpatialVector<TSelf, TScalar>
        where TScalar : IFloatingPointIeee754<TScalar>
    {
        if (destination.Length < TSelf.Count)
        {
            return false;
        }
        for (var i = 0; i < TSelf.Count; i++)
        {
            destination[i] = value[i];
        }
        return true;
    }

    /// <summary>
    /// Attempts to write the given vector to the given <see cref="Span{T}"/>.
    /// </summary>
    /// <param name="value">The vector</param>
    /// <param name="destination">The <see cref="Span{T}"/> to write to.</param>
    /// <param name="charsWritten">
    /// When this method returns, this will contains the number of characters written to <paramref name="destination"/>.
    /// </param>
    /// <param name="format">A format string containing formatting specifications.</param>
    /// <param name="provider">
    /// An object that supplies format information about the given vector.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the given vector was successfully written to the
    /// <paramref name="destination"/>; otherwise <see langword="false"/>.
    /// </returns>
    public static bool TryFormat<TSelf, TScalar>(
        TSelf value,
        Span<char> destination,
        out int charsWritten,
        ReadOnlySpan<char> format,
        IFormatProvider? provider)
        where TSelf : ISpatialVector<TSelf, TScalar>
        where TScalar : IFloatingPointIeee754<TScalar>
    {
        charsWritten = 0;
        if (destination.Length < 2 + TSelf.Count - 1)
        {
            return false;
        }
        destination[0] = '<';
        charsWritten = 1;
        destination = destination[1..];

        var separator = NumberFormatInfo.GetInstance(provider).NumberGroupSeparator;
        for (var i = 0; i < TSelf.Count; i++)
        {
            if (!value[i].TryFormat(destination, out var c, format, provider))
            {
                return false;
            }
            charsWritten += c;
            destination = destination[c..];

            if (i < TSelf.Count - 1)
            {
                if (destination.Length < separator.Length + 1
                    || !separator.TryCopyTo(destination))
                {
                    return false;
                }
                destination = destination[separator.Length..];
                destination[0] = ' ';
                charsWritten++;
                destination = destination[1..];
            }
        }

        if (destination.Length < 1)
        {
            return false;
        }
        destination[0] = '>';
        charsWritten++;
        return true;
    }
}
