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

            // TIPS: コンソールアプリケーションであっても、メインスレッドがSTA前提でメッセージループを持つ事を
            //   想定しなければならない場合がある。
            //   例えば、コンソールアプリケーションとして開始しておきながら、途中で何らかのライブラリが
            //   GUIを表示する場合が考えられる。
            //   GUIがモーダルダイアログウインドウではない場合、暗黙のメッセージループが必要となる。
            //   そういったライブラリやアプリケーションに備えて、コンソールを対象としていても
            //   メッセージループを構成するのは、より良い振る舞いとなる。
            //   但し、メッセージループの実装はプラットフォーム依存となるため、
            //   Host.Consoleプロジェクトを更に細分化する必要がある。
            //   例えば:
            //     * Win32(Win32/Windows Forms/WPF): (https://docs.microsoft.com/en-us/windows/desktop/winmsg/using-messages-and-message-queues#creating-a-message-loop)
            //       * Win32環境では、Windows FormsやWPFのApplicationクラスやそれぞれのSynchContextを流用すると実装が楽になるが、
            //         それらのライブラリに依存することになる。WPFで実装する例は、Host.Wpfプロジェクトを参照。
            //     * Gnome(GTK+): (https://developer.gnome.org/gtk3/stable/gtk3-General.html#gtk-main)
            //   Bait and switchでアセンブリ分割する方法以外に、実行時に動的に環境を判断して、
            //   プラットフォーム依存処理を切り替える手法も考えられる。
        }
    }
}
