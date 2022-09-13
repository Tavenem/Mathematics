using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace Tavenem.Mathematics;

/// <summary>
/// Mathematical extension methods.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Returns the absolute value of a number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">A number.</param>
    /// <returns>
    /// The absolute value of the given number.
    /// </returns>
    public static T Abs<T>(this T value) where T : INumberBase<T> => T.Abs(value);

    /// <summary>
    /// Returns the angle whose cosine is the specified number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">
    /// A number representing a cosine, where x must be greater than or equal to -1,
    /// but less than or equal to 1.
    /// </param>
    /// <returns>
    /// <para>
    /// An angle, θ, measured in radians, such that 0 ≤ θ ≤ π.
    /// </para>
    /// <para>
    /// -or- <see cref="IFloatingPointIeee754{TSelf}.NaN"/> if <paramref name="x"/> &lt; -1 or
    /// <paramref name="x"/> &gt; 1 or <paramref name="x"/> equals
    /// <see cref="IFloatingPointIeee754{TSelf}.NaN"/>.
    /// </para>
    /// </returns>
    public static T Acos<T>(this T x) where T : ITrigonometricFunctions<T> => T.Acos(x);

    /// <summary>
    /// Returns the angle whose hyperbolic cosine is the specified number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">
    /// A number representing a hyperbolic cosine, where x must be greater than or equal to 1,
    /// but less than or equal to <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/>.
    /// </param>
    /// <returns>
    /// <para>
    /// An angle, θ, measured in radians, such that 0 ≤ θ ≤ ∞.
    /// </para>
    /// <para>
    /// -or- <see cref="IFloatingPointIeee754{TSelf}.NaN"/> if <paramref name="x"/> &lt; 1 or
    /// <paramref name="x"/> &gt; 1 or <paramref name="x"/> equals
    /// <see cref="IFloatingPointIeee754{TSelf}.NaN"/>.
    /// </para>
    /// </returns>
    public static T Acosh<T>(this T x) where T : IHyperbolicFunctions<T> => T.Acosh(x);

    /// <summary>
    /// Computes the angle between two vectors.
    /// </summary>
    /// <param name="value1">The first vector.</param>
    /// <param name="value2">The second vector.</param>
    /// <returns>The angle between the vectors, in radians.</returns>
    public static double Angle(this Vector3 value1, Vector3 value2)
        => Math.Atan2(Vector3.Cross(value1, value2).Length(), Vector3.Dot(value1, value2));

    /// <summary>
    /// Returns the angle whose sine is the specified number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">
    /// A number representing a sine, where x must be greater than or equal to -1,
    /// but less than or equal to 1.
    /// </param>
    /// <returns>
    /// <para>
    /// An angle, θ, measured in radians, such that -π/2 ≤ θ ≤ π/2.
    /// </para>
    /// <para>
    /// -or- <see cref="IFloatingPointIeee754{TSelf}.NaN"/> if <paramref name="x"/> &lt; -1 or
    /// <paramref name="x"/> &gt; 1 or <paramref name="x"/> equals
    /// <see cref="IFloatingPointIeee754{TSelf}.NaN"/>.
    /// </para>
    /// </returns>
    public static T Asin<T>(this T x) where T : ITrigonometricFunctions<T> => T.Asin(x);

    /// <summary>
    /// Returns the angle whose hyperbolic sine is the specified number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">
    /// A number representing a hyperbolic sine, where x must be greater than or equal to
    /// <see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/>, but less than or equal to
    /// <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/>.
    /// </param>
    /// <returns>
    /// <para>
    /// An angle, θ, measured in radians, such that -∞ &lt; θ ≤ -1, or 1 ≤ θ &lt; ∞.
    /// </para>
    /// <para>
    /// -or- <see cref="IFloatingPointIeee754{TSelf}.NaN"/> if <paramref name="x"/> equals
    /// <see cref="IFloatingPointIeee754{TSelf}.NaN"/>.
    /// </para>
    /// </returns>
    public static T Asinh<T>(this T x) where T : IHyperbolicFunctions<T> => T.Asinh(x);

    /// <summary>
    /// Returns the angle whose tangent is the specified number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">A number representing a tangent.</param>
    /// <returns>
    /// <para>
    /// An angle, θ, measured in radians, such that -π/2 ≤ θ ≤ π/2.
    /// </para>
    /// <para>
    /// -or- <see cref="IFloatingPointIeee754{TSelf}.NaN"/> if <paramref name="x"/> equals
    /// <see cref="IFloatingPointIeee754{TSelf}.NaN"/>, -π/2 if <paramref name="x"/> equals
    /// <see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/>, or π/2 if <paramref name="x"/> equals
    /// <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/>.
    /// </para>
    /// </returns>
    public static T Atan<T>(this T x) where T : ITrigonometricFunctions<T> => T.Atan(x);

    /// <summary>
    /// Returns the angle whose tangent is the quotient of two specified numbers.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="y">The y coordinate of a point.</param>
    /// <param name="x">The x coordinate of a point.</param>
    /// <returns>
    /// <para>
    /// An angle, θ, measured in radians, such that -π ≤ θ ≤ π, and tan(θ) = <paramref name="y"/> /
    /// <paramref name="x"/>, where (<paramref name="x"/>, <paramref name="y"/>) is
    /// a point in the Cartesian plane.
    /// </para>
    /// <para>
    /// Observe the following:
    /// </para>
    /// <para>
    /// - For (<paramref name="x"/>, <paramref name="y"/>) in quadrant 1, 0 &lt; θ &lt; π/2.
    /// </para>
    /// <para>
    /// - For (<paramref name="x"/>, <paramref name="y"/>) in quadrant 2, π/2 &lt; θ ≤ π.
    /// </para>
    /// <para>
    /// - For (<paramref name="x"/>, <paramref name="y"/>) in quadrant 3, -π &lt; θ &lt; -π/2.
    /// </para>
    /// <para>- For (<paramref name="x"/>, <paramref name="y"/>) in quadrant 4, -π/2 &lt; θ &lt; 0.
    /// </para>
    /// <para>
    /// For points on the boundaries of the quadrants, the return value is the following:
    /// </para>
    /// <para>
    /// - If <paramref name="y"/> is 0 and <paramref name="x"/> is not negative, θ = 0.
    /// </para>
    /// <para>
    /// - If <paramref name="y"/> is 0 and <paramref name="x"/> is negative, θ = π.
    /// </para>
    /// <para>
    /// - If <paramref name="y"/> is positive and <paramref name="x"/> is 0, θ = π/2.
    /// </para>
    /// <para>
    /// - If <paramref name="y"/> is negative and <paramref name="x"/> is 0, θ = -π/2.
    /// </para>
    /// <para>
    /// - If <paramref name="y"/> is 0 and <paramref name="x"/> is 0, θ = 0.
    /// </para>
    /// <para>
    /// If <paramref name="x"/> or <paramref name="y"/> is <see cref="IFloatingPointIeee754{TSelf}.NaN"/>,
    /// or if <paramref name="x"/> and <paramref name="y"/> are either <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/>
    /// or <see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/>, the method returns <see cref="IFloatingPointIeee754{TSelf}.NaN"/>.
    /// </para>
    /// </returns>
    public static T Atan2<T>(this T y, T x) where T : IFloatingPointIeee754<T> => T.Atan2(y, x);

    /// <summary>
    /// Returns the angle whose hyperbolic tangent is the specified number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">
    /// A number representing a hyperbolic tangent, where x must be greater than or equal to -1,
    /// but less than or equal to 1.
    /// </param>
    /// <returns>
    /// <para>
    /// An angle, θ, measured in radians, such that -∞ &lt; θ &lt; -1, or 1 &lt; θ &lt; ∞.
    /// </para>
    /// <para>
    /// -or- <see cref="IFloatingPointIeee754{TSelf}.NaN"/> if <paramref name="x"/> &lt; -1 or d &gt; 1 or
    /// <paramref name="x"/> equals <see cref="IFloatingPointIeee754{TSelf}.NaN"/>.
    /// </para>
    /// </returns>
    public static T Atanh<T>(this T x) where T : IHyperbolicFunctions<T> => T.Atanh(x);

    /// <summary>
    /// Returns the next smallest value that compares less than <paramref name="x"/>.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value to decrement.</param>
    /// <returns>
    /// <para>
    /// The next smallest value that compares less than <paramref name="x"/>.
    /// </para>
    /// <para>
    /// -or- <see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/> if <paramref name="x"/> equals
    /// <see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/>.
    /// </para>
    /// <para>
    /// -or- <see cref="IFloatingPointIeee754{TSelf}.NaN"/> if <paramref name="x"/> equals
    /// <see cref="IFloatingPointIeee754{TSelf}.NaN"/>.
    /// </para>
    /// </returns>
    public static T BitDecrement<T>(this T x) where T : IFloatingPointIeee754<T> => T.BitDecrement(x);

    /// <summary>
    /// Returns the next largest value that compares greater than <paramref name="x"/>.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value to increment.</param>
    /// <returns>
    /// <para>
    /// The next largest value that compares greater than <paramref name="x"/>.
    /// </para>
    /// <para>
    /// -or- <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/> if <paramref name="x"/> equals
    /// <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/>.
    /// </para>
    /// <para>
    /// -or- <see cref="IFloatingPointIeee754{TSelf}.NaN"/> if <paramref name="x"/> equals
    /// <see cref="IFloatingPointIeee754{TSelf}.NaN"/>.
    /// </para>
    /// </returns>
    public static T BitIncrement<T>(this T x) where T : IFloatingPointIeee754<T> => T.BitIncrement(x);

    /// <summary>
    /// Returns the cube root of a specified number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The number whose cube root is to be found.</param>
    /// <returns>
    /// <para>
    /// The cube root of <paramref name="x"/>.
    /// </para>
    /// <para>
    /// -or- <see cref="IFloatingPointIeee754{TSelf}.NaN"/> if <paramref name="x"/> equals
    /// <see cref="IFloatingPointIeee754{TSelf}.NaN"/>.
    /// </para>
    /// </returns>
    public static T Cbrt<T>(this T x) where T : IRootFunctions<T> => T.Cbrt(x);

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
    /// Returns <paramref name="value"/> clamped to the inclusive range of <paramref name="min"/>
    /// and <paramref name="max"/>.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">The value to be clamped.</param>
    /// <param name="min">The lower bound of the result.</param>
    /// <param name="max">The upper bound of the result.</param>
    /// <returns>
    /// <para>
    /// <paramref name="value"/> if <paramref name="min"/> ≤ <paramref name="value"/> ≤ <paramref name="max"/>.
    /// </para>
    /// <para>
    /// -or- <paramref name="min"/> if <paramref name="value"/> &lt; <paramref name="min"/>.
    /// </para>
    /// <para>
    /// -or- <paramref name="max"/> if max &lt; <paramref name="value"/>
    /// </para>
    /// <para>
    /// -or- <see cref="IFloatingPointIeee754{TSelf}.NaN"/> if <typeparamref name="T"/> implements
    /// <see cref="IFloatingPointIeee754{TSelf}"/> and <paramref name="value"/> is
    /// <see cref="IFloatingPointIeee754{TSelf}.NaN"/>.
    /// </para>
    /// </returns>
    public static T Clamp<T>(this T value, T min, T max) where T : INumber<T> => T.Clamp(value, min, max);

    /// <summary>
    /// Returns a value with the magnitude of <paramref name="x"/> and the sign of <paramref name="y"/>.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">A number whose magnitude is used in the result.</param>
    /// <param name="y">A number whose sign is the used in the result.</param>
    /// <returns>
    /// A value with the magnitude of <paramref name="x"/> and the sign of <paramref name="y"/>.
    /// </returns>
    public static T CopySign<T>(this T x, T y) where T : INumber<T> => T.CopySign(x, y);

    /// <summary>
    /// Returns the cosine of the specified angle.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">An angle, measured in radians.</param>
    /// <returns>
    /// <para>
    /// The cosine of <paramref name="x"/>.
    /// </para>
    /// <para>
    /// If <paramref name="x"/> is equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/>,
    /// <see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/>, or
    /// <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/>, this method returns
    /// <see cref="IFloatingPointIeee754{TSelf}.NaN"/>.
    /// </para>
    /// </returns>
    public static T Cos<T>(this T x) where T : ITrigonometricFunctions<T> => T.Cos(x);

    /// <summary>
    /// Returns the hyperbolic cosine of the specified angle.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">An angle, measured in radians.</param>
    /// <returns>
    /// <para>
    /// The hyperbolic cosine of <paramref name="x"/>.
    /// </para>
    /// <para>
    /// If <paramref name="x"/> is equal to <see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/> or
    /// <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/>,
    /// <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/> is returned.
    /// </para>
    /// <para>
    /// If <paramref name="x"/> is equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/>,
    /// <see cref="IFloatingPointIeee754{TSelf}.NaN"/> is returned.
    /// </para>
    /// </returns>
    public static T Cosh<T>(this T x) where T : IHyperbolicFunctions<T> => T.Cosh(x);

    /// <summary>
    /// Create a new instance of <typeparamref name="TTarget"/> from the given <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TSelf">The type of <paramref name="value"/>.</typeparam>
    /// <typeparam name="TTarget">The type to create.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <returns>
    /// A value of type <typeparamref name="TTarget"/> with the same value as <paramref name="value"/>.
    /// </returns>
    /// <remarks>
    /// This method performs a checked conversion.
    /// </remarks>
    public static TTarget CreateChecked<TSelf, TTarget>(this TSelf value)
        where TSelf : INumberBase<TSelf>
        where TTarget : INumberBase<TTarget>
    {
        try
        {
            return TTarget.CreateChecked(value);
        }
        catch (NotSupportedException) when (value is ICreateOther<TSelf> createOther)
        {
            return createOther.CreateChecked<TTarget>();
        }
    }

    /// <summary>
    /// Create a new instance of <typeparamref name="TTarget"/> from the given <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TSelf">The type of <paramref name="value"/>.</typeparam>
    /// <typeparam name="TTarget">The type to create.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <returns>
    /// <para>
    /// A value of type <typeparamref name="TTarget"/> with the same value as <paramref name="value"/>.
    /// </para>
    /// <para>
    /// -or- if <paramref name="value"/> is less than the minimum allowed value of
    /// <typeparamref name="TTarget"/>, the minimum allowed value.
    /// </para>
    /// <para>
    /// -or- if <paramref name="value"/> is greater than the maximum allowed value of
    /// <typeparamref name="TTarget"/>, the maximum allowed value.
    /// </para>
    /// </returns>
    /// <remarks>
    /// This method performs a saturating (clamped) conversion.
    /// </remarks>
    public static TTarget CreateSaturating<TSelf, TTarget>(TSelf value)
        where TSelf : INumberBase<TSelf>
        where TTarget : INumberBase<TTarget>
    {
        try
        {
            return TTarget.CreateSaturating(value);
        }
        catch (NotSupportedException) when (value is ICreateOther<TSelf> createOther)
        {
            return createOther.CreateSaturating<TTarget>();
        }
    }

    /// <summary>
    /// Create a new instance of <typeparamref name="TTarget"/> from the given <paramref name="value"/>.
    /// </summary>
    /// <typeparam name="TSelf">The type of <paramref name="value"/>.</typeparam>
    /// <typeparam name="TTarget">The type to create.</typeparam>
    /// <param name="value">The value to convert.</param>
    /// <returns>
    /// A value of type <typeparamref name="TTarget"/> with the same value as <paramref name="value"/>.
    /// </returns>
    /// <remarks>
    /// This method performs a truncating conversion.
    /// </remarks>
    public static TTarget CreateTruncating<TSelf, TTarget>(TSelf value)
        where TSelf : INumberBase<TSelf>
        where TTarget : INumberBase<TTarget>
    {
        try
        {
            return TTarget.CreateTruncating(value);
        }
        catch (NotSupportedException) when (value is ICreateOther<TSelf> createOther)
        {
            return createOther.CreateTruncating<TTarget>();
        }
    }

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
    /// Calculates the quotient and remainder of two numbers.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="left">The dividend.</param>
    /// <param name="right">The divisor.</param>
    /// <returns>
    /// <para>
    /// The quotient of the specified numbers, and the remainder.
    /// </para>
    /// <para>
    /// -or- <see cref="IFloatingPointIeee754{TSelf}.NaN"/> if <typeparamref name="T"/> implements
    /// <see cref="IFloatingPointIeee754{TSelf}"/> and <paramref name="left"/> or <paramref name="right"/>
    /// is <see cref="IFloatingPointIeee754{TSelf}.NaN"/>, or <paramref name="right"/> is zero.
    /// </para>
    /// </returns>
    /// <exception cref="DivideByZeroException">
    /// if <paramref name="right"/> is zero and <typeparamref name="T"/> does not implement <see cref="IFloatingPointIeee754{TSelf}"/>
    /// </exception>
    public static (T Quotient, T Remainder) DivRem<T>(this T left, T right) where T : IBinaryInteger<T> => T.DivRem(left, right);

    /// <summary>
    /// Returns e raised to the specified power.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">A number specifying a power.</param>
    /// <returns>
    /// <para>
    /// The number e raised to the power <paramref name="x"/>.
    /// </para>
    /// <para>
    /// If <paramref name="x"/> equals <see cref="IFloatingPointIeee754{TSelf}.NaN"/>
    /// or <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/>, that value is returned.
    /// </para>
    /// <para>
    /// If <paramref name="x"/> equals <see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/>, 0 is
    /// returned.
    /// </para>
    /// </returns>
    public static T Exp<T>(this T x) where T : IExponentialFunctions<T> => T.Exp(x);

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
    /// Returns (x * y) + z, rounded as one ternary operation.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="left">The number to be multiplied with <paramref name="right"/>.</param>
    /// <param name="right">The number to be multiplied with <paramref name="left"/>.</param>
    /// <param name="addend">
    /// The number to be added to the result of <paramref name="left"/> multiplied by <paramref name="right"/>.
    /// </param>
    /// <returns>
    /// (x * y) + z, rounded as one ternary operation.
    /// </returns>
    public static T FusedMultiplyAdd<T>(this T left, T right, T addend) where T : IFloatingPointIeee754<T>
        => T.FusedMultiplyAdd(left, right, addend);

    /// <summary>
    /// Returns the remainder resulting from the division of a specified number by another specified number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="left">A dividend.</param>
    /// <param name="right">A divisor.</param>
    /// <returns>
    /// <para>
    /// The largest integral value that is less than or equal to <paramref name="left"/>.
    /// </para>
    /// <para>
    /// A number equal to <paramref name="left"/> - (<paramref name="right"/> Q), where Q is the
    /// quotient of <paramref name="left"/> / <paramref name="right"/> rounded to the nearest
    /// integer (if <paramref name="left"/> / <paramref name="right"/> falls halfway between two
    /// integers, the even integer is returned).
    /// </para>
    /// <para>
    /// If <paramref name="left"/> - (<paramref name="right"/> Q) is zero, the value
    /// <see cref="INumberBase{TSelf}.Zero"/> is returned if <paramref name="left"/> is positive,
    /// or <see cref="IFloatingPointIeee754{TSelf}.NegativeZero"/> if <paramref name="left"/> is negative.
    /// </para>
    /// <para>
    /// If <paramref name="right"/> = 0, <see cref="IFloatingPointIeee754{TSelf}.NaN"/> is returned.
    /// </para>
    /// </returns>
    public static T Ieee754Remainder<T>(this T left, T right) where T : IFloatingPointIeee754<T> => T.Ieee754Remainder(left, right);

    /// <summary>
    /// Returns the base 2 integer logarithm of a specified number.
    /// </summary>
    /// <typeparam name="TSelf">The type of number.</typeparam>
    /// <param name="x">The number whose logarithm is to be found.</param>
    /// <returns>
    /// One of the values in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term><paramref name="x"/> parameter</term>
    /// <description>Return value</description>
    /// </listheader>
    /// <item>
    /// <term>Default</term>
    /// <description>
    /// The base 2 integer log of <paramref name="x"/>; that is,
    /// (int)log2(<paramref name="x"/>).
    /// </description>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <description>
    /// <see cref="int.MinValue"/>.
    /// </description>
    /// </item>
    /// <item>
    /// <term>
    /// Equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/> or
    /// <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/> or
    /// <see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/>
    /// </term>
    /// <description>
    /// <see cref="int.MaxValue"/>.
    /// </description>
    /// </item>
    /// </list>
    /// </returns>
    public static int ILogB<TSelf>(this TSelf x)
        where TSelf : IFloatingPointIeee754<TSelf> => TSelf.ILogB(x);

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
    /// cref="IFloatingPointIeee754{TSelf}.NaN"/> if the weight cannot be computed for the given parameters.
    /// </returns>
    /// <remarks>
    /// <see cref="IFloatingPointIeee754{TSelf}.NaN"/> will be returned if the given values are nearly equal, but the
    /// given result is not also nearly equal to them, since the calculation in that case would
    /// require a division by zero.
    /// </remarks>
    public static T InverseLerp<T>(this T first, T second, T result) where T : IFloatingPointIeee754<T>
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
    /// Determines whether the specified value is finite (zero, subnormal, or normal).
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">A number.</param>
    /// <returns>
    /// <see langword="true"/> if the value is finite (zero, subnormal or normal);
    /// <see langword="false"/> otherwise.
    /// </returns>
    public static bool IsFinite<T>(this T x) where T : INumberBase<T> => T.IsFinite(x);

    /// <summary>
    /// Returns a value indicating whether the specified number evaluates to negative or positive
    /// infinity.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">A number.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="x"/> evaluates to
    /// <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/> or
    /// <see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsInfinity<T>(this T x) where T : INumberBase<T> => T.IsInfinity(x);

    /// <summary>
    /// Returns a value that indicates whether the specified value is not a number
    /// (<see cref="IFloatingPointIeee754{TSelf}.NaN"/>).
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">A number.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="x"/> evaluates to
    /// <see cref="IFloatingPointIeee754{TSelf}.NaN"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsNaN<T>(this T x) where T : INumberBase<T> => T.IsNaN(x);

    /// <summary>
    /// Determines whether the specified value is negative.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">A number.</param>
    /// <returns>
    /// <see langword="true"/> if the value is negative; <see langword="false"/> otherwise.
    /// </returns>
    public static bool IsNegative<T>(this T x) where T : INumberBase<T> => T.IsNegative(x);

    /// <summary>
    /// Determines whether the specified value is normal.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">A number.</param>
    /// <returns>
    /// <see langword="true"/> if the value is normal; <see langword="false"/> otherwise.
    /// </returns>
    public static bool IsNormal<T>(this T x) where T : INumberBase<T> => T.IsNormal(x);

    /// <summary>
    /// Returns a value indicating whether the specified number evaluates to negative infinity.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">A number.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="x"/> evaluates to
    /// <see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsNegativeInfinity<T>(this T x) where T : INumberBase<T> => T.IsNegativeInfinity(x);

    /// <summary>
    /// Returns a value indicating whether the specified number evaluates to positive infinity.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">A number.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="x"/> evaluates to
    /// <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsPositiveInfinity<T>(this T x) where T : INumberBase<T> => T.IsPositiveInfinity(x);

    /// <summary>
    /// Determines whether the specified value is subnormal.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">A number.</param>
    /// <returns>
    /// <see langword="true"/> if the value is subnormal; <see langword="false"/> otherwise.
    /// </returns>
    public static bool IsSubnormal<T>(this T x) where T : INumberBase<T> => T.IsSubnormal(x);

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
        var nearlyZero = NumberValues.NearlyZeroDouble.TryCreate(out T? nz)
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
    /// Returns the natural (base e) logarithm of a specified number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The number whose logarithm is to be found.</param>
    /// <returns>
    /// One of the values in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term><paramref name="x"/> parameter</term>
    /// <description>Return value</description>
    /// </listheader>
    /// <item>
    /// <term>Positive</term>
    /// <description>
    /// The natural logarithm of <paramref name="x"/>; that is, ln <paramref name="x"/>, or log e
    /// <paramref name="x"/>
    /// </description>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/></description>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></description>
    /// </item>
    /// </list>
    /// </returns>
    public static T Log<T>(this T x) where T : ILogarithmicFunctions<T> => T.Log(x);

