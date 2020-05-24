using System;

namespace CefSharp.MinimalExample.WinForms
{
    public class UnexpectedPriceQualifierException : Exception
    {
        public UnexpectedPriceQualifierException(string priceValue)
            : base(HumanReadable(priceValue))
        {
        }

        private static string HumanReadable(string priceValue)
        {
            return "Unexpected price qualifier '" + priceValue + "'";
        }
    }
}
