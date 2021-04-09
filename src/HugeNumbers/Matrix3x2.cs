using System;
using System.Runtime.Serialization;
using Tavenem.HugeNumbers;

namespace Tavenem.Mathematics.HugeNumbers
{
    /// <summary>
    /// A structure encapsulating a 3x2 matrix.
    /// </summary>
    /// <remarks>
    /// Unlike the <see cref="System.Numerics.Matrix3x2"/> implementation, this is an immutable struct
    /// with read-only properties, rather than writable fields.
    /// </remarks>
    [Serializable]
    [DataContract]
    public struct Matrix3x2 : IEquatable<Matrix3x2>
    {
        private static readonly HugeNumber _AngleEpsilon = HugeNumber.Milli * HugeNumber.PIOver180; // 0.1% of a degree

        /// <summary>
        /// Returns the multiplicative identity matrix.
        /// </summary>
        public static Matrix3x2 Identity { get; } = new Matrix3x2
        (
            HugeNumber.One, HugeNumber.Zero,
            HugeNumber.Zero, HugeNumber.One,
            HugeNumber.Zero, HugeNumber.Zero
        );

        /// <summary>
        /// The first element of the first row.
        /// </summary>
        [DataMember(Order = 1)]
        public HugeNumber M11 { get; }

        /// <summary>
        /// The second element of the first row.
        /// </summary>
        [DataMember(Order = 2)]
        public HugeNumber M12 { get; }

        /// <summary>
        /// The first element of the second row.
        /// </summary>
        [DataMember(Order = 3)]
        public HugeNumber M21 { get; }

        /// <summary>
        /// The second element of the second row.
        /// </summary>
        [DataMember(Order = 4)]
        public HugeNumber M22 { get; }

        /// <summary>
        /// The first element of the third row.
        /// </summary>
        [DataMember(Order = 5)]
        public HugeNumber M31 { get; }

        /// <summary>
        /// The second element of the third row.
        /// </summary>
        [DataMember(Order = 6)]
        public HugeNumber M32 { get; }

        /// <summary>
        /// Returns whether the matrix is the identity matrix.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public bool IsIdentity => Equals(Identity);

        /// <summary>
        /// Gets the translation component of this matrix.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Vector2 Translation => new(M31, M32);

        /// <summary>
        /// Constructs a Matrix3x2 from the given components.
        /// </summary>
        /// <param name="m11">The first element of the first row.</param>
        /// <param name="m12">The second element of the first row.</param>
        /// <param name="m21">The first element of the second row.</param>
        /// <param name="m22">The second element of the second row.</param>
        /// <param name="m31">The first element of the third row.</param>
        /// <param name="m32">The second element of the third row.</param>
        [System.Text.Json.Serialization.JsonConstructor]
        public Matrix3x2(HugeNumber m11, HugeNumber m12,
                         HugeNumber m21, HugeNumber m22,
                         HugeNumber m31, HugeNumber m32)
        {
            M11 = m11;
            M12 = m12;
            M21 = m21;
            M22 = m22;
            M31 = m31;
            M32 = m32;
        }

        /// <summary>
        /// Creates a translation matrix from the given vector.
        /// </summary>
        /// <param name="position">The translation position.</param>
        /// <returns>A translation matrix.</returns>
        public static Matrix3x2 CreateTranslation(Vector2 position)
            => new(HugeNumber.One, HugeNumber.Zero, HugeNumber.Zero, HugeNumber.One, position.X, position.Y);

        /// <summary>
        /// Creates a translation matrix from the given X and Y components.
        /// </summary>
        /// <param name="xPosition">The X position.</param>
        /// <param name="yPosition">The Y position.</param>
        /// <returns>A translation matrix.</returns>
        public static Matrix3x2 CreateTranslation(HugeNumber xPosition, HugeNumber yPosition)
            => new(HugeNumber.One, HugeNumber.Zero, HugeNumber.Zero, HugeNumber.One, xPosition, yPosition);

