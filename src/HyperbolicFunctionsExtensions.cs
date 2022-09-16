using System.Numerics;

namespace Tavenem.Mathematics;

/// <summary>
/// Mathematical extension methods.
/// </summary>
public static class HyperbolicFunctionsExtensions
{
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
}
