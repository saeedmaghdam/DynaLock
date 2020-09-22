
# ChangeLog
All notable changes to this project will be documented in this file.

## 2.4.1 - 2020-09-22
### Added
-	Added flags to IContext interface

## 2.4.0 - 2020-09-22
### Added
-	Added flag to DynaLock to give more power to developers

## 2.3.3 - 2020-09-22
### Fixed
-	Fixed a bug where locking context caused 'Argument must be initialized to false' error

## 2.3.2 - 2020-09-22
###	Changed
-	Changed IsMetaDataNull return type to boolean

## 2.3.1 - 2020-09-22
### Added
-	Added locking feature to context classes
-	Get context from locker objects

## 2.3.0 - 2020-09-22
### Added
-	Added ManualResetEvent to DynaLock library
-	Added ManualResetEvent's unit test project to solution

## 2.2.2 - 2020-09-06
### Changed
-	Multi-targeted DynaLock framework to .Net Standard 1.1 and .Net Framework 4.5

## 2.2.1 - 2020-09-06
### Changed
-	Upgraded framework version to .Net Standard 2.0

## 2.2.0 - 2020-09-05
### Added
-	Added AutoResetEvent to DynaLock library
-	Added AutoResetEvent unit test to unit test project
-	Added DynaLock factory

### Changed
-	Downgraded DynaLock's .NetStandard dependency to version 1.1

## 2.1.0 - 2020-09-03
### Fixed
-	Fixed a bug where monitor's exit method and semaphore's release method caused an error
### Changed
-	Moved DynaLock.Framework project inside Framework folder on DynaLock

## 2.0.1 - 2020-09-02
### Fixed
-	Fixed a bug where dependencies not included in nuget package

## 2.0.0 - 2020-09-02
### Added
-	Meta-data object to contexts
-	Added comments to methods, properties and classes

### Changed
- Refactored and improved the source code

## 1.1.0 - 2020-08-30
### Added
- Added Semaphore to DynaLock

### Changed
- Applied MonitorBuilder in unit test project

## 1.0.4 - 2020-08-30
### Added
- IBuilder to famework
- MonitorBuilder to hide construction complexities

## 1.0.3 - 2020-08-30
### Added
- Added unit test project

## 1.0.2 - 2020-08-28
### Added
- Added bounded context to prevent wasting CPU

## 1.0.1 - 2020-08-27
### Added
- Added Exit method to Monitor

## 1.0.0 - 2020-08-27
### Added
- Initialized DynaLock
- Added Monitor to the project