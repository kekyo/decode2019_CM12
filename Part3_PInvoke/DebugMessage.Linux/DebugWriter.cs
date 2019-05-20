using System.Runtime.InteropServices;

namespace DebugMessage
{
    /// <summary>
    /// デバッグ出力用のクラスです。
    /// </summary>
    public class DebugWriter
    {
        private const int LOG_PID = 1;
        private const int LOG_USER = 1<<3;
        private const int LOG_DEBUG = 7;

        [DllImport("libc", CharSet=CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void openlog(string ident, int option, int facility);

        [DllImport("libc", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        private static extern void syslog(int priority, string format);

        [DllImport("libc", CallingConvention = CallingConvention.Cdecl)]
        private static extern void closelog();

        /// <summary>
        /// デバッグ出力を実行します。
        /// </summary>
        /// <param name="message">デバッグメッセージ</param>
        public void Write(string message)
        {
            // syslogに出力する
            openlog("DebugMessage", LOG_PID, LOG_USER);
            syslog(LOG_DEBUG, message);
            closelog();
        }
    }
}
