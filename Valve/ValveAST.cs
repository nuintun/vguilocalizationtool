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
    private static ValveTokens Tokens = new ValveTokens();
    private static Regex ORILANGRE = new Regex(@"^\[[\w_]+\]");

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
      string BOF = null;
      string EOF = null;
      string lang = null;
      string tokenInID = null;
      bool isInLanguage = false;
      bool isPreEnterTokens = false;
      bool isEnterTokens = false;
      bool isLeaveTokens = false;
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
        int pos = 0;
        ValveLang item = null;

        // 已进入语言列表区域初始化语言对象
        if (isEnterTokens)
        {
          item = new ValveLang();
        }

        // 循环语法片断列
        for (int j = 0, cols = tokens[i].Length; j < cols; j++)
        {
          string token = tokens[i][j];

          // 进入语言列表区域之前
          if (!isEnterTokens && !isLeaveTokens)
          {
            if (token == "\"Tokens\"")
            {
              isPreEnterTokens = true;
            }
            else if (token == "{" && isPreEnterTokens)
            {
              isEnterTokens = true;

              if (j == cols - 1)
              {
                BOF += token + Environment.NewLine;
              }
            }
            else if (token == "\"Language\"")
            {
              isInLanguage = true;
            }
            else if (isInLanguage && IsQuotationWrap(token))
            {
              isInLanguage = false;
              BOF += "\"{0}\"";
              lang = token;
            }
            else
            {
              BOF += token;
            }
          }
          // 已进入语言列表区域
          else if (isEnterTokens && !isLeaveTokens)
          {
            string trimToken = token.Trim();

            // 离开语言列表区域
            if (token == "}")
            {
              isLeaveTokens = true;

              if (j == cols - 1)
              {
                EOF += Environment.NewLine;
              }
            }
            else
            {
              // 解析语法
              switch (pos)
              {
                // 字段
                case 0:
                  if (IsQuotationWrap(token))
                  {
                    pos++;
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
                    pos++;
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
                    pos++;
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
          // 离开语言列表区域
          else
          {
            EOF += token;
          }
        }

        // 进入语言列表区域之前
        if (!isEnterTokens && !isLeaveTokens)
        {
          BOF += Environment.NewLine;
        }
        // 进入语言列表区域
        else if (isEnterTokens && !isLeaveTokens)
        {
          // 解析成功
          if (pos >= 2)
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
        // 离开语言列表区域
        else
        {
          if (i < rows - 1)
          {
            EOF += Environment.NewLine;
          }
        }
      }

      // 添加属性
      valveAst.Add("BOF", BOF);
      valveAst.Add("Lang", lang);
      valveAst.Add("LangTokens", langTokens);
      valveAst.Add("EOF", EOF);
      valveAst.Add("WithOriginText", withOriginText);

      return valveAst;
    }
  }
}
