using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace TFSTeamForge.DataProcessing
{
    public static class StringExtensions
    {
        private static string EmailRegex = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";

        public static string MatchEmail(this string source)
        {
            var email = string.Empty;
            var match = Regex.Match(source, EmailRegex);
            if (match.Success)
            {
                email = match.Value;
            }
            return email;
        }
    }
}