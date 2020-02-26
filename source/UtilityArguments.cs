/*******************************************************************************************************************************/
// Project: snappy
// Filename: UtilityArguments.cs
// Purpose: CLI argument parsing class and methods.  "UtilityArguments & InputArguments" 
// Description: UtilityArguments class is a simplified helper class to the complex InputArguments class.
// Created by: http://dotnetfollower.com/wordpress/2012/03/c-simple-command-line-arguments-parser/
// JT, 02/24/2020
/*******************************************************************************************************************************/

namespace Snappy
{
    using System;
    using System.Collections.Generic;

    public class UtilityArguments : InputArguments
    {
        public string frequency
        {
            get { return GetValue("f"); }
        }
        public string duration
        {
            get { return GetValue("d"); }
        }

        public string path
        {
            get { return GetValue("p"); }
        }

        public UtilityArguments(string[] args) : base(args)
        {
        }

        // Not used
        protected bool GetBoolValue(string key)
        {
            string adjustedKey;
            if (ContainsKey(key, out adjustedKey))
            {
                bool res;
                bool.TryParse(_parsedArguments[adjustedKey], out res);
                return res;
            }
            return false;
        }
        public bool UseElevatedPrivileges
        {
            get { return GetBoolValue("-useElevatedPrivileges"); }
        }

    }

    public class InputArguments
    {
        #region fields & properties
        public const string DEFAULT_KEY_LEADING_PATTERN = "-";

        protected Dictionary<string, string> _parsedArguments = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        protected readonly string _keyLeadingPattern;

        public string this[string key]
        {
            get { return GetValue(key); }
            set
            {
                if (key != null)
                    _parsedArguments[key] = value;
            }
        }
        public string KeyLeadingPattern
        {
            get { return _keyLeadingPattern; }
        }
        #endregion

        #region public methods
        public InputArguments(string[] args, string keyLeadingPattern)
        {
            _keyLeadingPattern = !string.IsNullOrEmpty(keyLeadingPattern) ? keyLeadingPattern : DEFAULT_KEY_LEADING_PATTERN;

            if (args != null && args.Length > 0)
                Parse(args);
        }
        public InputArguments(string[] args) : this(args, null)
        {
        }

        public bool Contains(string key)
        {
            string adjustedKey;
            return ContainsKey(key, out adjustedKey);
        }

        public virtual string GetPeeledKey(string key)
        {
            return IsKey(key) ? key.Substring(_keyLeadingPattern.Length) : key;
        }
        public virtual string GetDecoratedKey(string key)
        {
            return !IsKey(key) ? (_keyLeadingPattern + key) : key;
        }
        public virtual bool IsKey(string str)
        {
            return str.StartsWith(_keyLeadingPattern);
        }
        #endregion

        #region internal methods
        protected virtual void Parse(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == null) continue;

                string key = null;
                string val = null;

                if (IsKey(args[i]))
                {
                    key = args[i];

                    if (i + 1 < args.Length && !IsKey(args[i + 1]))
                    {
                        val = args[i + 1];
                        i++;
                    }
                }
                else
                    val = args[i];

                // adjustment
                if (key == null)
                {
                    key = val;
                    val = null;
                }
                _parsedArguments[key] = val;
            }
        }

        protected virtual string GetValue(string key)
        {
            string adjustedKey;
            if (ContainsKey(key, out adjustedKey))
                return _parsedArguments[adjustedKey];

            return null;
        }

        protected virtual bool ContainsKey(string key, out string adjustedKey)
        {
            adjustedKey = key;

            if (_parsedArguments.ContainsKey(key))
                return true;

            if (IsKey(key))
            {
                string peeledKey = GetPeeledKey(key);
                if (_parsedArguments.ContainsKey(peeledKey))
                {
                    adjustedKey = peeledKey;
                    return true;
                }
                return false;
            }

            string decoratedKey = GetDecoratedKey(key);
            if (_parsedArguments.ContainsKey(decoratedKey))
            {
                adjustedKey = decoratedKey;
                return true;
            }
            return false;
        }
        #endregion
    }
}
