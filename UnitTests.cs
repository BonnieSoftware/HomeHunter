using System;
using System.Windows.Forms;

namespace CefSharp.MinimalExample.WinForms
{
    public static class UnitTests
    {
        public static void Run()
        {
            BrowserAddressIsAPropertyDetailsPage.TestWithCorrectAddress();
            BrowserAddressIsAPropertyDetailsPage.TestWithIncorrectAddress();

            SetPageIndex.TestWithNoIndex();
            SetPageIndex.TestWithNoIndexButNeedsIndex();
            SetPageIndex.TestWithIndexButNoValue();
            SetPageIndex.TestWithIndex();
            SetPageIndex.TestSetNoIndex();

            Postcodes.PartPostcode1();
            Postcodes.PartPostcode2();
            Postcodes.PartPostcode3();
            Postcodes.FullPostcode();
            Postcodes.SpaceInWrongPage();
            Postcodes.NoSpace();
            Postcodes.ValidPostcode1();
            Postcodes.ValidPostcode2();
            Postcodes.ValidPostcode3();
            Postcodes.ValidPostcode4();
            Postcodes.ValidPostcode5();
            Postcodes.ValidPostcode6();
            Postcodes.ValidPostcode1NoSpace();
            Postcodes.ValidPostcode2NoSpace();
            Postcodes.ValidPostcode3NoSpace();
            Postcodes.ValidPostcode4NoSpace();
            Postcodes.ValidPostcode5NoSpace();
            Postcodes.ValidPostcode6NoSpace();
            Postcodes.ValidPostcode1SpaceInWrongPlace();
            Postcodes.ValidPostcode2SpaceInWrongPlace();
            Postcodes.ValidPostcode3SpaceInWrongPlace();
            Postcodes.ValidPostcode4SpaceInWrongPlace();
            Postcodes.ValidPostcode5SpaceInWrongPlace();
            Postcodes.ValidPostcode6SpaceInWrongPlace();
            //Postcodes.WrongPostcode();

            MessageBox.Show("All tests passed!");
        }

        public static class BrowserAddressIsAPropertyDetailsPage
        {
            public static void TestWithCorrectAddress()
            {
                var correctAddress = "https://www.rightmove.co.uk/property-for-sale/property-87332558.html";
                var result = CrawlerHelper.BrowserAddressIsAPropertyDetailsPage(correctAddress);
                if (result == false) throw new FailedUnitTestException();
            }

            public static void TestWithIncorrectAddress()
            {
                var incorrectAddress = "https://www.rightmove.co.uk/estate-agents.html";
                var result = CrawlerHelper.BrowserAddressIsAPropertyDetailsPage(incorrectAddress);
                if (result == true) throw new FailedUnitTestException();
            }
        }

