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

    #region Numbers

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
        return T.One / (T.One + T.One + T.One);
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
        return T.One / (T.One + T.One);
    }

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
        return T.One + T.One;
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
        return T.One + T.One + T.One;
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
        return T.One + T.One + T.One + T.One;
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
        return T.One + T.One + T.One + T.One + T.One;
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
        return T.One + T.One + T.One + T.One + T.One + T.One;
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
        return T.One + T.One + T.One + T.One + T.One + T.One + T.One + T.One;
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
        return T.One + T.One + T.One + T.One + T.One + T.One + T.One + T.One + T.One + T.One;
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
        return Ten<T>() * Ten<T>();
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
        return Three<T>() * Six<T>() * Ten<T>();
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
        return Hundred<T>() * Ten<T>();
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
        return Thousand<T>() * Ten<T>();
    }

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

    /// <summary>
    /// √2
    /// </summary>
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
}
