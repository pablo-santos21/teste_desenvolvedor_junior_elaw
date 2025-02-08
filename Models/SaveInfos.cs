namespace Webcrawler.Models
{
    public class SaveInfos
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TotalPages { get; set; } = 1;
        public int TotalRowsExtracted { get; set; }
        public string JsonFileName { get; set; } = string.Empty;
    }
}
