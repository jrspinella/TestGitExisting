using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TFSTeamForge.DataProcessing.Models.TeamSystem.TeamFoundation;

namespace TFSTeamForge.DataProcessing
{
    public static class TFSExtensions
    {
        public static TFSWorkItem ConvertToTFSWorkItem(this WorkItem workItem)
        {
            if (workItem == null)
            {
                return null;
            }
            var tfsItem = new TFSWorkItem()
            {
                Id = workItem.Id,
                Revision = workItem.Rev,
                Fields = workItem.Fields
            };
            return tfsItem;
        }
    }
}