namespace Jenx.AzureDevOps.Client.Models
{
    public class Repository
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public object Clean { get; set; }
        public bool CheckoutSubmodules { get; set; }
    }
}