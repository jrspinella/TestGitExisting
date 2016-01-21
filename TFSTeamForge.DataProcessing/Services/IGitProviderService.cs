using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSTeamForge.DataProcessing.Services
{
    public interface IGitProviderService
    {
        string SourceUserName { get; set; }
        string SourcePassword { get; set; }
        string RemoteUserName { get; set; }
        string RemotePassword { get; set; }
        string SignatureUserName { get; set; }
        string SignatureUserEmail { get; set; }
        string DefaultRemoteName { get; set; }
        string CommitLogFile { get; set; }

        string PullFromRepository(string repository, string branch);
        bool PushToRepository(string localRepository, string remoteRepository, string remoteBranch);
    }
}