        /// <summary>
        /// Creates a scale matrix from the given X and Y components.
        /// </summary>
        /// <param name="xScale">Value to scale by on the X-axis.</param>
        /// <param name="yScale">Value to scale by on the Y-axis.</param>
        /// <returns>A scaling matrix.</returns>
        public static Matrix3x2 CreateScale(HugeNumber xScale, HugeNumber yScale)
            => new(xScale, HugeNumber.Zero, HugeNumber.Zero, yScale, HugeNumber.Zero, HugeNumber.Zero);

        /// <summary>
        /// Creates a scale matrix that is offset by a given center point.
        /// </summary>
        /// <param name="xScale">Value to scale by on the X-axis.</param>
        /// <param name="yScale">Value to scale by on the Y-axis.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>A scaling matrix.</returns>
        public static Matrix3x2 CreateScale(HugeNumber xScale, HugeNumber yScale, Vector2 centerPoint)
            => new(xScale, HugeNumber.Zero, HugeNumber.Zero, yScale, centerPoint.X * (HugeNumber.One - xScale), centerPoint.Y * (HugeNumber.One - yScale));

        /// <summary>
        /// Creates a scale matrix from the given vector scale.
        /// </summary>
        /// <param name="scales">The scale to use.</param>
        /// <returns>A scaling matrix.</returns>
        public static Matrix3x2 CreateScale(Vector2 scales)
            => new(scales.X, HugeNumber.Zero, HugeNumber.Zero, scales.Y, HugeNumber.Zero, HugeNumber.Zero);

        /// <summary>
        /// Creates a scale matrix from the given vector scale with an offset from the given center point.
        /// </summary>
        /// <param name="scales">The scale to use.</param>
        /// <param name="centerPoint">The center offset.</param>
        /// <returns>A scaling matrix.</returns>
        public static Matrix3x2 CreateScale(Vector2 scales, Vector2 centerPoint)
            => new(scales.X, HugeNumber.Zero, HugeNumber.Zero, scales.Y, centerPoint.X * (HugeNumber.One - scales.X), centerPoint.Y * (HugeNumber.One - scales.Y));

        /// <summary>
        /// Creates a scale matrix that scales uniformly with the given scale.
        /// </summary>
        /// <param name="scale">The uniform scale to use.</param>
        /// <returns>A scaling matrix.</returns>
        public static Matrix3x2 CreateScale(HugeNumber scale)
            => new(scale, HugeNumber.Zero, HugeNumber.Zero, scale, HugeNumber.Zero, HugeNumber.Zero);

        /// <summary>
        /// Creates a scale matrix that scales uniformly with the given scale with an offset from the given center.
        /// </summary>
        /// <param name="scale">The uniform scale to use.</param>
        /// <param name="centerPoint">The center offset.</param>
        /// <returns>A scaling matrix.</returns>
        public static Matrix3x2 CreateScale(HugeNumber scale, Vector2 centerPoint)
            => new(scale, HugeNumber.Zero, HugeNumber.Zero, scale, centerPoint.X * (HugeNumber.One - scale), centerPoint.Y * (HugeNumber.One - scale));

        /// <summary>
        /// Creates a skew matrix from the given angles in radians.
        /// </summary>
        /// <param name="radiansX">The X angle, in radians.</param>
        /// <param name="radiansY">The Y angle, in radians.</param>
        /// <returns>A skew matrix.</returns>
        public static Matrix3x2 CreateSkew(HugeNumber radiansX, HugeNumber radiansY)
            => new(HugeNumber.One, HugeNumber.Tan(radiansY), HugeNumber.Tan(radiansX), HugeNumber.One, HugeNumber.Zero, HugeNumber.Zero);

