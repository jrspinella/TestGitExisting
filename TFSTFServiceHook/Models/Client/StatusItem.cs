using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFSTFServiceHook.Models.Client
{
    public class StatusItem
    {
        public UInt64 Id { get; set; }
        public string EventTitle { get; set; }
        public string EventDescription { get; set; }
        public bool WasSuccessful { get; set; }
        public DateTime Date { get; set; }
    }
}