        public static class SetPageIndex
        {
            public static void TestWithNoIndex()
            {
                var noIndexAddress = "https://www.rightmove.co.uk/property-for-sale/find.html?locationIdentifier=USERDEFINEDAREA%5E%7B\"polylines\"%3A\"ctcpHvc_b%40o%7C%5BrqGsle%40gtcAqhkDwsvE%7BmIsn~AiuCownCgvIzp_Bgmj%40ra%7B%40p_OrdkAqrh%40bbj%40phV~kb%40tiD%7C%7Cx%40mrc%40p%7DgA%7Dsf%40hkSogqAkwuFgr_BblLtph%40ra%7B%40%7Bd%5Ep%7DgA%7Dpm%40gppAwzo%40h%7Ci%40cke%40uqGtpA%7B~eBp%7BP%7Dre%40gmSqeuCgwmAjhCa~tBdhjBymdAg%7Ci%40osmAgmpLbymB%7Dnr%40by%5C%7DzrBzbyAel%7DAtliDgl%7DAvtdAc%7CwFdbmA%7DvXhpmBftcAjcdApqnBbn%5EesbBn~k%40rjDn_%5EfppAx~U~eBzp_%40nz~C%7Dq%40r%7CfEnt_%40zbyAuj%40zrlCbrJf~vA%7Ddg%40f~vA%60sjBdjwCymZbwzDn%7DgAj~gBkb%60%40h%7Ci%40dpO%7Cc%7C%40\"%7D&maxBedrooms=0&minBedrooms=0&maxPrice=50000&sortType=1&propertyTypes=&mustHave=&dontShow=&furnishTypes=&keywords=";
                var result = CrawlerHelper.SetPageIndex(noIndexAddress, "0");
                if (result != "https://www.rightmove.co.uk/property-for-sale/find.html?locationIdentifier=USERDEFINEDAREA%5E%7B\"polylines\"%3A\"ctcpHvc_b%40o%7C%5BrqGsle%40gtcAqhkDwsvE%7BmIsn~AiuCownCgvIzp_Bgmj%40ra%7B%40p_OrdkAqrh%40bbj%40phV~kb%40tiD%7C%7Cx%40mrc%40p%7DgA%7Dsf%40hkSogqAkwuFgr_BblLtph%40ra%7B%40%7Bd%5Ep%7DgA%7Dpm%40gppAwzo%40h%7Ci%40cke%40uqGtpA%7B~eBp%7BP%7Dre%40gmSqeuCgwmAjhCa~tBdhjBymdAg%7Ci%40osmAgmpLbymB%7Dnr%40by%5C%7DzrBzbyAel%7DAtliDgl%7DAvtdAc%7CwFdbmA%7DvXhpmBftcAjcdApqnBbn%5EesbBn~k%40rjDn_%5EfppAx~U~eBzp_%40nz~C%7Dq%40r%7CfEnt_%40zbyAuj%40zrlCbrJf~vA%7Ddg%40f~vA%60sjBdjwCymZbwzDn%7DgAj~gBkb%60%40h%7Ci%40dpO%7Cc%7C%40\"%7D&maxBedrooms=0&minBedrooms=0&maxPrice=50000&sortType=1&propertyTypes=&mustHave=&dontShow=&furnishTypes=&keywords=")
                    throw new FailedUnitTestException();
            }

            public static void TestWithNoIndexButNeedsIndex()
            {
                var noIndexAddress = "https://www.rightmove.co.uk/property-for-sale/find.html?locationIdentifier=USERDEFINEDAREA%5E%7B\"polylines\"%3A\"ctcpHvc_b%40o%7C%5BrqGsle%40gtcAqhkDwsvE%7BmIsn~AiuCownCgvIzp_Bgmj%40ra%7B%40p_OrdkAqrh%40bbj%40phV~kb%40tiD%7C%7Cx%40mrc%40p%7DgA%7Dsf%40hkSogqAkwuFgr_BblLtph%40ra%7B%40%7Bd%5Ep%7DgA%7Dpm%40gppAwzo%40h%7Ci%40cke%40uqGtpA%7B~eBp%7BP%7Dre%40gmSqeuCgwmAjhCa~tBdhjBymdAg%7Ci%40osmAgmpLbymB%7Dnr%40by%5C%7DzrBzbyAel%7DAtliDgl%7DAvtdAc%7CwFdbmA%7DvXhpmBftcAjcdApqnBbn%5EesbBn~k%40rjDn_%5EfppAx~U~eBzp_%40nz~C%7Dq%40r%7CfEnt_%40zbyAuj%40zrlCbrJf~vA%7Ddg%40f~vA%60sjBdjwCymZbwzDn%7DgAj~gBkb%60%40h%7Ci%40dpO%7Cc%7C%40\"%7D&maxBedrooms=0&minBedrooms=0&maxPrice=50000&sortType=1&propertyTypes=&mustHave=&dontShow=&furnishTypes=&keywords=";
                var result = CrawlerHelper.SetPageIndex(noIndexAddress, "24");
                if (result != "https://www.rightmove.co.uk/property-for-sale/find.html?index=24&locationIdentifier=USERDEFINEDAREA%5E%7B\"polylines\"%3A\"ctcpHvc_b%40o%7C%5BrqGsle%40gtcAqhkDwsvE%7BmIsn~AiuCownCgvIzp_Bgmj%40ra%7B%40p_OrdkAqrh%40bbj%40phV~kb%40tiD%7C%7Cx%40mrc%40p%7DgA%7Dsf%40hkSogqAkwuFgr_BblLtph%40ra%7B%40%7Bd%5Ep%7DgA%7Dpm%40gppAwzo%40h%7Ci%40cke%40uqGtpA%7B~eBp%7BP%7Dre%40gmSqeuCgwmAjhCa~tBdhjBymdAg%7Ci%40osmAgmpLbymB%7Dnr%40by%5C%7DzrBzbyAel%7DAtliDgl%7DAvtdAc%7CwFdbmA%7DvXhpmBftcAjcdApqnBbn%5EesbBn~k%40rjDn_%5EfppAx~U~eBzp_%40nz~C%7Dq%40r%7CfEnt_%40zbyAuj%40zrlCbrJf~vA%7Ddg%40f~vA%60sjBdjwCymZbwzDn%7DgAj~gBkb%60%40h%7Ci%40dpO%7Cc%7C%40\"%7D&maxBedrooms=0&minBedrooms=0&maxPrice=50000&sortType=1&propertyTypes=&mustHave=&dontShow=&furnishTypes=&keywords=")
                    throw new FailedUnitTestException();
            }