        /// <summary>
        /// Creates a skew matrix from the given angles in radians and a center point.
        /// </summary>
        /// <param name="radiansX">The X angle, in radians.</param>
        /// <param name="radiansY">The Y angle, in radians.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>A skew matrix.</returns>
        public static Matrix3x2 CreateSkew(HugeNumber radiansX, HugeNumber radiansY, Vector2 centerPoint)
        {
            var xTan = HugeNumber.Tan(radiansX);
            var yTan = HugeNumber.Tan(radiansY);

            return new Matrix3x2(HugeNumber.One, yTan, xTan, HugeNumber.One, -centerPoint.Y * xTan, -centerPoint.X * yTan);
        }

        /// <summary>
        /// Creates a rotation matrix using the given rotation in radians.
        /// </summary>
        /// <param name="radians">The amount of rotation, in radians.</param>
        /// <returns>A rotation matrix.</returns>
        public static Matrix3x2 CreateRotation(HugeNumber radians)
        {
            radians = HugeNumber.Mod(radians, HugeNumber.TwoPI);

            HugeNumber c, s;

            if (radians > -_AngleEpsilon && radians < _AngleEpsilon)
            {
                // Exact case for zero rotation.
                c = HugeNumber.One;
                s = HugeNumber.Zero;
            }
            else if (radians > HugeNumber.HalfPI - _AngleEpsilon && radians < HugeNumber.HalfPI + _AngleEpsilon)
            {
                // Exact case for 90 degree rotation.
                c = HugeNumber.Zero;
                s = HugeNumber.One;
            }
            else if (radians < -HugeNumber.PI + _AngleEpsilon || radians > HugeNumber.PI - _AngleEpsilon)
            {
                // Exact case for 180 degree rotation.
                c = -HugeNumber.One;
                s = HugeNumber.Zero;
            }
            else if (radians > -HugeNumber.HalfPI - _AngleEpsilon && radians < -HugeNumber.HalfPI + _AngleEpsilon)
            {
                // Exact case for 270 degree rotation.
                c = HugeNumber.Zero;
                s = -HugeNumber.One;
            }
            else
            {
                // Arbitrary rotation.
                c = HugeNumber.Cos(radians);
                s = HugeNumber.Sin(radians);
            }

            return new Matrix3x2(c, s, -s, c, HugeNumber.Zero, HugeNumber.Zero);
        }

        /// <summary>
        /// Creates a rotation matrix using the given rotation in radians and a center point.
        /// </summary>
        /// <param name="radians">The amount of rotation, in radians.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>A rotation matrix.</returns>
        public static Matrix3x2 CreateRotation(HugeNumber radians, Vector2 centerPoint)
        {
            radians %= HugeNumber.TwoPI;

            HugeNumber c, s;

            if (radians > -_AngleEpsilon && radians < _AngleEpsilon)
            {
                // Exact case for zero rotation.
                c = HugeNumber.One;
                s = HugeNumber.Zero;
            }
            else if (radians > HugeNumber.HalfPI - _AngleEpsilon && radians < HugeNumber.HalfPI + _AngleEpsilon)
            {
                // Exact case for 90 degree rotation.
                c = HugeNumber.Zero;
                s = HugeNumber.One;
            }
            else if (radians < -HugeNumber.PI + _AngleEpsilon || radians > HugeNumber.PI - _AngleEpsilon)
            {
                // Exact case for 180 degree rotation.
                c = -HugeNumber.One;
                s = HugeNumber.Zero;
            }
            else if (radians > -HugeNumber.HalfPI - _AngleEpsilon && radians < -HugeNumber.HalfPI + _AngleEpsilon)
            {
                // Exact case for 270 degree rotation.
                c = HugeNumber.Zero;
                s = -HugeNumber.One;
            }
            else
            {
                // Arbitrary rotation.
                c = HugeNumber.Cos(radians);
                s = HugeNumber.Sin(radians);
            }

            var x = (centerPoint.X * (HugeNumber.One - c)) + (centerPoint.Y * s);
            var y = (centerPoint.Y * (HugeNumber.One - c)) - (centerPoint.X * s);

            return new Matrix3x2(c, s, -s, c, x, y);
        }

