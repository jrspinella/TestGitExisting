using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibGit2Sharp;
using System.IO;
using LibGit2Sharp.Handlers;

namespace TFSTeamForge.DataProcessing.Services
{
    public class GitProviderService : IGitProviderService
    {
        private static string LocalBaseRepositoryPath = @"C:\Repositories\TFSTF\";

        public GitProviderService()
        {
            CommitLogFile = "TFS-TF Sync Log.txt";
        }

        public string SourceUserName { get; set; }
        public string SourcePassword { get; set; }
        public string RemoteUserName { get; set; }
        public string RemotePassword { get; set; }
        public string SignatureUserName { get; set; }
        public string SignatureUserEmail { get; set; }
        public string DefaultRemoteName { get; set; }
        public string CommitLogFile { get; set; }

        public string PullFromRepository(string repository, string branch)
        {
            var localPath = GetLocalDirectoryPath(repository);
            var repoPath = string.Empty;

            if (Directory.Exists(localPath))
            {
                var directories = Directory.GetDirectories(localPath);
                var desiredPath = Path.Combine(localPath, ".git");
                var localGitPath = Directory.GetDirectories(localPath).FirstOrDefault(dir => string.Compare(dir, desiredPath, true) == 0);
                if (Directory.Exists(localGitPath))
                {
                    repoPath = PullLatest(repository, branch, localGitPath);
                }
                else
                {
                    throw new DirectoryNotFoundException("Local path is not a valid path: " + localGitPath);
                }
            }
            else
            {
                repoPath = CloneRepository(repository, branch, localPath);
            }
            return repoPath;
        }

        public bool PushToRepository(string localRepository, string remoteRepository, string remoteBranch)
        {
            var result = false;
            if (!Directory.Exists(localRepository))
            {
                throw new ArgumentException("Local repository does not exist");
            }

            try
            {
                using (var repo = new Repository(localRepository))
                {
                    var localRemotes = GetRemotes(repo);
                    var specificRemote = localRemotes.FirstOrDefault(r => string.Compare(r.Name, DefaultRemoteName, true) == 0);
                    Remote targetRemote = null;
                    if (specificRemote != null)
                    {
                        if (string.Compare(specificRemote.Url, remoteRepository, true) != 0)
                        {
                            throw new ArgumentException("Specified remote does not match current remote");
                        }
                        else
                        {
                            targetRemote = specificRemote;
                        }
                    }
                    else
                    {
                        targetRemote = repo.Network.Remotes.Add(DefaultRemoteName, remoteRepository);
                    }
                    //UpdateLogFile(repo.Info.WorkingDirectory);
                    //repo.Stage(CommitLogFile);
                    //repo.Commit("[artf6448]", GetSignature(), GetSignature());

                    //PushOptions sourcePushOptions = new PushOptions()
                    //{
                        //CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials
                        //{
                        //    Username = SourceUserName,
                        //    Password = SourcePassword,
                        //}
                    //    CredentialsProvider = (_url, _user, _cred) => new DefaultCredentials()
                    //};
                    PushOptions remotePushOptions = new PushOptions()
                    {
                        CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials
                        {
                            Username = RemoteUserName,
                            Password = RemotePassword,
                        },
                        OnPushTransferProgress = this.PushTransferProgressHandler,
                        OnPushStatusError = this.PushStatusErrorHandler,
                    };

                    //var sourceBranch = repo.Branches[remoteBranch];
                    //if (sourceBranch != null)
                    //{
                    //    repo.Network.Push(sourceBranch, sourcePushOptions);
                    //}
                    repo.Network.Push(targetRemote, "HEAD", @"refs/heads/master", remotePushOptions);
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        private bool PushTransferProgressHandler(int current, int total, long bytes)
        {
            System.Diagnostics.Debug.WriteLine(string.Format("transfer => Current: {0}, Total: {1}, Bytes: {2}", current, total, bytes));
            return true;
        }

        private void PushStatusErrorHandler(PushStatusError error)
        {
            System.Diagnostics.Debug.WriteLine("error => " + error.Message);
        }


        private string PullLatest(string repository, string branch, string localGitPath)
        {
            using (var repo = new Repository(localGitPath))
            {
                var targetBranch = repo.Branches[branch];
                if (targetBranch != null)
                {
                    if (!targetBranch.IsCurrentRepositoryHead)
                    {
                        repo.Checkout(branch);
                    }
                    try
                    {
                        var pullOptions = new PullOptions();
                        pullOptions.FetchOptions = new FetchOptions();
                        pullOptions.FetchOptions.CredentialsProvider = (_url, _user, _cred) => new DefaultCredentials();
                        repo.Network.Pull(GetSignature(), pullOptions);
                    }
                    catch (Exception)
                    {
                        var pullOptions = new PullOptions();
                        pullOptions.FetchOptions = new FetchOptions();
                        pullOptions.FetchOptions.CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials()
                        {
                            Username = SourceUserName,
                            Password = SourcePassword
                        };
                        repo.Network.Pull(GetSignature(), pullOptions);
                    }
                }
            }
            return localGitPath;
        }

        private void UpdateLogFile(string workingDirectory)
        {
            var logFilePath = Path.Combine(workingDirectory, CommitLogFile);
            using (var stream = new StreamWriter(logFilePath, true))
            {
                var time = DateTime.Now;
                stream.WriteLine(string.Format("Performing sync at " + time.ToLongDateString() + " "  + time.ToLongTimeString()));
            }
        }

        private List<Remote> GetRemotes(Repository repository)
        {
            var remotes = new List<Remote>();
            remotes = repository.Network.Remotes.ToList();
            return remotes;
        }

        private CredentialsHandler GetSourceCredentialsHandler()
        {
            var credentials = new UsernamePasswordCredentials()
            {
                Username = SourceUserName,
                Password = SourcePassword
            };
            CredentialsHandler credentialHandler = (_url, _user, _cred) => credentials;
            return credentialHandler;
        }

        private string CloneRepository(string repository, string branch, string localPath)
        {
            var clonedPath = string.Empty;
            try
            {
                clonedPath = Repository.Clone(repository, localPath, new CloneOptions()
                {
                    BranchName = branch,
                    CredentialsProvider = (_url, _user, _cred) => new DefaultCredentials(),
                    Checkout = true
                });
            }
            catch (Exception)
            {
                clonedPath = Repository.Clone(repository, localPath, new CloneOptions()
                {
                    BranchName = branch,
                    CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials()
                    {
                        Username = SourceUserName,
                        Password = SourcePassword
                    },
                    Checkout = true
                });
            }
            
            return clonedPath;
        }

        private Signature GetSignature()
        {
            return new Signature(SignatureUserName, SignatureUserEmail, DateTime.Now.ConvertToESTDateTimeOffset());
        }

        private string GetLocalDirectoryPath(string repository)
        {
            var localPath = LocalBaseRepositoryPath;

            var urlParts = repository.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            var repoName = urlParts.Last();
            var dotIndex = repoName.IndexOf('.');
            if (dotIndex > 0)
            {
                repoName = repoName.Substring(0, dotIndex - 1);
            }

            return Path.Combine(LocalBaseRepositoryPath, repoName);
        }
    }
}