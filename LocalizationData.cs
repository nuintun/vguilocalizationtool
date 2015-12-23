using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VGUILocalizationTool
{
  class LocalizationData
  {
    public string BOF { get; set; }

    public string ID { get; set; }

    public string DelimeterLocalized { get; set; }

    public string Localized { get; set; }

    public string DelimeterPlatform { get; set; }

    public string Platform { get; set; }

    public string EOF { get; set; }

    public string Origin { get; set; }

    public bool UseSlashN { get; set; }

    // 下面无用
    public bool OriginTextChanged { get; set; }

    public string DelimeterID { get; set; }

    public object DelimeterOrigin { get; set; }

    public string OriginOld { get; set; }
  }
}
