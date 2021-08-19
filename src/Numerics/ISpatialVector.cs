﻿using System.Numerics;

namespace Tavenem.Mathematics;

/// <summary>
/// Extends <see cref="IVector{TSelf, TScalar}"/> with additional common methods for vectors in
/// dimensional space.
/// </summary>
/// <typeparam name="TSelf">The type of the vector.</typeparam>
/// <typeparam name="TScalar">The type of the scalar value.</typeparam>
public interface ISpatialVector<TSelf, TScalar> : IVector<TSelf, TScalar>
    where TSelf : ISpatialVector<TSelf, TScalar>
    where TScalar : IFloatingPoint<TScalar>
{
    /// <summary>
    /// Restricts a vector between a min and max value.
    /// </summary>
    /// <param name="value1">The source vector.</param>
    /// <param name="min">The minimum value.</param>
    /// <param name="max">The maximum value.</param>
    public static abstract TSelf Clamp(TSelf value1, TSelf min, TSelf max);

    /// <summary>
    /// Returns the Euclidean distance between the two given points.
    /// </summary>
    /// <param name="value1">The first point.</param>
    /// <param name="value2">The second point.</param>
    /// <returns>The distance.</returns>
    public static abstract TScalar Distance(TSelf value1, TSelf value2);

    /// <summary>
    /// Returns the Euclidean distance squared between the two given points.
    /// </summary>
    /// <param name="value1">The first point.</param>
    /// <param name="value2">The second point.</param>
    /// <returns>The distance squared.</returns>
    public static abstract TScalar DistanceSquared(TSelf value1, TSelf value2);

    /// <summary>
    /// Determines if a vector is nearly zero (all elements closer to zero than <see
    /// cref="IComparableToZero{TSelf}.NearlyZero"/>).
    /// </summary>
    /// <param name="value">A vector to test.</param>
    /// <returns><see langword="true"/> if the vector is close to zero; otherwise <see
    /// langword="false"/>.</returns>
    public static abstract bool IsNearlyZero(TSelf value);

    /// <summary>
    /// Returns the length of the vector.
    /// </summary>
    /// <returns>The vector's length.</returns>
    public static abstract TScalar Length(TSelf value);

    /// <summary>
    /// Returns the length of the vector squared.
    /// </summary>
    /// <returns>The vector's length squared.</returns>
    public static abstract TScalar LengthSquared(TSelf value);

    /// <summary>
    /// Linearly interpolates between two vectors based on the given weighting.
    /// </summary>
    /// <param name="value1">The first source vector.</param>
    /// <param name="value2">The second source vector.</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of the second source
    /// vector.</param>
    /// <returns>The interpolated vector.</returns>
    public static abstract TSelf Lerp(TSelf value1, TSelf value2, TScalar amount);

    /// <summary>
    /// Returns a vector with the same direction as the given vector, but with a length of 1.
    /// </summary>
    /// <param name="value">The vector to normalize.</param>
    /// <returns>The normalized vector.</returns>
    public static abstract TSelf Normalize(TSelf value);

    /// <summary>
    /// Returns the reflection of a vector off a surface that has the specified normal.
    /// </summary>
    /// <param name="vector">The source vector.</param>
    /// <param name="normal">The normal of the surface being reflected off.</param>
    /// <returns>The reflected vector.</returns>
    public static abstract TSelf Reflect(TSelf vector, TSelf normal);

    /// <summary>
    /// Transforms a vector by the given matrix.
    /// </summary>
    /// <param name="position">The source vector.</param>
    /// <param name="matrix">The transformation matrix.</param>
    /// <returns>The transformed vector.</returns>
    public static abstract TSelf Transform(TSelf position, Matrix4x4<TScalar> matrix);

    /// <summary>
    /// Transforms a vector by the given Quaternion rotation value.
    /// </summary>
    /// <param name="value">The source vector to be rotated.</param>
    /// <param name="rotation">The rotation to apply.</param>
    /// <returns>The transformed vector.</returns>
    public static abstract TSelf Transform(TSelf value, Quaternion<TScalar> rotation);

    /// <summary>
    /// Multiplies a vector by the given scalar.
    /// </summary>
    /// <param name="left">The scalar value.</param>
    /// <param name="right">The source vector.</param>
    /// <returns>The scaled vector.</returns>
    public static abstract TSelf operator *(TScalar left, TSelf right);

    /// <summary>
    /// Multiplies a vector by the given scalar.
    /// </summary>
    /// <param name="left">The source vector.</param>
    /// <param name="right">The scalar value.</param>
    /// <returns>The scaled vector.</returns>
    public static abstract TSelf operator *(TSelf left, TScalar right);

    /// <summary>
    /// Divides the vector by the given scalar.
    /// </summary>
    /// <param name="value1">The source vector.</param>
    /// <param name="value2">The scalar value.</param>
    /// <returns>The result of the division.</returns>
    public static abstract TSelf operator /(TSelf value1, TScalar value2);
}
