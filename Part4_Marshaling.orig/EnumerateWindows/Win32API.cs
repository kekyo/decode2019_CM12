using System;
using System.Runtime.InteropServices;
using System.Text;

namespace EnumerateWindows
{
    internal static class Win32API
    {
        private delegate bool WNDENUMPROC(IntPtr handle, IntPtr param);

        // ウインドウを列挙するWin32 API
        // 第二引数のLPARAM値を、関数ポインタに引き渡すことが出来る。
        // BOOL EnumWindows(WNDENUMPROC lpEnumFunc, LPARAM lParam);
        [DllImport("user32.dll")]
        private static extern bool EnumWindows(WNDENUMPROC cb, IntPtr param);

        /// <summary>
        /// EnumWindows APIから呼び出されるコールバックメソッドです。
        /// </summary>
        /// <param name="handle">HWND</param>
        /// <param name="param">第二引数で指定されたパラメータ</param>
        /// <returns></returns>
        private static bool EnumWindowsCallback(IntPtr handle, IntPtr param)
        {
            // 引数のパラメータからデリゲートを復元する
            var actionHandle = GCHandle.FromIntPtr(param);
            var action = (Action<IntPtr>)actionHandle.Target;

            // デリゲートを実行する
            action(handle);

            return true;
        }

        /// <summary>
        /// EnumWindows APIをラップして、デリゲートを使用出来るようにしました。
        /// </summary>
        /// <param name="action">Windowを見つけたときに呼び出されるデリゲート。引数はウインドウハンドル(HWND)</param>
        public static void EnumWindows(Action<IntPtr> action)
        {
            // 注意: この例では、actionデリゲートを手動マーシャリングしているが、
            //   その目的はGCの追跡を可能にすることではなく、マネージド参照をIntPtrの抽象化された値に
            //   変換することにある。
            //   GCの追跡は、actionがメソッドの引数となっている時点で成立しているので、
            //   解放される心配はない(EnumWindowsCallbackも同様)。

            // 手動マーシャリング: デリゲートインスタンスに対応するハンドル値を得る。
            //   AddrOfPinnedObjectは(マーシャリング可能なら)対象を直接指すポインタを得るが、
            //   ToIntPtrは単にハンドルの抽象値を得て、後でFromIntPtrで復元できるだけなので注意。
            //   この用途の場合は、Pinnedを指定してアドレスまで固定する必要はない。
            //   (アドレスを固定するとインスタンスを移動できなくなるので、GCの自由度は低くなる)
            var actionHandle = GCHandle.Alloc(action);
            var actionHandlePtr = GCHandle.ToIntPtr(actionHandle);

            try
            {
                // EnumWindows APIを実行する。パラメータにはハンドル値を渡す。
                //   EnumWindowsCallbackは、暗黙デリゲート-->関数ポインタ、として自動マーシャリングされている。
                //   (暗黙に生成されている)デリゲートはGCが追跡できるので、意図せず解放される問題はない。
                //   デリゲートを一旦ローカル変数に入れても同じ結果となる。
                EnumWindows(EnumWindowsCallback, actionHandlePtr);
            }
            finally
            {
                // 手動マーシャリングで得たハンドルを解放する。
                // 確実に解放するために、try-finallyブロックで保護する。
                actionHandle.Free();
            }
        }

        // タイトル文字列を取得するAPI
        [DllImport("user32.dll")]
        private static extern bool GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        /// <summary>
        /// 指定されたウインドウのタイトル文字列を取得します。
        /// </summary>
        /// <param name="hWnd">HWND</param>
        /// <returns>タイトル文字列</returns>
        public static string GetWindowText(IntPtr hWnd)
        {
            // 可変長の文字列を受け取る場合は、LPWSTRやLPSTRに対して特別にStringBuilderを使うことが出来る。
            // API呼び出しから戻ると、自動マーシャリング機能によって、正しい文字列長に調整される。
            // 文字列長の判定は、終端NULLの検索によって行われる。
            var sb = new StringBuilder(256);
            GetWindowText(hWnd, sb, sb.Capacity);

            return sb.ToString();
        }
    }
}
