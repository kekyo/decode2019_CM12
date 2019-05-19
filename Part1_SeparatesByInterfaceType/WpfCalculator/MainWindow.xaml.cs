using Calculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ICalculatorHost
    {
        /// <summary>
        /// 現在非同期的に待機している入力を示すTCSです。
        /// </summary>
        private volatile TaskCompletionSource<string> readCompletion;

        /// <summary>
        /// コンストラクタです。
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            this.execute.Click += Execute_Click;
            this.Loaded += MainWindow_Loaded;
        }

        /// <summary>
        /// ウインドウがロードされた場合に呼び出されます。
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント情報</param>
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // このサンプルコードでは、ICalculatorHostを直接実装したので、自分自身をホストとして依存注入する
            await CalculatorImplementation.CalculateAsync(this);
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
        public Task<string> ReadLineAsync()
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
        public Task WriteLineAsync(string format, params object[] args)
        {
            this.outputBox.AppendText(string.Format(format, args) + "\r\n");

            // TextBoxへの追加は同期的に処理できるため、完了タスクを返す
            return Task.CompletedTask;
        }
    }
}
