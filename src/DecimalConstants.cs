namespace Tavenem.Mathematics;

/// <summary>
/// <para>
/// A collection of mathematical and scientific constants, as well as the results of some
/// precalculated operations involving those constants.
/// </para>
/// <para>
/// The constants in this class are <see cref="decimal"/> values. The <see
/// cref="DoubleConstants"/> class contains the same constants as <see cref="double"/> values,
/// and the <see cref="FloatConstants"/> class contains the same constants as <see
/// cref="float"/> values.
/// </para>
/// </summary>
/// <remarks>
/// Some of the constants available here are provided as <see cref="int"/> or <see cref="long"/>
/// values, where their value is a whole number, and small enough to fit in those data types.
/// </remarks>
public static class DecimalConstants
{
    #region Numbers

    /// <summary>
    /// ⅓
    /// </summary>
    public const decimal Third = 1.0m / 3.0m;

    #endregion Numbers

    #region Math

    /// <summary>
    /// The natural logarithmic base, specified by the constant, e, rounded to 29 places of
    /// precision, which is the limit of the <see cref="decimal"/> structure.
    /// </summary>
    public const decimal E = 2.7182818284590452353602874713m;

    /// <summary>
    /// 1/8π
    /// </summary>
    public const decimal EighthPi = Pi / 8;

    /// <summary>
    /// 1/e
    /// </summary>
    public const decimal InverseE = 1 / E;

    /// <summary>
    /// 4π
    /// </summary>
    public const decimal FourPi = Tau * 2;

    /// <summary>
    /// π+⅓π
    /// </summary>
    public const decimal FourThirdsPi = Tau * 2 / 3;

    /// <summary>
    /// ½π
    /// </summary>
    public const decimal HalfPi = Pi / 2;

    /// <summary>
    /// 1/π
    /// </summary>
    public const decimal InversePi = 1 / Pi;

    /// <summary>
    /// The natural logarithm of 2, rounded to 29 places of precision, which is the limit of
    /// the <see cref="decimal"/> structure.
    /// </summary>
    public const decimal Ln2 = 0.69314718055994530941723212146m;

    /// <summary>
    /// The natural logarithm of 10, rounded to 29 places of precision, which is the limit of
    /// the <see cref="decimal"/> structure.
    /// </summary>
    public const decimal Ln10 = 2.3025850929940456840179914547m;

    /// <summary>
    /// 180/π
    /// </summary>
    public const decimal OneEightyOverPi = 180 / Pi;

    /// <summary>
    /// Represents the golden ratio, specified by the constant, φ, rounded to 29 places of
    /// precision, which is the limit of the <see cref="decimal"/> structure.
    /// </summary>
    public const decimal Phi = 1.6180339887498948482045868344m;

    /// <summary>
    /// The ratio of the circumference of a circle to its diameter, specified by the constant,
    /// π, rounded to 29 places of precision, which is the limit of the <see cref="decimal"/>
    /// structure.
    /// </summary>
    public const decimal Pi = 3.1415926535897932384626433833m;

    /// <summary>
    /// π/180
    /// </summary>
    public const decimal PiOver180 = Pi / 180;

    /// <summary>
    /// π²
    /// </summary>
    public const decimal PiSquared = Pi * Pi;

    /// <summary>
    /// √2 rounded to 29 places of precision, which is the limit of the precision of the <see
    /// cref="decimal"/> structure.
    /// </summary>
    public const decimal Root2 = 1.4142135623730950488016887242m;

    /// <summary>
    /// ¼π
    /// </summary>
    public const decimal QuarterPi = Pi / 4;

    /// <summary>
    /// 1/6π
    /// </summary>
    public const decimal SixthPi = Pi / 6;

    /// <summary>
    /// The ratio of the circumference of a circle to its radius, specified by the constant,
    /// τ, rounded to 29 places of precision, which is the limit of the <see cref="decimal"/>
    /// structure.
    /// </summary>
    public const decimal Tau = 6.2831853071795864769252867665m;

    /// <summary>
    /// ⅓π
    /// </summary>
    public const decimal ThirdPi = Pi / 3;

    /// <summary>
    /// 3π
    /// </summary>
    public const decimal ThreePi = Tau + Pi;

    /// <summary>
    /// 3/2π
    /// </summary>
    public const decimal ThreeHalvesPi = ThreePi / 2;

    /// <summary>
    /// 3/4π
    /// </summary>
    public const decimal ThreeQuartersPi = ThreePi / 4;

