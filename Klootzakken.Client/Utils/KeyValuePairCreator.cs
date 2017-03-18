using System.Collections.Generic;

namespace Klootzakken.Client.Utils
{
    public static class KeyValuePairCreator
    {
        public static KeyValuePair<K, V> Create<K, V>(K key, V value)
        {
            return new KeyValuePair<K, V>(key, value);
        }
    }
}