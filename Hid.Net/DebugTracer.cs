using System.Diagnostics;

namespace Hid.Net
{
    public class DebugTracer : ITracer
    {
        public void Trace(bool isWrite, byte[] data)
        {
            Debug.WriteLine($"({string.Join(",", data)}) - {(isWrite ? "Write" : "Read")} ({data.Length})");
        }
    }
}
