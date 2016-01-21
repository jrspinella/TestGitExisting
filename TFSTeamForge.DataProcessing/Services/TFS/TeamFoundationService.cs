using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi.Patch;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TFSTeamForge.DataProcessing.Models.TeamSystem;
using TFSTeamForge.DataProcessing.Models.TeamSystem.TeamFoundation;

namespace TFSTeamForge.DataProcessing.Services.TFS
{
    public class TeamFoundationService : ITeamFoundationService
    {
        //private static string ApiVersion1 = "1.0";
        //private static string JsonPatch = "application/json-patch+json";

        //private const string GetTeamProjectsUri = "{0}/{1}/_apis/projects?api-version={2}";
        //private const string GetWorkItemsUri = "{0}/{1}/_apis/wit/workitems?ids={2}&api-version={3}";
        //private const string UpdateWorkItemUri = "{0}/{1}/_apis/wit/workitems/{2}?api-version={3}";
        private const string CollectionUri = "{0}/{1}/";

        protected TeamFoundationConnection Connection { get; private set; }

        public void SetConnection(TeamFoundationConnection connection)
        {
            Connection = connection;
            EnsureConnection();
        }

        public async Task<TFSWorkItem> GetWorkItemAsync(int id)
        {
            EnsureConnection();
            TFSWorkItem item = null;
            try
            {
                var witClient = CreateWorkItemHttpClient();
                var workItem = await witClient.GetWorkItemAsync(id);
                item = workItem.ConvertToTFSWorkItem();
                //using (var client = CreateHttpRequest())
                //{
                //    var url = string.Format(GetWorkItemsUri, Connection.ServerUrl, Connection.Collection, idValue, ApiVersion1);
                //    using (var response = await client.GetAsync(url))
                //    {
                //        response.EnsureSuccessStatusCode();
                //        using (var content = response.Content)
                //        {
                //            var queryResult = await content.ReadAsStringAsync();
                //            item = JsonConvert.DeserializeObject<TFSWorkItem>(queryResult);
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw ex;
            }
            return item;
        }

        public async Task<TFSWorkItemCollection> GetWorkItemsAsync(params int[] ids)
        {
            EnsureConnection();
            if (ids == null || ids.Length == 0)
            {
                throw new ArgumentNullException("ids");
            }
            var results = new TFSWorkItemCollection();
            //var idList = new StringBuilder();
            //var firstValue = true;
            //for (int idCount = 0; idCount < ids.Length; idCount++)
            //{
            //    if (!firstValue)
            //    {
            //        idList.Append(",");
            //    }
            //    idList.Append(ids[idCount]);
            //    firstValue = false;
            //}
            try
            {
                var witClient = CreateWorkItemHttpClient();
                var workItems = await witClient.GetWorkItemsAsync(ids);
                foreach (var wi in workItems)
                {
                    var item = wi.ConvertToTFSWorkItem();
                    results.WorkItems.Add(item);
                }
                results.Count = workItems.Count;
                
                //using (var client = CreateHttpRequest())
                //{
                //    var url = string.Format(GetWorkItemsUri, Connection.ServerUrl, Connection.Collection, idList.ToString(), ApiVersion1);
                //    using (var response = await client.GetAsync(url))
                //    {
                //        response.EnsureSuccessStatusCode();
                //        using (var content = response.Content)
                //        {
                //            var queryResult = await content.ReadAsStringAsync();
                //            results = JsonConvert.DeserializeObject<TFSWorkItemCollection>(queryResult);
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw ex;
            }
            return results;
        }

        public async Task<Models.TeamSystem.TeamFoundation.TeamProjectCollection> GetTeamProjectsAsync()
        {
            EnsureConnection();
            var collection = new Models.TeamSystem.TeamFoundation.TeamProjectCollection();

            try
            {
                var projectClient = CreateTeamProjectHttpClient();
                var projects = await projectClient.GetProjects(Microsoft.TeamFoundation.Common.ProjectState.WellFormed);
                if (projects == null)
                {
                    return collection;
                }
                var projectsList = projects.ToList();
                foreach (var project in projectsList)
                {
                    collection.Projects.Add(new Models.TeamSystem.TeamFoundation.TeamProject()
                    {
                        Id = project.Id,
                        Name = project.Name,
                        Url = project.Url,
                        Description = project.Description,
                        State = project.State.ToString()
                    });
                }
                collection.Count = projectsList.Count;

                //using (var client = CreateHttpRequest())
                //{
                //    var url = string.Format(GetTeamProjectsUri, Connection.ServerUrl, Connection.Collection, ApiVersion1);
                //    using (var response = await client.GetAsync(url))
                //    {
                //        response.EnsureSuccessStatusCode();
                //        using (var content = response.Content)
                //        {
                //            var queryResult = await content.ReadAsStringAsync();

                //            collection = JsonConvert.DeserializeObject<TeamProjectCollection>(queryResult);
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw ex;
            }

            return collection;
        }

