using System.Globalization;
using System.Threading;
using Foundation;
using Sales.Interfaces;
using Sales.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(Sales.iOS.Implementations.Localize))]
namespace Sales.iOS.Implementations
{
    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            var netLanguage = "en";
            if (NSLocale.PreferredLanguages.Length > 0)
            {
                var pref = NSLocale.PreferredLanguages[0];
                netLanguage = iOSToDotnetLanguage(pref);
            }

            // this gets called a lot - try/catch can be expensive so consider caching
            System.Globalization.CultureInfo ci = null;
            try
            {
                ci = new System.Globalization.CultureInfo(netLanguage);
            }
            catch (CultureNotFoundException ex1)
            {
                // ios locale not valid .NET culture (eg. "en-ES" : English in Spain)
                // fallback to first characters, in this case "en"
                try
                {
                    var fallback = ToDotnetFallbackLanguage(new PlatformCulture(netLanguage));
                    ci = new System.Globalization.CultureInfo(fallback);
                }
                catch (CultureNotFoundException ex2)
                {
                    // iOS language not valid .NET culture, falling back to English
                    ci = new System.Globalization.CultureInfo("en");
                }
            }

            return ci;
        }

        public void SetLocale(CultureInfo ci)
        {
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        private string iOSToDotnetLanguage(string iOSLanguage)
        {
            var netLanguage = iOSLanguage;
            switch (iOSLanguage)
            {
                case "ms-MY": // Malaysian (Malaysian) not supported .Net culture
                case "ms-SG": // Malaysian (Singapore) not supported .Net culture
                    netLanguage = "ms"; // closest supported
                    break;
                case "gsw-CH": // Schwiizertuutch (Swiss German) not supported .NET culture
                    netLanguage = "de-CH"; // closest supported
                    break;
                    // add more applications-specific cases here (if required)
                    // ONLY use cultures that have been tested and know to work
            }

            return netLanguage;
        }

        private string ToDotnetFallbackLanguage(PlatformCulture platformCulture)
        {
            var netLanguage = platformCulture.LanguageCode;
            switch (platformCulture.LanguageCode)
            {
                case "pt":
                    netLanguage = "pt-PT"; // fallback to Portuguese
                    break;
                case "gsw":
                    netLanguage = "de-CH"; // equivalent to German for this app
                    break;

                    // add more application-specific cases here (if required)
                    // ONLY use cultures that have benn tested and know to work
            }

            return netLanguage;
        }
    }
}