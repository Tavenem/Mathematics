using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Text.Json.Serialization;

namespace Tavenem.Mathematics;

/// <summary>
/// A floating-point value which specifies a minimum, maximum, and average value.
/// </summary>
/// <param name="min">The value at which <see cref="Min"/> is to be set.</param>
/// <param name="average">The value at which <see cref="Average"/> is to be set.</param>
/// <param name="max">The value at which <see cref="Max"/> is to be set.</param>
/// <remarks>
/// The relationship of <paramref name="average"/> to <paramref name="min"/> and <paramref
/// name="max"/> is not checked. When using the primary constructor, it is left to the implementer
/// to verify that the given average satisfies any requirements for its usage.
/// </remarks>
[JsonConverter(typeof(Converters.FloatRangeConverter))]
[DebuggerDisplay("{ToString()}")]
public readonly struct FloatRange(float min, float average, float max) :
    IEquatable<FloatRange>,
    IEqualityOperators<FloatRange, FloatRange, bool>,
    IFormattable,
    IMinMaxValue<FloatRange>,
    IParsable<FloatRange>
{
    /// <summary>
    /// A <see cref="FloatRange"/> with all values set to <see cref="float.MaxValue"/>.
    /// </summary>
    public static FloatRange MaxValue { get; } = new(float.MaxValue, float.MaxValue, float.MaxValue);

    /// <summary>
    /// A <see cref="FloatRange"/> with all values set to <see cref="float.MinValue"/>.
    /// </summary>
    public static FloatRange MinValue { get; } = new(float.MinValue, float.MinValue, float.MinValue);

    /// <summary>
    /// A <see cref="FloatRange"/> with all values set to one.
    /// </summary>
    public static FloatRange One { get; } = new(1f, 1f, 1f);

    /// <summary>
    /// A <see cref="FloatRange"/> with all values set to zero.
    /// </summary>
    public static FloatRange Zero { get; } = new();

    /// <summary>
    /// A <see cref="FloatRange"/> with the minimum set to 0, the average set to 0.5, and the
    /// maximum set to 1.
    /// </summary>
    public static FloatRange ZeroToOne { get; } = new(0f, 0.5f, 1f);

    /// <summary>
    /// The average value.
    /// </summary>
    public float Average { get; init; } = average;

    /// <summary>
    /// Whether this range begins and ends at zero.
    /// </summary>
    /// <remarks>
    /// Uses <see cref="FloatingPointExtensions.IsNearlyZero(float)"/> to determine near-equivalence with zero,
    /// rather than strict equality.
    /// </remarks>
    [JsonIgnore]
    public bool IsZero => Min.IsNearlyZero() && Max.IsNearlyZero();

    /// <summary>
    /// The maximum value.
    /// </summary>
    public float Max { get; init; } = max;

    /// <summary>
    /// The minimum value.
    /// </summary>
    public float Min { get; init; } = min;

    /// <summary>
    /// The range of values. (Max-Min, or 1-Min+Max when Min>Max)
    /// </summary>
    [JsonIgnore]
    public float Range => Min > Max
        ? 1 - Min + Max
        : Max - Min;

    /// <summary>
    /// Initializes a new instance of <see cref="FloatRange"/> with all properties set to the same
    /// value.
    /// </summary>
    /// <param name="value">
    /// The value to set for all three properties (<see cref="Average"/>, <see cref="Min"/> and <see
    /// cref="Max"/>).
    /// </param>
    public FloatRange(float value) : this(value, value, value) { }

    /// <summary>
    /// Initializes a new instance of <see cref="FloatRange"/>.
    /// </summary>
    /// <param name="min">The value at which <see cref="Min"/> is to be set.</param>
    /// <param name="max">The value at which <see cref="Max"/> is to be set.</param>
    /// <remarks>
    /// <see cref="Average"/> is set to the mathematical average of <paramref name="min"/> and
    /// <paramref name="max"/>.
    /// </remarks>
    public FloatRange(float min, float max) : this(min, (min + max) / 2, max) { }

    /// <summary>
    /// Attempts to parse the given string as a <see cref="FloatRange"/>.
    /// </summary>
    /// <param name="s">A string.</param>
    /// <returns>
    /// The resulting <see cref="FloatRange"/> value.
    /// </returns>
    /// <exception cref="FormatException">
    /// The provided value is not a valid <see cref="FloatRange"/>.
    /// </exception>
    public static FloatRange Parse(string? s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            throw new FormatException();
        }
        return Parse(s.AsSpan(), CultureInfo.CurrentCulture);
    }

    /// <summary>
    /// Parses the given span as a <see cref="FloatRange"/>.
    /// </summary>
    /// <param name="s">A <see cref="ReadOnlySpan{T}"/> of <see cref="char"/>.</param>
    /// <returns>
    /// The resulting <see cref="FloatRange"/> value.
    /// </returns>
    /// <exception cref="FormatException">
    /// The provided value is not a valid <see cref="FloatRange"/>.
    /// </exception>
    public static FloatRange Parse(in ReadOnlySpan<char> s)
        => Parse(s, CultureInfo.CurrentCulture);

    /// <summary>
    /// Attempts to parse the given string as a <see cref="FloatRange"/>.
    /// </summary>
    /// <param name="s">A string.</param>
    /// <param name="provider">
    /// An object that supplies culture-specific formatting information.
    /// </param>
    /// <returns>
    /// The resulting <see cref="FloatRange"/> value.
    /// </returns>
    /// <exception cref="FormatException">
    /// The provided value is not a valid <see cref="FloatRange"/>.
    /// </exception>
    public static FloatRange Parse(string? s, IFormatProvider? provider)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            throw new FormatException();
        }
        return Parse(s.AsSpan(), provider);
    }

    /// <summary>
    /// Parses the given span as a <see cref="FloatRange"/>.
    /// </summary>
    /// <param name="s">A <see cref="ReadOnlySpan{T}"/> of <see cref="char"/>.</param>
    /// <param name="provider">
    /// An object that supplies culture-specific formatting information.
    /// </param>
    /// <returns>
    /// The resulting <see cref="FloatRange"/> value.
    /// </returns>
    /// <exception cref="FormatException">
    /// The provided value is not a valid <see cref="FloatRange"/>.
    /// </exception>
    public static FloatRange Parse(in ReadOnlySpan<char> s, IFormatProvider? provider)
    {
        if (!TryParse(s, provider, out var value))
        {
            throw new FormatException();
        }
        return value;
    }

    /// <summary>
    /// Attempts to parse the given string as a <see cref="FloatRange"/>.
    /// </summary>
    /// <param name="s">A string.</param>
    /// <param name="value">
    /// If this method returns <see langword="true"/>, will be set to the resulting <see
    /// cref="FloatRange"/> value.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the span could be successfully parsed; otherwise <see
    /// langword="false"/>.
    /// </returns>
    public static bool TryParse(string? s, out FloatRange value)
    {
        value = Zero;
        if (string.IsNullOrWhiteSpace(s))
        {
            return false;
        }
        return TryParse(s.AsSpan(), CultureInfo.CurrentCulture, out value);
    }

    /// <summary>
    /// Attempts to parse the given span as a <see cref="FloatRange"/>.
    /// </summary>
    /// <param name="s">A <see cref="ReadOnlySpan{T}"/> of <see cref="char"/>.</param>
    /// <param name="value">
    /// If this method returns <see langword="true"/>, will be set to the resulting <see
    /// cref="FloatRange"/> value.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the span could be successfully parsed; otherwise <see
    /// langword="false"/>.
    /// </returns>
    public static bool TryParse(in ReadOnlySpan<char> s, out FloatRange value)
        => TryParse(s, CultureInfo.CurrentCulture, out value);

    /// <summary>
    /// Attempts to parse the given string as a <see cref="FloatRange"/>.
    /// </summary>
    /// <param name="s">A string.</param>
    /// <param name="provider">
    /// An object that supplies culture-specific formatting information.
    /// </param>
    /// <param name="value">
    /// If this method returns <see langword="true"/>, will be set to the resulting <see
    /// cref="FloatRange"/> value.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the span could be successfully parsed; otherwise <see
    /// langword="false"/>.
    /// </returns>
    public static bool TryParse(string? s, IFormatProvider? provider, out FloatRange value)
    {
        value = Zero;
        if (string.IsNullOrWhiteSpace(s))
        {
            return false;
        }
        return TryParse(s.AsSpan(), provider, out value);
    }

    /// <summary>
    /// Attempts to parse the given span as a <see cref="FloatRange"/>.
    /// </summary>
    /// <param name="s">A <see cref="ReadOnlySpan{T}"/> of <see cref="char"/>.</param>
    /// <param name="provider">
    /// An object that supplies culture-specific formatting information.
    /// </param>
    /// <param name="value">
    /// If this method returns <see langword="true"/>, will be set to the resulting <see
    /// cref="FloatRange"/> value.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the span could be successfully parsed; otherwise <see
    /// langword="false"/>.
    /// </returns>
    public static bool TryParse(in ReadOnlySpan<char> s, IFormatProvider? provider, out FloatRange value)
    {
        value = Zero;
        if (s.IsEmpty || s.IsWhiteSpace()
            || s.Length < 5
            || s[0] != '<'
            || s[^1] != '>')
        {
            return false;
        }
        var separatorIndex = s.IndexOf(';');
        if (separatorIndex == -1)
        {
            return false;
        }
        if (!float.TryParse(s[1..separatorIndex], NumberStyles.Float | NumberStyles.AllowThousands, provider, out var min))
        {
            return false;
        }
        if (!float.TryParse(s[(separatorIndex + 1)..^1], NumberStyles.Float | NumberStyles.AllowThousands, provider, out var max))
        {
            return false;
        }
        value = new FloatRange(min, max);
        return true;
    }

    /// <summary>Indicates whether this instance and a specified object are equal.</summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="obj" /> and this instance are the same type
    /// and represent the same value; otherwise, <see langword="false" />.
    /// </returns>
    public override bool Equals(object? obj) => obj is FloatRange range && Equals(range);

    /// <summary>Indicates whether the current object is equal to another object of the same
    /// type.</summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    /// <see langword="true" /> if the current object is equal to the <paramref name="other" />
    /// parameter; otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals([AllowNull] FloatRange other) => Max == other.Max && Min == other.Min;

    /// <summary>Returns the hash code for this instance.</summary>
    /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    public override int GetHashCode() => HashCode.Combine(Max, Min);

    /// <summary>Returns this instance as a <see cref="string"/>.</summary>
    /// <param name="format">A numeric format string.</param>
    /// <param name="provider">
    /// An object that supplies culture-specific formatting information.
    /// </param>
    /// <returns>The <see cref="string"/> equivalent of this instance.</returns>
    public string ToString(string? format, IFormatProvider? provider)
        => $"<{Min.ToString(format, provider)};{Max.ToString(format, provider)}>";

    /// <summary>Returns this instance as a <see cref="string"/>.</summary>
    /// <param name="format">A numeric format string.</param>
    /// <returns>The <see cref="string"/> equivalent of this instance.</returns>
    public string ToString(string? format) => ToString(format, null);

    /// <summary>Returns this instance as a <see cref="string"/>.</summary>
    /// <param name="provider">
    /// An object that supplies culture-specific formatting information.
    /// </param>
    /// <returns>The <see cref="string"/> equivalent of this instance.</returns>
    public string ToString(IFormatProvider? provider) => ToString(null, provider);

    /// <summary>Returns this instance as a <see cref="string"/>.</summary>
    /// <returns>The <see cref="string"/> equivalent of this instance.</returns>
    public override string ToString() => ToString(null, null);

    /// <summary>Indicates whether this instance and a specified object are equal.</summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="left" /> and <paramref name="right" />
    /// represent the same value; otherwise, <see langword="false" />.
    /// </returns>
    public static bool operator ==(FloatRange left, FloatRange right) => left.Equals(right);

    /// <summary>Indicates whether this instance and a specified object are unequal.</summary>
    /// <param name="left">The first object to compare.</param>
    /// <param name="right">The second object to compare.</param>
    /// <returns>
    /// <see langword="true" /> if <paramref name="left" /> and <paramref name="right" />
    /// represent different values; otherwise, <see langword="false" />.
    /// </returns>
    public static bool operator !=(FloatRange left, FloatRange right) => !(left == right);
}
