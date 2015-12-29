using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VGUILocalizationTool
{
  class LocalizationListData
  {
    public string ID { set; get; }
    public string Localized { set; get; }
    public string Platform { set; get; }
    public string Origin { set; get; }
    public string OriginOld { set; get; }
    public bool OriginTextChanged { set; get; }
  }
}
