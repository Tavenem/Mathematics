using Tavenem.HugeNumbers;

namespace Tavenem.Mathematics
{
    /// <summary>
    /// <para>
    /// A collection of scientific constants, as well as the results of some precalculated
    /// operations involving those constants.
    /// </para>
    /// <para>
    /// The constants in this class are <see cref="HugeNumber"/> values. The <see
    /// cref="DoubleConstants"/> class contains the same constants as <see cref="double"/> values,
    /// and the <see cref="DecimalConstants"/> class contains the same constants as <see
    /// cref="decimal"/> values.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// Some of the constants available here are provided as <see cref="int"/> or <see cref="long"/>
    /// values, where their value is a whole number, and small enough to fit in those data types.
    /// </para>
    /// <para>
    /// The mathematical constants contained in the <see cref="DoubleConstants"/> and <see
    /// cref="DecimalConstants"/> classes are available as <see langword="static"/> properties of
    /// the <see cref="HugeNumber"/> class itself.
    /// </para>
    /// </remarks>
    public static class HugeNumberConstants
    {
        /// <summary>
        /// The Avogadro constant (<i>N</i><sub>A</sub>, <i>L</i>), in SI base units.
        /// </summary>
        public static readonly HugeNumber AvogadroConstant = new(602214076, 15);
        /// <summary>
        /// The Avogadro constant (<i>N</i><sub>A</sub>, <i>L</i>), in SI base units.
        /// </summary>
        public static readonly HugeNumber L = AvogadroConstant;

        /// <summary>
        /// The Boltzmann constant (<i>k</i><sub>B</sub>, <i>k</i>), in SI base units.
        /// </summary>
        public static readonly HugeNumber BoltzmannConstant = new(1380649, -29);
        /// <summary>
        /// The Boltzmann constant (<i>k</i><sub>B</sub>, <i>k</i>), in SI base units.
        /// </summary>
        public static readonly HugeNumber k = BoltzmannConstant;

        /// <summary>
        /// The mass of a electron, in kg.
        /// </summary>
        public static readonly HugeNumber ElectronMass = new(910938356, -39);

        /// <summary>
        /// The elementary charge (<i>e</i>, <i>q</i><sub>e</sub>), in coulombs.
        /// </summary>
        public static readonly HugeNumber ElementaryCharge = new(1602176634, -28);
        /// <summary>
        /// The elementary charge (<i>e</i>, <i>q</i><sub>e</sub>), in coulombs.
        /// </summary>
        public static readonly HugeNumber qe = ElementaryCharge;

        /// <summary>
        /// The gravitational constant, in SI base units.
        /// </summary>
        public static readonly HugeNumber GravitationalConstant = new(667408, -16);
        /// <summary>
        /// The gravitational constant, in SI base units.
        /// </summary>
        public static readonly HugeNumber G = GravitationalConstant;
        /// <summary>
        /// Twice the gravitational constant, in SI base units.
        /// </summary>
        public static readonly HugeNumber TwoG = 2 * G;

        /// <summary>
        /// The heat of vaporization of water, in SI base units.
        /// </summary>
        public const int HeatOfVaporizationOfWater = 2501000;
        /// <summary>
        /// The heat of vaporization of water, in SI base units.
        /// </summary>
        public const int DeltaHvapWater = HeatOfVaporizationOfWater;

        /// <summary>
        /// The heat of vaporization of water, squared, in SI base units.
        /// </summary>
        public static readonly HugeNumber DeltaHvapWaterSquared = ((HugeNumber)DeltaHvapWater).Square();

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
        public static readonly HugeNumber MolarMassOfAir = new(289644, -7);
        /// <summary>
        /// The molar mass of air, in SI base units.
        /// </summary>
        public static readonly HugeNumber MAir = MolarMassOfAir;

        /// <summary>
        /// The mass of a neutron, in kg.
        /// </summary>
        public static readonly HugeNumber NeutronMass = new(1674927471, -36);

        /// <summary>
        /// The Planck constant (<i>h</i>) in SI base units.
        /// </summary>
        public static readonly HugeNumber PlanckConstant = new(662607015, -42);
        /// <summary>
        /// The Planck constant (<i>h</i>) in SI base units.
        /// </summary>
        public static readonly HugeNumber h = PlanckConstant;

        /// <summary>
        /// The mass of a proton, in kg.
        /// </summary>
        public static readonly HugeNumber ProtonMass = new(1672621898, -36);

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
        public static readonly HugeNumber SpecificGasConstantOfWater = new(4615, -1);
        /// <summary>
        /// The specific gas constant of water, in SI base units.
        /// </summary>
        public static readonly HugeNumber RSpecificWater = SpecificGasConstantOfWater;

        /// <summary>
        /// The ratio of the specific gas constants of dry air to water, in SI base units.
        /// </summary>
        public static readonly HugeNumber RSpecificRatioOfDryAirToWater = RSpecificDryAir / RSpecificWater;

        /// <summary>
        /// The specific heat of dry air at constant pressure, in SI base units.
        /// </summary>
        public static readonly HugeNumber SpecificHeatOfDryAir = new(10035, -1);
        /// <summary>
        /// The specific heat of dry air at constant pressure, in SI base units.
        /// </summary>
        public static readonly HugeNumber CpDryAir = SpecificHeatOfDryAir;

        /// <summary>
        /// The specific heat multiplied by the specific gas constant of dry air at constant pressure, in SI base units.
        /// </summary>
        public static readonly HugeNumber CpTimesRSpecificDryAir = CpDryAir * RSpecificDryAir;

        /// <summary>
        /// The specific gas constant divided by the specific heat of dry air at constant pressure, in SI base units.
        /// </summary>
        public static readonly HugeNumber RSpecificOverCpDryAir = RSpecificDryAir / CpDryAir;

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
        public static readonly HugeNumber SpeedOfLightSquared = ((HugeNumber)SpeedOfLight).Square();
        /// <summary>
        /// The speed of light in a vacuum, squared, in m/s.
        /// </summary>
        public static readonly HugeNumber cSquared = SpeedOfLightSquared;

        /// <summary>
        /// The standard atmospheric pressure, in SI base units.
        /// </summary>
        public static readonly HugeNumber StandardAtmosphericPressure = new(101325, -3);
        /// <summary>
        /// The standard atmospheric pressure, in SI base units.
        /// </summary>
        public static readonly HugeNumber atm = StandardAtmosphericPressure;

        /// <summary>
        /// The Stefan–Boltzmann constant, in SI base units.
        /// </summary>
        public static readonly HugeNumber StefanBoltzmannConstant = new(5670367, -14);
        /// <summary>
        /// The Stefan–Boltzmann constant, in SI base units.
        /// </summary>
        public static readonly HugeNumber sigma = StefanBoltzmannConstant;

        /// <summary>
        /// Four times The Stefan–Boltzmann constant, in SI base units.
        /// </summary>
        public static readonly HugeNumber FourSigma = 4 * sigma;

        /// <summary>
        /// The universal gas constant, in SI base units.
        /// </summary>
        public static readonly HugeNumber UniversalGasConstant = new(83144598, -7);
        /// <summary>
        /// The universal gas constant, in SI base units.
        /// </summary>
        public static readonly HugeNumber R = UniversalGasConstant;

        /// <summary>
        /// The molar mass of air divided by the universal gas constant, in SI base units.
        /// </summary>
        public static readonly HugeNumber MAirOverR = MAir / R;
    }
}
