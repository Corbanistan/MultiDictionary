# MultiDictionary

A generic implementation of a dictionary that maps keys to pairs of values, where each key is associated with two distinct values.

## Overview

`MultiDictionary<TKey, TValue1, TValue2>` is a strongly-typed collection that provides similar functionality to `Dictionary<TKey, TValue>` but associates each key with a tuple of two values instead of a single value.

Key features:
- Associates each key with a pair of values
- Standard dictionary operations (Add, Remove, ContainsKey, etc.) with value pairs
- Thread-unsafe (use external synchronization)
- Similar performance characteristics to standard dictionary
- Implements `IDictionary<TKey, (TValue1, TValue2)>`

## Installation

NuGet package may be coming soon. Until then, you can clone this repository or download the source code directly.

## Usage

### Basic Operations

```csharp
var dict = new MultiDictionary<string, int, DateTime>();

// Add items
dict.Add("key1", 42, DateTime.Now);
dict.AddOrUpdate("key2", 99, DateTime.Today);

// Get values
if (dict.TryGetValue("key1", out var value1, out var value2))
{
    Console.WriteLine($"Value1: {value1}, Value2: {value2}");
}

// Update values
dict["key1"] = (100, DateTime.MinValue);
dict.TryUpdate("key2", 200, DateTime.MaxValue);

// Remove items
dict.Remove("key1");
```

### Constructor Options

```csharp
// With custom capacity (initial size)
var dict1 = new MultiDictionary<string, double, bool>(capacity: 100);

// With custom key comparer (case-insensitive keys)
var dict2 = new MultiDictionary<string, int, int>(
    comparer: StringComparer.OrdinalIgnoreCase);

// With both capacity and comparer
var dict3 = new MultiDictionary<string, int, int>(
    capacity: 50,
    comparer: StringComparer.OrdinalIgnoreCase);
```

### Enumeration

```csharp
foreach (var kvp in dict)
{
    Console.WriteLine($"Key: {kvp.Key}, Value1: {kvp.Value.Item1}, Value2: {kvp.Value.Item2}");
}
```

## API Reference

### Properties
- `Count` - Gets the number of key/value pairs
- `IsReadOnly` - Indicates if dictionary is read-only
- `Keys` - Collection of keys
- `Values` - Collection of value pairs
- `Item[TKey]` - Indexer for getting/setting values

### Core Methods
- `Add(TKey, TValue1, TValue2)` - Adds key with specified values
- `AddOrUpdate(TKey, TValue1, TValue2)` - Adds or updates key/value pair
- `Clear()` - Removes all items
- `ContainsKey(TKey)` - Checks for key existence
- `TryGetValue(TKey, out TValue1, out TValue2)` - Gets both values
- `Remove(TKey)` - Removes key/value pair
- `TryUpdate(TKey, TValue1, TValue2)` - Updates existing key's values

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.
