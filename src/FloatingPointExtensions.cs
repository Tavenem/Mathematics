using System.Numerics;

namespace Tavenem.Mathematics;

/// <summary>
/// Mathematical extension methods.
/// </summary>
public static class FloatingPointExtensions
{
    /// <summary>
    /// Returns the smallest integral value that is greater than or equal to the specified number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">A number.</param>
    /// <returns>
    /// <para>
    /// The smallest integral value that is greater than or equal to <paramref name="x"/>.
    /// </para>
    /// <para>
    /// If <paramref name="x"/> is equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/>,
    /// <see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/>, or
    /// <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/>, that value is returned.
    /// </para>
    /// <para>
    /// Note that this method returns <typeparamref name="T"/> instead of an integral type.
    /// </para>
    /// </returns>
    public static T Ceiling<T>(this T x) where T : IFloatingPoint<T> => T.Ceiling(x);

    /// <summary>
    /// Returns the largest integral value that is less than or equal to the specified number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">A number.</param>
    /// <returns>
    /// <para>
    /// The largest integral value that is less than or equal to <paramref name="x"/>.
    /// </para>
    /// <para>
    /// If <paramref name="x"/> is equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/>,
    /// <see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/>, or
    /// <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/>, that value is returned.
    /// </para>
    /// <para>
    /// Note that this method returns <typeparamref name="T"/> instead of an integral type.
    /// </para>
    /// </returns>
    public static T Floor<T>(this T x) where T : IFloatingPoint<T> => T.Floor(x);

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
    public static bool IsNearlyEqualTo<T>(this T value, T other, T epsilon) where T :
        INumberBase<T>,
        IComparisonOperators<T, T, bool>,
        IEqualityOperators<T, T, bool>
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
    /// Uses <see cref="NumberValues.NearlyZeroDecimal"/> as the threshold for closeness to zero.
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
    /// Uses <see cref="NumberValues.NearlyZeroDouble"/> as the threshold for closeness to zero.
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
    /// Uses <see cref="NumberValues.NearlyZeroFloat"/> as the threshold for closeness to zero.
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
    /// Uses <see cref="NumberValues.NearlyZeroDouble"/> as the threshold for closeness to zero.
    /// </remarks>
    public static bool IsNearlyZero<T>(this T value) where T : IFloatingPoint<T>
    {
        var nearlyZero = NumberValues.NearlyZeroDouble.TryCreate(out T? nz)
            ? nz
            : T.Zero;
        return value < nearlyZero && value > (-nearlyZero);
    }

    /// <summary>
    /// Rounds a value to the nearest integral value, and rounds midpoint values to the nearest even number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">A number to be rounded.</param>
    /// <returns>
    /// <para>
    /// The integer nearest <paramref name="x"/>. If the fractional component of
    /// <paramref name="x"/> is halfway between two integers, one of which is even and the other
    /// odd, then the even number is returned.
    /// </para>
    /// <para>
    /// Note that this method returns <typeparamref name="T"/> instead of an integral type.
    /// </para>
    /// </returns>
    public static T Round<T>(this T x) where T : IFloatingPoint<T> => T.Round(x);

    /// <summary>
    /// Rounds a value to a specified number of fractional digits, and rounds midpoint values to
    /// the nearest even number.
    /// </summary>
    /// <typeparam name="TSelf">The type of number.</typeparam>
    /// <param name="x">A number to be rounded.</param>
    /// <param name="digits">The number of fractional digits in the return value.</param>
    /// <returns>
    /// The number nearest to <paramref name="x"/> that contains a number of fractional digits
    /// equal to digits. If <paramref name="x"/> has fewer fractional digits than
    /// <paramref name="digits"/>, <paramref name="x"/> is returned unchanged.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="digits"/> is less than 0 or greater than 15.
    /// </exception>
    public static TSelf Round<TSelf>(this TSelf x, int digits)
        where TSelf : IFloatingPoint<TSelf> => TSelf.Round(x, digits);

