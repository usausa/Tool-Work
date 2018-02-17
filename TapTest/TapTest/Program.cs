namespace TapTest
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;

    using HidLibrary;

    public static class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                var device = HidDevices.Enumerate()
                    .FirstOrDefault(x => x.Attributes.ProductHexId == "0x2201" & x.Attributes.VendorHexId == "0x040B");

                if (device != null)
                {
                    device.OpenDevice();

                    Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss"));

                    device.Write(new byte[] { 0x02, 0x18, 0x0A }, 100);

                    var data = device.Read(100);
                    if (data.Status == HidDeviceData.ReadStatus.Success)
                    {
                        Debug.WriteLine(BitConverter.ToString(data.Data));
                    }

                    var first = true;
                    while (data.Data[16] == 0x0A)
                    {
                        data = device.Read(100);
                        if (data.Status == HidDeviceData.ReadStatus.Success)
                        {
                            if (first)
                            {
                                Debug.WriteLine(BitConverter.ToString(data.Data));
                                first = false;
                            }
                        }
                    }

                    device.CloseDevice();
                }

                Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss"));

                Thread.Sleep(10000);
            }
        }
    }
}