            public static void TestWithIndexButNoValue()
            {
                var indexButNoValueAddress = "https://www.rightmove.co.uk/property-for-sale/find.html?locationIdentifier=USERDEFINEDAREA%5E%7B\"polylines\"%3A\"ctcpHvc_b%40o%7C%5BrqGsle%40gtcAqhkDwsvE%7BmIsn~AiuCownCgvIzp_Bgmj%40ra%7B%40p_OrdkAqrh%40bbj%40phV~kb%40tiD%7C%7Cx%40mrc%40p%7DgA%7Dsf%40hkSogqAkwuFgr_BblLtph%40ra%7B%40%7Bd%5Ep%7DgA%7Dpm%40gppAwzo%40h%7Ci%40cke%40uqGtpA%7B~eBp%7BP%7Dre%40gmSqeuCgwmAjhCa~tBdhjBymdAg%7Ci%40osmAgmpLbymB%7Dnr%40by%5C%7DzrBzbyAel%7DAtliDgl%7DAvtdAc%7CwFdbmA%7DvXhpmBftcAjcdApqnBbn%5EesbBn~k%40rjDn_%5EfppAx~U~eBzp_%40nz~C%7Dq%40r%7CfEnt_%40zbyAuj%40zrlCbrJf~vA%7Ddg%40f~vA%60sjBdjwCymZbwzDn%7DgAj~gBkb%60%40h%7Ci%40dpO%7Cc%7C%40\"%7D&maxBedrooms=0&minBedrooms=0&maxPrice=50000&sortType=1&propertyTypes=&mustHave=&dontShow=&furnishTypes=&keywords=&index=";
                var result = CrawlerHelper.SetPageIndex(indexButNoValueAddress, "24");
                if (result != "https://www.rightmove.co.uk/property-for-sale/find.html?locationIdentifier=USERDEFINEDAREA%5E%7B\"polylines\"%3A\"ctcpHvc_b%40o%7C%5BrqGsle%40gtcAqhkDwsvE%7BmIsn~AiuCownCgvIzp_Bgmj%40ra%7B%40p_OrdkAqrh%40bbj%40phV~kb%40tiD%7C%7Cx%40mrc%40p%7DgA%7Dsf%40hkSogqAkwuFgr_BblLtph%40ra%7B%40%7Bd%5Ep%7DgA%7Dpm%40gppAwzo%40h%7Ci%40cke%40uqGtpA%7B~eBp%7BP%7Dre%40gmSqeuCgwmAjhCa~tBdhjBymdAg%7Ci%40osmAgmpLbymB%7Dnr%40by%5C%7DzrBzbyAel%7DAtliDgl%7DAvtdAc%7CwFdbmA%7DvXhpmBftcAjcdApqnBbn%5EesbBn~k%40rjDn_%5EfppAx~U~eBzp_%40nz~C%7Dq%40r%7CfEnt_%40zbyAuj%40zrlCbrJf~vA%7Ddg%40f~vA%60sjBdjwCymZbwzDn%7DgAj~gBkb%60%40h%7Ci%40dpO%7Cc%7C%40\"%7D&maxBedrooms=0&minBedrooms=0&maxPrice=50000&sortType=1&propertyTypes=&mustHave=&dontShow=&furnishTypes=&keywords=&index=24")
                    throw new FailedUnitTestException();
            }

