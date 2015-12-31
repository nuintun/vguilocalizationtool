using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VGUILocalizationTool.Valve
{
  class ValveParser
  {
    /// <summary>
    /// 语言占位符
    /// </summary>
    public static string LANGHOLDER = "%Lang%";
    private static ValveTokensParser Tokens = new ValveTokensParser();
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
    /// 字符串两端添加双引号
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private string QuotationWrap(string token)
    {
      if (token == null)
      {
        return string.Empty;
      }

      return "\"" + token + "\"";
    }

    /// <summary>
    /// 添加语言 Tokens 项
    /// </summary>
    /// <param name="tokens"></param>
    /// <param name="item"></param>
    /// <param name="originLanguage"></param>
    /// <param name="withOriginText"></param>
    private void AddTokensItem(List<ValveToken> tokens, ValveToken item, string originLanguage, ref bool withOriginText)
    {
      if (item != null)
      {
        if (item.ID != null && ORILANGRE.IsMatch(item.ID))
        {
          // 计算原始语言属性
          if (originLanguage != null)
          {
            // 字段中的原始语言标识
            string languageInID = "[" + originLanguage + "]";

            if (item.Localized != null && item.ID.StartsWith(languageInID))
            {
              withOriginText = true;

              ValveToken selectedItem = (
                from it in tokens
                where it.ID == item.ID.Remove(0, languageInID.Length) && it.Platform == item.Platform
                select it
              ).LastOrDefault();

              if (selectedItem != null)
              {
                selectedItem.Origin = item.Localized;
              }
            }
          }
        }
        else
        {
          if (item.ID != null)
          {
            ValveToken selectedItem = (
              from it in tokens
              where it.ID == item.ID && it.Platform == item.Platform
              select it
            ).LastOrDefault();

            if (selectedItem != null)
            {
              tokens.RemoveAt(tokens.IndexOf(selectedItem));
            }
          }

          tokens.Add(item);
        }
      }
    }

    /// <summary>
    /// 获取语法树
    /// </summary>
    /// <param name="source"></param>
    /// <param name="originLanguage"></param>
    /// <returns></returns>
    public ValveAST Parse(string source, string originLanguage = null)
    {
      string frame = null;
      string language = null;
      // Language 解析状态 - 0：未解析、1：正在解析、2：解析完毕
      int languageState = 0;
      // Tokens 解析状态 - 0：未解析、1：准备解析、2：正在解析、3：解析完毕
      int tokensState = 0;
      bool withOriginText = false;
      string[][] tokens = Tokens.GetTokens(source);
      // AST 状态
      ValveAST AST = new ValveAST();
      List<ValveToken> languageTokens = new List<ValveToken>();

      // 循环语法片断行
      for (int i = 0, rows = tokens.Length; i < rows; i++)
      {
        // Tokens 解析标识 - 0：解析字段、1：准备语言、2：解析平台、3：解析其他
        int tokenFlag = 0;
        ValveToken item = null;

        // 已进入语言列表区域初始化语言对象
        if (tokensState == 2)
        {
          item = new ValveToken();
        }

        // 循环语法片断列
        for (int j = 0, cols = tokens[i].Length; j < cols; j++)
        {
          string token = tokens[i][j];

          if (languageState < 2 && tokensState != 1 && tokensState != 2)
          {
            if (languageState == 0 && token == "\"Language\"")
            {
              languageState = 1;
            }
            else if (languageState == 1)
            {
              if (IsQuotationWrap(token))
              {
                languageState = 2;
                language = RemoveQuotationWrap(token);
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
              frame += "{{";

              if (j == cols - 1)
              {
                frame += Environment.NewLine;
              }
              else
              {
                item = new ValveToken();
              }

              frame += "{1}";
              continue;
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
              else
              {
                AddTokensItem(languageTokens, item, originLanguage, ref withOriginText);
              }

              frame += "}}";
              continue;
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
        if (tokensState == 2)
        {
          AddTokensItem(languageTokens, item, originLanguage, ref withOriginText);
        }
      }

      // 添加属性
      AST.Frame = frame;
      AST.Language = language;
      AST.Tokens = languageTokens;
      AST.WithOriginText = withOriginText;
      AST.OriginLanguage = originLanguage;

      return AST;
    }

    /// <summary>
    /// 由语法树生成代码
    /// </summary>
    /// <param name="AST"></param>
    /// <returns></returns>
    public string Stringify(ValveAST AST)
    {
      string tokens = null;

      for (int i = 0, len = AST.Tokens.Count; i < len; i++)
      {
        ValveToken token = AST.Tokens[i];

        tokens += token.BOF
          + QuotationWrap(token.ID)
          + token.DelimeterLocalized + QuotationWrap(token.Localized)
          + token.DelimeterPlatform + token.Platform
          + token.EOF;

        if (AST.WithOriginText && AST.OriginLanguage != null && token.ID != null && token.Origin != null)
        {
          tokens += Environment.NewLine + token.BOF
          + QuotationWrap("[" + AST.OriginLanguage + "]" + token.ID)
          + token.DelimeterLocalized + QuotationWrap(token.Origin)
          + token.DelimeterPlatform + token.Platform
          + token.EOF;
        }

        if (i < len - 1)
        {
          tokens += Environment.NewLine;
        }
      }

      if (AST.Frame == null)
      {
        return string.Empty;
      }
      else
      {
        return String.Format(AST.Frame, AST.Language, tokens);
      }
    }
  }
}
