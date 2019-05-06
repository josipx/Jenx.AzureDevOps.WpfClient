using System;

namespace Jenx.AzureDevOps.Client.Models
{
    public class Value
    {
        public _Links _links { get; set; }
        public string Quality { get; set; }
        public AuthoredBy AuthoredBy { get; set; }
        public object[] Drafts { get; set; }
        public Queue Queue { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Uri { get; set; }
        public string Path { get; set; }
        public string Type { get; set; }
        public string QueueStatus { get; set; }
        public int Revision { get; set; }
        public DateTime CreatedDate { get; set; }
        public Project Project { get; set; }
    }
}