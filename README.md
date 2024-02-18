# chdb

[![Build Status](https://dev.azure.com/ChDB/ChDB/_apis/build/status/ChDB.chdb?branchName=master)](https://dev.azure.com/ChDB/ChDB/_build/latest?definitionId=1&branchName=master)
[![NuGet](https://img.shields.io/nuget/v/ChDB.svg)](https://www.nuget.org/packages/ChDB/)
[![NuGet](https://img.shields.io/nuget/dt/ChDB.svg)](https://www.nuget.org/packages/ChDB/)
[![License](https://img.shields.io/github/license/ChDB/chdb.svg)](https://github.com/chdb-io/chdb-dotnet/blob/main/LICENSE)

> NOTE: ❗❗❗ this repository is obsolete. Code and further development is moved to [chdb-io/chdb-dotnet](https://github.com/chdb-io/chdb-dotnet)

.NET Core binding for the awesome [ChDB](https://doc.chdb.io) library.

`chdb-tool` is a dotnet tool for [ChDB](https://doc.chdb.io) library. 
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

## Authors

* [Andreas Vilinski](https://github.com/vilinski)
