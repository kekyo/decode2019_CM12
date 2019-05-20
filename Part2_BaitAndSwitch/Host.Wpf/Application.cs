using System.Threading;
using System.Threading.Tasks;

namespace Host.Core
{
    public sealed class Application
    {
        private readonly System.Windows.Application wpfApplication;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public Application()
        {
            // WPF Applicationクラスのインスタンスを取得
            this.wpfApplication = new System.Windows.Application();

            // 非同期処理が現在のスレッド上で正しく処理されるように、SynchContextをセットアップする
            SynchronizationContext.SetSynchronizationContext(
                new System.Windows.Threading.DispatcherSynchronizationContext());
        }

        /// <summary>
        /// アプリケーションを実行可能にし、同期的に待機します。
        /// </summary>
        /// <param name="task">対応する非同期タスク</param>
        /// <returns>戻り値</returns>
        public int Run(Task<int> task)
        {
            // タスクの継続で、WPF Applicationをシャットダウンするようにセットアップする
            task.ContinueWith(t => wpfApplication.Shutdown(t.Result));

            // メッセージループの実行
            return this.wpfApplication.Run();
        }
    }
}
