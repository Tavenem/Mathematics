using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace Tavenem.Mathematics;

/// <summary>
/// Mathematical extension methods.
/// </summary>
public static class NumberBaseExtensions
{
    /// <summary>
    /// Computes the absolute of a value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">The value for which to get its absolute.</param>
    /// <returns>
    /// The absolute of <paramref name="value"/>.
    /// </returns>
    /// <exception cref="OverflowException">
    /// The absolute of <paramref name="value"/> is not representable by <typeparamref name="T"/>.
    /// </exception>
    public static T Abs<T>(this T value) where T : INumberBase<T> => T.Abs(value);

    /// <summary>
    /// Determines if a value is in its canonical representation.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is in its canonical representation;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsCanonical<T>(this T value) where T : INumberBase<T> => T.IsCanonical(value);

    /// <summary>
    /// Determines if a value represents a complex number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is a complex number; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This function returns <see langword="false"/> for a complex number <c>a + bi</c> where
    /// <c>b</c> is zero.
    /// </remarks>
    public static bool IsComplexNumber<T>(this T value) where T : INumberBase<T> => T.IsComplexNumber(value);

    /// <summary>
    /// Determines if a value represents an even integral number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is an even integer; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method correctly handles floating-point values and so <c>2.0</c> will return <see
    /// langword="true"/> while <c>2.2</c> will return <see langword="false"/>.
    /// </para>
    /// <para>
    /// A return value of <see langword="false"/> does not imply that <see
    /// cref="IsOddInteger{T}(T)"/> will return <see langword="true"/>. A number with a fractional
    /// portion, for example, <c>3.3</c>, is not even or odd.
    /// </para>
    /// </remarks>
    public static bool IsEvenInteger<T>(this T value) where T : INumberBase<T> => T.IsEvenInteger(value);

    /// <summary>
    /// Determines if a value is finite.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if the value is finite; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// A return value of <see langword="false"/> does not imply that <see cref="IsInfinity{T}(T)"/>
    /// will return <see langword="true"/>. <c>NaN</c> is not finite or infinite.
    /// </remarks>
    public static bool IsFinite<T>(this T value) where T : INumberBase<T> => T.IsFinite(value);

    /// <summary>
    /// Determines if a value represents an imaginary number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is an imaginary number; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This function returns <see langword="false"/> for an imaginary number <c>a + bi</c> where
    /// <c>b</c> is non-zero.
    /// </remarks>
    public static bool IsImaginaryNumber<T>(this T value) where T : INumberBase<T> => T.IsImaginaryNumber(value);

    /// <summary>
    /// Determines if a value is infinite.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is infinite; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    /// <remarks>
    /// A return value of <see langword="false"/> does not imply that <see cref="IsFinite{T}(T)"/>
    /// will return <see langword="true"/>. <c>NaN</c> is not finite or infinite.
    /// </remarks>
    public static bool IsInfinity<T>(this T value) where T : INumberBase<T> => T.IsInfinity(value);

    /// <summary>
    /// Determines if a value represents an integral number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is an integer; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This method correctly handles floating-point values and so <c>2.0</c> and <c>3.0</c> will
    /// return <see langword="true"/> while <c>2.2</c> and <c>3.3</c> will return <see
    /// langword="false"/>.
    /// </remarks>
    public static bool IsInteger<T>(this T value) where T : INumberBase<T> => T.IsInfinity(value);

    /// <summary>
    /// Determines if a value is NaN.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is NaN; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    public static bool IsNaN<T>(this T value) where T : INumberBase<T> => T.IsNaN(value);

    /// <summary>
    /// Determines if a value is negative.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is negative; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    /// <remarks>
    /// A return value of <see langword="false"/> does not imply that <see cref="IsPositive{T}(T)"/>
    /// will return <see langword="true"/>. A complex number, <c>a + bi</c> for non-zero <c>b</c>,
    /// is not positive or negative.
    /// </remarks>
    public static bool IsNegative<T>(this T value) where T : INumberBase<T> => T.IsNegative(value);

    /// <summary>
    /// Determines if a value is negative infinity.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is negative infinity; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    public static bool IsNegativeInfinity<T>(this T value) where T : INumberBase<T> => T.IsNegativeInfinity(value);

    /// <summary>
    /// Determines if a value is normal.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is normal; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    public static bool IsNormal<T>(this T value) where T : INumberBase<T> => T.IsNormal(value);

    /// <summary>
    /// Determines if a value represents an odd integral number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is an odd integer; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method correctly handles floating-point values and so <c>3.0</c> will return <see
    /// langword="true"/> while <c>3.3</c> will return <see langword="false"/>.
    /// </para>
    /// <para>
    /// A return value of <see langword="false"/> does not imply that <see
    /// cref="IsEvenInteger{T}(T)"/> will return <see langword="true"/>. A number with a fractional
    /// portion, for example, <c>3.3</c>, is not even or odd.
    /// </para>
    /// </remarks>
    public static bool IsOddInteger<T>(this T value) where T : INumberBase<T> => T.IsOddInteger(value);

