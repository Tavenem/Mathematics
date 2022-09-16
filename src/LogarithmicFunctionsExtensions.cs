using System.Numerics;

namespace Tavenem.Mathematics;

/// <summary>
/// Mathematical extension methods.
/// </summary>
public static class LogarithmicFunctionsExtensions
{
    /// <summary>
    /// Returns the natural (base <c>e</c>) logarithm of a specified number.
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
    /// The natural logarithm of <paramref name="x"/>; that is, ln <paramref name="x"/>, or
    /// log<sub>e</sub> <paramref name="x"/>
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
    /// Computes the logarithm of a value in the specified base.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value whose logarithm is to be computed.</param>
    /// <param name="newBase">The base in which the logarithm is to be computed.</param>
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
    /// Computes the base-10 logarithm of a value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value whose base-10 logarithm is to be computed.</param>
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
    /// Computes the base-10 logarithm of a value plus one.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">
    /// The value to which one is added before computing the base-10 logarithm.
    /// </param>
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
    /// The base 10 log of <paramref name="x"/> plus one; that is, log<sub>10</sub>(<paramref name="x"/>+1).
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
    public static T Log10P1<T>(this T x) where T : ILogarithmicFunctions<T> => T.Log10P1(x);

    /// <summary>
    /// Computes the base-2 logarithm of a value.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">The value whose base-2 logarithm is to be computed.</param>
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
    /// Computes the base-2 logarithm of a value plus one.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">
    /// The value to which one is added before computing the base-2 logarithm.
    /// </param>
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
    /// The base 2 log of <paramref name="x"/> plus one; that is, log<sub>2</sub>(<paramref name="x"/>+1).
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
    public static T Log2P1<T>(this T x) where T : ILogarithmicFunctions<T> => T.Log2P1(x);

    /// <summary>
    /// Returns the natural (base <c>e</c>) logarithm of a value plus one.
    /// </summary>
    /// <typeparam name="T">The type of number.</typeparam>
    /// <param name="x">
    /// The value to which one is added before computing the natural logarithm.
    /// </param>
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
    /// The natural logarithm of <paramref name="x"/>; that is, ln(<paramref name="x"/>+1), or
    /// log<sub>e</sub>(<paramref name="x"/>+1)
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
    public static T LogP1<T>(this T x) where T : ILogarithmicFunctions<T> => T.LogP1(x);
}
