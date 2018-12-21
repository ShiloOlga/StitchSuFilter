using System.Text;

namespace Web.Domain
{
    public class Fabric
    {

        public FabricType Type { get; }
        public int Count { get; }
        public string Name { get; }
        public int Priority { get; }
        public string Content { get; }

        public Fabric(string name, int count, FabricType type, int priority, string content)
        {
            Type = type;
            Count = count;
            Name = name;
            Priority = priority;
            Content = content;
        }

        public override string ToString()
        {
            return Name;
        }

        public string ToDescriptionString()
        {
            var sb = new StringBuilder();
            sb.Append($"{Name}, {Type.ToString().ToLowerInvariant()} count {Count}");
            if (!string.IsNullOrEmpty(Content))
            {
                sb.Append($", {Content}");
            }

            return sb.ToString();
        }
    }
}