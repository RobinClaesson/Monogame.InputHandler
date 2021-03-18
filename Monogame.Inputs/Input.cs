using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace MonoGame.Inputs
{
    public class Input
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
                foreach (Keys key in _keys)
                {
                    if (Input.KeyDown(key))
                        return true;
                }

                return false;
            }
        } //True if any keys is down
        public bool IsUp
        {
            get
            {
                return !IsDown;
            }
        }   //True if no key is down

        public bool Pressed
        {
            get
            {
                foreach (Keys key in _keys)
                {
                    if (Input.KeyPress(key))
                        return true;
                }

                return false;
            }
        } //True once on  press
        public bool Released
        {
            get
            {
                foreach (Keys key in _keys)
                {
                    if (Input.KeyRelease(key))
                        return true;
                }

                return false;
            }
        } //Key once on release

        //┌-----------------------------------------┐
        //|             Operator Overrides          |
        //└-----------------------------------------┘
        public static bool operator true(Input x) => x.IsDown;
        public static bool operator false(Input x) => x.IsUp;


        /*███████ ████████  █████  ████████ ██  ██████ 
          ██         ██    ██   ██    ██    ██ ██      
          ███████    ██    ███████    ██    ██ ██      
               ██    ██    ██   ██    ██    ██ ██      
          ███████    ██    ██   ██    ██    ██  ██████                                              
                                                      */
        private static KeyboardState[] _keyStates;
        private static MouseState[] _mouseStates;
        private static int _bufferSize;

        private static Point _onePoint = new Point(1);

        //┌-----------------------------------------┐
        //|             Init and Update             |
        //└-----------------------------------------┘
        public static void Init(int bufferSize)
        {
            if (bufferSize > 0)
            {
                _bufferSize = bufferSize;
                _mouseStates = new MouseState[bufferSize];
                _keyStates = new KeyboardState[bufferSize];

                KeyboardState ks = Keyboard.GetState();
                MouseState ms = Mouse.GetState();


                for (int i = 0; i < _bufferSize; i++)
                {
                    _keyStates[i] = Keyboard.GetState();
                    _mouseStates[i] = Mouse.GetState();
                }
            }
        }
        public static void Update()
        {
            //Update backbuffer
            for (int i = _bufferSize - 1; i > 0; i--)
            {
                _keyStates[i] = _keyStates[i - 1];
                _mouseStates[i] = _mouseStates[i - 1];
            }

            //Get current 
            _keyStates[0] = Keyboard.GetState();
            _mouseStates[0] = Mouse.GetState();
        }

        //┌-----------------------------------------┐
        //|             Keyboardevents              |
        //└-----------------------------------------┘
        public static bool KeyDown(Keys key)
        {
            if (_bufferSize > 0)
                return _keyStates[0].IsKeyDown(key);
            else
                return false;
        }
        public static bool KeyUp(Keys key)
        {
            if (_bufferSize > 0)
                return _keyStates[0].IsKeyUp(key);
            else
                return false;
        }

        public static bool KeyPress(Keys key)
        {
            if (_bufferSize > 1)
                return _keyStates[0].IsKeyDown(key) && _keyStates[1].IsKeyUp(key);
            else
                return false;
        }
        public static bool KeyRelease(Keys key)
        {
            return _keyStates[0].IsKeyUp(key) && _keyStates[1].IsKeyDown(key);
        }


        //┌-----------------------------------------┐
        //|             Mouseevents                 |
        //└-----------------------------------------┘
        public static bool LeftMouseDown
        {
            get
            {
                if (_bufferSize > 0)
                    return _mouseStates[0].LeftButton == ButtonState.Pressed;
                else
                    return false;
            }
        }
        public static bool RightMouseDown
        {
            get
            {
                if (_bufferSize > 0)
                    return _mouseStates[0].RightButton == ButtonState.Pressed;
                else
                    return false;
            }
        }
        public static bool MiddleMouseDown
        {
            get
            {
                if (_bufferSize > 0)
                    return _mouseStates[0].MiddleButton == ButtonState.Pressed;
                else
                    return false;
            }
        }

        public static bool LeftMouseClick
        {
            get
            {
                if (_bufferSize > 1)
                {
                    return _mouseStates[0].LeftButton == ButtonState.Pressed && _mouseStates[1].LeftButton == ButtonState.Released;
                }

                else return false;
            }
        }
        public static bool RightMouseClick
        {
            get
            {
                if (_bufferSize > 1)
                {
                    return _mouseStates[0].RightButton == ButtonState.Pressed && _mouseStates[1].RightButton == ButtonState.Released;
                }

                else return false;
            }
        }
        public static bool MiddleMouseClick
        {
            get
            {
                if (_bufferSize > 1)
                {
                    return _mouseStates[0].MiddleButton == ButtonState.Pressed && _mouseStates[1].MiddleButton == ButtonState.Released;
                }

                else return false;
            }
        }

        public static bool LeftMouseRelease
        {
            get
            {
                if (_bufferSize > 1)
                {
                    return _mouseStates[0].LeftButton == ButtonState.Released && _mouseStates[1].LeftButton == ButtonState.Released;
                }

                else return false;
            }
        }
        public static bool RightMouseRelease
        {
            get
            {
                if (_bufferSize > 1)
                {
                    return _mouseStates[0].RightButton == ButtonState.Released && _mouseStates[1].RightButton == ButtonState.Pressed;
                }

                else return false;
            }
        }
        public static bool MiddleMouseRelease
        {
            get
            {
                if (_bufferSize > 1)
                {
                    return _mouseStates[0].MiddleButton == ButtonState.Released && _mouseStates[1].MiddleButton == ButtonState.Pressed;
                }

                else return false;
            }
        }

        public static bool MouseScrollDown
        {
            get
            {
                if (_bufferSize > 1)
                    return _mouseStates[0].ScrollWheelValue < _mouseStates[1].ScrollWheelValue;
                else
                    return false;
            }
        }
        public static bool MouseScrollUp
        {
            get
            {
                if (_bufferSize > 1)
                    return _mouseStates[0].ScrollWheelValue > _mouseStates[1].ScrollWheelValue;
                else
                    return false;
            }
        }


        //┌-----------------------------------------┐
        //|             Mousevariables              |
        //└-----------------------------------------┘
        public static Point MousePos
        {
            get
            {
                if (_bufferSize > 0)
                    return _mouseStates[0].Position;
                else
                    return Point.Zero;
            }
        }
        public static Rectangle MouseRect
        {
            get
            {
                return new Rectangle(MousePos, _onePoint);
            }
        }

        public static int MouseScrollState
        {
            get
            {
                if (_bufferSize > 0)
                    return _mouseStates[0].ScrollWheelValue;
                else
                    return 0;
            }
        }
       
    }
}
