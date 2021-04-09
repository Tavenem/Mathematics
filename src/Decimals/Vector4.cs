using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace Tavenem.Mathematics.Decimals
{
    /// <summary>
    /// A structure encapsulating four decimal precision floating point values.
    /// </summary>
    /// <remarks>
    /// Unlike the <see cref="System.Numerics.Vector4"/> implementation, this is an immutable struct
    /// with read-only properties, rather than writable fields.
    /// </remarks>
    [Serializable]
    [DataContract]
    public struct Vector4 : IEquatable<Vector4>, IComparable, IComparable<Vector4>, IFormattable
    {
        /// <summary>
        /// Returns the vector (0,0,0,0).
        /// </summary>
        public static Vector4 Zero => new();

        /// <summary>
        /// Returns the vector (1,1,1,1).
        /// </summary>
        public static Vector4 One => new(1.0m, 1.0m, 1.0m, 1.0m);

        /// <summary>
        /// Returns the vector (1,0,0,0).
        /// </summary>
        public static Vector4 UnitX => new(1.0m, 0.0m, 0.0m, 0.0m);

        /// <summary>
        /// Returns the vector (0,1,0,0).
        /// </summary>
        public static Vector4 UnitY => new(0.0m, 1.0m, 0.0m, 0.0m);

        /// <summary>
        /// Returns the vector (0,0,1,0).
        /// </summary>
        public static Vector4 UnitZ => new(0.0m, 0.0m, 1.0m, 0.0m);

        /// <summary>
        /// Returns the vector (0,0,0,1).
        /// </summary>
        public static Vector4 UnitW => new(0.0m, 0.0m, 0.0m, 1.0m);

        /// <summary>
        /// The X component of the vector.
        /// </summary>
        [DataMember(Order = 1)]
        public decimal X { get; }

        /// <summary>
        /// The Y component of the vector.
        /// </summary>
        [DataMember(Order = 2)]
        public decimal Y { get; }

        /// <summary>
        /// The Z component of the vector.
        /// </summary>
        [DataMember(Order = 3)]
        public decimal Z { get; }

        /// <summary>
        /// The W component of the vector.
        /// </summary>
        [DataMember(Order = 4)]
        public decimal W { get; }

        /// <summary>
        /// Constructs a vector whose elements are all the single specified value.
        /// </summary>
        /// <param name="value">The element to fill the vector with.</param>
        public Vector4(decimal value) : this(value, value, value, value) { }

        /// <summary>
        /// Constructs a vector with the given individual elements.
        /// </summary>
        /// <param name="x">X component.</param>
        /// <param name="y">Y component.</param>
        /// <param name="z">Z component.</param>
        /// <param name="w">W component.</param>
        [System.Text.Json.Serialization.JsonConstructor]
        public Vector4(decimal x, decimal y, decimal z, decimal w)
        {
            W = w;
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Constructs a Vector4 from the given Vector2 and a Z and W component.
        /// </summary>
        /// <param name="value">The vector to use as the X and Y components.</param>
        /// <param name="z">The Z component.</param>
        /// <param name="w">The W component.</param>
        public Vector4(Vector2 value, decimal z, decimal w)
        {
            X = value.X;
            Y = value.Y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Constructs a Vector4 from the given Vector3 and a W component.
        /// </summary>
        /// <param name="value">The vector to use as the X, Y, and Z components.</param>
        /// <param name="w">The W component.</param>
        public Vector4(Vector3 value, decimal w)
        {
            X = value.X;
            Y = value.Y;
            Z = value.Z;
            W = w;
        }

        /// <summary>
        /// Returns a vector whose elements are the absolute values of each of the source vector's elements.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The absolute value vector.</returns>
        public static Vector4 Abs(Vector4 value)
            => new(Math.Abs(value.X), Math.Abs(value.Y), Math.Abs(value.Z), Math.Abs(value.W));

        /// <summary>
        /// Restricts a vector between a min and max value.
        /// </summary>
        /// <param name="value1">The source vector.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The restricted vector.</returns>
        public static Vector4 Clamp(Vector4 value1, Vector4 min, Vector4 max)
        {
            // This compare order is very important!!!
            // We must follow HLSL behavior in the case user specified min value is bigger than max value.
            var x = value1.X;
            x = (min.X > x) ? min.X : x;  // max(x, minx)
            x = (max.X < x) ? max.X : x;  // min(x, maxx)

            var y = value1.Y;
            y = (min.Y > y) ? min.Y : y;  // max(y, miny)
            y = (max.Y < y) ? max.Y : y;  // min(y, maxy)

            var z = value1.Z;
            z = (min.Z > z) ? min.Z : z;  // max(z, minz)
            z = (max.Z < z) ? max.Z : z;  // min(z, maxz)

            var w = value1.W;
            w = (min.W > w) ? min.W : w;  // max(w, minw)
            w = (max.W < w) ? max.W : w;  // min(w, minw)

            return new Vector4(x, y, z, w);
        }

        /// <summary>
        /// Returns the Euclidean distance between the two given points.
        /// </summary>
        /// <param name="value1">The first point.</param>
        /// <param name="value2">The second point.</param>
        /// <returns>The distance.</returns>
        public static decimal Distance(Vector4 value1, Vector4 value2)
            => DistanceSquared(value1, value2).Sqrt();

        /// <summary>
        /// Returns the Euclidean distance squared between the two given points.
        /// </summary>
        /// <param name="value1">The first point.</param>
        /// <param name="value2">The second point.</param>
        /// <returns>The distance squared.</returns>
        public static decimal DistanceSquared(Vector4 value1, Vector4 value2)
        {
            var dx = value1.X - value2.X;
            var dy = value1.Y - value2.Y;
            var dz = value1.Z - value2.Z;
            var dw = value1.W - value2.W;

            return (dx * dx) + (dy * dy) + (dz * dz) + (dw * dw);
        }

        /// <summary>
        /// Returns the dot product of two vectors.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The dot product.</returns>
        public static decimal Dot(Vector4 vector1, Vector4 vector2)
            => (vector1.X * vector2.X)
            + (vector1.Y * vector2.Y)
            + (vector1.Z * vector2.Z)
            + (vector1.W * vector2.W);

        /// <summary>
        /// Determines if a vector is zero.
        /// </summary>
        /// <param name="value">A vector to test.</param>
        /// <returns><see langword="true"/> if the vector is close to zero; otherwise <see
        /// langword="false"/>.</returns>
        public static bool IsZero(Vector4 value) => value.X == 0 && value.Y == 0 && value.Z == 0 && value.W == 0;

        /// <summary>
        /// Linearly interpolates between two vectors based on the given weighting.
        /// </summary>
        /// <param name="value1">The first source vector.</param>
        /// <param name="value2">The second source vector.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of the second source vector.</param>
        /// <returns>The interpolated vector.</returns>
        public static Vector4 Lerp(Vector4 value1, Vector4 value2, decimal amount)
            => new(
                value1.X + ((value2.X - value1.X) * amount),
                value1.Y + ((value2.Y - value1.Y) * amount),
                value1.Z + ((value2.Z - value1.Z) * amount),
                value1.W + ((value2.W - value1.W) * amount));

        /// <summary>
        /// Returns a vector whose elements are the maximum of each of the pairs of elements in the two source vectors.
        /// </summary>
        /// <param name="value1">The first source vector.</param>
        /// <param name="value2">The second source vector.</param>
        /// <returns>The maximized vector.</returns>
        public static Vector4 Max(Vector4 value1, Vector4 value2)
            => new(
                (value1.X > value2.X) ? value1.X : value2.X,
                (value1.Y > value2.Y) ? value1.Y : value2.Y,
                (value1.Z > value2.Z) ? value1.Z : value2.Z,
                (value1.W > value2.W) ? value1.W : value2.W);

        /// <summary>
        /// Returns a vector whose elements are the minimum of each of the pairs of elements in the two source vectors.
        /// </summary>
        /// <param name="value1">The first source vector.</param>
        /// <param name="value2">The second source vector.</param>
        /// <returns>The minimized vector.</returns>
        public static Vector4 Min(Vector4 value1, Vector4 value2)
            => new(
                (value1.X < value2.X) ? value1.X : value2.X,
                (value1.Y < value2.Y) ? value1.Y : value2.Y,
                (value1.Z < value2.Z) ? value1.Z : value2.Z,
                (value1.W < value2.W) ? value1.W : value2.W);

        /// <summary>
        /// Returns a vector with the same direction as the given vector, but with a length of 1.
        /// </summary>
        /// <param name="vector">The vector to normalize.</param>
        /// <returns>The normalized vector.</returns>
        public static Vector4 Normalize(Vector4 vector)
        {
            var ls = (vector.X * vector.X) + (vector.Y * vector.Y) + (vector.Z * vector.Z) + (vector.W * vector.W);
            var invNorm = 1.0m / ls.Sqrt();

            return new Vector4(
                vector.X * invNorm,
                vector.Y * invNorm,
                vector.Z * invNorm,
                vector.W * invNorm);
        }

        /// <summary>
        /// Returns a vector whose elements are the square root of each of the source vector's elements.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The square root vector.</returns>
        public static Vector4 SquareRoot(Vector4 value)
            => new(value.X.Sqrt(), value.Y.Sqrt(), value.Z.Sqrt(), value.W.Sqrt());

        /// <summary>
        /// Transforms a vector by the given matrix.
        /// </summary>
        /// <param name="position">The source vector.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector4 Transform(Vector2 position, Matrix4x4 matrix)
            => new(
                (position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41,
                (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42,
                (position.X * matrix.M13) + (position.Y * matrix.M23) + matrix.M43,
                (position.X * matrix.M14) + (position.Y * matrix.M24) + matrix.M44);

        /// <summary>
        /// Transforms a vector by the given matrix.
        /// </summary>
        /// <param name="position">The source vector.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector4 Transform(Vector3 position, Matrix4x4 matrix)
            => new(
                (position.X * matrix.M11) + (position.Y * matrix.M21) + (position.Z * matrix.M31) + matrix.M41,
                (position.X * matrix.M12) + (position.Y * matrix.M22) + (position.Z * matrix.M32) + matrix.M42,
                (position.X * matrix.M13) + (position.Y * matrix.M23) + (position.Z * matrix.M33) + matrix.M43,
                (position.X * matrix.M14) + (position.Y * matrix.M24) + (position.Z * matrix.M34) + matrix.M44);

        /// <summary>
        /// Transforms a vector by the given matrix.
        /// </summary>
        /// <param name="vector">The source vector.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector4 Transform(Vector4 vector, Matrix4x4 matrix)
            => new(
                (vector.X * matrix.M11) + (vector.Y * matrix.M21) + (vector.Z * matrix.M31) + (vector.W * matrix.M41),
                (vector.X * matrix.M12) + (vector.Y * matrix.M22) + (vector.Z * matrix.M32) + (vector.W * matrix.M42),
                (vector.X * matrix.M13) + (vector.Y * matrix.M23) + (vector.Z * matrix.M33) + (vector.W * matrix.M43),
                (vector.X * matrix.M14) + (vector.Y * matrix.M24) + (vector.Z * matrix.M34) + (vector.W * matrix.M44));

        /// <summary>
        /// Transforms a vector by the given Quaternion rotation value.
        /// </summary>
        /// <param name="value">The source vector to be rotated.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector4 Transform(Vector2 value, Quaternion rotation)
        {
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

            return new Vector4(
                (value.X * (1.0m - yy2 - zz2)) + (value.Y * (xy2 - wz2)),
                (value.X * (xy2 + wz2)) + (value.Y * (1.0m - xx2 - zz2)),
                (value.X * (xz2 - wy2)) + (value.Y * (yz2 + wx2)),
                1.0m);
        }

        /// <summary>
        /// Transforms a vector by the given Quaternion rotation value.
        /// </summary>
        /// <param name="value">The source vector to be rotated.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector4 Transform(Vector3 value, Quaternion rotation)
        {
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

            return new Vector4(
                (value.X * (1.0m - yy2 - zz2)) + (value.Y * (xy2 - wz2)) + (value.Z * (xz2 + wy2)),
                (value.X * (xy2 + wz2)) + (value.Y * (1.0m - xx2 - zz2)) + (value.Z * (yz2 - wx2)),
                (value.X * (xz2 - wy2)) + (value.Y * (yz2 + wx2)) + (value.Z * (1.0m - xx2 - yy2)),
                1.0m);
        }

        /// <summary>
        /// Transforms a vector by the given Quaternion rotation value.
        /// </summary>
        /// <param name="value">The source vector to be rotated.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector4 Transform(Vector4 value, Quaternion rotation)
        {
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

            return new Vector4(
                (value.X * (1.0m - yy2 - zz2)) + (value.Y * (xy2 - wz2)) + (value.Z * (xz2 + wy2)),
                (value.X * (xy2 + wz2)) + (value.Y * (1.0m - xx2 - zz2)) + (value.Z * (yz2 - wx2)),
                (value.X * (xz2 - wy2)) + (value.Y * (yz2 + wx2)) + (value.Z * (1.0m - xx2 - yy2)),
                value.W);
        }

        /// <summary>
        /// Returns a vector whose elements are the absolute values of each of this vector's elements.
        /// </summary>
        /// <returns>The absolute value vector.</returns>
        public Vector4 Abs() => Abs(this);

        /// <summary>
        /// Restricts this vector between a min and max value.
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The restricted vector.</returns>
        public Vector4 Clamp(Vector4 min, Vector4 max) => Clamp(this, min, max);

        /// <summary>Compares the current instance with another object of the same type and returns
        /// an integer that indicates whether the current instance precedes, follows, or occurs in
        /// the same position in the sort order as the other object.</summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return
        /// value has these meanings: Value Meaning Less than zero This instance precedes <paramref
        /// name="obj" /> in the sort order. Zero This instance occurs in the same position in the
        /// sort order as <paramref name="obj" />. Greater than zero This instance follows <paramref
        /// name="obj" /> in the sort order.</returns>
        /// <exception cref="ArgumentException"><paramref name="obj" /> is not the same type as this
        /// instance.</exception>
        public int CompareTo(object? obj)
        {
            if (obj is Vector4 other)
            {
                return CompareTo(other);
            }
            throw new ArgumentException($"{nameof(obj)} is not the same type as this instance.", nameof(obj));
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an
        /// integer that indicates whether the current instance precedes, follows, or occurs in the
        /// same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being
        /// compared.</returns>
        public int CompareTo(Vector4 other)
        {
            if (Equals(other))
            {
                return 0;
            }
            if (LengthSquared() >= other.LengthSquared())
            {
                return 1;
            }
            return -1;
        }

        /// <summary>
        /// Copies the contents of the vector into the given array.
        /// </summary>
        /// <param name="array">The array into which the elements will be copied.</param>
        public void CopyTo(decimal[] array) => CopyTo(array, 0);

        /// <summary>
        /// Copies the contents of the vector into the given array, starting from index.
        /// </summary>
        /// <param name="array">The array into which the elements will be copied.</param>
        /// <param name="index">The index at which copying will begin.</param>
        /// <exception cref="ArgumentNullException">If array is null.</exception>
        /// <exception cref="RankException">If array is multidimensional.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If index is greater than end of the array or index is less than zero.</exception>
        /// <exception cref="ArgumentException">If number of elements in source vector is greater than those available in destination array.</exception>
        public void CopyTo(decimal[] array, int index)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            if (index < 0 || index >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            if ((array.Length - index) < 4)
            {
                throw new ArgumentException(MathematicsErrorMessages.ArraySizeMismatch, nameof(index));
            }
            array[index] = X;
            array[index + 1] = Y;
            array[index + 2] = Z;
            array[index + 3] = W;
        }

        /// <summary>
        /// Returns the Euclidean distance between the this point and the given point.
        /// </summary>
        /// <param name="other">The second point.</param>
        /// <returns>The distance.</returns>
        public decimal Distance(Vector4 other) => Distance(this, other);

        /// <summary>
        /// Returns the Euclidean distance squared between this point and the given point.
        /// </summary>
        /// <param name="other">The second point.</param>
        /// <returns>The distance squared.</returns>
        public decimal DistanceSquared(Vector4 other) => DistanceSquared(this, other);

        /// <summary>
        /// Returns the dot product of two vectors.
        /// </summary>
        /// <param name="other">The second vector.</param>
        /// <returns>The dot product.</returns>
        public decimal Dot(Vector4 other) => Dot(this, other);

        /// <summary>
        /// Returns a boolean indicating whether the given Object is equal to this Vector4 instance.
        /// </summary>
        /// <param name="obj">The Object to compare against.</param>
        /// <returns>True if the Object is equal to this Vector4; False otherwise.</returns>
        public override bool Equals(object? obj) => obj is Vector4 other && Equals(other);

        /// <summary>
        /// Returns a boolean indicating whether the given Vector4 is equal to this Vector4 instance.
        /// </summary>
        /// <param name="other">The Vector4 to compare this instance to.</param>
        /// <returns>True if the other Vector4 is equal to this instance; False otherwise.</returns>
        public bool Equals(Vector4 other)
            => X == other.X
            && Y == other.Y
            && Z == other.Z
            && W == other.W;

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = (hashCode * -1521134295) + X.GetHashCode();
            hashCode = (hashCode * -1521134295) + Y.GetHashCode();
            hashCode = (hashCode * -1521134295) + Z.GetHashCode();
            return (hashCode * -1521134295) + W.GetHashCode();
        }

        /// <summary>
        /// Determines if this vector is zero.
        /// </summary>
        /// <returns><see langword="true"/> if this vector is zero; otherwise <see
        /// langword="false"/>.</returns>
        public bool IsZero() => IsZero(this);

        /// <summary>
        /// Returns the length of the vector.
        /// </summary>
        /// <returns>The vector's length.</returns>
        public decimal Length() => LengthSquared().Sqrt();

        /// <summary>
        /// Returns the length of the vector squared. This operation is cheaper than Length().
        /// </summary>
        /// <returns>The vector's length squared.</returns>
        public decimal LengthSquared() => (X * X) + (Y * Y) + (Z * Z) + (W * W);

        /// <summary>
        /// Returns a vector with the same direction as this vector, but with a length of 1.
        /// </summary>
        /// <returns>The normalized vector.</returns>
        public Vector4 Normalize() => Normalize(this);

        /// <summary>
        /// Returns a vector whose elements are the square root of each of this vector's elements.
        /// </summary>
        /// <returns>The square root vector.</returns>
        public Vector4 SquareRoot() => SquareRoot(this);

        /// <summary>
        /// Transforms this vector by the given matrix.
        /// </summary>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        public Vector4 Transform(Matrix4x4 matrix) => Transform(this, matrix);

        /// <summary>
        /// Transforms this vector by the given Quaternion rotation value.
        /// </summary>
        /// <param name="rotation">The rotation to apply.</param>
        /// <returns>The transformed vector.</returns>
        public Vector4 Transform(Quaternion rotation) => Transform(this, rotation);

        /// <summary>
        /// Returns a String representing this Vector4 instance.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString() => ToString("G", CultureInfo.CurrentCulture);

        /// <summary>
        /// Returns a String representing this Vector4 instance, using the specified format to format individual elements.
        /// </summary>
        /// <param name="format">The format of individual elements.</param>
        /// <returns>The string representation.</returns>
        public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

        /// <summary>
        /// Returns a String representing this Vector4 instance, using the specified format to format individual elements
        /// and the given IFormatProvider.
        /// </summary>
        /// <param name="format">The format of individual elements.</param>
        /// <param name="formatProvider">The format provider to use when formatting elements.</param>
        /// <returns>The string representation.</returns>
        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            var sb = new StringBuilder();
            var separator = NumberFormatInfo.GetInstance(formatProvider).NumberGroupSeparator;
            sb.Append('<');
            sb.Append(X.ToString(format, formatProvider));
            sb.Append(separator);
            sb.Append(' ');
            sb.Append(Y.ToString(format, formatProvider));
            sb.Append(separator);
            sb.Append(' ');
            sb.Append(Z.ToString(format, formatProvider));
            sb.Append(separator);
            sb.Append(' ');
            sb.Append(W.ToString(format, formatProvider));
            sb.Append('>');
            return sb.ToString();
        }

        /// <summary>
        /// Adds two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The summed vector.</returns>
        public static Vector4 Add(Vector4 left, Vector4 right) => left + right;

        /// <summary>
        /// Subtracts the second vector from the first.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The difference vector.</returns>
        public static Vector4 Subtract(Vector4 left, Vector4 right) => left - right;

        /// <summary>
        /// Multiplies two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The product vector.</returns>
        public static Vector4 Multiply(Vector4 left, Vector4 right) => left * right;

        /// <summary>
        /// Multiplies a vector by the given scalar.
        /// </summary>
        /// <param name="left">The source vector.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The scaled vector.</returns>
        public static Vector4 Multiply(Vector4 left, decimal right) => left * right;

        /// <summary>
        /// Multiplies a vector by the given scalar.
        /// </summary>
        /// <param name="left">The scalar value.</param>
        /// <param name="right">The source vector.</param>
        /// <returns>The scaled vector.</returns>
        public static Vector4 Multiply(decimal left, Vector4 right) => left * right;

        /// <summary>
        /// Divides the first vector by the second.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The vector resulting from the division.</returns>
        public static Vector4 Divide(Vector4 left, Vector4 right) => left / right;

        /// <summary>
        /// Divides the vector by the given scalar.
        /// </summary>
        /// <param name="left">The source vector.</param>
        /// <param name="divisor">The scalar value.</param>
        /// <returns>The result of the division.</returns>
        public static Vector4 Divide(Vector4 left, decimal divisor) => left / divisor;

        /// <summary>
        /// Negates a given vector.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The negated vector.</returns>
        public static Vector4 Negate(Vector4 value) => -value;

        /// <summary>
        /// Adds two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The summed vector.</returns>
        public static Vector4 operator +(Vector4 left, Vector4 right)
            => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

        /// <summary>
        /// Subtracts the second vector from the first.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The difference vector.</returns>
        public static Vector4 operator -(Vector4 left, Vector4 right)
            => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

        /// <summary>
        /// Multiplies two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The product vector.</returns>
        public static Vector4 operator *(Vector4 left, Vector4 right)
            => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

        /// <summary>
        /// Multiplies a vector by the given scalar.
        /// </summary>
        /// <param name="left">The source vector.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The scaled vector.</returns>
        public static Vector4 operator *(Vector4 left, decimal right)
            => new(left.X * right, left.Y * right, left.Z * right, left.W * right);

        /// <summary>
        /// Multiplies a vector by the given scalar.
        /// </summary>
        /// <param name="left">The scalar value.</param>
        /// <param name="right">The source vector.</param>
        /// <returns>The scaled vector.</returns>
        public static Vector4 operator *(decimal left, Vector4 right)
            => new(left * right.X, left * right.Y, left * right.Z, left * right.W);

        /// <summary>
        /// Divides the first vector by the second.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The vector resulting from the division.</returns>
        public static Vector4 operator /(Vector4 left, Vector4 right)
            => new(left.X / right.X, left.Y / right.Y, left.Z / right.Z, left.W / right.W);

        /// <summary>
        /// Divides the vector by the given scalar.
        /// </summary>
        /// <param name="value1">The source vector.</param>
        /// <param name="value2">The scalar value.</param>
        /// <returns>The result of the division.</returns>
        public static Vector4 operator /(Vector4 value1, decimal value2)
            => new(value1.X / value2, value1.Y / value2, value1.Z / value2, value1.W / value2);

        /// <summary>
        /// Negates a given vector.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The negated vector.</returns>
        public static Vector4 operator -(Vector4 value)
            => new(-value.X, -value.Y, -value.Z, -value.W);

        /// <summary>
        /// Returns a boolean indicating whether the two given vectors are equal.
        /// </summary>
        /// <param name="left">The first vector to compare.</param>
        /// <param name="right">The second vector to compare.</param>
        /// <returns>True if the vectors are equal; False otherwise.</returns>
        public static bool operator ==(Vector4 left, Vector4 right) => left.Equals(right);

        /// <summary>
        /// Returns a boolean indicating whether the two given vectors are not equal.
        /// </summary>
        /// <param name="left">The first vector to compare.</param>
        /// <param name="right">The second vector to compare.</param>
        /// <returns>True if the vectors are not equal; False if they are equal.</returns>
        public static bool operator !=(Vector4 left, Vector4 right) => !left.Equals(right);

        /// <summary>
        /// Converts between implementations of vectors.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator Vector4(System.Numerics.Vector4 value)
            => new((decimal)value.X, (decimal)value.Y, (decimal)value.Z, (decimal)value.W);

        /// <summary>
        /// Converts between implementations of vectors.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static explicit operator Vector4(HugeNumbers.Vector4 value)
            => new((decimal)value.X, (decimal)value.Y, (decimal)value.Z, (decimal)value.W);

        /// <summary>
        /// Converts between implementations of vectors.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static explicit operator Vector4(Doubles.Vector4 value)
            => new((decimal)value.X, (decimal)value.Y, (decimal)value.Z, (decimal)value.W);

        /// <summary>
        /// Converts between implementations of vectors.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static explicit operator System.Numerics.Vector4(Vector4 value)
            => new((float)value.X, (float)value.Y, (float)value.Z, (float)value.W);
    }
}
