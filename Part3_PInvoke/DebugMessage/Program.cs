using System;

namespace DebugMessage
{
    class Program
    {
        static void Main(string[] args)
        {
            var writer = new DebugWriter();
            writer.Write(args[0]);
        }
    }
}
