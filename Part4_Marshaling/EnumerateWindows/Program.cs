using System;

namespace EnumerateWindows
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            // 識別可能なすべてのウインドウを列挙し、タイトルを出力する
            Win32API.EnumWindows(hWnd =>
                Console.WriteLine(Win32API.GetWindowText(hWnd)));
        }
    }
}