#pragma warning disable RCS1243 // Duplicate word in a comment.
    /// <summary>
    /// Returns the logarithm of a specified number in a specified base.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The number whose logarithm is to be found.</param>
    /// <param name="newBase">The base of the logarithm.</param>
    /// <returns>
    /// One of the values in the following table. (+Infinity denotes
    /// <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/>, -Infinity denotes
    /// <see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/>, and NaN denotes
    /// <see cref="IFloatingPointIeee754{TSelf}.NaN"/>.)
    /// <list type="table">
    /// <listheader>
    /// <term><paramref name="x"/> parameter</term>
    /// <description><paramref name="newBase"/> Return value</description>
    /// </listheader>
    /// <item>
    /// <term><paramref name="x"/> &gt; 0</term>
    /// <description>
    /// (0 &lt; newBase &lt; 1) -or- (newBase &gt; 1) log<sub>newBase</sub>(<paramref name="x"/>)
    /// </description>
    /// </item>
    /// <item>
    /// <term><paramref name="x"/> &lt; 0</term>
    /// <description>(any value) NaN</description>
    /// </item>
    /// <item>
    /// <term>(any value)</term>
    /// <description>newBase &lt; 0 NaN</description>
    /// </item>
    /// <item>
    /// <term><paramref name="x"/> != 1</term>
    /// <description>newBase = 0 NaN</description>
    /// </item>
    /// <item>
    /// <term><paramref name="x"/> != 1</term>
    /// <description>newBase = +Infinity NaN</description>
    /// </item>
    /// <item>
    /// <term><paramref name="x"/> = NaN</term>
    /// <description>(any value) NaN</description>
    /// </item>
    /// <item>
    /// <term>(any value)</term>
    /// <description>newBase = NaN NaN</description>
    /// </item>
    /// <item>
    /// <term>(any value)</term>
    /// <description>newBase = 1 NaN</description>
    /// </item>
    /// <item>
    /// <term><paramref name="x"/> = 0</term>
    /// <description>0 &lt; newBase &lt; 1 +Infinity</description>
    /// </item>
    /// <item>
    /// <term><paramref name="x"/> = 0</term>
    /// <description>newBase &gt; 1 -Infinity</description>
    /// </item>
    /// <item>
    /// <term><paramref name="x"/> = +Infinity</term>
    /// <description>0 &lt; newBase &lt; 1 -Infinity</description>
    /// </item>
    /// <item>
    /// <term><paramref name="x"/> = +Infinity</term>
    /// <description>newBase &gt; 1 +Infinity</description>
    /// </item>
    /// <item>
    /// <term><paramref name="x"/> = 1</term>
    /// <description>newBase = 0 0</description>
    /// </item>
    /// <item>
    /// <term><paramref name="x"/> = 1</term>
    /// <description>newBase = +Infinity 0</description>
    /// </item>
    /// </list>
    /// </returns>
    public static T Log<T>(this T x, T newBase) where T : ILogarithmicFunctions<T> => T.Log(x, newBase);
