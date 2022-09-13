using System.Numerics;

namespace Tavenem.Mathematics;

/// <summary>
/// Extends <see cref="INumber{TSelf}"/> by allowing the creation of other types.
/// </summary>
public interface ICreateOther<TSelf> : INumberBase<TSelf> where TSelf : INumberBase<TSelf>
{
    /// <summary>
    /// Create a new instance of <typeparamref name="TTarget"/> from this instance.
    /// </summary>
    /// <typeparam name="TTarget">The type to create.</typeparam>
    /// <returns>
    /// A value of type <typeparamref name="TTarget"/> with the same value this instance.
    /// </returns>
    /// <remarks>
    /// This method performs a checked conversion.
    /// </remarks>
    TTarget CreateChecked<TTarget>();

    /// <summary>
    /// Create a new instance of <typeparamref name="TTarget"/> from this instance.
    /// </summary>
    /// <typeparam name="TTarget">The type to create.</typeparam>
    /// <returns>
    /// <para>
    /// A value of type <typeparamref name="TTarget"/> with the same value as this instance.
    /// </para>
    /// <para>
    /// -or- if this instance is less than the minimum allowed value of
    /// <typeparamref name="TTarget"/>, the minimum allowed value.
    /// </para>
    /// <para>
    /// -or- if this instance is greater than the maximum allowed value of
    /// <typeparamref name="TTarget"/>, the maximum allowed value.
    /// </para>
    /// </returns>
    /// <remarks>
    /// This method performs a saturating (clamped) conversion.
    /// </remarks>
    TTarget CreateSaturating<TTarget>();

    /// <summary>
    /// Create a new instance of <typeparamref name="TTarget"/> from this instance.
    /// </summary>
    /// <typeparam name="TTarget">The type to create.</typeparam>
    /// <returns>
    /// A value of type <typeparamref name="TTarget"/> with the same value as this instance.
    /// </returns>
    /// <remarks>
    /// This method performs a truncating conversion.
    /// </remarks>
    TTarget CreateTruncating<TTarget>();

    /// <summary>
    /// Attempts to create a new instance of <typeparamref name="TTarget"/> from this instance.
    /// </summary>
    /// <typeparam name="TTarget">The type to create.</typeparam>
    /// <param name="result">
    /// If this method returns <see langword="true"/>, this will be set to a value of type
    /// <typeparamref name="TTarget"/> with the same value as this instance.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the conversion succeeded; otherwise <see langword="false"/>.
    /// </returns>
    bool TryCreate<TTarget>(out TTarget result);
}
