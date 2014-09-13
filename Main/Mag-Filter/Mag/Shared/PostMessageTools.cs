namespace Mag.Shared
{
    using Decal.Adapter;
    using System;
    using System.Runtime.InteropServices;

    public static class PostMessageTools
    {
        private const byte VK_CONTROL = 0x11;
        private const byte VK_PAUSE = 0x13;
        private const byte VK_RETURN = 13;
        private const byte VK_SPACE = 0x20;
        private const int WM_CHAR = 0x102;
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;
        private const int WM_LBUTTONDOWN = 0x201;
        private const int WM_LBUTTONUP = 0x202;
        private const int WM_MOUSEMOVE = 0x200;

        private static byte CharCode(char Char)
        {
            switch (char.ToLower(Char))
            {
                case 'a':
                    return 0x41;

                case 'b':
                    return 0x42;

                case 'c':
                    return 0x43;

                case 'd':
                    return 0x44;

                case 'e':
                    return 0x45;

                case 'f':
                    return 70;

                case 'g':
                    return 0x47;

                case 'h':
                    return 0x48;

                case 'i':
                    return 0x49;

                case 'j':
                    return 0x4a;

                case 'k':
                    return 0x4b;

                case 'l':
                    return 0x4c;

                case 'm':
                    return 0x4d;

                case 'n':
                    return 0x4e;

                case 'o':
                    return 0x4f;

                case 'p':
                    return 80;

                case 'q':
                    return 0x51;

                case 'r':
                    return 0x52;

                case 's':
                    return 0x53;

                case 't':
                    return 0x54;

                case 'u':
                    return 0x55;

                case 'v':
                    return 0x56;

                case 'w':
                    return 0x57;

                case 'x':
                    return 0x58;

                case 'y':
                    return 0x59;

                case 'z':
                    return 90;

                case '/':
                    return 0xbf;

                case ' ':
                    return 0x20;
            }
            return 0x20;
        }

        public static void ClickNo()
        {
            SendMouseClick(480, 0x146);
        }

        public static void ClickOK()
        {
            SendMouseClick(400, 0x146);
        }

        public static void ClickYes()
        {
            SendMouseClick(320, 0x146);
        }

        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        private static extern bool PostMessage(int hhwnd, uint msg, IntPtr wparam, UIntPtr lparam);
        private static byte ScanCode(char Char)
        {
            switch (char.ToLower(Char))
            {
                case 'a':
                    return 30;

                case 'b':
                    return 0x30;

                case 'c':
                    return 0x2e;

                case 'd':
                    return 0x20;

                case 'e':
                    return 0x12;

                case 'f':
                    return 0x21;

                case 'g':
                    return 0x22;

                case 'h':
                    return 0x23;

                case 'i':
                    return 0x17;

                case 'j':
                    return 0x24;

                case 'k':
                    return 0x25;

                case 'l':
                    return 0x26;

                case 'm':
                    return 50;

                case 'n':
                    return 0x31;

                case 'o':
                    return 0x18;

                case 'p':
                    return 0x19;

                case 'q':
                    return 0x10;

                case 'r':
                    return 0x13;

                case 's':
                    return 0x1f;

                case 't':
                    return 20;

                case 'u':
                    return 0x16;

                case 'v':
                    return 0x2f;

                case 'w':
                    return 0x11;

                case 'x':
                    return 0x2d;

                case 'y':
                    return 0x15;

                case 'z':
                    return 0x2c;

                case '/':
                    return 0x35;

                case ' ':
                    return 0x39;
            }
            return 0;
        }

        public static void SendCntrl(char ch)
        {
            PostMessage(CoreManager.Current.Decal.Hwnd.ToInt32(), 0x100, (IntPtr) 0x11, (UIntPtr) 0x1d0001);
            PostMessage(CoreManager.Current.Decal.Hwnd.ToInt32(), 0x100, (IntPtr) CharCode(ch), (UIntPtr) 0x100001);
            PostMessage(CoreManager.Current.Decal.Hwnd.ToInt32(), 0x101, (IntPtr) CharCode(ch), (UIntPtr) (0xC0100001));
            PostMessage(CoreManager.Current.Decal.Hwnd.ToInt32(), 0x101, (IntPtr) 0x11, (UIntPtr) (0xC01D0001));
        }

        public static void SendEnter()
        {
            PostMessage(CoreManager.Current.Decal.Hwnd.ToInt32(), 0x100, (IntPtr) 13, (UIntPtr) 0x1c0001);
            PostMessage(CoreManager.Current.Decal.Hwnd.ToInt32(), 0x101, (IntPtr) 13, (UIntPtr) (0xC01C0001));
        }

        public static void SendF12()
        {
            PostMessage(CoreManager.Current.Decal.Hwnd.ToInt32(), 0x100, (IntPtr) 0x7b, (UIntPtr) 0x580001);
            PostMessage(CoreManager.Current.Decal.Hwnd.ToInt32(), 0x101, (IntPtr) 0x7b, (UIntPtr) (0xC0580001));
        }

        public static void SendF4()
        {
            PostMessage(CoreManager.Current.Decal.Hwnd.ToInt32(), 0x100, (IntPtr) 0x73, (UIntPtr) 0x3e0001);
            PostMessage(CoreManager.Current.Decal.Hwnd.ToInt32(), 0x101, (IntPtr)0x73, (UIntPtr)(0xC03E0001));
        }

        public static void SendMouseClick(int x, int y)
        {
            int num = (y * 0x10000) + x;
            PostMessage(CoreManager.Current.Decal.Hwnd.ToInt32(), 0x200, IntPtr.Zero, (UIntPtr) num);
            PostMessage(CoreManager.Current.Decal.Hwnd.ToInt32(), 0x201, (IntPtr) 1, (UIntPtr) num);
            PostMessage(CoreManager.Current.Decal.Hwnd.ToInt32(), 0x202, IntPtr.Zero, (UIntPtr) num);
        }

        public static void SendMsg(string msg)
        {
            foreach (char ch in msg)
            {
                byte num = CharCode(ch);
                uint num2 = (uint) ((ScanCode(ch) << 0x10) | 1);
                PostMessage(CoreManager.Current.Decal.Hwnd.ToInt32(), 0x100, (IntPtr) num, (UIntPtr) num2);
                PostMessage(CoreManager.Current.Decal.Hwnd.ToInt32(), 0x101, (IntPtr) num, (UIntPtr) (0xc0000000 | num2));
            }
        }

        public static void SendPause()
        {
            PostMessage(CoreManager.Current.Decal.Hwnd.ToInt32(), 0x100, (IntPtr) 0x13, (UIntPtr) 0x450001);
            PostMessage(CoreManager.Current.Decal.Hwnd.ToInt32(), 0x101, (IntPtr) 0x13, (UIntPtr) (0xC0450001));
        }

        public static void SendSpace()
        {
            PostMessage(CoreManager.Current.Decal.Hwnd.ToInt32(), 0x100, (IntPtr) 0x20, (UIntPtr) 0x390001);
            PostMessage(CoreManager.Current.Decal.Hwnd.ToInt32(), 0x101, (IntPtr) 0x20, (UIntPtr) (0xC0390001));
        }
    }
}