    /// <summary>
    /// Determines if a value is positive.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is positive; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    /// <remarks>
    /// A return value of <see langword="false"/> does not imply that <see cref="IsNegative{T}(T)"/>
    /// will return <see langword="true"/>. A complex number, <c>a + bi</c> for non-zero <c>b</c>,
    /// is not positive or negative.
    /// </remarks>
    public static bool IsPositive<T>(this T value) where T : INumberBase<T> => T.IsPositive(value);

    /// <summary>
    /// Determines if a value is positive infinity.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is positive infinity; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    public static bool IsPositiveInfinity<T>(this T value) where T : INumberBase<T> => T.IsPositiveInfinity(value);

    /// <summary>
    /// Determines if a value represents a real number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is a real number; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This function returns <see langword="true"/> for a complex number <c>a + bi</c> where
    /// <c>b</c> is zero.
    /// </remarks>
    public static bool IsRealNumber<T>(this T value) where T : INumberBase<T> => T.IsRealNumber(value);

    /// <summary>
    /// Determines if a value is subnormal.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is subnormal; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    public static bool IsSubnormal<T>(this T value) where T : INumberBase<T> => T.IsSubnormal(value);

    /// <summary>
    /// Determines if a value is zero.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is zero; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This function treats both positive and negative zero as zero and so will return <see
    /// langword="true"/> for <c>+0.0</c> and <c>-0.0</c>.
    /// </remarks>
    public static bool IsZero<T>(this T value) where T : INumberBase<T> => T.IsZero(value);

    /// <summary>
    /// Compares two values to compute which is greater.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value to compare with <paramref name="y"/>.</param>
    /// <param name="y">The value to compare with <paramref name="x"/>.</param>
    /// <returns>
    /// <paramref name="x"/> if it is greater than <paramref name="y"/>; otherwise <paramref
    /// name="y"/>.
    /// </returns>
    /// <remarks>
    /// For <see cref="IFloatingPointIeee754{TSelf}"/> this method matches the IEEE 754:2019
    /// <c>maximumMagnitude</c> function. This requires NaN inputs to be propagated back to the
    /// caller and for <c>-0.0</c> to be treated as less than <c>+0.0</c>.
    /// </remarks>
    public static T MaxMagnitude<T>(this T x, T y) where T : INumberBase<T> => T.MaxMagnitude(x, y);

    /// <summary>
    /// Compares two values to compute which has the greater magnitude and returning the other value
    /// if an input is <c>NaN</c>.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value to compare with <paramref name="y"/>.</param>
    /// <param name="y">The value to compare with <paramref name="x"/>.</param>
    /// <returns>
    /// <paramref name="x"/> if it is greater than <paramref name="y"/>; otherwise <paramref
    /// name="y"/>.
    /// </returns>
    /// <remarks>
    /// For <see cref="IFloatingPointIeee754{TSelf}"/> this method matches the IEEE 754:2019
    /// <c>maximumMagnitudeNumber</c> function. This requires NaN inputs to not be propagated back
    /// to the caller and for <c>-0.0</c> to be treated as less than <c>+0.0</c>.
    /// </remarks>
    public static T MaxMagnitudeNumber<T>(this T x, T y) where T : INumberBase<T> => T.MaxMagnitudeNumber(x, y);

    /// <summary>
    /// Compares two values to compute which is lesser.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value to compare with <paramref name="y"/>.</param>
    /// <param name="y">The value to compare with <paramref name="x"/>.</param>
    /// <returns>
    /// <paramref name="x"/> if it is less than <paramref name="y"/>; otherwise <paramref
    /// name="y"/>.
    /// </returns>
    /// <remarks>
    /// For <see cref="IFloatingPointIeee754{TSelf}"/> this method matches the IEEE 754:2019
    /// <c>minimumMagnitude</c> function. This requires NaN inputs to be propagated back to the
    /// caller and for <c>-0.0</c> to be treated as less than <c>+0.0</c>.
    /// </remarks>
    public static T MinMagnitude<T>(this T x, T y) where T : INumberBase<T> => T.MinMagnitude(x, y);

    /// <summary>
    /// Compares two values to compute which has the lesser magnitude and returning the other value
    /// if an input is <c>NaN</c>.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value to compare with <paramref name="y"/>.</param>
    /// <param name="y">The value to compare with <paramref name="x"/>.</param>
    /// <returns>
    /// <paramref name="x"/> if it is less than <paramref name="y"/>; otherwise <paramref
    /// name="y"/>.
    /// </returns>
    /// <remarks>
    /// For <see cref="IFloatingPointIeee754{TSelf}"/> this method matches the IEEE 754:2019
    /// <c>minimumMagnitudeNumber</c> function. This requires NaN inputs to not be propagated back
    /// to the caller and for <c>-0.0</c> to be treated as less than <c>+0.0</c>.
    /// </remarks>
    public static T MinMagnitudeNumber<T>(this T x, T y) where T : INumberBase<T> => T.MinMagnitudeNumber(x, y);