    /// <summary>
    /// Rounds a value to the nearest integral value, and uses the specified rounding convention
    /// for midpoint values.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">A number to be rounded.</param>
    /// <param name="mode">
    /// Specification for how to round value if it is midway between two other numbers.
    /// </param>
    /// <returns>
    /// <para>
    /// The integer nearest <paramref name="x"/>. If the fractional component of
    /// <paramref name="x"/> is halfway between two integers, then mode determines which of the two
    /// is returned.
    /// </para>
    /// <para>
    /// Note that this method returns <typeparamref name="T"/> instead of an integral type.
    /// </para>
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="mode"/> is not a valid value of <see cref="MidpointRounding"/>.
    /// </exception>
    public static T Round<T>(this T x, MidpointRounding mode) where T : IFloatingPoint<T> => T.Round(x, mode);

    /// <summary>
    /// Rounds a value to a specified number of fractional digits, and uses the specified rounding
    /// convention for midpoint values.
    /// </summary>
    /// <typeparam name="TSelf">The type of number.</typeparam>
    /// <param name="x">A number to be rounded.</param>
    /// <param name="digits">The number of fractional digits in the return value.</param>
    /// <param name="mode">
    /// Specification for how to round value if it is midway between two other numbers.
    /// </param>
    /// <returns>
    /// The number nearest to <paramref name="x"/> that contains a number of fractional digits
    /// equal to digits. If <paramref name="x"/> has fewer fractional digits than
    /// <paramref name="digits"/>, <paramref name="x"/> is returned unchanged.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="digits"/> is less than 0 or greater than 15.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="mode"/> is not a valid value of <see cref="MidpointRounding"/>.
    /// </exception>
    public static TSelf Round<TSelf>(this TSelf x, int digits, MidpointRounding mode)
        where TSelf : IFloatingPoint<TSelf> => TSelf.Round(x, digits, mode);

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
    public static int RoundToInt<T>(this T value) where T : IFloatingPoint<T>, IParsable<T>
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

        return T.Round(value, MidpointRounding.AwayFromZero).SafeTypeConvert<int, T>();
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
    public static long RoundToLong<T>(this T value) where T : IFloatingPoint<T>, IParsable<T>
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

        return T.Round(value, MidpointRounding.AwayFromZero).SafeTypeConvert<long, T>();
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
    public static uint RoundToUInt<T>(this T value) where T : IFloatingPoint<T>, IParsable<T>
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

        return T.Round(value, MidpointRounding.AwayFromZero).SafeTypeConvert<uint, T>();
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
    public static ulong RoundToULong<T>(this T value) where T : IFloatingPoint<T>, IParsable<T>
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

        return T.Round(value, MidpointRounding.AwayFromZero).SafeTypeConvert<ulong, T>();
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
    /// cref="IsNearlyZero(double)"/>), or <paramref name="value"/> itself
    /// if not.
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
    /// cref="IsNearlyZero(float)"/>), or <paramref name="value"/> itself if
    /// not.
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
    /// cref="IsNearlyZero{T}(T)"/>), or <paramref name="value"/> itself if
    /// not.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <returns>
    /// Zero, if <paramref name="value"/> is nearly equal to zero; otherwise <paramref
    /// name="value"/>.
    /// </returns>
    public static T SnapToZero<T>(this T value) where T : IFloatingPoint<T>
        => value.IsNearlyZero() ? T.Zero : value;

    /// <summary>
    /// Calculates the integral part of a specified number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">A number to truncate.</param>
    /// <returns>
    /// The integral part of <paramref name="x"/>; that is, the number that remains after any
    /// fractional digits have been discarded, or one of the values listed in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term><paramref name="x"/></term>
    /// <description>Return value</description>
    /// </listheader>
    /// <item>
    /// <term><see cref="IFloatingPointIeee754{TSelf}.NaN"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></description>
    /// </item>
    /// </list>
    /// <para>
    /// Note that this method returns <typeparamref name="T"/> instead of an integral type.
    /// </para>
    /// </returns>
    public static T Truncate<T>(this T x) where T : IFloatingPoint<T> => T.Truncate(x);

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
