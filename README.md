# chdb

[![Build Status](https://dev.azure.com/ChDB/ChDB/_apis/build/status/ChDB.chdb?branchName=master)](https://dev.azure.com/ChDB/ChDB/_build/latest?definitionId=1&branchName=master)
[![NuGet](https://img.shields.io/nuget/v/ChDB.svg)](https://www.nuget.org/packages/ChDB/)
[![NuGet](https://img.shields.io/nuget/dt/ChDB.svg)](https://www.nuget.org/packages/ChDB/)
[![License](https://img.shields.io/github/license/ChDB/chdb.svg)](https://github.com/vilinski/chdb/LICENSE.md)

# ChDb NuGet package

This is a .NET Core binding for [ChDB](https://doc.chdb.io) library.

# chdb-tool dotnet tool

This is a dotnet tool for [ChDB](https://doc.chdb.io) library. 
Actually you better just install clickhouse client and run `clickhouse local`

## Installation

```bash
dotnet tool install -g chdb-tool
```

## Usage

```bash
chdb --help
chdb --version
chdb "select version()" 
chdb "select * from system.formats where is_output = 1" PrettyCompact
```

## Build

```bash
# update latest chdb version
./update.sh
# install versionbump tool
dotnet tool install -g BumpVersion
# bump version
bumpversion patch
git push --foloow-tags
```

## Authors

* [Andreas Vilinski](https://github.com/vilinski)