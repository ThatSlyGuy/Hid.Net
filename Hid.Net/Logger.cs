using System;
using System.Runtime.CompilerServices;

#if(HIDNET)
namespace Hid.Net
#else
namespace CryptoCurrency.Net
#endif
{
    public class Logger
    {
        public static void Log(string message, Exception ex, string section, [CallerMemberName] string callerMemberName = null)
        {
            var formattedText = $"Message: {message}\r\nTime: {DateTime.Now}\r\nSection: {section}\r\nCalling Member: {callerMemberName}\r\nError: {ex}";
            System.Diagnostics.Debug.WriteLine($"--------------------------------------\r\n{formattedText}\r\n--------------------------------------");
        }
    }
}
