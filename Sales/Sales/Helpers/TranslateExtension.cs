﻿using Sales.Interfaces;
using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sales.Helpers
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        private readonly CultureInfo ci;
        private const string ResourceId = "Sales.Resources.Resource";
        private static readonly Lazy<ResourceManager> ResMgr =
            new Lazy<ResourceManager>(() => new ResourceManager(
                ResourceId,
                typeof(TranslateExtension).GetTypeInfo().Assembly));

        public TranslateExtension()
        {
            ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
            {
                return "";
            }

            var translation = ResMgr.Value.GetString(Text, ci);

            if (translation == null)
            {
                /*
                #if DEBUG
                                throw new ArgumentException(
                                    String.Format(
                                        "Key '{0}' was not found in resources '{1}' for culture '{2}'.",
                                        Text, ResourceId, ci.Name), "Text");
                #else
                                translation = Text; // returns the key, which GETS DISPLAYED TO THE USER
                #endif
                */
                translation = Text; // returns the key, which GETS DISPLAYED TO THE USER
            }

            return translation;
        }
    }
}
