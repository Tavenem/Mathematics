namespace Tavenem.Mathematics;

/// <summary>
/// <para>
/// A collection of mathematical and scientific constants, as well as the results of some
/// precalculated operations involving those constants.
/// </para>
/// <para>
/// The constants in this class are <see cref="float"/> values. The <see
/// cref="DoubleConstants"/> class contains the same constants as <see cref="double"/> values,
/// and the <see cref="DecimalConstants"/> class contains the same constants as <see
/// cref="decimal"/> values.
/// </para>
/// </summary>
/// <remarks>
/// Some of the constants available here are provided as <see cref="int"/> or <see cref="long"/>
/// values, where their value is a whole number, and small enough to fit in those data types.
/// </remarks>
public static class FloatConstants
{
    #region Numbers

    /// <summary>
    /// ⅓
    /// </summary>
    public const float Third = 1.0f / 3.0f;

    #endregion Numbers

    #region Math

    /// <summary>
    /// The natural logarithmic base, specified by the constant, e, rounded to 12 places of
    /// precision, which is more than the limit of the <see cref="float"/> structure.
    /// </summary>
    public const float E = 2.71828182846f;

    /// <summary>
    /// 1/8π
    /// </summary>
    public const float EighthPi = Pi / 8;

    /// <summary>
    /// 1/e
    /// </summary>
    public const float InverseE = 1 / E;

    /// <summary>
    /// 4π
    /// </summary>
    public const float FourPi = Tau * 2;

    /// <summary>
    /// π+⅓π
    /// </summary>
    public const float FourThirdsPi = Pi * 4 / 3;

    /// <summary>
    /// ½π
    /// </summary>
    public const float HalfPi = Pi / 2;

    /// <summary>
    /// 1/π
    /// </summary>
    public const float InversePi = 1 / Pi;

    /// <summary>
    /// The natural logarithm of 2, rounded to 12 places of precision, which is more than the limit of
    /// the <see cref="float"/> structure.
    /// </summary>
    public const float Ln2 = 0.693147180560f;

    /// <summary>
    /// The natural logarithm of 10, rounded to 12 places of precision, which is more than the limit of
    /// the <see cref="float"/> structure.
    /// </summary>
    public const float Ln10 = 2.30258509299f;

    /// <summary>
    /// 180/π
    /// </summary>
    public const float OneEightyOverPi = 180 / Pi;

    /// <summary>
    /// Represents the golden ratio, specified by the constant, φ, rounded to 12 places of
    /// precision, which is more than the limit of the <see cref="float"/> structure.
    /// </summary>
    public const float Phi = 1.61803398875f;

    /// <summary>
    /// The ratio of the circumference of a circle to its diameter, specified by the constant,
    /// π, rounded to 12 places of precision, which is more than the limit of the <see cref="float"/>
    /// structure.
    /// </summary>
    public const float Pi = 3.14159265359f;

    /// <summary>
    /// π/180
    /// </summary>
    public const float PiOver180 = Pi / 180;

    /// <summary>
    /// π²
    /// </summary>
    public const float PiSquared = Pi * Pi;

    /// <summary>
    /// √2 rounded to 12 places of precision, which is more than the limit of the precision of the <see
    /// cref="float"/> structure.
    /// </summary>
    public const float Root2 = 1.41421356237f;

    /// <summary>
    /// ¼π
    /// </summary>
    public const float QuarterPi = Pi / 4;

    /// <summary>
    /// 1/6π
    /// </summary>
    public const float SixthPi = Pi / 6;

    /// <summary>
    /// The ratio of the circumference of a circle to its radius, specified by the constant,
    /// τ, rounded to 12 places of precision, which is more than the limit of the <see cref="float"/>
    /// structure.
    /// </summary>
    public const float Tau = 6.28318530718f;

    /// <summary>
    /// ⅓π
    /// </summary>
    public const float ThirdPi = Pi / 3;

    /// <summary>
    /// 3π
    /// </summary>
    public const float ThreePi = Tau + Pi;

    /// <summary>
    /// 3/2π
    /// </summary>
    public const float ThreeHalvesPi = ThreePi / 2;

    /// <summary>
    /// 3/4π
    /// </summary>
    public const float ThreeQuartersPi = ThreePi / 4;

    /// <summary>
    /// 2π
    /// </summary>
    public const float TwoPi = Tau;

    /// <summary>
    /// 2π²
    /// </summary>
    public const float TwoPiSquared = 2 * PiSquared;

    #endregion Math

    #region Science

    /// <summary>
    /// The Avogadro constant (<i>N</i><sub>A</sub>, <i>L</i>), in SI base units.
    /// </summary>
    public const float AvogadroConstant = 6.02214076e23f;
    /// <summary>
    /// The Avogadro constant (<i>N</i><sub>A</sub>, <i>L</i>), in SI base units.
    /// </summary>
    public const float L = AvogadroConstant;

