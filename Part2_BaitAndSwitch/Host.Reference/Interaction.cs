using System;
using System.Threading.Tasks;

namespace Host.Core
{
    /// <summary>
    /// 計算の入出力を処理するクラスです。
    /// </summary>
    public class Interaction : IDisposable
    {
        /// <summary>
        /// Disposeメソッドです。
        /// </summary>
        public void Dispose()
        {
            // Bait and switchのビルド用ライブラリなので、実装は存在しない
        }

        /// <summary>
        /// ホストに対して指定された書式で文字列を出力します。
        /// </summary>
        /// <param name="format">書式</param>
        /// <param name="args">追加引数群</param>
        /// <returns>タスク</returns>
        public Task<string> ReadLineAsync()
        {
            // Bait and switchのビルド用ライブラリなので、実装は存在しない
            return null;
        }

        /// <summary>
        /// ホストから文字列を入力します。
        /// </summary>
        /// <returns>取得した文字列</returns>
        public Task WriteLineAsync(string format, params object[] args)
        {
            // Bait and switchのビルド用ライブラリなので、実装は存在しない
            return null;
        }
    }
}
