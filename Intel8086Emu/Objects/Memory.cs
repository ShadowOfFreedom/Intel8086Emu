using Intel8086Emu.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Intel8086Emu.Objects
{
    public class Memory
    {
        private readonly Dictionary<(ushort, ushort), byte[]> _memory;

        public byte[] this[(ushort, ushort) key]
        {
            get
            {
                if (_memory.ContainsKey(key)) 
                    return _memory[key];
                throw new ArgumentException("Nieprawidłowy adres pamięci");
            }

            set
            {
                if (_memory.ContainsKey(key))
                    _memory[key] = value;
                else
                    throw new ArgumentException("Nieprawidłowy adres pamięci");
            }
        }

        public Memory() =>
            _memory = new Dictionary<(ushort, ushort), byte[]>
            {
                { (0x0700, 0x0000) , new byte[]{ 0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0} },
                { (0x0700, 0x0010) , new byte[]{ 0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0} },
                { (0x0700, 0x0020) , new byte[]{ 0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0} },
                { (0x0700, 0x0030) , new byte[]{ 0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0} },
                { (0x0700, 0x0040) , new byte[]{ 0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0} },
                { (0x0700, 0x0050) , new byte[]{ 0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0} },
                { (0x0700, 0x0060) , new byte[]{ 0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0} },
                { (0x0700, 0x0070) , new byte[]{ 0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0,0x0} },
            };

        public void SetValue((ushort, ushort) key, byte address, ushort value)
        {
            if (!_memory.ContainsKey(key)) 
                throw new ArgumentException("Nieprawidłowy adres pamięci");

            var bytes = BitConverter.GetBytes(value);

            SetValue(key, address, bytes[0]);
            SetValue(key, ++address, bytes[1]);
        }

        public void SetValue((ushort, ushort) key, byte address, byte value)
        {
            if (!_memory.ContainsKey(key))
                throw new ArgumentException("Nieprawidłowy adres pamięci");

            _memory[key][address] = value;
        }

        public ushort GetValue((ushort, ushort) key, byte address)
        {
            var bytes = new[]
            {
                GetByteValue(key, address),
                GetByteValue(key, ++address)
            };

            return BitConverter.ToUInt16(bytes);
        }

        private byte GetByteValue((ushort, ushort) key, byte address)
        {
            if (_memory.ContainsKey(key))
            {
                return _memory[key][address];
            }
            throw new ArgumentException("Nieprawidłowy adres pamięci");
        }

        public override string ToString()
        {
            var sBuilder = new StringBuilder();
            foreach (var memoryCell in _memory)
            {
                sBuilder.AppendLine($"{memoryCell.Key.Item1:X4}:{memoryCell.Key.Item2:X4}\t{memoryCell.Value.ToMemoryString()}");
            }
            return sBuilder.ToString();
        }
    }
}
