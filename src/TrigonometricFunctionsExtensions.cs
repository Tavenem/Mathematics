using System.Numerics;

namespace Tavenem.Mathematics;

/// <summary>
/// Mathematical extension methods.
/// </summary>
public static class TrigonometricFunctionsExtensions
{
    /// <summary>
    /// Computes the arc-cosine of a value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">
    /// The value, in radians, whose arc-cosine is to be computed.
    /// </param>
    /// <returns>
    /// The arc-cosine of <paramref name="x"/>.
    /// </returns>
    /// <remarks>
    /// This computes <c>arccos(x)</c> in the interval <c>[+0, +π]</c> radians.
    /// </remarks>
    public static T Acos<T>(this T x) where T : ITrigonometricFunctions<T> => T.Acos(x);

    /// <summary>
    /// Computes the arc-cosine of a value and divides the result by <c>π</c>.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">
    /// The value whose arc-cosine is to be computed.
    /// </param>
    /// <returns>
    /// The arc-cosine of <paramref name="x"/>, divided by <c>π</c>.
    /// </returns>
    /// <remarks>
    /// This computes <c>arccos(x) / π</c> in the interval <c>[-0.5, +0.5]</c>.
    /// </remarks>
    public static T AcosPi<T>(this T x) where T : ITrigonometricFunctions<T> => T.AcosPi(x);

    /// <summary>
    /// Computes the arc-sine of a value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">
    /// The value, in radians, whose arc-sine is to be computed.
    /// </param>
    /// <returns>
    /// The arc-sine of <paramref name="x"/>.
    /// </returns>
    /// <remarks>
    /// This computes <c>arcsin(x)</c> in the interval <c>[-π/2, +π/2]</c> radians.
    /// </remarks>
    public static T Asin<T>(this T x) where T : ITrigonometricFunctions<T> => T.Asin(x);

    /// <summary>
    /// Computes the arc-sine of a value and divides the result by <c>π</c>.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">
    /// The value whose arc-sine is to be computed.
    /// </param>
    /// <returns>
    /// The arc-sine of <paramref name="x"/>, divided by <c>π</c>.
    /// </returns>
    /// <remarks>
    /// This computes <c>arcsin(x) / π</c> in the interval <c>[-0.5, +0.5]</c>.
    /// </remarks>
    public static T AsinPi<T>(this T x) where T : ITrigonometricFunctions<T> => T.AsinPi(x);

    /// <summary>
    /// Computes the arc-tangent of a value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value, in radians, whose arc-tangent is to be computed.</param>
    /// <returns>
    /// The arc-tangent of <paramref name="x"/>.
    /// </returns>
    /// <remarks>
    /// This computes <c>arctan(x)</c> in the interval <c>[-π/2, +π/2]</c> radians.
    /// </remarks>
    public static T Atan<T>(this T x) where T : ITrigonometricFunctions<T> => T.Atan(x);

    /// <summary>
    /// Computes the arc-tangent of a value and divides the result by <c>π</c>.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value whose arc-tangent is to be computed.</param>
    /// <returns>
    /// The arc-tangent of <paramref name="x"/>, divided by <c>π</c>.
    /// </returns>
    /// <remarks>
    /// This computes <c>arctan(x) / π</c> in the interval <c>[-0.5, +0.5]</c>.
    /// </remarks>
    public static T AtanPi<T>(this T x) where T : ITrigonometricFunctions<T> => T.AtanPi(x);

    /// <summary>
    /// Computes the cosine of a value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value, in radians, whose cosine is to be computed.</param>
    /// <returns>
    /// The cosine of <paramref name="x"/>.
    /// </returns>
    /// <remarks>
    /// This computes <c>cos(x)</c>.
    /// </remarks>
    public static T Cos<T>(this T x) where T : ITrigonometricFunctions<T> => T.Cos(x);

    /// <summary>
    /// Computes the cosine of a value that has been multipled by <c>π</c>.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value, in half-revolutions, whose cosine is to be computed.</param>
    /// <returns>
    /// The cosine of <paramref name="x"/> multiplied-by <c>π</c>.
    /// </returns>
    /// <remarks>
    /// This computes <c>cos(πx)</c>.
    /// </remarks>
    public static T CosPi<T>(this T x) where T : ITrigonometricFunctions<T> => T.CosPi(x);

    /// <summary>
    /// Computes the sine of a value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value, in radians, whose sine is to be computed.</param>
    /// <returns>
    /// The sine of <paramref name="x"/>.
    /// </returns>
    /// <remarks>
    /// This computes <c>sin(x)</c>.
    /// </remarks>
    public static T Sin<T>(this T x) where T : ITrigonometricFunctions<T> => T.Sin(x);

    /// <summary>
    /// Computes the sine and cosine of a value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value, in radians, whose sine and cosine are to be computed.</param>
    /// <returns>
    /// The sine and cosine of <paramref name="x"/>.
    /// </returns>
    /// <remarks>
    /// This computes <c>(sin(x), cos(x))</c>.
    /// </remarks>
    public static (T Sin, T Cos) SinCos<T>(this T x) where T : ITrigonometricFunctions<T> => T.SinCos(x);

    /// <summary>
    /// Computes the sine and cosine of a value that has been multipled by <c>π</c>.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">
    /// The value, in half-revolutions, that is multipled by <c>π</c> before computing its sine and
    /// cosine.
    /// </param>
    /// <returns>
    /// The sine and cosine of <paramref name="x"/> multipled-by <c>π</c>.
    /// </returns>
    /// <remarks>
    /// This computes <c>(sin(πx), cos(πx))</c>.
    /// </remarks>
    public static (T SinPi, T CosPi) SinCosPi<T>(this T x) where T : ITrigonometricFunctions<T> => T.SinCosPi(x);

    /// <summary>
    /// Computes the sine of a value that has been multipled by <c>π</c>.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">
    /// The value, in half-revolutions, that is multipled by <c>π</c> before computing its sine.
    /// </param>
    /// <returns>
    /// The sine of <paramref name="x"/> multiplied-by <c>π</c>.
    /// </returns>
    /// <remarks>
    /// This computes <c>sin(πx)</c>.
    /// </remarks>
    public static T SinPi<T>(this T x) where T : ITrigonometricFunctions<T> => T.SinPi(x);

    /// <summary>
    /// Computes the tangent of a value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value, in radians, whose tangent is to be computed.</param>
    /// <returns>
    /// The tangent of <paramref name="x"/>.
    /// </returns>
    /// <remarks>
    /// This computes <c>tan(x)</c>.
    /// </remarks>
    public static T Tan<T>(this T x) where T : ITrigonometricFunctions<T> => T.Tan(x);

    /// <summary>
    /// Computes the tangent of a value that has been multipled by <c>π</c>.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">
    /// The value, in half-revolutions, that is multipled by <c>π</c> before computing its tangent.
    /// </param>
    /// <returns>
    /// The tangent of <paramref name="x"/> multiplied-by <c>π</c>.
    /// </returns>
    /// <remarks>
    /// This computes <c>tan(πx)</c>.
    /// </remarks>
    public static T TanPi<T>(this T x) where T : ITrigonometricFunctions<T> => T.TanPi(x);
}
