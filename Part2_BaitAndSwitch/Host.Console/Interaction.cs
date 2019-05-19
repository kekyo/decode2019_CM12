using System;
using System.Threading.Tasks;

namespace Host.Core
{
    /// <summary>
    /// コンソール(コマンドライン)を使用して計算の入出力を処理するクラスです。
    /// </summary>
    public class Interaction : IDisposable
    {
        /// <summary>
        /// Disposeメソッドです。
        /// </summary>
        public void Dispose()
        {
            // コンソールの場合、特に破棄処理は必要ないため、何もしない
        }

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
