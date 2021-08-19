namespace System.Numerics;

/// <summary>
/// Placeholder: awaiting language support.
/// </summary>
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public interface IVector<TSelf, TScalar> :
    IAdditionOperators<TSelf, TSelf, TSelf>,
    IAdditiveIdentity<TSelf, TSelf>,
    IComparisonOperators<TSelf, TSelf>,
    IDivisionOperators<TSelf, TSelf, TSelf>,
    IMultiplyOperators<TSelf, TSelf, TSelf>,
    IMultiplicativeIdentity<TSelf, TSelf>,
    IUnaryNegationOperators<TSelf, TSelf>,
    ISpanFormattable,
    ISubtractionOperators<TSelf, TSelf, TSelf>
    where TSelf : IVector<TSelf, TScalar>
{
    public static abstract TSelf Create(TScalar value);
    public static abstract TSelf Create(TScalar[] values);
    public static abstract TSelf Create(TScalar[] values, int startIndex);
    public static abstract TSelf Create(ReadOnlySpan<TScalar> values);

    public static abstract int Count { get; }

    public static abstract TSelf One { get; }

    public static abstract TSelf Zero { get; }

    public TScalar this[int index] { get; }

    public static abstract TSelf Abs(TSelf value);
    public static abstract TScalar Dot(TSelf left, TSelf right);
    public static abstract TSelf Max(TSelf left, TSelf right);
    public static abstract TSelf Min(TSelf left, TSelf right);
    public static abstract TSelf SquareRoot(TSelf value);

    public void CopyTo(TScalar[] destination);
    public void CopyTo(TScalar[] destination, int startIndex);
    public void CopyTo(Span<TScalar> destination);

    public bool TryCopyTo(Span<TScalar> destination);
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
