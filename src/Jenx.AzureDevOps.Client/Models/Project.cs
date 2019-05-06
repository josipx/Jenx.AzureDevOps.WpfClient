using System;

namespace Jenx.AzureDevOps.Client.Models
{
    public class Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string State { get; set; }
        public int Revision { get; set; }
        public string Visibility { get; set; }
        public DateTime LastUpdateTime { get; set; }
    }
}