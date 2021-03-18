using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace MonoGame.Inputs
{
    class Input
    {
        private List<Keys> _keys = new List<Keys>();
    

        //┌-----------------------------------------┐
        //|             Constructors                |
        //└-----------------------------------------┘
        public Input(params Keys[] keys)
        {
            _keys.AddRange(keys);
        }

        //┌-----------------------------------------┐
        //|             Public Acces                |
        //└-----------------------------------------┘
        public Keys[] Keys { get { return _keys.ToArray(); } }
        
        public void AddKeys(params Keys[] keys)
        {
            foreach (Keys key in keys)
                if (!_keys.Contains(key))
                    _keys.Add(key);
        }
        public void RemoveKeys(params Keys[] keys)
        {
            foreach (Keys key in keys)
                if (_keys.Contains(key))
                    _keys.Remove(key);
        }

        //┌-----------------------------------------┐
        //|                 Events                  |
        //└-----------------------------------------┘
        public bool IsDown
        {
            get
            {
                foreach(Keys key in _keys)
                {
                    if (InputHandler.KeyDown(key))
                        return true;
                }

                return false;
            }
        }
        public bool Pressed
        {
            get
            {
                foreach (Keys key in _keys)
                {
                    if (InputHandler.KeyPress(key))
                        return true;
                }

                return false;
            }
        }
        public bool Released
        {
            get
            {
                foreach (Keys key in _keys)
                {
                    if (InputHandler.KeyRelease(key))
                        return true;
                }

                return false;
            }
        }

        
    }
}
