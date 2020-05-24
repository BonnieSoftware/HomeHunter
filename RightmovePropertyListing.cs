using System;
using System.Collections.Generic;

namespace CefSharp.MinimalExample.WinForms
{
    public class RightmovePropertyListing
    {
        public string Html { get; set; }
        public string Link { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string PriceQualifier { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        public List<string> Images { get; set; } = new List<string>();
        public DateTime DateAdded { get; set; }
    }
}
