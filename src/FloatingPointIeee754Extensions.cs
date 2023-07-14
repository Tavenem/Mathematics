using System.Numerics;

namespace Tavenem.Mathematics;

/// <summary>
/// Mathematical extension methods.
/// </summary>
public static class FloatingPointIeee754Extensions
{
    /// <summary>
    /// Computes the arc-tangent for the quotient of two values.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="y">The y-coordinate of a point.</param>
    /// <param name="x">The x-coordinate of a point.</param>
    /// <returns>
    /// <para>
    /// An angle, θ, measured in radians, such that -π ≤ θ ≤ π, and tan(θ) = <paramref name="y"/> /
    /// <paramref name="x"/>, where (<paramref name="x"/>, <paramref name="y"/>) is a point in the
    /// Cartesian plane.
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
    /// If <paramref name="x"/> or <paramref name="y"/> is <see
    /// cref="IFloatingPointIeee754{TSelf}.NaN"/>, or if <paramref name="x"/> and <paramref
    /// name="y"/> are either <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/> or <see
    /// cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/>, the method returns <see
    /// cref="IFloatingPointIeee754{TSelf}.NaN"/>.
    /// </para>
    /// </returns>
    public static T Atan2<T>(this T y, T x) where T : IFloatingPointIeee754<T> => T.Atan2(y, x);

    /// <summary>
    /// Computes the arc-tangent for the quotient of two values and divides the result by <c>π</c>.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="y">The y-coordinate of a point.</param>
    /// <param name="x">The x-coordinate of a point.</param>
    /// <returns>
    /// <para>
    /// An angle, θ, measured in radians, such that -1 ≤ θ ≤ 1, and tan(θπ) = <paramref name="y"/> /
    /// <paramref name="x"/>, where (<paramref name="x"/>, <paramref name="y"/>) is a point in the
    /// Cartesian plane.
    /// </para>
    /// <para>
    /// Observe the following:
    /// </para>
    /// <para>
    /// - For (<paramref name="x"/>, <paramref name="y"/>) in quadrant 1, 0 &lt; θ &lt; 1/2.
    /// </para>
    /// <para>
    /// - For (<paramref name="x"/>, <paramref name="y"/>) in quadrant 2, 1/2 &lt; θ ≤ 1.
    /// </para>
    /// <para>
    /// - For (<paramref name="x"/>, <paramref name="y"/>) in quadrant 3, -1 &lt; θ &lt; -1/2.
    /// </para>
    /// <para>- For (<paramref name="x"/>, <paramref name="y"/>) in quadrant 4, -1/2 &lt; θ &lt; 0.
    /// </para>
    /// <para>
    /// For points on the boundaries of the quadrants, the return value is the following:
    /// </para>
    /// <para>
    /// - If <paramref name="y"/> is 0 and <paramref name="x"/> is not negative, θ = 0.
    /// </para>
    /// <para>
    /// - If <paramref name="y"/> is 0 and <paramref name="x"/> is negative, θ = 1.
    /// </para>
    /// <para>
    /// - If <paramref name="y"/> is positive and <paramref name="x"/> is 0, θ = 1/2.
    /// </para>
    /// <para>
    /// - If <paramref name="y"/> is negative and <paramref name="x"/> is 0, θ = -1/2.
    /// </para>
    /// <para>
    /// - If <paramref name="y"/> is 0 and <paramref name="x"/> is 0, θ = 0.
    /// </para>
    /// <para>
    /// If <paramref name="x"/> or <paramref name="y"/> is <see
    /// cref="IFloatingPointIeee754{TSelf}.NaN"/>, or if <paramref name="x"/> and <paramref
    /// name="y"/> are either <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/> or <see
    /// cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/>, the method returns <see
    /// cref="IFloatingPointIeee754{TSelf}.NaN"/>.
    /// </para>
    /// </returns>
    public static T Atan2Pi<T>(this T y, T x) where T : IFloatingPointIeee754<T> => T.Atan2Pi(y, x);

