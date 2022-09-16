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
    /// Computes the quotient and remainder of two values.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="left">The value which <paramref name="right"/> divides.</param>
    /// <param name="right">The value which divides <paramref name="left"/>.</param>
    /// <returns>
    /// The quotient and remainder of <paramref name="left"/> divided by <paramref name="right"/>.
    /// </returns>
    public static (T Quotient, T Remainder) DivRem<T>(this T left, T right) where T : IBinaryInteger<T> => T.DivRem(left, right);

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
    /// Computes a value raised to a given power.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value which is raised to the power of <paramref name="y"/>.</param>
    /// <param name="y">The power to which <paramref name="x"/> is raised.</param>
    /// <returns>
    /// <paramref name="x"/> raised to the power of <paramref name="y"/>.
    /// </returns>
    public static T Pow<T>(this T x, T y) where T : IPowerFunctions<T> => T.Pow(x, y);
}
