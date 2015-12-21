using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VGUILocalizationTool
{
  class LocalizationData
  {
    public string DelimeterID { get; set; }

    public string ID { get; set; }

    public bool UseSlashN { get; set; }

    public object DelimeterOrigin { get; set; }

    public string Origin { get; set; }

    public string OriginOld { get; set; }

    public bool OriginTextChanged { get; set; }

    public string DelimeterLocalized { get; set; }

    public string Localized { get; set; }
  }
}
