using System.IO;
using System.Text.Json;

namespace ECRWlanDemo
{
    public class PairedDataSave
    {
        private static readonly string pairedDataFilePath = "ip.address.file";

        public void SavePairedData(DeviceData data)
        {
            if (null == data)
            {
                File.Delete(pairedDataFilePath);
            }
            else { 
            string json = JsonSerializer.Serialize(data);
            File.WriteAllText(pairedDataFilePath, json);
        }
        }

        public DeviceData LoadPairedData()
        {
            if (File.Exists(pairedDataFilePath))
            {
                string json = File.ReadAllText(pairedDataFilePath);
                return JsonSerializer.Deserialize<DeviceData>(json);
            }
            return null;
        }
    }
}
