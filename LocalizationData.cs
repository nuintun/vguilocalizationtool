using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VGUILocalizationTool
{
    class LocalizationData
    {
        public string DelimeterID
        { get; set; }

        public string ID
        { get; set; }

        public bool UseSlashN
        { get; set; }

        public string DelimeterEnglish
        { get; set; }

        public string English
        { get; set; }

        public string EnglishOld
        { get; set; }

        public bool EnglishTextChanged
        { get; set; }

        public string DelimeterLocalized
        { get; set; }

        public string Localized
        { get; set; }
    }
}
