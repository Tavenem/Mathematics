using System.Numerics;

namespace System.Linq;

/// <summary>
/// Mathematical extension methods to <see cref="IEnumerable{T}"/>.
/// </summary>
public static class TavenemMathematicsEnumerableExtensions
{
    /// <summary>
    /// Computes the average of a sequence of values.
    /// </summary>
    /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <param name="source">A sequence of values to calculate the average of.</param>
    /// <returns>The average of the sequence of values.</returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// <paramref name="source"/> contains no elements.
    /// </exception>
    /// <exception cref="OverflowException">
    /// The sum of the elements in the sequence is larger than the maximum value of <typeparamref name="T"/>.
    /// </exception>
    public static T Average<T>(this IEnumerable<T> source) where T : INumberBase<T>
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }
        if (!source.Any())
        {
            throw new InvalidOperationException();
        }
        var sum = T.Zero;
        var count = 0UL;
        foreach (var value in source)
        {
            sum += value;
            count++;
        }
        return sum / T.CreateChecked(count);
    }

    /// <summary>
    /// Computes the average of a sequence of values that are obtained by invoking a transform
    /// function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <typeparam name="TScalar">The return type of the transform function.</typeparam>
    /// <param name="source">A sequence of values to calculate the average of.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The average of the sequence of values.</returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// <paramref name="source"/> contains no elements.
    /// </exception>
    /// <exception cref="OverflowException">
    /// The sum of the elements in the sequence is larger than the maximum value of <typeparamref name="TScalar"/>.
    /// </exception>
    public static TScalar Average<TSource, TScalar>(this IEnumerable<TSource> source, Func<TSource, TScalar> selector)
        where TScalar : INumberBase<TScalar> => source.Select(selector).Average();

    /// <summary>
    /// Computes the sum of a sequence of values.
    /// </summary>
    /// <typeparam name="T">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <param name="source">A sequence of values to calculate the sum of.</param>
    /// <returns>The sum of the sequence of values.</returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="OverflowException">
    /// The sum of the elements in the sequence is larger than the maximum value of <typeparamref name="T"/>.
    /// </exception>
    public static T Sum<T>(this IEnumerable<T> source) where T : INumberBase<T>
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }
        var sum = T.Zero;
        foreach (var value in source)
        {
            sum += value;
        }
        return sum;
    }

    /// <summary>
    /// Computes the sum of a sequence of values that are obtained by invoking a transform
    /// function on each element of the input sequence.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <typeparam name="TScalar">The return type of the transform function.</typeparam>
    /// <param name="source">A sequence of values to calculate the sum of.</param>
    /// <param name="selector">A transform function to apply to each element.</param>
    /// <returns>The sum of the sequence of values.</returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> or <paramref name="selector"/> is <see langword="null"/>.
    /// </exception>
    /// <exception cref="OverflowException">
    /// The sum of the elements in the sequence is larger than the maximum value of <typeparamref name="TScalar"/>.
    /// </exception>
    public static TScalar Sum<TSource, TScalar>(this IEnumerable<TSource> source, Func<TSource, TScalar> selector)
        where TScalar : INumberBase<TScalar> => source.Select(selector).Sum();
}
