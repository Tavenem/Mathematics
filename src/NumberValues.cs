namespace Tavenem.Mathematics;

/// <summary>
/// A collection of mathematical and scientific values, as well as the results of some
/// operations involving those constants.
/// </summary>
public static class NumberValues
{
    /// <summary>
    /// A floating-point value close to zero, intended to determine near-equivalence to 0.
    /// </summary>
    public const decimal NearlyZeroDecimal = 1e-15m;

    /// <summary>
    /// A floating-point value close to zero, intended to determine near-equivalence to 0.
    /// </summary>
    public const double NearlyZeroDouble = 1e-15;

    /// <summary>
    /// <para>
    /// A floating-point value close to zero, intended to determine near-equivalence to 0.
    /// </para>
    /// <para>
    /// This value has less precision than <see cref="NearlyZeroDouble"/>, and is better suited when
    /// checking <see cref="float"/> values.
    /// </para>
    /// </summary>
    public const float NearlyZeroFloat = 1e-6f;

    /// <summary>
    /// Represents the smallest positive <typeparamref name="T"/> value that is greater than zero.
    /// </summary>
    public static T Epsilon<T>() where T : IFloatingPoint<T> => T.Epsilon;

    /// <summary>Not a number (NaN)</summary>
    public static T NaN<T>() where T : IFloatingPoint<T> => T.NaN;

    #region Numbers

    /// <summary>-∞</summary>
    public static T NegativeInfinity<T>() where T : IFloatingPoint<T> => T.NegativeInfinity;

    /// <summary>-1</summary>
    public static T NegativeOne<T>() where T : ISignedNumber<T> => T.NegativeOne;

    /// <summary>-0</summary>
    public static T NegativeZero<T>() where T : IFloatingPoint<T> => T.NegativeZero;

    /// <summary>0</summary>
    public static T Zero<T>() where T : INumber<T> => T.Zero;