    /// <summary>
    /// Decrements a value to the smallest value that compares less than a given value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value to be bitwise decremented.</param>
    /// <returns>
    /// The smallest value that compares less than <paramref name="x"/>.
    /// </returns>
    public static T BitDecrement<T>(this T x) where T : IFloatingPointIeee754<T> => T.BitDecrement(x);

    /// <summary>
    /// Increments a value to the smallest value that compares greater than a given value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value to be bitwise incremented.</param>
    /// <returns>
    /// The smallest value that compares greater than <paramref name="x"/>.
    /// </returns>
    public static T BitIncrement<T>(this T x) where T : IFloatingPointIeee754<T> => T.BitIncrement(x);

    /// <summary>
    /// Returns (x * y) + z, rounded as one ternary operation.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="left">The value which <paramref name="right"/> multiplies.</param>
    /// <param name="right">The value which multiplies <paramref name="left"/>.</param>
    /// <param name="addend">
    /// The value that is added to the product of <paramref name="left"/> and <paramref name="right"/>.
    /// </param>
    /// <returns>
    /// The result of <paramref name="left"/> times <paramref name="right"/> plus <paramref
    /// name="addend"/> computed as one ternary operation.
    /// </returns>
    public static T FusedMultiplyAdd<T>(this T left, T right, T addend) where T : IFloatingPointIeee754<T>
        => T.FusedMultiplyAdd(left, right, addend);

    /// <summary>
    /// Computes the remainder of two values as specified by IEEE 754.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="left">The value which <paramref name="right"/> divides.</param>
    /// <param name="right">The value which divides <paramref name="left"/>.</param>
    /// <returns>
    /// The remainder of <paramref name="left"/> divided by <paramref name="right"/> as specified by
    /// IEEE 754.
    /// </returns>
    public static T Ieee754Remainder<T>(this T left, T right) where T : IFloatingPointIeee754<T> => T.Ieee754Remainder(left, right);

    /// <summary>
    /// Computes the integer logarithm of a value.
    /// </summary>
    /// <typeparam name="TSelf">The type of number.</typeparam>
    /// <param name="x">The value whose integer logarithm is to be computed.</param>
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
    /// The base 2 integer log of <paramref name="x"/>; that is, (int)log2(<paramref name="x"/>).
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
    /// Equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/> or <see
    /// cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/> or <see
    /// cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/>
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
        if (difference.IsNearlyZero())
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
    /// Computes an estimate of the reciprocal of a value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value whose estimate of the reciprocal is to be computed.</param>
    /// <returns>An estimate of the reciprocal of <paramref name="x"/>.</returns>
    public static T ReciprocalEstimate<T>(this T x) where T : IFloatingPointIeee754<T> => T.ReciprocalEstimate(x);

    /// <summary>
    /// Computes an estimate of the reciprocal square root of a value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">
    /// The value whose estimate of the reciprocal square root is to be computed.
    /// </param>
    /// <returns>An estimate of the reciprocal square root of <paramref name="x"/>.</returns>
    public static T ReciprocalSqrtEstimate<T>(this T x) where T : IFloatingPointIeee754<T> => T.ReciprocalSqrtEstimate(x);

    /// <summary>
    /// Computes the product of a value and its base-radix raised to the specified power.
    /// </summary>
    /// <typeparam name="TSelf">The type of number.</typeparam>
    /// <param name="x">
    /// The value which base-radix raised to the power of <paramref name="n"/> multiplies.
    /// </param>
    /// <param name="n">
    /// The value to which base-radix is raised before multiplying <paramref name="x"/>.
    /// </param>
    /// <returns>
    /// The product of <paramref name="x"/> and base-radix raised to the power of <paramref
    /// name="n"/>.
    /// </returns>
    public static TSelf ScaleB<TSelf>(this TSelf x, int n)
        where TSelf : IFloatingPointIeee754<TSelf> => TSelf.ScaleB(x, n);
}
