using CefSharp.MinimalExample.WinForms.Controls;
using System;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    public class ShortlistEditor : WebForm
    {
        private Shortlist _shortlist = new Shortlist();

        private Action _closingAction;

        public static void Open()
        {
            var form = new ShortlistEditor();
            form.Show();
        }

        public static ShortlistEditor Open(string id, Action action)
        {
            var form = Open(id);
            form.CenterToScreen();
            form._closingAction = action;
            return form;
        }

        public static ShortlistEditor Open(string id, bool show = true)
        {
            var form = new ShortlistEditor();
            form._shortlist = Shortlist.Open(id);
            if (show) form.Show();
            return form;
        }

        //TODO: Need to ensure ShotlistEditor.html is published to bin on build.
        public ShortlistEditor()
            : base("ShotlistEditor.html")
        {
            Text = "Shortlist Editor";
            Width = 500;
            
            FormClosing += new FormClosingEventHandler(this.Shortlist_FormClosing);

            browser.JavascriptObjectRepository.Register("shotlistEditor", this, true, BindingOptions.DefaultBinder);
        }

        protected override void OnBrowserFrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            SetInputValue("name", _shortlist.Name);

            SetInputValue("excludeTerms", WebInput.ToWebString(_shortlist.ExcludeTerms));
            SetInputValue("includeTerms", WebInput.ToWebString(_shortlist.IncludeTerms));

            SetInputValue("excludePostcodes", WebInput.ToWebString(_shortlist.ExcludePostcodes));

            if (_shortlist.MinimumPrice > -1)
                SetInputValue("minimumPrice", _shortlist.MinimumPrice.ToString());
            if (_shortlist.MaximumPrice > -1)
                SetInputValue("maximumPrice", _shortlist.MaximumPrice.ToString());

            SetInputValue("advertisedOnOrAfter", (int)_shortlist.AdvertisedOnOrAfter);
            SetInputValue("advertisedForOrOver", (int)_shortlist.AdvertisedForOrOver);

            SetInputValue("sortOrder", (int)_shortlist.SortOrder);
            SetInputValue("locationToSortBy", _shortlist.LocationToSortBy);

            SetInputValue("excludePriceOnApplication", _shortlist.ExcludePriceOnApplication);
            SetInputValue("excludeOffersInvited", _shortlist.ExcludeOffersInvited);

            SetInputValue("resetIndexAtStart", _shortlist.ResetIndexAtStart);
            SetInputValue("rightmove42PageLimitFixEnabled", _shortlist.Rightmove42PageLimitFixEnabled);

            //browser.ShowDevTools();
        }

        public void Save(dynamic eventArgs)
        {
            _shortlist.Name = eventArgs.name;
            _shortlist.ExcludeTerms = WebInput.ToList(eventArgs.excludeTerms);
            _shortlist.IncludeTerms = WebInput.ToList(eventArgs.includeTerms);
            _shortlist.ExcludePostcodes = WebInput.ToList(eventArgs.excludePostcodes);
            _shortlist.MinimumPrice = WebInput.ToInt32(eventArgs.minimumPrice);
            _shortlist.MaximumPrice = WebInput.ToInt32(eventArgs.maximumPrice);
            _shortlist.AdvertisedOnOrAfter = (DateTimeRange)Convert.ToInt32(eventArgs.advertisedOnOrAfter);
            _shortlist.AdvertisedForOrOver = (DateTimeRange)Convert.ToInt32(eventArgs.advertisedForOrOver);
            _shortlist.SortOrder = (PropertyListingSortOrder)Convert.ToInt32(eventArgs.sortOrder);
            _shortlist.LocationToSortBy = eventArgs.locationToSortBy;
            _shortlist.ExcludePriceOnApplication = Convert.ToBoolean(eventArgs.excludePriceOnApplication);
            _shortlist.ExcludeOffersInvited = Convert.ToBoolean(eventArgs.excludeOffersInvited);
            _shortlist.Save();
            this.InvokeOnUiThreadIfRequired(() => Close());
        }

        public void Cancel()
        {
            this.InvokeOnUiThreadIfRequired(() => Close());
        }

        private void Shortlist_FormClosing(object sender, FormClosingEventArgs e)
        {
            _closingAction?.Invoke();
        }
    }
}
