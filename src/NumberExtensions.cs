using System.Numerics;

namespace Tavenem.Mathematics;

/// <summary>
/// Mathematical extension methods.
/// </summary>
public static class NumberExtensions
{
    /// <summary>
    /// Clamps a value to an inclusive minimum and maximum value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">
    /// The inclusive minimum to which <paramref name="value"/> should clamp.
    /// </param>
    /// <param name="max">
    /// The inclusive maximum to which <paramref name="value"/> should clamp.
    /// </param>
    /// <returns>
    /// The result of clamping value to the inclusive range of min and max.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="min"/> is greater than <paramref name="max"/>.
    /// </exception>
    public static T Clamp<T>(this T value, T min, T max) where T : INumber<T> => T.Clamp(value, min, max);

    /// <summary>
    /// Copies the sign of a value to the sign of another value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">The value whose magnitude is used in the result.</param>
    /// <param name="sign">The value whose sign is used in the result.</param>
    /// <returns>
    /// A value with the magnitude of <paramref name="value"/> and the sign of <paramref name="sign"/>.
    /// </returns>
    public static T CopySign<T>(this T value, T sign) where T : INumber<T> => T.CopySign(value, sign);

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
    /// For <see cref="IFloatingPoint{TSelf}"/>, this method matches the IEEE 754:2019
    /// <c>maximum</c> function. This requires NaN inputs to be propagated back to the caller and
    /// for -0.0 to be treated as less than +0.0.
    /// </remarks>
    public static T Max<T>(this T x, T y) where T : INumber<T> => T.Max(x, y);

    /// <summary>
    /// Compares two values to compute which is greater and returning the other value if an input is
    /// <c>NaN</c>.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value to compare with <paramref name="y"/>.</param>
    /// <param name="y">The value to compare with <paramref name="x"/>.</param>
    /// <returns>
    /// <paramref name="x"/> if it is greater than <paramref name="y"/>; otherwise <paramref
    /// name="y"/>.
    /// </returns>
    /// <remarks>
    /// For <see cref="IFloatingPoint{TSelf}"/>, this method matches the IEEE 754:2019 maximum
    /// function. This requires NaN inputs to be propagated back to the caller and for -0.0 to be
    /// treated as less than +0.0.
    /// </remarks>
    public static T MaxNumber<T>(this T x, T y) where T : INumber<T> => T.MaxNumber(x, y);

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
    /// For <see cref="IFloatingPoint{TSelf}"/>, this method matches the IEEE 754:2019
    /// <c>minimum</c> function. This requires NaN inputs to be propagated back to the caller and
    /// for -0.0 to be treated as less than +0.0.
    /// </remarks>
    public static T Min<T>(this T x, T y) where T : INumber<T> => T.Min(x, y);

    /// <summary>
    /// Compares two values to compute which is lesser and returning the other value if an input is
    /// <c>NaN</c>.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value to compare with <paramref name="y"/>.</param>
    /// <param name="y">The value to compare with <paramref name="x"/>.</param>
    /// <returns>
    /// <paramref name="x"/> if it is less than <paramref name="y"/>; otherwise <paramref
    /// name="y"/>.
    /// </returns>
    /// <remarks>
    /// For <see cref="IFloatingPoint{TSelf}"/>, this method matches the IEEE 754:2019
    /// <c>minimum</c> function. This requires NaN inputs to be propagated back to the caller and
    /// for -0.0 to be treated as less than +0.0.
    /// </remarks>
    public static T MinNumber<T>(this T x, T y) where T : INumber<T> => T.MinNumber(x, y);

    /// <summary>
    /// Computes the sign of a value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">The value whose sign is to be computed.</param>
    /// <returns>
    /// A positive value if <paramref name="value"/> is positive, Zero if <paramref name="value"/>
    /// is zero, and a negative value if <paramref name="value"/> is negative.
    /// </returns>
    /// <remarks>
    /// It is recommended that a function return <c>1</c>, <c>0</c>, and <c>-1</c>, respectively.
    /// </remarks>
    public static int Sign<T>(this T value) where T : INumber<T> => T.Sign(value);
}
