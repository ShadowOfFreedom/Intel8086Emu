using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intel8086Emu.Objects
{
    public class MemoryStack
    {
        private readonly Dictionary<(ushort, ushort), ushort> _stack;

        public ushort this[(ushort, ushort) key]
        {
            get
            {
                if (_stack.ContainsKey(key)) 
                    return _stack[key];
                throw  new ArgumentException("Nieprawidłowy adres stosu");
            }

            set
            {
                if (_stack.ContainsKey(key))
                    _stack[key] = value;
                else
                    throw new ArgumentException("Nieprawidłowy adres stosu");
            }
        }

        public MemoryStack() =>
            _stack = new Dictionary<(ushort, ushort), ushort>
            {
                { (0x0700, 0xfffe) , 0x0000},
                { (0x0700, 0xfffc) , 0x0000},
                { (0x0700, 0xfffa) , 0x0000},
                { (0x0700, 0xfff8) , 0x0000},
                { (0x0700, 0xfff6) , 0x0000},
                { (0x0700, 0xfff4) , 0x0000},
                { (0x0700, 0xfff2) , 0x0000},
                { (0x0700, 0xfff0) , 0x0000},
                { (0x0700, 0xffee) , 0x0000},
                { (0x0700, 0xffec) , 0x0000},
                { (0x0700, 0xffea) , 0x0000},
                { (0x0700, 0xffe8) , 0x0000},
                { (0x0700, 0xffe6) , 0x0000},
                { (0x0700, 0xffe4) , 0x0000},
                { (0x0700, 0xffe2) , 0x0000},
                { (0x0700, 0xffe0) , 0x0000},
                { (0x0700, 0xffde) , 0x0000},
                { (0x0700, 0xffdc) , 0x0000},
                { (0x0700, 0xffda) , 0x0000},
                { (0x0700, 0xffd8) , 0x0000},
                { (0x0700, 0xffd6) , 0x0000},
                { (0x0700, 0xffd4) , 0x0000},
                { (0x0700, 0xffd2) , 0x0000},
                { (0x0700, 0xffd0) , 0x0000},
                { (0x0700, 0xffce) , 0x0000},
                { (0x0700, 0xffcc) , 0x0000}
            };
        
        public override string ToString()
        {
            var sBuilder = new StringBuilder();
            foreach (var memoryCell in _stack)
            {
                sBuilder.AppendLine($"{memoryCell.Key.Item1:X4}:{memoryCell.Key.Item2:X4}\t{memoryCell.Value:X4}");
            }
            
            return sBuilder.ToString();
        }

        public bool IsOffsetInScope(ushort offset) => _stack.Keys.Contains(((ushort)0x0700, offset));
    }
}
