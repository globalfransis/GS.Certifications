﻿using Microsoft.Extensions.Localization;
using System.Linq;
using System.Text.Json;

namespace GS.Certifications.Web.Common.Helpers
{
    public class LocalizationHelper<T> where T : class
    {
        private readonly IStringLocalizer<T> _loc;

        public LocalizationHelper(IStringLocalizer<T> stringLocalizer)
        {
            _loc = stringLocalizer;
        }

        public string GetStringTranslations()
        {
            // for translations
            var allStrings = _loc.GetAllStrings()?
                                       .ToDictionary(localizedString => localizedString.Name, localizedString => localizedString.Value);

            return JsonSerializer.Serialize(allStrings);
        }
    }
}
