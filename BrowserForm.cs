using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    public partial class BrowserForm : Form
    {
        private bool _initialisedAddress = false;
        private string _blankPageAddress = "about:blank";

        // Shortlist settings.
        private Shortlist _shortlist;

        private bool _crawling = false;
        private int _crawlerErrors = 0;
        private List<string> _pagination = new List<string>();
        private string _propertySearchListingsPage;

        private string _currentHtml;
        private HtmlParser _parser = new HtmlParser();
        private IHtmlDocument _document = null;

        private List<RightmovePropertyListing> _filteredPropertyListings;
        private List<RightmovePropertyListing> _shortlistedProperties = new List<RightmovePropertyListing>();
        private Dictionary<string, string> _removedProperties = new Dictionary<string, string>();

        private List<string> _urlLog = new List<string>();

        private void OnBrowserFrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (_browser.Address == _blankPageAddress)
            {
                return;
            }

            if (_crawlerErrors > 6)
            {
                _crawlerErrors = 0;
                _crawling = false;
                MessageBox.Show("Too many crawl errors, turn on debugging and review.", "Crawl failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GenerateReport();
            }

            if (e.Frame.IsMain)
            {
                _urlLog.Add(_browser.Address);

                //_browser.ViewSource();
                _browser.GetSourceAsync().ContinueWith(taskHtml =>
                {
                    _currentHtml = taskHtml.Result;

                    // Sometimes Rightmove returns an empty response when the crawler is running.
                    if (string.IsNullOrEmpty(_currentHtml))
                    {
                        _crawlerErrors++;
                        _browser.Reload(true);
                    }

                    _document = _parser.ParseDocument(_currentHtml);

                    if (OptionsForm.DebugingEnabled)
                    {
                        try
                        {
                            File.WriteAllText(Path.Combine(OptionsForm.DataDirectory, "debug-response-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".html"), _browser.Address + Environment.NewLine + _currentHtml);
                        }
                        catch (IOException)
                        {
                            // Sometimes the writing of the file can fail but this should not cause the app to
                            // stop working.
                        }
                    }

                    if (_crawling)
                    {
                        Crawl(_shortlist);
                    }
                });
            }
        }

        private bool IsCrawlable()
        {
            if (CrawlerHelper.BrowserAddressIsAPropertySearchListingsPage(_browser.Address))
            {
                return true;
            }

            // If not crawling there is no point in saying the page
            // can be crawled because this would result in one property
            // being shortlisted.
            if (CrawlerHelper.BrowserAddressIsAPropertyDetailsPage(_browser.Address) && _crawling == true)
            {
                return true;
            }

            return false;
        }

        private void Crawl(Shortlist shortlist)
        {
            if (!IsCrawlable())
            {
                if (_crawling)
                {
                    File.WriteAllText(Path.Combine(OptionsForm.DataDirectory, "not-crawlable-response-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".html"), _browser.Address + Environment.NewLine + _currentHtml);
                    _crawling = false;
                }
                throw new UnexpectedPageFormatException();
            }

            if (!_crawling)
            {
                _crawling = true;
                _shortlist = shortlist;
                _shortlistedProperties.Clear();
                _removedProperties.Clear();
                _urlLog.Clear();

                var newUrl = InitialisePagination();
                newUrl = InitialisePriceRangeFilter(newUrl);
                newUrl = InitialiseBedroomsFilter(newUrl);

                if (!string.IsNullOrEmpty(newUrl))
                {
                    LoadUrl(newUrl);
                    return;
                }

                //TODO: We should update the URL to take into account the price range in the filter.
                //TODO: We should update the URL to take into account the bedrooms in the filter.
            }

            ContinueCrawl();
        }

        private string InitialisePagination()
        {
            _pagination.Clear();

            var paginationElement = _document.QuerySelectorAll(".pagination-dropdown option");
            foreach (IElement paginationOption in paginationElement)
            {
                _pagination.Add(paginationOption.GetAttribute("value"));
            }

            var numberOfPages = _pagination.Count();
            if (numberOfPages > 42)
            {
                throw new UnexpectedPageCountException();
            }
            else if (numberOfPages == 42)
            {
                //TODO: If we hit the 42 page max and the overcome limit
                // is picked then enforce it, if the 42 page limit is
                // not enabled, warn the user that not all available
                // properties will be displayed.
                //REVIEW: It might even be better to just try and work
                // around the limitation anyway, and just warn the user
                // when the workaround is running.
                MessageBox.Show("Rightmove page limit will reduce the number of properties found. Limit your criteria to work around the issue. A future fix will be implemented to resolve this issue.", "Rightmove page limit being enforced!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (_shortlist.ResetIndexAtStart)
            {
                // Check to ensure we are on page 1 to avoid missing
                // properties.

                var currentUrl = _browser.Address;
                var newUrl = CrawlerHelper.SetPageIndex(currentUrl, _pagination.First());

                if (newUrl != currentUrl)
                {
                    return newUrl;
                }
            }

            //REVIEW: Do we still need this?
            //if (OptionsForm.DebugingEnabled)
            //{
            //    // When debugging, and the sort order has been changed, ensure we crawl
            //    // the properties in the order as they appear in front of us to aid
            //    // debugging.
            //    _browser.Reload(true);
            //    return true;
            //}

            return null;
        }

        private string InitialisePriceRangeFilter(string newUrl)
        {
            //TODO: modify the URL to filter by price to reduce the number of listings
            // for faster crawling.
            return newUrl;
        }

        private string InitialiseBedroomsFilter(string newUrl)
        {
            //TODO: modify the URL to filter by price to reduce the number of listings
            // for faster crawling.
            return newUrl;
        }

        private void ContinueCrawl()
        {
            if (CrawlerHelper.BrowserAddressIsAPropertySearchListingsPage(_browser.Address))
            {
                // Should not get here, this is just a guard.
                if (_pagination.Count() < 1)
                    throw new InvalidOperationException("Crawling has finished.");

                // Should not get here, this is just a guard.
                if (!_browser.Address.Contains("index=") && _shortlistedProperties.Count > 0)
                    throw new InvalidOperationException("Already crawled the first page.");

                // We need to keep track of the property search listings page we are on so we
                // know which page to move to next.
                _propertySearchListingsPage = _browser.Address;

                var property = FindPropertyListings(_currentHtml);

                // Sometimes the property listings don't load quick enough so
                // we need to refresh the page to get them.
                if (property == null) return;

                _filteredPropertyListings = FilterPropertyListings(property);
            }
            else if (CrawlerHelper.BrowserAddressIsAPropertyDetailsPage(_browser.Address))
            {
                var completePropertyListing = CrawlPropertyDetailsPage();
                if (PropertyMatchesShortlist(completePropertyListing))
                {
                    _shortlistedProperties.Add(completePropertyListing);
                }
            }

            // Move to next property.
            if (_filteredPropertyListings.Count() > 0)
            {
                // Take the next property from the list and crawl it.
                var propertyListing = _filteredPropertyListings.First();
                _filteredPropertyListings.RemoveAt(0);
                LoadUrl(propertyListing.Link);
            }
            else
            {
                // Once we have checked all the property details pages
                // that we are interested in, move to the next search
                // results page.
                var loadingNextSearchResultsPage = MoveToNextSearchResultsPage();
                if (!loadingNextSearchResultsPage)
                {
                    _crawling = false;
                    GenerateReport();
                }
                // There is no else as the page will be reloaded.
            }
        }

        private RightmovePropertyListing CrawlPropertyDetailsPage()
        {
            //TODO: Validate the current HTML and throw an exception
            // if the elements we expect to find are not present
            // in the page.

            var rightmovePropertyListing = new RightmovePropertyListing();

            rightmovePropertyListing.Html = _currentHtml;
            rightmovePropertyListing.Link = _browser.Address.Trim();

            rightmovePropertyListing.Title = _document.QuerySelector("h1").Text();

            rightmovePropertyListing.Description = _document.QuerySelector(".agent-content").OuterHtml;

            var price = _document.QuerySelector(".property-header-price strong").Text();
            var priceQualifier = _document.QuerySelector(".property-header-qualifier")?.Text();
            SetPrice(rightmovePropertyListing, price, priceQualifier);

            rightmovePropertyListing.Address = _document.QuerySelector(".property-header address").Text().Trim();

            var imageElements = _document.QuerySelectorAll("meta[property=\"og:image\"]");
            foreach (IElement imageElement in imageElements)
            {
                rightmovePropertyListing.Images.Add(imageElement.GetAttribute("content"));
            }

            var addedOnRightmove = _document.QuerySelector("#firstListedDateValue")?.Text();
            if (!string.IsNullOrEmpty(addedOnRightmove))
            {
                rightmovePropertyListing.DateAdded = DateTime.ParseExact(addedOnRightmove, "dd MMMM yyyy", new CultureInfo("en-GB"));
            }
            else
            {
                // Annoyingly, not all properties on Rightmove list when they were added.
                rightmovePropertyListing.DateAdded = DateTime.MinValue;
            }

            return rightmovePropertyListing;
        }

        private void SetPrice(RightmovePropertyListing rightmovePropertyListing, string price, string priceQualifier)
        {
            //REVIEW: It might make sense just to consider any non-numeric as a price qualifier.
            if (price != "POA" && price != "Offers Invited" && price != "Sale by Tender")
            {
                rightmovePropertyListing.Price = ParsePrice(price);
                rightmovePropertyListing.PriceQualifier = priceQualifier;
            }
            else
            {
                // Handle price on application scenario, etc. No
                // property on Rightmove can have a price of £0,
                // £1 is the minium property price so it is safe
                // to set the price to 0.
                rightmovePropertyListing.Price = 0;
                rightmovePropertyListing.PriceQualifier = price;
            }
        }

        private bool PropertyMatchesShortlist(RightmovePropertyListing rightmovePropertyListing, bool fullTest = true)
        {
            // The property might be excluded more than once if it is a featured property so we
            // need to account for this, hence why we don't use the _removedProperties.Add()
            // method and instead use the indexer

            if (_shortlist.MinimumPrice > 0 && rightmovePropertyListing.Price > 0 && rightmovePropertyListing.Price < _shortlist.MinimumPrice)
            {
                if (OptionsForm.DebugingEnabled)
                    _removedProperties[rightmovePropertyListing.Link] = "Price too low: " + rightmovePropertyListing.PriceQualifier + " " + rightmovePropertyListing.Price;
                return false;
            }

            if (_shortlist.MaximumPrice > 0 && rightmovePropertyListing.Price > 0 && rightmovePropertyListing.Price > _shortlist.MaximumPrice)
            {
                if (OptionsForm.DebugingEnabled)
                    _removedProperties[rightmovePropertyListing.Link] = "Price too high: " + rightmovePropertyListing.PriceQualifier + " " + rightmovePropertyListing.Price;
                return false;
            }

            if (_shortlist.MaximumPrice > 0 && rightmovePropertyListing.Price > 0 && rightmovePropertyListing.Price == _shortlist.MaximumPrice && rightmovePropertyListing.PriceQualifier == "Offers in Excess of")
            {
                if (OptionsForm.DebugingEnabled)
                    _removedProperties[rightmovePropertyListing.Link] = "Price too high: Offers in Excess of " + rightmovePropertyListing.PriceQualifier + " " + rightmovePropertyListing.Price;
                return false;
            }

            if (_shortlist.ExcludePriceOnApplication && rightmovePropertyListing.PriceQualifier == "POA")
            {
                if (OptionsForm.DebugingEnabled)
                    _removedProperties[rightmovePropertyListing.Link] = "POA excluded.";
                return false;
            }

            if (_shortlist.ExcludeOffersInvited && rightmovePropertyListing.PriceQualifier == "Offers Invited")
            {
                if (OptionsForm.DebugingEnabled)
                    _removedProperties[rightmovePropertyListing.Link] = "Offers Invited excluded.";
                return false;
            }

            if (_shortlist.ExcludeTerms.Count() > 0)
            {
                string excludedTermFound = null;
                foreach (string term in _shortlist.ExcludeTerms)
                {
                    var regex = new Regex(@"\b" + term + @"\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                    if (regex.IsMatch(rightmovePropertyListing.Title))
                    {
                        excludedTermFound = term;
                        break;
                    }
                    else if (regex.IsMatch(rightmovePropertyListing.Description))
                    {
                        excludedTermFound = term;
                        break;
                    }
                    else if (!string.IsNullOrEmpty(rightmovePropertyListing.PriceQualifier) && regex.IsMatch(rightmovePropertyListing.PriceQualifier))
                    {
                        excludedTermFound = term;
                        break;
                    }
                }
                if (!string.IsNullOrEmpty(excludedTermFound))
                {
                    if (OptionsForm.DebugingEnabled)
                        _removedProperties[rightmovePropertyListing.Link] = "Excluded term found: " + excludedTermFound;
                    return false;
                }
            }

            if (fullTest)
            {
                // We only test for terms that must be included in a full test
                // as the search listings page doesn't include the full
                // description so we could potentially filter out matching
                // properties if we were to do this prior to having the full
                // description of the property.

                if (_shortlist.ExcludeTerms.Count() > 0)
                {
                    bool requiredTermFound = false;
                    foreach (string term in _shortlist.IncludeTerms)
                    {
                        var regex = new Regex(@"\b" + term + @"\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                        if (regex.IsMatch(rightmovePropertyListing.Title))
                        {
                            requiredTermFound = true;
                            break;
                        }
                        else if (regex.IsMatch(rightmovePropertyListing.Description))
                        {
                            requiredTermFound = true;
                            break;
                        }
                        else if (!string.IsNullOrEmpty(rightmovePropertyListing.PriceQualifier) && regex.IsMatch(rightmovePropertyListing.PriceQualifier))
                        {
                            requiredTermFound = true;
                            break;
                        }
                    }
                    if (!requiredTermFound)
                    {
                        if (OptionsForm.DebugingEnabled)
                            _removedProperties[rightmovePropertyListing.Link] = "One or more required terms were not found.";
                        return false;
                    }
                }

                // Annoyingly, not all properties on Rightmove list when they were added so any with a DateTime.MinValue
                // are properties that were added on an unknown date.

                if (_shortlist.ExcludePostcodes.Count() > 0)
                {
                    var postcodeRegex = new Regex("propertyPostcode: \"([^\"]+)\",", RegexOptions.Compiled);
                    var postcodeMatch = postcodeRegex.Match(rightmovePropertyListing.Html);
                    if (postcodeMatch.Success)
                    {
                        var postcode = Postcode.Parse(postcodeMatch.Groups[1].Value);
                        foreach (string excludePostcode in _shortlist.ExcludePostcodes)
                        {
                            var excludedPostcode = Postcode.Parse(excludePostcode);
                            if (excludedPostcode.IsPartcialMatch(postcode)) return false;
                        }
                    }
                }

                //TODO: finish filtering the listings....
                // AddedAfter, etc.
            }

            return true;
        }

        private bool MoveToNextSearchResultsPage()
        {
            if (string.IsNullOrEmpty(_propertySearchListingsPage))
            {
                throw new InvalidOperationException(nameof(_propertySearchListingsPage) + " has not been set.");
            }

            var currentUrl = new Uri(_propertySearchListingsPage);
            string newUrl = "";
            string currentIndex = HttpUtility.ParseQueryString(currentUrl.Query).Get("index");
            string nextIndex = null;

            //TODO: There is a bug where the index is not being added correctly.
            // See file:///C:/Users/MatthewB/AppData/Roaming/HomeHunter/report-20200524032325.html

            _pagination.RemoveAt(0);
            if (_pagination.Count() < 1)
            {
                // Finished crawling.
                return false;
            }
            nextIndex = _pagination.FirstOrDefault();

            if (currentIndex == null)
            {
                newUrl = _propertySearchListingsPage.Replace("?", "?index=" + nextIndex + "&");
            }
            else
            {
                newUrl = _propertySearchListingsPage.Replace("index=" + currentIndex, "index=" + nextIndex);
            }

            LoadUrl(newUrl);

            return true;
        }

        private void GenerateReport()
        {
            _shortlistedProperties = SortProperties(_shortlistedProperties);

            var reportHtml = File.ReadAllText("Report.html");

            var tempHtml = "";
            if (_shortlistedProperties.Count > 0)
            {
                foreach (RightmovePropertyListing rightmovePropertyListing in _shortlistedProperties)
                {
                    tempHtml += "<div class=\"row\">";

                    //TODO: Add an X which when clicked removes the property from the results and future
                    // searches.

                    // Image
                    var mainImage = rightmovePropertyListing.Images.FirstOrDefault();
                    if (mainImage == null) mainImage = "placeholder.jpg";
                    tempHtml += "<div class=\"placeholder col-md-6\">";
                    tempHtml += "<img src=\"" + mainImage + "\">";
                    //TODO: List the other images here...
                    tempHtml += "</div>";

                    // Description
                    tempHtml += "<div class=\"description col-md-6\">";
                    tempHtml += "<a href=\"" + rightmovePropertyListing.Link + "\">" + rightmovePropertyListing.Title + "</a><br>";
                    tempHtml += FormatPrice(rightmovePropertyListing.Price) + "<br>";
                    tempHtml += "<strong>" + rightmovePropertyListing.Address + "</strong><br>";
                    if (rightmovePropertyListing.DateAdded > DateTime.MinValue)
                        tempHtml += "<strong>Date added to Rightmove:</strong> " + rightmovePropertyListing.DateAdded.ToString("dd MMMM yyyy") + "<br>";
                    tempHtml += rightmovePropertyListing.Description;
                    tempHtml += "</div>";

                    tempHtml += "</div>";
                }
            }
            else
            {
                tempHtml = "<div class=\"row\"><p>No matching properties found!</p></div>";
            }
            reportHtml = reportHtml.Replace("@Properties", tempHtml);

            tempHtml = "<ul>";
            foreach (KeyValuePair<string, string> rightmovePropertyListing in _removedProperties)
            {
                tempHtml += "<li><a href=" + rightmovePropertyListing.Key + ">" + rightmovePropertyListing.Key + "</a> " + rightmovePropertyListing.Value + "</li>";
            }
            tempHtml += "</ul>";
            reportHtml = reportHtml.Replace("@RemovedProperties", tempHtml);

            tempHtml = "<ul>";
            foreach (string url in _urlLog)
            {
                tempHtml += "<li><a href=" + url + ">" + url + "</a></li>";
            }
            tempHtml += "</ul>";
            reportHtml = reportHtml.Replace("@UrlsCrawled", tempHtml);

            Directory.CreateDirectory(_ReportFolderPath);
            var reportPath = Path.Combine(_ReportFolderPath, _shortlist.Name + " " + DateTime.Now.ToString("dd MMMM yyyy") + ".html");
            File.WriteAllText(reportPath, reportHtml);

            LoadUrl(reportPath);
        }

        private int ParsePrice(string priceValue)
        {
            var priceNumbers = priceValue.Trim().Replace("£", "").Replace(",", "");

            // Rightmove supports adding information in the price, such as the
            // number of acres for land, so the following works around this
            // issue, see https://www.rightmove.co.uk/commercial-property-for-sale/property-77914549.html
            int indexOfLineBreak = priceNumbers.IndexOf("\n");
            if (indexOfLineBreak >= 0)
                priceNumbers = priceNumbers.Remove(indexOfLineBreak);

            if (!CrawlerHelper.IsDigitsOnly(priceNumbers))
            {
                File.WriteAllText(Path.Combine(OptionsForm.DataDirectory, "unknown-price-qualifier-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt"), priceValue);

                throw new UnexpectedPriceQualifierException(priceValue);
            }
            return Int32.Parse(priceNumbers);
        }

        private string FormatPrice(int price)
        {
            return "£" + string.Format("{0:n0}", price);
        }

        private List<RightmovePropertyListing> SortProperties(List<RightmovePropertyListing> rightmovePropertyListing)
        {
            switch (_shortlist.SortOrder)
            {
                case PropertyListingSortOrder.LowestPrice:
                    return rightmovePropertyListing.OrderBy(p => p.Price).ToList();
                default:
                    //TODO: Finish adding sort order support.
                    throw new NotSupportedException();
            }
        }

        private List<RightmovePropertyListing> FindPropertyListings(string html)
        {
            //TODO: Validate the current HTML and throw an exception
            // if the elements we expect to find are not present
            // in the page. Make sure to take into account the below issue
            // whereby the DOM simply hasn't caught up with the crawler.
            // See ref xxx1.

            var propertyListings = new List<RightmovePropertyListing>();

            var searchResults = _document.QuerySelectorAll(".l-searchResult");

            // Loop through each of the listings first, gather as much as we can.
            // We will later filter the listings based on the information gathered
            // here to reduce the number of requests we have to make, this speeds
            // up the crawling and makes the traffic look more human. This is why
            // we don't crawl the individual property details pages yet.
            foreach (IElement element in searchResults)
            {
                if (element.ClassList.Contains("l-searchResult-loading"))
                {
                    // Sometimes the property listings don't load quick enough so
                    // we need to refresh the page to get them. Ref: xxx1
                    _browser.Reload(true);
                    _crawlerErrors++;
                    return null;
                }

                var rightmovePropertyListing = new RightmovePropertyListing();

                rightmovePropertyListing.Html = element.Html();
                rightmovePropertyListing.Link = "https://www.rightmove.co.uk" + element.QuerySelector(".propertyCard-link").GetAttribute("href");

                rightmovePropertyListing.Title = element.QuerySelector(".propertyCard-title").Text();

                // We set the description for now in case it helps us save making a request
                // when filtering later even though we know it isn't the complete description
                // that will be obtained when visiting the property details page.
                rightmovePropertyListing.Description = element.QuerySelector(".propertyCard-description span span").Text();

                var price = element.QuerySelector(".propertyCard-priceValue").Text();
                var priceQualifier = element.QuerySelector(".propertyCard-priceQualifier")?.Text();
                SetPrice(rightmovePropertyListing, price, priceQualifier);

                propertyListings.Add(rightmovePropertyListing);
            }

            return propertyListings;
        }

        private List<RightmovePropertyListing> FilterPropertyListings(List<RightmovePropertyListing> rightmovePropertyListings)
        {
            var filteredList = new List<RightmovePropertyListing>();

            foreach (RightmovePropertyListing rightmovePropertyListing in rightmovePropertyListings)
            {
                if (!PropertyMatchesShortlist(rightmovePropertyListing, false)) continue;
                if (HasPropertyAlreadyBeenCrawled(rightmovePropertyListing)) continue;
                filteredList.Add(rightmovePropertyListing);
            }

            return filteredList;
        }

        private bool HasPropertyAlreadyBeenCrawled(RightmovePropertyListing rightmovePropertyListing)
        {
            return _urlLog.Contains(rightmovePropertyListing.Link);
        }

        private void LoadUrl(string url)
        {
            // Adds some guards to make sure we are not sending the crawler to the
            // wrong page.

            if (!url.StartsWith("C:\\") && !Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
                throw new ArgumentException("Url is not well formed.");

            // Should not get here, this is just a guard, added because there is a problem
            // with this happening so to help track it down and ensure it doesn't happen
            // again this guard will remain.
            if (CrawlerHelper.BrowserAddressIsAPropertySearchListingsPage(url) && !url.Contains("index=") && _shortlistedProperties.Count > 0)
                throw new InvalidOperationException("Already crawled the first page.");

            // Keep seeing duplicates so if the same URL has been crawled once already it isn't crawled again
            // as there is no reason to as to why it should be crawled a second time.
            if (_urlLog.Contains(url))
                throw new InvalidOperationException("URL has already been crawled.");

            // Don't crawl properties that have already been excluded.
            if (_crawling && _shortlist.ExcludePropertyUrls.Contains(url))
                return;

            _browser.Load(url);
        }
    }
}