        /// <summary>
        /// Attempts to invert the given matrix. If the operation succeeds, the inverted matrix is stored in the result parameter.
        /// </summary>
        /// <param name="matrix">The source matrix.</param>
        /// <param name="result">The output matrix.</param>
        /// <returns>True if the operation succeeded, False otherwise.</returns>
        public static bool Invert(Matrix3x2 matrix, out Matrix3x2 result)
        {
            var det = matrix.GetDeterminant();

            if (HugeNumber.Abs(det) == HugeNumber.Zero)
            {
                result = Identity;
                return false;
            }

            var invDet = HugeNumber.One / det;

            result = new Matrix3x2(
                matrix.M22 * invDet,
                -matrix.M12 * invDet,
                -matrix.M21 * invDet,
                matrix.M11 * invDet,
                ((matrix.M21 * matrix.M32) - (matrix.M31 * matrix.M22)) * invDet,
                ((matrix.M31 * matrix.M12) - (matrix.M11 * matrix.M32)) * invDet);

            return true;
        }

        /// <summary>
        /// Linearly interpolates from matrix1 to matrix2, based on the third parameter.
        /// </summary>
        /// <param name="matrix1">The first source matrix.</param>
        /// <param name="matrix2">The second source matrix.</param>
        /// <param name="amount">The relative weighting of matrix2.</param>
        /// <returns>The interpolated matrix.</returns>
        public static Matrix3x2 Lerp(Matrix3x2 matrix1, Matrix3x2 matrix2, HugeNumber amount) => new(
            // First row
            matrix1.M11 + ((matrix2.M11 - matrix1.M11) * amount),
            matrix1.M12 + ((matrix2.M12 - matrix1.M12) * amount),

            // Second row
            matrix1.M21 + ((matrix2.M21 - matrix1.M21) * amount),
            matrix1.M22 + ((matrix2.M22 - matrix1.M22) * amount),

            // Third row
            matrix1.M31 + ((matrix2.M31 - matrix1.M31) * amount),
            matrix1.M32 + ((matrix2.M32 - matrix1.M32) * amount));

        /// <summary>
        /// Returns a boolean indicating whether the given Object is equal to this matrix instance.
        /// </summary>
        /// <param name="obj">The Object to compare against.</param>
        /// <returns>True if the Object is equal to this matrix; False otherwise.</returns>
        public override bool Equals(object? obj) => obj is Matrix3x2 other && Equals(other);

        /// <summary>
        /// Returns a boolean indicating whether the matrix is equal to the other given matrix.
        /// </summary>
        /// <param name="other">The other matrix to test equality against.</param>
        /// <returns>True if this matrix is equal to other; False otherwise.</returns>
        /// <remarks>
        /// Checks diagonal element first for early out.
        /// </remarks>
        public bool Equals(Matrix3x2 other)
            => M11 == other.M11 && M22 == other.M22 && M12 == other.M12 && M21 == other.M21 && M31 == other.M31 && M32 == other.M32;

        /// <summary>
        /// Calculates the determinant for this matrix.
        /// The determinant is calculated by expanding the matrix with a third column whose values are (HugeNumber.Zero,HugeNumber.Zero,HugeNumber.One).
        /// </summary>
        /// <returns>The determinant.</returns>
        /// <remarks>
        /// <para>
        /// There isn't actually any such thing as a determinant for a non-square matrix,
        /// but this 3x2 type is really just an optimization of a 3x3 where we happen to
        /// know the rightmost column is always (HugeNumber.Zero, HugeNumber.Zero, HugeNumber.One). So we expand to 3x3 format:
        /// </para>
        /// <para>
        ///  [ M11, M12, HugeNumber.Zero ]
        ///  [ M21, M22, HugeNumber.Zero ]
        ///  [ M31, M32, HugeNumber.One ]
        /// </para>
        /// <para>
        /// Sum the diagonal products:
        ///  (M11 * M22 * HugeNumber.One) + (M12 * HugeNumber.Zero * M31) + (HugeNumber.Zero * M21 * M32)
        /// </para>
        /// <para>
        /// Subtract the opposite diagonal products:
        ///  (M31 * M22 * HugeNumber.Zero) + (M32 * HugeNumber.Zero * M11) + (HugeNumber.One * M21 * M12)
        /// </para>
        /// <para>Collapse out the constants and oh look, this is just a 2x2 determinant!</para>
        /// </remarks>
        public HugeNumber GetDeterminant() => (M11 * M22) - (M21 * M12);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
            => unchecked(M11.GetHashCode() + M12.GetHashCode()
            + M21.GetHashCode() + M22.GetHashCode()
            + M31.GetHashCode() + M32.GetHashCode());

