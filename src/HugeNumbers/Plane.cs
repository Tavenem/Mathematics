using System;
using System.Globalization;
using System.Runtime.Serialization;
using Tavenem.HugeNumbers;

namespace Tavenem.Mathematics.HugeNumbers
{
    /// <summary>
    /// A structure encapsulating a 3D Plane
    /// </summary>
    /// <remarks>
    /// Unlike the <see cref="System.Numerics.Plane"/> implementation, this is an immutable struct
    /// with read-only properties, rather than writable fields.
    /// </remarks>
    [Serializable]
    [DataContract]
    public struct Plane : IEquatable<Plane>
    {
        /// <summary>
        /// The normal vector of the Plane.
        /// </summary>
        [DataMember(Order = 1)]
        public Vector3 Normal { get; }

        /// <summary>
        /// The distance of the Plane along its normal from the origin.
        /// </summary>
        [DataMember(Order = 2)]
        public HugeNumber D { get; }

        /// <summary>
        /// Constructs a Plane from the X, Y, and Z components of its normal, and its distance from the origin on that normal.
        /// </summary>
        /// <param name="x">The X-component of the normal.</param>
        /// <param name="y">The Y-component of the normal.</param>
        /// <param name="z">The Z-component of the normal.</param>
        /// <param name="d">The distance of the Plane along its normal from the origin.</param>
        public Plane(HugeNumber x, HugeNumber y, HugeNumber z, HugeNumber d)
        {
            Normal = new Vector3(x, y, z);
            D = d;
        }

        /// <summary>
        /// Constructs a Plane from the given normal and distance along the normal from the origin.
        /// </summary>
        /// <param name="normal">The Plane's normal vector.</param>
        /// <param name="d">The Plane's distance from the origin along its normal vector.</param>
        [System.Text.Json.Serialization.JsonConstructor]
        public Plane(Vector3 normal, HugeNumber d)
        {
            Normal = normal;
            D = d;
        }

        /// <summary>
        /// Constructs a Plane from the given Vector4.
        /// </summary>
        /// <param name="value">A vector whose first 3 elements describe the normal vector,
        /// and whose W component defines the distance along that normal from the origin.</param>
        public Plane(Vector4 value)
        {
            Normal = new Vector3(value.X, value.Y, value.Z);
            D = value.W;
        }

        /// <summary>
        /// Creates a Plane that contains the three given points.
        /// </summary>
        /// <param name="point1">The first point defining the Plane.</param>
        /// <param name="point2">The second point defining the Plane.</param>
        /// <param name="point3">The third point defining the Plane.</param>
        /// <returns>The Plane containing the three points.</returns>
        public static Plane CreateFromVertices(Vector3 point1, Vector3 point2, Vector3 point3)
        {
            var ax = point2.X - point1.X;
            var ay = point2.Y - point1.Y;
            var az = point2.Z - point1.Z;

            var bx = point3.X - point1.X;
            var by = point3.Y - point1.Y;
            var bz = point3.Z - point1.Z;

            // N=Cross(a,b)
            var nx = (ay * bz) - (az * by);
            var ny = (az * bx) - (ax * bz);
            var nz = (ax * by) - (ay * bx);

            // Normalize(N)
            var ls = (nx * nx) + (ny * ny) + (nz * nz);
            var invNorm = 1 / ls.Sqrt();

            var normal = new Vector3(
                nx * invNorm,
                ny * invNorm,
                nz * invNorm);

            return new Plane(
                normal,
                -((normal.X * point1.X) + (normal.Y * point1.Y) + (normal.Z * point1.Z)));
        }

        /// <summary>
        /// Calculates the dot product of a Plane and Vector4.
        /// </summary>
        /// <param name="plane">The Plane.</param>
        /// <param name="value">The Vector4.</param>
        /// <returns>The dot product.</returns>
        public static HugeNumber Dot(Plane plane, Vector4 value)
            => (plane.Normal.X * value.X)
            + (plane.Normal.Y * value.Y)
            + (plane.Normal.Z * value.Z)
            + (plane.D * value.W);

