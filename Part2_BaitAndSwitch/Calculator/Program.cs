using System;
using System.Threading.Tasks;

namespace Calculator
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            // 通常、Windows FormsやWPFでは、メインスレッドがSTAであることを要求するため、
            // STAThread属性を適用します。
            // このアセンブリでは、MainメソッドがWPFに一切依存しないものの、非同期処理前提としているため、
            // STAThread属性を適用しても、その後Task.Wait()を実行するとデッドロックを起こします。
            // したがって、ここで手動でメッセージポンプを回します。

            // MainAsyncが非同期的に完了した場合にメッセージループを終了させる
            MainAsync(args).
                ContinueWith(_ => Application.Exit());

            // メッセージループを実行する
            Application.Run();
        }

        public static Task MainAsync(string[] args)
        {
            // 計算を実行する
            return CalculatorImplementation.CalculateAsync();
        }
    }
}
