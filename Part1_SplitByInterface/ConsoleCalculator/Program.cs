using System.Threading.Tasks;

using Calculator;

namespace ConsoleCalculator
{
    public static class Program
    {
        public static Task Main(string[] args)
        {
            // コンソールに入出力を行うICalculatorHostインターフェイスの実装
            ICalculatorHost host = new ConsoleHost();

            // 引数から依存注入を行い、計算の入出力をコンソールに接続する
            return CalculatorImplementation.CalculateAsync(host);
        }
    }
}