            public static void TestWithIndex()
            {
                var indexAddress = "https://www.rightmove.co.uk/property-to-rent/find.html?locationIdentifier=REGION%5E689&minBedrooms=0&sortType=1&index=120&propertyTypes=&includeLetAgreed=false&mustHave=&dontShow=&furnishTypes=&keywords=";
                var result = CrawlerHelper.SetPageIndex(indexAddress, "144");
                if (result != "https://www.rightmove.co.uk/property-to-rent/find.html?locationIdentifier=REGION%5E689&minBedrooms=0&sortType=1&index=144&propertyTypes=&includeLetAgreed=false&mustHave=&dontShow=&furnishTypes=&keywords=")
                    throw new FailedUnitTestException();
            }

            public static void TestSetNoIndex()
            {
                var indexAddress = "https://www.rightmove.co.uk/property-for-sale/find.html?locationIdentifier=USERDEFINEDAREA%5E%7B\"polylines\"%3A\"ctcpHvc_b%40o%7C%5BrqGsle%40gtcAqhkDwsvE%7BmIsn~AiuCownCgvIzp_Bgmj%40ra%7B%40p_OrdkAqrh%40bbj%40phV~kb%40tiD%7C%7Cx%40mrc%40p%7DgA%7Dsf%40hkSogqAkwuFgr_BblLtph%40ra%7B%40%7Bd%5Ep%7DgA%7Dpm%40gppAwzo%40h%7Ci%40cke%40uqGtpA%7B~eBp%7BP%7Dre%40gmSqeuCgwmAjhCa~tBdhjBymdAg%7Ci%40osmAgmpLbymB%7Dnr%40by%5C%7DzrBzbyAel%7DAtliDgl%7DAvtdAc%7CwFdbmA%7DvXhpmBftcAjcdApqnBbn%5EesbBn~k%40rjDn_%5EfppAx~U~eBzp_%40nz~C%7Dq%40r%7CfEnt_%40zbyAuj%40zrlCbrJf~vA%7Ddg%40f~vA%60sjBdjwCymZbwzDn%7DgAj~gBkb%60%40h%7Ci%40dpO%7Cc%7C%40\"%7D&maxBedrooms=0&minBedrooms=0&maxPrice=50000&sortType=1&propertyTypes=&index=144&mustHave=&dontShow=&furnishTypes=&keywords=";
                var result = CrawlerHelper.SetPageIndex(indexAddress, "0");
                if (result != "https://www.rightmove.co.uk/property-for-sale/find.html?locationIdentifier=USERDEFINEDAREA%5E%7B\"polylines\"%3A\"ctcpHvc_b%40o%7C%5BrqGsle%40gtcAqhkDwsvE%7BmIsn~AiuCownCgvIzp_Bgmj%40ra%7B%40p_OrdkAqrh%40bbj%40phV~kb%40tiD%7C%7Cx%40mrc%40p%7DgA%7Dsf%40hkSogqAkwuFgr_BblLtph%40ra%7B%40%7Bd%5Ep%7DgA%7Dpm%40gppAwzo%40h%7Ci%40cke%40uqGtpA%7B~eBp%7BP%7Dre%40gmSqeuCgwmAjhCa~tBdhjBymdAg%7Ci%40osmAgmpLbymB%7Dnr%40by%5C%7DzrBzbyAel%7DAtliDgl%7DAvtdAc%7CwFdbmA%7DvXhpmBftcAjcdApqnBbn%5EesbBn~k%40rjDn_%5EfppAx~U~eBzp_%40nz~C%7Dq%40r%7CfEnt_%40zbyAuj%40zrlCbrJf~vA%7Ddg%40f~vA%60sjBdjwCymZbwzDn%7DgAj~gBkb%60%40h%7Ci%40dpO%7Cc%7C%40\"%7D&maxBedrooms=0&minBedrooms=0&maxPrice=50000&sortType=1&propertyTypes=&mustHave=&dontShow=&furnishTypes=&keywords=")
                    throw new FailedUnitTestException();
            }
        }

