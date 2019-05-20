using System;
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
            // Bait and switchのビルド用ライブラリなので、実装は存在しない
            throw new NotImplementedException();
        }

        /// <summary>
        /// アプリケーションを実行可能にし、同期的に待機します。
        /// </summary>
        /// <param name="task">対応する非同期タスク</param>
        /// <returns>戻り値</returns>
        public int Run(Task<int> task)
        {
            // Bait and switchのビルド用ライブラリなので、実装は存在しない
            throw new NotImplementedException();
        }
    }
}
