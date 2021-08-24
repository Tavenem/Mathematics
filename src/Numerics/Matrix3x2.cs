using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace Tavenem.Mathematics;

/// <summary>
/// A structure encapsulating a 3x2 matrix.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public readonly struct Matrix3x2<TScalar> :
    IAdditionOperators<Matrix3x2<TScalar>, Matrix3x2<TScalar>, Matrix3x2<TScalar>>,
    IEqualityOperators<Matrix3x2<TScalar>, Matrix3x2<TScalar>>,
    IMultiplicativeIdentity<Matrix3x2<TScalar>, Matrix3x2<TScalar>>,
    IMultiplyOperators<Matrix3x2<TScalar>, Matrix3x2<TScalar>, Matrix3x2<TScalar>>,
    IMultiplyOperators<Matrix3x2<TScalar>, TScalar, Matrix3x2<TScalar>>,
    ISpanFormattable,
    ISubtractionOperators<Matrix3x2<TScalar>, Matrix3x2<TScalar>, Matrix3x2<TScalar>>,
    IUnaryNegationOperators<Matrix3x2<TScalar>, Matrix3x2<TScalar>>
    where TScalar : IFloatingPoint<TScalar>
{
    /// <summary>
    /// Returns the multiplicative identity matrix.
    /// </summary>
    public static Matrix3x2<TScalar> MultiplicativeIdentity { get; } = Matrix3x2<TScalar>.Create
    (
        TScalar.One, TScalar.Zero,
        TScalar.Zero, TScalar.One,
        TScalar.Zero, TScalar.Zero
    );

    /// <summary>
    /// Returns a matrix with zero for all elements.
    /// </summary>
    public static Matrix3x2<TScalar> Zero { get; } = new();
    /// <summary>
    /// Returns the additive identity matrix.
    /// </summary>
    public static Matrix3x2<TScalar> AdditiveIdentity { get; } = Matrix3x2<TScalar>.Zero;

    /// <summary>
    /// The first element of the first row.
    /// </summary>
    public TScalar M11 { get; init; }

    /// <summary>
    /// The second element of the first row.
    /// </summary>
    public TScalar M12 { get; init; }

    /// <summary>
    /// The first element of the second row.
    /// </summary>
    public TScalar M21 { get; init; }

    /// <summary>
    /// The second element of the second row.
    /// </summary>
    public TScalar M22 { get; init; }

    /// <summary>
    /// The first element of the third row.
    /// </summary>
    public TScalar M31 { get; init; }

    /// <summary>
    /// The second element of the third row.
    /// </summary>
    public TScalar M32 { get; init; }

    /// <summary>
    /// Gets the translation component of this matrix.
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    public Vector2<TScalar> Translation => new() { X = M31, Y = M32 };

    /// <summary>
    /// Create a new <see cref="Matrix3x2{TScalar}"/> instance with the given values.
    /// </summary>
    /// <param name="value11">The value for the 1x1 position in the matrix.</param>
    /// <param name="value12">The value for the 1x2 position in the matrix.</param>
    /// <param name="value21">The value for the 2x1 position in the matrix.</param>
    /// <param name="value22">The value for the 2x2 position in the matrix.</param>
    /// <param name="value31">The value for the 3x1 position in the matrix.</param>
    /// <param name="value32">The value for the 3x2 position in the matrix.</param>
    /// <returns>
    /// The newly created <see cref="Matrix3x2{TScalar}"/> instance.
    /// </returns>
    public Matrix3x2(
        TScalar value11, TScalar value12,
        TScalar value21, TScalar value22,
        TScalar value31, TScalar value32)
    {
        M11 = value11;
        M12 = value12;
        M21 = value21;
        M22 = value22;
        M31 = value31;
        M32 = value32;
    }

    /// <summary>
    /// Create a new <see cref="Matrix3x2{TScalar}"/> instance where all the values
    /// in the matrix are identical.
    /// </summary>
    /// <param name="value">The value for all positions in the matrix.</param>
    /// <returns>
    /// The newly created <see cref="Matrix3x2{TScalar}"/> instance.
    /// </returns>
    public static Matrix3x2<TScalar> Create(TScalar value) => new()
    {
        M11 = value,
        M12 = value,
        M21 = value,
        M22 = value,
        M31 = value,
        M32 = value,
    };

    /// <summary>
    /// Create a new <see cref="Matrix3x2{TScalar}"/> instance with the given values.
    /// </summary>
    /// <param name="value11">The value for the 1x1 position in the matrix.</param>
    /// <param name="value12">The value for the 1x2 position in the matrix.</param>
    /// <param name="value21">The value for the 2x1 position in the matrix.</param>
    /// <param name="value22">The value for the 2x2 position in the matrix.</param>
    /// <param name="value31">The value for the 3x1 position in the matrix.</param>
    /// <param name="value32">The value for the 3x2 position in the matrix.</param>
    /// <returns>
    /// The newly created <see cref="Matrix3x2{TScalar}"/> instance.
    /// </returns>
    public static Matrix3x2<TScalar> Create(
        TScalar value11, TScalar value12,
        TScalar value21, TScalar value22,
        TScalar value31, TScalar value32) => new()
        {
            M11 = value11,
            M12 = value12,
            M21 = value21,
            M22 = value22,
            M31 = value31,
            M32 = value32,
        };

    /// <summary>
    /// Create a new <see cref="Matrix3x2{TScalar}"/> instance with the given values.
    /// </summary>
    /// <param name="values">The values for all positions in the matrix.</param>
    /// <returns>
    /// The newly created <see cref="Matrix3x2{TScalar}"/> instance.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="values"/> does not contain enough elements.
    /// </exception>
    public static Matrix3x2<TScalar> Create(TScalar[] values)
    {
        if (values.Length < 6)
        {
            throw new ArgumentException($"{nameof(values)} does not contain enough elements", nameof(values));
        }
        return new()
        {
            M11 = values[0],
            M12 = values[1],
            M21 = values[2],
            M22 = values[3],
            M31 = values[4],
            M32 = values[5],
        };
    }

    /// <summary>
    /// Create a new <see cref="Matrix3x2{TScalar}"/> instance with values from the given
    /// array, starting at the given index.
    /// </summary>
    /// <param name="values">The values for all positions in the matrix.</param>
    /// <param name="startIndex">The index of the first value in the array to use.</param>
    /// <returns>
    /// The newly created <see cref="Matrix3x2{TScalar}"/> instance.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="values"/> does not contain enough elements.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// the value of <paramref name="startIndex"/> does not leave enough elements in <paramref name="values"/>
    /// </exception>
    public static Matrix3x2<TScalar> Create(TScalar[] values, int startIndex)
    {
        if (values.Length < 6)
        {
            throw new ArgumentException($"{nameof(values)} does not contain enough elements");
        }
        if (startIndex + values.Length < 6)
        {
            throw new ArgumentOutOfRangeException(
                nameof(startIndex),
                $"the value of {nameof(startIndex)} does not leave enough elements in {nameof(values)}");
        }
        return new()
        {
            M11 = values[startIndex],
            M12 = values[startIndex + 1],
            M21 = values[startIndex + 2],
            M22 = values[startIndex + 3],
            M31 = values[startIndex + 4],
            M32 = values[startIndex + 5],
        };
    }

    /// <summary>
    /// Create a new <see cref="Matrix3x2{TScalar}"/> instance with the given values.
    /// </summary>
    /// <param name="values">The values for all positions in the matrix.</param>
    /// <returns>
    /// The newly created <see cref="Matrix3x2{TScalar}"/> instance.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="values"/> does not contain enough elements.
    /// </exception>
    public static Matrix3x2<TScalar> Create(ReadOnlySpan<TScalar> values)
    {
        if (values.Length < 6)
        {
            throw new ArgumentException($"{nameof(values)} does not contain enough elements", nameof(values));
        }
        return new()
        {
            M11 = values[0],
            M12 = values[1],
            M21 = values[2],
            M22 = values[3],
            M31 = values[4],
            M32 = values[5],
        };
    }

    /// <summary>
    /// Creates a translation matrix from the given vector.
    /// </summary>
    /// <param name="position">The translation position.</param>
    /// <returns>A translation matrix.</returns>
    public static Matrix3x2<TScalar> CreateTranslation(Vector2<TScalar> position) => new()
    {
        M11 = TScalar.One,
        M12 = TScalar.Zero,
        M21 = TScalar.Zero,
        M22 = TScalar.One,
        M31 = position.X,
        M32 = position.Y,
    };

    /// <summary>
    /// Creates a translation matrix from the given X and Y components.
    /// </summary>
    /// <param name="xPosition">The X position.</param>
    /// <param name="yPosition">The Y position.</param>
    /// <returns>A translation matrix.</returns>
    public static Matrix3x2<TScalar> CreateTranslation(TScalar xPosition, TScalar yPosition) => new()
    {
        M11 = TScalar.One,
        M12 = TScalar.Zero,
        M21 = TScalar.Zero,
        M22 = TScalar.One,
        M31 = xPosition,
        M32 = yPosition,
    };

    /// <summary>
    /// Creates a scale matrix from the given X and Y components.
    /// </summary>
    /// <param name="xScale">Value to scale by on the X-axis.</param>
    /// <param name="yScale">Value to scale by on the Y-axis.</param>
    /// <returns>A scaling matrix.</returns>
    public static Matrix3x2<TScalar> CreateScale(TScalar xScale, TScalar yScale) => new()
    {
        M11 = xScale,
        M12 = TScalar.Zero,
        M21 = TScalar.Zero,
        M22 = yScale,
        M31 = TScalar.Zero,
        M32 = TScalar.Zero,
    };

    /// <summary>
    /// Creates a scale matrix that is offset by a given center point.
    /// </summary>
    /// <param name="xScale">Value to scale by on the X-axis.</param>
    /// <param name="yScale">Value to scale by on the Y-axis.</param>
    /// <param name="centerPoint">The center point.</param>
    /// <returns>A scaling matrix.</returns>
    public static Matrix3x2<TScalar> CreateScale(TScalar xScale, TScalar yScale, Vector2<TScalar> centerPoint) => new()
    {
        M11 = xScale,
        M12 = TScalar.Zero,
        M21 = TScalar.Zero,
        M22 = yScale,
        M31 = centerPoint.X * (TScalar.One - xScale),
        M32 = centerPoint.Y * (TScalar.One - yScale),
    };

    /// <summary>
    /// Creates a scale matrix from the given vector scale.
    /// </summary>
    /// <param name="scales">The scale to use.</param>
    /// <returns>A scaling matrix.</returns>
    public static Matrix3x2<TScalar> CreateScale(Vector2<TScalar> scales) => new()
    {
        M11 = scales.X,
        M12 = TScalar.Zero,
        M21 = TScalar.Zero,
        M22 = scales.Y,
        M31 = TScalar.Zero,
        M32 = TScalar.Zero,
    };

    /// <summary>
    /// Creates a scale matrix from the given vector scale with an offset from the given center point.
    /// </summary>
    /// <param name="scales">The scale to use.</param>
    /// <param name="centerPoint">The center offset.</param>
    /// <returns>A scaling matrix.</returns>
    public static Matrix3x2<TScalar> CreateScale(Vector2<TScalar> scales, Vector2<TScalar> centerPoint) => new()
    {
        M11 = scales.X,
        M12 = TScalar.Zero,
        M21 = TScalar.Zero,
        M22 = scales.Y,
        M31 = centerPoint.X * (TScalar.One - scales.X),
        M32 = centerPoint.Y * (TScalar.One - scales.Y),
    };

    /// <summary>
    /// Creates a scale matrix that scales uniformly with the given scale.
    /// </summary>
    /// <param name="scale">The uniform scale to use.</param>
    /// <returns>A scaling matrix.</returns>
    public static Matrix3x2<TScalar> CreateScale(TScalar scale) => new()
    {
        M11 = scale,
        M12 = TScalar.Zero,
        M21 = TScalar.Zero,
        M22 = scale,
        M31 = TScalar.Zero,
        M32 = TScalar.Zero,
    };

    /// <summary>
    /// Creates a scale matrix that scales uniformly with the given scale with an offset from the given center.
    /// </summary>
    /// <param name="scale">The uniform scale to use.</param>
    /// <param name="centerPoint">The center offset.</param>
    /// <returns>A scaling matrix.</returns>
    public static Matrix3x2<TScalar> CreateScale(TScalar scale, Vector2<TScalar> centerPoint) => new()
    {
        M11 = scale,
        M12 = TScalar.Zero,
        M21 = TScalar.Zero,
        M22 = scale,
        M31 = centerPoint.X * (TScalar.One - scale),
        M32 = centerPoint.Y * (TScalar.One - scale),
    };

    /// <summary>
    /// Creates a skew matrix from the given angles in radians.
    /// </summary>
    /// <param name="radiansX">The X angle, in radians.</param>
    /// <param name="radiansY">The Y angle, in radians.</param>
    /// <returns>A skew matrix.</returns>
    public static Matrix3x2<TScalar> CreateSkew(TScalar radiansX, TScalar radiansY) => new()
    {
        M11 = TScalar.One,
        M12 = TScalar.Tan(radiansY),
        M21 = TScalar.Tan(radiansX),
        M22 = TScalar.One,
        M31 = TScalar.Zero,
        M32 = TScalar.Zero,
    };

    /// <summary>
    /// Creates a skew matrix from the given angles in radians and a center point.
    /// </summary>
    /// <param name="radiansX">The X angle, in radians.</param>
    /// <param name="radiansY">The Y angle, in radians.</param>
    /// <param name="centerPoint">The center point.</param>
    /// <returns>A skew matrix.</returns>
    public static Matrix3x2<TScalar> CreateSkew(TScalar radiansX, TScalar radiansY, Vector2<TScalar> centerPoint)
    {
        var xTan = TScalar.Tan(radiansX);
        var yTan = TScalar.Tan(radiansY);

        return new()
        {
            M11 = TScalar.One,
            M12 = yTan,
            M21 = xTan,
            M22 = TScalar.One,
            M31 = -centerPoint.Y * xTan,
            M32 = -centerPoint.X * yTan,
        };
    }

    /// <summary>
    /// Creates a rotation matrix using the given rotation in radians.
    /// </summary>
    /// <param name="radians">The amount of rotation, in radians.</param>
    /// <returns>A rotation matrix.</returns>
    public static Matrix3x2<TScalar> CreateRotation(TScalar radians)
    {
        radians = TScalar.IEEERemainder(radians, TScalar.Tau);

        TScalar c, s;

        var epsilon = NumberValues.PiOver180<TScalar>() / NumberValues.Thousand<TScalar>(); // 0.1% of a degree

        var halfPi = NumberValues.HalfPi<TScalar>();
        if (radians > (-epsilon) && radians < epsilon)
        {
            // Exact case for zero rotation.
            c = TScalar.One;
            s = TScalar.Zero;
        }
        else if (radians > (halfPi - epsilon)
            && radians < (halfPi + epsilon))
        {
            // Exact case for 90 degree rotation.
            c = TScalar.Zero;
            s = TScalar.One;
        }
        else if (radians < (-TScalar.Pi + epsilon)
            || radians > (TScalar.Pi - epsilon))
        {
            // Exact case for 180 degree rotation.
            c = TScalar.NegativeOne;
            s = TScalar.Zero;
        }
        else if (radians > (-halfPi - epsilon)
            && radians < (-halfPi + epsilon))
        {
            // Exact case for 270 degree rotation.
            c = TScalar.Zero;
            s = TScalar.NegativeOne;
        }
        else
        {
            // Arbitrary rotation.
            c = TScalar.Cos(radians);
            s = TScalar.Sin(radians);
        }

        return new()
        {
            M11 = c,
            M12 = s,
            M21 = -s,
            M22 = c,
            M31 = TScalar.Zero,
            M32 = TScalar.Zero,
        };
    }

    /// <summary>
    /// Creates a rotation matrix using the given rotation in radians and a center point.
    /// </summary>
    /// <param name="radians">The amount of rotation, in radians.</param>
    /// <param name="centerPoint">The center point.</param>
    /// <returns>A rotation matrix.</returns>
    public static Matrix3x2<TScalar> CreateRotation(TScalar radians, Vector2<TScalar> centerPoint)
    {
        radians = TScalar.IEEERemainder(radians, TScalar.Tau);

        TScalar c, s;

        var epsilon = NumberValues.PiOver180<TScalar>() / NumberValues.Thousand<TScalar>(); // 0.1% of a degree

        if (radians > (-epsilon) && radians < epsilon)
        {
            // Exact case for zero rotation.
            c = TScalar.One;
            s = TScalar.Zero;
        }
        else if (radians > (NumberValues.HalfPi<TScalar>() - epsilon)
            && radians < (NumberValues.HalfPi<TScalar>() + epsilon))
        {
            // Exact case for 90 degree rotation.
            c = TScalar.Zero;
            s = TScalar.One;
        }
        else if (radians < (-TScalar.Pi + epsilon)
            || radians > (TScalar.Pi - epsilon))
        {
            // Exact case for 180 degree rotation.
            c = TScalar.NegativeOne;
            s = TScalar.Zero;
        }
        else if (radians > (-NumberValues.HalfPi<TScalar>() - epsilon)
            && radians < (-NumberValues.HalfPi<TScalar>() + epsilon))
        {
            // Exact case for 270 degree rotation.
            c = TScalar.Zero;
            s = TScalar.NegativeOne;
        }
        else
        {
            // Arbitrary rotation.
            c = TScalar.Cos(radians);
            s = TScalar.Sin(radians);
        }

        var x = (centerPoint.X * (TScalar.One - c)) + (centerPoint.Y * s);
        var y = (centerPoint.Y * (TScalar.One - c)) - (centerPoint.X * s);

        return new()
        {
            M11 = c,
            M12 = s,
            M21 = -s,
            M22 = c,
            M31 = x,
            M32 = y,
        };
    }

    /// <summary>
    /// Attempts to invert the given matrix. If the operation succeeds, the inverted matrix is stored in the result parameter.
    /// </summary>
    /// <param name="matrix">The source matrix.</param>
    /// <param name="result">The output matrix.</param>
    /// <returns>True if the operation succeeded, False otherwise.</returns>
    public static bool Invert(Matrix3x2<TScalar> matrix, out Matrix3x2<TScalar> result)
    {
        var det = matrix.GetDeterminant();

        if (TScalar.Abs(det).IsNearlyZero())
        {
            result = MultiplicativeIdentity;
            return false;
        }

        var invDet = TScalar.One / det;

        result = new()
        {
            M11 = matrix.M22 * invDet,
            M12 = -matrix.M12 * invDet,
            M21 = -matrix.M21 * invDet,
            M22 = matrix.M11 * invDet,
            M31 = ((matrix.M21 * matrix.M32) - (matrix.M31 * matrix.M22)) * invDet,
            M32 = ((matrix.M31 * matrix.M12) - (matrix.M11 * matrix.M32)) * invDet,
        };

        return true;
    }

    /// <summary>
    /// Linearly interpolates from matrix1 to matrix2, based on the third parameter.
    /// </summary>
    /// <param name="matrix1">The first source matrix.</param>
    /// <param name="matrix2">The second source matrix.</param>
    /// <param name="amount">The relative weighting of matrix2.</param>
    /// <returns>The interpolated matrix.</returns>
    public static Matrix3x2<TScalar> Lerp(Matrix3x2<TScalar> matrix1, Matrix3x2<TScalar> matrix2, TScalar amount) => new()
    {
        M11 = matrix1.M11 + ((matrix2.M11 - matrix1.M11) * amount),
        M12 = matrix1.M12 + ((matrix2.M12 - matrix1.M12) * amount),
        M21 = matrix1.M21 + ((matrix2.M21 - matrix1.M21) * amount),
        M22 = matrix1.M22 + ((matrix2.M22 - matrix1.M22) * amount),
        M31 = matrix1.M31 + ((matrix2.M31 - matrix1.M31) * amount),
        M32 = matrix1.M32 + ((matrix2.M32 - matrix1.M32) * amount),
    };

    /// <summary>
    /// Negates the given matrix by multiplying all values by -1.
    /// </summary>
    /// <param name="value">The source matrix.</param>
    /// <returns>The negated matrix.</returns>
    public static Matrix3x2<TScalar> operator -(Matrix3x2<TScalar> value) => new()
    {
        M11 = -value.M11,
        M12 = -value.M12,
        M21 = -value.M21,
        M22 = -value.M22,
        M31 = -value.M31,
        M32 = -value.M32,
    };

    /// <summary>
    /// Adds each matrix element in value1 with its corresponding element in value2.
    /// </summary>
    /// <param name="value1">The first source matrix.</param>
    /// <param name="value2">The second source matrix.</param>
    /// <returns>The matrix containing the summed values.</returns>
    public static Matrix3x2<TScalar> operator +(Matrix3x2<TScalar> value1, Matrix3x2<TScalar> value2) => new()
    {
        M11 = value1.M11 + value2.M11,
        M12 = value1.M12 + value2.M12,
        M21 = value1.M21 + value2.M21,
        M22 = value1.M22 + value2.M22,
        M31 = value1.M31 + value2.M31,
        M32 = value1.M32 + value2.M32,
    };

    /// <summary>
    /// Subtracts each matrix element in value2 from its corresponding element in value1.
    /// </summary>
    /// <param name="value1">The first source matrix.</param>
    /// <param name="value2">The second source matrix.</param>
    /// <returns>The matrix containing the resulting values.</returns>
    public static Matrix3x2<TScalar> operator -(Matrix3x2<TScalar> value1, Matrix3x2<TScalar> value2) => new()
    {
        M11 = value1.M11 - value2.M11,
        M12 = value1.M12 - value2.M12,
        M21 = value1.M21 - value2.M21,
        M22 = value1.M22 - value2.M22,
        M31 = value1.M31 - value2.M31,
        M32 = value1.M32 - value2.M32,
    };

    /// <summary>
    /// Multiplies two matrices together and returns the resulting matrix.
    /// </summary>
    /// <param name="value1">The first source matrix.</param>
    /// <param name="value2">The second source matrix.</param>
    /// <returns>The product matrix.</returns>
    public static Matrix3x2<TScalar> operator *(Matrix3x2<TScalar> value1, Matrix3x2<TScalar> value2) => new()
    {
        M11 = (value1.M11 * value2.M11) + (value1.M12 * value2.M21),
        M12 = (value1.M11 * value2.M12) + (value1.M12 * value2.M22),
        M21 = (value1.M21 * value2.M11) + (value1.M22 * value2.M21),
        M22 = (value1.M21 * value2.M12) + (value1.M22 * value2.M22),
        M31 = (value1.M31 * value2.M11) + (value1.M32 * value2.M21) + value2.M31,
        M32 = (value1.M31 * value2.M12) + (value1.M32 * value2.M22) + value2.M32,
    };

    /// <summary>
    /// Scales all elements in a matrix by the given scalar factor.
    /// </summary>
    /// <param name="value1">The source matrix.</param>
    /// <param name="value2">The scaling value to use.</param>
    /// <returns>The resulting matrix.</returns>
    public static Matrix3x2<TScalar> operator *(Matrix3x2<TScalar> value1, TScalar value2) => new()
    {
        M11 = value1.M11 * value2,
        M12 = value1.M12 * value2,
        M21 = value1.M21 * value2,
        M22 = value1.M22 * value2,
        M31 = value1.M31 * value2,
        M32 = value1.M32 * value2,
    };

    /// <summary>
    /// Returns a boolean indicating whether the given matrices are equal.
    /// </summary>
    /// <param name="value1">The first source matrix.</param>
    /// <param name="value2">The second source matrix.</param>
    /// <returns>True if the matrices are equal; False otherwise.</returns>
    public static bool operator ==(Matrix3x2<TScalar> value1, Matrix3x2<TScalar> value2)
        => value1.Equals(value2);

    /// <summary>
    /// Returns a boolean indicating whether the given matrices are not equal.
    /// </summary>
    /// <param name="value1">The first source matrix.</param>
    /// <param name="value2">The second source matrix.</param>
    /// <returns>True if the matrices are not equal; False if they are equal.</returns>
    public static bool operator !=(Matrix3x2<TScalar> value1, Matrix3x2<TScalar> value2)
        => !value1.Equals(value2);

    /// <summary>
    /// Converts between implementations of matrices.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator Matrix3x2<TScalar>(Matrix3x2 value) => new()
    {
        M11 = TScalar.Create(value.M11),
        M12 = TScalar.Create(value.M12),
        M21 = TScalar.Create(value.M21),
        M22 = TScalar.Create(value.M22),
        M31 = TScalar.Create(value.M31),
        M32 = TScalar.Create(value.M32),
    };

    /// <summary>
    /// Converts between implementations of matrices.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static explicit operator Matrix3x2(Matrix3x2<TScalar> value) => new(
        value.M11.Create<TScalar, float>(),
        value.M12.Create<TScalar, float>(),
        value.M21.Create<TScalar, float>(),
        value.M22.Create<TScalar, float>(),
        value.M31.Create<TScalar, float>(),
        value.M32.Create<TScalar, float>());

    /// <summary>
    /// Returns a boolean indicating whether the matrix is equal to the other given matrix.
    /// </summary>
    /// <param name="other">The other matrix to test equality against.</param>
    /// <returns>
    /// <see langword="true"/> if this matrix is equal to other; otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Checks diagonal element first for early out.
    /// </remarks>
    public bool Equals(Matrix3x2<TScalar> other) => M11 == other.M11
        && M22 == other.M22
        && M12 == other.M12
        && M21 == other.M21
        && M31 == other.M31
        && M32 == other.M32;

    /// <summary>
    /// Returns a boolean indicating whether the given Object is equal to this matrix instance.
    /// </summary>
    /// <param name="obj">The Object to compare against.</param>
    /// <returns>True if the Object is equal to this matrix; False otherwise.</returns>
    public override bool Equals(object? obj) => obj is Matrix3x2<TScalar> other && Equals(other);

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
    public TScalar GetDeterminant() => (M11 * M22) - (M21 * M12);

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode() => HashCode.Combine(M11, M12, M21, M22, M31, M32);

    /// <summary>
    /// Returns a <see cref="string"/> representing this instance, using the specified
    /// <paramref name="format"/> to format individual elements and the given <see cref="IFormatProvider"/>.
    /// </summary>
    /// <param name="format">The format of individual elements.</param>
    /// <param name="formatProvider">
    /// The format provider to use when formatting elements.
    /// </param>
    /// <returns>The <see cref="string"/> representation.</returns>
    public string ToString(string? format, IFormatProvider? formatProvider) => new StringBuilder("{ {M11:")
        .Append(M11.ToString(format, formatProvider))
        .Append(" M12:")
        .Append(M12.ToString(format, formatProvider))
        .Append("} {M21:")
        .Append(M21.ToString(format, formatProvider))
        .Append(" M22:")
        .Append(M22.ToString(format, formatProvider))
        .Append("} {M31:")
        .Append(M31.ToString(format, formatProvider))
        .Append(" M32:")
        .Append(M32.ToString(format, formatProvider))
        .Append("} }")
        .ToString();

    /// <summary>
    /// Returns a <see cref="string"/> representing this instance, using the specified
    /// <paramref name="format"/> to format individual elements.
    /// </summary>
    /// <param name="format">The format of individual elements.</param>
    /// <returns>The <see cref="string"/> representation.</returns>
    public string ToString(string format) => ToString(format, null);

    /// <summary>
    /// Returns a <see cref="string"/> representing this instance.
    /// </summary>
    /// <returns>The <see cref="string"/> representation.</returns>
    public override string ToString() => ToString(null, null);

    /// <summary>
    /// Attempts to write this instance to the given <see cref="Span{T}"/>.
    /// </summary>
    /// <param name="destination">The <see cref="Span{T}"/> to write to.</param>
    /// <param name="charsWritten">
    /// When this method returns, this will contains the number of characters written to <paramref name="destination"/>.
    /// </param>
    /// <param name="format">A format string containing formatting specifications.</param>
    /// <param name="provider">
    /// An object that supplies format information about the current instance.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if this instance was successfully written to the
    /// <paramref name="destination"/>; otherwise <see langword="false"/>.
    /// </returns>
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        charsWritten = 0;
        if (destination.Length < 39
            || !"{ {M11:".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 7;
        destination = destination[7..];

        if (!M11.TryFormat(destination, out var c, format, provider))
        {
            return false;
        }
        charsWritten += c;
        destination = destination[c..];

        if (destination.Length < 32
            || !" M12:".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 5;
        destination = destination[5..];

        if (!M12.TryFormat(destination, out c, format, provider))
        {
            return false;
        }
        charsWritten += c;
        destination = destination[c..];

        if (destination.Length < 27
            || !"} {M21:".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 7;
        destination = destination[7..];

        if (!M21.TryFormat(destination, out c, format, provider))
        {
            return false;
        }
        charsWritten += c;
        destination = destination[c..];

        if (destination.Length < 20
            || !" M22:".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 5;
        destination = destination[5..];

        if (!M22.TryFormat(destination, out c, format, provider))
        {
            return false;
        }
        charsWritten += c;
        destination = destination[c..];

        if (destination.Length < 15
            || !"} {M31:".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 7;
        destination = destination[7..];

        if (!M31.TryFormat(destination, out c, format, provider))
        {
            return false;
        }
        charsWritten += c;
        destination = destination[c..];

        if (destination.Length < 8
            || !" M32:".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 5;
        destination = destination[5..];

        if (!M32.TryFormat(destination, out c, format, provider))
        {
            return false;
        }
        charsWritten += c;
        destination = destination[c..];

        if (destination.Length < 3
            || !"} }".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 3;
        return true;
    }
}
