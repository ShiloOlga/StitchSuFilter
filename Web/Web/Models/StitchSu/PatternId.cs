namespace Web.Models.StitchSu
{
    public class PatternId
    {
        public string Id { get; set; }
        public string PatternLink { get; set; }

        public override string ToString()
        {
            return Id;
        }
    }
}
