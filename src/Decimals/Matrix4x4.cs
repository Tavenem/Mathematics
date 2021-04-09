using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Tavenem.Mathematics.Decimals
{
    /// <summary>
    /// A structure encapsulating a 4x4 matrix.
    /// </summary>
    /// <remarks>
    /// Unlike the <see cref="System.Numerics.Matrix4x4"/> implementation, this is an immutable struct
    /// with read-only properties, rather than writable fields.
    /// </remarks>
    [Serializable]
    [DataContract]
    public struct Matrix4x4 : IEquatable<Matrix4x4>
    {
        /// <summary>
        /// Returns the multiplicative identity matrix.
        /// </summary>
        public static Matrix4x4 Identity { get; } = new Matrix4x4
        (
            1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1
        );

        /// <summary>
        /// Value at row 1, column 1 of the matrix.
        /// </summary>
        [DataMember(Order = 1)]
        public decimal M11 { get; }

        /// <summary>
        /// Value at row 1, column 2 of the matrix.
        /// </summary>
        [DataMember(Order = 2)]
        public decimal M12 { get; }

        /// <summary>
        /// Value at row 1, column 3 of the matrix.
        /// </summary>
        [DataMember(Order = 3)]
        public decimal M13 { get; }

        /// <summary>
        /// Value at row 1, column 4 of the matrix.
        /// </summary>
        [DataMember(Order = 4)]
        public decimal M14 { get; }

        /// <summary>
        /// Value at row 2, column 1 of the matrix.
        /// </summary>
        [DataMember(Order = 5)]
        public decimal M21 { get; }

        /// <summary>
        /// Value at row 2, column 2 of the matrix.
        /// </summary>
        [DataMember(Order = 6)]
        public decimal M22 { get; }

        /// <summary>
        /// Value at row 2, column 3 of the matrix.
        /// </summary>
        [DataMember(Order = 7)]
        public decimal M23 { get; }

        /// <summary>
        /// Value at row 2, column 4 of the matrix.
        /// </summary>
        [DataMember(Order = 8)]
        public decimal M24 { get; }

        /// <summary>
        /// Value at row 3, column 1 of the matrix.
        /// </summary>
        [DataMember(Order = 9)]
        public decimal M31 { get; }

        /// <summary>
        /// Value at row 3, column 2 of the matrix.
        /// </summary>
        [DataMember(Order = 10)]
        public decimal M32 { get; }

        /// <summary>
        /// Value at row 3, column 3 of the matrix.
        /// </summary>
        [DataMember(Order = 11)]
        public decimal M33 { get; }

        /// <summary>
        /// Value at row 3, column 4 of the matrix.
        /// </summary>
        [DataMember(Order = 12)]
        public decimal M34 { get; }

        /// <summary>
        /// Value at row 4, column 1 of the matrix.
        /// </summary>
        [DataMember(Order = 13)]
        public decimal M41 { get; }

        /// <summary>
        /// Value at row 4, column 2 of the matrix.
        /// </summary>
        [DataMember(Order = 14)]
        public decimal M42 { get; }

        /// <summary>
        /// Value at row 4, column 3 of the matrix.
        /// </summary>
        [DataMember(Order = 15)]
        public decimal M43 { get; }

        /// <summary>
        /// Value at row 4, column 4 of the matrix.
        /// </summary>
        [DataMember(Order = 16)]
        public decimal M44 { get; }

        /// <summary>
        /// Returns whether the matrix is the identity matrix.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public bool IsIdentity => Equals(Identity);

        /// <summary>
        /// Gets the translation component of this matrix.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public Vector3 Translation => new(M41, M42, M43);

        /// <summary>
        /// Constructs a Matrix4x4 from the given components.
        /// </summary>
        /// <param name="m11">Value at row 1, column 1 of the matrix.</param>
        /// <param name="m12">Value at row 1, column 2 of the matrix.</param>
        /// <param name="m13">Value at row 1, column 3 of the matrix.</param>
        /// <param name="m14">Value at row 1, column 4 of the matrix.</param>
        /// <param name="m21">Value at row 2, column 1 of the matrix.</param>
        /// <param name="m22">Value at row 2, column 2 of the matrix.</param>
        /// <param name="m23">Value at row 2, column 3 of the matrix.</param>
        /// <param name="m24">Value at row 2, column 4 of the matrix.</param>
        /// <param name="m31">Value at row 3, column 1 of the matrix.</param>
        /// <param name="m32">Value at row 3, column 2 of the matrix.</param>
        /// <param name="m33">Value at row 3, column 3 of the matrix.</param>
        /// <param name="m34">Value at row 3, column 4 of the matrix.</param>
        /// <param name="m41">Value at row 4, column 1 of the matrix.</param>
        /// <param name="m42">Value at row 4, column 2 of the matrix.</param>
        /// <param name="m43">Value at row 4, column 3 of the matrix.</param>
        /// <param name="m44">Value at row 4, column 4 of the matrix.</param>
        [System.Text.Json.Serialization.JsonConstructor]
        public Matrix4x4(decimal m11, decimal m12, decimal m13, decimal m14,
                         decimal m21, decimal m22, decimal m23, decimal m24,
                         decimal m31, decimal m32, decimal m33, decimal m34,
                         decimal m41, decimal m42, decimal m43, decimal m44)
        {
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M14 = m14;

            M21 = m21;
            M22 = m22;
            M23 = m23;
            M24 = m24;

            M31 = m31;
            M32 = m32;
            M33 = m33;
            M34 = m34;

            M41 = m41;
            M42 = m42;
            M43 = m43;
            M44 = m44;
        }

        /// <summary>
        /// Constructs a Matrix4x4 from the given Matrix3x2.
        /// </summary>
        /// <param name="value">The source Matrix3x2.</param>
        public Matrix4x4(Matrix3x2 value)
        {
            M11 = value.M11;
            M12 = value.M12;
            M13 = 0;
            M14 = 0;
            M21 = value.M21;
            M22 = value.M22;
            M23 = 0;
            M24 = 0;
            M31 = 0;
            M32 = 0;
            M33 = 1;
            M34 = 0;
            M41 = value.M31;
            M42 = value.M32;
            M43 = 0;
            M44 = 1;
        }

        /// <summary>
        /// Creates a spherical billboard that rotates around a specified object position.
        /// </summary>
        /// <param name="objectPosition">Position of the object the billboard will rotate around.</param>
        /// <param name="cameraPosition">Position of the camera.</param>
        /// <param name="cameraUpVector">The up vector of the camera.</param>
        /// <param name="cameraForwardVector">The forward vector of the camera.</param>
        /// <returns>The created billboard matrix</returns>
        public static Matrix4x4 CreateBillboard(Vector3 objectPosition, Vector3 cameraPosition, Vector3 cameraUpVector, Vector3 cameraForwardVector)
        {
            const decimal Epsilon = 1e-4m;

            var zaxis = new Vector3(
                objectPosition.X - cameraPosition.X,
                objectPosition.Y - cameraPosition.Y,
                objectPosition.Z - cameraPosition.Z);

            var norm = zaxis.LengthSquared();

            if (norm < Epsilon)
            {
                zaxis = -cameraForwardVector;
            }
            else
            {
                zaxis = Vector3.Multiply(zaxis, 1.0m / norm.Sqrt());
            }

            var xaxis = Vector3.Normalize(Vector3.Cross(cameraUpVector, zaxis));

            var yaxis = Vector3.Cross(zaxis, xaxis);

            return new Matrix4x4(
                xaxis.X,
                xaxis.Y,
                xaxis.Z,
                0.0m,
                yaxis.X,
                yaxis.Y,
                yaxis.Z,
                0.0m,
                zaxis.X,
                zaxis.Y,
                zaxis.Z,
                0.0m,

                objectPosition.X,
                objectPosition.Y,
                objectPosition.Z,
                1.0m);
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
        public static Matrix4x4 CreateConstrainedBillboard(Vector3 objectPosition, Vector3 cameraPosition, Vector3 rotateAxis, Vector3 cameraForwardVector, Vector3 objectForwardVector)
        {
            const decimal Epsilon = 1e-4m;
            const decimal MinAngle = 1.0m - (0.1m * DecimalConstants.PIOver180); // 0.1m degrees

            // Treat the case when object and camera positions are too close.
            var faceDir = new Vector3(
                objectPosition.X - cameraPosition.X,
                objectPosition.Y - cameraPosition.Y,
                objectPosition.Z - cameraPosition.Z);

            var norm = faceDir.LengthSquared();

            if (norm < Epsilon)
            {
                faceDir = -cameraForwardVector;
            }
            else
            {
                faceDir = Vector3.Multiply(faceDir, 1.0m / norm.Sqrt());
            }

            var yaxis = rotateAxis;
            Vector3 xaxis;
            Vector3 zaxis;

            // Treat the case when angle between faceDir and rotateAxis is too close to 0.
            var dot = Vector3.Dot(rotateAxis, faceDir);

            if (Math.Abs(dot) > MinAngle)
            {
                zaxis = objectForwardVector;

                // Make sure passed values are useful for compute.
                dot = Vector3.Dot(rotateAxis, zaxis);

                if (Math.Abs(dot) > MinAngle)
                {
                    zaxis = (Math.Abs(rotateAxis.Z) > MinAngle) ? new Vector3(1, 0, 0) : new Vector3(0, 0, -1);
                }

                xaxis = Vector3.Normalize(Vector3.Cross(rotateAxis, zaxis));
                zaxis = Vector3.Normalize(Vector3.Cross(xaxis, rotateAxis));
            }
            else
            {
                xaxis = Vector3.Normalize(Vector3.Cross(rotateAxis, faceDir));
                zaxis = Vector3.Normalize(Vector3.Cross(xaxis, yaxis));
            }

            return new Matrix4x4(
                xaxis.X,
                xaxis.Y,
                xaxis.Z,
                0.0m,
                yaxis.X,
                yaxis.Y,
                yaxis.Z,
                0.0m,
                zaxis.X,
                zaxis.Y,
                zaxis.Z,
                0.0m,

                objectPosition.X,
                objectPosition.Y,
                objectPosition.Z,
                1.0m);
        }

        /// <summary>
        /// Creates a translation matrix.
        /// </summary>
        /// <param name="position">The amount to translate in each axis.</param>
        /// <returns>The translation matrix.</returns>
        public static Matrix4x4 CreateTranslation(Vector3 position) => new(
            1.0m,
            0.0m,
            0.0m,
            0.0m,
            0.0m,
            1.0m,
            0.0m,
            0.0m,
            0.0m,
            0.0m,
            1.0m,
            0.0m,

            position.X,
            position.Y,
            position.Z,
            1.0m);

        /// <summary>
        /// Creates a translation matrix.
        /// </summary>
        /// <param name="xPosition">The amount to translate on the X-axis.</param>
        /// <param name="yPosition">The amount to translate on the Y-axis.</param>
        /// <param name="zPosition">The amount to translate on the Z-axis.</param>
        /// <returns>The translation matrix.</returns>
        public static Matrix4x4 CreateTranslation(decimal xPosition, decimal yPosition, decimal zPosition) => new(
            1.0m,
            0.0m,
            0.0m,
            0.0m,
            0.0m,
            1.0m,
            0.0m,
            0.0m,
            0.0m,
            0.0m,
            1.0m,
            0.0m,

            xPosition,
            yPosition,
            zPosition,
            1.0m);

        /// <summary>
        /// Creates a scaling matrix.
        /// </summary>
        /// <param name="xScale">Value to scale by on the X-axis.</param>
        /// <param name="yScale">Value to scale by on the Y-axis.</param>
        /// <param name="zScale">Value to scale by on the Z-axis.</param>
        /// <returns>The scaling matrix.</returns>
        public static Matrix4x4 CreateScale(decimal xScale, decimal yScale, decimal zScale) => new(
            xScale,
            0.0m,
            0.0m,
            0.0m,
            0.0m,
            yScale,
            0.0m,
            0.0m,
            0.0m,
            0.0m,
            zScale,
            0.0m,
            0.0m,
            0.0m,
            0.0m,
            1.0m);

        /// <summary>
        /// Creates a scaling matrix with a center point.
        /// </summary>
        /// <param name="xScale">Value to scale by on the X-axis.</param>
        /// <param name="yScale">Value to scale by on the Y-axis.</param>
        /// <param name="zScale">Value to scale by on the Z-axis.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>The scaling matrix.</returns>
        public static Matrix4x4 CreateScale(decimal xScale, decimal yScale, decimal zScale, Vector3 centerPoint)
        {
            var tx = centerPoint.X * (1 - xScale);
            var ty = centerPoint.Y * (1 - yScale);
            var tz = centerPoint.Z * (1 - zScale);

            return new Matrix4x4(
                xScale,
                0.0m,
                0.0m,
                0.0m,
                0.0m,
                yScale,
                0.0m,
                0.0m,
                0.0m,
                0.0m,
                zScale,
                0.0m,
                tx,
                ty,
                tz,
                1.0m);
        }

        /// <summary>
        /// Creates a scaling matrix.
        /// </summary>
        /// <param name="scales">The vector containing the amount to scale by on each axis.</param>
        /// <returns>The scaling matrix.</returns>
        public static Matrix4x4 CreateScale(Vector3 scales) => new(
            scales.X,
            0.0m,
            0.0m,
            0.0m,
            0.0m,
            scales.Y,
            0.0m,
            0.0m,
            0.0m,
            0.0m,
            scales.Z,
            0.0m,
            0.0m,
            0.0m,
            0.0m,
            1.0m);

        /// <summary>
        /// Creates a scaling matrix with a center point.
        /// </summary>
        /// <param name="scales">The vector containing the amount to scale by on each axis.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>The scaling matrix.</returns>
        public static Matrix4x4 CreateScale(Vector3 scales, Vector3 centerPoint)
        {
            var tx = centerPoint.X * (1 - scales.X);
            var ty = centerPoint.Y * (1 - scales.Y);
            var tz = centerPoint.Z * (1 - scales.Z);

            return new Matrix4x4(
                scales.X,
                0.0m,
                0.0m,
                0.0m,
                0.0m,
                scales.Y,
                0.0m,
                0.0m,
                0.0m,
                0.0m,
                scales.Z,
                0.0m,
                tx,
                ty,
                tz,
                1.0m);
        }

        /// <summary>
        /// Creates a uniform scaling matrix that scales equally on each axis.
        /// </summary>
        /// <param name="scale">The uniform scaling factor.</param>
        /// <returns>The scaling matrix.</returns>
        public static Matrix4x4 CreateScale(decimal scale) => new(
            scale,
            0.0m,
            0.0m,
            0.0m,
            0.0m,
            scale,
            0.0m,
            0.0m,
            0.0m,
            0.0m,
            scale,
            0.0m,
            0.0m,
            0.0m,
            0.0m,
            1.0m);

        /// <summary>
        /// Creates a uniform scaling matrix that scales equally on each axis with a center point.
        /// </summary>
        /// <param name="scale">The uniform scaling factor.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>The scaling matrix.</returns>
        public static Matrix4x4 CreateScale(decimal scale, Vector3 centerPoint)
        {
            var tx = centerPoint.X * (1 - scale);
            var ty = centerPoint.Y * (1 - scale);
            var tz = centerPoint.Z * (1 - scale);

            return new Matrix4x4(
                scale,
                0.0m,
                0.0m,
                0.0m,
                0.0m,
                scale,
                0.0m,
                0.0m,
                0.0m,
                0.0m,
                scale,
                0.0m,
                tx,
                ty,
                tz,
                1.0m);
        }

        /// <summary>
        /// Creates a matrix for rotating points around the X-axis.
        /// </summary>
        /// <param name="radians">The amount, in radians, by which to rotate around the X-axis.</param>
        /// <returns>The rotation matrix.</returns>
        public static Matrix4x4 CreateRotationX(decimal radians)
        {
            var c = (decimal)Math.Cos((double)radians);
            var s = (decimal)Math.Sin((double)radians);

            return new Matrix4x4(
                1.0m,
                0.0m,
                0.0m,
                0.0m,
                0.0m,
                c,
                s,
                0.0m,
                0.0m,
                -s,
                c,
                0.0m,
                0.0m,
                0.0m,
                0.0m,
                1.0m);
        }

        /// <summary>
        /// Creates a matrix for rotating points around the X-axis, from a center point.
        /// </summary>
        /// <param name="radians">The amount, in radians, by which to rotate around the X-axis.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>The rotation matrix.</returns>
        public static Matrix4x4 CreateRotationX(decimal radians, Vector3 centerPoint)
        {
            var c = (decimal)Math.Cos((double)radians);
            var s = (decimal)Math.Sin((double)radians);

            var y = (centerPoint.Y * (1 - c)) + (centerPoint.Z * s);
            var z = (centerPoint.Z * (1 - c)) - (centerPoint.Y * s);

            return new Matrix4x4(
                1.0m,
                0.0m,
                0.0m,
                0.0m,
                0.0m,
                c,
                s,
                0.0m,
                0.0m,
                -s,
                c,
                0.0m,
                0.0m,
                y,
                z,
                1.0m);
        }

        /// <summary>
        /// Creates a matrix for rotating points around the Y-axis.
        /// </summary>
        /// <param name="radians">The amount, in radians, by which to rotate around the Y-axis.</param>
        /// <returns>The rotation matrix.</returns>
        public static Matrix4x4 CreateRotationY(decimal radians)
        {
            var c = (decimal)Math.Cos((double)radians);
            var s = (decimal)Math.Sin((double)radians);

            return new Matrix4x4(
                c,
                0.0m,
                -s,
                0.0m,
                0.0m,
                1.0m,
                0.0m,
                0.0m,
                s,
                0.0m,
                c,
                0.0m,
                0.0m,
                0.0m,
                0.0m,
                1.0m);
        }

        /// <summary>
        /// Creates a matrix for rotating points around the Y-axis, from a center point.
        /// </summary>
        /// <param name="radians">The amount, in radians, by which to rotate around the Y-axis.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>The rotation matrix.</returns>
        public static Matrix4x4 CreateRotationY(decimal radians, Vector3 centerPoint)
        {
            var c = (decimal)Math.Cos((double)radians);
            var s = (decimal)Math.Sin((double)radians);

            var x = (centerPoint.X * (1 - c)) - (centerPoint.Z * s);
            var z = (centerPoint.Z * (1 - c)) + (centerPoint.X * s);

            return new Matrix4x4(
                c,
                0.0m,
                -s,
                0.0m,
                0.0m,
                1.0m,
                0.0m,
                0.0m,
                s,
                0.0m,
                c,
                0.0m,
                x,
                0.0m,
                z,
                1.0m);
        }

        /// <summary>
        /// Creates a matrix for rotating points around the Z-axis.
        /// </summary>
        /// <param name="radians">The amount, in radians, by which to rotate around the Z-axis.</param>
        /// <returns>The rotation matrix.</returns>
        public static Matrix4x4 CreateRotationZ(decimal radians)
        {
            var c = (decimal)Math.Cos((double)radians);
            var s = (decimal)Math.Sin((double)radians);

            return new Matrix4x4(
                c,
                s,
                0.0m,
                0.0m,
                -s,
                c,
                0.0m,
                0.0m,
                0.0m,
                0.0m,
                1.0m,
                0.0m,
                0.0m,
                0.0m,
                0.0m,
                1.0m);
        }

        /// <summary>
        /// Creates a matrix for rotating points around the Z-axis, from a center point.
        /// </summary>
        /// <param name="radians">The amount, in radians, by which to rotate around the Z-axis.</param>
        /// <param name="centerPoint">The center point.</param>
        /// <returns>The rotation matrix.</returns>
        public static Matrix4x4 CreateRotationZ(decimal radians, Vector3 centerPoint)
        {
            var c = (decimal)Math.Cos((double)radians);
            var s = (decimal)Math.Sin((double)radians);

            var x = (centerPoint.X * (1 - c)) + (centerPoint.Y * s);
            var y = (centerPoint.Y * (1 - c)) - (centerPoint.X * s);

            return new Matrix4x4(
                c,
                s,
                0.0m,
                0.0m,
                -s,
                c,
                0.0m,
                0.0m,
                0.0m,
                0.0m,
                1.0m,
                0.0m,
                x,
                y,
                0.0m,
                1.0m);
        }

        /// <summary>
        /// Creates a matrix that rotates around an arbitrary vector.
        /// </summary>
        /// <param name="axis">The axis to rotate around.</param>
        /// <param name="angle">The angle to rotate around the given axis, in radians.</param>
        /// <returns>The rotation matrix.</returns>
        public static Matrix4x4 CreateFromAxisAngle(Vector3 axis, decimal angle)
        {
            decimal x = axis.X, y = axis.Y, z = axis.Z;
            decimal sa = (decimal)Math.Sin((double)angle), ca = (decimal)Math.Cos((double)angle);
            decimal xx = x * x, yy = y * y, zz = z * z;
            decimal xy = x * y, xz = x * z, yz = y * z;

            return new Matrix4x4(
                xx + (ca * (1.0m - xx)),
                xy - (ca * xy) + (sa * z),
                xz - (ca * xz) - (sa * y),
                0.0m,
                xy - (ca * xy) - (sa * z),
                yy + (ca * (1.0m - yy)),
                yz - (ca * yz) + (sa * x),
                0.0m,
                xz - (ca * xz) + (sa * y),
                yz - (ca * yz) - (sa * x),
                zz + (ca * (1.0m - zz)),
                0.0m,
                0.0m,
                0.0m,
                0.0m,
                1.0m);
        }

        /// <summary>
        /// Creates a perspective projection matrix based on a field of view, aspect ratio, and near and far view plane distances.
        /// </summary>
        /// <param name="fieldOfView">Field of view in the y direction, in radians.</param>
        /// <param name="aspectRatio">Aspect ratio, defined as view space width divided by height.</param>
        /// <param name="nearPlaneDistance">Distance to the near view plane.</param>
        /// <param name="farPlaneDistance">Distance to the far view plane.</param>
        /// <returns>The perspective projection matrix.</returns>
        public static Matrix4x4 CreatePerspectiveFieldOfView(decimal fieldOfView, decimal aspectRatio, decimal nearPlaneDistance, decimal farPlaneDistance)
        {
            if (fieldOfView is <= 0.0m or >= DecimalConstants.PI)
            {
                throw new ArgumentOutOfRangeException(nameof(fieldOfView));
            }

            if (nearPlaneDistance <= 0.0m)
            {
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
            }

            if (farPlaneDistance <= 0.0m)
            {
                throw new ArgumentOutOfRangeException(nameof(farPlaneDistance));
            }

            if (nearPlaneDistance >= farPlaneDistance)
            {
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
            }

            var yScale = 1.0m / (decimal)Math.Tan((double)(fieldOfView * 0.5m));
            var xScale = yScale / aspectRatio;

            var negFarRange = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);

            return new Matrix4x4(
                xScale,
                0.0m,
                0.0m,
                0.0m,

                yScale,
                0.0m,
                0.0m,
                0.0m,

                0.0m,
                0.0m,
                negFarRange,
                -1.0m,

                0.0m,
                0.0m,
                0.0m,
                nearPlaneDistance * negFarRange);
        }

        /// <summary>
        /// Creates a perspective projection matrix from the given view volume dimensions.
        /// </summary>
        /// <param name="width">Width of the view volume at the near view plane.</param>
        /// <param name="height">Height of the view volume at the near view plane.</param>
        /// <param name="nearPlaneDistance">Distance to the near view plane.</param>
        /// <param name="farPlaneDistance">Distance to the far view plane.</param>
        /// <returns>The perspective projection matrix.</returns>
        public static Matrix4x4 CreatePerspective(decimal width, decimal height, decimal nearPlaneDistance, decimal farPlaneDistance)
        {
            if (nearPlaneDistance <= 0.0m)
            {
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
            }

            if (farPlaneDistance <= 0.0m)
            {
                throw new ArgumentOutOfRangeException(nameof(farPlaneDistance));
            }

            if (nearPlaneDistance >= farPlaneDistance)
            {
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
            }

            var negFarRange = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);

            return new Matrix4x4(
                2.0m * nearPlaneDistance / width,
                0.0m,
                0.0m,
                0.0m,

                2.0m * nearPlaneDistance / height,
                0.0m,
                0.0m,
                0.0m,

                negFarRange,
                0.0m,
                0.0m,
                -1.0m,

                0.0m,
                0.0m,
                0.0m,
                nearPlaneDistance * negFarRange);
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
        public static Matrix4x4 CreatePerspectiveOffCenter(decimal left, decimal right, decimal bottom, decimal top, decimal nearPlaneDistance, decimal farPlaneDistance)
        {
            if (nearPlaneDistance <= 0.0m)
            {
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
            }

            if (farPlaneDistance <= 0.0m)
            {
                throw new ArgumentOutOfRangeException(nameof(farPlaneDistance));
            }

            if (nearPlaneDistance >= farPlaneDistance)
            {
                throw new ArgumentOutOfRangeException(nameof(nearPlaneDistance));
            }

            var negFarRange = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);

            return new Matrix4x4(
                2.0m * nearPlaneDistance / (right - left),
                0.0m,
                0.0m,
                0.0m,

                2.0m * nearPlaneDistance / (top - bottom),
                0.0m,
                0.0m,
                0.0m,

                (left + right) / (right - left),
                (top + bottom) / (top - bottom),
                negFarRange,
                -1.0m,

                nearPlaneDistance * negFarRange,
                0.0m,
                0.0m,
                0.0m);
        }

        /// <summary>
        /// Creates an orthographic perspective matrix from the given view volume dimensions.
        /// </summary>
        /// <param name="width">Width of the view volume.</param>
        /// <param name="height">Height of the view volume.</param>
        /// <param name="zNearPlane">Minimum Z-value of the view volume.</param>
        /// <param name="zFarPlane">Maximum Z-value of the view volume.</param>
        /// <returns>The orthographic projection matrix.</returns>
        public static Matrix4x4 CreateOrthographic(decimal width, decimal height, decimal zNearPlane, decimal zFarPlane) => new(
            2.0m / width,
            0.0m,
            0.0m,
            0.0m,

            2.0m / height,
            0.0m,
            0.0m,
            0.0m,

            1.0m / (zNearPlane - zFarPlane),
            0.0m,
            0.0m,
            0.0m,

            0.0m,
            0.0m,
            zNearPlane / (zNearPlane - zFarPlane),
            1.0m);

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
        public static Matrix4x4 CreateOrthographicOffCenter(decimal left, decimal right, decimal bottom, decimal top, decimal zNearPlane, decimal zFarPlane) => new(
            2.0m / (right - left),
            0.0m,
            0.0m,
            0.0m,

            2.0m / (top - bottom),
            0.0m,
            0.0m,
            0.0m,

            1.0m / (zNearPlane - zFarPlane),
            0.0m,
            0.0m,
            0.0m,

            (left + right) / (left - right),
            (top + bottom) / (bottom - top),
            zNearPlane / (zNearPlane - zFarPlane),
            1.0m);

        /// <summary>
        /// Creates a view matrix.
        /// </summary>
        /// <param name="cameraPosition">The position of the camera.</param>
        /// <param name="cameraTarget">The target towards which the camera is pointing.</param>
        /// <param name="cameraUpVector">The direction that is "up" from the camera's point of view.</param>
        /// <returns>The view matrix.</returns>
        public static Matrix4x4 CreateLookAt(Vector3 cameraPosition, Vector3 cameraTarget, Vector3 cameraUpVector)
        {
            var zaxis = Vector3.Normalize(cameraPosition - cameraTarget);
            var xaxis = Vector3.Normalize(Vector3.Cross(cameraUpVector, zaxis));
            var yaxis = Vector3.Cross(zaxis, xaxis);

            return new Matrix4x4(
                xaxis.X,
                yaxis.X,
                zaxis.X,
                0.0m,
                xaxis.Y,
                yaxis.Y,
                zaxis.Y,
                0.0m,
                xaxis.Z,
                yaxis.Z,
                zaxis.Z,
                0.0m,
                -Vector3.Dot(xaxis, cameraPosition),
                -Vector3.Dot(yaxis, cameraPosition),
                -Vector3.Dot(zaxis, cameraPosition),
                1.0m);
        }

        /// <summary>
        /// Creates a world matrix with the specified parameters.
        /// </summary>
        /// <param name="position">The position of the object; used in translation operations.</param>
        /// <param name="forward">Forward direction of the object.</param>
        /// <param name="up">Upward direction of the object; usually [0, 1, 0].</param>
        /// <returns>The world matrix.</returns>
        public static Matrix4x4 CreateWorld(Vector3 position, Vector3 forward, Vector3 up)
        {
            var zaxis = Vector3.Normalize(-forward);
            var xaxis = Vector3.Normalize(Vector3.Cross(up, zaxis));
            var yaxis = Vector3.Cross(zaxis, xaxis);

            return new Matrix4x4(
                xaxis.X,
                xaxis.Y,
                xaxis.Z,
                0.0m,
                yaxis.X,
                yaxis.Y,
                yaxis.Z,
                0.0m,
                zaxis.X,
                zaxis.Y,
                zaxis.Z,
                0.0m,
                position.X,
                position.Y,
                position.Z,
                1.0m);
        }

        /// <summary>
        /// Creates a rotation matrix from the given Quaternion rotation value.
        /// </summary>
        /// <param name="quaternion">The source Quaternion.</param>
        /// <returns>The rotation matrix.</returns>
        public static Matrix4x4 CreateFromQuaternion(Quaternion quaternion)
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

            return new Matrix4x4(
                1.0m - (2.0m * (yy + zz)),
                2.0m * (xy + wz),
                2.0m * (xz - wy),
                0.0m,
                2.0m * (xy - wz),
                1.0m - (2.0m * (zz + xx)),
                2.0m * (yz + wx),
                0.0m,
                2.0m * (xz + wy),
                2.0m * (yz - wx),
                1.0m - (2.0m * (yy + xx)),
                0.0m,
                0.0m,
                0.0m,
                0.0m,
                1.0m);
        }

        /// <summary>
        /// Creates a rotation matrix from the specified yaw, pitch, and roll.
        /// </summary>
        /// <param name="yaw">Angle of rotation, in radians, around the Y-axis.</param>
        /// <param name="pitch">Angle of rotation, in radians, around the X-axis.</param>
        /// <param name="roll">Angle of rotation, in radians, around the Z-axis.</param>
        /// <returns>The rotation matrix.</returns>
        public static Matrix4x4 CreateFromYawPitchRoll(decimal yaw, decimal pitch, decimal roll)
            => CreateFromQuaternion(Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll));

        /// <summary>
        /// Creates a Matrix that flattens geometry into a specified Plane as if casting a shadow from a specified light source.
        /// </summary>
        /// <param name="lightDirection">The direction from which the light that will cast the shadow is coming.</param>
        /// <param name="plane">The Plane onto which the new matrix should flatten geometry so as to cast a shadow.</param>
        /// <returns>A new Matrix that can be used to flatten geometry onto the specified plane from the specified direction.</returns>
        public static Matrix4x4 CreateShadow(Vector3 lightDirection, Plane plane)
        {
            var p = Plane.Normalize(plane);

            var dot = (p.Normal.X * lightDirection.X) + (p.Normal.Y * lightDirection.Y) + (p.Normal.Z * lightDirection.Z);
            var a = -p.Normal.X;
            var b = -p.Normal.Y;
            var c = -p.Normal.Z;
            var d = -p.D;

            return new Matrix4x4(
                (a * lightDirection.X) + dot,
                b * lightDirection.X,
                c * lightDirection.X,
                d * lightDirection.X,

                a * lightDirection.Y,
                (b * lightDirection.Y) + dot,
                c * lightDirection.Y,
                d * lightDirection.Y,

                a * lightDirection.Z,
                b * lightDirection.Z,
                (c * lightDirection.Z) + dot,
                d * lightDirection.Z,

                0.0m,
                0.0m,
                0.0m,
                dot);
        }

        /// <summary>
        /// Creates a Matrix that reflects the coordinate system about a specified Plane.
        /// </summary>
        /// <param name="value">The Plane about which to create a reflection.</param>
        /// <returns>A new matrix expressing the reflection.</returns>
        public static Matrix4x4 CreateReflection(Plane value)
        {
            value = Plane.Normalize(value);

            var a = value.Normal.X;
            var b = value.Normal.Y;
            var c = value.Normal.Z;

            var fa = -2.0m * a;
            var fb = -2.0m * b;
            var fc = -2.0m * c;

            return new Matrix4x4(
                (fa * a) + 1.0m,
                fb * a,
                fc * a,
                0.0m,

                fa * b,
                (fb * b) + 1.0m,
                fc * b,
                0.0m,

                fa * c,
                fb * c,
                (fc * c) + 1.0m,
                0.0m,

                fa * value.D,
                fb * value.D,
                fc * value.D,
                1.0m);
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
        public static bool Decompose(Matrix4x4 matrix, out Vector3 scale, out Quaternion rotation, out Vector3 translation)
        {
            var result = true;

            const decimal EPSILON = 0.0001m;
            decimal det;

            var pCanonicalBasis = new decimal[3, 3]
            {
                { 1, 0, 0 },
                { 0, 1, 0 },
                { 0, 0, 1 },
            };

            var pVectorBasis = new Vector3[3]
            {
                new Vector3(matrix.M11, matrix.M12, matrix.M13),
                new Vector3(matrix.M21, matrix.M22, matrix.M23),
                new Vector3(matrix.M31, matrix.M32, matrix.M33),
            };

            var pfScales = new decimal[3]
            {
                pVectorBasis[0].Length(),
                pVectorBasis[1].Length(),
                pVectorBasis[2].Length(),
            };

            uint a, b, c;
            decimal x = pfScales[0], y = pfScales[1], z = pfScales[2];
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

            if (pfScales[a] < EPSILON)
            {
                pVectorBasis[a] = new Vector3(pCanonicalBasis[a, 0], pCanonicalBasis[a, 1], pCanonicalBasis[a, 2]);
            }

            pVectorBasis[a] = Vector3.Normalize(pVectorBasis[a]);
            pfScales[a] = 1;

            if (pfScales[b] < EPSILON)
            {
                uint cc;
                decimal fAbsX, fAbsY, fAbsZ;

                fAbsX = Math.Abs(pVectorBasis[a].X);
                fAbsY = Math.Abs(pVectorBasis[a].Y);
                fAbsZ = Math.Abs(pVectorBasis[a].Z);

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

                pVectorBasis[b] = Vector3.Cross(pVectorBasis[a], new Vector3(pCanonicalBasis[cc, 0], pCanonicalBasis[cc, 1], pCanonicalBasis[cc, 2]));
            }

            pVectorBasis[b] = Vector3.Normalize(pVectorBasis[b]);
            pfScales[b] = 1;

            if (pfScales[c] < EPSILON)
            {
                pVectorBasis[c] = Vector3.Cross(pVectorBasis[a], pVectorBasis[b]);
            }

            pVectorBasis[c] = Vector3.Normalize(pVectorBasis[c]);
            pfScales[c] = 1;

            var matTemp = new Matrix4x4(
                pVectorBasis[0].X, pVectorBasis[0].Y, pVectorBasis[0].Z, 0,
                pVectorBasis[1].X, pVectorBasis[1].Y, pVectorBasis[1].Z, 0,
                pVectorBasis[2].X, pVectorBasis[2].Y, pVectorBasis[2].Z, 0,
                0, 0, 0, 1);
            det = matTemp.GetDeterminant();

            // use Kramer's rule to check for handedness of coordinate system
            if (det < 0.0m)
            {
                // switch coordinate system by negating the scale and inverting the basis vector on the x-axis
                pfScales[a] = -pfScales[a];
                pVectorBasis[a] = -pVectorBasis[a];

                matTemp = new Matrix4x4(
                    pVectorBasis[0].X, pVectorBasis[0].Y, pVectorBasis[0].Z, 0,
                    pVectorBasis[1].X, pVectorBasis[1].Y, pVectorBasis[1].Z, 0,
                    pVectorBasis[2].X, pVectorBasis[2].Y, pVectorBasis[2].Z, 0,
                    0, 0, 0, 1);

                det = -det;
            }

            det--;
            det *= det;

            scale = new Vector3(pfScales[0], pfScales[1], pfScales[2]);

            translation = new Vector3(
                matrix.M41,
                matrix.M42,
                matrix.M43);

            if (EPSILON < det)
            {
                // Non-SRT matrix encountered
                rotation = Quaternion.Identity;
                result = false;
            }
            else
            {
                // generate the quaternion from the matrix
                rotation = Quaternion.CreateFromRotationMatrix(matTemp);
            }

            return result;
        }

        /// <summary>
        /// Attempts to calculate the inverse of the given matrix. If successful, result will contain the inverted matrix.
        /// </summary>
        /// <param name="matrix">The source matrix to invert.</param>
        /// <param name="result">If successful, contains the inverted matrix.</param>
        /// <returns>True if the source matrix could be inverted; False otherwise.</returns>
        public static bool Invert(Matrix4x4 matrix, out Matrix4x4 result)
        {
            decimal a = matrix.M11, b = matrix.M12, c = matrix.M13, d = matrix.M14;
            decimal e = matrix.M21, f = matrix.M22, g = matrix.M23, h = matrix.M24;
            decimal i = matrix.M31, j = matrix.M32, k = matrix.M33, l = matrix.M34;
            decimal m = matrix.M41, n = matrix.M42, o = matrix.M43, p = matrix.M44;

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

            if (Math.Abs(det) == 0)
            {
                result = Identity;
                return false;
            }

            var invDet = 1.0m / det;

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

            result = new Matrix4x4(
                a11 * invDet,
                a12 * invDet,
                a13 * invDet,
                a14 * invDet,

                -((b * kp_lo) - (c * jp_ln) + (d * jo_kn)) * invDet,
                +((a * kp_lo) - (c * ip_lm) + (d * io_km)) * invDet,
                -((a * jp_ln) - (b * ip_lm) + (d * in_jm)) * invDet,
                +((a * jo_kn) - (b * io_km) + (c * in_jm)) * invDet,

                +((b * gp_ho) - (c * fp_hn) + (d * fo_gn)) * invDet,
                -((a * gp_ho) - (c * ep_hm) + (d * eo_gm)) * invDet,
                +((a * fp_hn) - (b * ep_hm) + (d * en_fm)) * invDet,
                -((a * fo_gn) - (b * eo_gm) + (c * en_fm)) * invDet,

                -((b * gl_hk) - (c * fl_hj) + (d * fk_gj)) * invDet,
                +((a * gl_hk) - (c * el_hi) + (d * ek_gi)) * invDet,
                -((a * fl_hj) - (b * el_hi) + (d * ej_fi)) * invDet,
                +((a * fk_gj) - (b * ek_gi) + (c * ej_fi)) * invDet);

            return true;
        }

        /// <summary>
        /// Transforms the given matrix by applying the given Quaternion rotation.
        /// </summary>
        /// <param name="value">The source matrix to transform.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <returns>The transformed matrix.</returns>
        public static Matrix4x4 Transform(Matrix4x4 value, Quaternion rotation)
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

            var q11 = 1.0m - yy2 - zz2;
            var q21 = xy2 - wz2;
            var q31 = xz2 + wy2;

            var q12 = xy2 + wz2;
            var q22 = 1.0m - xx2 - zz2;
            var q32 = yz2 - wx2;

            var q13 = xz2 - wy2;
            var q23 = yz2 + wx2;
            var q33 = 1.0m - xx2 - yy2;

            return new Matrix4x4(
                // First row
                (value.M11 * q11) + (value.M12 * q21) + (value.M13 * q31),
                (value.M11 * q12) + (value.M12 * q22) + (value.M13 * q32),
                (value.M11 * q13) + (value.M12 * q23) + (value.M13 * q33),
                value.M14,

                // Second row
                (value.M21 * q11) + (value.M22 * q21) + (value.M23 * q31),
                (value.M21 * q12) + (value.M22 * q22) + (value.M23 * q32),
                (value.M21 * q13) + (value.M22 * q23) + (value.M23 * q33),
                value.M24,

                // Third row
                (value.M31 * q11) + (value.M32 * q21) + (value.M33 * q31),
                (value.M31 * q12) + (value.M32 * q22) + (value.M33 * q32),
                (value.M31 * q13) + (value.M32 * q23) + (value.M33 * q33),
                value.M34,

                // Fourth row
                (value.M41 * q11) + (value.M42 * q21) + (value.M43 * q31),
                (value.M41 * q12) + (value.M42 * q22) + (value.M43 * q32),
                (value.M41 * q13) + (value.M42 * q23) + (value.M43 * q33),
                value.M44);
        }

        /// <summary>
        /// Transposes the rows and columns of a matrix.
        /// </summary>
        /// <param name="matrix">The source matrix.</param>
        /// <returns>The transposed matrix.</returns>
        public static Matrix4x4 Transpose(Matrix4x4 matrix) => new(
            matrix.M11,
            matrix.M21,
            matrix.M31,
            matrix.M41,
            matrix.M12,
            matrix.M22,
            matrix.M32,
            matrix.M42,
            matrix.M13,
            matrix.M23,
            matrix.M33,
            matrix.M43,
            matrix.M14,
            matrix.M24,
            matrix.M34,
            matrix.M44);

        /// <summary>
        /// Linearly interpolates between the corresponding values of two matrices.
        /// </summary>
        /// <param name="matrix1">The first source matrix.</param>
        /// <param name="matrix2">The second source matrix.</param>
        /// <param name="amount">The relative weight of the second source matrix.</param>
        /// <returns>The interpolated matrix.</returns>
        public static Matrix4x4 Lerp(Matrix4x4 matrix1, Matrix4x4 matrix2, decimal amount) => new(
            // First row
            matrix1.M11 + ((matrix2.M11 - matrix1.M11) * amount),
            matrix1.M12 + ((matrix2.M12 - matrix1.M12) * amount),
            matrix1.M13 + ((matrix2.M13 - matrix1.M13) * amount),
            matrix1.M14 + ((matrix2.M14 - matrix1.M14) * amount),

            // Second row
            matrix1.M21 + ((matrix2.M21 - matrix1.M21) * amount),
            matrix1.M22 + ((matrix2.M22 - matrix1.M22) * amount),
            matrix1.M23 + ((matrix2.M23 - matrix1.M23) * amount),
            matrix1.M24 + ((matrix2.M24 - matrix1.M24) * amount),

            // Third row
            matrix1.M31 + ((matrix2.M31 - matrix1.M31) * amount),
            matrix1.M32 + ((matrix2.M32 - matrix1.M32) * amount),
            matrix1.M33 + ((matrix2.M33 - matrix1.M33) * amount),
            matrix1.M34 + ((matrix2.M34 - matrix1.M34) * amount),

            // Fourth row
            matrix1.M41 + ((matrix2.M41 - matrix1.M41) * amount),
            matrix1.M42 + ((matrix2.M42 - matrix1.M42) * amount),
            matrix1.M43 + ((matrix2.M43 - matrix1.M43) * amount),
            matrix1.M44 + ((matrix2.M44 - matrix1.M44) * amount));

        /// <summary>
        /// Attempts to extract the scale, translation, and rotation components from this
        /// scale/rotation/translation matrix. If successful, the out parameters will contained the
        /// extracted values.
        /// </summary>
        /// <param name="scale">The scaling component of the transformation matrix.</param>
        /// <param name="rotation">The rotation component of the transformation matrix.</param>
        /// <param name="translation">The translation component of the transformation matrix</param>
        /// <returns>True if this matrix was successfully decomposed; False otherwise.</returns>
        public bool Decompose(out Vector3 scale, out Quaternion rotation, out Vector3 translation)
            => Decompose(this, out scale, out rotation, out translation);

        /// <summary>
        /// Returns a boolean indicating whether the given Object is equal to this matrix instance.
        /// </summary>
        /// <param name="obj">The Object to compare against.</param>
        /// <returns>True if the Object is equal to this matrix; False otherwise.</returns>
        public override bool Equals(object? obj) => obj is Matrix4x4 other && Equals(other);

        /// <summary>
        /// Returns a boolean indicating whether this matrix instance is equal to the other given matrix.
        /// </summary>
        /// <param name="other">The matrix to compare this instance to.</param>
        /// <returns>True if the matrices are equal; False otherwise.</returns>
        /// <remarks>
        /// Checks diagonal element first for early out.
        /// </remarks>
        public bool Equals(Matrix4x4 other)
            => M11 == other.M11 && M22 == other.M22 && M33 == other.M33 && M44 == other.M44
            && M12 == other.M12 && M13 == other.M13 && M14 == other.M14 && M21 == other.M21
            && M23 == other.M23 && M24 == other.M24 && M31 == other.M31 && M32 == other.M32
            && M34 == other.M34 && M41 == other.M41 && M42 == other.M42 && M43 == other.M43;

        /// <summary>
        /// Calculates the determinant of the matrix.
        /// </summary>
        /// <returns>The determinant of the matrix.</returns>
        public decimal GetDeterminant()
        {
            decimal a = M11, b = M12, c = M13, d = M14;
            decimal e = M21, f = M22, g = M23, h = M24;
            decimal i = M31, j = M32, k = M33, l = M34;
            decimal m = M41, n = M42, o = M43, p = M44;

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
            unchecked
            {
                return M11.GetHashCode() + M12.GetHashCode() + M13.GetHashCode() + M14.GetHashCode()
                       + M21.GetHashCode() + M22.GetHashCode() + M23.GetHashCode() + M24.GetHashCode()
                       + M31.GetHashCode() + M32.GetHashCode() + M33.GetHashCode() + M34.GetHashCode()
                       + M41.GetHashCode() + M42.GetHashCode() + M43.GetHashCode() + M44.GetHashCode();
            }
        }

        /// <summary>
        /// Attempts to calculate the inverse of this matrix. If successful, result will contain the inverted matrix.
        /// </summary>
        /// <param name="result">If successful, contains the inverted matrix.</param>
        /// <returns>True if this matrix could be inverted; False otherwise.</returns>
        public bool Invert(out Matrix4x4 result) => Invert(this, out result);

        /// <summary>
        /// Returns a String representing this matrix instance.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            var ci = CultureInfo.CurrentCulture;

            return string.Format(ci, "{{ {{M11:{0} M12:{1} M13:{2} M14:{3}}} {{M21:{4} M22:{5} M23:{6} M24:{7}}} {{M31:{8} M32:{9} M33:{10} M34:{11}}} {{M41:{12} M42:{13} M43:{14} M44:{15}}} }}",
                                 M11.ToString(ci), M12.ToString(ci), M13.ToString(ci), M14.ToString(ci),
                                 M21.ToString(ci), M22.ToString(ci), M23.ToString(ci), M24.ToString(ci),
                                 M31.ToString(ci), M32.ToString(ci), M33.ToString(ci), M34.ToString(ci),
                                 M41.ToString(ci), M42.ToString(ci), M43.ToString(ci), M44.ToString(ci));
        }

        /// <summary>
        /// Transforms this matrix by applying the given Quaternion rotation.
        /// </summary>
        /// <param name="rotation">The rotation to apply.</param>
        /// <returns>The transformed matrix.</returns>
        public Matrix4x4 Transform(Quaternion rotation) => Transform(this, rotation);

        /// <summary>
        /// Transposes the rows and columns of this matrix.
        /// </summary>
        /// <returns>The transposed matrix.</returns>
        public Matrix4x4 Transpose() => Transpose(this);

        /// <summary>
        /// Returns a new matrix with the negated elements of the given matrix.
        /// </summary>
        /// <param name="value">The source matrix.</param>
        /// <returns>The negated matrix.</returns>
        public static Matrix4x4 Negate(Matrix4x4 value) => -value;

        /// <summary>
        /// Adds two matrices together.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The resulting matrix.</returns>
        public static Matrix4x4 Add(Matrix4x4 value1, Matrix4x4 value2) => value1 + value2;

        /// <summary>
        /// Subtracts the second matrix from the first.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Matrix4x4 Subtract(Matrix4x4 value1, Matrix4x4 value2) => value1 - value2;

        /// <summary>
        /// Multiplies a matrix by another matrix.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Matrix4x4 Multiply(Matrix4x4 value1, Matrix4x4 value2) => value1 * value2;

        /// <summary>
        /// Multiplies a matrix by a scalar value.
        /// </summary>
        /// <param name="value1">The source matrix.</param>
        /// <param name="value2">The scaling factor.</param>
        /// <returns>The scaled matrix.</returns>
        public static Matrix4x4 Multiply(Matrix4x4 value1, decimal value2) => value1 * value2;

        /// <summary>
        /// Returns a new matrix with the negated elements of the given matrix.
        /// </summary>
        /// <param name="value">The source matrix.</param>
        /// <returns>The negated matrix.</returns>
        public static Matrix4x4 operator -(Matrix4x4 value) => new(
            -value.M11,
            -value.M12,
            -value.M13,
            -value.M14,
            -value.M21,
            -value.M22,
            -value.M23,
            -value.M24,
            -value.M31,
            -value.M32,
            -value.M33,
            -value.M34,
            -value.M41,
            -value.M42,
            -value.M43,
            -value.M44);

        /// <summary>
        /// Adds two matrices together.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The resulting matrix.</returns>
        public static Matrix4x4 operator +(Matrix4x4 value1, Matrix4x4 value2) => new(
            value1.M11 + value2.M11,
            value1.M12 + value2.M12,
            value1.M13 + value2.M13,
            value1.M14 + value2.M14,
            value1.M21 + value2.M21,
            value1.M22 + value2.M22,
            value1.M23 + value2.M23,
            value1.M24 + value2.M24,
            value1.M31 + value2.M31,
            value1.M32 + value2.M32,
            value1.M33 + value2.M33,
            value1.M34 + value2.M34,
            value1.M41 + value2.M41,
            value1.M42 + value2.M42,
            value1.M43 + value2.M43,
            value1.M44 + value2.M44);

        /// <summary>
        /// Subtracts the second matrix from the first.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Matrix4x4 operator -(Matrix4x4 value1, Matrix4x4 value2) => new(
            value1.M11 - value2.M11,
            value1.M12 - value2.M12,
            value1.M13 - value2.M13,
            value1.M14 - value2.M14,
            value1.M21 - value2.M21,
            value1.M22 - value2.M22,
            value1.M23 - value2.M23,
            value1.M24 - value2.M24,
            value1.M31 - value2.M31,
            value1.M32 - value2.M32,
            value1.M33 - value2.M33,
            value1.M34 - value2.M34,
            value1.M41 - value2.M41,
            value1.M42 - value2.M42,
            value1.M43 - value2.M43,
            value1.M44 - value2.M44);

        /// <summary>
        /// Multiplies a matrix by another matrix.
        /// </summary>
        /// <param name="value1">The first source matrix.</param>
        /// <param name="value2">The second source matrix.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Matrix4x4 operator *(Matrix4x4 value1, Matrix4x4 value2) => new(
            // First row
            (value1.M11 * value2.M11) + (value1.M12 * value2.M21) + (value1.M13 * value2.M31) + (value1.M14 * value2.M41),
            (value1.M11 * value2.M12) + (value1.M12 * value2.M22) + (value1.M13 * value2.M32) + (value1.M14 * value2.M42),
            (value1.M11 * value2.M13) + (value1.M12 * value2.M23) + (value1.M13 * value2.M33) + (value1.M14 * value2.M43),
            (value1.M11 * value2.M14) + (value1.M12 * value2.M24) + (value1.M13 * value2.M34) + (value1.M14 * value2.M44),

            // Second row
            (value1.M21 * value2.M11) + (value1.M22 * value2.M21) + (value1.M23 * value2.M31) + (value1.M24 * value2.M41),
            (value1.M21 * value2.M12) + (value1.M22 * value2.M22) + (value1.M23 * value2.M32) + (value1.M24 * value2.M42),
            (value1.M21 * value2.M13) + (value1.M22 * value2.M23) + (value1.M23 * value2.M33) + (value1.M24 * value2.M43),
            (value1.M21 * value2.M14) + (value1.M22 * value2.M24) + (value1.M23 * value2.M34) + (value1.M24 * value2.M44),

            // Third row
            (value1.M31 * value2.M11) + (value1.M32 * value2.M21) + (value1.M33 * value2.M31) + (value1.M34 * value2.M41),
            (value1.M31 * value2.M12) + (value1.M32 * value2.M22) + (value1.M33 * value2.M32) + (value1.M34 * value2.M42),
            (value1.M31 * value2.M13) + (value1.M32 * value2.M23) + (value1.M33 * value2.M33) + (value1.M34 * value2.M43),
            (value1.M31 * value2.M14) + (value1.M32 * value2.M24) + (value1.M33 * value2.M34) + (value1.M34 * value2.M44),

            // Fourth row
            (value1.M41 * value2.M11) + (value1.M42 * value2.M21) + (value1.M43 * value2.M31) + (value1.M44 * value2.M41),
            (value1.M41 * value2.M12) + (value1.M42 * value2.M22) + (value1.M43 * value2.M32) + (value1.M44 * value2.M42),
            (value1.M41 * value2.M13) + (value1.M42 * value2.M23) + (value1.M43 * value2.M33) + (value1.M44 * value2.M43),
            (value1.M41 * value2.M14) + (value1.M42 * value2.M24) + (value1.M43 * value2.M34) + (value1.M44 * value2.M44));

        /// <summary>
        /// Multiplies a matrix by a scalar value.
        /// </summary>
        /// <param name="value1">The source matrix.</param>
        /// <param name="value2">The scaling factor.</param>
        /// <returns>The scaled matrix.</returns>
        public static Matrix4x4 operator *(Matrix4x4 value1, decimal value2) => new(
            value1.M11 * value2,
            value1.M12 * value2,
            value1.M13 * value2,
            value1.M14 * value2,
            value1.M21 * value2,
            value1.M22 * value2,
            value1.M23 * value2,
            value1.M24 * value2,
            value1.M31 * value2,
            value1.M32 * value2,
            value1.M33 * value2,
            value1.M34 * value2,
            value1.M41 * value2,
            value1.M42 * value2,
            value1.M43 * value2,
            value1.M44 * value2);

        /// <summary>
        /// Returns a boolean indicating whether the given two matrices are equal.
        /// </summary>
        /// <param name="value1">The first matrix to compare.</param>
        /// <param name="value2">The second matrix to compare.</param>
        /// <returns>True if the given matrices are equal; False otherwise.</returns>
        public static bool operator ==(Matrix4x4 value1, Matrix4x4 value2)
            => value1.Equals(value2);

        /// <summary>
        /// Returns a boolean indicating whether the given two matrices are not equal.
        /// </summary>
        /// <param name="value1">The first matrix to compare.</param>
        /// <param name="value2">The second matrix to compare.</param>
        /// <returns>True if the given matrices are not equal; False if they are equal.</returns>
        public static bool operator !=(Matrix4x4 value1, Matrix4x4 value2)
            => !value1.Equals(value2);

        /// <summary>
        /// Converts between implementations of matrices.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator Matrix4x4(System.Numerics.Matrix4x4 value)
            => new(
                (decimal)value.M11, (decimal)value.M12, (decimal)value.M13, (decimal)value.M14,
                (decimal)value.M21, (decimal)value.M22, (decimal)value.M23, (decimal)value.M24,
                (decimal)value.M31, (decimal)value.M32, (decimal)value.M33, (decimal)value.M34,
                (decimal)value.M41, (decimal)value.M42, (decimal)value.M43, (decimal)value.M44);

        /// <summary>
        /// Converts between implementations of matrices.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static explicit operator Matrix4x4(HugeNumbers.Matrix4x4 value)
            => new(
                (decimal)value.M11, (decimal)value.M12, (decimal)value.M13, (decimal)value.M14,
                (decimal)value.M21, (decimal)value.M22, (decimal)value.M23, (decimal)value.M24,
                (decimal)value.M31, (decimal)value.M32, (decimal)value.M33, (decimal)value.M34,
                (decimal)value.M41, (decimal)value.M42, (decimal)value.M43, (decimal)value.M44);

        /// <summary>
        /// Converts between implementations of matrices.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static explicit operator Matrix4x4(Doubles.Matrix4x4 value)
            => new(
                (decimal)value.M11, (decimal)value.M12, (decimal)value.M13, (decimal)value.M14,
                (decimal)value.M21, (decimal)value.M22, (decimal)value.M23, (decimal)value.M24,
                (decimal)value.M31, (decimal)value.M32, (decimal)value.M33, (decimal)value.M34,
                (decimal)value.M41, (decimal)value.M42, (decimal)value.M43, (decimal)value.M44);

        /// <summary>
        /// Converts between implementations of matrices.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static explicit operator System.Numerics.Matrix4x4(Matrix4x4 value)
            => new(
                (float)value.M11, (float)value.M12, (float)value.M13, (float)value.M14,
                (float)value.M21, (float)value.M22, (float)value.M23, (float)value.M24,
                (float)value.M31, (float)value.M32, (float)value.M33, (float)value.M34,
                (float)value.M41, (float)value.M42, (float)value.M43, (float)value.M44);
    }
}