#pragma warning restore RCS1243 // Duplicate word in a comment.

    /// <summary>
    /// Returns the base 10 logarithm of a specified number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The number whose logarithm is to be found.</param>
    /// <returns>
    /// One of the values in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term><paramref name="x"/> parameter</term>
    /// <description>Return value</description>
    /// </listheader>
    /// <item>
    /// <term>Positive</term>
    /// <description>
    /// The base 10 log of <paramref name="x"/>; that is, log<sub>10</sub><paramref name="x"/>.
    /// </description>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/></description>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></description>
    /// </item>
    /// </list>
    /// </returns>
    public static T Log10<T>(this T x) where T : ILogarithmicFunctions<T> => T.Log10(x);

    /// <summary>
    /// Returns the base 2 logarithm of a specified number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The number whose logarithm is to be found.</param>
    /// <returns>
    /// One of the values in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term><paramref name="x"/> parameter</term>
    /// <description>Return value</description>
    /// </listheader>
    /// <item>
    /// <term>Positive</term>
    /// <description>
    /// The base 2 log of <paramref name="x"/>; that is, log<sub>2</sub><paramref name="x"/>.
    /// </description>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/></description>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></description>
    /// </item>
    /// </list>
    /// </returns>
    public static T Log2<T>(this T x) where T : ILogarithmicFunctions<T> => T.Log2(x);

    /// <summary>
    /// Returns the larger of two numbers.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The first of two numbers to compare.</param>
    /// <param name="y">The second of two numbers to compare.</param>
    /// <returns>
    /// <para>
    /// Parameter <paramref name="x"/> or <paramref name="y"/>, whichever is larger.
    /// </para>
    /// <para>
    /// If <typeparamref name="T"/> implements <see cref="IFloatingPointIeee754{TSelf}"/> and
    /// <paramref name="x"/>, <paramref name="y"/>, or both <paramref name="x"/> and
    /// <paramref name="y"/> are equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/>,
    /// <see cref="IFloatingPointIeee754{TSelf}.NaN"/> is returned.
    /// </para>
    /// </returns>
    public static T Max<T>(this T x, T y) where T : INumber<T> => T.Max(x, y);

    /// <summary>
    /// Returns the larger magnitude of two numbers.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The first of two numbers to compare.</param>
    /// <param name="y">The second of two numbers to compare.</param>
    /// <returns>
    /// <para>
    /// Parameter <paramref name="x"/> or <paramref name="y"/>, whichever has the larger magnitude.
    /// </para>
    /// <para>
    /// If <paramref name="x"/>, or <paramref name="y"/>, or both <paramref name="x"/> and
    /// <paramref name="y"/> are equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/>,
    /// <see cref="IFloatingPointIeee754{TSelf}.NaN"/> is returned.
    /// </para>
    /// </returns>
    public static T MaxMagnitude<T>(this T x, T y) where T : INumberBase<T> => T.MaxMagnitude(x, y);

    /// <summary>
    /// Returns the smaller of two numbers.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The first of two numbers to compare.</param>
    /// <param name="y">The second of two numbers to compare.</param>
    /// <returns>
    /// <para>
    /// Parameter <paramref name="x"/> or <paramref name="y"/>, whichever is smaller.
    /// </para>
    /// <para>
    /// If <typeparamref name="T"/> implements <see cref="IFloatingPointIeee754{TSelf}"/> and
    /// <paramref name="x"/>, <paramref name="y"/>, or both <paramref name="x"/> and
    /// <paramref name="y"/> are equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/>,
    /// <see cref="IFloatingPointIeee754{TSelf}.NaN"/> is returned.
    /// </para>
    /// </returns>
    public static T Min<T>(this T x, T y) where T : INumber<T> => T.Min(x, y);

    /// <summary>
    /// Returns the smaller magnitude of two numbers.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The first of two numbers to compare.</param>
    /// <param name="y">The second of two numbers to compare.</param>
    /// <returns>
    /// <para>
    /// Parameter <paramref name="x"/> or <paramref name="y"/>, whichever has the smaller magnitude.
    /// </para>
    /// <para>
    /// If <paramref name="x"/>, or <paramref name="y"/>, or both <paramref name="x"/> and
    /// <paramref name="y"/> are equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/>,
    /// <see cref="IFloatingPointIeee754{TSelf}.NaN"/> is returned.
    /// </para>
    /// </returns>
    public static T MinMagnitude<T>(this T x, T y) where T : INumberBase<T> => T.MinMagnitude(x, y);

    /// <summary>
    /// Converts the string representation of a number in a specified style and culture-specific
    /// format to its number equivalent.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="s">A string that contains a number to convert.</param>
    /// <param name="style">
    /// A bitwise combination of enumeration values that indicate the style elements
    /// that can be present in <paramref name="s"/>.
    /// </param>
    /// <param name="provider">
    /// An object that supplies culture-specific formatting information about <paramref name="s"/>.
    /// </param>
    /// <returns>
    /// A number that is equivalent to the numeric value or symbol specified in <paramref name="s"/>.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="s"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="FormatException">
    /// <paramref name="s"/> does not represent a numeric value.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// <para>
    /// <paramref name="style"/> is not a <see cref="NumberStyles"/> value.
    /// </para>
    /// <para>
    /// -or- <paramref name="style"/> is not an allowed value for <typeparamref name="T"/>.
    /// </para>
    /// </exception>
    public static T Parse<T>(this string s, NumberStyles style, IFormatProvider? provider)
        where T : INumber<T> => T.Parse(s, style, provider);

    /// <summary>
    /// Converts a character span that contains the string representation of a number in a
    /// specified style and culture-specific format to its number equivalent.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="s">A character span that contains the number to convert.</param>
    /// <param name="style">
    /// A bitwise combination of enumeration values that indicate the style elements
    /// that can be present in <paramref name="s"/>.
    /// </param>
    /// <param name="provider">
    /// An object that supplies culture-specific formatting information about <paramref name="s"/>.
    /// </param>
    /// <returns>
    /// A number that is equivalent to the numeric value or symbol specified in <paramref name="s"/>.
    /// </returns>
    /// <exception cref="FormatException">
    /// <paramref name="s"/> does not represent a numeric value.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// <para>
    /// <paramref name="style"/> is not a <see cref="NumberStyles"/> value.
    /// </para>
    /// <para>
    /// -or- <paramref name="style"/> is not an allowed value for <typeparamref name="T"/>.
    /// </para>
    /// </exception>
    public static T Parse<T>(this ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider)
        where T : INumberBase<T> => T.Parse(s, style, provider);

    /// <summary>
    /// Returns a specified number raised to the specified power.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">A number to be raised to a power.</param>
    /// <param name="y">A number that specifies a power.</param>
    /// <returns>
    /// The number <paramref name="x"/> raised to the power <paramref name="y"/>.
    /// </returns>
    public static T Pow<T>(this T x, T y) where T : IPowerFunctions<T> => T.Pow(x, y);

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
        where TSelf : IFloatingPointIeee754<TSelf> => TSelf.Round(x, digits, mode);

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

        return SafeTypeConvert<ulong, T>(T.Round(value, MidpointRounding.AwayFromZero));
    }

    /// <summary>
    /// Returns x * 2^n computed efficiently.
    /// </summary>
    /// <typeparam name="TSelf">The type of number.</typeparam>
    /// <param name="x">A number that specifies the base value.</param>
    /// <param name="n">An integer that specifies the power.</param>
    /// <returns>
    /// x * 2^n computed efficiently.
    /// </returns>
    public static TSelf ScaleB<TSelf>(this TSelf x, int n)
        where TSelf : IFloatingPointIeee754<TSelf> => TSelf.ScaleB(x, n);

    /// <summary>
    /// Returns an integer that indicates the sign of a number.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="value">A signed number.</param>
    /// <returns>
    /// A number that indicates the sign of value, as shown in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term>Return value</term>
    /// <description>Meaning</description>
    /// </listheader>
    /// <item>
    /// <term>-1</term>
    /// <description>value is less than zero.</description>
    /// </item>
    /// <item>
    /// <term>0</term>
    /// <description>value is equal to zero.</description>
    /// </item>
    /// <item>
    /// <term>1</term>
    /// <description>value is greater than zero.</description>
    /// </item>
    /// </list>
    /// </returns>
    /// <exception cref="ArithmeticException">
    /// <paramref name="value"/> is <see cref="IFloatingPointIeee754{TSelf}.NaN"/>
    /// </exception>
    public static int Sign<T>(this T value) where T : INumber<T> => T.Sign(value);

    /// <summary>
    /// Returns the sine of the specified angle.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">An angle, measured in radians.</param>
    /// <returns>
    /// <para>
    /// The sine of <paramref name="x"/>.
    /// </para>
    /// <para>
    /// If <paramref name="x"/> is equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/>,
    /// <see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/>, or
    /// <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/>, this method returns
    /// <see cref="IFloatingPointIeee754{TSelf}.NaN"/>.
    /// </para>
    /// </returns>
    public static T Sin<T>(this T x) where T : ITrigonometricFunctions<T> => T.Sin(x);

    /// <summary>
    /// Returns the hyperbolic sine of the specified angle.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">An angle, measured in radians.</param>
    /// <returns>
    /// <para>
    /// The hyperbolic sine of <paramref name="x"/>.
    /// </para>
    /// <para>
    /// If <paramref name="x"/> is equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/>,
    /// <see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/>, or
    /// <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/>, this method returns
    /// <paramref name="x"/>.
    /// </para>
    /// </returns>
    public static T Sinh<T>(this T x) where T : IHyperbolicFunctions<T> => T.Sinh(x);

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
    public static T SnapToZero<T>(this T value) where T : IFloatingPointIeee754<T>
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
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The number whose square root is to be found.</param>
    /// <returns>
    /// One of the values in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term><paramref name="x"/> parameter</term>
    /// <description>Return value</description>
    /// </listheader>
    /// <item>
    /// <term>Zero or positive</term>
    /// <description>
    /// The positive square root of <paramref name="x"/>.
    /// </description>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></description>
    /// </item>
    /// </list>
    /// </returns>
    public static T Sqrt<T>(this T x) where T : IRootFunctions<T> => T.Sqrt(x);

    /// <summary>
    /// Returns the tangent of the specified angle.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">An angle, measured in radians.</param>
    /// <returns>
    /// <para>
    /// The tangent of <paramref name="x"/>.
    /// </para>
    /// <para>
    /// If <paramref name="x"/> is equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/>,
    /// <see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/>, or
    /// <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/>, this method returns
    /// <see cref="IFloatingPointIeee754{TSelf}.NaN"/>.
    /// </para>
    /// </returns>
    public static T Tan<T>(this T x) where T : ITrigonometricFunctions<T> => T.Tan(x);

    /// <summary>
    /// Returns the hyperbolic tangent of the specified angle.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">An angle, measured in radians.</param>
    /// <returns>
    /// <para>
    /// The hyperbolic tangent of <paramref name="x"/>.
    /// </para>
    /// <para>
    /// If <paramref name="x"/> is equal to <see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/>,
    /// this method returns -1. If <paramref name="x"/> is equal to
    /// <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/>, this method returns 1. If
    /// <paramref name="x"/> is equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/>, this method
    /// returns <see cref="IFloatingPointIeee754{TSelf}.NaN"/>.
    /// </para>
    /// </returns>
    public static T Tanh<T>(this T x) where T : IHyperbolicFunctions<T> => T.Tanh(x);

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
        catch (NotSupportedException) when (value is ICreateOther<TSelf> createOther)
        {
            return createOther.TryCreate(out result);
        }
        catch
        {
            result = default;
            return false;
        }
    }

    /// <summary>
    /// Converts the string representation of a number in a specified style and culture-specific
    /// format to its number equivalent. A return value indicates whether the conversion succeeded
    /// or failed.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="s">A string that contains a number to convert.</param>
    /// <param name="style">
    /// A bitwise combination of enumeration values that indicate the style elements
    /// that can be present in <paramref name="s"/>.
    /// </param>
    /// <param name="provider">
    /// An object that supplies culture-specific formatting information about <paramref name="s"/>.
    /// </param>
    /// <param name="result">
    /// When this method returns, contains a number equivalent of the numeric value or symbol
    /// contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion
    /// failed. The conversion fails if the <paramref name="s"/> parameter is
    /// <see langword="null"/> or <see cref="string.Empty"/> or is not in a format compliant with
    /// <paramref name="style"/>, or if <paramref name="style"/> is not a valid combination of
    /// <see cref="NumberStyles"/> enumeration constants. This parameter is passed uninitialized;
    /// any value originally supplied in <paramref name="result"/> will be overwritten.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="s"/> was converted successfully; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    public static bool TryParse<T>(
        [NotNullWhen(true)] this string? s,
        NumberStyles style,
        IFormatProvider? provider,
        out T result)
        where T : INumberBase<T> => T.TryParse(s, style, provider, out result);

    /// <summary>
    /// Converts a character span containing the string representation of a number in a specified
    /// style and culture-specific format to its number equivalent. A return value indicates
    /// whether the conversion succeeded or failed.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="s">A read-only character span that contains the number to convert.</param>
    /// <param name="style">
    /// A bitwise combination of enumeration values that indicate the style elements
    /// that can be present in <paramref name="s"/>.
    /// </param>
    /// <param name="provider">
    /// An object that supplies culture-specific formatting information about <paramref name="s"/>.
    /// </param>
    /// <param name="result">
    /// When this method returns, contains a number equivalent of the numeric value or symbol
    /// contained in <paramref name="s"/>, if the conversion succeeded, or zero if the conversion
    /// failed. The conversion fails if the <paramref name="s"/> parameter is empty or is not in a
    /// format compliant with <paramref name="style"/>, or if <paramref name="style"/> is not a valid
    /// combination of <see cref="NumberStyles"/> enumeration constants. This parameter is passed
    /// uninitialized; any value originally supplied in <paramref name="result"/> will be
    /// overwritten.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="s"/> was converted successfully; otherwise,
    /// <see langword="false"/>.
    /// </returns>
    public static bool TryParse<T>(
        this ReadOnlySpan<char> s,
        NumberStyles style,
        IFormatProvider? provider,
        out T result)
        where T : INumberBase<T> => T.TryParse(s, style, provider, out result);

    internal static TTarget SafeTypeConvert<TTarget, TOther>(TOther value)
        where TTarget : INumberBase<TTarget>
        where TOther : INumberBase<TOther> => value.TryCreate(out TTarget? result)
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