    /// <summary>⅓</summary>
    public static T Third<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.Third;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.Third;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.Third;
        }
        return T.One / Three<T>();
    }

    /// <summary>½</summary>
    public static T Half<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)0.5m;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)0.5;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)0.5f;
        }
        return T.One / Two<T>();
    }

    /// <summary>1</summary>
    public static T One<T>() where T : INumber<T> => T.One;

    /// <summary>2</summary>
    public static T Two<T>() where T : INumber<T>
    {
        if (typeof(T) == typeof(byte))
        {
            return (T)(object)2;
        }
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)2.0m;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)2.0;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)2.0f;
        }
        if (typeof(T) == typeof(int))
        {
            return (T)(object)2;
        }
        if (typeof(T) == typeof(long))
        {
            return (T)(object)2L;
        }
        if (typeof(T) == typeof(nint))
        {
            return (T)(object)2;
        }
        if (typeof(T) == typeof(nuint))
        {
            return (T)(object)2;
        }
        if (typeof(T) == typeof(sbyte))
        {
            return (T)(object)2;
        }
        if (typeof(T) == typeof(short))
        {
            return (T)(object)2;
        }
        if (typeof(T) == typeof(ushort))
        {
            return (T)(object)2;
        }
        if (typeof(T) == typeof(uint))
        {
            return (T)(object)2u;
        }
        if (typeof(T) == typeof(ulong))
        {
            return (T)(object)2UL;
        }
        return T.Create(2);
    }

    /// <summary>3</summary>
    public static T Three<T>() where T : INumber<T>
    {
        if (typeof(T) == typeof(byte))
        {
            return (T)(object)3;
        }
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)3.0m;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)3.0;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)3.0f;
        }
        if (typeof(T) == typeof(int))
        {
            return (T)(object)3;
        }
        if (typeof(T) == typeof(long))
        {
            return (T)(object)3L;
        }
        if (typeof(T) == typeof(nint))
        {
            return (T)(object)3;
        }
        if (typeof(T) == typeof(nuint))
        {
            return (T)(object)3;
        }
        if (typeof(T) == typeof(sbyte))
        {
            return (T)(object)3;
        }
        if (typeof(T) == typeof(short))
        {
            return (T)(object)3;
        }
        if (typeof(T) == typeof(ushort))
        {
            return (T)(object)3;
        }
        if (typeof(T) == typeof(uint))
        {
            return (T)(object)3u;
        }
        if (typeof(T) == typeof(ulong))
        {
            return (T)(object)3UL;
        }
        return T.Create(3);
    }

    /// <summary>4</summary>
    public static T Four<T>() where T : INumber<T>
    {
        if (typeof(T) == typeof(byte))
        {
            return (T)(object)4;
        }
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)4.0m;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)4.0;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)4.0f;
        }
        if (typeof(T) == typeof(int))
        {
            return (T)(object)4;
        }
        if (typeof(T) == typeof(long))
        {
            return (T)(object)4L;
        }
        if (typeof(T) == typeof(nint))
        {
            return (T)(object)4;
        }
        if (typeof(T) == typeof(nuint))
        {
            return (T)(object)4;
        }
        if (typeof(T) == typeof(sbyte))
        {
            return (T)(object)4;
        }
        if (typeof(T) == typeof(short))
        {
            return (T)(object)4;
        }
        if (typeof(T) == typeof(ushort))
        {
            return (T)(object)4;
        }
        if (typeof(T) == typeof(uint))
        {
            return (T)(object)4u;
        }
        if (typeof(T) == typeof(ulong))
        {
            return (T)(object)4UL;
        }
        return T.Create(4);
    }

    /// <summary>5</summary>
    public static T Five<T>() where T : INumber<T>
    {
        if (typeof(T) == typeof(byte))
        {
            return (T)(object)5;
        }
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)5.0m;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)5.0;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)5.0f;
        }
        if (typeof(T) == typeof(int))
        {
            return (T)(object)5;
        }
        if (typeof(T) == typeof(long))
        {
            return (T)(object)5L;
        }
        if (typeof(T) == typeof(nint))
        {
            return (T)(object)5;
        }
        if (typeof(T) == typeof(nuint))
        {
            return (T)(object)5;
        }
        if (typeof(T) == typeof(sbyte))
        {
            return (T)(object)5;
        }
        if (typeof(T) == typeof(short))
        {
            return (T)(object)5;
        }
        if (typeof(T) == typeof(ushort))
        {
            return (T)(object)5;
        }
        if (typeof(T) == typeof(uint))
        {
            return (T)(object)5u;
        }
        if (typeof(T) == typeof(ulong))
        {
            return (T)(object)5UL;
        }
        return T.Create(5);
    }

    /// <summary>6</summary>
    public static T Six<T>() where T : INumber<T>
    {
        if (typeof(T) == typeof(byte))
        {
            return (T)(object)6;
        }
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)6.0m;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)6.0;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)6.0f;
        }
        if (typeof(T) == typeof(int))
        {
            return (T)(object)6;
        }
        if (typeof(T) == typeof(long))
        {
            return (T)(object)6L;
        }
        if (typeof(T) == typeof(nint))
        {
            return (T)(object)6;
        }
        if (typeof(T) == typeof(nuint))
        {
            return (T)(object)6;
        }
        if (typeof(T) == typeof(sbyte))
        {
            return (T)(object)6;
        }
        if (typeof(T) == typeof(short))
        {
            return (T)(object)6;
        }
        if (typeof(T) == typeof(ushort))
        {
            return (T)(object)6;
        }
        if (typeof(T) == typeof(uint))
        {
            return (T)(object)6u;
        }
        if (typeof(T) == typeof(ulong))
        {
            return (T)(object)6UL;
        }
        return T.Create(6);
    }

    /// <summary>8</summary>
    public static T Eight<T>() where T : INumber<T>
    {
        if (typeof(T) == typeof(byte))
        {
            return (T)(object)8;
        }
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)8.0m;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)8.0;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)8.0f;
        }
        if (typeof(T) == typeof(int))
        {
            return (T)(object)8;
        }
        if (typeof(T) == typeof(long))
        {
            return (T)(object)8L;
        }
        if (typeof(T) == typeof(nint))
        {
            return (T)(object)8;
        }
        if (typeof(T) == typeof(nuint))
        {
            return (T)(object)8;
        }
        if (typeof(T) == typeof(sbyte))
        {
            return (T)(object)8;
        }
        if (typeof(T) == typeof(short))
        {
            return (T)(object)8;
        }
        if (typeof(T) == typeof(ushort))
        {
            return (T)(object)8;
        }
        if (typeof(T) == typeof(uint))
        {
            return (T)(object)8u;
        }
        if (typeof(T) == typeof(ulong))
        {
            return (T)(object)8UL;
        }
        return T.Create(8);
    }

    /// <summary>10</summary>
    public static T Ten<T>() where T : INumber<T>
    {
        if (typeof(T) == typeof(byte))
        {
            return (T)(object)10;
        }
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)10.0m;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)10.0;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)10.0f;
        }
        if (typeof(T) == typeof(int))
        {
            return (T)(object)10;
        }
        if (typeof(T) == typeof(long))
        {
            return (T)(object)10L;
        }
        if (typeof(T) == typeof(nint))
        {
            return (T)(object)10;
        }
        if (typeof(T) == typeof(nuint))
        {
            return (T)(object)10;
        }
        if (typeof(T) == typeof(sbyte))
        {
            return (T)(object)10;
        }
        if (typeof(T) == typeof(short))
        {
            return (T)(object)10;
        }
        if (typeof(T) == typeof(ushort))
        {
            return (T)(object)10;
        }
        if (typeof(T) == typeof(uint))
        {
            return (T)(object)10u;
        }
        if (typeof(T) == typeof(ulong))
        {
            return (T)(object)10UL;
        }
        return T.Create(10);
    }

    /// <summary>100</summary>
    public static T Hundred<T>() where T : INumber<T>
    {
        if (typeof(T) == typeof(byte))
        {
            return (T)(object)100;
        }
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)100.0m;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)100.0;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)100.0f;
        }
        if (typeof(T) == typeof(int))
        {
            return (T)(object)100;
        }
        if (typeof(T) == typeof(long))
        {
            return (T)(object)100L;
        }
        if (typeof(T) == typeof(nint))
        {
            return (T)(object)100;
        }
        if (typeof(T) == typeof(nuint))
        {
            return (T)(object)100;
        }
        if (typeof(T) == typeof(sbyte))
        {
            return (T)(object)100;
        }
        if (typeof(T) == typeof(short))
        {
            return (T)(object)100;
        }
        if (typeof(T) == typeof(ushort))
        {
            return (T)(object)100;
        }
        if (typeof(T) == typeof(uint))
        {
            return (T)(object)100u;
        }
        if (typeof(T) == typeof(ulong))
        {
            return (T)(object)100UL;
        }
        return T.Create(100);
    }

    /// <summary>180</summary>
    public static T OneHundredEighty<T>() where T : INumber<T>
    {
        if (typeof(T) == typeof(byte))
        {
            return (T)(object)180;
        }
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)180.0m;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)180.0;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)180.0f;
        }
        if (typeof(T) == typeof(int))
        {
            return (T)(object)180;
        }
        if (typeof(T) == typeof(long))
        {
            return (T)(object)180L;
        }
        if (typeof(T) == typeof(nint))
        {
            return (T)(object)180;
        }
        if (typeof(T) == typeof(nuint))
        {
            return (T)(object)180;
        }
        if (typeof(T) == typeof(sbyte))
        {
            return (T)(object)180;
        }
        if (typeof(T) == typeof(short))
        {
            return (T)(object)180;
        }
        if (typeof(T) == typeof(ushort))
        {
            return (T)(object)180;
        }
        if (typeof(T) == typeof(uint))
        {
            return (T)(object)180u;
        }
        if (typeof(T) == typeof(ulong))
        {
            return (T)(object)180UL;
        }
        return T.Create(180);
    }

    /// <summary>1000</summary>
    public static T Thousand<T>() where T : INumber<T>
    {
        if (typeof(T) == typeof(byte))
        {
            return (T)(object)1000;
        }
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)1000.0m;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)1000.0;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)1000.0f;
        }
        if (typeof(T) == typeof(int))
        {
            return (T)(object)1000;
        }
        if (typeof(T) == typeof(long))
        {
            return (T)(object)1000L;
        }
        if (typeof(T) == typeof(nint))
        {
            return (T)(object)1000;
        }
        if (typeof(T) == typeof(nuint))
        {
            return (T)(object)1000;
        }
        if (typeof(T) == typeof(sbyte))
        {
            return (T)(object)1000;
        }
        if (typeof(T) == typeof(short))
        {
            return (T)(object)1000;
        }
        if (typeof(T) == typeof(ushort))
        {
            return (T)(object)1000;
        }
        if (typeof(T) == typeof(uint))
        {
            return (T)(object)1000u;
        }
        if (typeof(T) == typeof(ulong))
        {
            return (T)(object)1000UL;
        }
        return T.Create(1000);
    }

    /// <summary>10000</summary>
    public static T TenThousand<T>() where T : INumber<T>
    {
        if (typeof(T) == typeof(byte))
        {
            return (T)(object)10000;
        }
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)10000.0m;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)10000.0;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)10000.0f;
        }
        if (typeof(T) == typeof(int))
        {
            return (T)(object)10000;
        }
        if (typeof(T) == typeof(long))
        {
            return (T)(object)10000L;
        }
        if (typeof(T) == typeof(nint))
        {
            return (T)(object)10000;
        }
        if (typeof(T) == typeof(nuint))
        {
            return (T)(object)10000;
        }
        if (typeof(T) == typeof(sbyte))
        {
            return (T)(object)10000;
        }
        if (typeof(T) == typeof(short))
        {
            return (T)(object)10000;
        }
        if (typeof(T) == typeof(ushort))
        {
            return (T)(object)10000;
        }
        if (typeof(T) == typeof(uint))
        {
            return (T)(object)10000u;
        }
        if (typeof(T) == typeof(ulong))
        {
            return (T)(object)10000UL;
        }
        return T.Create(10000);
    }

    /// <summary>∞</summary>
    public static T PositiveInfinity<T>() where T : IFloatingPoint<T> => T.PositiveInfinity;

    #endregion Numbers

    #region E

    /// <summary>
    /// The natural logarithmic base, specified by the constant, e.
    /// </summary>
    public static T E<T>() where T : IFloatingPoint<T> => T.E;

    /// <summary>1/e</summary>
    public static T InverseE<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.InverseE;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.InverseE;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.InverseE;
        }
        return T.One / T.E;
    }

    #endregion E

    #region Pi / Tau

    /// <summary>1/8π</summary>
    public static T EighthPi<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.EighthPi;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.EighthPi;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.EighthPi;
        }
        return T.Pi / Eight<T>();
    }

    /// <summary>4π</summary>
    public static T FourPi<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.FourPi;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.FourPi;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.FourPi;
        }
        return T.Pi / Four<T>();
    }

    /// <summary>π+⅓π</summary>
    public static T FourThirdsPi<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.FourThirdsPi;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.FourThirdsPi;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.FourThirdsPi;
        }
        return T.Pi * Four<T>() / Three<T>();
    }

    /// <summary>½π</summary>
    public static T HalfPi<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.HalfPi;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.HalfPi;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.HalfPi;
        }
        return T.Pi / Two<T>();
    }

    /// <summary>1/π</summary>
    public static T InversePi<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.InversePi;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.InversePi;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.InversePi;
        }
        return T.One / T.Pi;
    }

    /// <summary>180/π</summary>
    public static T OneEightyOverPi<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.OneEightyOverPi;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.OneEightyOverPi;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.OneEightyOverPi;
        }
        return OneHundredEighty<T>() / T.Pi;
    }

    /// <summary>π/180</summary>
    public static T PiOver180<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.PiOver180;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.PiOver180;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.PiOver180;
        }
        return T.Pi / OneHundredEighty<T>();
    }

    /// <summary>π²</summary>
    public static T PiSquared<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.PiSquared;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.PiSquared;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.PiSquared;
        }
        return T.Pi * T.Pi;
    }

    /// <summary>¼π</summary>
    public static T QuarterPi<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.QuarterPi;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.QuarterPi;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.QuarterPi;
        }
        return T.Pi / Four<T>();
    }

    /// <summary>1/6π</summary>
    public static T SixthPi<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.SixthPi;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.SixthPi;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.SixthPi;
        }
        return T.Pi / Six<T>();
    }

    /// <summary>⅓π</summary>
    public static T ThirdPi<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.ThirdPi;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.ThirdPi;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.ThirdPi;
        }
        return T.Pi / Three<T>();
    }

    /// <summary>3π</summary>
    public static T ThreePi<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.ThreePi;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.ThreePi;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.ThreePi;
        }
        return T.Pi + T.Pi + T.Pi;
    }

    /// <summary>3/2π</summary>
    public static T ThreeHalvesPi<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.ThreeHalvesPi;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.ThreeHalvesPi;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.ThreeHalvesPi;
        }
        return ThreePi<T>() / Two<T>();
    }

    /// <summary>3/4π</summary>
    public static T ThreeQuartersPi<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.ThreeQuartersPi;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.ThreeQuartersPi;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.ThreeQuartersPi;
        }
        return ThreePi<T>() / Four<T>();
    }

    /// <summary>2π²</summary>
    public static T TwoPiSquared<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.TwoPiSquared;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.TwoPiSquared;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.TwoPiSquared;
        }
        return Two<T>() * T.Pi * T.Pi;
    }

    #endregion Pi / Tau

    /// <summary>
    /// The natural logarithm of 2.
    /// </summary>
    public static T Ln2<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.Ln2;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.Ln2;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.Ln2;
        }
        return T.Log(Two<T>());
    }

    /// <summary>
    /// The natural logarithm of 10.
    /// </summary>
    public static T Ln10<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.Ln10;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.Ln10;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.Ln10;
        }
        return T.Log(Ten<T>());
    }

    /// <summary>
    /// Represents the golden ratio, specified by the constant, φ.
    /// </summary>
    public static T Phi<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.Phi;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.Phi;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.Phi;
        }
        return (T.One + T.Sqrt(Five<T>())) / Two<T>();
    }

    /// <summary>√2</summary>
    public static T Root2<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.Root2;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.Root2;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.Root2;
        }
        return T.Sqrt(Two<T>());
    }

    #region Science

    /// <summary>
    /// The Avogadro constant (<i>N</i><sub>A</sub>, <i>L</i>), in SI base units.
    /// </summary>
    public static T AvogadroConstant<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.AvogadroConstant;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.AvogadroConstant;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.AvogadroConstant;
        }
        return T.Create(DoubleConstants.AvogadroConstant);
    }

    /// <summary>
    /// The Boltzmann constant (<i>k</i><sub>B</sub>, <i>k</i>), in SI base units.
    /// </summary>
    public static T BoltzmannConstant<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.BoltzmannConstant;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.BoltzmannConstant;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.BoltzmannConstant;
        }
        return T.Create(DoubleConstants.BoltzmannConstant);
    }

    /// <summary>
    /// The mass of a electron, in kg.
    /// </summary>
    public static T ElectronMass<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.ElectronMass;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.ElectronMass;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.ElectronMass;
        }
        return T.Create(DoubleConstants.ElectronMass);
    }

    /// <summary>
    /// The elementary charge (<i>e</i>, <i>q</i><sub>e</sub>), in coulombs.
    /// </summary>
    public static T ElementaryCharge<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.ElementaryCharge;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.ElementaryCharge;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.ElementaryCharge;
        }
        return T.Create(DoubleConstants.ElementaryCharge);
    }

    /// <summary>
    /// The gravitational constant, in SI base units.
    /// </summary>
    public static T GravitationalConstant<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.GravitationalConstant;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.GravitationalConstant;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.GravitationalConstant;
        }
        return T.Create(DoubleConstants.GravitationalConstant);
    }

    /// <summary>
    /// The heat of vaporization of water, in SI base units.
    /// </summary>
    public static T HeatOfVaporizationOfWater<T>() where T : INumber<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.HeatOfVaporizationOfWater;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.HeatOfVaporizationOfWater;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.HeatOfVaporizationOfWater;
        }
        if (typeof(T) == typeof(int))
        {
            return (T)(object)DoubleConstants.HeatOfVaporizationOfWater;
        }
        if (typeof(T) == typeof(long))
        {
            return (T)(object)DoubleConstants.HeatOfVaporizationOfWater;
        }
        if (typeof(T) == typeof(nint))
        {
            return (T)(object)DoubleConstants.HeatOfVaporizationOfWater;
        }
        if (typeof(T) == typeof(nuint))
        {
            return (T)(object)DoubleConstants.HeatOfVaporizationOfWater;
        }
        if (typeof(T) == typeof(uint))
        {
            return (T)(object)DoubleConstants.HeatOfVaporizationOfWater;
        }
        if (typeof(T) == typeof(ulong))
        {
            return (T)(object)DoubleConstants.HeatOfVaporizationOfWater;
        }
        return T.Create(DoubleConstants.HeatOfVaporizationOfWater);
    }

    /// <summary>
    /// The distance light travels in a Julian year, in m.
    /// </summary>
    public static T LightYear<T>() where T : INumber<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.LightYear;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.LightYear;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.LightYear;
        }
        if (typeof(T) == typeof(long))
        {
            return (T)(object)DoubleConstants.LightYear;
        }
        if (typeof(T) == typeof(ulong))
        {
            return (T)(object)DoubleConstants.LightYear;
        }
        return T.Create(DoubleConstants.LightYear);
    }

    /// <summary>
    /// The molar mass of air, in SI base units.
    /// </summary>
    public static T MolarMassOfAir<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.MolarMassOfAir;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.MolarMassOfAir;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.MolarMassOfAir;
        }
        return T.Create(DoubleConstants.MolarMassOfAir);
    }

    /// <summary>
    /// The mass of a neutron, in kg.
    /// </summary>
    public static T NeutronMass<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.NeutronMass;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.NeutronMass;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.NeutronMass;
        }
        return T.Create(DoubleConstants.NeutronMass);
    }

    /// <summary>
    /// The Planck constant (<i>h</i>) in SI base units.
    /// </summary>
    public static T PlanckConstant<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.PlanckConstant;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.PlanckConstant;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.PlanckConstant;
        }
        return T.Create(DoubleConstants.PlanckConstant);
    }

    /// <summary>
    /// The mass of a proton, in kg.
    /// </summary>
    public static T ProtonMass<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.ProtonMass;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.ProtonMass;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.ProtonMass;
        }
        return T.Create(DoubleConstants.ProtonMass);
    }

    /// <summary>
    /// The specific gas constant of dry air, in SI base units.
    /// </summary>
    public static T SpecificGasConstantOfDryAir<T>() where T : INumber<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.SpecificGasConstantOfDryAir;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.SpecificGasConstantOfDryAir;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.SpecificGasConstantOfDryAir;
        }
        if (typeof(T) == typeof(int))
        {
            return (T)(object)DoubleConstants.SpecificGasConstantOfDryAir;
        }
        if (typeof(T) == typeof(long))
        {
            return (T)(object)DoubleConstants.SpecificGasConstantOfDryAir;
        }
        if (typeof(T) == typeof(nint))
        {
            return (T)(object)DoubleConstants.SpecificGasConstantOfDryAir;
        }
        if (typeof(T) == typeof(nuint))
        {
            return (T)(object)DoubleConstants.SpecificGasConstantOfDryAir;
        }
        if (typeof(T) == typeof(short))
        {
            return (T)(object)DoubleConstants.SpecificGasConstantOfDryAir;
        }
        if (typeof(T) == typeof(ushort))
        {
            return (T)(object)DoubleConstants.SpecificGasConstantOfDryAir;
        }
        if (typeof(T) == typeof(uint))
        {
            return (T)(object)DoubleConstants.SpecificGasConstantOfDryAir;
        }
        if (typeof(T) == typeof(ulong))
        {
            return (T)(object)DoubleConstants.SpecificGasConstantOfDryAir;
        }
        return T.Create(DoubleConstants.SpecificGasConstantOfDryAir);
    }

    /// <summary>
    /// The specific gas constant of water, in SI base units.
    /// </summary>
    public static T SpecificGasConstantOfWater<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.SpecificGasConstantOfWater;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.SpecificGasConstantOfWater;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.SpecificGasConstantOfWater;
        }
        return T.Create(DoubleConstants.SpecificGasConstantOfWater);
    }

    /// <summary>
    /// The specific heat of dry air at constant pressure, in SI base units.
    /// </summary>
    public static T SpecificHeatOfDryAir<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.SpecificHeatOfDryAir;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.SpecificHeatOfDryAir;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.SpecificHeatOfDryAir;
        }
        return T.Create(DoubleConstants.SpecificHeatOfDryAir);
    }

    /// <summary>
    /// The speed of light in a vacuum, in m/s.
    /// </summary>
    public static T SpeedOfLight<T>() where T : INumber<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.SpeedOfLight;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.SpeedOfLight;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.SpeedOfLight;
        }
        if (typeof(T) == typeof(int))
        {
            return (T)(object)DoubleConstants.SpeedOfLight;
        }
        if (typeof(T) == typeof(long))
        {
            return (T)(object)DoubleConstants.SpeedOfLight;
        }
        if (typeof(T) == typeof(nint))
        {
            return (T)(object)DoubleConstants.SpeedOfLight;
        }
        if (typeof(T) == typeof(nuint))
        {
            return (T)(object)DoubleConstants.SpeedOfLight;
        }
        if (typeof(T) == typeof(uint))
        {
            return (T)(object)DoubleConstants.SpeedOfLight;
        }
        if (typeof(T) == typeof(ulong))
        {
            return (T)(object)DoubleConstants.SpeedOfLight;
        }
        return T.Create(DoubleConstants.SpeedOfLight);
    }

    /// <summary>
    /// The speed of light in a vacuum, squared, in m/s.
    /// </summary>
    public static T SpeedOfLightSquared<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.SpeedOfLightSquared;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.SpeedOfLightSquared;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.SpeedOfLightSquared;
        }
        return SpeedOfLight<T>().Square();
    }

    /// <summary>
    /// The standard atmospheric pressure, in SI base units.
    /// </summary>
    public static T StandardAtmosphericPressure<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.StandardAtmosphericPressure;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.StandardAtmosphericPressure;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.StandardAtmosphericPressure;
        }
        return T.Create(DoubleConstants.StandardAtmosphericPressure);
    }

    /// <summary>
    /// The Stefan–Boltzmann constant, in SI base units.
    /// </summary>
    public static T StefanBoltzmannConstant<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.StefanBoltzmannConstant;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.StefanBoltzmannConstant;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.StefanBoltzmannConstant;
        }
        return T.Create(DoubleConstants.StefanBoltzmannConstant);
    }

    /// <summary>
    /// The universal gas constant, in SI base units.
    /// </summary>
    public static T UniversalGasConstant<T>() where T : IFloatingPoint<T>
    {
        if (typeof(T) == typeof(decimal))
        {
            return (T)(object)DecimalConstants.UniversalGasConstant;
        }
        if (typeof(T) == typeof(double))
        {
            return (T)(object)DoubleConstants.UniversalGasConstant;
        }
        if (typeof(T) == typeof(float))
        {
            return (T)(object)FloatConstants.UniversalGasConstant;
        }
        return T.Create(DoubleConstants.UniversalGasConstant);
    }

    #endregion Science
}
