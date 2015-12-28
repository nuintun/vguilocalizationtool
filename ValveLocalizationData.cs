using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGUILocalizationTool
{
  class ValveLocalizationData
  {
    public string Language { set; get; }
    public bool WithOriginText { set; get; }
    public List<LocalizationData> List { set; get; }
  }
}
