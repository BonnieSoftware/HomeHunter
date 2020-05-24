using System;

namespace CefSharp.MinimalExample.WinForms
{
    public class Postcode
    {
        public string FirstPart { get; private set; }
        public string LastPart { get; private set; }

        public static Postcode Parse(string postcode)
        {
            // This method is not so interested in the validity of the postcode,
            // it is more so here to split the postcode into the start and end
            // part for matching purposes.

            postcode = postcode.Trim().Replace(" ", "");

            if (postcode.Length < 2 || postcode.Length > 7)
                throw new ArgumentException("Postcode is not valid");

            var parsedPostcode = new Postcode();

            // First part only, AN ie M1
            if (postcode.Length == 2)
            {
                parsedPostcode.FirstPart = postcode;
                return parsedPostcode;
            }

            parsedPostcode.FirstPart = postcode.Substring(0, postcode.Length - 3);
            if (parsedPostcode.FirstPart.Length < 2)
            {
                parsedPostcode.FirstPart = postcode;
            }
            else
            {
                parsedPostcode.LastPart = postcode.Substring(parsedPostcode.FirstPart.Length);
            }

            if (parsedPostcode.LastPart != null && parsedPostcode.LastPart.Length != 3)
            {
                throw new ArgumentException("Postcode is not valid");
            }

            return parsedPostcode;
        }

        public bool IsMatch(Postcode postcode)
        {
            if (postcode.FirstPart == FirstPart && postcode.LastPart == LastPart) return true;
            return false;
        }

        public bool IsPartcialMatch(Postcode postcode)
        {
            if (postcode.FirstPart == FirstPart) return true;
            return false;
        }
    }
}