    /// <summary>
    /// The Boltzmann constant (<i>k</i><sub>B</sub>, <i>k</i>), in SI base units.
    /// </summary>
    public const float BoltzmannConstant = 1.380649e-23f;
    /// <summary>
    /// The Boltzmann constant (<i>k</i><sub>B</sub>, <i>k</i>), in SI base units.
    /// </summary>
    public const float k = BoltzmannConstant;

    /// <summary>
    /// The mass of a electron, in kg.
    /// </summary>
    public const float ElectronMass = 9.10938356e-31f;

    /// <summary>
    /// The elementary charge (<i>e</i>, <i>q</i><sub>e</sub>), in coulombs.
    /// </summary>
    public const float ElementaryCharge = 1.602176634e-19f;
    /// <summary>
    /// The elementary charge (<i>e</i>, <i>q</i><sub>e</sub>), in coulombs.
    /// </summary>
    public const float qe = ElementaryCharge;

    /// <summary>
    /// The gravitational constant, in SI base units.
    /// </summary>
    public const float GravitationalConstant = 6.67408e-11f;
    /// <summary>
    /// The gravitational constant, in SI base units.
    /// </summary>
    public const float G = GravitationalConstant;

    /// <summary>
    /// The heat of vaporization of water, in SI base units.
    /// </summary>
    public const int HeatOfVaporizationOfWater = 2501000;
    /// <summary>
    /// The heat of vaporization of water, in SI base units.
    /// </summary>
    public const int DeltaHvapWater = HeatOfVaporizationOfWater;

    /// <summary>
    /// The distance light travels in a Julian year, in m.
    /// </summary>
    public const long LightYear = 9460730472580800;
    /// <summary>
    /// The distance light travels in a Julian year, in m.
    /// </summary>
    public const long ly = LightYear;

    /// <summary>
    /// The molar mass of air, in SI base units.
    /// </summary>
    public const float MolarMassOfAir = 0.0289644f;
    /// <summary>
    /// The molar mass of air, in SI base units.
    /// </summary>
    public const float MAir = MolarMassOfAir;

    /// <summary>
    /// The mass of a neutron, in kg.
    /// </summary>
    public const float NeutronMass = 1.674927471e-27f;

    /// <summary>
    /// The Planck constant (<i>h</i>) in SI base units.
    /// </summary>
    public const float PlanckConstant = 6.62607015e-34f;
    /// <summary>
    /// The Planck constant (<i>h</i>) in SI base units.
    /// </summary>
    public const float h = PlanckConstant;

    /// <summary>
    /// The mass of a proton, in kg.
    /// </summary>
    public const float ProtonMass = 1.672621898e-27f;

    /// <summary>
    /// The specific gas constant of dry air, in SI base units.
    /// </summary>
    public const int SpecificGasConstantOfDryAir = 287;
    /// <summary>
    /// The specific gas constant of dry air, in SI base units.
    /// </summary>
    public const int RSpecificDryAir = SpecificGasConstantOfDryAir;

    /// <summary>
    /// The specific gas constant of water, in SI base units.
    /// </summary>
    public const float SpecificGasConstantOfWater = 461.5f;
    /// <summary>
    /// The specific gas constant of water, in SI base units.
    /// </summary>
    public const float RSpecificWater = SpecificGasConstantOfWater;

    /// <summary>
    /// The specific heat of dry air at constant pressure, in SI base units.
    /// </summary>
    public const float SpecificHeatOfDryAir = 1003.5f;
    /// <summary>
    /// The specific heat of dry air at constant pressure, in SI base units.
    /// </summary>
    public const float CpDryAir = SpecificHeatOfDryAir;

    /// <summary>
    /// The speed of light in a vacuum, in m/s.
    /// </summary>
    public const int SpeedOfLight = 299792458;
    /// <summary>
    /// The speed of light in a vacuum, in m/s.
    /// </summary>
    public const int c = SpeedOfLight;

    /// <summary>
    /// The speed of light in a vacuum, squared, in m/s.
    /// </summary>
    public const float SpeedOfLightSquared = (float)SpeedOfLight * SpeedOfLight;
    /// <summary>
    /// The speed of light in a vacuum, squared, in m/s.
    /// </summary>
    public const float cSquared = SpeedOfLightSquared;

    /// <summary>
    /// The standard atmospheric pressure, in SI base units.
    /// </summary>
    public const float StandardAtmosphericPressure = 101.325f;
    /// <summary>
    /// The standard atmospheric pressure, in SI base units.
    /// </summary>
    public const float atm = StandardAtmosphericPressure;

    /// <summary>
    /// The Stefan–Boltzmann constant, in SI base units.
    /// </summary>
    public const float StefanBoltzmannConstant = 5.670367e-8f;
    /// <summary>
    /// The Stefan–Boltzmann constant, in SI base units.
    /// </summary>
    public const float sigma = StefanBoltzmannConstant;

    /// <summary>
    /// The universal gas constant, in SI base units.
    /// </summary>
    public const float UniversalGasConstant = 8.3144598f;
    /// <summary>
    /// The universal gas constant, in SI base units.
    /// </summary>
    public const float R = UniversalGasConstant;

    #endregion Science
}
