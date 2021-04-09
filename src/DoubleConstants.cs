namespace Tavenem.Mathematics
{
    /// <summary>
    /// <para>
    /// A collection of mathematical and scientific constants, as well as the results of some
    /// precalculated operations involving those constants.
    /// </para>
    /// <para>
    /// The constants in this class are <see cref="double"/> values. The <see
    /// cref="DecimalConstants"/> class contains the same constants as <see cref="decimal"/> values,
    /// and the <see cref="HugeNumberConstants"/> class contains the same constants as <see
    /// cref="Tavenem.HugeNumbers.HugeNumber"/> values.
    /// </para>
    /// </summary>
    /// <remarks>
    /// Some of the constants available here are provided as <see cref="int"/> or <see cref="long"/>
    /// values, where their value is a whole number, and small enough to fit in those data types.
    /// </remarks>
    public static class DoubleConstants
    {
        #region Math

        /// <summary>
        /// A convenience shortcut to <see cref="System.Math.E"/>. Represents the natural
        /// logarithmic base, specified by the constant, e.
        /// </summary>
        public const double e = System.Math.E;

        /// <summary>
        /// 1/8π
        /// </summary>
        public const double EighthPI = PI / 8;

        /// <summary>
        /// 1/e
        /// </summary>
        public const double InverseE = 1 / e;

        /// <summary>
        /// 4π
        /// </summary>
        public const double FourPI = PI * 4;

        /// <summary>
        /// π+⅓π
        /// </summary>
        public const double FourThirdsPI = PI * 4 / 3;

        /// <summary>
        /// ½π
        /// </summary>
        public const double HalfPI = PI / 2;

        /// <summary>
        /// 1/π
        /// </summary>
        public const double InversePI = 1 / PI;

        /// <summary>
        /// The natural logarithm of 2, rounded to 20 places of precision (more than can be
        /// accurately represented by the <see cref="double"/> structure).
        /// </summary>
        public const double Ln2 = 0.69314718055994530942;

        /// <summary>
        /// The natural logarithm of 10, rounded to 20 places of precision (more than can be
        /// accurately represented by the <see cref="double"/> structure).
        /// </summary>
        public const double Ln10 = 2.3025850929940456840;

        /// <summary>
        /// A floating-point value close to zero, intended to determine near-equivalence to 0.
        /// </summary>
        public const double NearlyZero = 1e-15;

        /// <summary>
        /// <para>
        /// A floating-point value close to zero, intended to determine near-equivalence to 0.
        /// </para>
        /// <para>
        /// This value has less precision than <see cref="NearlyZero"/>, and is better suited when
        /// checking <see cref="float"/> values.
        /// </para>
        /// </summary>
        public const float NearlyZeroFloat = 1e-6f;

        /// <summary>
        /// 180/π
        /// </summary>
        public const double OneEightyOverPI = 180 / PI;

        /// <summary>
        /// Represents the golden ratio, specified by the constant, φ, rounded to 20 places of
        /// precision (more than can be accurately represented by the <see cref="double"/>
        /// structure).
        /// </summary>
        public const double phi = 1.6180339887498948482;

        /// <summary>
        /// A convenience shortcut to <see cref="System.Math.PI"/>. Represents the ratio of the
        /// circumference of a circle to its diameter, specified by the constant, π.
        /// </summary>
        public const double PI = System.Math.PI;

        /// <summary>
        /// π/180
        /// </summary>
        public const double PIOver180 = PI / 180;

        /// <summary>
        /// π²
        /// </summary>
        public const double PISquared = PI * PI;

        /// <summary>
        /// √2 rounded to 20 places of precision (more than can be accurately represented by the
        /// <see cref="double"/> structure).
        /// </summary>
        public const double Root2 = 1.4142135623730950488;

        /// <summary>
        /// ¼π
        /// </summary>
        public const double QuarterPI = PI / 4;

        /// <summary>
        /// 1/6π
        /// </summary>
        public const double SixthPI = PI / 6;

        /// <summary>
        /// ⅓π
        /// </summary>
        public const double ThirdPI = PI / 3;

        /// <summary>
        /// 3π
        /// </summary>
        public const double ThreePI = PI * 3;

        /// <summary>
        /// 3/2π
        /// </summary>
        public const double ThreeHalvesPI = PI * 3 / 2;

        /// <summary>
        /// 3/4π
        /// </summary>
        public const double ThreeQuartersPI = PI * 3 / 4;

        /// <summary>
        /// 2π
        /// </summary>
        public const double TwoPI = PI * 2;

        /// <summary>
        /// 2π²
        /// </summary>
        public const double TwoPISquared = 2 * PI * PI;

        #endregion Math

        #region Science

        /// <summary>
        /// The Avogadro constant (<i>N</i><sub>A</sub>, <i>L</i>), in SI base units.
        /// </summary>
        public const double AvogadroConstant = 6.02214076e23;
        /// <summary>
        /// The Avogadro constant (<i>N</i><sub>A</sub>, <i>L</i>), in SI base units.
        /// </summary>
        public const double L = AvogadroConstant;

        /// <summary>
        /// The Boltzmann constant (<i>k</i><sub>B</sub>, <i>k</i>), in SI base units.
        /// </summary>
        public const double BoltzmannConstant = 1.380649e-23;
        /// <summary>
        /// The Boltzmann constant (<i>k</i><sub>B</sub>, <i>k</i>), in SI base units.
        /// </summary>
        public const double k = BoltzmannConstant;

        /// <summary>
        /// The specific heat multiplied by the specific gas constant of dry air at constant pressure, in SI base units.
        /// </summary>
        public const double CpTimesRSpecificDryAir = CpDryAir * RSpecificDryAir;

        /// <summary>
        /// The heat of vaporization of water, squared, in SI base units.
        /// </summary>
        public const double DeltaHvapWaterSquared = (double)DeltaHvapWater * DeltaHvapWater;

        /// <summary>
        /// The mass of a electron, in kg.
        /// </summary>
        public const double ElectronMass = 9.10938356e-31;

        /// <summary>
        /// The elementary charge (<i>e</i>, <i>q</i><sub>e</sub>), in coulombs.
        /// </summary>
        public const double ElementaryCharge = 1.602176634e-19;
        /// <summary>
        /// The elementary charge (<i>e</i>, <i>q</i><sub>e</sub>), in coulombs.
        /// </summary>
        public const double qe = ElementaryCharge;

        /// <summary>
        /// Four times The Stefan–Boltzmann constant, in SI base units.
        /// </summary>
        public const double FourSigma = 4 * sigma;

        /// <summary>
        /// The gravitational constant, in SI base units.
        /// </summary>
        public const double GravitationalConstant = 6.67408e-11;
        /// <summary>
        /// The gravitational constant, in SI base units.
        /// </summary>
        public const double G = GravitationalConstant;
        /// <summary>
        /// Twice the gravitational constant, in SI base units.
        /// </summary>
        public const double TwoG = 2 * G;

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
        public const double MolarMassOfAir = 0.0289644;
        /// <summary>
        /// The molar mass of air, in SI base units.
        /// </summary>
        public const double MAir = MolarMassOfAir;

        /// <summary>
        /// The molar mass of air divided by the universal gas constant, in SI base units.
        /// </summary>
        public const double MAirOverR = MAir / R;

        /// <summary>
        /// The mass of a neutron, in kg.
        /// </summary>
        public const double NeutronMass = 1.674927471e-27;

        /// <summary>
        /// The Planck constant (<i>h</i>) in SI base units.
        /// </summary>
        public const double PlanckConstant = 6.62607015e-34;
        /// <summary>
        /// The Planck constant (<i>h</i>) in SI base units.
        /// </summary>
        public const double h = PlanckConstant;

        /// <summary>
        /// The mass of a proton, in kg.
        /// </summary>
        public const double ProtonMass = 1.672621898e-27;

        /// <summary>
        /// The specific gas constant divided by the specific heat of dry air at constant pressure, in SI base units.
        /// </summary>
        public const double RSpecificOverCpDryAir = RSpecificDryAir / CpDryAir;

        /// <summary>
        /// The ratio of the specific gas constants of dry air to water, in SI base units.
        /// </summary>
        public const double RSpecificRatioOfDryAirToWater = RSpecificDryAir / RSpecificWater;

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
        public const double SpecificGasConstantOfWater = 461.5;
        /// <summary>
        /// The specific gas constant of water, in SI base units.
        /// </summary>
        public const double RSpecificWater = SpecificGasConstantOfWater;

        /// <summary>
        /// The specific heat of dry air at constant pressure, in SI base units.
        /// </summary>
        public const double SpecificHeatOfDryAir = 1003.5;
        /// <summary>
        /// The specific heat of dry air at constant pressure, in SI base units.
        /// </summary>
        public const double CpDryAir = SpecificHeatOfDryAir;

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
        public const double SpeedOfLightSquared = (double)SpeedOfLight * SpeedOfLight;
        /// <summary>
        /// The speed of light in a vacuum, squared, in m/s.
        /// </summary>
        public const double cSquared = SpeedOfLightSquared;

        /// <summary>
        /// The standard atmospheric pressure, in SI base units.
        /// </summary>
        public const double StandardAtmosphericPressure = 101.325;
        /// <summary>
        /// The standard atmospheric pressure, in SI base units.
        /// </summary>
        public const double atm = StandardAtmosphericPressure;

        /// <summary>
        /// The Stefan–Boltzmann constant, in SI base units.
        /// </summary>
        public const double StefanBoltzmannConstant = 5.670367e-8;
        /// <summary>
        /// The Stefan–Boltzmann constant, in SI base units.
        /// </summary>
        public const double sigma = StefanBoltzmannConstant;

        /// <summary>
        /// The universal gas constant, in SI base units.
        /// </summary>
        public const double UniversalGasConstant = 8.3144598;
        /// <summary>
        /// The universal gas constant, in SI base units.
        /// </summary>
        public const double R = UniversalGasConstant;

        #endregion Science
    }
}
