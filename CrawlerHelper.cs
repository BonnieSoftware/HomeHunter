using System;
using System.Text.RegularExpressions;
using System.Web;

namespace CefSharp.MinimalExample.WinForms
{
    public static class CrawlerHelper
    {
        public static bool CaseInsensitiveContains(string text, string contains)
        {
            return text?.IndexOf(contains, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }

        public static bool IsDigitsOnly(string text)
        {
            foreach (char character in text)
            {
                if (character < '0' || character > '9')
                    return false;
            }

            return true;
        }

        public static bool BrowserAddressIsAPropertyDetailsPage(string url)
        {
            // Matches on URL's ending similar to "/property-90162500.html"
            // ie /commercial-property-for-sale/property-76430246.html 
            // ie /property-for-sale/property-90388769.html
            Match match = Regex.Match(url, @"\/property\-\d+\.html$");
            if (match != Match.Empty)
            {
                return true;
            }
            return false;
        }

        public static bool BrowserAddressIsAPropertySearchListingsPage(string url)
        {
            if (url.StartsWith("https://www.rightmove.co.uk/property-for-sale/find.html"))
                return true;

            if (url.StartsWith("https://www.rightmove.co.uk/property-to-rent/find.html"))
                return true;

            if (url.StartsWith("https://www.rightmove.co.uk/commercial-property-for-sale/search.html"))
                return true;

            if (url.StartsWith("https://www.rightmove.co.uk/commercial-property-to-let/search.html"))
                return true;

            return false;
        }

        public static string SetPageIndex(string currentUrl, string requiredPageIndex)
        {
            var newUrl = currentUrl;

            var queryStringInformation = new Uri(newUrl).Query;
            var queryStringParts = HttpUtility.ParseQueryString(queryStringInformation);

            if (queryStringParts.Count < 1)
            {
                // This method only works with URL's that already have a querystring.
                throw new ArgumentException(currentUrl);
            }

            var currentPageIndex = queryStringParts.Get("index");
            if (currentPageIndex == null) // index=X not in URL (page 1)
            {
                if (requiredPageIndex != "0")
                {
                    // Only add in the index if on page 2+.
                    // Add index=X to query string.
                    newUrl = currentUrl.Replace("?", "?index=" + requiredPageIndex + "&");
                }
                // Nothing to do for page 1 as the index is not in the URL.
            }
            else if (currentPageIndex == "")
            {
                if (requiredPageIndex != "0")
                {
                    // Only add in the index if on page 2+.
                    // Change index= to index=X.
                    newUrl = currentUrl.Replace("index=", "index=" + requiredPageIndex);
                }
                else
                {
                    // Remove index= as it isn't needed for page 1.
                    newUrl = currentUrl.Replace("?index=", "");
                    newUrl = newUrl.Replace("&index=", "");
                }
            }
            else if (requiredPageIndex != currentPageIndex)
            {
                if (requiredPageIndex != "0")
                {
                    // Replace index=Y with index=X
                    newUrl = currentUrl.Replace("index=" + currentPageIndex, "index=" + requiredPageIndex);
                }
                else
                {
                    // Remove index= as it isn't needed for page 1.
                    newUrl = currentUrl.Replace("?index=" + currentPageIndex, "");
                    newUrl = newUrl.Replace("&index=" + currentPageIndex, "");
                }
            }

            return newUrl;
        }
    }
}
