using System.Numerics;

namespace Tavenem.Mathematics;

/// <summary>
/// Mathematical extension methods.
/// </summary>
public static class RootFunctionsExtensions
{
    /// <summary>
    /// Computes the cube-root of a value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value whose cube-root is to be computed.</param>
    /// <returns>
    /// The cube root of <paramref name="x"/>.
    /// </returns>
    public static T Cbrt<T>(this T x) where T : IRootFunctions<T> => T.Cbrt(x);

    /// <summary>
    /// Computes the hypotenuse given two values representing the lengths of the shorter sides in a
    /// right-angled triangle.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value to square and add to <paramref name="y"/>.</param>
    /// <param name="y">The value to square and add to <paramref name="x"/>.</param>
    /// <returns>
    /// The square root of <paramref name="x"/>-squared plus <paramref name="y"/>-squared.
    /// </returns>
    public static T Hypot<T>(this T x, T y) where T : IRootFunctions<T> => T.Hypot(x, y);

    /// <summary>
    /// Computes the n-th root of a value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value whose <c>n</c>-th root is to be computed.</param>
    /// <param name="n">The degree of the root to be computed.</param>
    /// <returns>
    /// The <c>n</c>-th root of <paramref name="x"/>.
    /// </returns>
    public static T RootN<T>(this T x, int n) where T : IRootFunctions<T> => T.RootN(x, n);

    /// <summary>
    /// Computes the square-root of a value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value whose square-root is to be computed.</param>
    /// <returns>
    /// The square-root of <paramref name="x"/>.
    /// </returns>
    public static T Sqrt<T>(this T x) where T : IRootFunctions<T> => T.Sqrt(x);
}
