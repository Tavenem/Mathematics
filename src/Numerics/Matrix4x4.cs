using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace Tavenem.Mathematics;

/// <summary>
/// A structure encapsulating a 4x4 matrix.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public struct Matrix4x4<TScalar> :
    IAdditionOperators<Matrix4x4<TScalar>, Matrix4x4<TScalar>, Matrix4x4<TScalar>>,
    IEqualityOperators<Matrix4x4<TScalar>, Matrix4x4<TScalar>>,
    IMultiplicativeIdentity<Matrix4x4<TScalar>, Matrix4x4<TScalar>>,
    IMultiplyOperators<Matrix4x4<TScalar>, Matrix4x4<TScalar>, Matrix4x4<TScalar>>,
    IMultiplyOperators<Matrix4x4<TScalar>, TScalar, Matrix4x4<TScalar>>,
    ISpanFormattable,
    ISubtractionOperators<Matrix4x4<TScalar>, Matrix4x4<TScalar>, Matrix4x4<TScalar>>,
    IUnaryNegationOperators<Matrix4x4<TScalar>, Matrix4x4<TScalar>>
    where TScalar : IFloatingPoint<TScalar>
{
    /// <summary>
    /// Returns the multiplicative identity matrix.
    /// </summary>
    public static Matrix4x4<TScalar> MultiplicativeIdentity { get; } = new()
    {
        M11 = TScalar.One,
        M12 = TScalar.Zero,
        M13 = TScalar.Zero,
        M14 = TScalar.Zero,
        M21 = TScalar.Zero,
        M22 = TScalar.One,
        M23 = TScalar.Zero,
        M24 = TScalar.Zero,
        M31 = TScalar.Zero,
        M32 = TScalar.Zero,
        M33 = TScalar.One,
        M34 = TScalar.Zero,
        M41 = TScalar.Zero,
        M42 = TScalar.Zero,
        M43 = TScalar.Zero,
        M44 = TScalar.One,
    };

    /// <summary>
    /// Returns a matrix with zero for all elements.
    /// </summary>
    public static Matrix4x4<TScalar> Zero { get; } = new();
    /// <summary>
    /// Returns the additive identity matrix.
    /// </summary>
    public static Matrix4x4<TScalar> AdditiveIdentity { get; } = Matrix4x4<TScalar>.Zero;

    /// <summary>
    /// Value at row 1, column 1 of the matrix.
    /// </summary>
    public TScalar M11 { get; init; }

    /// <summary>
    /// Value at row 1, column 2 of the matrix.
    /// </summary>
    public TScalar M12 { get; init; }

    /// <summary>
    /// Value at row 1, column 3 of the matrix.
    /// </summary>
    public TScalar M13 { get; init; }

    /// <summary>
    /// Value at row 1, column 4 of the matrix.
    /// </summary>
    public TScalar M14 { get; init; }

    /// <summary>
    /// Value at row 2, column 1 of the matrix.
    /// </summary>
    public TScalar M21 { get; init; }

    /// <summary>
    /// Value at row 2, column 2 of the matrix.
    /// </summary>
    public TScalar M22 { get; init; }

    /// <summary>
    /// Value at row 2, column 3 of the matrix.
    /// </summary>
    public TScalar M23 { get; init; }

    /// <summary>
    /// Value at row 2, column 4 of the matrix.
    /// </summary>
    public TScalar M24 { get; init; }

    /// <summary>
    /// Value at row 3, column 1 of the matrix.
    /// </summary>
    public TScalar M31 { get; init; }

    /// <summary>
    /// Value at row 3, column 2 of the matrix.
    /// </summary>
    public TScalar M32 { get; init; }

    /// <summary>
    /// Value at row 3, column 3 of the matrix.
    /// </summary>
    public TScalar M33 { get; init; }

    /// <summary>
    /// Value at row 3, column 4 of the matrix.
    /// </summary>
    public TScalar M34 { get; init; }

    /// <summary>
    /// Value at row 4, column 1 of the matrix.
    /// </summary>
    public TScalar M41 { get; init; }

    /// <summary>
    /// Value at row 4, column 2 of the matrix.
    /// </summary>
    public TScalar M42 { get; init; }

    /// <summary>
    /// Value at row 4, column 3 of the matrix.
    /// </summary>
    public TScalar M43 { get; init; }

    /// <summary>
    /// Value at row 4, column 4 of the matrix.
    /// </summary>
    public TScalar M44 { get; init; }

    /// <summary>
    /// Gets the translation component of this matrix.
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    public Vector3<TScalar> Translation => new() { X = M41, Y = M42, Z = M43 };

    /// <summary>
    /// Create a new <see cref="Matrix4x4{TScalar}"/> instance with the given values.
    /// </summary>
    /// <param name="value11">The value for the 1x1 position in the matrix.</param>
    /// <param name="value12">The value for the 1x2 position in the matrix.</param>
    /// <param name="value13">The value for the 1x3 position in the matrix.</param>
    /// <param name="value14">The value for the 1x4 position in the matrix.</param>
    /// <param name="value21">The value for the 2x1 position in the matrix.</param>
    /// <param name="value22">The value for the 2x2 position in the matrix.</param>
    /// <param name="value23">The value for the 2x3 position in the matrix.</param>
    /// <param name="value24">The value for the 2x4 position in the matrix.</param>
    /// <param name="value31">The value for the 3x1 position in the matrix.</param>
    /// <param name="value32">The value for the 3x2 position in the matrix.</param>
    /// <param name="value33">The value for the 3x3 position in the matrix.</param>
    /// <param name="value34">The value for the 3x4 position in the matrix.</param>
    /// <param name="value41">The value for the 4x1 position in the matrix.</param>
    /// <param name="value42">The value for the 4x2 position in the matrix.</param>
    /// <param name="value43">The value for the 4x3 position in the matrix.</param>
    /// <param name="value44">The value for the 4x4 position in the matrix.</param>
    /// <returns>
    /// The newly created <see cref="Matrix4x4{TScalar}"/> instance.
    /// </returns>
    public Matrix4x4(
        TScalar value11, TScalar value12, TScalar value13, TScalar value14,
        TScalar value21, TScalar value22, TScalar value23, TScalar value24,
        TScalar value31, TScalar value32, TScalar value33, TScalar value34,
        TScalar value41, TScalar value42, TScalar value43, TScalar value44)
    {
        M11 = value11;
        M12 = value12;
        M13 = value13;
        M14 = value14;
        M21 = value21;
        M22 = value22;
        M23 = value23;
        M24 = value24;
        M31 = value31;
        M32 = value32;
        M33 = value33;
        M34 = value34;
        M41 = value41;
        M42 = value42;
        M43 = value43;
        M44 = value44;
    }

    /// <summary>
    /// Create a new <see cref="Matrix4x4{TScalar}"/> instance where all the values
    /// in the matrix are identical.
    /// </summary>
    /// <param name="value">The value for all positions in the matrix.</param>
    /// <returns>
    /// The newly created <see cref="Matrix4x4{TScalar}"/> instance.
    /// </returns>
    public static Matrix4x4<TScalar> Create(TScalar value) => new()
    {
        M11 = value,
        M12 = value,
        M13 = value,
        M14 = value,
        M21 = value,
        M22 = value,
        M23 = value,
        M24 = value,
        M31 = value,
        M32 = value,
        M33 = value,
        M34 = value,
        M41 = value,
        M42 = value,
        M43 = value,
        M44 = value,
    };

    /// <summary>
    /// Create a new <see cref="Matrix4x4{TScalar}"/> instance with the given values.
    /// </summary>
    /// <param name="value11">The value for the 1x1 position in the matrix.</param>
    /// <param name="value12">The value for the 1x2 position in the matrix.</param>
    /// <param name="value13">The value for the 1x3 position in the matrix.</param>
    /// <param name="value14">The value for the 1x4 position in the matrix.</param>
    /// <param name="value21">The value for the 2x1 position in the matrix.</param>
    /// <param name="value22">The value for the 2x2 position in the matrix.</param>
    /// <param name="value23">The value for the 2x3 position in the matrix.</param>
    /// <param name="value24">The value for the 2x4 position in the matrix.</param>
    /// <param name="value31">The value for the 3x1 position in the matrix.</param>
    /// <param name="value32">The value for the 3x2 position in the matrix.</param>
    /// <param name="value33">The value for the 3x3 position in the matrix.</param>
    /// <param name="value34">The value for the 3x4 position in the matrix.</param>
    /// <param name="value41">The value for the 4x1 position in the matrix.</param>
    /// <param name="value42">The value for the 4x2 position in the matrix.</param>
    /// <param name="value43">The value for the 4x3 position in the matrix.</param>
    /// <param name="value44">The value for the 4x4 position in the matrix.</param>
    /// <returns>
    /// The newly created <see cref="Matrix4x4{TScalar}"/> instance.
    /// </returns>
    public static Matrix4x4<TScalar> Create(
        TScalar value11, TScalar value12, TScalar value13, TScalar value14,
        TScalar value21, TScalar value22, TScalar value23, TScalar value24,
        TScalar value31, TScalar value32, TScalar value33, TScalar value34,
        TScalar value41, TScalar value42, TScalar value43, TScalar value44) => new()
        {
            M11 = value11,
            M12 = value12,
            M13 = value13,
            M14 = value14,
            M21 = value21,
            M22 = value22,
            M23 = value23,
            M24 = value24,
            M31 = value31,
            M32 = value32,
            M33 = value33,
            M34 = value34,
            M41 = value41,
            M42 = value42,
            M43 = value43,
            M44 = value44,
        };

    /// <summary>
    /// Create a new <see cref="Matrix4x4{TScalar}"/> instance with the given values.
    /// </summary>
    /// <param name="values">The values for all positions in the matrix.</param>
    /// <returns>
    /// The newly created <see cref="Matrix4x4{TScalar}"/> instance.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="values"/> does not contain enough elements.
    /// </exception>
    public static Matrix4x4<TScalar> Create(TScalar[] values)
    {
        if (values.Length < 16)
        {
            throw new ArgumentException($"{nameof(values)} does not contain enough elements", nameof(values));
        }
        return new()
        {
            M11 = values[0],
            M12 = values[1],
            M13 = values[2],
            M14 = values[3],
            M21 = values[4],
            M22 = values[5],
            M23 = values[6],
            M24 = values[7],
            M31 = values[8],
            M32 = values[9],
            M33 = values[10],
            M34 = values[11],
            M41 = values[12],
            M42 = values[13],
            M43 = values[14],
            M44 = values[15],
        };
    }

    /// <summary>
    /// Create a new <see cref="Matrix4x4{TScalar}"/> instance with values from the given
    /// array, starting at the given index.
    /// </summary>
    /// <param name="values">The values for all positions in the matrix.</param>
    /// <param name="startIndex">The index of the first value in the array to use.</param>
    /// <returns>
    /// The newly created <see cref="Matrix4x4{TScalar}"/> instance.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="values"/> does not contain enough elements.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// the value of <paramref name="startIndex"/> does not leave enough elements in <paramref name="values"/>
    /// </exception>
    public static Matrix4x4<TScalar> Create(TScalar[] values, int startIndex)
    {
        if (values.Length < 16)
        {
            throw new ArgumentException($"{nameof(values)} does not contain enough elements");
        }
        if (startIndex + values.Length < 16)
        {
            throw new ArgumentOutOfRangeException(
                nameof(startIndex),
                $"the value of {nameof(startIndex)} does not leave enough elements in {nameof(values)}");
        }
        return new()
        {
            M11 = values[startIndex],
            M12 = values[startIndex + 1],
            M13 = values[startIndex + 2],
            M14 = values[startIndex + 3],
            M21 = values[startIndex + 4],
            M22 = values[startIndex + 5],
            M23 = values[startIndex + 6],
            M24 = values[startIndex + 7],
            M31 = values[startIndex + 8],
            M32 = values[startIndex + 9],
            M33 = values[startIndex + 10],
            M34 = values[startIndex + 11],
            M41 = values[startIndex + 12],
            M42 = values[startIndex + 13],
            M43 = values[startIndex + 14],
            M44 = values[startIndex + 15],
        };
    }

    /// <summary>
    /// Create a new <see cref="Matrix4x4{TScalar}"/> instance with the given values.
    /// </summary>
    /// <param name="values">The values for all positions in the matrix.</param>
    /// <returns>
    /// The newly created <see cref="Matrix4x4{TScalar}"/> instance.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="values"/> does not contain enough elements.
    /// </exception>
    public static Matrix4x4<TScalar> Create(ReadOnlySpan<TScalar> values)
    {
        if (values.Length < 16)
        {
            throw new ArgumentException($"{nameof(values)} does not contain enough elements", nameof(values));
        }
        return new()
        {
            M11 = values[0],
            M12 = values[1],
            M13 = values[2],
            M14 = values[3],
            M21 = values[4],
            M22 = values[5],
            M23 = values[6],
            M24 = values[7],
            M31 = values[8],
            M32 = values[9],
            M33 = values[10],
            M34 = values[11],
            M41 = values[12],
            M42 = values[13],
            M43 = values[14],
            M44 = values[15],
        };
    }

    /// <summary>
    /// Constructs a <see cref="Matrix4x4{TScalar}"/> from the given <see cref="Matrix3x2{TScalar}"/>.
    /// </summary>
    /// <param name="value">The source <see cref="Matrix3x2{TScalar}"/>.</param>
    public static Matrix4x4<TScalar> Create(Matrix3x2<TScalar> value) => new()
    {
        M11 = value.M11,
        M12 = value.M12,
        M13 = TScalar.Zero,
        M14 = TScalar.Zero,
        M21 = value.M21,
        M22 = value.M22,
        M23 = TScalar.Zero,
        M24 = TScalar.Zero,
        M31 = TScalar.Zero,
        M32 = TScalar.Zero,
        M33 = TScalar.One,
        M34 = TScalar.Zero,
        M41 = value.M31,
        M42 = value.M32,
        M43 = TScalar.Zero,
        M44 = TScalar.One,
    };

    /// <summary>
    /// Creates a spherical billboard that rotates around a specified object position.
    /// </summary>
    /// <param name="objectPosition">Position of the object the billboard will rotate around.</param>
    /// <param name="cameraPosition">Position of the camera.</param>
    /// <param name="cameraUpVector">The up vector of the camera.</param>
    /// <param name="cameraForwardVector">The forward vector of the camera.</param>
    /// <returns>The created billboard matrix</returns>
    public static Matrix4x4<TScalar> CreateBillboard(
        Vector3<TScalar> objectPosition,
        Vector3<TScalar> cameraPosition,
        Vector3<TScalar> cameraUpVector,
        Vector3<TScalar> cameraForwardVector)
    {
        var zaxis = Vector3<TScalar>.Create(
            objectPosition.X - cameraPosition.X,
            objectPosition.Y - cameraPosition.Y,
            objectPosition.Z - cameraPosition.Z);

        var norm = Vector3<TScalar>.LengthSquared(zaxis);

        if (norm < TScalar.One / NumberValues.TenThousand<TScalar>()) // 1e-4
        {
            zaxis = -cameraForwardVector;
        }
        else
        {
            zaxis *= TScalar.One / TScalar.Sqrt(norm);
        }

        var xaxis = Vector3<TScalar>.Normalize(Vector3<TScalar>.Cross(cameraUpVector, zaxis));

        var yaxis = Vector3<TScalar>.Cross(zaxis, xaxis);

        return new()
        {
            M11 = xaxis.X,
            M12 = xaxis.Y,
            M13 = xaxis.Z,
            M14 = TScalar.Zero,
            M21 = yaxis.X,
            M22 = yaxis.Y,
            M23 = yaxis.Z,
            M24 = TScalar.Zero,
            M31 = zaxis.X,
            M32 = zaxis.Y,
            M33 = zaxis.Z,
            M34 = TScalar.Zero,
            M41 = objectPosition.X,
            M42 = objectPosition.Y,
            M43 = objectPosition.Z,
            M44 = TScalar.One,
        };
    }

    /// <summary>
    /// Creates a cylindrical billboard that rotates around a specified axis.
    /// </summary>
    /// <param name="objectPosition">Position of the object the billboard will rotate around.</param>
    /// <param name="cameraPosition">Position of the camera.</param>
    /// <param name="rotateAxis">Axis to rotate the billboard around.</param>
    /// <param name="cameraForwardVector">Forward vector of the camera.</param>
    /// <param name="objectForwardVector">Forward vector of the object.</param>
    /// <returns>The created billboard matrix.</returns>
    public static Matrix4x4<TScalar> CreateConstrainedBillboard(
        Vector3<TScalar> objectPosition,
        Vector3<TScalar> cameraPosition,
        Vector3<TScalar> rotateAxis,
        Vector3<TScalar> cameraForwardVector,
        Vector3<TScalar> objectForwardVector)
    {
        // Treat the case when object and camera positions are too close.
        var faceDir = Vector3<TScalar>.Create(
            objectPosition.X - cameraPosition.X,
            objectPosition.Y - cameraPosition.Y,
            objectPosition.Z - cameraPosition.Z);

        var norm = Vector3<TScalar>.LengthSquared(faceDir);

        if (norm < TScalar.One / NumberValues.TenThousand<TScalar>()) // 1e-4
        {
            faceDir = -cameraForwardVector;
        }
        else
        {
            faceDir *= TScalar.One / TScalar.Sqrt(norm);
        }

        var yaxis = rotateAxis;
        Vector3<TScalar> xaxis;
        Vector3<TScalar> zaxis;

        // Treat the case when angle between faceDir and rotateAxis is too close to 0.
        var dot = Vector3<TScalar>.Dot(rotateAxis, faceDir);

        var minAngle = TScalar.One - (NumberValues.PiOver180<TScalar>() / NumberValues.Ten<TScalar>()); // 0.1 degrees
        if (TScalar.Abs(dot) > minAngle)
        {
            zaxis = objectForwardVector;

            // Make sure passed values are useful for compute.
            dot = Vector3<TScalar>.Dot(rotateAxis, zaxis);

            if (TScalar.Abs(dot) > minAngle)
            {
                zaxis = (TScalar.Abs(rotateAxis.Z) > minAngle)
                    ? Vector3<TScalar>.UnitX
                    : -Vector3<TScalar>.UnitZ;
            }

            xaxis = Vector3<TScalar>.Normalize(Vector3<TScalar>.Cross(rotateAxis, zaxis));
            zaxis = Vector3<TScalar>.Normalize(Vector3<TScalar>.Cross(xaxis, rotateAxis));
        }
        else
        {
            xaxis = Vector3<TScalar>.Normalize(Vector3<TScalar>.Cross(rotateAxis, faceDir));
            zaxis = Vector3<TScalar>.Normalize(Vector3<TScalar>.Cross(xaxis, yaxis));
        }

        return new()
        {
            M11 = xaxis.X,
            M12 = xaxis.Y,
            M13 = xaxis.Z,
            M14 = TScalar.Zero,
            M21 = yaxis.X,
            M22 = yaxis.Y,
            M23 = yaxis.Z,
            M24 = TScalar.Zero,
            M31 = zaxis.X,
            M32 = zaxis.Y,
            M33 = zaxis.Z,
            M34 = TScalar.Zero,
            M41 = objectPosition.X,
            M42 = objectPosition.Y,
            M43 = objectPosition.Z,
            M44 = TScalar.One,
        };
    }

    /// <summary>
    /// Creates a translation matrix.
    /// </summary>
    /// <param name="position">The amount to translate in each axis.</param>
    /// <returns>The translation matrix.</returns>
    public static Matrix4x4<TScalar> CreateTranslation(Vector3<TScalar> position) => new()
    {
        M11 = TScalar.One,
        M12 = TScalar.Zero,
        M13 = TScalar.Zero,
        M14 = TScalar.Zero,
        M21 = TScalar.Zero,
        M22 = TScalar.One,
        M23 = TScalar.Zero,
        M24 = TScalar.Zero,
        M31 = TScalar.Zero,
        M32 = TScalar.Zero,
        M33 = TScalar.One,
        M34 = TScalar.Zero,
        M41 = position.X,
        M42 = position.Y,
        M43 = position.Z,
        M44 = TScalar.One,
    };

    /// <summary>
    /// Creates a translation matrix.
    /// </summary>
    /// <param name="xPosition">The amount to translate on the X-axis.</param>
    /// <param name="yPosition">The amount to translate on the Y-axis.</param>
    /// <param name="zPosition">The amount to translate on the Z-axis.</param>
    /// <returns>The translation matrix.</returns>
    public static Matrix4x4<TScalar> CreateTranslation(TScalar xPosition, TScalar yPosition, TScalar zPosition) => new()
    {
        M11 = TScalar.One,
        M12 = TScalar.Zero,
        M13 = TScalar.Zero,
        M14 = TScalar.Zero,
        M21 = TScalar.Zero,
        M22 = TScalar.One,
        M23 = TScalar.Zero,
        M24 = TScalar.Zero,
        M31 = TScalar.Zero,
        M32 = TScalar.Zero,
        M33 = TScalar.One,
        M34 = TScalar.Zero,
        M41 = xPosition,
        M42 = yPosition,
        M43 = zPosition,
        M44 = TScalar.One,
    };

    /// <summary>
    /// Creates a scaling matrix.
    /// </summary>
    /// <param name="xScale">Value to scale by on the X-axis.</param>
    /// <param name="yScale">Value to scale by on the Y-axis.</param>
    /// <param name="zScale">Value to scale by on the Z-axis.</param>
    /// <returns>The scaling matrix.</returns>
    public static Matrix4x4<TScalar> CreateScale(TScalar xScale, TScalar yScale, TScalar zScale) => new()
    {
        M11 = xScale,
        M12 = TScalar.Zero,
        M13 = TScalar.Zero,
        M14 = TScalar.Zero,
        M21 = TScalar.Zero,
        M22 = yScale,
        M23 = TScalar.Zero,
        M24 = TScalar.Zero,
        M31 = TScalar.Zero,
        M32 = TScalar.Zero,
        M33 = zScale,
        M34 = TScalar.Zero,
        M41 = TScalar.Zero,
        M42 = TScalar.Zero,
        M43 = TScalar.Zero,
        M44 = TScalar.One,
    };

    /// <summary>
    /// Creates a scaling matrix with a center point.
    /// </summary>
    /// <param name="xScale">Value to scale by on the X-axis.</param>
    /// <param name="yScale">Value to scale by on the Y-axis.</param>
    /// <param name="zScale">Value to scale by on the Z-axis.</param>
    /// <param name="centerPoint">The center point.</param>
    /// <returns>The scaling matrix.</returns>
    public static Matrix4x4<TScalar> CreateScale(TScalar xScale, TScalar yScale, TScalar zScale, Vector3<TScalar> centerPoint) => new()
    {
        M11 = xScale,
        M12 = TScalar.Zero,
        M13 = TScalar.Zero,
        M14 = TScalar.Zero,
        M21 = TScalar.Zero,
        M22 = yScale,
        M23 = TScalar.Zero,
        M24 = TScalar.Zero,
        M31 = TScalar.Zero,
        M32 = TScalar.Zero,
        M33 = zScale,
        M34 = TScalar.Zero,
        M41 = centerPoint.X * (TScalar.One - xScale),
        M42 = centerPoint.Y * (TScalar.One - yScale),
        M43 = centerPoint.Z * (TScalar.One - zScale),
        M44 = TScalar.One,
    };

    /// <summary>
    /// Creates a scaling matrix.
    /// </summary>
    /// <param name="scales">The vector containing the amount to scale by on each axis.</param>
    /// <returns>The scaling matrix.</returns>
    public static Matrix4x4<TScalar> CreateScale(Vector3<TScalar> scales) => new()
    {
        M11 = scales.X,
        M12 = TScalar.Zero,
        M13 = TScalar.Zero,
        M14 = TScalar.Zero,
        M21 = TScalar.Zero,
        M22 = scales.Y,
        M23 = TScalar.Zero,
        M24 = TScalar.Zero,
        M31 = TScalar.Zero,
        M32 = TScalar.Zero,
        M33 = scales.Z,
        M34 = TScalar.Zero,
        M41 = TScalar.Zero,
        M42 = TScalar.Zero,
        M43 = TScalar.Zero,
        M44 = TScalar.One,
    };

    /// <summary>
    /// Creates a scaling matrix with a center point.
    /// </summary>
    /// <param name="scales">The vector containing the amount to scale by on each axis.</param>
    /// <param name="centerPoint">The center point.</param>
    /// <returns>The scaling matrix.</returns>
    public static Matrix4x4<TScalar> CreateScale(Vector3<TScalar> scales, Vector3<TScalar> centerPoint) => new()
    {
        M11 = scales.X,
        M12 = TScalar.Zero,
        M13 = TScalar.Zero,
        M14 = TScalar.Zero,
        M21 = TScalar.Zero,
        M22 = scales.Y,
        M23 = TScalar.Zero,
        M24 = TScalar.Zero,
        M31 = TScalar.Zero,
        M32 = TScalar.Zero,
        M33 = scales.Z,
        M34 = TScalar.Zero,
        M41 = centerPoint.X * (TScalar.One - scales.X),
        M42 = centerPoint.Y * (TScalar.One - scales.Y),
        M43 = centerPoint.Z * (TScalar.One - scales.Z),
        M44 = TScalar.One,
    };

    /// <summary>
    /// Creates a uniform scaling matrix that scales equally on each axis.
    /// </summary>
    /// <param name="scale">The uniform scaling factor.</param>
    /// <returns>The scaling matrix.</returns>
    public static Matrix4x4<TScalar> CreateScale(TScalar scale) => new()
    {
        M11 = scale,
        M12 = TScalar.Zero,
        M13 = TScalar.Zero,
        M14 = TScalar.Zero,
        M21 = TScalar.Zero,
        M22 = scale,
        M23 = TScalar.Zero,
        M24 = TScalar.Zero,
        M31 = TScalar.Zero,
        M32 = TScalar.Zero,
        M33 = scale,
        M34 = TScalar.Zero,
        M41 = TScalar.Zero,
        M42 = TScalar.Zero,
        M43 = TScalar.Zero,
        M44 = TScalar.One,
    };

    /// <summary>
    /// Creates a uniform scaling matrix that scales equally on each axis with a center point.
    /// </summary>
    /// <param name="scale">The uniform scaling factor.</param>
    /// <param name="centerPoint">The center point.</param>
    /// <returns>The scaling matrix.</returns>
    public static Matrix4x4<TScalar> CreateScale(TScalar scale, Vector3<TScalar> centerPoint) => new()
    {
        M11 = scale,
        M12 = TScalar.Zero,
        M13 = TScalar.Zero,
        M14 = TScalar.Zero,
        M21 = TScalar.Zero,
        M22 = scale,
        M23 = TScalar.Zero,
        M24 = TScalar.Zero,
        M31 = TScalar.Zero,
        M32 = TScalar.Zero,
        M33 = scale,
        M34 = TScalar.Zero,
        M41 = centerPoint.X * (TScalar.One - scale),
        M42 = centerPoint.Y * (TScalar.One - scale),
        M43 = centerPoint.Z * (TScalar.One - scale),
        M44 = TScalar.One,
    };

    /// <summary>
    /// Creates a matrix for rotating points around the X-axis.
    /// </summary>
    /// <param name="radians">The amount, in radians, by which to rotate around the X-axis.</param>
    /// <returns>The rotation matrix.</returns>
    public static Matrix4x4<TScalar> CreateRotationX(TScalar radians)
    {
        var c = TScalar.Cos(radians);
        var s = TScalar.Sin(radians);

        return new()
        {
            M11 = TScalar.One,
            M12 = TScalar.Zero,
            M13 = TScalar.Zero,
            M14 = TScalar.Zero,
            M21 = TScalar.Zero,
            M22 = c,
            M23 = s,
            M24 = TScalar.Zero,
            M31 = TScalar.Zero,
            M32 = -s,
            M33 = c,
            M34 = TScalar.Zero,
            M41 = TScalar.Zero,
            M42 = TScalar.Zero,
            M43 = TScalar.Zero,
            M44 = TScalar.One,
        };
    }

    /// <summary>
    /// Creates a matrix for rotating points around the X-axis, from a center point.
    /// </summary>
    /// <param name="radians">The amount, in radians, by which to rotate around the X-axis.</param>
    /// <param name="centerPoint">The center point.</param>
    /// <returns>The rotation matrix.</returns>
    public static Matrix4x4<TScalar> CreateRotationX(TScalar radians, Vector3<TScalar> centerPoint)
    {
        var c = TScalar.Cos(radians);
        var s = TScalar.Sin(radians);

        return new()
        {
            M11 = TScalar.One,
            M12 = TScalar.Zero,
            M13 = TScalar.Zero,
            M14 = TScalar.Zero,
            M21 = TScalar.Zero,
            M22 = c,
            M23 = s,
            M24 = TScalar.Zero,
            M31 = TScalar.Zero,
            M32 = -s,
            M33 = c,
            M34 = TScalar.Zero,
            M41 = TScalar.Zero,
            M42 = (centerPoint.Y * (TScalar.One - c)) + (centerPoint.Z * s),
            M43 = (centerPoint.Z * (TScalar.One - c)) - (centerPoint.Y * s),
            M44 = TScalar.One,
        };
    }

    /// <summary>
    /// Creates a matrix for rotating points around the Y-axis.
    /// </summary>
    /// <param name="radians">The amount, in radians, by which to rotate around the Y-axis.</param>
    /// <returns>The rotation matrix.</returns>
    public static Matrix4x4<TScalar> CreateRotationY(TScalar radians)
    {
        var c = TScalar.Cos(radians);
        var s = TScalar.Sin(radians);

        return new()
        {
            M11 = c,
            M12 = TScalar.Zero,
            M13 = -s,
            M14 = TScalar.Zero,
            M21 = TScalar.Zero,
            M22 = TScalar.One,
            M23 = TScalar.Zero,
            M24 = TScalar.Zero,
            M31 = s,
            M32 = TScalar.Zero,
            M33 = c,
            M34 = TScalar.Zero,
            M41 = TScalar.Zero,
            M42 = TScalar.Zero,
            M43 = TScalar.Zero,
            M44 = TScalar.One,
        };
    }

    /// <summary>
    /// Creates a matrix for rotating points around the Y-axis, from a center point.
    /// </summary>
    /// <param name="radians">The amount, in radians, by which to rotate around the Y-axis.</param>
    /// <param name="centerPoint">The center point.</param>
    /// <returns>The rotation matrix.</returns>
    public static Matrix4x4<TScalar> CreateRotationY(TScalar radians, Vector3<TScalar> centerPoint)
    {
        var c = TScalar.Cos(radians);
        var s = TScalar.Sin(radians);

        return new()
        {
            M11 = c,
            M12 = TScalar.Zero,
            M13 = -s,
            M14 = TScalar.Zero,
            M21 = TScalar.Zero,
            M22 = TScalar.One,
            M23 = TScalar.Zero,
            M24 = TScalar.Zero,
            M31 = s,
            M32 = TScalar.Zero,
            M33 = c,
            M34 = TScalar.Zero,
            M41 = (centerPoint.X * (TScalar.One - c)) - (centerPoint.Z * s),
            M42 = TScalar.Zero,
            M43 = (centerPoint.Z * (TScalar.One - c)) + (centerPoint.X * s),
            M44 = TScalar.One,
        };
    }

    /// <summary>
    /// Creates a matrix for rotating points around the Z-axis.
    /// </summary>
    /// <param name="radians">The amount, in radians, by which to rotate around the Z-axis.</param>
    /// <returns>The rotation matrix.</returns>
    public static Matrix4x4<TScalar> CreateRotationZ(TScalar radians)
    {
        var c = TScalar.Cos(radians);
        var s = TScalar.Sin(radians);

        return new()
        {
            M11 = c,
            M12 = s,
            M13 = TScalar.Zero,
            M14 = TScalar.Zero,
            M21 = -s,
            M22 = c,
            M23 = TScalar.Zero,
            M24 = TScalar.Zero,
            M31 = TScalar.Zero,
            M32 = TScalar.Zero,
            M33 = TScalar.One,
            M34 = TScalar.Zero,
            M41 = TScalar.Zero,
            M42 = TScalar.Zero,
            M43 = TScalar.Zero,
            M44 = TScalar.One,
        };
    }

    /// <summary>
    /// Creates a matrix for rotating points around the Z-axis, from a center point.
    /// </summary>
    /// <param name="radians">The amount, in radians, by which to rotate around the Z-axis.</param>
    /// <param name="centerPoint">The center point.</param>
    /// <returns>The rotation matrix.</returns>
    public static Matrix4x4<TScalar> CreateRotationZ(TScalar radians, Vector3<TScalar> centerPoint)
    {
        var c = TScalar.Cos(radians);
        var s = TScalar.Sin(radians);

        return new()
        {
            M11 = c,
            M12 = s,
            M13 = TScalar.Zero,
            M14 = TScalar.Zero,
            M21 = -s,
            M22 = c,
            M23 = TScalar.Zero,
            M24 = TScalar.Zero,
            M31 = TScalar.Zero,
            M32 = TScalar.Zero,
            M33 = TScalar.One,
            M34 = TScalar.Zero,
            M41 = (centerPoint.X * (TScalar.One - c)) + (centerPoint.Y * s),
            M42 = (centerPoint.Y * (TScalar.One - c)) - (centerPoint.X * s),
            M43 = TScalar.Zero,
            M44 = TScalar.One,
        };
    }

    /// <summary>
    /// Creates a matrix that rotates around an arbitrary vector.
    /// </summary>
    /// <param name="axis">The axis to rotate around.</param>
    /// <param name="angle">The angle to rotate around the given axis, in radians.</param>
    /// <returns>The rotation matrix.</returns>
    public static Matrix4x4<TScalar> CreateFromAxisAngle(Vector3<TScalar> axis, TScalar angle)
    {
        TScalar x = axis.X, y = axis.Y, z = axis.Z;
        TScalar sa = TScalar.Sin(angle), ca = TScalar.Cos(angle);
        TScalar xx = x * x, yy = y * y, zz = z * z;
        TScalar xy = x * y, xz = x * z, yz = y * z;

        return new()
        {
            M11 = xx + (ca * (TScalar.One - xx)),
            M12 = xy - (ca * xy) + (sa * z),
            M13 = xz - (ca * xz) - (sa * y),
            M14 = TScalar.Zero,
            M21 = xy - (ca * xy) - (sa * z),
            M22 = yy + (ca * (TScalar.One - yy)),
            M23 = yz - (ca * yz) + (sa * x),
            M24 = TScalar.Zero,
            M31 = xz - (ca * xz) + (sa * y),
            M32 = yz - (ca * yz) - (sa * x),
            M33 = zz + (ca * (TScalar.One - zz)),
            M34 = TScalar.Zero,
            M41 = TScalar.Zero,
            M42 = TScalar.Zero,
            M43 = TScalar.Zero,
            M44 = TScalar.One,
        };
    }

    /// <summary>
    /// Creates a perspective projection matrix based on a field of view, aspect ratio, and near and far view plane distances.
    /// </summary>
    /// <param name="fieldOfView">Field of view in the y direction, in radians.</param>
    /// <param name="aspectRatio">Aspect ratio, defined as view space width divided by height.</param>
    /// <param name="nearPlaneDistance">Distance to the near view plane.</param>
    /// <param name="farPlaneDistance">Distance to the far view plane.</param>
    /// <returns>The perspective projection matrix.</returns>
    public static Matrix4x4<TScalar> CreatePerspectiveFieldOfView(
        TScalar fieldOfView,
        TScalar aspectRatio,
        TScalar nearPlaneDistance,
        TScalar farPlaneDistance)
    {
        if (fieldOfView <= TScalar.Zero
            || fieldOfView >= TScalar.Pi)
        {
            throw new ArgumentOutOfRangeException(nameof(fieldOfView));
        }

        if (nearPlaneDistance <= TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
        }

        if (farPlaneDistance <= TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(farPlaneDistance));
        }

        if (nearPlaneDistance >= farPlaneDistance)
        {
            throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
        }

        var yScale = TScalar.One / TScalar.Tan(fieldOfView / NumberValues.Two<TScalar>());
        var xScale = yScale / aspectRatio;

        var negFarRange = TScalar.IsPositiveInfinity(farPlaneDistance)
            ? -TScalar.One
            : farPlaneDistance / (nearPlaneDistance - farPlaneDistance);

        return new()
        {
            M11 = xScale,
            M12 = TScalar.Zero,
            M13 = TScalar.Zero,
            M14 = TScalar.Zero,
            M21 = yScale,
            M22 = TScalar.Zero,
            M23 = TScalar.Zero,
            M24 = TScalar.Zero,
            M31 = TScalar.Zero,
            M32 = TScalar.Zero,
            M33 = negFarRange,
            M34 = -TScalar.One,
            M41 = TScalar.Zero,
            M42 = TScalar.Zero,
            M43 = TScalar.Zero,
            M44 = nearPlaneDistance * negFarRange,
        };
    }

    /// <summary>
    /// Creates a perspective projection matrix from the given view volume dimensions.
    /// </summary>
    /// <param name="width">Width of the view volume at the near view plane.</param>
    /// <param name="height">Height of the view volume at the near view plane.</param>
    /// <param name="nearPlaneDistance">Distance to the near view plane.</param>
    /// <param name="farPlaneDistance">Distance to the far view plane.</param>
    /// <returns>The perspective projection matrix.</returns>
    public static Matrix4x4<TScalar> CreatePerspective(TScalar width, TScalar height, TScalar nearPlaneDistance, TScalar farPlaneDistance)
    {
        if (nearPlaneDistance <= TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
        }

        if (farPlaneDistance <= TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(farPlaneDistance));
        }

        if (nearPlaneDistance >= farPlaneDistance)
        {
            throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
        }

        var negFarRange = TScalar.IsPositiveInfinity(farPlaneDistance)
            ? -TScalar.One
            : farPlaneDistance / (nearPlaneDistance - farPlaneDistance);

        return new()
        {
            M11 = NumberValues.Two<TScalar>() * nearPlaneDistance / width,
            M12 = TScalar.Zero,
            M13 = TScalar.Zero,
            M14 = TScalar.Zero,
            M21 = NumberValues.Two<TScalar>() * nearPlaneDistance / height,
            M22 = TScalar.Zero,
            M23 = TScalar.Zero,
            M24 = TScalar.Zero,
            M31 = negFarRange,
            M32 = TScalar.Zero,
            M33 = TScalar.Zero,
            M34 = -TScalar.One,
            M41 = TScalar.Zero,
            M42 = TScalar.Zero,
            M43 = TScalar.Zero,
            M44 = nearPlaneDistance * negFarRange,
        };
    }

    /// <summary>
    /// Creates a customized, perspective projection matrix.
    /// </summary>
    /// <param name="left">Minimum x-value of the view volume at the near view plane.</param>
    /// <param name="right">Maximum x-value of the view volume at the near view plane.</param>
    /// <param name="bottom">Minimum y-value of the view volume at the near view plane.</param>
    /// <param name="top">Maximum y-value of the view volume at the near view plane.</param>
    /// <param name="nearPlaneDistance">Distance to the near view plane.</param>
    /// <param name="farPlaneDistance">Distance to of the far view plane.</param>
    /// <returns>The perspective projection matrix.</returns>
    public static Matrix4x4<TScalar> CreatePerspectiveOffCenter(
        TScalar left,
        TScalar right,
        TScalar bottom,
        TScalar top,
        TScalar nearPlaneDistance,
        TScalar farPlaneDistance)
    {
        if (nearPlaneDistance <= TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
        }

        if (farPlaneDistance <= TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(farPlaneDistance));
        }

        if (nearPlaneDistance >= farPlaneDistance)
        {
            throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
        }

        var negFarRange = TScalar.IsPositiveInfinity(farPlaneDistance)
            ? -TScalar.One
            : farPlaneDistance / (nearPlaneDistance - farPlaneDistance);

        return new()
        {
            M11 = NumberValues.Two<TScalar>() * nearPlaneDistance / (right - left),
            M12 = TScalar.Zero,
            M13 = TScalar.Zero,
            M14 = TScalar.Zero,
            M21 = NumberValues.Two<TScalar>() * nearPlaneDistance / (top - bottom),
            M22 = TScalar.Zero,
            M23 = TScalar.Zero,
            M24 = TScalar.Zero,
            M31 = (left + right) / (right - left),
            M32 = (top + bottom) / (top - bottom),
            M33 = negFarRange,
            M34 = -TScalar.One,
            M41 = nearPlaneDistance * negFarRange,
            M42 = TScalar.Zero,
            M43 = TScalar.Zero,
            M44 = TScalar.Zero,
        };
    }

    /// <summary>
    /// Creates an orthographic perspective matrix from the given view volume dimensions.
    /// </summary>
    /// <param name="width">Width of the view volume.</param>
    /// <param name="height">Height of the view volume.</param>
    /// <param name="zNearPlane">Minimum Z-value of the view volume.</param>
    /// <param name="zFarPlane">Maximum Z-value of the view volume.</param>
    /// <returns>The orthographic projection matrix.</returns>
    public static Matrix4x4<TScalar> CreateOrthographic(TScalar width, TScalar height, TScalar zNearPlane, TScalar zFarPlane) => new()
    {
        M11 = NumberValues.Two<TScalar>() / width,
        M12 = TScalar.Zero,
        M13 = TScalar.Zero,
        M14 = TScalar.Zero,
        M21 = NumberValues.Two<TScalar>() / height,
        M22 = TScalar.Zero,
        M23 = TScalar.Zero,
        M24 = TScalar.Zero,
        M31 = TScalar.One / (zNearPlane - zFarPlane),
        M32 = TScalar.Zero,
        M33 = TScalar.Zero,
        M34 = TScalar.Zero,
        M41 = TScalar.Zero,
        M42 = TScalar.Zero,
        M43 = zNearPlane / (zNearPlane - zFarPlane),
        M44 = TScalar.One,
    };

    /// <summary>
    /// Builds a customized, orthographic projection matrix.
    /// </summary>
    /// <param name="left">Minimum X-value of the view volume.</param>
    /// <param name="right">Maximum X-value of the view volume.</param>
    /// <param name="bottom">Minimum Y-value of the view volume.</param>
    /// <param name="top">Maximum Y-value of the view volume.</param>
    /// <param name="zNearPlane">Minimum Z-value of the view volume.</param>
    /// <param name="zFarPlane">Maximum Z-value of the view volume.</param>
    /// <returns>The orthographic projection matrix.</returns>
    public static Matrix4x4<TScalar> CreateOrthographicOffCenter(
        TScalar left,
        TScalar right,
        TScalar bottom,
        TScalar top,
        TScalar zNearPlane,
        TScalar zFarPlane) => new()
        {
            M11 = NumberValues.Two<TScalar>() / (right - left),
            M12 = TScalar.Zero,
            M13 = TScalar.Zero,
            M14 = TScalar.Zero,
            M21 = NumberValues.Two<TScalar>() / (top - bottom),
            M22 = TScalar.Zero,
            M23 = TScalar.Zero,
            M24 = TScalar.Zero,
            M31 = TScalar.One / (zNearPlane - zFarPlane),
            M32 = TScalar.Zero,
            M33 = TScalar.Zero,
            M34 = TScalar.Zero,
            M41 = (left + right) / (left - right),
            M42 = (top + bottom) / (bottom - top),
            M43 = zNearPlane / (zNearPlane - zFarPlane),
            M44 = TScalar.One,
        };

    /// <summary>
    /// Creates a view matrix.
    /// </summary>
    /// <param name="cameraPosition">The position of the camera.</param>
    /// <param name="cameraTarget">The target towards which the camera is pointing.</param>
    /// <param name="cameraUpVector">The direction that is "up" from the camera's point of view.</param>
    /// <returns>The view matrix.</returns>
    public static Matrix4x4<TScalar> CreateLookAt(
        Vector3<TScalar> cameraPosition,
        Vector3<TScalar> cameraTarget,
        Vector3<TScalar> cameraUpVector)
    {
        var zaxis = Vector3<TScalar>.Normalize(cameraPosition - cameraTarget);
        var xaxis = Vector3<TScalar>.Normalize(Vector3<TScalar>.Cross(cameraUpVector, zaxis));
        var yaxis = Vector3<TScalar>.Cross(zaxis, xaxis);

        return new()
        {
            M11 = xaxis.X,
            M12 = yaxis.X,
            M13 = zaxis.X,
            M14 = TScalar.Zero,
            M21 = xaxis.Y,
            M22 = yaxis.Y,
            M23 = zaxis.Y,
            M24 = TScalar.Zero,
            M31 = xaxis.Z,
            M32 = yaxis.Z,
            M33 = zaxis.Z,
            M34 = TScalar.Zero,
            M41 = -Vector3<TScalar>.Dot(xaxis, cameraPosition),
            M42 = -Vector3<TScalar>.Dot(yaxis, cameraPosition),
            M43 = -Vector3<TScalar>.Dot(zaxis, cameraPosition),
            M44 = TScalar.One,
        };
    }

    /// <summary>
    /// Creates a world matrix with the specified parameters.
    /// </summary>
    /// <param name="position">The position of the object; used in translation operations.</param>
    /// <param name="forward">Forward direction of the object.</param>
    /// <param name="up">Upward direction of the object; usually [0, 1, 0].</param>
    /// <returns>The world matrix.</returns>
    public static Matrix4x4<TScalar> CreateWorld(Vector3<TScalar> position, Vector3<TScalar> forward, Vector3<TScalar> up)
    {
        var zaxis = Vector3<TScalar>.Normalize(-forward);
        var xaxis = Vector3<TScalar>.Normalize(Vector3<TScalar>.Cross(up, zaxis));
        var yaxis = Vector3<TScalar>.Cross(zaxis, xaxis);

        return new()
        {
            M11 = xaxis.X,
            M12 = xaxis.Y,
            M13 = xaxis.Z,
            M14 = TScalar.Zero,
            M21 = yaxis.X,
            M22 = yaxis.Y,
            M23 = yaxis.Z,
            M24 = TScalar.Zero,
            M31 = zaxis.X,
            M32 = zaxis.Y,
            M33 = zaxis.Z,
            M34 = TScalar.Zero,
            M41 = position.X,
            M42 = position.Y,
            M43 = position.Z,
            M44 = TScalar.One,
        };
    }

    /// <summary>
    /// Creates a rotation matrix from the given Quaternion rotation value.
    /// </summary>
    /// <param name="quaternion">The source Quaternion.</param>
    /// <returns>The rotation matrix.</returns>
    public static Matrix4x4<TScalar> CreateFromQuaternion(Quaternion<TScalar> quaternion)
    {
        var xx = quaternion.X * quaternion.X;
        var yy = quaternion.Y * quaternion.Y;
        var zz = quaternion.Z * quaternion.Z;

        var xy = quaternion.X * quaternion.Y;
        var wz = quaternion.Z * quaternion.W;
        var xz = quaternion.Z * quaternion.X;
        var wy = quaternion.Y * quaternion.W;
        var yz = quaternion.Y * quaternion.Z;
        var wx = quaternion.X * quaternion.W;

        return new()
        {
            M11 = TScalar.One - (NumberValues.Two<TScalar>() * (yy + zz)),
            M12 = NumberValues.Two<TScalar>() * (xy + wz),
            M13 = NumberValues.Two<TScalar>() * (xz - wy),
            M14 = TScalar.Zero,
            M21 = NumberValues.Two<TScalar>() * (xy - wz),
            M22 = TScalar.One - (NumberValues.Two<TScalar>() * (zz + xx)),
            M23 = NumberValues.Two<TScalar>() * (yz + wx),
            M24 = TScalar.Zero,
            M31 = NumberValues.Two<TScalar>() * (xz + wy),
            M32 = NumberValues.Two<TScalar>() * (yz - wx),
            M33 = TScalar.One - (NumberValues.Two<TScalar>() * (yy + xx)),
            M34 = TScalar.Zero,
            M41 = TScalar.Zero,
            M42 = TScalar.Zero,
            M43 = TScalar.Zero,
            M44 = TScalar.One,
        };
    }

    /// <summary>
    /// Creates a rotation matrix from the specified yaw, pitch, and roll.
    /// </summary>
    /// <param name="yaw">Angle of rotation, in radians, around the Y-axis.</param>
    /// <param name="pitch">Angle of rotation, in radians, around the X-axis.</param>
    /// <param name="roll">Angle of rotation, in radians, around the Z-axis.</param>
    /// <returns>The rotation matrix.</returns>
    public static Matrix4x4<TScalar> CreateFromYawPitchRoll(TScalar yaw, TScalar pitch, TScalar roll)
        => CreateFromQuaternion(Quaternion<TScalar>.CreateFromYawPitchRoll(yaw, pitch, roll));

    /// <summary>
    /// Creates a Matrix that flattens geometry into a specified Plane as if casting a shadow from a specified light source.
    /// </summary>
    /// <param name="lightDirection">The direction from which the light that will cast the shadow is coming.</param>
    /// <param name="plane">The Plane onto which the new matrix should flatten geometry so as to cast a shadow.</param>
    /// <returns>A new Matrix that can be used to flatten geometry onto the specified plane from the specified direction.</returns>
    public static Matrix4x4<TScalar> CreateShadow(Vector3<TScalar> lightDirection, Plane<TScalar> plane)
    {
        var p = Plane<TScalar>.Normalize(plane);

        var dot = (p.Normal.X * lightDirection.X) + (p.Normal.Y * lightDirection.Y) + (p.Normal.Z * lightDirection.Z);
        var a = -p.Normal.X;
        var b = -p.Normal.Y;
        var c = -p.Normal.Z;
        var d = -p.D;

        return new()
        {
            M11 = (a * lightDirection.X) + dot,
            M12 = b * lightDirection.X,
            M13 = c * lightDirection.X,
            M14 = d * lightDirection.X,
            M21 = a * lightDirection.Y,
            M22 = (b * lightDirection.Y) + dot,
            M23 = c * lightDirection.Y,
            M24 = d * lightDirection.Y,
            M31 = a * lightDirection.Z,
            M32 = b * lightDirection.Z,
            M33 = (c * lightDirection.Z) + dot,
            M34 = d * lightDirection.Z,
            M41 = TScalar.Zero,
            M42 = TScalar.Zero,
            M43 = TScalar.Zero,
            M44 = dot,
        };
    }

    /// <summary>
    /// Creates a Matrix that reflects the coordinate system about a specified Plane.
    /// </summary>
    /// <param name="value">The Plane about which to create a reflection.</param>
    /// <returns>A new matrix expressing the reflection.</returns>
    public static Matrix4x4<TScalar> CreateReflection(Plane<TScalar> value)
    {
        value = Plane<TScalar>.Normalize(value);

        var a = value.Normal.X;
        var b = value.Normal.Y;
        var c = value.Normal.Z;

        var fa = -NumberValues.Two<TScalar>() * a;
        var fb = -NumberValues.Two<TScalar>() * b;
        var fc = -NumberValues.Two<TScalar>() * c;

        return new()
        {
            M11 = (fa * a) + TScalar.One,
            M12 = fb * a,
            M13 = fc * a,
            M14 = TScalar.Zero,
            M21 = fa * b,
            M22 = (fb * b) + TScalar.One,
            M23 = fc * b,
            M24 = TScalar.Zero,
            M31 = fa * c,
            M32 = fb * c,
            M33 = (fc * c) + TScalar.One,
            M34 = TScalar.Zero,
            M41 = fa * value.D,
            M42 = fb * value.D,
            M43 = fc * value.D,
            M44 = TScalar.One,
        };
    }

    /// <summary>
    /// Attempts to extract the scale, translation, and rotation components from the given scale/rotation/translation matrix.
    /// If successful, the out parameters will contained the extracted values.
    /// </summary>
    /// <param name="matrix">The source matrix.</param>
    /// <param name="scale">The scaling component of the transformation matrix.</param>
    /// <param name="rotation">The rotation component of the transformation matrix.</param>
    /// <param name="translation">The translation component of the transformation matrix</param>
    /// <returns>True if the source matrix was successfully decomposed; False otherwise.</returns>
    public static bool Decompose(
        Matrix4x4<TScalar> matrix,
        out Vector3<TScalar> scale,
        out Quaternion<TScalar> rotation,
        out Vector3<TScalar> translation)
    {
        var result = true;

        var pCanonicalBasis = new TScalar[3, 3]
        {
            { TScalar.One, TScalar.Zero, TScalar.Zero },
            { TScalar.Zero, TScalar.One, TScalar.Zero },
            { TScalar.Zero, TScalar.Zero, TScalar.One },
        };

        var pVectorBasis = new Vector3<TScalar>[3]
        {
            Vector3<TScalar>.Create(matrix.M11, matrix.M12, matrix.M13),
            Vector3<TScalar>.Create(matrix.M21, matrix.M22, matrix.M23),
            Vector3<TScalar>.Create(matrix.M31, matrix.M32, matrix.M33),
        };

        var pfScales = new TScalar[3]
        {
            Vector3<TScalar>.Length(pVectorBasis[0]),
            Vector3<TScalar>.Length(pVectorBasis[1]),
            Vector3<TScalar>.Length(pVectorBasis[2]),
        };

        uint a, b, c;
        TScalar x = pfScales[0], y = pfScales[1], z = pfScales[2];
        if (x < y)
        {
            if (y < z)
            {
                a = 2;
                b = 1;
                c = 0;
            }
            else
            {
                a = 1;

                if (x < z)
                {
                    b = 2;
                    c = 0;
                }
                else
                {
                    b = 0;
                    c = 2;
                }
            }
        }
        else if (x < z)
        {
            a = 2;
            b = 0;
            c = 1;
        }
        else
        {
            a = 0;

            if (y < z)
            {
                b = 2;
                c = 1;
            }
            else
            {
                b = 1;
                c = 2;
            }
        }

        var epsilon = TScalar.One / NumberValues.TenThousand<TScalar>();
        if (pfScales[a] < epsilon)
        {
            pVectorBasis[a] = Vector3<TScalar>.Create(pCanonicalBasis[a, 0], pCanonicalBasis[a, 1], pCanonicalBasis[a, 2]);
        }

        pVectorBasis[a] = Vector3<TScalar>.Normalize(pVectorBasis[a]);
        pfScales[a] = TScalar.One;

        if (pfScales[b] < epsilon)
        {
            uint cc;
            TScalar fAbsX, fAbsY, fAbsZ;

            fAbsX = TScalar.Abs(pVectorBasis[a].X);
            fAbsY = TScalar.Abs(pVectorBasis[a].Y);
            fAbsZ = TScalar.Abs(pVectorBasis[a].Z);

            if (fAbsX < fAbsY)
            {
                if (fAbsY < fAbsZ)
                {
                    cc = 0;
                }
                else if (fAbsX < fAbsZ)
                {
                    cc = 0;
                }
                else
                {
                    cc = 2;
                }
            }
            else if (fAbsX < fAbsZ)
            {
                cc = 1;
            }
            else if (fAbsY < fAbsZ)
            {
                cc = 1;
            }
            else
            {
                cc = 2;
            }

            pVectorBasis[b] = Vector3<TScalar>.Cross(
                pVectorBasis[a],
                Vector3<TScalar>.Create(pCanonicalBasis[cc, 0], pCanonicalBasis[cc, 1], pCanonicalBasis[cc, 2]));
        }

        pVectorBasis[b] = Vector3<TScalar>.Normalize(pVectorBasis[b]);
        pfScales[b] = TScalar.One;

        if (pfScales[c] < epsilon)
        {
            pVectorBasis[c] = Vector3<TScalar>.Cross(pVectorBasis[a], pVectorBasis[b]);
        }

        pVectorBasis[c] = Vector3<TScalar>.Normalize(pVectorBasis[c]);
        pfScales[c] = TScalar.One;

        var matTemp = new Matrix4x4<TScalar>()
        {
            M11 = pVectorBasis[0].X,
            M12 = pVectorBasis[0].Y,
            M13 = pVectorBasis[0].Z,
            M14 = TScalar.Zero,
            M21 = pVectorBasis[1].X,
            M22 = pVectorBasis[1].Y,
            M23 = pVectorBasis[1].Z,
            M24 = TScalar.Zero,
            M31 = pVectorBasis[2].X,
            M32 = pVectorBasis[2].Y,
            M33 = pVectorBasis[2].Z,
            M34 = TScalar.Zero,
            M41 = TScalar.Zero,
            M42 = TScalar.Zero,
            M43 = TScalar.Zero,
            M44 = TScalar.One,
        };
        var det = matTemp.GetDeterminant();

        // use Kramer's rule to check for handedness of coordinate system
        if (det < TScalar.Zero)
        {
            // switch coordinate system by negating the scale and inverting the basis vector on the x-axis
            pfScales[a] = -pfScales[a];
            pVectorBasis[a] = -pVectorBasis[a];

            matTemp = new Matrix4x4<TScalar>()
            {
                M11 = pVectorBasis[0].X,
                M12 = pVectorBasis[0].Y,
                M13 = pVectorBasis[0].Z,
                M14 = TScalar.Zero,
                M21 = pVectorBasis[1].X,
                M22 = pVectorBasis[1].Y,
                M23 = pVectorBasis[1].Z,
                M24 = TScalar.Zero,
                M31 = pVectorBasis[2].X,
                M32 = pVectorBasis[2].Y,
                M33 = pVectorBasis[2].Z,
                M34 = TScalar.Zero,
                M41 = TScalar.Zero,
                M42 = TScalar.Zero,
                M43 = TScalar.Zero,
                M44 = TScalar.One,
            };

            det = -det;
        }

        det--;
        det *= det;

        scale = Vector3<TScalar>.Create(pfScales[0], pfScales[1], pfScales[2]);

        translation = Vector3<TScalar>.Create(
            matrix.M41,
            matrix.M42,
            matrix.M43);

        if (epsilon < det)
        {
            // Non-SRT matrix encountered
            rotation = Quaternion<TScalar>.MultiplicativeIdentity;
            result = false;
        }
        else
        {
            // generate the quaternion from the matrix
            rotation = Quaternion<TScalar>.CreateFromRotationMatrix(matTemp);
        }

        return result;
    }

    /// <summary>
    /// Attempts to calculate the inverse of the given matrix. If successful, result will contain the inverted matrix.
    /// </summary>
    /// <param name="matrix">The source matrix to invert.</param>
    /// <param name="result">If successful, contains the inverted matrix.</param>
    /// <returns>True if the source matrix could be inverted; False otherwise.</returns>
    public static bool Invert(Matrix4x4<TScalar> matrix, out Matrix4x4<TScalar> result)
    {
        TScalar a = matrix.M11, b = matrix.M12, c = matrix.M13, d = matrix.M14;
        TScalar e = matrix.M21, f = matrix.M22, g = matrix.M23, h = matrix.M24;
        TScalar i = matrix.M31, j = matrix.M32, k = matrix.M33, l = matrix.M34;
        TScalar m = matrix.M41, n = matrix.M42, o = matrix.M43, p = matrix.M44;

        var kp_lo = (k * p) - (l * o);
        var jp_ln = (j * p) - (l * n);
        var jo_kn = (j * o) - (k * n);
        var ip_lm = (i * p) - (l * m);
        var io_km = (i * o) - (k * m);
        var in_jm = (i * n) - (j * m);

        var a11 = +((f * kp_lo) - (g * jp_ln) + (h * jo_kn));
        var a12 = -((e * kp_lo) - (g * ip_lm) + (h * io_km));
        var a13 = +((e * jp_ln) - (f * ip_lm) + (h * in_jm));
        var a14 = -((e * jo_kn) - (f * io_km) + (g * in_jm));

        var det = (a * a11) + (b * a12) + (c * a13) + (d * a14);

        if (TScalar.Abs(det).IsNearlyZero())
        {
            result = MultiplicativeIdentity;
            return false;
        }

        var invDet = TScalar.One / det;

        var gp_ho = (g * p) - (h * o);
        var fp_hn = (f * p) - (h * n);
        var fo_gn = (f * o) - (g * n);
        var ep_hm = (e * p) - (h * m);
        var eo_gm = (e * o) - (g * m);
        var en_fm = (e * n) - (f * m);

        var gl_hk = (g * l) - (h * k);
        var fl_hj = (f * l) - (h * j);
        var fk_gj = (f * k) - (g * j);
        var el_hi = (e * l) - (h * i);
        var ek_gi = (e * k) - (g * i);
        var ej_fi = (e * j) - (f * i);

        result = new Matrix4x4<TScalar>()
        {
            M11 = a11 * invDet,
            M12 = a12 * invDet,
            M13 = a13 * invDet,
            M14 = a14 * invDet,
            M21 = -((b * kp_lo) - (c * jp_ln) + (d * jo_kn)) * invDet,
            M22 = +((a * kp_lo) - (c * ip_lm) + (d * io_km)) * invDet,
            M23 = -((a * jp_ln) - (b * ip_lm) + (d * in_jm)) * invDet,
            M24 = +((a * jo_kn) - (b * io_km) + (c * in_jm)) * invDet,
            M31 = +((b * gp_ho) - (c * fp_hn) + (d * fo_gn)) * invDet,
            M32 = -((a * gp_ho) - (c * ep_hm) + (d * eo_gm)) * invDet,
            M33 = +((a * fp_hn) - (b * ep_hm) + (d * en_fm)) * invDet,
            M34 = -((a * fo_gn) - (b * eo_gm) + (c * en_fm)) * invDet,
            M41 = -((b * gl_hk) - (c * fl_hj) + (d * fk_gj)) * invDet,
            M42 = +((a * gl_hk) - (c * el_hi) + (d * ek_gi)) * invDet,
            M43 = -((a * fl_hj) - (b * el_hi) + (d * ej_fi)) * invDet,
            M44 = +((a * fk_gj) - (b * ek_gi) + (c * ej_fi)) * invDet,
        };

        return true;
    }

    /// <summary>
    /// Transforms the given matrix by applying the given Quaternion rotation.
    /// </summary>
    /// <param name="value">The source matrix to transform.</param>
    /// <param name="rotation">The rotation to apply.</param>
    /// <returns>The transformed matrix.</returns>
    public static Matrix4x4<TScalar> Transform(Matrix4x4<TScalar> value, Quaternion<TScalar> rotation)
    {
        // Compute rotation matrix.
        var x2 = rotation.X + rotation.X;
        var y2 = rotation.Y + rotation.Y;
        var z2 = rotation.Z + rotation.Z;

        var wx2 = rotation.W * x2;
        var wy2 = rotation.W * y2;
        var wz2 = rotation.W * z2;
        var xx2 = rotation.X * x2;
        var xy2 = rotation.X * y2;
        var xz2 = rotation.X * z2;
        var yy2 = rotation.Y * y2;
        var yz2 = rotation.Y * z2;
        var zz2 = rotation.Z * z2;

        var q11 = TScalar.One - yy2 - zz2;
        var q21 = xy2 - wz2;
        var q31 = xz2 + wy2;

        var q12 = xy2 + wz2;
        var q22 = TScalar.One - xx2 - zz2;
        var q32 = yz2 - wx2;

        var q13 = xz2 - wy2;
        var q23 = yz2 + wx2;
        var q33 = TScalar.One - xx2 - yy2;

        return new Matrix4x4<TScalar>()
        {
            M11 = (value.M11 * q11) + (value.M12 * q21) + (value.M13 * q31),
            M12 = (value.M11 * q12) + (value.M12 * q22) + (value.M13 * q32),
            M13 = (value.M11 * q13) + (value.M12 * q23) + (value.M13 * q33),
            M14 = value.M14,
            M21 = (value.M21 * q11) + (value.M22 * q21) + (value.M23 * q31),
            M22 = (value.M21 * q12) + (value.M22 * q22) + (value.M23 * q32),
            M23 = (value.M21 * q13) + (value.M22 * q23) + (value.M23 * q33),
            M24 = value.M24,
            M31 = (value.M31 * q11) + (value.M32 * q21) + (value.M33 * q31),
            M32 = (value.M31 * q12) + (value.M32 * q22) + (value.M33 * q32),
            M33 = (value.M31 * q13) + (value.M32 * q23) + (value.M33 * q33),
            M34 = value.M34,
            M41 = (value.M41 * q11) + (value.M42 * q21) + (value.M43 * q31),
            M42 = (value.M41 * q12) + (value.M42 * q22) + (value.M43 * q32),
            M43 = (value.M41 * q13) + (value.M42 * q23) + (value.M43 * q33),
            M44 = value.M44,
        };
    }

    /// <summary>
    /// Transposes the rows and columns of a matrix.
    /// </summary>
    /// <param name="matrix">The source matrix.</param>
    /// <returns>The transposed matrix.</returns>
    public static Matrix4x4<TScalar> Transpose(Matrix4x4<TScalar> matrix) => new()
    {
        M11 = matrix.M11,
        M12 = matrix.M21,
        M13 = matrix.M31,
        M14 = matrix.M41,
        M21 = matrix.M12,
        M22 = matrix.M22,
        M23 = matrix.M32,
        M24 = matrix.M42,
        M31 = matrix.M13,
        M32 = matrix.M23,
        M33 = matrix.M33,
        M34 = matrix.M43,
        M41 = matrix.M14,
        M42 = matrix.M24,
        M43 = matrix.M34,
        M44 = matrix.M44,
    };

    /// <summary>
    /// Linearly interpolates between the corresponding values of two matrices.
    /// </summary>
    /// <param name="matrix1">The first source matrix.</param>
    /// <param name="matrix2">The second source matrix.</param>
    /// <param name="amount">The relative weight of the second source matrix.</param>
    /// <returns>The interpolated matrix.</returns>
    public static Matrix4x4<TScalar> Lerp(Matrix4x4<TScalar> matrix1, Matrix4x4<TScalar> matrix2, TScalar amount) => new()
    {
        M11 = matrix1.M11 + ((matrix2.M11 - matrix1.M11) * amount),
        M12 = matrix1.M12 + ((matrix2.M12 - matrix1.M12) * amount),
        M13 = matrix1.M13 + ((matrix2.M13 - matrix1.M13) * amount),
        M14 = matrix1.M14 + ((matrix2.M14 - matrix1.M14) * amount),
        M21 = matrix1.M21 + ((matrix2.M21 - matrix1.M21) * amount),
        M22 = matrix1.M22 + ((matrix2.M22 - matrix1.M22) * amount),
        M23 = matrix1.M23 + ((matrix2.M23 - matrix1.M23) * amount),
        M24 = matrix1.M24 + ((matrix2.M24 - matrix1.M24) * amount),
        M31 = matrix1.M31 + ((matrix2.M31 - matrix1.M31) * amount),
        M32 = matrix1.M32 + ((matrix2.M32 - matrix1.M32) * amount),
        M33 = matrix1.M33 + ((matrix2.M33 - matrix1.M33) * amount),
        M34 = matrix1.M34 + ((matrix2.M34 - matrix1.M34) * amount),
        M41 = matrix1.M41 + ((matrix2.M41 - matrix1.M41) * amount),
        M42 = matrix1.M42 + ((matrix2.M42 - matrix1.M42) * amount),
        M43 = matrix1.M43 + ((matrix2.M43 - matrix1.M43) * amount),
        M44 = matrix1.M44 + ((matrix2.M44 - matrix1.M44) * amount),
    };

    /// <summary>
    /// Returns a new matrix with the negated elements of the given matrix.
    /// </summary>
    /// <param name="value">The source matrix.</param>
    /// <returns>The negated matrix.</returns>
    public static Matrix4x4<TScalar> operator -(Matrix4x4<TScalar> value) => new()
    {
        M11 = -value.M11,
        M12 = -value.M12,
        M13 = -value.M13,
        M14 = -value.M14,
        M21 = -value.M21,
        M22 = -value.M22,
        M23 = -value.M23,
        M24 = -value.M24,
        M31 = -value.M31,
        M32 = -value.M32,
        M33 = -value.M33,
        M34 = -value.M34,
        M41 = -value.M41,
        M42 = -value.M42,
        M43 = -value.M43,
        M44 = -value.M44,
    };

    /// <summary>
    /// Adds two matrices together.
    /// </summary>
    /// <param name="value1">The first source matrix.</param>
    /// <param name="value2">The second source matrix.</param>
    /// <returns>The resulting matrix.</returns>
    public static Matrix4x4<TScalar> operator +(Matrix4x4<TScalar> value1, Matrix4x4<TScalar> value2) => new()
    {
        M11 = value1.M11 + value2.M11,
        M12 = value1.M12 + value2.M12,
        M13 = value1.M13 + value2.M13,
        M14 = value1.M14 + value2.M14,
        M21 = value1.M21 + value2.M21,
        M22 = value1.M22 + value2.M22,
        M23 = value1.M23 + value2.M23,
        M24 = value1.M24 + value2.M24,
        M31 = value1.M31 + value2.M31,
        M32 = value1.M32 + value2.M32,
        M33 = value1.M33 + value2.M33,
        M34 = value1.M34 + value2.M34,
        M41 = value1.M41 + value2.M41,
        M42 = value1.M42 + value2.M42,
        M43 = value1.M43 + value2.M43,
        M44 = value1.M44 + value2.M44,
    };

    /// <summary>
    /// Subtracts the second matrix from the first.
    /// </summary>
    /// <param name="value1">The first source matrix.</param>
    /// <param name="value2">The second source matrix.</param>
    /// <returns>The result of the subtraction.</returns>
    public static Matrix4x4<TScalar> operator -(Matrix4x4<TScalar> value1, Matrix4x4<TScalar> value2) => new()
    {
        M11 = value1.M11 - value2.M11,
        M12 = value1.M12 - value2.M12,
        M13 = value1.M13 - value2.M13,
        M14 = value1.M14 - value2.M14,
        M21 = value1.M21 - value2.M21,
        M22 = value1.M22 - value2.M22,
        M23 = value1.M23 - value2.M23,
        M24 = value1.M24 - value2.M24,
        M31 = value1.M31 - value2.M31,
        M32 = value1.M32 - value2.M32,
        M33 = value1.M33 - value2.M33,
        M34 = value1.M34 - value2.M34,
        M41 = value1.M41 - value2.M41,
        M42 = value1.M42 - value2.M42,
        M43 = value1.M43 - value2.M43,
        M44 = value1.M44 - value2.M44,
    };

    /// <summary>
    /// Multiplies a matrix by another matrix.
    /// </summary>
    /// <param name="value1">The first source matrix.</param>
    /// <param name="value2">The second source matrix.</param>
    /// <returns>The result of the multiplication.</returns>
    public static Matrix4x4<TScalar> operator *(Matrix4x4<TScalar> value1, Matrix4x4<TScalar> value2) => new()
    {
        M11 = (value1.M11 * value2.M11) + (value1.M12 * value2.M21) + (value1.M13 * value2.M31) + (value1.M14 * value2.M41),
        M12 = (value1.M11 * value2.M12) + (value1.M12 * value2.M22) + (value1.M13 * value2.M32) + (value1.M14 * value2.M42),
        M13 = (value1.M11 * value2.M13) + (value1.M12 * value2.M23) + (value1.M13 * value2.M33) + (value1.M14 * value2.M43),
        M14 = (value1.M11 * value2.M14) + (value1.M12 * value2.M24) + (value1.M13 * value2.M34) + (value1.M14 * value2.M44),
        M21 = (value1.M21 * value2.M11) + (value1.M22 * value2.M21) + (value1.M23 * value2.M31) + (value1.M24 * value2.M41),
        M22 = (value1.M21 * value2.M12) + (value1.M22 * value2.M22) + (value1.M23 * value2.M32) + (value1.M24 * value2.M42),
        M23 = (value1.M21 * value2.M13) + (value1.M22 * value2.M23) + (value1.M23 * value2.M33) + (value1.M24 * value2.M43),
        M24 = (value1.M21 * value2.M14) + (value1.M22 * value2.M24) + (value1.M23 * value2.M34) + (value1.M24 * value2.M44),
        M31 = (value1.M31 * value2.M11) + (value1.M32 * value2.M21) + (value1.M33 * value2.M31) + (value1.M34 * value2.M41),
        M32 = (value1.M31 * value2.M12) + (value1.M32 * value2.M22) + (value1.M33 * value2.M32) + (value1.M34 * value2.M42),
        M33 = (value1.M31 * value2.M13) + (value1.M32 * value2.M23) + (value1.M33 * value2.M33) + (value1.M34 * value2.M43),
        M34 = (value1.M31 * value2.M14) + (value1.M32 * value2.M24) + (value1.M33 * value2.M34) + (value1.M34 * value2.M44),
        M41 = (value1.M41 * value2.M11) + (value1.M42 * value2.M21) + (value1.M43 * value2.M31) + (value1.M44 * value2.M41),
        M42 = (value1.M41 * value2.M12) + (value1.M42 * value2.M22) + (value1.M43 * value2.M32) + (value1.M44 * value2.M42),
        M43 = (value1.M41 * value2.M13) + (value1.M42 * value2.M23) + (value1.M43 * value2.M33) + (value1.M44 * value2.M43),
        M44 = (value1.M41 * value2.M14) + (value1.M42 * value2.M24) + (value1.M43 * value2.M34) + (value1.M44 * value2.M44),
    };

    /// <summary>
    /// Multiplies a matrix by a scalar value.
    /// </summary>
    /// <param name="value1">The source matrix.</param>
    /// <param name="value2">The scaling factor.</param>
    /// <returns>The scaled matrix.</returns>
    public static Matrix4x4<TScalar> operator *(Matrix4x4<TScalar> value1, TScalar value2) => new()
    {
        M11 = value1.M11 * value2,
        M12 = value1.M12 * value2,
        M13 = value1.M13 * value2,
        M14 = value1.M14 * value2,
        M21 = value1.M21 * value2,
        M22 = value1.M22 * value2,
        M23 = value1.M23 * value2,
        M24 = value1.M24 * value2,
        M31 = value1.M31 * value2,
        M32 = value1.M32 * value2,
        M33 = value1.M33 * value2,
        M34 = value1.M34 * value2,
        M41 = value1.M41 * value2,
        M42 = value1.M42 * value2,
        M43 = value1.M43 * value2,
        M44 = value1.M44 * value2,
    };

    /// <summary>
    /// Returns a boolean indicating whether the given two matrices are equal.
    /// </summary>
    /// <param name="value1">The first matrix to compare.</param>
    /// <param name="value2">The second matrix to compare.</param>
    /// <returns>True if the given matrices are equal; False otherwise.</returns>
    public static bool operator ==(Matrix4x4<TScalar> value1, Matrix4x4<TScalar> value2)
        => value1.Equals(value2);

    /// <summary>
    /// Returns a boolean indicating whether the given two matrices are not equal.
    /// </summary>
    /// <param name="value1">The first matrix to compare.</param>
    /// <param name="value2">The second matrix to compare.</param>
    /// <returns>True if the given matrices are not equal; False if they are equal.</returns>
    public static bool operator !=(Matrix4x4<TScalar> value1, Matrix4x4<TScalar> value2)
        => !value1.Equals(value2);

    /// <summary>
    /// Converts between implementations of matrices.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator Matrix4x4<TScalar>(Matrix4x4 value) => new()
    {
        M11 = TScalar.Create(value.M11),
        M12 = TScalar.Create(value.M12),
        M13 = TScalar.Create(value.M13),
        M14 = TScalar.Create(value.M14),
        M21 = TScalar.Create(value.M21),
        M22 = TScalar.Create(value.M22),
        M23 = TScalar.Create(value.M23),
        M24 = TScalar.Create(value.M24),
        M31 = TScalar.Create(value.M31),
        M32 = TScalar.Create(value.M32),
        M33 = TScalar.Create(value.M33),
        M34 = TScalar.Create(value.M34),
        M41 = TScalar.Create(value.M41),
        M42 = TScalar.Create(value.M42),
        M43 = TScalar.Create(value.M43),
        M44 = TScalar.Create(value.M44),
    };

    /// <summary>
    /// Converts between implementations of matrices.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static explicit operator Matrix4x4(Matrix4x4<TScalar> value) => new(
        value.M11.Create<TScalar, float>(),
        value.M12.Create<TScalar, float>(),
        value.M13.Create<TScalar, float>(),
        value.M14.Create<TScalar, float>(),
        value.M21.Create<TScalar, float>(),
        value.M22.Create<TScalar, float>(),
        value.M23.Create<TScalar, float>(),
        value.M24.Create<TScalar, float>(),
        value.M31.Create<TScalar, float>(),
        value.M32.Create<TScalar, float>(),
        value.M33.Create<TScalar, float>(),
        value.M34.Create<TScalar, float>(),
        value.M41.Create<TScalar, float>(),
        value.M42.Create<TScalar, float>(),
        value.M43.Create<TScalar, float>(),
        value.M44.Create<TScalar, float>());

    /// <summary>
    /// Returns a boolean indicating whether this matrix instance is equal to the other given matrix.
    /// </summary>
    /// <param name="other">The matrix to compare this instance to.</param>
    /// <returns>True if the matrices are equal; False otherwise.</returns>
    /// <remarks>
    /// Checks diagonal element first for early out.
    /// </remarks>
    public bool Equals(Matrix4x4<TScalar> other)
        => M11 == other.M11 && M22 == other.M22 && M33 == other.M33 && M44 == other.M44
        && M12 == other.M12 && M13 == other.M13 && M14 == other.M14 && M21 == other.M21
        && M23 == other.M23 && M24 == other.M24 && M31 == other.M31 && M32 == other.M32
        && M34 == other.M34 && M41 == other.M41 && M42 == other.M42 && M43 == other.M43;

    /// <summary>
    /// Returns a boolean indicating whether the given Object is equal to this matrix instance.
    /// </summary>
    /// <param name="obj">The Object to compare against.</param>
    /// <returns>True if the Object is equal to this matrix; False otherwise.</returns>
    public override bool Equals(object? obj) => obj is Matrix4x4<TScalar> other && Equals(other);

    /// <summary>
    /// Calculates the determinant of the matrix.
    /// </summary>
    /// <returns>The determinant of the matrix.</returns>
    public TScalar GetDeterminant()
    {
        TScalar a = M11, b = M12, c = M13, d = M14;
        TScalar e = M21, f = M22, g = M23, h = M24;
        TScalar i = M31, j = M32, k = M33, l = M34;
        TScalar m = M41, n = M42, o = M43, p = M44;

        var kp_lo = (k * p) - (l * o);
        var jp_ln = (j * p) - (l * n);
        var jo_kn = (j * o) - (k * n);
        var ip_lm = (i * p) - (l * m);
        var io_km = (i * o) - (k * m);
        var in_jm = (i * n) - (j * m);

        return (a * ((f * kp_lo) - (g * jp_ln) + (h * jo_kn)))
                - (b * ((e * kp_lo) - (g * ip_lm) + (h * io_km)))
                + (c * ((e * jp_ln) - (f * ip_lm) + (h * in_jm)))
                - (d * ((e * jo_kn) - (f * io_km) + (g * in_jm)));
    }

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode()
    {
        var hc = new HashCode();
        hc.Add(M11);
        hc.Add(M12);
        hc.Add(M13);
        hc.Add(M14);
        hc.Add(M21);
        hc.Add(M22);
        hc.Add(M23);
        hc.Add(M24);
        hc.Add(M31);
        hc.Add(M32);
        hc.Add(M33);
        hc.Add(M34);
        hc.Add(M41);
        hc.Add(M42);
        hc.Add(M43);
        hc.Add(M44);
        return hc.ToHashCode();
    }

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
        .Append(" M13:")
        .Append(M13.ToString(format, formatProvider))
        .Append(" M14:")
        .Append(M14.ToString(format, formatProvider))
        .Append("} {M21:")
        .Append(M21.ToString(format, formatProvider))
        .Append(" M22:")
        .Append(M22.ToString(format, formatProvider))
        .Append(" M23:")
        .Append(M23.ToString(format, formatProvider))
        .Append(" M24:")
        .Append(M24.ToString(format, formatProvider))
        .Append("} {M31:")
        .Append(M31.ToString(format, formatProvider))
        .Append(" M32:")
        .Append(M32.ToString(format, formatProvider))
        .Append(" M33:")
        .Append(M33.ToString(format, formatProvider))
        .Append(" M34:")
        .Append(M34.ToString(format, formatProvider))
        .Append("} {M41:")
        .Append(M41.ToString(format, formatProvider))
        .Append(" M42:")
        .Append(M42.ToString(format, formatProvider))
        .Append(" M43:")
        .Append(M43.ToString(format, formatProvider))
        .Append(" M44:")
        .Append(M44.ToString(format, formatProvider))
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
        if (destination.Length < 91
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

        if (destination.Length < 84
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

        if (destination.Length < 79
            || !" M13:".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 5;
        destination = destination[5..];

        if (!M13.TryFormat(destination, out c, format, provider))
        {
            return false;
        }
        charsWritten += c;
        destination = destination[c..];

        if (destination.Length < 74
            || !" M14:".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 5;
        destination = destination[5..];

        if (!M14.TryFormat(destination, out c, format, provider))
        {
            return false;
        }
        charsWritten += c;
        destination = destination[c..];

        if (destination.Length < 69
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

        if (destination.Length < 62
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

        if (destination.Length < 57
            || !" M23:".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 5;
        destination = destination[5..];

        if (!M23.TryFormat(destination, out c, format, provider))
        {
            return false;
        }
        charsWritten += c;
        destination = destination[c..];

        if (destination.Length < 52
            || !" M24:".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 5;
        destination = destination[5..];

        if (!M24.TryFormat(destination, out c, format, provider))
        {
            return false;
        }
        charsWritten += c;
        destination = destination[c..];

        if (destination.Length < 47
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

        if (destination.Length < 40
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

        if (destination.Length < 35
            || !" M33:".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 5;
        destination = destination[5..];

        if (!M33.TryFormat(destination, out c, format, provider))
        {
            return false;
        }
        charsWritten += c;
        destination = destination[c..];

        if (destination.Length < 30
            || !" M34:".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 5;
        destination = destination[5..];

        if (!M34.TryFormat(destination, out c, format, provider))
        {
            return false;
        }
        charsWritten += c;
        destination = destination[c..];

        if (destination.Length < 25
            || !"} {M41:".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 7;
        destination = destination[7..];

        if (!M41.TryFormat(destination, out c, format, provider))
        {
            return false;
        }
        charsWritten += c;
        destination = destination[c..];

        if (destination.Length < 18
            || !" M42:".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 5;
        destination = destination[5..];

        if (!M42.TryFormat(destination, out c, format, provider))
        {
            return false;
        }
        charsWritten += c;
        destination = destination[c..];

        if (destination.Length < 13
            || !" M43:".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 5;
        destination = destination[5..];

        if (!M43.TryFormat(destination, out c, format, provider))
        {
            return false;
        }
        charsWritten += c;
        destination = destination[c..];

        if (destination.Length < 8
            || !" M44:".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 5;
        destination = destination[5..];

        if (!M44.TryFormat(destination, out c, format, provider))
        {
            return false;
        }
        charsWritten += c;
        destination = destination[c..];

        if (destination.Length < 3)
        {
            return false;
        }
        if (!"} }".AsSpan().TryCopyTo(destination))
        {
            return false;
        }
        charsWritten += 3;
        return true;
    }
}