        public Task<bool> AddTeamForgeArtifactIdAsync(int workItemId, string artifactId)
        {
            EnsureConnection();
            if (string.IsNullOrEmpty(artifactId))
            {
                throw new ArgumentNullException("artifactId");
            }
            try
            {
                return UpdateWorkItemWithClientObjectModel(workItemId, artifactId);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                throw ex;
            }
                //try
                //{
                //    var witClient = CreateWorkItemHttpClient();
                //    var workItem = witClient.GetWorkItemAsync(idValue).Result;

                //    if (workItem != null)
                //    {
                //        JsonPatchOperation patch = null;
                //        if (workItem.Fields.ContainsKey("System.Title"))
                //        {
                //            var currentTitle = workItem.Fields["System.Title"];
                //            patch = new JsonPatchOperation()
                //            {
                //                Operation = Operation.Replace,
                //                Path = "/fields/System.Title",
                //                Value = currentTitle + " " + artifactId
                //            };
                //        }
                //        else
                //        {
                //            patch = new JsonPatchOperation()
                //            {
                //                Operation = Operation.Add,
                //                Path = "/fields/System.Title",
                //                Value = artifactId
                //            };
                //        }
                //        var patchDocument = new JsonPatchDocument();
                //        patchDocument.Add(patch);
                //        var newWorkItem = witClient.UpdateWorkItemAsync(patchDocument, idValue).Result;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    System.Diagnostics.Debug.WriteLine(ex.ToString());
                //}
            //}
            //else
            //{
            //    throw new ArgumentException("workItemId is not valid");
            //}
            //return Task.FromResult(result);
        }

        private Task<bool> UpdateWorkItemWithClientObjectModel(int workItemId, string artifactId)
        {
            var result = false;
            try
            {
                var collectionUri = new Uri(string.Format(CollectionUri, Connection.ServerUrl, Connection.Collection));
                Microsoft.TeamFoundation.Client.TfsTeamProjectCollection teamProjectCollection =
                   new Microsoft.TeamFoundation.Client.TfsTeamProjectCollection(collectionUri, new NetworkCredential(Connection.UserName, Connection.Password));

                Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore workItemStore = teamProjectCollection.GetService<Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItemStore>();

                Microsoft.TeamFoundation.WorkItemTracking.Client.WorkItem workItem = workItemStore.GetWorkItem(workItemId);

                if (workItem.Fields.Contains("Artifact ID"))
                {
                    workItem.Fields["Artifact ID"].Value = artifactId;
                    workItem.Save();
                }
                result = true;
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception.Message);
            }
            return Task.FromResult(result);
        }

        private void EnsureConnection()
        {
            if (Connection == null)
            {
                throw new ArgumentException("Connection cannot be empty");
            }
            if (string.IsNullOrWhiteSpace(Connection.Collection))
            {
                throw new ArgumentNullException("Connection.Collection");
            }
            if (string.IsNullOrWhiteSpace(Connection.Password))
            {
                throw new ArgumentNullException("Connection.Password");
            }
            if (string.IsNullOrWhiteSpace(Connection.ServerUrl))
            {
                throw new ArgumentNullException("Connection.ServerUrl");
            }
            if (string.IsNullOrWhiteSpace(Connection.UserName))
            {
                throw new ArgumentNullException("Connection.UserName");
            }
        }

        private WorkItemTrackingHttpClient CreateWorkItemHttpClient()
        {
            var uri = string.Format(CollectionUri, Connection.ServerUrl, Connection.Collection);
            WorkItemTrackingHttpClient witClient = null;
            if (Regex.IsMatch(uri, "visualstudio.com"))
            {
                witClient = new WorkItemTrackingHttpClient(new Uri(uri), new VssBasicCredential(Connection.UserName, Connection.Password));
            }
            else
            {
                witClient = new WorkItemTrackingHttpClient(new Uri(uri), new VssCredentials(new WindowsCredential(new NetworkCredential(Connection.UserName, Connection.Password))));
            }
            return witClient;
        }

        private ProjectHttpClient CreateTeamProjectHttpClient()
        {
            var uri = string.Format(CollectionUri, Connection.ServerUrl, Connection.Collection);
            ProjectHttpClient projClient = null;
            if (Regex.IsMatch(uri, "visualstudio.com"))
            {
                projClient = new ProjectHttpClient(new Uri(uri), new VssBasicCredential(Connection.UserName, Connection.Password));
            }
            else
            {
                projClient = new ProjectHttpClient(new Uri(uri), new VssCredentials(new WindowsCredential(new NetworkCredential(Connection.UserName, Connection.Password))));
            }
            return projClient;
        }

        //private HttpClient CreateHttpRequest()
        //{
        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(
        //        ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", Connection.UserName, Connection.Password))));
        //    return client;
        //}

        //private string RetrieveFieldValue(string key, JObject fields)
        //{
        //    var token = (from p in fields.Properties()
        //                 where string.Compare(key, p.Name, true) == 0
        //                 select fields[p.Name]).FirstOrDefault();

        //    var result = string.Empty;

        //    if (token != null)
        //    {
        //        result = token.Value<string>();
        //    }

        //    return result;
        //}
    }
}
