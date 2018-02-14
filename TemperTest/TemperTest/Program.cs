namespace TemperTest
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    using HidLibrary;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var interfaces = HidDevices.Enumerate()
                .Where(x => x.Attributes.ProductHexId == "0x7401" & x.Attributes.VendorHexId == "0x0C45")
                .ToList();
            var control = interfaces.Find(x => x.DevicePath.Contains("mi_00"));
            var bulk = interfaces.Find(x => x.DevicePath.Contains("mi_01"));

            if ((control == null) || (bulk == null))
            {
                return;
            }

            if (!control.IsConnected)
            {
                return;
            }

            control.OpenDevice();
            bulk.OpenDevice();

            control.ReadManufacturer(out var manufacturer);
            control.ReadProduct(out var product);
            control.ReadSerialNumber(out var serial);
            Debug.WriteLine(Encoding.ASCII.GetString(Encoding.Convert(Encoding.Unicode, Encoding.ASCII, manufacturer)).TrimEnd('\0'));
            Debug.WriteLine(Encoding.ASCII.GetString(Encoding.Convert(Encoding.Unicode, Encoding.ASCII, product)).TrimEnd('\0'));
            Debug.WriteLine(Encoding.ASCII.GetString(Encoding.Convert(Encoding.Unicode, Encoding.ASCII, serial)).TrimEnd('\0'));

            // Initialize
            control.Write(new byte[] { 0x00, 0x01, 0x01 }, 100);
            var data = control.Read(100);
            if (data.Status != HidDeviceData.ReadStatus.Success)
            {
                return;
            }

            Debug.WriteLine(BitConverter.ToString(data.Data));

            // Initialize1
            bulk.Write(new byte[] { 0x00, 0x01, 0x82, 0x77, 0x01, 0x00, 0x00, 0x00, 0x00 }, 100);
            data = bulk.Read(100);
            if (data.Status != HidDeviceData.ReadStatus.Success)
            {
                return;
            }

            Debug.WriteLine(BitConverter.ToString(data.Data));

            // Initialize2
            bulk.Write(new byte[] { 0x00, 0x01, 0x86, 0xff, 0x01, 0x00, 0x00, 0x00, 0x00 }, 100);
            data = bulk.Read(100);
            if (data.Status != HidDeviceData.ReadStatus.Success)
            {
                return;
            }

            Debug.WriteLine(BitConverter.ToString(data.Data));

            // Clear garbage
            bulk.Write(new byte[] { 0x00, 0x01, 0x80, 0x33, 0x01, 0x00, 0x00, 0x00, 0x00 }, 100);
            data = bulk.Read(100);
            if (data.Status != HidDeviceData.ReadStatus.Success)
            {
                return;
            }

            Debug.WriteLine(BitConverter.ToString(data.Data));

            // Temperture
            bulk.Write(new byte[] { 0x00, 0x01, 0x80, 0x33, 0x01, 0x00, 0x00, 0x00, 0x00 }, 100);
            data = bulk.Read(100);
            if (data.Status != HidDeviceData.ReadStatus.Success)
            {
                return;
            }

            Debug.WriteLine(BitConverter.ToString(data.Data));

            var temperature = (((data.Data[4] & 0xFF) + (data.Data[3] << 8)) * (125.0 / 32000.0)) - 1.70;
            Console.WriteLine(temperature.ToString("0.00"));
        }
    }
}
