using System.ComponentModel.DataAnnotations;

namespace UnityBacnet.API.Models.DTOs
{
    public class SendReadingRequest
    {
        [Required]
        public int AssetId { get; set; }

        [Required]
        public string AssetType { get; set; }

        [Range(-50, 150)]
        public double Value { get; set; }

        [Required]
        public string ReadingType { get; set; }

        public bool HasAlarm { get; set; }
    }
}