        /// <summary>
        /// Returns the dot product of a specified Vector3 and the normal vector of this Plane plus the distance (D) value of the Plane.
        /// </summary>
        /// <param name="plane">The plane.</param>
        /// <param name="value">The Vector3.</param>
        /// <returns>The resulting value.</returns>
        public static HugeNumber DotCoordinate(Plane plane, Vector3 value)
            => (plane.Normal.X * value.X)
            + (plane.Normal.Y * value.Y)
            + (plane.Normal.Z * value.Z)
            + plane.D;

        /// <summary>
        /// Returns the dot product of a specified Vector3 and the Normal vector of this Plane.
        /// </summary>
        /// <param name="plane">The plane.</param>
        /// <param name="value">The Vector3.</param>
        /// <returns>The resulting dot product.</returns>
        public static HugeNumber DotNormal(Plane plane, Vector3 value)
            => (plane.Normal.X * value.X)
            + (plane.Normal.Y * value.Y)
            + (plane.Normal.Z * value.Z);

        /// <summary>
        /// Creates a new Plane whose normal vector is the source Plane's normal vector normalized.
        /// </summary>
        /// <param name="value">The source Plane.</param>
        /// <returns>The normalized Plane.</returns>
        public static Plane Normalize(Plane value)
        {
            var f = (value.Normal.X * value.Normal.X) + (value.Normal.Y * value.Normal.Y) + (value.Normal.Z * value.Normal.Z);

            if (HugeNumber.Abs(f - 1).IsZero)
            {
                return value; // It already normalized, so we don't need to further process.
            }

            var fInv = 1 / f.Sqrt();

            return new Plane(
                value.Normal.X * fInv,
                value.Normal.Y * fInv,
                value.Normal.Z * fInv,
                value.D * fInv);
        }

        /// <summary>
        /// Transforms a normalized Plane by a Matrix.
        /// </summary>
        /// <param name="plane"> The normalized Plane to transform.
        /// This Plane must already be normalized, so that its Normal vector is of unit length, before this method is called.</param>
        /// <param name="matrix">The transformation matrix to apply to the Plane.</param>
        /// <returns>The transformed Plane.</returns>
        public static Plane Transform(Plane plane, Matrix4x4 matrix)
        {
            Matrix4x4.Invert(matrix, out var m);

            HugeNumber x = plane.Normal.X, y = plane.Normal.Y, z = plane.Normal.Z, w = plane.D;

            return new Plane(
                (x * m.M11) + (y * m.M12) + (z * m.M13) + (w * m.M14),
                (x * m.M21) + (y * m.M22) + (z * m.M23) + (w * m.M24),
                (x * m.M31) + (y * m.M32) + (z * m.M33) + (w * m.M34),
                (x * m.M41) + (y * m.M42) + (z * m.M43) + (w * m.M44));
        }

        /// <summary>
        ///  Transforms a normalized Plane by a Quaternion rotation.
        /// </summary>
        /// <param name="plane"> The normalized Plane to transform.
        /// This Plane must already be normalized, so that its Normal vector is of unit length, before this method is called.</param>
        /// <param name="rotation">The Quaternion rotation to apply to the Plane.</param>
        /// <returns>A new Plane that results from applying the rotation.</returns>
        public static Plane Transform(Plane plane, Quaternion rotation)
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

            var m11 = 1 - yy2 - zz2;
            var m21 = xy2 - wz2;
            var m31 = xz2 + wy2;

            var m12 = xy2 + wz2;
            var m22 = 1 - xx2 - zz2;
            var m32 = yz2 - wx2;

            var m13 = xz2 - wy2;
            var m23 = yz2 + wx2;
            var m33 = 1 - xx2 - yy2;

            HugeNumber x = plane.Normal.X, y = plane.Normal.Y, z = plane.Normal.Z;

            return new Plane(
                (x * m11) + (y * m21) + (z * m31),
                (x * m12) + (y * m22) + (z * m32),
                (x * m13) + (y * m23) + (z * m33),
                plane.D);
        }

        /// <summary>
        /// Calculates the dot product of this Plane and a Vector4.
        /// </summary>
        /// <param name="value">The Vector4.</param>
        /// <returns>The dot product.</returns>
        public HugeNumber Dot(Vector4 value) => Dot(this, value);

        /// <summary>
        /// Returns the dot product of a specified Vector3 and the normal vector of this Plane plus the distance (D) value of the Plane.
        /// </summary>
        /// <param name="value">The Vector3.</param>
        /// <returns>The resulting value.</returns>
        public HugeNumber DotCoordinate(Vector3 value) => DotCoordinate(this, value);

