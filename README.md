![build](https://img.shields.io/github/actions/workflow/status/Tavenem/Mathematics/publish.yml?branch=main) [![NuGet downloads](https://img.shields.io/nuget/dt/Tavenem.Mathematics)](https://www.nuget.org/packages/Tavenem.Mathematics/)

Tavenem.Mathematics
==

Tavenem.Mathematics is a collection of math utilities. It includes:
- An assortment of mathematic and scientific constants, as double and decimal values (available in the `DecimalConstants` and `DoubleConstants` static classes)
- Versions of some types from the `System.Numerics` namespace, modified to use generic math interfaces as the base value type, for cases when greater precision than the `float` of the native `System.Numerics` types is required.
  
  Includes:
    - `Matrix3x2`
    - `Matrix4x4`
    - `Plane`
    - `Quaternion`
    - `Vector2`
    - `Vector3`
    - `Vector4`
  
  Note that these types are not SIMD-enabled and will not benefit from the high performance of the native `System.Numerics` types. Those should be preferred whenever performance is valued over precision.
  
- Classes defining geometric shapes, including:
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

Tavenem.Mathematics is a relatively stable library. Although additions and bugfixes are possible at any time, release should generally be expected to folow the .NET release cycle, with one or more preview releases during a framework preview, and a new stable release coinciding with the release of a new .NET framework major version.

## Contributing

Contributions are always welcome. Pull requests with well-tested code that improves the performance of the library are especially encouraged. Please carefully read the [contributing](docs/CONTRIBUTING.md) document to learn more before submitting issues or pull requests.

## Code of conduct

Please read the [code of conduct](docs/CODE_OF_CONDUCT.md) before engaging with our community, including but not limited to submitting or replying to an issue or pull request.