using System.Numerics;

namespace Tavenem.Mathematics;

/// <summary>
/// Mathematical extension methods.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Computes the angle between two vectors.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <returns>The angle between the vectors, in radians.</returns>
    public static double Angle(this Vector3 value1, Vector3 value2)
        => Math.Atan2(Vector3.Cross(value1, value2).Length(), Vector3.Dot(value1, value2));

    /// <summary>
    /// A fast implementation of cube (a number raised to the power of 3).
    /// </summary>
    /// <param name="value">The value to cube.</param>
    /// <returns><paramref name="value"/> cubed (raised to the power of 3).</returns>
    public static int Cube(this byte value) => value * value * value;

    /// <summary>
    /// A fast implementation of cube (a number raised to the power of 3).
    /// </summary>
    /// <param name="value">The value to cube.</param>
    /// <returns><paramref name="value"/> cubed (raised to the power of 3).</returns>
    public static double Cube(this int value) => value == 0 ? 0 : (double)value * value * value;

    /// <summary>
    /// A fast implementation of cube (a number raised to the power of 3).
    /// </summary>
    /// <param name="value">The value to cube.</param>
    /// <returns><paramref name="value"/> cubed (raised to the power of 3).</returns>
    public static double Cube(this long value) => value == 0 ? 0 : (double)value * value * value;

    /// <summary>
    /// A fast implementation of cube (a number raised to the power of 3).
    /// </summary>
    /// <param name="value">The value to cube.</param>
    /// <returns><paramref name="value"/> cubed (raised to the power of 3).</returns>
    public static int Cube(this sbyte value) => value * value * value;

    /// <summary>
    /// A fast implementation of cube (a number raised to the power of 3).
    /// </summary>
    /// <param name="value">The value to cube.</param>
    /// <returns><paramref name="value"/> cubed (raised to the power of 3).</returns>
    public static int Cube(this short value) => value * value * value;

    /// <summary>
    /// A fast implementation of cube (a number raised to the power of 3).
    /// </summary>
    /// <param name="value">The value to cube.</param>
    /// <returns><paramref name="value"/> cubed (raised to the power of 3).</returns>
    public static double Cube(this uint value) => value == 0 ? 0 : (double)value * value * value;

    /// <summary>
    /// A fast implementation of cube (a number raised to the power of 3).
    /// </summary>
    /// <param name="value">The value to cube.</param>
    /// <returns><paramref name="value"/> cubed (raised to the power of 3).</returns>
    public static double Cube(this ulong value) => value == 0 ? 0 : (double)value * value * value;

    /// <summary>
    /// A fast implementation of cube (a number raised to the power of 3).
    /// </summary>
    /// <param name="value">The value to cube.</param>
    /// <returns><paramref name="value"/> cubed (raised to the power of 3).</returns>
    public static int Cube(this ushort value) => value * value * value;

    /// <summary>
    /// A fast implementation of cube (a number raised to the power of 3).
    /// </summary>
    /// <param name="value">The value to cube.</param>
    /// <returns><paramref name="value"/> cubed (raised to the power of 3).</returns>
    public static T Cube<T>(this T value) where T : IMultiplyOperators<T, T, T> => value * value * value;

    /// <summary>
    /// Finds the weight which would produce the given <paramref name="result"/> when linearly
    /// interpolating between the two given values.
    /// </summary>
    /// <param name="first">The first value.</param>
    /// <param name="second">The second value.</param>
    /// <param name="result">The desired result of a linear interpolation between <paramref
    /// name="first"/> and <paramref name="second"/>.</param>
    /// <returns>
    /// The weight which would produce <paramref name="result"/> when linearly
    /// interpolating between <paramref name="first"/> and <paramref name="second"/>; or <see
    /// cref="IFloatingPoint{TSelf}.NaN"/> if the weight cannot be computed for the given parameters.
    /// </returns>
    /// <remarks>
    /// <see cref="IFloatingPoint{TSelf}.NaN"/> will be returned if the given values are nearly equal, but the
    /// given result is not also nearly equal to them, since the calculation in that case would
    /// require a division by zero.
    /// </remarks>
    public static T InverseLerp<T>(this T first, T second, T result) where T : IFloatingPoint<T>
    {
        var difference = second - first;
        if (IsNearlyZero(difference))
        {
            if (result.IsNearlyEqualTo(first))
            {
                return T.One / (T.One + T.One);
            }
            else
            {
                return T.NaN;
            }
        }
        return (result - first) / difference;
    }

    /// <summary>
    /// Determines if floating-point values are nearly equal, within a tolerance determined by
    /// an epsilon value based on their magnitudes.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <param name="other">The other value.</param>
    /// <returns><see langword="true"/> if the values are nearly equal; otherwise <see
    /// langword="false"/>.</returns>
    public static bool IsNearlyEqualTo(this decimal value, decimal other)
        => IsNearlyEqualTo(value, other, Math.Max(value, other).GetEpsilon());

    /// <summary>
    /// Determines if floating-point values are nearly equal, within a tolerance determined by
    /// an epsilon value based on their magnitudes.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <param name="other">The other value.</param>
    /// <returns><see langword="true"/> if the values are nearly equal; otherwise <see
    /// langword="false"/>.</returns>
    public static bool IsNearlyEqualTo(this double value, double other)
        => IsNearlyEqualTo(value, other, Math.Max(value, other).GetEpsilon());

    /// <summary>
    /// Determines if floating-point values are nearly equal, within a tolerance determined by
    /// an epsilon value based on their magnitudes.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <param name="other">The other value.</param>
    /// <returns><see langword="true"/> if the values are nearly equal; otherwise <see
    /// langword="false"/>.</returns>
    public static bool IsNearlyEqualTo(this float value, float other)
        => IsNearlyEqualTo(value, other, Math.Max(value, other).GetEpsilon());

    /// <summary>
    /// Determines if values are nearly equal, within a tolerance determined by
    /// the given epsilon value.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <param name="other">The other value.</param>
    /// <param name="epsilon">
    /// An epsilon value which determines the tolerance for near-equality between the values.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if this value and the other are nearly equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool IsNearlyEqualTo<T>(this T value, T other, T epsilon) where T : INumber<T>
        => value == other || T.Abs(value - other) < epsilon;

    /// <summary>
    /// Determines if floating-point values are nearly equal, within a tolerance determined by
    /// an epsilon value based on their magnitudes.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <param name="other">The other value.</param>
    /// <returns>
    /// <see langword="true"/> if the values are nearly equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool IsNearlyEqualTo<T>(this T value, T other) where T : IFloatingPoint<T>
        => value.IsNearlyEqualTo(other, T.Max(value, other).GetEpsilon());

    /// <summary>
    /// Determines if a floating-point value is nearly zero.
    /// </summary>
    /// <param name="value">A value to test.</param>
    /// <returns>
    /// <see langword="true"/> if the value is close to 0; otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Uses <see cref="NumberValues.NearlyZeroDecimal"/> as the threshhold for closeness to zero.
    /// </remarks>
    public static bool IsNearlyZero(this decimal value) => value is < NumberValues.NearlyZeroDecimal and > (-NumberValues.NearlyZeroDecimal);

    /// <summary>
    /// Determines if a floating-point value is nearly zero.
    /// </summary>
    /// <param name="value">A value to test.</param>
    /// <returns>
    /// <see langword="true"/> if the value is close to 0; otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Uses <see cref="NumberValues.NearlyZeroDouble"/> as the threshhold for closeness to zero.
    /// </remarks>
    public static bool IsNearlyZero(this double value) => value is < NumberValues.NearlyZeroDouble and > (-NumberValues.NearlyZeroDouble);

    /// <summary>
    /// Determines if a floating-point value is nearly zero.
    /// </summary>
    /// <param name="value">A value to test.</param>
    /// <returns>
    /// <see langword="true"/> if the value is close to 0; otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Uses <see cref="NumberValues.NearlyZeroFloat"/> as the threshhold for closeness to zero.
    /// </remarks>
    public static bool IsNearlyZero(this float value) => value is < NumberValues.NearlyZeroFloat and > (-NumberValues.NearlyZeroFloat);

    /// <summary>
    /// Determines if a floating-point value is nearly zero.
    /// </summary>
    /// <param name="value">A value to test.</param>
    /// <returns>
    /// <see langword="true"/> if the value is close to 0; otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Uses <see cref="NumberValues.NearlyZeroDouble"/> as the threshhold for closeness to zero.
    /// </remarks>
    public static bool IsNearlyZero<T>(this T value) where T : IFloatingPoint<T>
    {
        var nearlyZero = T.TryCreate(NumberValues.NearlyZeroDouble, out var nz)
            ? nz
            : T.Zero;
        return value < nearlyZero && value > (-nearlyZero);
    }

    /// <summary>
    /// Linearly interpolates between two values based on the given weighting.
    /// </summary>
    /// <param name="first">The first value.</param>
    /// <param name="second">The second value.</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of the second source
    /// vector.</param>
    /// <returns>The interpolated value.</returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="amount"/> is negative, a value will be obtained that is in the
    /// opposite direction on a number line from <paramref name="first"/> as <paramref
    /// name="second"/>, rather than between them. E.g. <c>Lerp(2, 3, -0.5)</c> would return
    /// <c>1.5</c>.
    /// </para>
    /// <para>
    /// If <paramref name="amount"/> is greater than one, a value will be obtained that is
    /// in the opposite direction on a number line from <paramref name="second"/> as <paramref
    /// name="first"/>, rather than between them. E.g. <c>Lerp(2, 3, 1.5)</c> would return
    /// <c>3.5</c>.
    /// </para>
    /// </remarks>
    public static T Lerp<T>(this T first, T second, T amount)
        where T : IAdditionOperators<T, T, T>,
            ISubtractionOperators<T, T, T>,
            IMultiplyOperators<T, T, T>
        => first + ((second - first) * amount);

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="int"/>. Truncates to <see
    /// cref="int.MinValue"/> or <see cref="int.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="int"/> to this value.</returns>
    public static int RoundToInt(this double value)
    {
        if (value < int.MinValue)
        {
            return int.MinValue;
        }
        else if (value > int.MaxValue)
        {
            return int.MaxValue;
        }
        else
        {
            return Convert.ToInt32(Math.Round(value, MidpointRounding.AwayFromZero));
        }
    }

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="int"/>. Truncates to <see
    /// cref="int.MinValue"/> or <see cref="int.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="int"/> to this value.</returns>
    public static int RoundToInt(this float value)
    {
        if (value < int.MinValue)
        {
            return int.MinValue;
        }
        else if (value > int.MaxValue)
        {
            return int.MaxValue;
        }
        else
        {
            return Convert.ToInt32(Math.Round(value, MidpointRounding.AwayFromZero));
        }
    }

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="int"/>. Truncates to <see
    /// cref="int.MinValue"/> or <see cref="int.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="int"/> to this value.</returns>
    public static int RoundToInt<T>(this T value) where T : IFloatingPoint<T>, IParseable<T>
    {
        var min = T.CreateSaturating(int.MinValue);
        if (value < min)
        {
            return int.MinValue;
        }

        var max = T.CreateSaturating(int.MinValue);
        if (value > max)
        {
            return int.MaxValue;
        }

        return SafeTypeConvert<int, T>(T.Round(value, MidpointRounding.AwayFromZero));
    }

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="long"/>. Truncates to <see
    /// cref="long.MinValue"/> or <see cref="long.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="long"/> to this value.</returns>
    public static long RoundToLong(this double value)
    {
        if (value < long.MinValue)
        {
            return long.MinValue;
        }
        else if (value > long.MaxValue)
        {
            return long.MaxValue;
        }
        else
        {
            return Convert.ToInt64(Math.Round(value, MidpointRounding.AwayFromZero));
        }
    }

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="long"/>. Truncates to <see
    /// cref="long.MinValue"/> or <see cref="long.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="long"/> to this value.</returns>
    public static long RoundToLong(this float value)
    {
        if (value < long.MinValue)
        {
            return long.MinValue;
        }
        else if (value > long.MaxValue)
        {
            return long.MaxValue;
        }
        else
        {
            return Convert.ToInt64(Math.Round(value, MidpointRounding.AwayFromZero));
        }
    }

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="long"/>. Truncates to <see
    /// cref="long.MinValue"/> or <see cref="long.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="long"/> to this value.</returns>
    public static long RoundToLong<T>(this T value) where T : IFloatingPoint<T>, IParseable<T>
    {
        var min = T.CreateSaturating(long.MinValue);
        if (value < min)
        {
            return long.MinValue;
        }

        var max = T.CreateSaturating(long.MinValue);
        if (value > max)
        {
            return long.MaxValue;
        }

        return SafeTypeConvert<long, T>(T.Round(value, MidpointRounding.AwayFromZero));
    }

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="uint"/>. Truncates to 0 or
    /// <see cref="uint.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="uint"/> to this value.</returns>
    public static uint RoundToUInt(this double value)
    {
        if (value < 0)
        {
            return 0;
        }
        else if (value > uint.MaxValue)
        {
            return uint.MaxValue;
        }
        else
        {
            return Convert.ToUInt32(Math.Round(value, MidpointRounding.AwayFromZero));
        }
    }

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="uint"/>. Truncates to 0 or
    /// <see cref="uint.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="uint"/> to this value.</returns>
    public static uint RoundToUInt(this float value)
    {
        if (value < 0)
        {
            return 0;
        }
        else if (value > uint.MaxValue)
        {
            return uint.MaxValue;
        }
        else
        {
            return Convert.ToUInt32(Math.Round(value, MidpointRounding.AwayFromZero));
        }
    }

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="uint"/>. Truncates to 0 or
    /// <see cref="uint.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="uint"/> to this value.</returns>
    public static uint RoundToUInt<T>(this T value) where T : IFloatingPoint<T>, IParseable<T>
    {
        var min = T.CreateSaturating(uint.MinValue);
        if (value < min)
        {
            return uint.MinValue;
        }

        var max = T.CreateSaturating(uint.MinValue);
        if (value > max)
        {
            return uint.MaxValue;
        }

        return SafeTypeConvert<uint, T>(T.Round(value, MidpointRounding.AwayFromZero));
    }

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="ulong"/>. Truncates to 0 or
    /// <see cref="ulong.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="ulong"/> to this value.</returns>
    public static ulong RoundToULong(this double value)
    {
        if (value < 0)
        {
            return 0;
        }
        else if (value > ulong.MaxValue)
        {
            return ulong.MaxValue;
        }
        else
        {
            return Convert.ToUInt32(Math.Round(value, MidpointRounding.AwayFromZero));
        }
    }

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="ulong"/>. Truncates to 0 or
    /// <see cref="ulong.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="ulong"/> to this value.</returns>
    public static ulong RoundToULong(this float value)
    {
        if (value < 0)
        {
            return 0;
        }
        else if (value > ulong.MaxValue)
        {
            return ulong.MaxValue;
        }
        else
        {
            return Convert.ToUInt32(Math.Round(value, MidpointRounding.AwayFromZero));
        }
    }

    /// <summary>
    /// Rounds this floating-point value to the nearest <see cref="ulong"/>. Truncates to 0 or
    /// <see cref="ulong.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">This value.</param>
    /// <returns>The closest <see cref="ulong"/> to this value.</returns>
    public static ulong RoundToULong<T>(this T value) where T : IFloatingPoint<T>, IParseable<T>
    {
        var min = T.CreateSaturating(ulong.MinValue);
        if (value < min)
        {
            return ulong.MinValue;
        }

        var max = T.CreateSaturating(ulong.MinValue);
        if (value > max)
        {
            return ulong.MaxValue;
        }

        return SafeTypeConvert<ulong, T>(T.Round(value, MidpointRounding.AwayFromZero));
    }

    /// <summary>
    /// Returns <paramref name="target"/> if <paramref name="value"/> is nearly equal to it (cf.
    /// <see cref="IsNearlyEqualTo(double, double)"/>), or <paramref name="value"/> itself if not.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <param name="target">The value to snap to.</param>
    /// <returns>
    /// <paramref name="target"/>, if <paramref name="value"/> is nearly equal to it;
    /// otherwise <paramref name="value"/>.
    /// </returns>
    public static double SnapTo(this double value, double target)
        => value.IsNearlyEqualTo(target) ? target : value;

    /// <summary>
    /// Returns <paramref name="target"/> if <paramref name="value"/> is nearly equal to it (cf.
    /// <see cref="IsNearlyEqualTo(float, float)"/>), or <paramref name="value"/> itself if not.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <param name="target">The value to snap to.</param>
    /// <returns>
    /// <paramref name="target"/>, if <paramref name="value"/> is nearly equal to it;
    /// otherwise <paramref name="value"/>.
    /// </returns>
    public static float SnapTo(this float value, float target)
        => value.IsNearlyEqualTo(target) ? target : value;

    /// <summary>
    /// Returns <paramref name="target"/> if <paramref name="value"/> is nearly equal to it (cf.
    /// <see cref="IsNearlyEqualTo{T}(T, T, T)"/>), within a tolerance determined by
    /// the given epsilon value, or <paramref name="value"/> itself if not.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <param name="target">The value to snap to.</param>
    /// <param name="epsilon">
    /// An epsilon value which determines the tolerance for near-equality between the values.
    /// </param>
    /// <returns>
    /// <paramref name="target"/>, if <paramref name="value"/> is nearly equal to it;
    /// otherwise <paramref name="value"/>.
    /// </returns>
    public static T SnapTo<T>(this T value, T target, T epsilon) where T : INumber<T>
        => IsNearlyEqualTo(value, target, epsilon) ? target : value;

    /// <summary>
    /// Returns <paramref name="target"/> if <paramref name="value"/> is nearly equal to it (cf.
    /// <see cref="IsNearlyEqualTo{T}(T, T)"/>), or <paramref name="value"/> itself if not.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <param name="target">The value to snap to.</param>
    /// <returns>
    /// <paramref name="target"/>, if <paramref name="value"/> is nearly equal to it;
    /// otherwise <paramref name="value"/>.
    /// </returns>
    public static T SnapTo<T>(this T value, T target) where T : IFloatingPoint<T>
        => value.SnapTo(target, value.GetEpsilon());

    /// <summary>
    /// Returns zero if <paramref name="value"/> is nearly equal to zero (cf. <see
    /// cref="IsNearlyZero(double)"/>), or <paramref name="value"/> itself if not.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <returns>
    /// Zero, if <paramref name="value"/> is nearly equal to zero; otherwise <paramref
    /// name="value"/>.
    /// </returns>
    public static double SnapToZero(this double value)
        => value.IsNearlyZero() ? 0 : value;

    /// <summary>
    /// Returns zero if <paramref name="value"/> is nearly equal to zero (cf. <see
    /// cref="IsNearlyZero(float)"/>), or <paramref name="value"/> itself if not.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <returns>
    /// Zero, if <paramref name="value"/> is nearly equal to zero; otherwise <paramref
    /// name="value"/>.
    /// </returns>
    public static float SnapToZero(this float value)
        => value.IsNearlyZero() ? 0 : value;

    /// <summary>
    /// Returns zero if <paramref name="value"/> is nearly equal to zero (cf. <see
    /// cref="IsNearlyZero{T}(T)"/>), or <paramref name="value"/> itself if not.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <returns>
    /// Zero, if <paramref name="value"/> is nearly equal to zero; otherwise <paramref
    /// name="value"/>.
    /// </returns>
    public static T SnapToZero<T>(this T value) where T : IFloatingPoint<T>
        => value.IsNearlyZero() ? T.Zero : value;

    /// <summary>
    /// A fast implementation of squaring (a number raised to the power of 2).
    /// </summary>
    /// <param name="value">The value to square.</param>
    /// <returns><paramref name="value"/> squared (raised to the power of 2).</returns>
    public static int Square(this byte value) => value * value;

    /// <summary>
    /// A fast implementation of squaring (a number raised to the power of 2).
    /// </summary>
    /// <param name="value">The value to square.</param>
    /// <returns><paramref name="value"/> squared (raised to the power of 2).</returns>
    public static double Square(this int value) => value == 0 ? 0 : (double)value * value;

    /// <summary>
    /// A fast implementation of squaring (a number raised to the power of 2).
    /// </summary>
    /// <param name="value">The value to square.</param>
    /// <returns><paramref name="value"/> squared (raised to the power of 2).</returns>
    public static double Square(this long value) => value == 0 ? 0 : (double)value * value;

    /// <summary>
    /// A fast implementation of squaring (a number raised to the power of 2).
    /// </summary>
    /// <param name="value">The value to square.</param>
    /// <returns><paramref name="value"/> squared (raised to the power of 2).</returns>
    public static int Square(this sbyte value) => value * value;

    /// <summary>
    /// A fast implementation of squaring (a number raised to the power of 2).
    /// </summary>
    /// <param name="value">The value to square.</param>
    /// <returns><paramref name="value"/> squared (raised to the power of 2).</returns>
    public static int Square(this short value) => value * value;

    /// <summary>
    /// A fast implementation of squaring (a number raised to the power of 2).
    /// </summary>
    /// <param name="value">The value to square.</param>
    /// <returns><paramref name="value"/> squared (raised to the power of 2).</returns>
    public static double Square(this uint value) => value == 0 ? 0 : (double)value * value;

    /// <summary>
    /// A fast implementation of squaring (a number raised to the power of 2).
    /// </summary>
    /// <param name="value">The value to square.</param>
    /// <returns><paramref name="value"/> squared (raised to the power of 2).</returns>
    public static double Square(this ulong value) => value == 0 ? 0 : (double)value * value;

    /// <summary>
    /// A fast implementation of squaring (a number raised to the power of 2).
    /// </summary>
    /// <param name="value">The value to square.</param>
    /// <returns><paramref name="value"/> squared (raised to the power of 2).</returns>
    public static int Square(this ushort value) => value * value;

    /// <summary>
    /// A fast implementation of squaring (a number raised to the power of 2).
    /// </summary>
    /// <param name="value">The value to square.</param>
    /// <returns><paramref name="value"/> squared (raised to the power of 2).</returns>
    public static T Square<T>(this T value) where T : IMultiplyOperators<T, T, T> => value * value;

    /// <summary>
    /// Returns the square root of a specified number.
    /// </summary>
    /// <param name="value">The number whose square root is to be found.</param>
    /// <returns>
    /// One of the values in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term><paramref name="value"/> parameter</term>
    /// <description>Return value</description>
    /// </listheader>
    /// <item>
    /// <term>Zero or positive</term>
    /// <description>The positive square root of <paramref name="value"/></description>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <description>An <see cref="ArgumentOutOfRangeException"/></description>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// Uses the <see cref="Math.Sqrt(double)"/> function, then corrects the result using
    /// addition and division in a loop until the result is accurate to within the precision of
    /// a <see cref="decimal"/> value. In the worst case, the loop will iterate at most three
    /// times.
    /// </remarks>
    public static decimal Sqrt(this decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }
        var current = (decimal)Math.Sqrt((double)value);
        decimal previous;
        do
        {
            previous = current;
            if (previous == 0m)
            {
                return 0;
            }
            current = (previous + (value / previous)) / 2;
        } while (Math.Abs(previous - current) > 0);
        return current;
    }

    internal static TTarget TypeConvert<TTarget, TOther>(TOther value)
        where TTarget : INumber<TTarget>
        where TOther : INumber<TOther>
        => TTarget.Create(value);

    internal static TTarget SafeTypeConvert<TTarget, TOther>(TOther value)
        where TTarget : INumber<TTarget>
        where TOther : INumber<TOther> => TTarget.TryCreate(value, out var result)
        ? result
        : TTarget.Zero;

    /// <summary>
    /// Gets an appropriate epsilon for floating-point equality comparisons based on the
    /// magnitude of the given value.
    /// </summary>
    /// <param name="value">
    /// The value whose equality-comparison epsilon should be calculated.
    /// </param>
    private static decimal GetEpsilon(this decimal value) => value * NumberValues.NearlyZeroDecimal;

    /// <summary>
    /// Gets an appropriate epsilon for floating-point equality comparisons based on the
    /// magnitude of the given value.
    /// </summary>
    /// <param name="value">
    /// The value whose equality-comparison epsilon should be calculated.
    /// </param>
    private static double GetEpsilon(this double value) => value * NumberValues.NearlyZeroDouble;

    /// <summary>
    /// Gets an appropriate epsilon for floating-point equality comparisons based on the
    /// magnitude of the given value.
    /// </summary>
    /// <param name="value">
    /// The value whose equality-comparison epsilon should be calculated.
    /// </param>
    private static float GetEpsilon(this float value) => value * NumberValues.NearlyZeroFloat;

    /// <summary>
    /// Gets an appropriate epsilon for floating-point equality comparisons based on the
    /// magnitude of the given value.
    /// </summary>
    /// <param name="value">
    /// The value whose equality-comparison epsilon should be calculated.
    /// </param>
    private static T GetEpsilon<T>(this T value) where T : IFloatingPoint<T>
        => value * T.CreateSaturating(NumberValues.NearlyZeroDouble);
}