        public static class Postcodes
        {
            public static void PartPostcode1()
            {
                var postcode = Postcode.Parse("IP16");
                if (postcode.FirstPart != "IP16") throw new FailedUnitTestException();
                if (postcode.LastPart != null) throw new FailedUnitTestException();
            }

            public static void PartPostcode2()
            {
                var postcode = Postcode.Parse("M1");
                if (postcode.FirstPart != "M1") throw new FailedUnitTestException();
                if (postcode.LastPart != null) throw new FailedUnitTestException();
            }

            public static void PartPostcode3()
            {
                var postcode = Postcode.Parse("TS1");
                if (postcode.FirstPart != "TS1") throw new FailedUnitTestException();
                if (postcode.LastPart != null) throw new FailedUnitTestException();
            }

            public static void FullPostcode()
            {
                var postcode = Postcode.Parse("IP16 4AY");
                if (postcode.FirstPart != "IP16") throw new FailedUnitTestException();
                if (postcode.LastPart != "4AY") throw new FailedUnitTestException();
            }

            public static void SpaceInWrongPage()
            {
                var postcode = Postcode.Parse("IP1 64AY");
                if (postcode.FirstPart != "IP16") throw new FailedUnitTestException();
                if (postcode.LastPart != "4AY") throw new FailedUnitTestException();
            }

            public static void NoSpace()
            {
                var postcode = Postcode.Parse("IP164AY");
                if (postcode.FirstPart != "IP16") throw new FailedUnitTestException();
                if (postcode.LastPart != "4AY") throw new FailedUnitTestException();
            }

            public static void ValidPostcode1()
            {
                var postcode = Postcode.Parse("M1 1AA");
                if (postcode.FirstPart != "M1") throw new FailedUnitTestException();
                if (postcode.LastPart != "1AA") throw new FailedUnitTestException();
            }

            public static void ValidPostcode2()
            {
                var postcode = Postcode.Parse("M60 1NW");
                if (postcode.FirstPart != "M60") throw new FailedUnitTestException();
                if (postcode.LastPart != "1NW") throw new FailedUnitTestException();
            }

            public static void ValidPostcode3()
            {
                var postcode = Postcode.Parse("CR2 6XH");
                if (postcode.FirstPart != "CR2") throw new FailedUnitTestException();
                if (postcode.LastPart != "6XH") throw new FailedUnitTestException();
            }

            public static void ValidPostcode4()
            {
                var postcode = Postcode.Parse("DN55 1PT");
                if (postcode.FirstPart != "DN55") throw new FailedUnitTestException();
                if (postcode.LastPart != "1PT") throw new FailedUnitTestException();
            }

            public static void ValidPostcode5()
            {
                var postcode = Postcode.Parse("W1A 1HQ");
                if (postcode.FirstPart != "W1A") throw new FailedUnitTestException();
                if (postcode.LastPart != "1HQ") throw new FailedUnitTestException();
            }

