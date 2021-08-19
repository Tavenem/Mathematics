using System.Text.Json.Serialization;

namespace Tavenem.Mathematics;

/// <summary>
/// Provides information about the properties of a line as a shape.
/// </summary>
public readonly struct Line<TScalar> : IShape<Line<TScalar>, TScalar>
    where TScalar : IFloatingPoint<TScalar>
{
    private readonly Vector3<TScalar> _start;
    private readonly Vector3<TScalar> _end;

    /// <summary>
    /// A circular radius which fully contains the shape.
    /// </summary>
    [JsonIgnore]
    public TScalar ContainingRadius { get; }

    /// <summary>
    /// The point on this shape highest on the Y axis.
    /// </summary>
    [JsonIgnore]
    public Vector3<TScalar> HighestPoint { get; }

    /// <summary>
    /// The length of the line.
    /// </summary>
    [JsonIgnore]
    public TScalar Length { get; }

    /// <summary>
    /// The point on this shape lowest on the Y axis.
    /// </summary>
    [JsonIgnore]
    public Vector3<TScalar> LowestPoint { get; }

    /// <summary>
    /// The vector which the line follows.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Note that <see cref="Path"/> does not match the position and extent of the line itself,
    /// since <see cref="Position"/> refers to the center of the line.
    /// </para>
    /// <para>
    /// The line, therefore, has the same direction as <see cref="Path"/>, but
    /// ends half its length away from <see cref="Position"/>, while extending in the reverse
    /// direction from <see cref="Position"/> for the same distance.
    /// </para>
    /// </remarks>
    public Vector3<TScalar> Path { get; }

    /// <summary>
    /// The position of the shape in 3D space.
    /// </summary>
    public Vector3<TScalar> Position { get; }

    /// <summary>
    /// The rotation of this shape in 3D space. Always <see cref="Quaternion{TScalar}.MultiplicativeIdentity"/> for
    /// line, whose <see cref="Path"/> defines its orientation.
    /// </summary>
    [JsonIgnore]
    public Quaternion<TScalar> Rotation => Quaternion<TScalar>.MultiplicativeIdentity;

    /// <summary>
    /// Identifies the type of this <see cref="IShape{TScalar}"/>.
    /// </summary>
    public ShapeType ShapeType => ShapeType.Line;

    /// <summary>
    /// The length of the shortest of the three dimensions of this shape. Always 0 for line,
    /// which has a 0-length cross-section.
    /// </summary>
    /// <remarks>
    /// Note: this is not the length of smallest possible cross-section, but of the total length
    /// of the shape along any of the three primary axes of 3D space, prior to any rotation.
    /// </remarks>
    [JsonIgnore]
    public TScalar SmallestDimension => TScalar.Zero;

    /// <summary>
    /// The total volume of the shape. Always 0 for line, which has a 0-length
    /// cross-section.
    /// </summary>
    [JsonIgnore]
    public TScalar Volume => TScalar.Zero;

    /// <summary>
    /// Initializes a new instance of line.
    /// </summary>
    /// <param name="path">The vector which the line follows.</param>
    /// <param name="position">
    /// <para>
    /// The position of the shape in 3D space. Note that as with all <see
    /// cref="IShape{TScalar}"/> instances, this is the center of the line, not either
    /// endpoint.
    /// </para>
    /// <para>
    /// See <see cref="Create(Vector3{TScalar}, Vector3{TScalar})"/> to construct a line
    /// from an endpoint.
    /// </para>
    /// </param>
    [JsonConstructor]
    public Line(Vector3<TScalar> path, Vector3<TScalar> position)
    {
        Path = path;
        Position = position;

        Length = Vector3<TScalar>.Length(path);

        var _halfPath = Vector3<TScalar>.Normalize(path) * (Length / (TScalar.One + TScalar.One));
        _start = Position - _halfPath;
        _end = _start + path;

        ContainingRadius = Length;
        if (_end.Y >= _start.Y)
        {
            HighestPoint = _end;
            LowestPoint = _start;
        }
        else
        {
            HighestPoint = _start;
            LowestPoint = _end;
        }
    }

    /// <summary>
    /// Initializes a new instance of line.
    /// </summary>
    /// <param name="path">The vector which the line follows.</param>
    public Line(Vector3<TScalar> path) : this(path, Vector3<TScalar>.Zero) { }

    /// <summary>
    /// Initializes a new instance of line.
    /// </summary>
    public Line() : this(Vector3<TScalar>.Zero) { }

    /// <summary>
    /// Gets a new line which originates from the given <paramref name="start"/>
    /// position.
    /// </summary>
    /// <param name="start">The origin point of the line.</param>
    /// <param name="path">The vector which the line follows.</param>
    /// <returns>The new line</returns>
    public static Line<TScalar> Create(Vector3<TScalar> start, Vector3<TScalar> path) => new(
        path,
        start
        + (Vector3<TScalar>.Normalize(path)
        * (Vector3<TScalar>.Length(path) / (TScalar.One + TScalar.One))));

    private static void ComputeIntersection(
        TScalar mF00,
        TScalar mF10,
        TScalar mB,
        TScalar[] sValue,
        int[] classify,
        out int[] edge,
        out TScalar[,] end)
    {
        edge = new int[2];
        end = new TScalar[2, 2];

        var half = NumberValues.Half<TScalar>();
        if (classify[0] < 0)
        {
            edge[0] = 0;
            end[0, 0] = TScalar.Zero;
            end[0, 1] = mF00 / mB;
            if (end[0, 1] is < 0 or > 1)
            {
                end[0, 1] = half;
            }
            if (classify[1] == 0)
            {
                edge[1] = 3;
                end[1, 0] = sValue[1];
                end[1, 1] = TScalar.One;
            }
            else
            {
                edge[1] = 1;
                end[1, 0] = TScalar.One;
                end[1, 1] = mF10 / mB;
                if (end[1, 1] is < 0 or > 1)
                {
                    end[1, 1] = half;
                }
            }
        }
        else if (classify[0] == 0)
        {
            edge[0] = 2;
            end[0, 0] = sValue[0];
            end[0, 1] = TScalar.Zero;
            if (classify[1] < 0)
            {
                edge[1] = 0;
                end[1, 0] = TScalar.Zero;
                end[1, 1] = mF00 / mB;
                if (end[1, 1] is < 0 or > 1)
                {
                    end[1, 1] = half;
                }
            }
            else if (classify[1] == 0)
            {
                edge[1] = 3;
                end[1, 0] = sValue[1];
                end[1, 1] = TScalar.One;
            }
            else
            {
                edge[1] = 1;
                end[1, 0] = TScalar.One;
                end[1, 1] = mF10 / mB;
                if (end[1, 1] is < 0 or > 1)
                {
                    end[1, 1] = half;
                }
            }
        }
        else if (classify[0] > 0)
        {
            edge[0] = 1;
            end[0, 0] = TScalar.One;
            end[0, 1] = mF10 / mB;
            if (end[0, 1] is < 0 or > 1)
            {
                end[0, 1] = half;
            }
            if (classify[1] == 0)
            {
                edge[1] = 3;
                end[1, 0] = sValue[1];
                end[1, 1] = TScalar.One;
            }
            else
            {
                edge[1] = 0;
                end[1, 0] = TScalar.Zero;
                end[1, 1] = mF00 / mB;
                if (end[1, 1] is < 0 or > 1)
                {
                    end[1, 1] = half;
                }
            }
        }
    }

    private static void ComputeMinimumParameters(
        TScalar mG00,
        TScalar mG01,
        TScalar mG10,
        TScalar mG11,
        TScalar mB,
        TScalar mC,
        TScalar mE,
        int[] edge,
        TScalar[,] end,
        out TScalar[] parameters)
    {
        parameters = new TScalar[2];
        var delta = end[1, 1] - end[0, 1];
        var h0 = delta * ((-mB * end[0, 0]) + (mC * end[0, 1]) - mE);
        if (h0 >= TScalar.Zero)
        {
            if (edge[0] == 0)
            {
                parameters[0] = TScalar.Zero;
                parameters[1] = GetClampedRoot(mC, mG00, mG01);
            }
            else if (edge[0] == 1)
            {
                parameters[0] = TScalar.One;
                parameters[1] = GetClampedRoot(mC, mG10, mG11);
            }
            else
            {
                parameters[0] = end[0, 0];
                parameters[1] = end[0, 1];
            }
        }
        else
        {
            var h1 = delta * ((-mB * end[1, 0]) + (mC * end[1, 1]) - mE);
            if (h1 <= TScalar.Zero)
            {
                if (edge[1] == 0)
                {
                    parameters[0] = TScalar.Zero;
                    parameters[1] = GetClampedRoot(mC, mG00, mG01);
                }
                else if (edge[1] == 1)
                {
                    parameters[0] = TScalar.One;
                    parameters[1] = GetClampedRoot(mC, mG10, mG11);
                }
                else
                {
                    parameters[0] = end[1, 0];
                    parameters[1] = end[1, 1];
                }
            }
            else
            {
                var z = TScalar.Min(TScalar.Max(h0 / (h0 - h1), TScalar.Zero), TScalar.One);
                var omz = TScalar.One - z;
                parameters[0] = (omz * end[0, 0]) + (z * end[1, 0]);
                parameters[1] = (omz * end[0, 1]) + (z * end[1, 1]);
            }
        }
    }

    private static TScalar GetClampedRoot(TScalar slope, TScalar h0, TScalar h1)
    {
        if (h0 < TScalar.Zero)
        {
            if (h1 > TScalar.Zero)
            {
                var result = -h0 / slope;
                return result > TScalar.One ? NumberValues.Half<TScalar>() : result;
            }
            return TScalar.One;
        }
        return TScalar.Zero;
    }

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="Position"/> set to the given
    /// value.
    /// </summary>
    /// <param name="position">The new <see cref="Position"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="Position"/> set to the given value.
    /// </returns>
    public IShape<TScalar> GetCloneAtPosition(Vector3<TScalar> position) => GetTypedCloneAtPosition(position);

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="Rotation"/> set to the given
    /// value.
    /// </summary>
    /// <param name="rotation">The new <see cref="Rotation"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="Rotation"/> set to the given value.
    /// </returns>
    /// <remarks>Returns the same instance, since rotation does not affect spheres.</remarks>
    public IShape<TScalar> GetCloneWithRotation(Quaternion<TScalar> rotation) => GetTypedCloneWithRotation(rotation);

    /// <summary>
    /// Gets a copy of this instance whose dimensions have been multiplied by the
    /// given factor.
    /// </summary>
    /// <param name="factor">The amount by which to scale this instance's size.</param>
    /// <returns>
    /// A copy of this instance whose dimensions have been multiplied by the given factor.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="factor"/> must be &gt;= 0.
    /// </exception>
    public IShape<TScalar> GetScaledByDimension(TScalar factor) => GetTypedScaledByDimension(factor);

    /// <summary>
    /// Gets a copy of this instance whose dimensions have beens scaled such that
    /// its volume will be multiplied by the given factor.
    /// </summary>
    /// <param name="factor">The amount by which to scale this instance's volume.</param>
    /// <returns>
    /// A copy of this instance whose dimensions have been scaled such that
    /// its volume will be multiplied by the given factor.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="factor"/> must be &gt;= 0.
    /// </exception>
    public IShape<TScalar> GetScaledByVolume(TScalar factor) => this;

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="IShape{TScalar}.Position"/> set to the given
    /// value.
    /// </summary>
    /// <param name="position">The new <see cref="IShape{TScalar}.Position"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="IShape{TScalar}.Position"/> set to the given value.
    /// </returns>
    public Line<TScalar> GetTypedCloneAtPosition(Vector3<TScalar> position) => new(Path, position);

    /// <summary>
    /// Gets a deep clone of this instance with its <see cref="IShape{TScalar}.Rotation"/> set to the given
    /// value.
    /// </summary>
    /// <param name="rotation">The new <see cref="IShape{TScalar}.Rotation"/> for the clone.</param>
    /// <returns>
    /// A deep clone of this instance with its <see cref="IShape{TScalar}.Rotation"/> set to the given value.
    /// </returns>
    /// <remarks>Returns the same instance, since rotation does not affect spheres.</remarks>
    public Line<TScalar> GetTypedCloneWithRotation(Quaternion<TScalar> rotation)
        => new(Vector3<TScalar>.Transform(Path, rotation), Position);

    /// <summary>
    /// Gets a copy of this instance whose dimensions have been multiplied by the
    /// given factor.
    /// </summary>
    /// <param name="factor">The amount by which to scale this instance's size.</param>
    /// <returns>
    /// A copy of this instance whose dimensions have been multiplied by the given factor.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="factor"/> must be &gt;= 0.
    /// </exception>
    public Line<TScalar> GetTypedScaledByDimension(TScalar factor)
    {
        if (factor < TScalar.Zero)
        {
            throw new ArgumentOutOfRangeException(nameof(factor), $"{nameof(factor)} must be >= 0");
        }

        return new Line<TScalar>(
            factor == TScalar.Zero
                ? Vector3<TScalar>.Zero
                :Path * factor,
            Position);
    }

    /// <summary>
    /// Gets a copy of this instance whose dimensions have beens scaled such that
    /// its volume will be multiplied by the given factor.
    /// </summary>
    /// <param name="factor">The amount by which to scale this instance's volume.</param>
    /// <returns>
    /// A copy of this instance whose dimensions have been scaled such that
    /// its volume will be multiplied by the given factor.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="factor"/> must be &gt;= 0.
    /// </exception>
    public Line<TScalar> GetTypedScaledByVolume(TScalar factor) => this;

    /// <summary>
    /// Calculates the distance to the given <paramref name="point"/>.
    /// </summary>
    /// <param name="point">
    /// A <see cref="Vector3{TScalar}"/> to which the distance is to be calculated.
    /// </param>
    /// <param name="closestPoint">
    /// When the method returns, will be set to the closest point on
    /// this instance to the given <paramref name="point"/>.
    /// </param>
    /// <returns>The distance to the given <paramref name="point"/>.</returns>
    public TScalar GetDistanceTo(Vector3<TScalar> point, out Vector3<TScalar> closestPoint)
    {
        var diff = point - _end;
        closestPoint = _start;
        var t = Vector3<TScalar>.Dot(Path, diff);
        if (t >= TScalar.Zero)
        {
            closestPoint = _end;
        }
        else
        {
            var sqrLn = Vector3<TScalar>.Dot(Path, Path);
            if (sqrLn > TScalar.Zero)
            {
                t /= sqrLn;
                closestPoint = _start + (t * Path);
            }
        }
        diff = point - closestPoint;
        return Vector3<TScalar>.Length(diff);
    }

    /// <summary>
    /// Calculates the distance to the given <paramref name="point"/>.
    /// </summary>
    /// <param name="point">
    /// The <see cref="SinglePoint{TScalar}"/> to which the distance is to be calculated.
    /// </param>
    /// <param name="closestPoint">
    /// When the method returns, will be set to the closest point on
    /// this instance to the given <paramref name="point"/>.
    /// </param>
    /// <returns>The distance to the given <paramref name="point"/>.</returns>
    public TScalar GetDistanceTo(SinglePoint<TScalar> point, out Vector3<TScalar> closestPoint)
        => GetDistanceTo(point.Position, out closestPoint);

    /// <summary>
    /// Calculates the distance to the given <paramref name="line"/>.
    /// </summary>
    /// <param name="line">The line to which the distance is to be
    /// calculated.</param>
    /// <param name="closestPoint">
    /// When the method returns, will be set to the closest point on
    /// this instance to the given <paramref name="line"/>.
    /// </param>
    /// <param name="closestPointOther">
    /// When the method returns, will be set to the closest
    /// point on the given <paramref name="line"/> to this instance.
    /// </param>
    /// <returns>The distance to the given <paramref name="line"/>.</returns>
    public TScalar GetDistanceTo(Line<TScalar> line, out Vector3<TScalar> closestPoint, out Vector3<TScalar> closestPointOther)
    {
        var startDiff = _start - line._start;
        var mA = Vector3<TScalar>.Dot(Path, Path);
        var mB = Vector3<TScalar>.Dot(Path, line.Path);
        var mC = Vector3<TScalar>.Dot(line.Path, line.Path);
        var mD = Vector3<TScalar>.Dot(Path, startDiff);
        var mE = Vector3<TScalar>.Dot(line.Path, startDiff);
        var mF00 = mD;
        var mF10 = mF00 + mA;
        var mF01 = mF00 - mB;
        var mF11 = mF10 - mB;
        var mG00 = -mE;
        var mG10 = mG00 - mB;
        var mG01 = mG00 + mC;
        var mG11 = mG10 + mC;
        var parameters = new TScalar[2];

        if (mA > TScalar.Zero && mC > TScalar.Zero)
        {
            var sValue = new TScalar[2];
            sValue[0] = GetClampedRoot(mA, mF00, mF10);
            sValue[1] = GetClampedRoot(mA, mF01, mF11);
            var classify = new int[2];
            for (var i = 0; i < 2; ++i)
            {
                if (sValue[i] <= TScalar.Zero)
                {
                    classify[i] = -1;
                }
                else if (sValue[i] >= TScalar.One)
                {
                    classify[i] = 1;
                }
                else
                {
                    classify[i] = 0;
                }
            }
            if (classify[0] == -1 && classify[1] == -1)
            {
                parameters[0] = TScalar.Zero;
                parameters[1] = GetClampedRoot(mC, mG00, mG01);
            }
            else if (classify[0] == 1 && classify[1] == 1)
            {
                parameters[0] = TScalar.One;
                parameters[1] = GetClampedRoot(mC, mG10, mG11);
            }
            else
            {
                ComputeIntersection(mF00, mF10, mB, sValue, classify, out var edge, out var end);
                ComputeMinimumParameters(mG00, mG01, mG10, mG11, mB, mC, mE, edge, end, out parameters);
            }
        }
        else if (mA > TScalar.Zero)
        {
            parameters[0] = GetClampedRoot(mA, mF00, mF10);
            parameters[1] = TScalar.Zero;
        }
        else if (mC > TScalar.Zero)
        {
            parameters[0] = TScalar.Zero;
            parameters[1] = GetClampedRoot(mC, mG00, mG01);
        }
        else
        {
            parameters[0] = TScalar.Zero;
            parameters[1] = TScalar.Zero;
        }

        closestPoint = ((TScalar.One - parameters[0]) * _start) + (parameters[0] * _end);
        closestPointOther = ((TScalar.One - parameters[1]) * line._start) + (parameters[1] * line._end);
        var diff = closestPoint - closestPointOther;
        return Vector3<TScalar>.Length(diff);
    }

    /// <summary>
    /// Determines if this instance intersects with the given <see cref="IShape{TScalar}"/>.
    /// </summary>
    /// <param name="shape">
    /// The <see cref="IShape{TScalar}"/> to check for intersection with this instance.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if this instance intersects with the given <see
    /// cref="IShape{TScalar}"/>; otherwise <see langword="false"/>.
    /// </returns>
    public bool Intersects(IShape<TScalar> shape)
    {
        if (shape is SinglePoint<TScalar> point)
        {
            return GetDistanceTo(point, out _).IsNearlyZero();
        }
        else if (shape is Line<TScalar> line)
        {
            return Intersects(line);
        }
        else
        {
            return shape.Intersects(this);
        }
    }

    /// <summary>
    /// Determines if a given point lies within this shape.
    /// </summary>
    /// <param name="point">A point in the same 3D space as this shape.</param>
    /// <returns>
    /// <see langword="true"/> if the point is within (or tangent to) this shape; otherwise <see langword="false"/>.
    /// </returns>
    public bool IsPointWithin(Vector3<TScalar> point) => GetDistanceTo(point, out var _).IsNearlyZero();

    /// <summary>
    /// Determines whether the specified line is equivalent to the current object.
    /// </summary>
    /// <param name="other">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true"/> if the specified line is equivalent to the
    /// current object; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(Line<TScalar> other) => other.Path == Path && other.Position == Position;

    /// <summary>
    /// Determines whether the specified <see cref="IShape{TScalar}"/> is equivalent to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns>
    /// <see langword="true"/> if the specified <see cref="IShape{TScalar}"/> is equivalent to the
    /// current object; otherwise, <see langword="false"/>.
    /// </returns>
    public bool Equals(IShape<TScalar>? obj) => obj is Line<TScalar> other && Equals(other);

    /// <summary>
    /// Indicates whether this instance and a specified object are equal.
    /// </summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns>
    /// <see langword="true"/> if obj and this instance are the same type and represent
    /// the same value; otherwise, <see langword="false"/>.
    /// </returns>
    public override bool Equals(object? obj) => obj is Line<TScalar> shape && Equals(shape);

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    public override int GetHashCode() => HashCode.Combine(Path, Position);

    private bool Intersects(Line<TScalar> line)
    {
        var startDiff = line._start - _start;
        var crossPaths = Vector3<TScalar>.Cross(Path, line.Path);

        if (!Vector3<TScalar>.Dot(startDiff, crossPaths).IsNearlyZero())
        {
            return false;
        }

        var s = Vector3<TScalar>.Dot(Vector3<TScalar>.Cross(startDiff, line.Path), crossPaths)
            / Vector3<TScalar>.Dot(crossPaths, crossPaths);
        return s >= TScalar.Zero && s <= TScalar.One;
    }

    /// <summary>
    /// Returns a boolean indicating whether the two given shapes are equal.
    /// </summary>
    /// <param name="left">The first shapes to compare.</param>
    /// <param name="right">The second shapes to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the shapes are equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool operator ==(Line<TScalar> left, IShape<TScalar> right)
        => left.Equals(right);

    /// <summary>
    /// Returns a boolean indicating whether the two given shapes are not equal.
    /// </summary>
    /// <param name="left">The first shapes to compare.</param>
    /// <param name="right">The second shapes to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the shapes are not equal; otherwise <see langword="false"/>.
    /// </returns>
    public static bool operator !=(Line<TScalar> left, IShape<TScalar> right)
        => !(left == right);
}
