using System;

namespace Jenx.AzureDevOps.Client.Models
{
    public class BuildTriggeredResult
    {
        public Links _links { get; set; }
        public Properties Properties { get; set; }
        public object[] Tags { get; set; }
        public object[] ValidationResults { get; set; }
        public Plan[] Plans { get; set; }
        public Triggerinfo TriggerInfo { get; set; }
        public int Id { get; set; }
        public string BuildNumber { get; set; }
        public string Status { get; set; }
        public DateTime QueueTime { get; set; }
        public string Url { get; set; }
        public Definition Definition { get; set; }
        public int BuildNumberRevision { get; set; }
        public Project Project { get; set; }
        public string Uri { get; set; }
        public string SourceBranch { get; set; }
        public Queue Queue { get; set; }
        public string Priority { get; set; }
        public string Reason { get; set; }
        public RequestedFor RequestedFor { get; set; }
        public RequestedBy RequestedBy { get; set; }
        public DateTime LastChangedDate { get; set; }
        public LastChangedBy LastChangedBy { get; set; }
        public OrchestrationPlan OchestrationPlan { get; set; }
        public Logs Logs { get; set; }
        public Repository Repository { get; set; }
        public bool KeepForever { get; set; }
        public bool RetainedByRelease { get; set; }
        public object TriggeredByBuild { get; set; }
    }
}