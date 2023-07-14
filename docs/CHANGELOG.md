# Changelog

## 2.2-preview.1
### Updated
- Update to .NET 8

## 2.1
### Updated
- Update to .NET 7

## 2.1.0-preview.1
### Updated
- Update to .NET 7 RC
- Remove dependency on preview features and experimental NuGet package

## 2.0.0
### Updated
- Update to release packages

## 2.0.0-preview.11
### Added
- Made Zero vector and matrix initialization explicit rather than the default constructor

## 2.0.0-preview.11
### Changed
- Aligned the extensions for the `System.Numerics` interfaces with the new shape of those interfaces.

## 2.0.0-preview.10
### Added
- `ICreateOther` interface to allow number types to define their own conversion to standard numeric types

## 2.0.0-preview.9
### Fixed
- vector `ToString`

## 2.0.0-preview.8
### Added
- Instance methods on `Numerics` types which call the static equivalents

## 2.0.0-preview.7
### Changed
- Improve JSON deserialization of `IShape` with `JsonPropertyOrder`

## 2.0.0-preview.6
### Added
- Add science constants to the `NumberValues` extensions
### Removed
- Removed the more obscure precalculated science values (now located in the `Universe` library)

## 2.0.0-preview.5
### Added
- Average and Sum extension methods for `INumber` in the `System.Linq` namespace

## 2.0.0-preview.4
### Changed
- Make the create number methods extensions

## 2.0.0-preview.3
### Added
- Expose static abstract methods of `INumber` and `IFloatingPoint` as extension methods
- Expose static abstract properties of `INumber` and `IFloatingPoint` as static properties of `NumberValues`

## 2.0.0-preview.2
### Changed
- Remove dependency on HugeNumber

## 2.0.0-preview.1
### Added
- New interfaces which mirror the new mathematical interfaces, and generic implementations which accept any `IFloatingPoint` type:
    - `IShape<T>` and `Shape<T>`
    - `IMatrix3x2<T>` and `Matrix3x2<T>`
    - `IMatrix4x4<T>` and `Matrix4x4<T>`
    - `IPlane<T>` and `Plane<T>`
    - `IQuaternion<T>` and `Quaternion<T>`
    - `IQuaternion<T>` and `Quaternion<T>`
    - `Vector2<T>`
    - `Vector3<T>`
    - `Vector4<T>`
### Changed
- Update to .NET 6 preview
- Update to C# 10 preview
- Rename static `DecimalConstants.e` and `DoubleConstants.e` to `E` to align with `IFloatingPoint`
- Rename statics related to `PI` to use `Pi` in camel case to align with `IFloatingPoint`
- Rename static `DecimalConstants.phi` and `DoubleConstants.phi` to `Phi` to align with others
- Changed shape classes to readonly structs
### Removed
- Type-specific Numerics classes: these and other types can now be represented by the generics
- Type-specific `IShape` implementations: these and other types can now be represented by the generics
- Removed support for non-JSON serialization

## 1.1.2
### Updated
- Update dependencies

## 1.1.1
### Fixed
- Fix `Capsule` dimension calculations

## 1.1.0
### Added
- `FloatRange` struct

## 1.0.0
### Added
- Initial release