    /// <summary>
    /// 2π
    /// </summary>
    public const decimal TwoPi = Tau;

    /// <summary>
    /// 2π²
    /// </summary>
    public const decimal TwoPiSquared = 2 * PiSquared;

    #endregion Math

    #region Science

    /// <summary>
    /// The Avogadro constant (<i>N</i><sub>A</sub>, <i>L</i>), in SI base units.
    /// </summary>
    public const decimal AvogadroConstant = 6.02214076e23m;
    /// <summary>
    /// The Avogadro constant (<i>N</i><sub>A</sub>, <i>L</i>), in SI base units.
    /// </summary>
    public const decimal L = AvogadroConstant;

    /// <summary>
    /// The Boltzmann constant (<i>k</i><sub>B</sub>, <i>k</i>), in SI base units.
    /// </summary>
    public const decimal BoltzmannConstant = 1.380649e-23m;
    /// <summary>
    /// The Boltzmann constant (<i>k</i><sub>B</sub>, <i>k</i>), in SI base units.
    /// </summary>
    public const decimal k = BoltzmannConstant;

    /// <summary>
    /// The mass of a electron, in kg.
    /// </summary>
    public const decimal ElectronMass = 9.10938356e-31m;

    /// <summary>
    /// The elementary charge (<i>e</i>, <i>q</i><sub>e</sub>), in coulombs.
    /// </summary>
    public const decimal ElementaryCharge = 1.602176634e-19m;
    /// <summary>
    /// The elementary charge (<i>e</i>, <i>q</i><sub>e</sub>), in coulombs.
    /// </summary>
    public const decimal qe = ElementaryCharge;

    /// <summary>
    /// The gravitational constant, in SI base units.
    /// </summary>
    public const decimal GravitationalConstant = 6.67408e-11m;
    /// <summary>
    /// The gravitational constant, in SI base units.
    /// </summary>
    public const decimal G = GravitationalConstant;

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
    public const decimal MolarMassOfAir = 0.0289644m;
    /// <summary>
    /// The molar mass of air, in SI base units.
    /// </summary>
    public const decimal MAir = MolarMassOfAir;

    /// <summary>
    /// The mass of a neutron, in kg.
    /// </summary>
    public const decimal NeutronMass = 1.674927471e-27m;

    /// <summary>
    /// The Planck constant (<i>h</i>) in SI base units.
    /// </summary>
    public const decimal PlanckConstant = 6.62607015e-34m;
    /// <summary>
    /// The Planck constant (<i>h</i>) in SI base units.
    /// </summary>
    public const decimal h = PlanckConstant;

    /// <summary>
    /// The mass of a proton, in kg.
    /// </summary>
    public const decimal ProtonMass = 1.672621898e-27m;

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
    public const decimal SpecificGasConstantOfWater = 461.5m;
    /// <summary>
    /// The specific gas constant of water, in SI base units.
    /// </summary>
    public const decimal RSpecificWater = SpecificGasConstantOfWater;

    /// <summary>
    /// The specific heat of dry air at constant pressure, in SI base units.
    /// </summary>
    public const decimal SpecificHeatOfDryAir = 1003.5m;
    /// <summary>
    /// The specific heat of dry air at constant pressure, in SI base units.
    /// </summary>
    public const decimal CpDryAir = SpecificHeatOfDryAir;

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
    public const decimal SpeedOfLightSquared = (decimal)SpeedOfLight * SpeedOfLight;
    /// <summary>
    /// The speed of light in a vacuum, squared, in m/s.
    /// </summary>
    public const decimal cSquared = SpeedOfLightSquared;

    /// <summary>
    /// The standard atmospheric pressure, in SI base units.
    /// </summary>
    public const decimal StandardAtmosphericPressure = 101.325m;
    /// <summary>
    /// The standard atmospheric pressure, in SI base units.
    /// </summary>
    public const decimal atm = StandardAtmosphericPressure;

    /// <summary>
    /// The Stefan–Boltzmann constant, in SI base units.
    /// </summary>
    public const decimal StefanBoltzmannConstant = 5.670367e-8m;
    /// <summary>
    /// The Stefan–Boltzmann constant, in SI base units.
    /// </summary>
    public const decimal sigma = StefanBoltzmannConstant;

    /// <summary>
    /// The universal gas constant, in SI base units.
    /// </summary>
    public const decimal UniversalGasConstant = 8.3144598m;
    /// <summary>
    /// The universal gas constant, in SI base units.
    /// </summary>
    public const decimal R = UniversalGasConstant;

    #endregion Science
}