    /// <summary>
    /// Parses a span of characters into a value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="s">The span of characters to parse.</param>
    /// <param name="style">
    /// A bitwise combination of number styles that can be present in <paramref name="s"/>.
    /// </param>
    /// <param name="provider">
    /// An object that provides culture-specific formatting information about <paramref name="s"/>.
    /// </param>
    /// <returns>
    /// The result of parsing <paramref name="s"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="style"/> is not a supported <see cref="NumberStyles"/> value.
    /// </exception>
    /// <exception cref="FormatException">
    /// <paramref name="s"/> is not in the correct format.
    /// </exception>
    /// <exception cref="OverflowException">
    /// <paramref name="s"/> is not representable by <typeparamref name="T"/>.
    /// </exception>
    public static T Parse<T>(this ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        where T : INumberBase<T> => T.Parse(s, style, provider);

    /// <summary>
    /// Parses a string into a value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="s">The string to parse.</param>
    /// <param name="style">
    /// A bitwise combination of number styles that can be present in <paramref name="s"/>.
    /// </param>
    /// <param name="provider">
    /// An object that provides culture-specific formatting information about <paramref name="s"/>.
    /// </param>
    /// <returns>
    /// The result of parsing <paramref name="s"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="style"/> is not a supported <see cref="NumberStyles"/> value.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="s"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="FormatException">
    /// <paramref name="s"/> is not in the correct format.
    /// </exception>
    /// <exception cref="OverflowException">
    /// <paramref name="s"/> is not representable by <typeparamref name="T"/>.
    /// </exception>
    public static T Parse<T>(this string s, NumberStyles style, IFormatProvider? provider)
        where T : INumberBase<T> => T.Parse(s, style, provider);

    /// <summary>
    /// Attempts to create a new instance of <typeparamref name="TTarget"/> from the given
    /// <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TSelf">The type of <paramref name="value"/>.</typeparam>
    /// <typeparam name="TTarget">The type to create.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <param name="result">
    /// If this method returns <see langword="true"/>, this will be set to a value of type
    /// <typeparamref name="TTarget"/> with the same value as <paramref name="value"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the conversion succeeded; otherwise <see langword="false"/>.
    /// </returns>
    public static bool TryCreate<TSelf, TTarget>(this TSelf value, [NotNullWhen(true)] out TTarget? result)
        where TSelf : INumberBase<TSelf>
        where TTarget : INumberBase<TTarget>
    {
        try
        {
            result = TTarget.CreateChecked(value);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }

    /// <summary>
    /// Tries to parses a span of characters into a value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="s">The span of characters to parse.</param>
    /// <param name="style">
    /// A bitwise combination of number styles that can be present in <paramref name="s"/>.
    /// </param>
    /// <param name="provider">
    /// An object that provides culture-specific formatting information about <paramref name="s"/>.
    /// </param>
    /// <param name="result">
    /// On return, contains the result of succesfully parsing <paramref name="s"/> or an undefined
    /// value on failure.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="s"/> was successfully parsed; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="style"/> is not a supported <see cref="NumberStyles"/> value.
    /// </exception>
    public static bool TryParse<T>(
        this ReadOnlySpan<char> s,
        NumberStyles style,
        IFormatProvider? provider,
        out T result)
        where T : INumberBase<T> => T.TryParse(s, style, provider, out result);

    /// <summary>
    /// Tries to parses a string into a value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="s">The string to parse.</param>
    /// <param name="style">
    /// A bitwise combination of number styles that can be present in <paramref name="s"/>.
    /// </param>
    /// <param name="provider">
    /// An object that provides culture-specific formatting information about <paramref name="s"/>.
    /// </param>
    /// <param name="result">
    /// On return, contains the result of succesfully parsing <paramref name="s"/> or an undefined
    /// value on failure.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="s"/> was successfully parsed; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="style"/> is not a supported <see cref="NumberStyles"/> value.
    /// </exception>
    public static bool TryParse<T>(
        [NotNullWhen(true)] this string? s,
        NumberStyles style,
        IFormatProvider? provider,
        out T result)
        where T : INumberBase<T> => T.TryParse(s, style, provider, out result);

    internal static TTarget SafeTypeConvert<TTarget, TOther>(this TOther value)
        where TTarget : INumberBase<TTarget>
        where TOther : INumberBase<TOther> => value.TryCreate(out TTarget? result)
        ? result
        : TTarget.Zero;
}
