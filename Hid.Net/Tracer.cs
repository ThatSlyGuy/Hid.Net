using System.Diagnostics;

namespace Hid.Net
{
    public class Tracer
    {
        public static void Trace(bool isWrite, byte[] data, int length)
        {
            Debug.WriteLine($"({string.Join(",", data)}) - {(isWrite ? "Write" : "Read")} ({length})");
        }
    }
}
