using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Tavenem.Mathematics.Doubles
{
    /// <summary>
    /// A structure encapsulating a four-dimensional vector (x,y,z,w), which is used to efficiently
    /// rotate an object about the (x,y,z) vector by the angle theta, where w = cos(theta/2).
    /// </summary>
    /// <remarks>
    /// Unlike the <see cref="System.Numerics.Quaternion"/> implementation, this is an immutable struct
    /// with read-only properties, rather than writable fields.
    /// </remarks>
    [Serializable]
    [DataContract]
    public struct Quaternion : IEquatable<Quaternion>
    {
        /// <summary>
        /// Returns a Quaternion representing no rotation.
        /// </summary>
        public static Quaternion Identity => new(0, 0, 0, 1);

        /// <summary>
        /// Specifies the X-value of the vector component of the Quaternion.
        /// </summary>
        [DataMember(Order = 1)]
        public double X { get; }

        /// <summary>
        /// Specifies the Y-value of the vector component of the Quaternion.
        /// </summary>
        [DataMember(Order = 2)]
        public double Y { get; }

        /// <summary>
        /// Specifies the Z-value of the vector component of the Quaternion.
        /// </summary>
        [DataMember(Order = 3)]
        public double Z { get; }

        /// <summary>
        /// Specifies the rotation component of the Quaternion.
        /// </summary>
        [DataMember(Order = 4)]
        public double W { get; }

        /// <summary>
        /// Returns whether the Quaternion is the identity Quaternion.
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public bool IsIdentity => Equals(Identity);

        /// <summary>
        /// Constructs a Quaternion from the given components.
        /// </summary>
        /// <param name="x">The X component of the Quaternion.</param>
        /// <param name="y">The Y component of the Quaternion.</param>
        /// <param name="z">The Z component of the Quaternion.</param>
        /// <param name="w">The W component of the Quaternion.</param>
        [System.Text.Json.Serialization.JsonConstructor]
        public Quaternion(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Constructs a Quaternion from the given vector and rotation parts.
        /// </summary>
        /// <param name="vectorPart">The vector part of the Quaternion.</param>
        /// <param name="scalarPart">The rotation part of the Quaternion.</param>
        public Quaternion(Vector3 vectorPart, double scalarPart)
        {
            X = vectorPart.X;
            Y = vectorPart.Y;
            Z = vectorPart.Z;
            W = scalarPart;
        }

        /// <summary>
        /// Concatenates two Quaternions; the result represents the value1 rotation followed by the value2 rotation.
        /// </summary>
        /// <param name="value1">The first Quaternion rotation in the series.</param>
        /// <param name="value2">The second Quaternion rotation in the series.</param>
        /// <returns>A new Quaternion representing the concatenation of the value1 rotation followed by the value2 rotation.</returns>
        public static Quaternion Concatenate(Quaternion value1, Quaternion value2)
        {
            // Concatenate rotation is actually q2 * q1 instead of q1 * q2.
            // So that's why value2 goes q1 and value1 goes q2.
            var q1x = value2.X;
            var q1y = value2.Y;
            var q1z = value2.Z;
            var q1w = value2.W;

            var q2x = value1.X;
            var q2y = value1.Y;
            var q2z = value1.Z;
            var q2w = value1.W;

            // cross(av, bv)
            var cx = (q1y * q2z) - (q1z * q2y);
            var cy = (q1z * q2x) - (q1x * q2z);
            var cz = (q1x * q2y) - (q1y * q2x);

            var dot = (q1x * q2x) + (q1y * q2y) + (q1z * q2z);

            return new Quaternion(
                (q1x * q2w) + (q2x * q1w) + cx,
                (q1y * q2w) + (q2y * q1w) + cy,
                (q1z * q2w) + (q2z * q1w) + cz,
                (q1w * q2w) - dot);
        }

        /// <summary>
        /// Creates the conjugate of a specified Quaternion.
        /// </summary>
        /// <param name="value">The Quaternion of which to return the conjugate.</param>
        /// <returns>A new Quaternion that is the conjugate of the specified one.</returns>
        public static Quaternion Conjugate(Quaternion value) => new(
            -value.X,
            -value.Y,
            -value.Z,
            value.W);

        /// <summary>
        /// Creates a Quaternion from a normalized vector axis and an angle to rotate about the vector.
        /// </summary>
        /// <param name="axis">The unit vector to rotate around.
        /// This vector must be normalized before calling this function or the resulting Quaternion will be incorrect.</param>
        /// <param name="angle">The angle, in radians, to rotate around the vector.</param>
        /// <returns>The created Quaternion.</returns>
        public static Quaternion CreateFromAxisAngle(Vector3 axis, double angle)
        {
            var halfAngle = angle * 0.5;
            var s = Math.Sin(halfAngle);
            var c = Math.Cos(halfAngle);

            return new Quaternion(
                axis.X * s,
                axis.Y * s,
                axis.Z * s,
                c);
        }

        /// <summary>
        /// Creates a new Quaternion from the given yaw, pitch, and roll, in radians.
        /// </summary>
        /// <param name="yaw">The yaw angle, in radians, around the Y-axis.</param>
        /// <param name="pitch">The pitch angle, in radians, around the X-axis.</param>
        /// <param name="roll">The roll angle, in radians, around the Z-axis.</param>
        /// <returns></returns>
        public static Quaternion CreateFromYawPitchRoll(double yaw, double pitch, double roll)
        {
            double sr, cr, sp, cp, sy, cy;

            var halfRoll = roll * 0.5;
            sr = Math.Sin(halfRoll);
            cr = Math.Cos(halfRoll);

            var halfPitch = pitch * 0.5;
            sp = Math.Sin(halfPitch);
            cp = Math.Cos(halfPitch);

            var halfYaw = yaw * 0.5;
            sy = Math.Sin(halfYaw);
            cy = Math.Cos(halfYaw);

            return new Quaternion(
                (cy * sp * cr) + (sy * cp * sr),
                (sy * cp * cr) - (cy * sp * sr),
                (cy * cp * sr) - (sy * sp * cr),
                (cy * cp * cr) + (sy * sp * sr));
        }

        /// <summary>
        /// Creates a Quaternion from the given rotation matrix.
        /// </summary>
        /// <param name="matrix">The rotation matrix.</param>
        /// <returns>The created Quaternion.</returns>
        public static Quaternion CreateFromRotationMatrix(Matrix4x4 matrix)
        {
            var trace = matrix.M11 + matrix.M22 + matrix.M33;

            if (trace > 0.0)
            {
                var s = Math.Sqrt(trace + 1.0);
                var invS = 0.5 / s;
                return new Quaternion(
                    s * 0.5,
                    (matrix.M23 - matrix.M32) * invS,
                    (matrix.M31 - matrix.M13) * invS,
                    (matrix.M12 - matrix.M21) * invS);
            }
            else if (matrix.M11 >= matrix.M22 && matrix.M11 >= matrix.M33)
            {
                var s = Math.Sqrt(1.0 + matrix.M11 - matrix.M22 - matrix.M33);
                var invS = 0.5 / s;
                return new Quaternion(
                    0.5 * s,
                    (matrix.M12 + matrix.M21) * invS,
                    (matrix.M13 + matrix.M31) * invS,
                    (matrix.M23 - matrix.M32) * invS);
            }
            else if (matrix.M22 > matrix.M33)
            {
                var s = Math.Sqrt(1.0 + matrix.M22 - matrix.M11 - matrix.M33);
                var invS = 0.5 / s;
                return new Quaternion(
                    (matrix.M21 + matrix.M12) * invS,
                    0.5 * s,
                    (matrix.M32 + matrix.M23) * invS,
                    (matrix.M31 - matrix.M13) * invS);
            }
            else
            {
                var s = Math.Sqrt(1.0 + matrix.M33 - matrix.M11 - matrix.M22);
                var invS = 0.5 / s;
                return new Quaternion(
                    (matrix.M31 + matrix.M13) * invS,
                    (matrix.M32 + matrix.M23) * invS,
                    0.5 * s,
                    (matrix.M12 - matrix.M21) * invS);
            }
        }

        /// <summary>
        /// Calculates the dot product of two Quaternions.
        /// </summary>
        /// <param name="quaternion1">The first source Quaternion.</param>
        /// <param name="quaternion2">The second source Quaternion.</param>
        /// <returns>The dot product of the Quaternions.</returns>
        public static double Dot(Quaternion quaternion1, Quaternion quaternion2)
            => (quaternion1.X * quaternion2.X)
            + (quaternion1.Y * quaternion2.Y)
            + (quaternion1.Z * quaternion2.Z)
            + (quaternion1.W * quaternion2.W);

        /// <summary>
        /// Returns the inverse of a Quaternion.
        /// </summary>
        /// <param name="value">The source Quaternion.</param>
        /// <returns>The inverted Quaternion.</returns>
        public static Quaternion Inverse(Quaternion value)
        {
            var ls = (value.X * value.X) + (value.Y * value.Y) + (value.Z * value.Z) + (value.W * value.W);
            var invNorm = 1.0 / ls;

            return new Quaternion(
                -value.X * invNorm,
                -value.Y * invNorm,
                -value.Z * invNorm,
                value.W * invNorm);
        }

        /// <summary>
        ///  Linearly interpolates between two quaternions.
        /// </summary>
        /// <param name="quaternion1">The first source Quaternion.</param>
        /// <param name="quaternion2">The second source Quaternion.</param>
        /// <param name="amount">The relative weight of the second source Quaternion in the interpolation.</param>
        /// <returns>The interpolated Quaternion.</returns>
        public static Quaternion Lerp(Quaternion quaternion1, Quaternion quaternion2, double amount)
        {
            var t = amount;
            var t1 = 1.0 - t;

            Quaternion r;

            var dot = (quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)
                        + (quaternion1.Z * quaternion2.Z) + (quaternion1.W * quaternion2.W);

            if (dot >= 0.0)
            {
                r = new Quaternion(
                    (t1 * quaternion1.X) + (t * quaternion2.X),
                    (t1 * quaternion1.Y) + (t * quaternion2.Y),
                    (t1 * quaternion1.Z) + (t * quaternion2.Z),
                    (t1 * quaternion1.W) + (t * quaternion2.W));
            }
            else
            {
                r = new Quaternion(
                    (t1 * quaternion1.X) - (t * quaternion2.X),
                    (t1 * quaternion1.Y) - (t * quaternion2.Y),
                    (t1 * quaternion1.Z) - (t * quaternion2.Z),
                    (t1 * quaternion1.W) - (t * quaternion2.W));
            }

            // Normalize it.
            var ls = (r.X * r.X) + (r.Y * r.Y) + (r.Z * r.Z) + (r.W * r.W);
            var invNorm = 1.0 / Math.Sqrt(ls);

            return r * invNorm;
        }

        /// <summary>
        /// Divides each component of the Quaternion by the length of the Quaternion.
        /// </summary>
        /// <param name="value">The source Quaternion.</param>
        /// <returns>The normalized Quaternion.</returns>
        public static Quaternion Normalize(Quaternion value)
        {
            var ls = (value.X * value.X) + (value.Y * value.Y) + (value.Z * value.Z) + (value.W * value.W);

            var invNorm = 1.0 / Math.Sqrt(ls);

            return new Quaternion(
                value.X * invNorm,
                value.Y * invNorm,
                value.Z * invNorm,
                value.W * invNorm);
        }

        /// <summary>
        /// Interpolates between two quaternions, using spherical linear interpolation.
        /// </summary>
        /// <param name="quaternion1">The first source Quaternion.</param>
        /// <param name="quaternion2">The second source Quaternion.</param>
        /// <param name="amount">The relative weight of the second source Quaternion in the interpolation.</param>
        /// <returns>The interpolated Quaternion.</returns>
        public static Quaternion Slerp(Quaternion quaternion1, Quaternion quaternion2, double amount)
        {
            const double Epsilon = 1e-6;

            var t = amount;

            var cosOmega = (quaternion1.X * quaternion2.X) + (quaternion1.Y * quaternion2.Y)
                             + (quaternion1.Z * quaternion2.Z) + (quaternion1.W * quaternion2.W);

            var flip = false;

            if (cosOmega < 0.0)
            {
                flip = true;
                cosOmega = -cosOmega;
            }

            double s1, s2;

            if (cosOmega > (1.0 - Epsilon))
            {
                // Too close, do straight linear interpolation.
                s1 = 1.0 - t;
                s2 = flip ? -t : t;
            }
            else
            {
                var omega = Math.Acos(cosOmega);
                var invSinOmega = 1 / Math.Sin(omega);

                s1 = Math.Sin((1.0 - t) * omega) * invSinOmega;
                s2 = flip
                    ? -Math.Sin(t * omega) * invSinOmega
                    : Math.Sin(t * omega) * invSinOmega;
            }

            return new Quaternion(
                (s1 * quaternion1.X) + (s2 * quaternion2.X),
                (s1 * quaternion1.Y) + (s2 * quaternion2.Y),
                (s1 * quaternion1.Z) + (s2 * quaternion2.Z),
                (s1 * quaternion1.W) + (s2 * quaternion2.W));
        }

        /// <summary>
        /// Concatenates two Quaternions; the result represents this rotation followed by the other rotation.
        /// </summary>
        /// <param name="other">The second Quaternion rotation in the series.</param>
        /// <returns>A new Quaternion representing the concatenation of this rotation followed by the other rotation.</returns>
        public Quaternion Concatenate(Quaternion other) => Concatenate(this, other);

        /// <summary>
        /// Creates the conjugate of this Quaternion.
        /// </summary>
        /// <returns>A new Quaternion that is the conjugate of this one.</returns>
        public Quaternion Conjugate() => Conjugate(this);

        /// <summary>
        /// Returns a boolean indicating whether the given Object is equal to this Quaternion instance.
        /// </summary>
        /// <param name="obj">The Object to compare against.</param>
        /// <returns>True if the Object is equal to this Quaternion; False otherwise.</returns>
        public override bool Equals(object? obj) => obj is Quaternion other && Equals(other);

        /// <summary>
        /// Returns a boolean indicating whether the given Quaternion is equal to this Quaternion instance.
        /// </summary>
        /// <param name="other">The Quaternion to compare this instance to.</param>
        /// <returns>True if the other Quaternion is equal to this instance; False otherwise.</returns>
        public bool Equals(Quaternion other)
            => X == other.X && Y == other.Y && Z == other.Z && W == other.W;

        /// <summary>
        /// Calculates the dot product of two Quaternions.
        /// </summary>
        /// <param name="other">The second source Quaternion.</param>
        /// <returns>The dot product of the Quaternions.</returns>
        public double Dot(Quaternion other) => Dot(this, other);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
            => unchecked(X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + W.GetHashCode());

        /// <summary>
        /// Returns the inverse of this Quaternion.
        /// </summary>
        /// <returns>The inverted Quaternion.</returns>
        public Quaternion Inverse() => Inverse(this);

        /// <summary>
        /// Calculates the length of the Quaternion.
        /// </summary>
        /// <returns>The computed length of the Quaternion.</returns>
        public double Length() => Math.Sqrt(LengthSquared());

        /// <summary>
        /// Calculates the length squared of the Quaternion. This operation is cheaper than Length().
        /// </summary>
        /// <returns>The length squared of the Quaternion.</returns>
        public double LengthSquared() => (X * X) + (Y * Y) + (Z * Z) + (W * W);

        /// <summary>
        /// Divides each component of this Quaternion by the length of the Quaternion.
        /// </summary>
        /// <returns>The normalized Quaternion.</returns>
        public Quaternion Normalize() => Normalize(this);

        /// <summary>
        /// Returns a String representing this Quaternion instance.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            var ci = CultureInfo.CurrentCulture;

            return string.Format(ci, "{{X:{0} Y:{1} Z:{2} W:{3}}}", X.ToString(ci), Y.ToString(ci), Z.ToString(ci), W.ToString(ci));
        }

        /// <summary>
        /// Flips the sign of each component of the quaternion.
        /// </summary>
        /// <param name="value">The source Quaternion.</param>
        /// <returns>The negated Quaternion.</returns>
        public static Quaternion Negate(Quaternion value) => new(
            -value.X,
            -value.Y,
            -value.Z,
            -value.W);

        /// <summary>
        /// Adds two Quaternions element-by-element.
        /// </summary>
        /// <param name="value1">The first source Quaternion.</param>
        /// <param name="value2">The second source Quaternion.</param>
        /// <returns>The result of adding the Quaternions.</returns>
        public static Quaternion Add(Quaternion value1, Quaternion value2) => new(
            value1.X + value2.X,
            value1.Y + value2.Y,
            value1.Z + value2.Z,
            value1.W + value2.W);

        /// <summary>
        /// Subtracts one Quaternion from another.
        /// </summary>
        /// <param name="value1">The first source Quaternion.</param>
        /// <param name="value2">The second Quaternion, to be subtracted from the first.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Quaternion Subtract(Quaternion value1, Quaternion value2) => new(
            value1.X - value2.X,
            value1.Y - value2.Y,
            value1.Z - value2.Z,
            value1.W - value2.W);

        /// <summary>
        /// Multiplies two Quaternions together.
        /// </summary>
        /// <param name="value1">The Quaternion on the left side of the multiplication.</param>
        /// <param name="value2">The Quaternion on the right side of the multiplication.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Quaternion Multiply(Quaternion value1, Quaternion value2)
        {
            var q1x = value1.X;
            var q1y = value1.Y;
            var q1z = value1.Z;
            var q1w = value1.W;

            var q2x = value2.X;
            var q2y = value2.Y;
            var q2z = value2.Z;
            var q2w = value2.W;

            // cross(av, bv)
            var cx = (q1y * q2z) - (q1z * q2y);
            var cy = (q1z * q2x) - (q1x * q2z);
            var cz = (q1x * q2y) - (q1y * q2x);

            var dot = (q1x * q2x) + (q1y * q2y) + (q1z * q2z);

            return new Quaternion(
                (q1x * q2w) + (q2x * q1w) + cx,
                (q1y * q2w) + (q2y * q1w) + cy,
                (q1z * q2w) + (q2z * q1w) + cz,
                (q1w * q2w) - dot);
        }

        /// <summary>
        /// Multiplies a Quaternion by a scalar value.
        /// </summary>
        /// <param name="value1">The source Quaternion.</param>
        /// <param name="value2">The scalar value.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Quaternion Multiply(Quaternion value1, double value2) => new(
            value1.X * value2,
            value1.Y * value2,
            value1.Z * value2,
            value1.W * value2);

        /// <summary>
        /// Divides a Quaternion by another Quaternion.
        /// </summary>
        /// <param name="value1">The source Quaternion.</param>
        /// <param name="value2">The divisor.</param>
        /// <returns>The result of the division.</returns>
        public static Quaternion Divide(Quaternion value1, Quaternion value2)
        {
            var q1x = value1.X;
            var q1y = value1.Y;
            var q1z = value1.Z;
            var q1w = value1.W;

            //-------------------------------------
            // Inverse part.
            var ls = (value2.X * value2.X) + (value2.Y * value2.Y)
                       + (value2.Z * value2.Z) + (value2.W * value2.W);
            var invNorm = 1.0 / ls;

            var q2x = -value2.X * invNorm;
            var q2y = -value2.Y * invNorm;
            var q2z = -value2.Z * invNorm;
            var q2w = value2.W * invNorm;

            //-------------------------------------
            // Multiply part.

            // cross(av, bv)
            var cx = (q1y * q2z) - (q1z * q2y);
            var cy = (q1z * q2x) - (q1x * q2z);
            var cz = (q1x * q2y) - (q1y * q2x);

            var dot = (q1x * q2x) + (q1y * q2y) + (q1z * q2z);

            return new Quaternion(
                (q1x * q2w) + (q2x * q1w) + cx,
                (q1y * q2w) + (q2y * q1w) + cy,
                (q1z * q2w) + (q2z * q1w) + cz,
                (q1w * q2w) - dot);
        }

        /// <summary>
        /// Flips the sign of each component of the quaternion.
        /// </summary>
        /// <param name="value">The source Quaternion.</param>
        /// <returns>The negated Quaternion.</returns>
        public static Quaternion operator -(Quaternion value) => new(
            -value.X,
            -value.Y,
            -value.Z,
            -value.W);

        /// <summary>
        /// Adds two Quaternions element-by-element.
        /// </summary>
        /// <param name="value1">The first source Quaternion.</param>
        /// <param name="value2">The second source Quaternion.</param>
        /// <returns>The result of adding the Quaternions.</returns>
        public static Quaternion operator +(Quaternion value1, Quaternion value2) => new(
            value1.X + value2.X,
            value1.Y + value2.Y,
            value1.Z + value2.Z,
            value1.W + value2.W);

        /// <summary>
        /// Subtracts one Quaternion from another.
        /// </summary>
        /// <param name="value1">The first source Quaternion.</param>
        /// <param name="value2">The second Quaternion, to be subtracted from the first.</param>
        /// <returns>The result of the subtraction.</returns>
        public static Quaternion operator -(Quaternion value1, Quaternion value2) => new(
            value1.X - value2.X,
            value1.Y - value2.Y,
            value1.Z - value2.Z,
            value1.W - value2.W);

        /// <summary>
        /// Multiplies two Quaternions together.
        /// </summary>
        /// <param name="value1">The Quaternion on the left side of the multiplication.</param>
        /// <param name="value2">The Quaternion on the right side of the multiplication.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Quaternion operator *(Quaternion value1, Quaternion value2)
        {
            var q1x = value1.X;
            var q1y = value1.Y;
            var q1z = value1.Z;
            var q1w = value1.W;

            var q2x = value2.X;
            var q2y = value2.Y;
            var q2z = value2.Z;
            var q2w = value2.W;

            // cross(av, bv)
            var cx = (q1y * q2z) - (q1z * q2y);
            var cy = (q1z * q2x) - (q1x * q2z);
            var cz = (q1x * q2y) - (q1y * q2x);

            var dot = (q1x * q2x) + (q1y * q2y) + (q1z * q2z);

            return new Quaternion(
                (q1x * q2w) + (q2x * q1w) + cx,
                (q1y * q2w) + (q2y * q1w) + cy,
                (q1z * q2w) + (q2z * q1w) + cz,
                (q1w * q2w) - dot);
        }

        /// <summary>
        /// Multiplies a Quaternion by a scalar value.
        /// </summary>
        /// <param name="value1">The source Quaternion.</param>
        /// <param name="value2">The scalar value.</param>
        /// <returns>The result of the multiplication.</returns>
        public static Quaternion operator *(Quaternion value1, double value2) => new(
            value1.X * value2,
            value1.Y * value2,
            value1.Z * value2,
            value1.W * value2);

        /// <summary>
        /// Divides a Quaternion by another Quaternion.
        /// </summary>
        /// <param name="value1">The source Quaternion.</param>
        /// <param name="value2">The divisor.</param>
        /// <returns>The result of the division.</returns>
        public static Quaternion operator /(Quaternion value1, Quaternion value2)
        {
            var q1x = value1.X;
            var q1y = value1.Y;
            var q1z = value1.Z;
            var q1w = value1.W;

            //-------------------------------------
            // Inverse part.
            var ls = (value2.X * value2.X) + (value2.Y * value2.Y)
                       + (value2.Z * value2.Z) + (value2.W * value2.W);
            var invNorm = 1.0 / ls;

            var q2x = -value2.X * invNorm;
            var q2y = -value2.Y * invNorm;
            var q2z = -value2.Z * invNorm;
            var q2w = value2.W * invNorm;

            //-------------------------------------
            // Multiply part.

            // cross(av, bv)
            var cx = (q1y * q2z) - (q1z * q2y);
            var cy = (q1z * q2x) - (q1x * q2z);
            var cz = (q1x * q2y) - (q1y * q2x);

            var dot = (q1x * q2x) + (q1y * q2y) + (q1z * q2z);

            return new Quaternion(
                (q1x * q2w) + (q2x * q1w) + cx,
                (q1y * q2w) + (q2y * q1w) + cy,
                (q1z * q2w) + (q2z * q1w) + cz,
                (q1w * q2w) - dot);
        }

        /// <summary>
        /// Returns a boolean indicating whether the two given Quaternions are equal.
        /// </summary>
        /// <param name="value1">The first Quaternion to compare.</param>
        /// <param name="value2">The second Quaternion to compare.</param>
        /// <returns>True if the Quaternions are equal; False otherwise.</returns>
        public static bool operator ==(Quaternion value1, Quaternion value2)
            => value1.X == value2.X
            && value1.Y == value2.Y
            && value1.Z == value2.Z
            && value1.W == value2.W;

        /// <summary>
        /// Returns a boolean indicating whether the two given Quaternions are not equal.
        /// </summary>
        /// <param name="value1">The first Quaternion to compare.</param>
        /// <param name="value2">The second Quaternion to compare.</param>
        /// <returns>True if the Quaternions are not equal; False if they are equal.</returns>
        public static bool operator !=(Quaternion value1, Quaternion value2)
            => value1.X != value2.X
            || value1.Y != value2.Y
            || value1.Z != value2.Z
            || value1.W != value2.W;

        /// <summary>
        /// Converts between implementations of quaternions.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator Quaternion(System.Numerics.Quaternion value)
            => new(value.X, value.Y, value.Z, value.W);

        /// <summary>
        /// Converts between implementations of quaternions.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static explicit operator Quaternion(HugeNumbers.Quaternion value)
            => new((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);

        /// <summary>
        /// Converts between implementations of quaternions.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static explicit operator Quaternion(Decimals.Quaternion value)
            => new((double)value.X, (double)value.Y, (double)value.Z, (double)value.W);

        /// <summary>
        /// Converts between implementations of quaternions.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static explicit operator System.Numerics.Quaternion(Quaternion value)
            => new((float)value.X, (float)value.Y, (float)value.Z, (float)value.W);
    }
}
