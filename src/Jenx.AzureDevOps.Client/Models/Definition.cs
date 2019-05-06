namespace Jenx.AzureDevOps.Client.Models
{
    public class Definition
    {
        public object[] Drafts { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Uri { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public string QueueStatus { get; set; }
        public int Revision { get; set; }
        public Project Project { get; set; }
    }
}