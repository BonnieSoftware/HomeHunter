using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace CefSharp.MinimalExample.WinForms
{
    public class Shortlist
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> ExcludeTerms { get; set; } = new List<string>()
        {
            "leasehold",
            "lease",
            "apartment",
            "mobile home",
            "park home",
            "auction",
            "Over 50's Only",
            "Over 50s Only",
            "retirement",
            "Chalet",
            "Garages for sale",
            "houseshare",
            "house share",
            "flat share",
            "student flat",
            "house of multiple occupation",
            "student house",
            "beach hut",
            "Shepherd's Hut",
            "Shepherds Hut",
            "static caravan",
            "Shared ownership",
            "holiday park",
            "holiday home park",
            "Parkmove Ltd",
            "barge",
            "lodge",
            "maisonette",
            "mews",
            "flat for sale",
            "vessel",
            "caravan for sale",
            "Holiday Home"
        };
        public List<string> IncludeTerms { get; set; } = new List<string>();
        public List<string> ExcludePostcodes { get; set; } = new List<string>();
        public int MinimumPrice { get; set; } = -1;
        public int MaximumPrice { get; set; } = -1;
        public DateTimeRange AdvertisedOnOrAfter { get; set; } = DateTimeRange.Anytime;
        public DateTimeRange AdvertisedForOrOver { get; set; } = DateTimeRange.Anytime;
        public PropertyListingSortOrder SortOrder { get; set; } = PropertyListingSortOrder.LowestPrice;
        public string LocationToSortBy { get; set; } = string.Empty;
        public bool ExcludePriceOnApplication { get; set; } = true;
        public bool ExcludeOffersInvited { get; set; } = true;
        public bool ResetIndexAtStart { get; set; } = true; // Advanced feature hidden from user.
        public bool Rightmove42PageLimitFixEnabled { get; set; } = false; // Experimental feature hidden from user.
        public List<string> ExcludePropertyUrls { get; set; } = new List<string>();

        public static Shortlist Open(string id)
        {
            var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\HomeHunter\Shortlists\" + id);

            if (key == null)
            {
                throw new InvalidOperationException("Shortlist " + id + " does not exist.");
            }

            var shortlist = new Shortlist();
            shortlist.Id = id;
            shortlist.Name = key.GetValue("Name").ToString();
            shortlist.ExcludeTerms = WebInput.ToList(key.GetValue("ExcludeTerms"));
            shortlist.IncludeTerms = WebInput.ToList(key.GetValue("IncludeTerms"));
            shortlist.ExcludePostcodes = WebInput.ToList(key.GetValue("ExcludePostcodes"));
            shortlist.MinimumPrice = Convert.ToInt32(key.GetValue("MinimumPrice"));
            shortlist.MaximumPrice = Convert.ToInt32(key.GetValue("MaximumPrice"));
            shortlist.AdvertisedOnOrAfter = (DateTimeRange)Convert.ToInt32(key.GetValue("AdvertisedOnOrAfter"));
            shortlist.AdvertisedForOrOver = (DateTimeRange)Convert.ToInt32(key.GetValue("AdvertisedForOrOver"));
            shortlist.SortOrder = (PropertyListingSortOrder)Convert.ToInt32(key.GetValue("SortOrder"));
            shortlist.LocationToSortBy = key.GetValue("LocationToSortBy").ToString();
            shortlist.ExcludePriceOnApplication = Convert.ToBoolean(key.GetValue("ExcludePriceOnApplication"));
            shortlist.ExcludeOffersInvited = Convert.ToBoolean(key.GetValue("ExcludeOffersInvited"));
            shortlist.ResetIndexAtStart = Convert.ToBoolean(key.GetValue("ResetIndexAtStart"));
            shortlist.Rightmove42PageLimitFixEnabled = Convert.ToBoolean(key.GetValue("Rightmove42PageLimitFixEnabled"));
            shortlist.ExcludePropertyUrls = WebInput.ToList(key.GetValue("ExcludePropertyUrls"));

            return shortlist;
        }

        public void Save()
        {
            RegistryKey key;

            if (string.IsNullOrEmpty(Id))
            {
                Id = GenerateNewId();
                key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\HomeHunter\Shortlists\" + Id);
            }
            else
            {
                key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\HomeHunter\Shortlists\" + Id, true);
            }

            key.SetValue("Name", Name);
            key.SetValue("ExcludeTerms", WebInput.ToRegistryString(ExcludeTerms));
            key.SetValue("IncludeTerms", WebInput.ToRegistryString(IncludeTerms));
            key.SetValue("ExcludePostcodes", WebInput.ToRegistryString(ExcludePostcodes));
            key.SetValue("MinimumPrice", MinimumPrice.ToString()); // Stored as string due to the -1 value.
            key.SetValue("MaximumPrice", MaximumPrice.ToString()); // Stored as string due to the -1 value.
            key.SetValue("AdvertisedOnOrAfter", (int)AdvertisedOnOrAfter);
            key.SetValue("AdvertisedForOrOver", (int)AdvertisedForOrOver);
            key.SetValue("SortOrder", (int)SortOrder);
            key.SetValue("LocationToSortBy", LocationToSortBy);
            key.SetValue("ExcludePriceOnApplication", ExcludePriceOnApplication);
            key.SetValue("ExcludeOffersInvited", ExcludeOffersInvited);
            key.SetValue("ResetIndexAtStart", ResetIndexAtStart);
            key.SetValue("Rightmove42PageLimitFixEnabled", Rightmove42PageLimitFixEnabled);
            key.SetValue("ExcludePropertyUrls", ExcludePropertyUrls);
        }

        private static string GenerateNewId()
        {
            var bytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }

            return BitConverter.ToString(bytes);
        }
    }
}
