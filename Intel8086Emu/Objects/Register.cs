using System;
using System.Collections.Generic;

namespace Intel8086Emu.Objects
{
    public class Register
    {
        private readonly Dictionary<string, ushort> _register;

        public ushort this[string key]
        {
            get
            {
                if (_register.ContainsKey(key.ToLower())) 
                    return _register[key];
                throw new ArgumentException("Nieprawidłowa nazwa rejestru");
            }

            set
            {
                if (_register.ContainsKey(key.ToLower()))
                    _register[key.ToLower()] = value;
                else
                    throw new ArgumentException("Nieprawidłowa nazwa rejestru");
            }
        }

        public Register() =>
            _register = new Dictionary<string, ushort>()
            {
                {"cs", 0 },
                {"ip", 0 },
                {"ss", 0 },
                {"sp", 0 },
                {"bp", 0 },
                {"si", 0 },
                {"di", 0 },
                {"ds", 0 },
                {"es", 0 },
            };
        
        public bool ContainsKey(string key) => _register.ContainsKey(key);
    }
}