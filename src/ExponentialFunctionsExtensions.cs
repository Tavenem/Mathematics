using System.Numerics;

namespace Tavenem.Mathematics;

/// <summary>
/// Mathematical extension methods.
/// </summary>
public static class ExponentialFunctionsExtensions
{
    /// <summary>
    /// Returns <c>e</c> raised to the specified power.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The power to which <c>e</c> is raised.</param>
    /// <returns>
    /// The number <c>e</c> raised to the power of <paramref name="x"/>.
    /// </returns>
    public static T Exp<T>(this T x) where T : IExponentialFunctions<T> => T.Exp(x);

    /// <summary>
    /// Computes <c>10</c> raised to a given power.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The power to which <c>10</c> is raised.</param>
    /// <returns>
    /// <c>10</c> raised to the power of <paramref name="x"/>.
    /// </returns>
    public static T Exp10<T>(this T x) where T : IExponentialFunctions<T> => T.Exp10(x);

    /// <summary>
    /// Computes <c>10</c> raised to a given power and subtracts one.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The power to which <c>10</c> is raised.</param>
    /// <returns>
    /// <c>10</c> raised to the power of <paramref name="x"/>, minus <c>1</c>.
    /// </returns>
    public static T Exp10M1<T>(this T x) where T : IExponentialFunctions<T> => T.Exp10M1(x);

    /// <summary>
    /// Computes <c>2</c> raised to a given power.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The power to which <c>2</c> is raised.</param>
    /// <returns>
    /// <c>2</c> raised to the power of <paramref name="x"/>.
    /// </returns>
    public static T Exp2<T>(this T x) where T : IExponentialFunctions<T> => T.Exp2(x);

    /// <summary>
    /// Computes <c>2</c> raised to a given power and subtracts one.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The power to which <c>2</c> is raised.</param>
    /// <returns>
    /// <c>2</c> raised to the power of <paramref name="x"/>, minus <c>1</c>.
    /// </returns>
    public static T Exp2M1<T>(this T x) where T : IExponentialFunctions<T> => T.Exp2M1(x);

    /// <summary>
    /// Returns <c>e</c> raised to the specified power and subtracts one.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The power to which <c>e</c> is raised.</param>
    /// <returns>
    /// The number <c>e</c> raised to the power of <paramref name="x"/>, minus <c>1</c>.
    /// </returns>
    public static T ExpM1<T>(this T x) where T : IExponentialFunctions<T> => T.ExpM1(x);
}
