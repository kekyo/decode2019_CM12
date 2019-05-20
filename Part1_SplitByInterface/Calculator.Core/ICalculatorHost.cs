using System;
using System.Threading.Tasks;

namespace Calculator
{
    /// <summary>
    /// ホストインターフェースを抽象化するインターフェイスです。
    /// </summary>
    public interface ICalculatorHost
    {
        /// <summary>
        /// ホストに対して指定された書式で文字列を出力します。
        /// </summary>
        /// <param name="format">書式</param>
        /// <param name="args">追加引数群</param>
        /// <returns>タスク</returns>
        Task WriteLineAsync(string format, params object[] args);

        /// <summary>
        /// ホストから文字列を入力します。
        /// </summary>
        /// <returns>取得した文字列</returns>
        Task<string> ReadLineAsync();
    }
}
