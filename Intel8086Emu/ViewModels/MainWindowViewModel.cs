using Intel8086Emu.Objects;
using System.ComponentModel;

namespace Intel8086Emu.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Register _register;
        private GeneralRegister _generalRegister;
        private MemoryStack _stack;
        private Memory _memory;

        public MainWindowViewModel()
        {
            _register = new Register();
            _generalRegister = new GeneralRegister();
            _stack = new MemoryStack();
            _memory = new Memory();

            _register["cs"] = 0x0700;
            _register["ip"] = 0x0100;
            _register["ss"] = 0x0700;
            _register["sp"] = 0xFFFE;
            _register["ds"] = 0x0700;
            _register["es"] = 0x0700;
        }

        public GeneralRegister GeneralRegister
        {
            get => _generalRegister;
            set
            {
                _generalRegister = value;
                OnPropertyChanged("GeneralRegister");
            }
        }

        public Register Register
        {
            get => _register;
            set
            {
                _register = value;
                OnPropertyChanged("Register");
            }
        }

        public MemoryStack MemoryStack
        {
            get => _stack;
            set
            {
                _stack = value;
                OnPropertyChanged("MemoryStack");
            }
        }

        public Memory Memory
        {
            get => _memory;
            set
            {
                _memory = value;
                OnPropertyChanged("Memory");
            }
        }

        private void SetValue(string key, byte value)
        {
            _generalRegister[key] = value;
            OnPropertyChanged("GeneralRegister");
        }
        
        public void SetValue(string key, ushort value)
        {
            if (_register.ContainsKey(key))
            {
                _register[key] = value; 
                OnPropertyChanged("Register");
            }
            else if (_generalRegister.Is16RegisterKey(key))
            {
                _generalRegister.Set16RegisterValue(key, value);
                OnPropertyChanged("GeneralRegister");
            }
            else if (_generalRegister.ContainsKey(key) && !_generalRegister.Is16RegisterKey(key))
                SetValue(key, (byte) value);
        }

        public void SetStackValue((ushort, ushort) key, ushort value)
        {
            _stack[key] = value;
            DecreaseStackPointer();
            OnPropertyChanged("MemoryStack");
        }
        
        public ushort GetStackValue((ushort, ushort) key)
        {
            var result = MemoryStack[key];
            return result;
        }

        private void DecreaseStackPointer()
        {
            if (!MemoryStack.IsOffsetInScope((ushort) (Register["sp"] - 2))) 
                return;

            Register["sp"] -= 2;
            OnPropertyChanged("Register");

        }

        public void IncreaseStackPointer()
        {
            if (!MemoryStack.IsOffsetInScope((ushort)(Register["sp"] + 2)))
                return;

            Register["sp"] += 2;
            OnPropertyChanged("Register");
        }

        public void IncreaseIpValue(ushort value)
        {
            _register["ip"] += value;
            OnPropertyChanged("Register");
        }

        public void SetValueToMemory((ushort, ushort) key, byte address, ushort value)
        {
            _memory.SetValue(key,address,value);
            OnPropertyChanged("Memory");
        }
        
        public void SetValueToMemory((ushort, ushort) key, byte address, byte value)
        {
            _memory.SetValue(key,address,value);
            OnPropertyChanged("Memory");
        }

        public ushort GetValue(string key) =>
            GeneralRegister.Is16RegisterKey(key.ToLower()) ? GeneralRegister.Get16RegisterValue(key.ToLower()) :
            GeneralRegister.ContainsKey(key.ToLower()) ? GeneralRegister[key.ToLower()] :
            Register.ContainsKey(key.ToLower()) ? Register[key.ToLower()] : (ushort) 0;

        private void OnPropertyChanged(string propertyName) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}