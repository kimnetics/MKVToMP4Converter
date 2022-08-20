using System.Runtime.InteropServices;

namespace MKVToMP4Converter
{
    // Based on https://www.codeproject.com/Articles/5264831/How-to-Send-Inputs-using-Csharp
    // By Bojidar Qnkov
    public static class SendMouseAndKeyboard
    {
        public static void MouseWriteCursorPos()
        {
            var point = new Point();

            // This is meant to assist with finding a position to use.
            // The user must manually stop this method.
            do
            {
                GetCursorPos(out point);
                Console.WriteLine($"x: {point.x} y: {point.y}");
            } while (true);
        }

        public static void MouseSetCursorPos(int x, int y)
        {
            SetCursorPos(x, y);
        }

        public static void MouseClick()
        {
            var inputList = new List<Input>();

            inputList.Add(new Input
            {
                type = (int)InputType.Mouse,
                u = new InputUnion
                {
                    mi = new MouseInput
                    {
                        dwFlags = (uint)MouseEventF.LeftDown,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            });

            inputList.Add(new Input
            {
                type = (int)InputType.Mouse,
                u = new InputUnion
                {
                    mi = new MouseInput
                    {
                        dwFlags = (uint)MouseEventF.LeftUp,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            });

            var inputs = inputList.ToArray();
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
        }

        public static void KeyboardSendBackspace()
        {
            var inputList = new List<Input>();

            inputList.Add(new Input
            {
                type = (int)InputType.Keyboard,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = (ushort)8, // Backspace key.
                        dwFlags = (uint)KeyEventF.KeyDown,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            });

            inputList.Add(new Input
            {
                type = (int)InputType.Keyboard,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = (ushort)8, // Backspace key.
                        dwFlags = (uint)KeyEventF.KeyUp,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            });

            var inputs = inputList.ToArray();
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
        }

        public static void KeyboardSendCharacters(string characters)
        {
            var keyboardLayout = GetKeyboardLayout(0);

            var inputList = new List<Input>();

            foreach (char character in characters)
            {
                inputList.AddRange(HandleKeyPress(character, keyboardLayout));
            }

            var inputs = inputList.ToArray();
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
        }

        public static void KeyboardSendControlA()
        {
            var inputList = new List<Input>();

            inputList.Add(new Input
            {
                type = (int)InputType.Keyboard,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = (ushort)17, // CTRL key.
                        dwFlags = (uint)KeyEventF.KeyDown,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            });

            inputList.Add(new Input
            {
                type = (int)InputType.Keyboard,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = (ushort)65, // A key.
                        dwFlags = (uint)KeyEventF.KeyDown,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            });

            inputList.Add(new Input
            {
                type = (int)InputType.Keyboard,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = (ushort)65, // A key.
                        dwFlags = (uint)KeyEventF.KeyUp,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            });

            inputList.Add(new Input
            {
                type = (int)InputType.Keyboard,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = (ushort)17, // CTRL key.
                        dwFlags = (uint)KeyEventF.KeyUp,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            });

            var inputs = inputList.ToArray();
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
        }

        public static void KeyboardSendControlV()
        {
            var inputList = new List<Input>();

            inputList.Add(new Input
            {
                type = (int)InputType.Keyboard,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = (ushort)17, // CTRL key.
                        dwFlags = (uint)KeyEventF.KeyDown,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            });

            inputList.Add(new Input
            {
                type = (int)InputType.Keyboard,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = (ushort)86, // V key.
                        dwFlags = (uint)KeyEventF.KeyDown,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            });

            inputList.Add(new Input
            {
                type = (int)InputType.Keyboard,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = (ushort)86, // V key.
                        dwFlags = (uint)KeyEventF.KeyUp,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            });

            inputList.Add(new Input
            {
                type = (int)InputType.Keyboard,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = (ushort)17, // CTRL key.
                        dwFlags = (uint)KeyEventF.KeyUp,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            });

            var inputs = inputList.ToArray();
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
        }

        public static void KeyboardSendControlX()
        {
            var inputList = new List<Input>();

            inputList.Add(new Input
            {
                type = (int)InputType.Keyboard,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = (ushort)17, // CTRL key.
                        dwFlags = (uint)KeyEventF.KeyDown,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            });

            inputList.Add(new Input
            {
                type = (int)InputType.Keyboard,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = (ushort)88, // X key.
                        dwFlags = (uint)KeyEventF.KeyDown,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            });

            inputList.Add(new Input
            {
                type = (int)InputType.Keyboard,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = (ushort)88, // X key.
                        dwFlags = (uint)KeyEventF.KeyUp,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            });

            inputList.Add(new Input
            {
                type = (int)InputType.Keyboard,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = (ushort)17, // CTRL key.
                        dwFlags = (uint)KeyEventF.KeyUp,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            });

            var inputs = inputList.ToArray();
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
        }

        public static void KeyboardSendCR()
        {
            var inputList = new List<Input>();

            inputList.Add(new Input
            {
                type = (int)InputType.Keyboard,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = (ushort)13, // Enter key.
                        dwFlags = (uint)KeyEventF.KeyDown,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            });

            inputList.Add(new Input
            {
                type = (int)InputType.Keyboard,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = (ushort)13, // Enter key.
                        dwFlags = (uint)KeyEventF.KeyUp,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            });

            var inputs = inputList.ToArray();
            SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(Input)));
        }

        private static List<Input> HandleKeyPress(char character, IntPtr keyboardLayout)
        {
            short virtualKeyCode = VkKeyScanExA(character, keyboardLayout);

            bool shiftKeyPressed;
            if (virtualKeyCode > 255)
            {
                virtualKeyCode = (short)(virtualKeyCode - 256);
                shiftKeyPressed = true;
            }
            else
            {
                shiftKeyPressed = false;
            }

            var inputList = new List<Input>();

            if (shiftKeyPressed)
            {
                inputList.Add(new Input
                {
                    type = (int)InputType.Keyboard,
                    u = new InputUnion
                    {
                        ki = new KeyboardInput
                        {
                            wVk = (ushort)160, // Left Shift key.
                            dwFlags = (uint)KeyEventF.KeyDown,
                            dwExtraInfo = GetMessageExtraInfo()
                        }
                    }
                });
            }

            inputList.Add(new Input
            {
                type = (int)InputType.Keyboard,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = (ushort)virtualKeyCode,
                        dwFlags = (uint)KeyEventF.KeyDown,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            });

            inputList.Add(new Input
            {
                type = (int)InputType.Keyboard,
                u = new InputUnion
                {
                    ki = new KeyboardInput
                    {
                        wVk = (ushort)virtualKeyCode,
                        dwFlags = (uint)KeyEventF.KeyUp,
                        dwExtraInfo = GetMessageExtraInfo()
                    }
                }
            });

            if (shiftKeyPressed)
            {
                inputList.Add(new Input
                {
                    type = (int)InputType.Keyboard,
                    u = new InputUnion
                    {
                        ki = new KeyboardInput
                        {
                            wVk = (ushort)160, // Left Shift key.
                            dwFlags = (uint)KeyEventF.KeyUp,
                            dwExtraInfo = GetMessageExtraInfo()
                        }
                    }
                });
            }

            return inputList;
        }

        [Flags]
        public enum InputType
        {
            Mouse = 0,
            Keyboard = 1,
            Hardware = 2
        }

        [Flags]
        public enum MouseEventF
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,
            HWheel = 0x01000,
            MoveNoCoalesce = 0x2000,
            VirtualDesk = 0x4000,
            Absolute = 0x8000
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MouseInput
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [Flags]
        public enum KeyEventF
        {
            KeyDown = 0x0000,
            ExtendedKey = 0x0001,
            KeyUp = 0x0002,
            Unicode = 0x0004,
            Scancode = 0x0008
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KeyboardInput
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HardwareInput
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct InputUnion
        {
            [FieldOffset(0)] public MouseInput mi;
            [FieldOffset(0)] public KeyboardInput ki;
            [FieldOffset(0)] public HardwareInput hi;
        }

        public struct Input
        {
            public uint type;
            public InputUnion u;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public int x;
            public int y;
        }

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point lpPoint);

        [DllImport("user32.dll")]
        private static extern IntPtr GetKeyboardLayout(uint idThread);

        [DllImport("user32.dll")]
        private static extern IntPtr GetMessageExtraInfo();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, Input[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        private static extern short VkKeyScanExA(char ch, IntPtr dwhkl);
    }
}
