using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LearningSystem.Web.Infrastructure.Extesions
{
    public static class StringExtensions
    {
        public static string ToFriendlyUrl(this string text)
         => Regex.Replace(text, @"[^A-Za-z0-9_\.~]+", "-").ToLower();
    }
}
