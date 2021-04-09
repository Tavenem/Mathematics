using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Tavenem.Mathematics.Doubles
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
        /// <summary>
        /// Returns the multiplicative identity matrix.
        /// </summary>
        public static Matrix3x2 Identity { get; } = new Matrix3x2
        (
            1, 0,
            0, 1,
            0, 0
        );

        /// <summary>
        /// The first element of the first row.
        /// </summary>
        [DataMember(Order = 1)]
        public double M11 { get; }

        /// <summary>
        /// The second element of the first row.
        /// </summary>
        [DataMember(Order = 2)]
        public double M12 { get; }

        /// <summary>
        /// The first element of the second row.
        /// </summary>
        [DataMember(Order = 3)]
        public double M21 { get; }

        /// <summary>
        /// The second element of the second row.
        /// </summary>
        [DataMember(Order = 4)]
        public double M22 { get; }

        /// <summary>
        /// The first element of the third row.
        /// </summary>
        [DataMember(Order = 5)]
        public double M31 { get; }

        /// <summary>
        /// The second element of the third row.
        /// </summary>
        [DataMember(Order = 6)]
        public double M32 { get; }

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
        public Matrix3x2(double m11, double m12,
                         double m21, double m22,
                         double m31, double m32)
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
            => new(1, 0, 0, 1, position.X, position.Y);

        /// <summary>
        /// Creates a translation matrix from the given X and Y components.
        /// </summary>
        /// <param name="xPosition">The X position.</param>
        /// <param name="yPosition">The Y position.</param>
        /// <returns>A translation matrix.</returns>
        public static Matrix3x2 CreateTranslation(double xPosition, double yPosition)
            => new(1, 0, 0, 1, xPosition, yPosition);

        /// <summary>
        /// Creates a scale matrix from the given X and Y components.
        /// </summary>
        /// <param name="xScale">Value to scale by on the X-axis.</param>
        /// <param name="yScale">Value to scale by on the Y-axis.</param>
        /// <returns>A scaling matrix.</returns>
        public static Matrix3x2 CreateScale(double xScale, double yScale)
            => new(xScale, 0, 0, yScale, 0, 0);

        /// <summary>
        /// Creates a scale matrix that is offset by a given center point.
        /// </summary>
        /// <param name="xScale">Value to scale by on the X-axis.</param>
        /// <param name="yScale">Value to scale by on the Y-axis.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>A scaling matrix.</returns>
        public static Matrix3x2 CreateScale(double xScale, double yScale, Vector2 centerPoint)
            => new(xScale, 0, 0, yScale, centerPoint.X * (1 - xScale), centerPoint.Y * (1 - yScale));

        /// <summary>
        /// Creates a scale matrix from the given vector scale.
        /// </summary>
        /// <param name="scales">The scale to use.</param>
        /// <returns>A scaling matrix.</returns>
        public static Matrix3x2 CreateScale(Vector2 scales)
            => new(scales.X, 0, 0, scales.Y, 0, 0);

        /// <summary>
        /// Creates a scale matrix from the given vector scale with an offset from the given center point.
        /// </summary>
        /// <param name="scales">The scale to use.</param>
        /// <param name="centerPoint">The center offset.</param>
        /// <returns>A scaling matrix.</returns>
        public static Matrix3x2 CreateScale(Vector2 scales, Vector2 centerPoint)
            => new(scales.X, 0, 0, scales.Y, centerPoint.X * (1 - scales.X), centerPoint.Y * (1 - scales.Y));

        /// <summary>
        /// Creates a scale matrix that scales uniformly with the given scale.
        /// </summary>
        /// <param name="scale">The uniform scale to use.</param>
        /// <returns>A scaling matrix.</returns>
        public static Matrix3x2 CreateScale(double scale)
            => new(scale, 0, 0, scale, 0, 0);

        /// <summary>
        /// Creates a scale matrix that scales uniformly with the given scale with an offset from the given center.
        /// </summary>
        /// <param name="scale">The uniform scale to use.</param>
        /// <param name="centerPoint">The center offset.</param>
        /// <returns>A scaling matrix.</returns>
        public static Matrix3x2 CreateScale(double scale, Vector2 centerPoint)
            => new(scale, 0, 0, scale, centerPoint.X * (1 - scale), centerPoint.Y * (1 - scale));

        /// <summary>
        /// Creates a skew matrix from the given angles in radians.
        /// </summary>
        /// <param name="radiansX">The X angle, in radians.</param>
        /// <param name="radiansY">The Y angle, in radians.</param>
        /// <returns>A skew matrix.</returns>
        public static Matrix3x2 CreateSkew(double radiansX, double radiansY)
            => new(1, Math.Tan(radiansY), Math.Tan(radiansX), 1, 0, 0);

        /// <summary>
        /// Creates a skew matrix from the given angles in radians and a center point.
        /// </summary>
        /// <param name="radiansX">The X angle, in radians.</param>
        /// <param name="radiansY">The Y angle, in radians.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>A skew matrix.</returns>
        public static Matrix3x2 CreateSkew(double radiansX, double radiansY, Vector2 centerPoint)
        {
            var xTan = Math.Tan(radiansX);
            var yTan = Math.Tan(radiansY);

            return new Matrix3x2(1, yTan, xTan, 1, -centerPoint.Y * xTan, -centerPoint.X * yTan);
        }

        /// <summary>
        /// Creates a rotation matrix using the given rotation in radians.
        /// </summary>
        /// <param name="radians">The amount of rotation, in radians.</param>
        /// <returns>A rotation matrix.</returns>
        public static Matrix3x2 CreateRotation(double radians)
        {
            radians = Math.IEEERemainder(radians, DoubleConstants.TwoPI);

            double c, s;

            const double Epsilon = 0.001 * DoubleConstants.PIOver180;     // 0.1% of a degree

            if (radians is > (-Epsilon) and < Epsilon)
            {
                // Exact case for zero rotation.
                c = 1;
                s = 0;
            }
            else if (radians is > (DoubleConstants.HalfPI - Epsilon) and < (DoubleConstants.HalfPI + Epsilon))
            {
                // Exact case for 90 degree rotation.
                c = 0;
                s = 1;
            }
            else if (radians is < (-Math.PI + Epsilon) or > (Math.PI - Epsilon))
            {
                // Exact case for 180 degree rotation.
                c = -1;
                s = 0;
            }
            else if (radians is > (-DoubleConstants.HalfPI - Epsilon) and < (-DoubleConstants.HalfPI + Epsilon))
            {
                // Exact case for 270 degree rotation.
                c = 0;
                s = -1;
            }
            else
            {
                // Arbitrary rotation.
                c = Math.Cos(radians);
                s = Math.Sin(radians);
            }

            return new Matrix3x2(c, s, -s, c, 0, 0);
        }

        /// <summary>
        /// Creates a rotation matrix using the given rotation in radians and a center point.
        /// </summary>
        /// <param name="radians">The amount of rotation, in radians.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>A rotation matrix.</returns>
        public static Matrix3x2 CreateRotation(double radians, Vector2 centerPoint)
        {
            radians = Math.IEEERemainder(radians, DoubleConstants.TwoPI);

            double c, s;

            const double Epsilon = 0.001 * DoubleConstants.PIOver180;     // 0.1% of a degree

            if (radians is > (-Epsilon) and < Epsilon)
            {
                // Exact case for zero rotation.
                c = 1;
                s = 0;
            }
            else if (radians is > (DoubleConstants.HalfPI - Epsilon) and < (DoubleConstants.HalfPI + Epsilon))
            {
                // Exact case for 90 degree rotation.
                c = 0;
                s = 1;
            }
            else if (radians is < (-Math.PI + Epsilon) or > (Math.PI - Epsilon))
            {
                // Exact case for 180 degree rotation.
                c = -1;
                s = 0;
            }
            else if (radians is > (-DoubleConstants.HalfPI - Epsilon) and < (-DoubleConstants.HalfPI + Epsilon))
            {
                // Exact case for 270 degree rotation.
                c = 0;
                s = -1;
            }
            else
            {
                // Arbitrary rotation.
                c = Math.Cos(radians);
                s = Math.Sin(radians);
            }

            var x = (centerPoint.X * (1 - c)) + (centerPoint.Y * s);
            var y = (centerPoint.Y * (1 - c)) - (centerPoint.X * s);

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

            if (Math.Abs(det).IsNearlyZero())
            {
                result = Identity;
                return false;
            }

            var invDet = 1.0 / det;

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
        public static Matrix3x2 Lerp(Matrix3x2 matrix1, Matrix3x2 matrix2, double amount) => new(
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
        /// The determinant is calculated by expanding the matrix with a third column whose values are (0,0,1).
        /// </summary>
        /// <returns>The determinant.</returns>
        /// <remarks>
        /// <para>
        /// There isn't actually any such thing as a determinant for a non-square matrix,
        /// but this 3x2 type is really just an optimization of a 3x3 where we happen to
        /// know the rightmost column is always (0, 0, 1). So we expand to 3x3 format:
        /// </para>
        /// <para>
        ///  [ M11, M12, 0 ]
        ///  [ M21, M22, 0 ]
        ///  [ M31, M32, 1 ]
        /// </para>
        /// <para>
        /// Sum the diagonal products:
        ///  (M11 * M22 * 1) + (M12 * 0 * M31) + (0 * M21 * M32)
        /// </para>
        /// <para>
        /// Subtract the opposite diagonal products:
        ///  (M31 * M22 * 0) + (M32 * 0 * M11) + (1 * M21 * M12)
        /// </para>
        /// <para>Collapse out the constants and oh look, this is just a 2x2 determinant!</para>
        /// </remarks>
        public double GetDeterminant() => (M11 * M22) - (M21 * M12);

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
            var ci = CultureInfo.CurrentCulture;
            return string.Format(ci, "{{ {{M11:{0} M12:{1}}} {{M21:{2} M22:{3}}} {{M31:{4} M32:{5}}} }}",
                                 M11.ToString(ci), M12.ToString(ci),
                                 M21.ToString(ci), M22.ToString(ci),
                                 M31.ToString(ci), M32.ToString(ci));
        }

        /// <summary>
        /// Negates the given matrix by multiplying all values by -1.
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
        public static Matrix3x2 Multiply(Matrix3x2 value1, double value2) => new(
            value1.M11 * value2,
            value1.M12 * value2,
            value1.M21 * value2,
            value1.M22 * value2,
            value1.M31 * value2,
            value1.M32 * value2);

        /// <summary>
        /// Negates the given matrix by multiplying all values by -1.
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
        public static Matrix3x2 operator *(Matrix3x2 value1, double value2) => new(
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
        public static explicit operator Matrix3x2(HugeNumbers.Matrix3x2 value)
            => new(
                (double)value.M11, (double)value.M12,
                (double)value.M21, (double)value.M22,
                (double)value.M31, (double)value.M32);

        /// <summary>
        /// Converts between implementations of matrices.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static explicit operator Matrix3x2(Decimals.Matrix3x2 value)
            => new(
                (double)value.M11, (double)value.M12,
                (double)value.M21, (double)value.M22,
                (double)value.M31, (double)value.M32);

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
