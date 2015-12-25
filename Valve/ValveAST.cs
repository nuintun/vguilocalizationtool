using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VGUILocalizationTool.Valve
{
  class ValveAST
  {
    /// <summary>
    /// 语言占位符
    /// </summary>
    public static string LANGHOLDER = "%Lang%";
    private static ValveTokens Tokens = new ValveTokens();
    private static Regex ORILANGRE = new Regex(@"^\[[\w_]+\]");

    /// <summary>
    /// 转义花括号
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private string EscapeBrace(string token)
    {
      return token.Replace("{", "{{").Replace("}", "}}");
    }

    /// <summary>
    /// 是否是双引号包含的字符串
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private bool IsQuotationWrap(string token)
    {
      return token.StartsWith("\"") && token.EndsWith("\"");
    }

    /// <summary>
    /// 去处两端的字符串两端的双引号
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private string RemoveQuotationWrap(string token)
    {
      if (token.Length <= 2)
      {
        return string.Empty;
      }
      else
      {
        return token.Substring(1, token.Length - 2);
      }
    }

    /// <summary>
    /// 获取语法树
    /// </summary>
    /// <param name="path"></param>
    /// <param name="originLang"></param>
    /// <returns></returns>
    public Dictionary<string, object> GetAST(string path, string originLang = null)
    {
      string lang = null;
      string frame = null;
      string tokenInID = null;
      // Language 解析状态 - 0：未解析、1：正在解析、2：解析完毕
      int langState = 0;
      // Tokens 解析状态 - 0：未解析、1：准备解析、2：正在解析、3：解析完毕
      int tokensState = 0;
      bool withOriginText = false;
      string[][] tokens = Tokens.GetTokens(path);
      List<ValveLang> langTokens = new List<ValveLang>();
      Dictionary<string, object> valveAst = new Dictionary<string, object>();

      // 计算原始语言标识
      if (originLang != null)
      {
        tokenInID = "[" + originLang + "]";
      }

      // 循环语法片断行
      for (int i = 0, rows = tokens.Length; i < rows; i++)
      {
        // Tokens 解析标识 - 0：解析字段、1：准备语言、2：解析平台、3：解析其他
        int tokenFlag = 0;
        ValveLang item = null;

        // 已进入语言列表区域初始化语言对象
        if (tokensState == 2)
        {
          item = new ValveLang();
        }

        // 循环语法片断列
        for (int j = 0, cols = tokens[i].Length; j < cols; j++)
        {
          string token = tokens[i][j];

          if (langState < 2 && tokensState != 1 && tokensState != 2)
          {
            if (langState == 0 && token == "\"Language\"")
            {
              langState = 1;
            }
            else if (langState == 1)
            {
              if (IsQuotationWrap(token))
              {
                langState = 2;
                lang = RemoveQuotationWrap(token);
                frame += "\"{0}\"";
                continue;
              }
            }
          }

          if (tokensState == 0)
          {
            if (token == "\"Tokens\"")
            {
              tokensState = 1;
            }
          }
          else if (tokensState == 1)
          {
            if (token == "{")
            {
              tokensState = 2;
              item = new ValveLang();
              frame += EscapeBrace(token);

              if (j == cols - 1)
              {
                frame += Environment.NewLine;
              }

              frame += "{1}";
            }
          }
          else if (tokensState == 2)
          {
            if (token == "}")
            {
              tokensState = 3;

              if (j == 0)
              {
                frame += Environment.NewLine;
              }
            }
            else
            {
              // 解析语法
              switch (tokenFlag)
              {
                // 字段
                case 0:
                  if (IsQuotationWrap(token))
                  {
                    tokenFlag = 1;
                    item.ID = RemoveQuotationWrap(token);
                  }
                  else
                  {
                    item.BOF += token;
                  }
                  break;
                // 语言
                case 1:
                  if (IsQuotationWrap(token))
                  {
                    tokenFlag = 2;
                    item.Localized = RemoveQuotationWrap(token);
                  }
                  else
                  {
                    item.DelimeterLocalized += token;
                  }
                  break;
                // 平台
                case 2:
                  if (token.StartsWith("[") && token.EndsWith("]"))
                  {
                    tokenFlag = 3;
                    item.Platform = token;
                  }
                  else
                  {
                    item.DelimeterPlatform += token;
                  }
                  break;
                default:
                  item.EOF += token;
                  break;
              }
            }
          }

          if (tokensState != 2)
          {
            frame += EscapeBrace(token);
          }
        }

        if (tokensState != 2 && i < rows - 1)
        {
          frame += Environment.NewLine;
        }

        // 解析成功
        if (tokensState == 2 && tokenFlag >= 2)
        {
          // 没有默认语言跳出原始语言计算
          if (originLang == null && ORILANGRE.IsMatch(item.ID))
          {
            continue;
          }

          // 计算原始语言属性
          if (originLang != null && item.ID.StartsWith(tokenInID))
          {
            withOriginText = true;

            ValveLang selectedItem = (
              from it in langTokens
              where it.ID == item.ID.Remove(0, tokenInID.Length) && it.Platform == item.Platform
              select it
            ).SingleOrDefault();

            if (selectedItem != null)
            {
              selectedItem.Origin = item.Localized;
            }
          }
          else
          {
            langTokens.Add(item);
          }
        }
      }

      // 添加属性
      valveAst.Add("Language", lang);
      valveAst.Add("Tokens", langTokens);
      valveAst.Add("Frame", frame);
      valveAst.Add("WithOriginText", withOriginText);

      return valveAst;
    }
  }
}
