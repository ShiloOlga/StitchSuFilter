namespace Web.Models.CrossStitch.Kit
{
    public class KitId
    {
        public string Id { get; set; }
        public string Link { get; set; }

        public override string ToString()
        {
            return Id;
        }
    }
}
