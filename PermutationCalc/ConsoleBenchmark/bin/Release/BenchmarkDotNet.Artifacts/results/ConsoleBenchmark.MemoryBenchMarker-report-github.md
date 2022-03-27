``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19041.1415 (2004/May2020Update/20H1)
Intel Core i7-10510U CPU 1.80GHz, 1 CPU, 8 logical and 4 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4420.0), X86 LegacyJIT  [AttachedDebugger]
  DefaultJob : .NET Framework 4.8 (4.8.4420.0), X86 LegacyJIT


```
|               Method |     Mean |   Error |  StdDev |    Gen 0 | Allocated |
|--------------------- |---------:|--------:|--------:|---------:|----------:|
|   Permutation_Cycles | 179.8 μs | 3.28 μs | 3.91 μs |  18.3105 |     76 KB |
| Permutation_Codomain | 764.1 μs | 8.36 μs | 6.98 μs | 360.3516 |  1,478 KB |