            public static void ValidPostcode6()
            {
                var postcode = Postcode.Parse("EC1A 1BB");
                if (postcode.FirstPart != "EC1A") throw new FailedUnitTestException();
                if (postcode.LastPart != "1BB") throw new FailedUnitTestException();
            }

            public static void ValidPostcode1NoSpace()
            {
                var postcode = Postcode.Parse("M11AA");
                if (postcode.FirstPart != "M1") throw new FailedUnitTestException();
                if (postcode.LastPart != "1AA") throw new FailedUnitTestException();
            }

            public static void ValidPostcode2NoSpace()
            {
                var postcode = Postcode.Parse("M601NW");
                if (postcode.FirstPart != "M60") throw new FailedUnitTestException();
                if (postcode.LastPart != "1NW") throw new FailedUnitTestException();
            }

            public static void ValidPostcode3NoSpace()
            {
                var postcode = Postcode.Parse("CR26XH");
                if (postcode.FirstPart != "CR2") throw new FailedUnitTestException();
                if (postcode.LastPart != "6XH") throw new FailedUnitTestException();
            }

            public static void ValidPostcode4NoSpace()
            {
                var postcode = Postcode.Parse("DN551PT");
                if (postcode.FirstPart != "DN55") throw new FailedUnitTestException();
                if (postcode.LastPart != "1PT") throw new FailedUnitTestException();
            }

            public static void ValidPostcode5NoSpace()
            {
                var postcode = Postcode.Parse("W1A1HQ");
                if (postcode.FirstPart != "W1A") throw new FailedUnitTestException();
                if (postcode.LastPart != "1HQ") throw new FailedUnitTestException();
            }

            public static void ValidPostcode6NoSpace()
            {
                var postcode = Postcode.Parse("EC1A1BB");
                if (postcode.FirstPart != "EC1A") throw new FailedUnitTestException();
                if (postcode.LastPart != "1BB") throw new FailedUnitTestException();
            }

            public static void ValidPostcode1SpaceInWrongPlace()
            {
                var postcode = Postcode.Parse("M1 1AA");
                if (postcode.FirstPart != "M1") throw new FailedUnitTestException();
                if (postcode.LastPart != "1AA") throw new FailedUnitTestException();
            }

            public static void ValidPostcode2SpaceInWrongPlace()
            {
                var postcode = Postcode.Parse("M601 NW");
                if (postcode.FirstPart != "M60") throw new FailedUnitTestException();
                if (postcode.LastPart != "1NW") throw new FailedUnitTestException();
            }

            public static void ValidPostcode3SpaceInWrongPlace()
            {
                var postcode = Postcode.Parse("CR 26XH");
                if (postcode.FirstPart != "CR2") throw new FailedUnitTestException();
                if (postcode.LastPart != "6XH") throw new FailedUnitTestException();
            }

            public static void ValidPostcode4SpaceInWrongPlace()
            {
                var postcode = Postcode.Parse("DN5 51PT");
                if (postcode.FirstPart != "DN55") throw new FailedUnitTestException();
                if (postcode.LastPart != "1PT") throw new FailedUnitTestException();
            }

            public static void ValidPostcode5SpaceInWrongPlace()
            {
                var postcode = Postcode.Parse("W1 A1HQ");
                if (postcode.FirstPart != "W1A") throw new FailedUnitTestException();
                if (postcode.LastPart != "1HQ") throw new FailedUnitTestException();
            }

            public static void ValidPostcode6SpaceInWrongPlace()
            {
                var postcode = Postcode.Parse("EC1 A1BB");
                if (postcode.FirstPart != "EC1A") throw new FailedUnitTestException();
                if (postcode.LastPart != "1BB") throw new FailedUnitTestException();
            }

            // Not so interested in jumbled letters/numbers.
            //public static void WrongPostcode()
            //{
            //    try
            //    {
            //        var postcode = Postcode.Parse("EC1A1");
            //    }
            //    catch (ArgumentException)
            //    {
            //        return;
            //    }
            //    throw new FailedUnitTestException();
            //}
        }
    }
}
