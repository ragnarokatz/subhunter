using System;
using System.Collections;
using System.Collections.Generic;

namespace Foundation
{
    /// <summary>
    /// Dbase, enhanced version of a string -> object dictionary.
    /// </summary>
    public class Dbase : IDictionary
    {
        private const int INIT_COUNT = 20;
        private Dictionary<string, object> data;

        // For complying with the requirement of Idictionary
        public int Count { get { return this.data.Count; } }

        public bool IsFixedSize { get { return false; } }
        public bool IsReadOnly { get { return false; } }
        public bool IsSynchronized { get { return false; } }

        public object SyncRoot { get { throw new NotImplementedException(); } }
        public void CopyTo(Array array, int index) { throw new NotImplementedException(); }

        public ICollection Keys { get { return this.data.Keys; } }
        public ICollection Values { get { return this.data.Values; } }

        public void Remove(object key)
        {
            CheckKeyType(key);
            this.data.Remove(key as string);
        }

        public void Clear()
        {
            this.data.Clear();
        }

        public object this[object key] {
            get
            {
                CheckKeyType(key);
                return this.data[key as string];
            }
            set
            {
                CheckKeyType(key);
                Set(key as string, value);
            }
        }

        private void CheckKeyType(object key)
        {
            if (! (key is string))
                throw new NotSupportedException("Index must be string.");
        }
        private void ThrowNotImplementedException()
        {
            throw new NotImplementedException("Not implemented.");
        }

        public bool Contains(object key)
        {
            CheckKeyType(key);
            return this.data.ContainsKey(key as string);
        }

        public void Add(object key, object value)
        {
            CheckKeyType(key);
            Set(key as string, value);
        }

        // Set function
        public void Set(string key, object value)
        {
            if (this.data.ContainsKey(key))
            {
                this.data[key] = value;
                return;
            }
            this.data.Add(key, value);
        }

        public IDictionaryEnumerator GetEnumerator() { return this.data.GetEnumerator(); }
        IEnumerator IEnumerable.GetEnumerator() { return this.data.GetEnumerator(); }

        // Constructors
        public Dbase()
        {
            this.data = new Dictionary<string, object>(Dbase.INIT_COUNT);
        }

        public Dbase(int count)
        {
            this.data = new Dictionary<string, object>(count);
        }

        // Query functions
        public Dbase QueryDbase(string idx)
        {
            var d = this.data[idx];

            if (d != null && d is Dbase)
                return d as Dbase;

            return null;
        }

        public int QueryInt(string idx)
        {
            var d = this.data[idx];

            if (d == null)
                return 0;

            if (! (d is string))
                return 0;

            int v = 0;

            if (int.TryParse((string) d, out v))
                return v;

            return 0;
        }

        public bool QueryBoolean(string idx)
        {
            var d = this.data[idx];
            if (d == null)
                return false;

            return (bool) d;
        }

        public string QueryString(string idx)
        {
            var d = this.data[idx];

            if (d == null)
                return null;

            if (d is string)
                return d as string;

            return (string) d;
        }

        public object[] QueryArray(string idx)
        {
            var d = this.data[idx];
            if (d == null || ! (d is Array))
                return null;

            return (object[]) d;
        }

        public override string ToString ()
        {
            return Serialize();
        }

        public string Serialize()
        {
            return Pathfinding.Serialization.JsonFx.JsonWriter.Serialize(this);
        }
    }
}
