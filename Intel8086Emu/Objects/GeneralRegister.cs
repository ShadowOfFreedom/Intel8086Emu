using System;
using System.Collections.Generic;
using System.Linq;

namespace Intel8086Emu.Objects
{
    public class GeneralRegister
    {
        private readonly string[] _16ByteKeys = {"ax", "bx", "cx", "dx"};
        private readonly Dictionary<string, byte> _generalRegister;

        public byte this[string key]
        {
            get
            {
                if (_generalRegister.ContainsKey(key.ToLower())) 
                    return _generalRegister[key.ToLower()];

                throw new ArgumentException("Nieprawidłowa nazwa rejestru");
            }

            set
            {
                if (_generalRegister.ContainsKey(key.ToLower()))
                    _generalRegister[key.ToLower()] = value;
                else
                    throw new ArgumentException("Nieprawidłowa nazwa rejestru");
            }
        }

        public GeneralRegister()
        {
            _generalRegister = new Dictionary<string, byte>()
            {
                {"ah", 0 },
                {"al", 0 },
                {"bh", 0 },
                {"bl", 0 },
                {"ch", 0 },
                {"cl", 0 },
                {"dh", 0 },
                {"dl", 0 },
            };
        }

        public bool ContainsKey(string key) => _generalRegister.ContainsKey(key.ToLower());

        public bool Is16RegisterKey(string key) => _16ByteKeys.Contains(key.ToLower());

        public ushort Get16RegisterValue(string key) =>
            key.ToLower() switch
            {
                "ax" => BitConverter.ToUInt16(new[] { _generalRegister["al"], _generalRegister["ah"] }, 0),
                "bx" => BitConverter.ToUInt16(new[] { _generalRegister["bl"], _generalRegister["bh"] }, 0),
                "cx" => BitConverter.ToUInt16(new[] { _generalRegister["cl"], _generalRegister["ch"] }, 0),
                "dx" => BitConverter.ToUInt16(new[] { _generalRegister["dl"], _generalRegister["dh"] }, 0),
                _ => throw new ArgumentException("Podany rejestr nie jest 16 bitowy")
            };

        public void Set16RegisterValue(string key, ushort value)
        {
            if (!Is16RegisterKey(key)) throw new ArgumentException("Podany rejestr nie jest 16 bitowy");

            var bytes = BitConverter.GetBytes(value);

            switch (key.ToLower())
            {
                case "ax":
                    _generalRegister["al"] = bytes[0];
                    _generalRegister["ah"] = bytes[1];
                    break;
                case "bx":
                    _generalRegister["bl"] = bytes[0];
                    _generalRegister["bh"] = bytes[1];
                    break;
                case "cx":
                    _generalRegister["cl"] = bytes[0];
                    _generalRegister["ch"] = bytes[1];
                    break;
                case "dx":
                    _generalRegister["dl"] = bytes[0];
                    _generalRegister["dh"] = bytes[1];
                    break;
            }
        }
    }
}