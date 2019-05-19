using System;
using System.Runtime.InteropServices;

namespace Calculator
{
    internal static class Application
    {
        private const uint WM_QUIT = 0x0012;

        [StructLayout(LayoutKind.Sequential)]
        private struct MSG
        {
            public IntPtr hwnd;
            public uint message;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public POINT pt;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            int x;
            int y;
        }

        [DllImport("user32.dll")]
        private static extern int GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [DllImport("user32.dll")]
        private static extern int TranslateMessage(ref MSG lpMsg);

        [DllImport("user32.dll")]
        private static extern IntPtr DispatchMessage(ref MSG lpMsg);

        [DllImport("user32.dll")]
        private static extern bool PostThreadMessage(uint threadId, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern uint GetCurrentThreadId();

        private static uint boundThreadId;

        /// <summary>
        /// Win32メッセージループを終了させます。
        /// </summary>
        public static void Exit() =>
            PostThreadMessage(boundThreadId, WM_QUIT, IntPtr.Zero, IntPtr.Zero);

        /// <summary>
        /// Win32メッセージループを実行します。
        /// </summary>
        public static void Run()
        {
            boundThreadId = GetCurrentThreadId();

            // メッセージループ
            while (GetMessage(out var msg, IntPtr.Zero, 0, 0) != 0)
            {
                TranslateMessage(ref msg);
                DispatchMessage(ref msg);
            }
        }
    }
}
