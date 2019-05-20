using System;
using System.Threading.Tasks;

using Host.Core;

namespace Calculator
{
    public static class Program
    {
        /// <summary>
        /// メインエントリポイントです。
        /// </summary>
        /// <param name="args">コマンドライン引数</param>
        /// <returns>戻り値</returns>
        [STAThread]
        public static int Main(string[] args)
        {
            // メモ: Host.Referenceのプラットフォーム実装は、メインスレッドがSTAに属していることを期待する場合がある。
            //   本サンプルコードにおいては、Bait and switchで置き換えられたアセンブリがHost.Wpfの場合に該当する。
            //   そのため、メインメソッドは常にSTAThread属性で修飾しておく必要がある。
            //   Host.Consoleの場合、STAに属している必要はないが、属していても実害はない。

            // 非同期メインメソッドを実行する
            var application = new Application();
            return application.Run(MainAsync(args));
        }

        /// <summary>
        /// 非同期メインメソッドです。
        /// </summary>
        /// <param name="args">コマンドライン引数</param>
        /// <returns>戻り値</returns>
        private static async Task<int> MainAsync(string[] args)
        {
            // 計算を実行する
            await CalculatorImplementation.CalculateAsync();
            return 0;
        }
    }
}
