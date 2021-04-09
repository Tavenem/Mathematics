using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace Tavenem.Mathematics.Doubles
{
    /// <summary>
    /// A structure encapsulating three double precision floating point values.
    /// </summary>
    /// <remarks>
    /// Unlike the <see cref="System.Numerics.Vector3"/> implementation, this is an immutable struct
    /// with read-only properties, rather than writable fields.
    /// </remarks>
    [Serializable]
    [DataContract]
    public struct Vector3 : IEquatable<Vector3>, IComparable, IComparable<Vector3>, IFormattable
    {
        /// <summary>
        /// Returns the vector (0,0,0).
        /// </summary>
        public static Vector3 Zero => new();

        /// <summary>
        /// Returns the vector (1,1,1).
        /// </summary>
        public static Vector3 One => new(1.0, 1.0, 1.0);

        /// <summary>
        /// Returns the vector (1,0,0).
        /// </summary>
        public static Vector3 UnitX => new(1.0, 0.0, 0.0);

        /// <summary>
        /// Returns the vector (0,1,0).
        /// </summary>
        public static Vector3 UnitY => new(0.0, 1.0, 0.0);

        /// <summary>
        /// Returns the vector (0,0,1).
        /// </summary>
        public static Vector3 UnitZ => new(0.0, 0.0, 1.0);

        /// <summary>
        /// The X component of the vector.
        /// </summary>
        [DataMember(Order = 1)]
        public double X { get; }

        /// <summary>
        /// The Y component of the vector.
        /// </summary>
        [DataMember(Order = 2)]
        public double Y { get; }

        /// <summary>
        /// The Z component of the vector.
        /// </summary>
        [DataMember(Order = 3)]
        public double Z { get; }

        /// <summary>
        /// Constructs a vector whose elements are all the single specified value.
        /// </summary>
        /// <param name="value">The element to fill the vector with.</param>
        public Vector3(double value) : this(value, value, value) { }

        /// <summary>
        /// Constructs a Vector3 from the given Vector2 and a third value.
        /// </summary>
        /// <param name="value">The Vector to extract X and Y components from.</param>
        /// <param name="z">The Z component.</param>
        public Vector3(Vector2 value, double z) : this(value.X, value.Y, z) { }

        /// <summary>
        /// Constructs a vector with the given individual elements.
        /// </summary>
        /// <param name="x">The X component.</param>
        /// <param name="y">The Y component.</param>
        /// <param name="z">The Z component.</param>
        [System.Text.Json.Serialization.JsonConstructor]
        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Returns a vector whose elements are the absolute values of each of the source vector's elements.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The absolute value vector.</returns>
        public static Vector3 Abs(Vector3 value)
            => new(Math.Abs(value.X), Math.Abs(value.Y), Math.Abs(value.Z));

        /// <summary>
        /// Computes the angle between two vectors.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <returns>The angle between the vectors, in radians.</returns>
        public static double Angle(Vector3 value1, Vector3 value2)
            => Math.Atan2(Cross(value1, value2).Length(), Dot(value1, value2));

        /// <summary>
        /// Determines if the given vectors are parallel.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        /// <param name="allowSmallError">If <see langword="true"/>, a small amount of error is
        /// disregarded, to account for floating point errors.</param>
        /// <returns><see langword="true"/> if the vectors are parallel; otherwise <see
        /// langword="false"/>.</returns>
        public static bool AreParallel(Vector3 value1, Vector3 value2, bool allowSmallError = true)
        {
            var cross = Vector3.Cross(value1, value2);
            return allowSmallError ? cross.IsZero() : cross == Vector3.Zero;
        }

        /// <summary>
        /// Restricts a vector between a min and max value.
        /// </summary>
        /// <param name="value1">The source vector.</param>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The restricted vector.</returns>
        public static Vector3 Clamp(Vector3 value1, Vector3 min, Vector3 max)
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

            return new Vector3(x, y, z);
        }

        /// <summary>
        /// Computes the cross product of two vectors.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The cross product.</returns>
        public static Vector3 Cross(Vector3 vector1, Vector3 vector2)
            => new(
                (vector1.Y * vector2.Z) - (vector1.Z * vector2.Y),
                (vector1.Z * vector2.X) - (vector1.X * vector2.Z),
                (vector1.X * vector2.Y) - (vector1.Y * vector2.X));

        /// <summary>
        /// Returns the Euclidean distance between the two given points.
        /// </summary>
        /// <param name="value1">The first point.</param>
        /// <param name="value2">The second point.</param>
        /// <returns>The distance.</returns>
        public static double Distance(Vector3 value1, Vector3 value2)
            => Math.Sqrt(DistanceSquared(value1, value2));

        /// <summary>
        /// Returns the Euclidean distance squared between the two given points.
        /// </summary>
        /// <param name="value1">The first point.</param>
        /// <param name="value2">The second point.</param>
        /// <returns>The distance squared.</returns>
        public static double DistanceSquared(Vector3 value1, Vector3 value2)
        {
            var dx = value1.X - value2.X;
            var dy = value1.Y - value2.Y;
            var dz = value1.Z - value2.Z;

            return (dx * dx) + (dy * dy) + (dz * dz);
        }

        /// <summary>
        /// Returns the dot product of two vectors.
        /// </summary>
        /// <param name="vector1">The first vector.</param>
        /// <param name="vector2">The second vector.</param>
        /// <returns>The dot product.</returns>
        public static double Dot(Vector3 vector1, Vector3 vector2)
            => (vector1.X * vector2.X)
            + (vector1.Y * vector2.Y)
            + (vector1.Z * vector2.Z);

        /// <summary>
        /// Determines if a vector is nearly zero (all elements closer to zero than <see
        /// cref="DoubleConstants.NearlyZero"/>).
        /// </summary>
        /// <param name="value">A vector to test.</param>
        /// <returns><see langword="true"/> if the vector is close to zero; otherwise <see
        /// langword="false"/>.</returns>
        public static bool IsZero(Vector3 value) => value.X.IsNearlyZero() && value.Y.IsNearlyZero() && value.Z.IsNearlyZero();

        /// <summary>
        /// Linearly interpolates between two vectors based on the given weighting.
        /// </summary>
        /// <param name="value1">The first source vector.</param>
        /// <param name="value2">The second source vector.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of the second source vector.</param>
        /// <returns>The interpolated vector.</returns>
        public static Vector3 Lerp(Vector3 value1, Vector3 value2, double amount)
            => new(
                value1.X + ((value2.X - value1.X) * amount),
                value1.Y + ((value2.Y - value1.Y) * amount),
                value1.Z + ((value2.Z - value1.Z) * amount));

        /// <summary>
        /// Returns a vector whose elements are the maximum of each of the pairs of elements in the two source vectors.
        /// </summary>
        /// <param name="value1">The first source vector.</param>
        /// <param name="value2">The second source vector.</param>
        /// <returns>The maximized vector.</returns>
        public static Vector3 Max(Vector3 value1, Vector3 value2)
            => new(
                (value1.X > value2.X) ? value1.X : value2.X,
                (value1.Y > value2.Y) ? value1.Y : value2.Y,
                (value1.Z > value2.Z) ? value1.Z : value2.Z);

        /// <summary>
        /// Returns a vector whose elements are the minimum of each of the pairs of elements in the two source vectors.
        /// </summary>
        /// <param name="value1">The first source vector.</param>
        /// <param name="value2">The second source vector.</param>
        /// <returns>The minimized vector.</returns>
        public static Vector3 Min(Vector3 value1, Vector3 value2)
            => new(
                (value1.X < value2.X) ? value1.X : value2.X,
                (value1.Y < value2.Y) ? value1.Y : value2.Y,
                (value1.Z < value2.Z) ? value1.Z : value2.Z);

        /// <summary>
        /// Returns a vector with the same direction as the given vector, but with a length of 1.
        /// </summary>
        /// <param name="value">The vector to normalize.</param>
        /// <returns>The normalized vector.</returns>
        public static Vector3 Normalize(Vector3 value)
        {
            var ls = (value.X * value.X) + (value.Y * value.Y) + (value.Z * value.Z);
            var length = Math.Sqrt(ls);
            return new Vector3(value.X / length, value.Y / length, value.Z / length);
        }

        /// <summary>
        /// Returns the reflection of a vector off a surface that has the specified normal.
        /// </summary>
        /// <param name="vector">The source vector.</param>
        /// <param name="normal">The normal of the surface being reflected off.</param>
        /// <returns>The reflected vector.</returns>
        public static Vector3 Reflect(Vector3 vector, Vector3 normal)
        {
            var dot = (vector.X * normal.X) + (vector.Y * normal.Y) + (vector.Z * normal.Z);
            var tempX = normal.X * dot * 2;
            var tempY = normal.Y * dot * 2;
            var tempZ = normal.Z * dot * 2;
            return new Vector3(vector.X - tempX, vector.Y - tempY, vector.Z - tempZ);
        }

        /// <summary>
        /// Calculates the quaternion which represents the rotation from one vector to another.
        /// </summary>
        /// <param name="value1">The first vector.</param>
        /// <param name="value2">The second vector.</param>
        public static Quaternion RotationTo(Vector3 value1, Vector3 value2)
        {
            if (value1.IsParallel(value2))
            {
                if (value1 != value2)
                {
                    var inter = value1.IsParallel(UnitX) ? UnitY : UnitX;
                    return RotationTo(inter, value2) * RotationTo(value1, inter);
                }
                else
                {
                    return Quaternion.Identity;
                }
            }
            else
            {
                return Quaternion.Normalize(
                    new Quaternion(
                        Cross(value1, value2),
                        Math.Sqrt(value1.LengthSquared() * value2.LengthSquared()) + Dot(value1, value2)));
            }
        }

        /// <summary>
        /// Returns a vector whose elements are the square root of each of the source vector's elements.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The square root vector.</returns>
        public static Vector3 SquareRoot(Vector3 value)
            => new(Math.Sqrt(value.X), Math.Sqrt(value.Y), Math.Sqrt(value.Z));

        /// <summary>
        /// Transforms a vector by the given matrix.
        /// </summary>
        /// <param name="position">The source vector.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector3 Transform(Vector3 position, Matrix4x4 matrix)
            => new(
                (position.X * matrix.M11) + (position.Y * matrix.M21) + (position.Z * matrix.M31) + matrix.M41,
                (position.X * matrix.M12) + (position.Y * matrix.M22) + (position.Z * matrix.M32) + matrix.M42,
                (position.X * matrix.M13) + (position.Y * matrix.M23) + (position.Z * matrix.M33) + matrix.M43);

        /// <summary>
        /// Transforms a vector by the given Quaternion rotation value.
        /// </summary>
        /// <param name="value">The source vector to be rotated.</param>
        /// <param name="rotation">The rotation to apply.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector3 Transform(Vector3 value, Quaternion rotation)
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

            return new Vector3(
                (value.X * (1.0 - yy2 - zz2)) + (value.Y * (xy2 - wz2)) + (value.Z * (xz2 + wy2)),
                (value.X * (xy2 + wz2)) + (value.Y * (1.0 - xx2 - zz2)) + (value.Z * (yz2 - wx2)),
                (value.X * (xz2 - wy2)) + (value.Y * (yz2 + wx2)) + (value.Z * (1.0 - xx2 - yy2)));
        }

        /// <summary>
        /// Transforms a vector normal by the given matrix.
        /// </summary>
        /// <param name="normal">The source vector.</param>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        public static Vector3 TransformNormal(Vector3 normal, Matrix4x4 matrix)
            => new(
                (normal.X * matrix.M11) + (normal.Y * matrix.M21) + (normal.Z * matrix.M31),
                (normal.X * matrix.M12) + (normal.Y * matrix.M22) + (normal.Z * matrix.M32),
                (normal.X * matrix.M13) + (normal.Y * matrix.M23) + (normal.Z * matrix.M33));

        /// <summary>
        /// Returns a vector whose elements are the absolute values of each of this vector's elements.
        /// </summary>
        /// <returns>The absolute value vector.</returns>
        public Vector3 Abs() => Abs(this);

        /// <summary>
        /// Computes the angle between this vector and the given vector.
        /// </summary>
        /// <param name="other">The second vector.</param>
        /// <returns>The angle between the vectors, in radians.</returns>
        public double Angle(Vector3 other) => Angle(this, other);

        /// <summary>
        /// Restricts this vector between a min and max value.
        /// </summary>
        /// <param name="min">The minimum value.</param>
        /// <param name="max">The maximum value.</param>
        /// <returns>The restricted vector.</returns>
        public Vector3 Clamp(Vector3 min, Vector3 max) => Clamp(this, min, max);

        /// <summary>
        /// Computes the cross product of two vectors.
        /// </summary>
        /// <param name="other">The second vector.</param>
        /// <returns>The cross product.</returns>
        public Vector3 Cross(Vector3 other) => Cross(this, other);

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
            if (obj is Vector3 other)
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
        public int CompareTo(Vector3 other)
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
        public void CopyTo(double[] array) => CopyTo(array, 0);

        /// <summary>
        /// Copies the contents of the vector into the given array, starting from index.
        /// </summary>
        /// <param name="array">The array into which the elements will be copied.</param>
        /// <param name="index">The index at which copying will begin.</param>
        /// <exception cref="ArgumentNullException">If array is null.</exception>
        /// <exception cref="RankException">If array is multidimensional.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If index is greater than end of the array or index is less than zero.</exception>
        /// <exception cref="ArgumentException">If number of elements in source vector is greater than those available in destination array.</exception>
        public void CopyTo(double[] array, int index)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            if (index < 0 || index >= array.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            if ((array.Length - index) < 3)
            {
                throw new ArgumentException(MathematicsErrorMessages.ArraySizeMismatch, nameof(index));
            }
            array[index] = X;
            array[index + 1] = Y;
            array[index + 2] = Z;
        }

        /// <summary>
        /// Returns the Euclidean distance between the this point and the given point.
        /// </summary>
        /// <param name="other">The second point.</param>
        /// <returns>The distance.</returns>
        public double Distance(Vector3 other) => Distance(this, other);

        /// <summary>
        /// Returns the Euclidean distance squared between this point and the given point.
        /// </summary>
        /// <param name="other">The second point.</param>
        /// <returns>The distance squared.</returns>
        public double DistanceSquared(Vector3 other) => DistanceSquared(this, other);

        /// <summary>
        /// Returns the dot product of two vectors.
        /// </summary>
        /// <param name="other">The second vector.</param>
        /// <returns>The dot product.</returns>
        public double Dot(Vector3 other) => Dot(this, other);

        /// <summary>
        /// Returns a boolean indicating whether the given Object is equal to this Vector3 instance.
        /// </summary>
        /// <param name="obj">The Object to compare against.</param>
        /// <returns>True if the Object is equal to this Vector3; False otherwise.</returns>
        public override bool Equals(object? obj) => obj is Vector3 other && Equals(other);

        /// <summary>
        /// Returns a boolean indicating whether the given Vector3 is equal to this Vector3 instance.
        /// </summary>
        /// <param name="other">The Vector3 to compare this instance to.</param>
        /// <returns>True if the other Vector3 is equal to this instance; False otherwise.</returns>
        public bool Equals(Vector3 other) => X == other.X && Y == other.Y && Z == other.Z;

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = (hashCode * -1521134295) + X.GetHashCode();
            hashCode = (hashCode * -1521134295) + Y.GetHashCode();
            return (hashCode * -1521134295) + Z.GetHashCode();
        }

        /// <summary>
        /// Determines if this value and the given vector are parallel.
        /// </summary>
        /// <param name="other">The second vector.</param>
        /// <param name="allowSmallError">If <see langword="true"/>, a small amount of error is
        /// disregarded, to account for floating point errors.</param>
        /// <returns><see langword="true"/> if the vectors are parallel; otherwise <see
        /// langword="false"/>.</returns>
        public bool IsParallel(Vector3 other, bool allowSmallError = true)
            => AreParallel(this, other, allowSmallError);

        /// <summary>
        /// Determines if this vector is nearly zero (all elements closer to zero than <see
        /// cref="DoubleConstants.NearlyZero"/>).
        /// </summary>
        /// <returns><see langword="true"/> if this vector is close to zero; otherwise <see
        /// langword="false"/>.</returns>
        public bool IsZero() => IsZero(this);

        /// <summary>
        /// Returns the length of the vector.
        /// </summary>
        /// <returns>The vector's length.</returns>
        public double Length() => Math.Sqrt(LengthSquared());

        /// <summary>
        /// Returns the length of the vector squared. This operation is cheaper than Length().
        /// </summary>
        /// <returns>The vector's length squared.</returns>
        public double LengthSquared() => (X * X) + (Y * Y) + (Z * Z);

        /// <summary>
        /// Returns a vector with the same direction as this vector, but with a length of 1.
        /// </summary>
        /// <returns>The normalized vector.</returns>
        public Vector3 Normalize() => Normalize(this);

        /// <summary>
        /// Returns the reflection of this vector off a surface that has the specified normal.
        /// </summary>
        /// <param name="normal">The normal of the surface being reflected off.</param>
        /// <returns>The reflected vector.</returns>
        public Vector3 Reflect(Vector3 normal) => Reflect(this, normal);

        /// <summary>
        /// Calculates the quaternion which represents the rotation from this vector to another.
        /// </summary>
        /// <param name="other">The second vector.</param>
        public Quaternion RotationTo(Vector3 other) => RotationTo(this, other);

        /// <summary>
        /// Returns a vector whose elements are the square root of each of this vector's elements.
        /// </summary>
        /// <returns>The square root vector.</returns>
        public Vector3 SquareRoot() => SquareRoot(this);

        /// <summary>
        /// Returns a String representing this Vector3 instance.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString() => ToString("G", CultureInfo.CurrentCulture);

        /// <summary>
        /// Returns a String representing this Vector3 instance, using the specified format to format individual elements.
        /// </summary>
        /// <param name="format">The format of individual elements.</param>
        /// <returns>The string representation.</returns>
        public string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

        /// <summary>
        /// Returns a String representing this Vector3 instance, using the specified format to format individual elements
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
            sb.Append('>');
            return sb.ToString();
        }

        /// <summary>
        /// Transforms this vector by the given matrix.
        /// </summary>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        public Vector3 Transform(Matrix4x4 matrix) => Transform(this, matrix);

        /// <summary>
        /// Transforms this vector by the given Quaternion rotation value.
        /// </summary>
        /// <param name="rotation">The rotation to apply.</param>
        /// <returns>The transformed vector.</returns>
        public Vector3 Transform(Quaternion rotation) => Transform(this, rotation);

        /// <summary>
        /// Transforms this vector normal by the given matrix.
        /// </summary>
        /// <param name="matrix">The transformation matrix.</param>
        /// <returns>The transformed vector.</returns>
        public Vector3 TransformNormal(Matrix4x4 matrix) => TransformNormal(this, matrix);

        /// <summary>
        /// Adds two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The summed vector.</returns>
        public static Vector3 Add(Vector3 left, Vector3 right) => left + right;

        /// <summary>
        /// Subtracts the second vector from the first.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The difference vector.</returns>
        public static Vector3 Subtract(Vector3 left, Vector3 right) => left - right;

        /// <summary>
        /// Multiplies two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The product vector.</returns>
        public static Vector3 Multiply(Vector3 left, Vector3 right) => left * right;

        /// <summary>
        /// Multiplies a vector by the given scalar.
        /// </summary>
        /// <param name="left">The source vector.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The scaled vector.</returns>
        public static Vector3 Multiply(Vector3 left, double right) => left * right;

        /// <summary>
        /// Multiplies a vector by the given scalar.
        /// </summary>
        /// <param name="left">The scalar value.</param>
        /// <param name="right">The source vector.</param>
        /// <returns>The scaled vector.</returns>
        public static Vector3 Multiply(double left, Vector3 right) => left * right;

        /// <summary>
        /// Divides the first vector by the second.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The vector resulting from the division.</returns>
        public static Vector3 Divide(Vector3 left, Vector3 right) => left / right;

        /// <summary>
        /// Divides the vector by the given scalar.
        /// </summary>
        /// <param name="left">The source vector.</param>
        /// <param name="divisor">The scalar value.</param>
        /// <returns>The result of the division.</returns>
        public static Vector3 Divide(Vector3 left, double divisor) => left / divisor;

        /// <summary>
        /// Negates a given vector.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The negated vector.</returns>
        public static Vector3 Negate(Vector3 value) => -value;

        /// <summary>
        /// Adds two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The summed vector.</returns>
        public static Vector3 operator +(Vector3 left, Vector3 right)
            => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

        /// <summary>
        /// Subtracts the second vector from the first.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The difference vector.</returns>
        public static Vector3 operator -(Vector3 left, Vector3 right)
            => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

        /// <summary>
        /// Multiplies two vectors together.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The product vector.</returns>
        public static Vector3 operator *(Vector3 left, Vector3 right)
            => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

        /// <summary>
        /// Multiplies a vector by the given scalar.
        /// </summary>
        /// <param name="left">The source vector.</param>
        /// <param name="right">The scalar value.</param>
        /// <returns>The scaled vector.</returns>
        public static Vector3 operator *(Vector3 left, double right)
            => new(left.X * right, left.Y * right, left.Z * right);

        /// <summary>
        /// Multiplies a vector by the given scalar.
        /// </summary>
        /// <param name="left">The scalar value.</param>
        /// <param name="right">The source vector.</param>
        /// <returns>The scaled vector.</returns>
        public static Vector3 operator *(double left, Vector3 right)
            => new(left * right.X, left * right.Y, left * right.Z);

        /// <summary>
        /// Divides the first vector by the second.
        /// </summary>
        /// <param name="left">The first source vector.</param>
        /// <param name="right">The second source vector.</param>
        /// <returns>The vector resulting from the division.</returns>
        public static Vector3 operator /(Vector3 left, Vector3 right)
            => new(left.X / right.X, left.Y / right.Y, left.Z / right.Z);

        /// <summary>
        /// Divides the vector by the given scalar.
        /// </summary>
        /// <param name="value1">The source vector.</param>
        /// <param name="value2">The scalar value.</param>
        /// <returns>The result of the division.</returns>
        public static Vector3 operator /(Vector3 value1, double value2)
            => new(value1.X / value2, value1.Y / value2, value1.Z / value2);

        /// <summary>
        /// Negates a given vector.
        /// </summary>
        /// <param name="value">The source vector.</param>
        /// <returns>The negated vector.</returns>
        public static Vector3 operator -(Vector3 value) => new(-value.X, -value.Y, -value.Z);

        /// <summary>
        /// Returns a boolean indicating whether the two given vectors are equal.
        /// </summary>
        /// <param name="left">The first vector to compare.</param>
        /// <param name="right">The second vector to compare.</param>
        /// <returns>True if the vectors are equal; False otherwise.</returns>
        public static bool operator ==(Vector3 left, Vector3 right)
            => left.X == right.X && left.Y == right.Y && left.Z == right.Z;

        /// <summary>
        /// Returns a boolean indicating whether the two given vectors are not equal.
        /// </summary>
        /// <param name="left">The first vector to compare.</param>
        /// <param name="right">The second vector to compare.</param>
        /// <returns>True if the vectors are not equal; False if they are equal.</returns>
        public static bool operator !=(Vector3 left, Vector3 right)
            => left.X != right.X || left.Y != right.Y || left.Z != right.Z;

        /// <summary>
        /// Converts between implementations of vectors.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator Vector3(System.Numerics.Vector3 value)
            => new(value.X, value.Y, value.Z);

        /// <summary>
        /// Converts between implementations of vectors.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static explicit operator Vector3(HugeNumbers.Vector3 value)
            => new((double)value.X, (double)value.Y, (double)value.Z);

        /// <summary>
        /// Converts between implementations of vectors.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static explicit operator Vector3(Decimals.Vector3 value)
            => new((double)value.X, (double)value.Y, (double)value.Z);

        /// <summary>
        /// Converts between implementations of vectors.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static explicit operator System.Numerics.Vector3(Vector3 value)
            => new((float)value.X, (float)value.Y, (float)value.Z);
    }
}
