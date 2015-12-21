using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGUILocalizationTool
{
  class ValveLocalizationCache
  {
    public bool WithOriginText { get; set; }

    public List<LocalizationData> Data { get; set; }

    public bool DontSaveNotLocalized { get; set; }
  }
}
