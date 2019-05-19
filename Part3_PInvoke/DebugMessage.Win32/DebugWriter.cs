using System.Runtime.InteropServices;

namespace DebugMessage
{
    /// <summary>
    /// デバッグ出力用のクラスです。
    /// </summary>
    public class DebugWriter
    {
        [DllImport("kernel32.dll", CharSet=CharSet.Unicode)]
        private static extern void OutputDebugString(string lpOutputString);

        /// <summary>
        /// デバッグ出力を実行します。
        /// </summary>
        /// <param name="message">デバッグメッセージ</param>
        public void Write(string message) =>
            OutputDebugString(message);
    }
}
