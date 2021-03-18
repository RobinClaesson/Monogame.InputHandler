﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace MonoGame.Inputs
{
    public class InputHandler
    {
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