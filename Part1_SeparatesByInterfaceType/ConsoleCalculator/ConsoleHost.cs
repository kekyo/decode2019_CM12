using System;
using System.Threading.Tasks;

using Calculator;

namespace ConsoleCalculator
{
    /// <summary>
    /// コンソール(コマンドライン)を使用して計算を処理させるホストクラスです。
    /// </summary>
    internal sealed class ConsoleHost : ICalculatorHost
    {
        /// <summary>
        /// ホストに対して指定された書式で文字列を出力します。
        /// </summary>
        /// <param name="format">書式</param>
        /// <param name="args">追加引数群</param>
        /// <returns>タスク</returns>
        public Task<string> ReadLineAsync()
        {
            // Consoleクラスにバイパスする
            return Console.In.ReadLineAsync();
        }

        /// <summary>
        /// ホストから文字列を入力します。
        /// </summary>
        /// <returns>取得した文字列</returns>
        public Task WriteLineAsync(string format, params object[] args)
        {
            // Consoleクラスにバイパスする
            return Console.Out.WriteLineAsync(string.Format(format, args));
        }
    }
}
