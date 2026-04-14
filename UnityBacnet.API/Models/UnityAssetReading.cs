namespace UnityBacnet.API.Models
{
    public class UnityAssetReading
    {
        public int AssetId { get; set; }
        public string AssetType { get; set; }
        public double Value { get; set; }
        public string ReadingType { get; set; }
        public bool HasAlarm { get; set; }
    }
}
