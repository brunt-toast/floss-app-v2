# README

## Getting Started

### App

You'll need the .NET 9 SDK. While the projects target .NET 8, the .NET 9 SDK is required to compile preview C# language features. 

To run:
```bash
dotnet run --project src/FlossApp.Wasm/FlossApp.Wasm.csproj
```

### Tests 

Todo

### Benchmarks

For exporters to work, you'll need R installed and Rscript(.exe) available in PATH. 

To run: 
```bash
dotnet run \
    --project test/FlossApp.Application.Benchmarks/FlossApp.Application.Benchmarks.csproj \
    -- \
    --filter *
```