        /// <summary>
        /// Returns a String representing this matrix instance.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            var ci = HugeNumberFormatProvider.Instance;
            return string.Format(ci, "{{ {{M11:{0} M12:{1}}} {{M21:{2} M22:{3}}} {{M31:{4} M32:{5}}} }}",
                                 M11.ToString(ci), M12.ToString(ci),
                                 M21.ToString(ci), M22.ToString(ci),
                                 M31.ToString(ci), M32.ToString(ci));
        }

        /// <summary>
        /// Negates the given matrix by multiplying all values by -HugeNumber.One.
        /// </summary>
        /// <param name="value">The source matrix.</param>
        /// <returns>The negated matrix.</returns>
        public static Matrix3x2 Negate(Matrix3x2 value) => new(
            -value.M11,
            -value.M12,
            -value.M21,
            -value.M22,
            -value.M31,
            -value.M32);

        /// <summary>
        /// Adds each matrix element in value1 with its corresponding element in value2.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The matrix containing the summed values.</returns>
        public static Matrix3x2 Add(Matrix3x2 value1, Matrix3x2 value2) => new(
            value1.M11 + value2.M11,
            value1.M12 + value2.M12,
            value1.M21 + value2.M21,
            value1.M22 + value2.M22,
            value1.M31 + value2.M31,
            value1.M32 + value2.M32);

        /// <summary>
        /// Subtracts each matrix element in value2 from its corresponding element in value1.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The matrix containing the resulting values.</returns>
        public static Matrix3x2 Subtract(Matrix3x2 value1, Matrix3x2 value2) => new(
            value1.M11 - value2.M11,
            value1.M12 - value2.M12,
            value1.M21 - value2.M21,
            value1.M22 - value2.M22,
            value1.M31 - value2.M31,
            value1.M32 - value2.M32);

        /// <summary>
        /// Multiplies two matrices together and returns the resulting matrix.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The product matrix.</returns>
        public static Matrix3x2 Multiply(Matrix3x2 value1, Matrix3x2 value2) => new(
            // First row
            (value1.M11 * value2.M11) + (value1.M12 * value2.M21),
            (value1.M11 * value2.M12) + (value1.M12 * value2.M22),

            // Second row
            (value1.M21 * value2.M11) + (value1.M22 * value2.M21),
            (value1.M21 * value2.M12) + (value1.M22 * value2.M22),

            // Third row
            (value1.M31 * value2.M11) + (value1.M32 * value2.M21) + value2.M31,
            (value1.M31 * value2.M12) + (value1.M32 * value2.M22) + value2.M32);

        /// <summary>
        /// Scales all elements in a matrix by the given scalar factor.
        /// </summary>
        /// <param name="value1">The source matrix.</param>
        /// <param name="value2">The scaling value to use.</param>
        /// <returns>The resulting matrix.</returns>
        public static Matrix3x2 Multiply(Matrix3x2 value1, HugeNumber value2) => new(
            value1.M11 * value2,
            value1.M12 * value2,
            value1.M21 * value2,
            value1.M22 * value2,
            value1.M31 * value2,
            value1.M32 * value2);

        /// <summary>
        /// Negates the given matrix by multiplying all values by -HugeNumber.One.
        /// </summary>
        /// <param name="value">The source matrix.</param>
        /// <returns>The negated matrix.</returns>
        public static Matrix3x2 operator -(Matrix3x2 value) => new(
            -value.M11,
            -value.M12,
            -value.M21,
            -value.M22,
            -value.M31,
            -value.M32);

