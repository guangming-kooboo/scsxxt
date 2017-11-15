﻿using System;
using System.Linq;

namespace Qx.Tools
{
    public class UrlUtility
    {
        public static string AddQueryParameter(string url, string parameterName, string parameterValue)
        {
            string delimiter;
            if (url.EndsWith("?") || url.EndsWith("&"))
            {
                delimiter = String.Empty;
            }
            else if (!url.Contains("?"))
            {
                delimiter = "?";
            }
            else
            {
                delimiter = "&";
            }

            return url + delimiter + parameterName + "=" + parameterValue;
        }

        public static string Combine(params string[] paths)
        {
            return paths.Aggregate(
                (current, path) =>
                {
                    if (String.IsNullOrEmpty(path))
                    {
                        return current;
                    }

                    return String.Format("{0}/{1}", current.TrimEnd('/'), path.TrimStart('/'));
                });
        }
    }
}