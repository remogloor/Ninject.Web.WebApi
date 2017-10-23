# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [3.3.0]

### Removed
- Dropped support for pre .NET 4.5.

## [3.2.4]

### Changed
- OwinHost and SelfHost does not depend on WebHost any more. This is a **Breaking Change**, Please install package "Ninject.Web.WebApi.WebHost" if use WebHost, but not just "Ninject.Web.WebApi".

## [3.2.3]

### Added
- Added support for passing in HttpServer to UseNinjectWebApi

## [3.2.2]

### Added
- Added Microsoft.Owin 3.0 support

## [3.2.1]

### Changed
- Moved common bindings to Ninject.Web.Common so that multiple web extensions can be used together.

### Fixed 
- Fixed that filters on the configuration are not executed twice

## [3.2.0]

### Added
- Added Owin support

## [3.0.0.0]
initial version