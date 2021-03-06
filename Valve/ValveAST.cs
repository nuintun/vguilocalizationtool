﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGUILocalizationTool.Valve
{
  class ValveAST
  {
    public string Frame { set; get; }
    public string Language { set; get; }
    public List<ValveToken> Tokens { set; get; }
    public bool WithOriginText { set; get; }
    public string OriginLanguage { set; get; }
  }
}
