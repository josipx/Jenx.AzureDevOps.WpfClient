namespace Jenx.AzureDevOps.Client.Models
{
    public class Queue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Pool Pool { get; set; }
    }
}