        /// <summary>
        /// Returns the dot product of a specified Vector3 and the Normal vector of this Plane.
        /// </summary>
        /// <param name="value">The Vector3.</param>
        /// <returns>The resulting dot product.</returns>
        public HugeNumber DotNormal(Vector3 value) => DotNormal(this, value);

        /// <summary>
        /// Returns a boolean indicating whether the given Plane is equal to this Plane instance.
        /// </summary>
        /// <param name="other">The Plane to compare this instance to.</param>
        /// <returns>True if the other Plane is equal to this instance; False otherwise.</returns>
        public bool Equals(Plane other)
            => Normal.X == other.Normal.X
            && Normal.Y == other.Normal.Y
            && Normal.Z == other.Normal.Z
            && D == other.D;

        /// <summary>
        /// Returns a boolean indicating whether the given Object is equal to this Plane instance.
        /// </summary>
        /// <param name="obj">The Object to compare against.</param>
        /// <returns>True if the Object is equal to this Plane; False otherwise.</returns>
        public override bool Equals(object? obj)
            => obj is Plane other && Equals(other);

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>The hash code.</returns>
        public override int GetHashCode()
            => unchecked(Normal.GetHashCode() + D.GetHashCode());

        /// <summary>
        /// Creates a new Plane whose normal vector is this Plane's normal vector normalized.
        /// </summary>
        /// <returns>The normalized Plane.</returns>
        public Plane Normalize() => Normalize(this);

        /// <summary>
        /// Returns a String representing this Plane instance.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            var ci = CultureInfo.CurrentCulture;

            return string.Format(ci, "{{Normal:{0} D:{1}}}", Normal.ToString(), D.ToString(ci));
        }

        /// <summary>
        /// Transforms this normalized Plane by a Matrix. This Plane must already be normalized, so
        /// that its Normal vector is of unit length, before this method is called.
        /// </summary>
        /// <param name="matrix">The transformation matrix to apply to the Plane.</param>
        /// <returns>The transformed Plane.</returns>
        public Plane Transform(Matrix4x4 matrix) => Transform(this, matrix);

        /// <summary>
        ///  Transforms this normalized Plane by a Quaternion rotation. This Plane must already be
        ///  normalized, so that its Normal vector is of unit length, before this method is called.
        /// </summary>
        /// <param name="rotation">The Quaternion rotation to apply to the Plane.</param>
        /// <returns>A new Plane that results from applying the rotation.</returns>
        public Plane Transform(Quaternion rotation) => Transform(this, rotation);

        /// <summary>
        /// Returns a boolean indicating whether the two given Planes are equal.
        /// </summary>
        /// <param name="value1">The first Plane to compare.</param>
        /// <param name="value2">The second Plane to compare.</param>
        /// <returns>True if the Planes are equal; False otherwise.</returns>
        public static bool operator ==(Plane value1, Plane value2)
            => value1.Equals(value2);

        /// <summary>
        /// Returns a boolean indicating whether the two given Planes are not equal.
        /// </summary>
        /// <param name="value1">The first Plane to compare.</param>
        /// <param name="value2">The second Plane to compare.</param>
        /// <returns>True if the Planes are not equal; False if they are equal.</returns>
        public static bool operator !=(Plane value1, Plane value2)
            => !value1.Equals(value2);

        /// <summary>
        /// Converts between implementations of planes.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator Plane(System.Numerics.Plane value)
            => new(value.Normal.X, value.Normal.Y, value.Normal.Z, value.D);

        /// <summary>
        /// Converts between implementations of planes.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator Plane(Doubles.Plane value)
            => new(value.Normal.X, value.Normal.Y, value.Normal.Z, value.D);

        /// <summary>
        /// Converts between implementations of planes.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static explicit operator Plane(Decimals.Plane value)
            => new((HugeNumber)value.Normal.X, (HugeNumber)value.Normal.Y, (HugeNumber)value.Normal.Z, (HugeNumber)value.D);

        /// <summary>
        /// Converts between implementations of planes.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static explicit operator System.Numerics.Plane(Plane value)
            => new((float)value.Normal.X, (float)value.Normal.Y, (float)value.Normal.Z, (float)value.D);
    }
}
