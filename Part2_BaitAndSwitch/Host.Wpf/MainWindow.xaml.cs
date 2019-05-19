using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WpfCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 現在非同期的に待機している入力を示すTCSです。
        /// </summary>
        private volatile TaskCompletionSource<string> readCompletion;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        internal MainWindow()
        {
            InitializeComponent();

            this.execute.Click += Execute_Click;
        }

        /// <summary>
        /// Executeボタンがクリックされた。
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント情報</param>
        private void Execute_Click(object sender, RoutedEventArgs e)
        {
            // TCSが存在する場合だけ処理する
            var readCompletion = Interlocked.Exchange(ref this.readCompletion, null);
            if (readCompletion != null)
            {
                var text = this.inputBox.Text;
                this.inputBox.Text = string.Empty;

                this.outputBox.AppendText(text + "\r\n");

                readCompletion.TrySetResult(text);
            }
        }

        /// <summary>
        /// ホストから文字列を入力します。
        /// </summary>
        /// <returns>取得した文字列</returns>
        internal Task<string> ReadLineAsync()
        {
            // 新たに入力を待機するTCSを用意する
            var readCompletion = new TaskCompletionSource<string>();

            // 待機用のTCSを入れ替える
            var lastReadCompletion = Interlocked.Exchange(ref this.readCompletion, readCompletion);

            // 前回のTCSが完了していないかもしれないので、キャンセルしておく
            lastReadCompletion?.TrySetCanceled();

            return readCompletion.Task;
        }

        /// <summary>
        /// ホストに対して指定された書式で文字列を出力します。
        /// </summary>
        /// <param name="format">書式</param>
        /// <param name="args">追加引数群</param>
        /// <returns>タスク</returns>
        internal Task WriteLineAsync(string format, params object[] args)
        {
            this.outputBox.AppendText(string.Format(format, args) + "\r\n");

            // TextBoxへの追加は同期的に処理できるため、完了タスクを返す
            return Task.FromResult(0);
        }
    }
}
