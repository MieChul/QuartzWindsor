using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuartzWithDependancy.Models
{
    public class JobsDetailViewModel
    {
        public string JobGroup { get; set; }
        public string JobName { get; set; }
        public string JobDescription{ get; set; }
        public string TriggerName { get; set; }
        public string TriggerGroup { get; set; }
        public string TriggerTypeName { get; set; }
        public string TriggerState { get; set; }
        public string NextFireTime { get; set; }
        public string PreviousFireTime { get; set; }
    }
}