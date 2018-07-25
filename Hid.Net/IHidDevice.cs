using System;
using System.Threading.Tasks;

namespace Hid.Net
{
    public interface IHidDevice : IDisposable
    {
        event EventHandler Connected;
        event EventHandler Disconnected;

        Task<bool> GetIsConnectedAsync();

        Task<byte[]> ReadAsync();
        Task WriteAsync(byte[] data);

        int VendorId { get; }
        int ProductId { get; }
    }
}