        /// <summary>
        /// Adds each matrix element in value1 with its corresponding element in value2.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The matrix containing the summed values.</returns>
        public static Matrix3x2 operator +(Matrix3x2 value1, Matrix3x2 value2) => new(
            value1.M11 + value2.M11,
            value1.M12 + value2.M12,
            value1.M21 + value2.M21,
            value1.M22 + value2.M22,
            value1.M31 + value2.M31,
            value1.M32 + value2.M32);

        /// <summary>
        /// Subtracts each matrix element in value2 from its corresponding element in value1.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The matrix containing the resulting values.</returns>
        public static Matrix3x2 operator -(Matrix3x2 value1, Matrix3x2 value2) => new(
            value1.M11 - value2.M11,
            value1.M12 - value2.M12,
            value1.M21 - value2.M21,
            value1.M22 - value2.M22,
            value1.M31 - value2.M31,
            value1.M32 - value2.M32);

        /// <summary>
        /// Multiplies two matrices together and returns the resulting matrix.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The product matrix.</returns>
        public static Matrix3x2 operator *(Matrix3x2 value1, Matrix3x2 value2) => new(
            // First row
            (value1.M11 * value2.M11) + (value1.M12 * value2.M21),
            (value1.M11 * value2.M12) + (value1.M12 * value2.M22),

            // Second row
            (value1.M21 * value2.M11) + (value1.M22 * value2.M21),
            (value1.M21 * value2.M12) + (value1.M22 * value2.M22),

            // Third row
            (value1.M31 * value2.M11) + (value1.M32 * value2.M21) + value2.M31,
            (value1.M31 * value2.M12) + (value1.M32 * value2.M22) + value2.M32);

        /// <summary>
        /// Scales all elements in a matrix by the given scalar factor.
        /// </summary>
        /// <param name="value1">The source matrix.</param>
        /// <param name="value2">The scaling value to use.</param>
        /// <returns>The resulting matrix.</returns>
        public static Matrix3x2 operator *(Matrix3x2 value1, HugeNumber value2) => new(
            value1.M11 * value2,
            value1.M12 * value2,
            value1.M21 * value2,
            value1.M22 * value2,
            value1.M31 * value2,
            value1.M32 * value2);

        /// <summary>
        /// Returns a boolean indicating whether the given matrices are equal.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>True if the matrices are equal; False otherwise.</returns>
        public static bool operator ==(Matrix3x2 value1, Matrix3x2 value2)
            => value1.Equals(value2);

        /// <summary>
        /// Returns a boolean indicating whether the given matrices are not equal.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>True if the matrices are not equal; False if they are equal.</returns>
        public static bool operator !=(Matrix3x2 value1, Matrix3x2 value2)
            => !value1.Equals(value2);

        /// <summary>
        /// Converts between implementations of matrices.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator Matrix3x2(System.Numerics.Matrix3x2 value)
            => new(
                value.M11, value.M12,
                value.M21, value.M22,
                value.M31, value.M32);

        /// <summary>
        /// Converts between implementations of matrices.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator Matrix3x2(Doubles.Matrix3x2 value)
            => new(
                value.M11, value.M12,
                value.M21, value.M22,
                value.M31, value.M32);

        /// <summary>
        /// Converts between implementations of matrices.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static explicit operator Matrix3x2(Decimals.Matrix3x2 value)
            => new(
                (HugeNumber)value.M11, (HugeNumber)value.M12,
                (HugeNumber)value.M21, (HugeNumber)value.M22,
                (HugeNumber)value.M31, (HugeNumber)value.M32);

        /// <summary>
        /// Converts between implementations of matrices.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static explicit operator System.Numerics.Matrix3x2(Matrix3x2 value)
            => new(
                (float)value.M11, (float)value.M12,
                (float)value.M21, (float)value.M22,
                (float)value.M31, (float)value.M32);
    }
}
