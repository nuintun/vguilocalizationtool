using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGUILocalizationTool.Valve
{
  class ValveToken
  {
    // 字段前面字符
    public string BOF { get; set; }
    // 字段字符串
    public string ID { get; set; }
    // 字段和语言之间的字符串
    public string DelimeterLocalized { get; set; }
    // 语言字符串
    public string Localized { get; set; }
    // 语言和平台之间的字符串
    public string DelimeterPlatform { get; set; }
    // 平台字符串
    public string Platform { get; set; }
    // 平台后面的字符串
    public string EOF { get; set; }
    // 原始语言字符串
    public string Origin { get; set; }
  }
}
