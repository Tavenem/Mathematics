# Changelog

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