![build](https://img.shields.io/github/workflow/status/Tavenem/Mathematics/publish/main) [![NuGet downloads](https://img.shields.io/nuget/dt/Tavenem.Mathematics)](https://www.nuget.org/packages/Tavenem.Mathematics/)

Tavenem.Mathematics
==

Tavenem.Mathematics is a collection of math utilities. It includes:
- An assortment of mathematic and scientific constants, as double, decimal, and
  [HugeNumber](https://github.com/Tavenem/HugeNumber) values (available in the `DecimalConstants`,
  `DoubleConstants`, and `HugeNumberConstants` static classes)
- Versions of some types from the `System.Numerics` namespace, modified to use double, decimal, or
  [HugeNumber](https://github.com/Tavenem/HugeNumber) as the base value type, for cases when greater
  precision is required.
  
  Includes:
    - `Matrix3x2`
    - `Matrix4x4`
    - `Plane`
    - `Quaternion`
    - `Vector2`
    - `Vector3`
    - `Vector4`
  
  Note that these types are not SIMD-enabled and will not benefit from the high performance of the
  native `System.Numerics` types. Those should be preferred whenever performance is valued over
  precision.
  
- Classes defining geometric shapes, with versions based on decimal, double, and
  [HugeNumber](https://github.com/Tavenem/HugeNumber), including:
    - `Capsule` (a swept sphere)
    - `Cone`
    - `Cuboid`
    - `Cylinder`
    - `Ellipsoid`
    - `Frustum`
    - `HollowSphere` (a sphere with two radii, representing a shell with a thickness)
    - `Line` (2-dimensional)
    - `SinglePoint` (1-dimensional)
    - `Sphere`
    - `Torus`

- Also includes the `FloatRange` struct, which specifies a floating point value as a minimum,
  maximum, and average value.

## Installation

Tavenem.Mathematics is available as a [NuGet package](https://www.nuget.org/packages/Tavenem.Mathematics/).

## Roadmap

Tavenem.Mathematics' latest preview release targets .NET 7, which is also in preview. When a stable release of .NET 7 is published, a new stable release of Tavenem.Mathematics will follow shortly.

## Contributing

Contributions are always welcome. Pull requests with well-tested code that improves the performance of the library are especially encouraged. Please carefully read the [contributing](docs/CONTRIBUTING.md) document to learn more before submitting issues or pull requests.

## Code of conduct

Please read the [code of conduct](docs/CODE_OF_CONDUCT.md) before engaging with our community, including but not limited to submitting or replying to an issue or pull request.