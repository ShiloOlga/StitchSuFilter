namespace Web.Models.CrossStitch
{
    public class PatternId
    {
        public int Id { get; set; }
        public string PatternLink { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
