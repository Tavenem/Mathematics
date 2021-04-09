using System;

namespace Tavenem.Mathematics.Decimals
{
    /// <summary>
    /// Provides information about the properties of a geometric shape.
    /// </summary>
    [JsonInterfaceConverter(typeof(ShapeConverter))]
    public interface IShape : IEquatable<IShape>
    {
        /// <summary>
        /// A circular radius which fully contains the shape. Read-only.
        /// </summary>
        decimal ContainingRadius { get; }

        /// <summary>
        /// The point on this shape highest on the Y axis.
        /// </summary>
        Vector3 HighestPoint { get; }

        /// <summary>
        /// The point on this shape lowest on the Y axis.
        /// </summary>
        Vector3 LowestPoint { get; }

        /// <summary>
        /// The position of the shape in 3D space.
        /// </summary>
        Vector3 Position { get; }

        /// <summary>
        /// The rotation of this shape in 3D space.
        /// </summary>
        Quaternion Rotation { get; }

        /// <summary>
        /// Identifies the type of this <see cref="IShape"/>.
        /// </summary>
        ShapeType ShapeType { get; }

        /// <summary>
        /// The length of the shortest of the three dimensions of this shape.
        /// </summary>
        /// <remarks>
        /// Note: this is not the length of smallest possible cross-section, but of the total length
        /// of the shape along any of the three primary axes of 3D space, prior to any rotation.
        /// </remarks>
        decimal SmallestDimension { get; }

        /// <summary>
        /// The total volume of the shape. Read-only.
        /// </summary>
        decimal Volume { get; }

        /// <summary>
        /// Gets a deep clone of this instance with its <see cref="Position"/> set to the given
        /// value.
        /// </summary>
        /// <param name="position">The new <see cref="Position"/> for the clone.</param>
        /// <returns>A deep clone of this instance with its <see cref="Position"/> set to the given
        /// value.</returns>
        IShape GetCloneAtPosition(Vector3 position);

        /// <summary>
        /// Gets a deep clone of this instance with its <see cref="Rotation"/> set to the given
        /// value.
        /// </summary>
        /// <param name="rotation">The new <see cref="Rotation"/> for the clone.</param>
        /// <returns>A deep clone of this instance with its <see cref="Rotation"/> set to the given
        /// value.</returns>
        IShape GetCloneWithRotation(Quaternion rotation);

        /// <summary>
        /// Determines if this instance intersects with the given <see cref="IShape"/>.
        /// </summary>
        /// <param name="shape">The <see cref="IShape"/> to check for intersection with this
        /// instance.</param>
        /// <returns><see langword="true"/> if this instance intersects with the given <see
        /// cref="IShape"/>; otherwise <see langword="false"/>.</returns>
        bool Intersects(IShape shape);

        /// <summary>
        /// Determines if a given point lies within this shape.
        /// </summary>
        /// <param name="point">A point in the same 3D space as this shape.</param>
        /// <returns>True if the point is within (or tangent to) this shape.</returns>
        bool IsPointWithin(Vector3 point);

        /// <summary>
        /// Gets a copy of this <see cref="IShape"/> whose dimensions have been multiplied by the
        /// given factor.
        /// </summary>
        /// <param name="factor">The amount by which to scale this <see cref="IShape"/>'s
        /// size.</param>
        /// <returns>A copy of this <see cref="IShape"/> whose dimensions have been multiplied by
        /// the given factor.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="factor"/> must be &gt;= 0.
        /// </exception>
        IShape ScaleByDimension(decimal factor);

        /// <summary>
        /// Gets a copy of this <see cref="IShape"/> whose dimensions have beens scaled such that
        /// its volume will be multiplied by the given factor.
        /// </summary>
        /// <param name="factor">The amount by which to scale this <see cref="IShape"/>'s
        /// volume.</param>
        /// <returns>A copy of this <see cref="IShape"/> whose dimensions have been scaled such that
        /// its volume will be multiplied by the given factor.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="factor"/> must be &gt;= 0.
        /// </exception>
        IShape ScaleVolume(decimal factor);
    }
}
