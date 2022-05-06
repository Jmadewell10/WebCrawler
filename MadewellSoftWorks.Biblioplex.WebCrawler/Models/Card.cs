namespace MadewellSoftWorks.Biblioplex.WebCrawler.Models
{
    public class Card
    {
        public int ID { get; set; }
        public string? CMC { get; set; }
        public string? Name { get; set; }
        public string? ImageURL { get; set; }
        public string? Type { get; set; }
        public string? SubType { get; set; }
        public string? Text { get; set; }
        public string? Artist { get; set; }
        public string? Set { get; set; }
        public string? SetSymbolURL { get; set; }
        public string? CollectorNumber { get; set; }

        public Card() { }

        public Card(int id, string name, string imageURL, string type, string subType, string text, string artist, string set, string setSymbol, string collectorNumber)
        {
            this.ID = id; 
            this.Name = name;
            this.ImageURL = imageURL;
            this.Type = type;
            this.SubType = subType;
            this.Text = text;
            this.Artist = artist;
            this.Set = set;
            this.SetSymbolURL = setSymbol;
            this.CollectorNumber = collectorNumber;
        }
    }
}
