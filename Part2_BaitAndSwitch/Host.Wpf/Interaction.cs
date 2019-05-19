using System;
using System.Threading;
using System.Threading.Tasks;

using WpfCalculator;

namespace Host.Core
{
    /// <summary>
    /// WPFを使用して計算の入出力を処理するクラスです。
    /// </summary>
    public class Interaction : IDisposable
    {
        private volatile MainWindow mainWindow;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public Interaction()
        {
            this.mainWindow = new MainWindow();
            this.mainWindow.Show();
        }

        /// <summary>
        /// Disposeメソッドです。
        /// </summary>
        public void Dispose()
        {
            var mainWindow = Interlocked.Exchange(ref this.mainWindow, null);
            if (mainWindow != null)
            {
                mainWindow.Close();
            }
        }

        /// <summary>
        /// ホストに対して指定された書式で文字列を出力します。
        /// </summary>
        /// <param name="format">書式</param>
        /// <param name="args">追加引数群</param>
        /// <returns>タスク</returns>
        public Task<string> ReadLineAsync()
        {
            // ウインドウにバイパスする
            return this.mainWindow.ReadLineAsync();
        }

        /// <summary>
        /// ホストから文字列を入力します。
        /// </summary>
        /// <returns>取得した文字列</returns>
        public Task WriteLineAsync(string format, params object[] args)
        {
            // ウインドウにバイパスする
            return this.mainWindow.WriteLineAsync(string.Format(format, args));
        }
    }
}
