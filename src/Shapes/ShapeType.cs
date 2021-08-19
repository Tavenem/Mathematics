namespace Tavenem.Mathematics
{
    /// <summary>
    /// Identifies the type of an <c>IShape</c>.
    /// </summary>
    public enum ShapeType
    {
        /// <summary>
        /// No known type.
        /// </summary>
        None = 0,

        /// <summary>
        /// Capsule
        /// </summary>
        Capsule = 1,

        /// <summary>
        /// Cone
        /// </summary>
        Cone = 2,

        /// <summary>
        /// Cuboid
        /// </summary>
        Cuboid = 3,

        /// <summary>
        /// Cylinder
        /// </summary>
        Cylinder = 4,

        /// <summary>
        /// Ellipsoid
        /// </summary>
        Ellipsoid = 5,

        /// <summary>
        /// Frustum
        /// </summary>
        Frustum = 6,

        /// <summary>
        /// HollowSphere
        /// </summary>
        HollowSphere = 7,

        /// <summary>
        /// Line
        /// </summary>
        Line = 8,

        /// <summary>
        /// SinglePoint
        /// </summary>
        SinglePoint = 9,

        /// <summary>
        /// Sphere
        /// </summary>
        Sphere = 10,

        /// <summary>
        /// Torus
        /// </summary>
        Torus = 11,
    }
}
