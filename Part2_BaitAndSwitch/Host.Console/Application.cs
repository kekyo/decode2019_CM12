using System.Threading.Tasks;

namespace Host.Core
{
    public sealed class Application
    {
        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public Application()
        {
        }

        /// <summary>
        /// アプリケーションを実行可能にし、同期的に待機します。
        /// </summary>
        /// <param name="task">対応する非同期タスク</param>
        /// <returns>戻り値</returns>
        public int Run(Task<int> task)
        {
            // Taskを同期的に待機する。
            // コンソールの場合、ハードブロッキングで待機してもタスクはワーカースレッドで駆動されるので問題ない。
            return task.Result;
        }
    }
}
