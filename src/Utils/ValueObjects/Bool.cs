﻿namespace Utils.ValueObjects
{
    public class Bool
    {
        private readonly string _source;

        private bool? _value;

        /// <summary>
        /// Null is allowed.
        /// </summary>
        /// <param name="source">Source.</param>
        public Bool(string source)
        {
            _source = source;
        }

        public bool Value()
        {
            if (_value == null)
            {
                _value = _source != null && bool.TryParse(_source, out var result) && result;
            }

            return _value.Value;
        }

        public bool Equals(bool second) => second == Value();
    }
}