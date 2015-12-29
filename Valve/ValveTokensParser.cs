using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGUILocalizationTool.Valve
{
  class ValveTokensParser
  {
    /// <summary>
    /// 在行中添加语法片断
    /// </summary>
    /// <param name="lineTokens"></param>
    /// <param name="token"></param>
    private void AddLineTokens(LinkedList<string> lineTokens, StringBuilder token)
    {
      if (token.Length > 0)
      {
        lineTokens.AddLast(token.ToString());
        token.Clear();
      }
    }

    /// <summary>
    /// 获取语法片断
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public string[][] GetTokens(string source)
    {
      char ch = '\0';
      char pch = '\0';
      char nch = '\0';
      string line = null;
      bool isComment = false;
      bool isQuotation = false;
      LinkedList<string> lineTokens = null;
      StringBuilder token = new StringBuilder();
      StringReader sourceRead = new StringReader(source);
      LinkedList<string[]> tokens = new LinkedList<string[]>();

      // 逐行读取
      while ((line = sourceRead.ReadLine()) != null)
      {
        isComment = false;

        if (isQuotation)
        {
          token.Append(Environment.NewLine);
        }
        else
        {
          lineTokens = new LinkedList<string>();
        }

        for (int i = 0, len = line.Length; i < len; i++)
        {
          ch = line[i];

          if (isComment)
          {
            token.Append(ch);
            continue;
          }

          // 获取下一个字符
          nch = i + 1 < len ? line[i + 1] : '\0';

          if (!isQuotation && pch != '\\' && ch == '/' && nch == '/')
          {
            isComment = true;

            AddLineTokens(lineTokens, token);
            token.Append(ch);
          }
          else if (pch != '\\' && ch == '"')
          {
            if (isQuotation)
            {
              isQuotation = false;

              token.Append(ch);
              AddLineTokens(lineTokens, token);
            }
            else
            {
              isQuotation = true;

              AddLineTokens(lineTokens, token);
              token.Append(ch);
            }
          }
          else if (pch != '\\' && (ch == '{' || ch == '}'))
          {
            if (isQuotation)
            {
              token.Append(ch);
            }
            else
            {
              AddLineTokens(lineTokens, token);
              token.Append(ch);
              AddLineTokens(lineTokens, token);
            }
          }
          else if (pch != '\\' && (ch == '[' || ch == ']'))
          {
            if (isQuotation)
            {
              token.Append(ch);
            }
            else
            {
              if (ch == ']')
              {
                token.Append(ch);
              }

              AddLineTokens(lineTokens, token);

              if (ch == '[')
              {
                token.Append(ch);
              }
            }
          }
          else
          {
            token.Append(ch);
          }

          // 保存前一个字符
          pch = ch;
        }

        if (!isQuotation)
        {
          AddLineTokens(lineTokens, token);
          tokens.AddLast(lineTokens.ToArray());
        }
      }

      return tokens.ToArray();
    }
  }
}
