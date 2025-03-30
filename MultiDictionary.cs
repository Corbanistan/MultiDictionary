using System.Collections;

namespace MultiDictionary
{
    /// <summary>
    /// A dictionary that maps keys to pairs of values, where each key is associated with two distinct values.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue1">The type of the first value in the value pair.</typeparam>
    /// <typeparam name="TValue2">The type of the second value in the value pair.</typeparam>
    /// <remarks>
    /// <para>This class provides similar functionality to <see cref="Dictionary{TKey, TValue}"/> but associates each key with a tuple of two values.</para>
    /// <para>Like <see cref="Dictionary{TKey, TValue}"/>, this class is not thread-safe. For concurrent access, external synchronization is required.</para>
    /// </remarks>
    [Serializable]
    public class MultiDictionary<TKey, TValue1, TValue2> :
        IEnumerable<KeyValuePair<TKey, (TValue1, TValue2)>>,
        ICollection<KeyValuePair<TKey, (TValue1, TValue2)>>,
        IReadOnlyDictionary<TKey, (TValue1, TValue2)>
    {
        private readonly Dictionary<TKey, (TValue1, TValue2)> _dictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiDictionary{TKey, TValue1, TValue2}"/> class
        /// that is empty and has the default initial capacity.
        /// </summary>
        public MultiDictionary()
        {
            _dictionary = new Dictionary<TKey, (TValue1, TValue2)>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiDictionary{TKey, TValue1, TValue2}"/> class
        /// that is empty and has the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The initial number of elements the dictionary can contain.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity"/> is less than 0.</exception>
        public MultiDictionary(int capacity)
        {
            if (capacity < 0) throw new ArgumentOutOfRangeException(nameof(capacity));
            _dictionary = new Dictionary<TKey, (TValue1, TValue2)>(capacity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiDictionary{TKey, TValue1, TValue2}"/> class
        /// that is empty, has the default initial capacity, and uses the specified equality comparer.
        /// </summary>
        /// <param name="comparer">The <see cref="IEqualityComparer{TKey}"/> implementation to use when comparing keys,
        /// or <c>null</c> to use the default <see cref="EqualityComparer{TKey}"/> for the type of the key.</param>
        public MultiDictionary(IEqualityComparer<TKey> comparer)
        {
            _dictionary = new Dictionary<TKey, (TValue1, TValue2)>(comparer ?? throw new ArgumentNullException(nameof(comparer)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiDictionary{TKey, TValue1, TValue2}"/> class
        /// that is empty, has the specified initial capacity, and uses the specified equality comparer.
        /// </summary>
        /// <param name="capacity">The initial number of elements the dictionary can contain.</param>
        /// <param name="comparer">The <see cref="IEqualityComparer{TKey}"/> implementation to use when comparing keys,
        /// or <c>null</c> to use the default <see cref="EqualityComparer{TKey}"/> for the type of the key.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity"/> is less than 0.</exception>
        public MultiDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            if (capacity < 0) throw new ArgumentOutOfRangeException(nameof(capacity));
            _dictionary = new Dictionary<TKey, (TValue1, TValue2)>(capacity, comparer ?? throw new ArgumentNullException(nameof(comparer)));
        }

        /// <summary>
        /// Gets the number of key/value pairs contained in the <see cref="MultiDictionary{TKey, TValue1, TValue2}"/>.
        /// </summary>
        /// <value>The number of key/value pairs contained in the dictionary.</value>
        public int Count => _dictionary.Count;

        /// <summary>
        /// Gets a value indicating whether the <see cref="MultiDictionary{TKey, TValue1, TValue2}"/> is read-only.
        /// </summary>
        /// <value><c>true</c> if the dictionary is read-only; otherwise, <c>false</c>.</value>
        public bool IsReadOnly => false;

        /// <summary>
        /// Gets a collection containing the keys in the <see cref="MultiDictionary{TKey, TValue1, TValue2}"/>.
        /// </summary>
        /// <value>A collection containing the keys in the dictionary.</value>
        public ICollection<TKey> Keys => _dictionary.Keys;

        /// <summary>
        /// Gets a collection containing the value pairs in the <see cref="MultiDictionary{TKey, TValue1, TValue2}"/>.
        /// </summary>
        /// <value>A collection containing the value pairs in the dictionary.</value>
        public ICollection<(TValue1, TValue2)> Values => _dictionary.Values;

        /// <summary>
        /// Gets a collection containing the keys in the <see cref="MultiDictionary{TKey, TValue1, TValue2}"/>.
        /// </summary>
        /// <value>A collection containing the keys in the dictionary.</value>
        IEnumerable<TKey> IReadOnlyDictionary<TKey, (TValue1, TValue2)>.Keys => Keys;

        /// <summary>
        /// Gets a collection containing the value pairs in the <see cref="MultiDictionary{TKey, TValue1, TValue2}"/>.
        /// </summary>
        /// <value>A collection containing the value pairs in the dictionary.</value>
        IEnumerable<(TValue1, TValue2)> IReadOnlyDictionary<TKey, (TValue1, TValue2)>.Values => Values;

        /// <summary>
        /// Gets or sets the value pair associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value pair to get or set.</param>
        /// <value>The value pair associated with the specified key.</value>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is null.</exception>
        /// <exception cref="KeyNotFoundException">The property is retrieved and <paramref name="key"/> does not exist in the collection.</exception>
        public (TValue1, TValue2) this[TKey key]
        {
            get => _dictionary[key];
            set
            {
                if (key == null) throw new ArgumentNullException(nameof(key));
                _dictionary[key] = value;
            }
        }

        /// <summary>
        /// Adds the specified key and value pair to the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value1">The first value of the element to add.</param>
        /// <param name="value2">The second value of the element to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is null.</exception>
        /// <exception cref="ArgumentException">An element with the same key already exists in the dictionary.</exception>
        public void Add(TKey key, TValue1 value1, TValue2 value2)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (_dictionary.ContainsKey(key)) throw new ArgumentException("An element with the same key already exists.");
            _dictionary.Add(key, (value1, value2));
        }

        /// <summary>
        /// Adds the specified key-value pair to the dictionary.
        /// </summary>
        /// <param name="item">The key-value pair to add.</param>
        /// <exception cref="ArgumentNullException">The key of <paramref name="item"/> is null.</exception>
        /// <exception cref="ArgumentException">An element with the same key already exists in the dictionary.</exception>
        public void Add(KeyValuePair<TKey, (TValue1, TValue2)> item)
        {
            if (item.Key == null) throw new ArgumentNullException(nameof(item.Key));
            if (_dictionary.ContainsKey(item.Key)) throw new ArgumentException("An element with the same key already exists.");
            _dictionary.Add(item.Key, item.Value);
        }

        /// <summary>
        /// Removes all keys and value pairs from the <see cref="MultiDictionary{TKey, TValue1, TValue2}"/>.
        /// </summary>
        public void Clear()
        {
            _dictionary.Clear();
        }

        /// <summary>
        /// Determines whether the <see cref="MultiDictionary{TKey, TValue1, TValue2}"/> contains the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the dictionary.</param>
        /// <returns><c>true</c> if the dictionary contains an element with the specified key; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is null.</exception>
        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        /// <summary>
        /// Determines whether the <see cref="MultiDictionary{TKey, TValue1, TValue2}"/> contains a specific key-value pair.
        /// </summary>
        /// <param name="item">The key-value pair to locate in the dictionary.</param>
        /// <returns><c>true</c> if <paramref name="item"/> is found in the dictionary; otherwise, <c>false</c>.</returns>
        public bool Contains(KeyValuePair<TKey, (TValue1, TValue2)> item)
        {
            return _dictionary.Contains(item);
        }

        /// <summary>
        /// Attempts to get the values associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the values to get.</param>
        /// <param name="value1">When this method returns, contains the first value associated with the specified key,
        /// if the key is found; otherwise, the default value for <typeparamref name="TValue1"/>.</param>
        /// <param name="value2">When this method returns, contains the second value associated with the specified key,
        /// if the key is found; otherwise, the default value for <typeparamref name="TValue2"/>.</param>
        /// <returns><c>true</c> if the dictionary contains an element with the specified key; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is null.</exception>
        public bool TryGetValue(TKey key, out TValue1 value1, out TValue2 value2)
        {
            if (_dictionary.TryGetValue(key, out var values))
            {
                value1 = values.Item1;
                value2 = values.Item2;
                return true;
            }
            value1 = default;
            value2 = default;
            return false;
        }

        /// <summary>
        /// Gets the value pair associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value pair to get.</param>
        /// <param name="values">When this method returns, contains the value pair associated with the specified key,
        /// if the key is found; otherwise, the default value for the tuple type.</param>
        /// <returns><c>true</c> if the dictionary contains an element with the specified key; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is null.</exception>
        public bool TryGetValue(TKey key, out (TValue1, TValue2) values)
        {
            return _dictionary.TryGetValue(key, out values);
        }

        /// <summary>
        /// Removes the value pair with the specified key from the <see cref="MultiDictionary{TKey, TValue1, TValue2}"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns><c>true</c> if the element is successfully found and removed; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is null.</exception>
        public bool Remove(TKey key)
        {
            return _dictionary.Remove(key);
        }

        /// <summary>
        /// Removes the specified key-value pair from the dictionary.
        /// </summary>
        /// <param name="item">The key-value pair to remove.</param>
        /// <returns><c>true</c> if <paramref name="item"/> was successfully removed from the dictionary; otherwise, <c>false</c>.</returns>
        public bool Remove(KeyValuePair<TKey, (TValue1, TValue2)> item)
        {
            return ((ICollection<KeyValuePair<TKey, (TValue1, TValue2)>>)_dictionary).Remove(item);
        }

        /// <summary>
        /// Copies the elements of the <see cref="MultiDictionary{TKey, TValue1, TValue2}"/> to an array of key-value pairs,
        /// starting at the specified array index.
        /// </summary>
        /// <param name="array">The one-dimensional array that is the destination of the elements copied from the dictionary.
        /// The array must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        /// <exception cref="ArgumentNullException"><paramref name="array"/> is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.</exception>
        /// <exception cref="ArgumentException">The number of elements in the source dictionary is greater than the available space
        /// from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.</exception>
        public void CopyTo(KeyValuePair<TKey, (TValue1, TValue2)>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<TKey, (TValue1, TValue2)>>)_dictionary).CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="MultiDictionary{TKey, TValue1, TValue2}"/>.
        /// </summary>
        /// <returns>An enumerator for the <see cref="MultiDictionary{TKey, TValue1, TValue2}"/>.</returns>
        public IEnumerator<KeyValuePair<TKey, (TValue1, TValue2)>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Adds a new key-value pair to the dictionary or updates the existing value pair if the key already exists.
        /// </summary>
        /// <param name="key">The key of the element to add or update.</param>
        /// <param name="value1">The first value to associate with the key.</param>
        /// <param name="value2">The second value to associate with the key.</param>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is null.</exception>
        public void AddOrUpdate(TKey key, TValue1 value1, TValue2 value2)
        {
            _dictionary[key] = (value1, value2);
        }

        /// <summary>
        /// Updates the value pair for a specified key if it exists in the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to update.</param>
        /// <param name="value1">The new first value to associate with the key.</param>
        /// <param name="value2">The new second value to associate with the key.</param>
        /// <returns><c>true</c> if the key was found and the value pair was updated; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="key"/> is null.</exception>
        public bool TryUpdate(TKey key, TValue1 value1, TValue2 value2)
        {
            if (ContainsKey(key))
            {
                _dictionary[key] = (value1, value2);
                return true;
            }
            return false;
        }
    }
}