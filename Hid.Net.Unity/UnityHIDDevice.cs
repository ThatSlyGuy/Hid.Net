using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hid.Net.Unity
{
    public sealed class UnityHIDDevice : IHidDevice
    {
        public event EventHandler Connected;
        public event EventHandler Disconnected;

        private HidLibrary.HidDevice hidDevice;

        public int VendorId => hidDevice.Attributes.VendorId;

        public int ProductId => hidDevice.Attributes.ProductId;

        private UnityHIDDevice(HidLibrary.HidDevice hidDevice)
        {
            this.hidDevice = hidDevice;

            hidDevice.Inserted += () => Connected(null, null);
            hidDevice.Removed += () => Disconnected(null, null);
        }

        public static IEnumerable<UnityHIDDevice> GetConnectedDevices(int vendorId, int? productId, short? usagePage, short? usage)
        {
            List<HidLibrary.HidDevice> devices = new List<HidLibrary.HidDevice>();
            if (!productId.HasValue)
                devices.AddRange(HidLibrary.HidDevices.Enumerate(vendorId));
            else
                devices.AddRange(HidLibrary.HidDevices.Enumerate(vendorId, productId.Value));

            var hidDevices = devices.Where(d => usagePage == null || usage == null || (d.Capabilities.UsagePage == usagePage && (ushort)d.Capabilities.Usage == usage));

            return hidDevices.Select(d => new UnityHIDDevice(d));
        }

        public void Dispose()
        {
            hidDevice.Dispose();
        }

        public Task<bool> GetIsConnectedAsync()
        {
            return Task<bool>.Factory.StartNew(() => hidDevice.IsConnected);
        }

        public Task<byte[]> ReadAsync()
        {
            return Task<byte[]>.Factory.StartNew(() =>
            {
                var result = hidDevice.Read();

                if (result.Status == HidLibrary.HidDeviceData.ReadStatus.Success)
                {
                    if (result.Data?.Length - 1 <= 0)
                        return new byte[0];

                    return result.Data.Skip(1).ToArray();
                }

                return new byte[0];
            });
        }

        public Task WriteAsync(byte[] data)
        {
            return Task.Factory.StartNew(() =>
            {
                var sent = new byte[1].Concat(data).ToArray();
                hidDevice.Write(sent);
            });
        }
    }
}
