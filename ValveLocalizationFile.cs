using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace VGUILocalizationTool
{
  class ValveLocalizationFile
  {
    private int pos { get; set; }
    private string file { get; set; }
    private string tokens { get; set; }
    private string filename { get; set; }
    private bool WithoutLang { get; set; }
    public bool WithOriginText { get; set; }
    public bool DontSaveNotLocalized { get; set; }

    // VGUI 解析和写入类
    public ValveLocalizationFile(string file)
    {
      WithoutLang = false;
      WithOriginText = false;

      this.file = file;
      this.filename = Path.GetFileNameWithoutExtension(file);

      this.pos = this.filename.LastIndexOf("_");

      if (pos != -1)
      {
        this.tokens = this.filename.Substring(pos + 1);
      }
      else
      {
        this.tokens = String.Empty;
      }
    }

    // 获取本地化文件名
    private string GetLocalFileName(string local)
    {
      string ext = Path.GetExtension(this.file);
      string localFile = this.filename.Remove(this.pos) + "_" + local + ext;

      return Path.GetDirectoryName(this.file) + "\\" + localFile;
    }

    // 解析引擎
    string[] SplitWithQuotas(string str, ref bool unterm)
    {
      char o = '\0';
      bool q = false;
      bool s = false;

      StringBuilder sb = new StringBuilder();
      LinkedList<string> list = new LinkedList<string>();

      for (int i = 0; i < str.Length; i++)
      {
        char c = str[i];

        if (!q)
        {
          if (c == '\"' && o != '\\')
          {
            if (sb.Length > 0)
            {
              list.AddLast(sb.ToString());
              sb.Length = 0;
            }

            q = true;
            s = false;
          }
          else if (c != ' ' && c != '\t')
          {
            if (s && sb.Length > 0)
            {
              list.AddLast(sb.ToString());
              sb.Length = 0;
            }

            sb.Append(c);
            s = false;
          }
          else
          {
            if (!s && sb.Length > 0)
            {
              list.AddLast(sb.ToString());
              sb.Length = 0;
            }

            sb.Append(c);
            s = true;
          }
        }
        else
        {
          if (c == '\"' && o != '\\')
          {
            q = false;
            list.AddLast(sb.ToString());
            sb.Length = 0;
          }
          else
          {
            sb.Append(c);
          }
        }

        o = c;
      }

      if (q || sb.Length != 0)
      {
        list.AddLast(sb.ToString());
      }

      unterm = q;

      return list.ToArray();
    }

    // 跳过空白
    private int SkipEmpty(string[] tokens, int start)
    {
      for (int i = start; i < tokens.Length; i++)
      {
        if (tokens[i].Trim().Length > 0)
        {
          return i;
        }
      }

      return -1;
    }

    // 数据读取
    public List<LocalizationData> ReadData(string local = null)
    {
      if (local == null)
      {
        local = tokens;
      }

      int l = 0;
      string fileName = GetLocalFileName(local);
      List<LocalizationData> data = new List<LocalizationData>();

      WithoutLang = false;
      WithOriginText = false;

      if (File.Exists(fileName))
      {
        String line;
        StreamReader sr = new StreamReader(fileName);

        while ((line = sr.ReadLine()) != null)
        {
          bool ML = false;
          bool unterm = false;
          string[] tokens = SplitWithQuotas(line, ref unterm);

          if (unterm)
          {
            bool q = false;
            int idx = tokens.Length - 1;

            do
            {
              line = sr.ReadLine();

              if (line == null)
              {
                break;
              }

              ML = true;

              if (line.Length > 0)
              {
                for (int ci = line.Length - 1; ci >= 0; ci--)
                {
                  if (line[ci] == ' ' || line[ci] == '\t')
                  {
                    continue;
                  }
                  else if (line[ci] == '\"' && (ci == 0 || line[ci - 1] != '\\'))
                  {
                    q = true;
                    tokens[idx] += Environment.NewLine + line.Substring(0, ci);
                    break;
                  }
                  else
                  {
                    q = false;
                    tokens[idx] += Environment.NewLine + line;
                    break;
                  }
                }
              }
              else
              {
                tokens[idx] += Environment.NewLine + line;
              }
            } while (!q);
          }

          int i = SkipEmpty(tokens, 0);

          switch (l)
          {
            case 0:
              if (i == -1 || tokens[i].StartsWith("//"))
              {
                continue;
              }

              if (tokens[i] == "lang")
              {
                l++;
                WithoutLang = false;
              }
              else if (tokens[i] == "Language")
              {
                l = 3;
                WithoutLang = true;
              }
              break;
            case 1:
              if (i == -1 || tokens[i].StartsWith("//"))
              {
                continue;
              }

              if (tokens[i] == "{")
              {
                l++;
              }
              break;
            case 2:
              if (i == -1 || tokens[i].StartsWith("//"))
              {
                continue;
              }

              if (tokens[i] == "Language")
              {
                l++;
              }
              break;
            case 3:
              if (i == -1 || tokens[i].StartsWith("//"))
              {
                continue;
              }

              if (tokens[i] == "Tokens")
              {
                l++;
              }
              break;
            case 4:
              if (i == -1 || tokens[i].StartsWith("//"))
              {
                continue;
              }

              if (tokens[i] == "{")
              {
                l++;
              }
              break;
            case 5:
              LocalizationData cd;

              if (i == -1 || tokens[i].StartsWith("//"))
              {
                cd = new LocalizationData();

                if (tokens.Length > 0)
                {
                  cd.DelimeterID = line;
                }

                data.Add(cd);
                continue;
              }

              if (tokens[i] == "}")
              {
                l++;
                continue;
              }

              // all work
              int j = i + 2;
              string s = tokens[i];
              bool ori = s.StartsWith("[" + this.tokens + "]");


              if (ori)
              {
                s = s.Remove(0, 9);
              }

              cd = (from d in data
                    where d.ID == s
                    select d).SingleOrDefault();

              if (cd == null)
              {
                cd = new LocalizationData();

                if (i > 0)
                {
                  cd.DelimeterID = tokens[0];
                }

                cd.ID = s;
                data.Add(cd);
              }

              if (j < tokens.Length)
              {
                if (tokens[j].IndexOf("\\n") >= 0)
                {
                  cd.UseSlashN = true;
                  tokens[j] = tokens[j].Replace("\\n", Environment.NewLine);
                }
                else
                {
                  cd.UseSlashN = (ML ? false : true);
                }

                tokens[j] = tokens[j].Replace("\\\"", "\"");

                if (ori)
                {
                  WithOriginText = true;
                  cd.Origin = tokens[j];
                  cd.DelimeterOrigin = tokens[j - 1];
                }
                else
                {
                  cd.Localized = tokens[j];
                  cd.DelimeterLocalized = tokens[j - 1];
                }
              }
              break;
            case 6:
              if (i == -1 || tokens[i].StartsWith("//"))
              {
                continue;
              }

              if (tokens[i] == "{")
              {
                l++;
              }
              break;
          }
        }

        sr.Close();
      }

      return data;
    }

    // 数据写入
    public void WriteData(string local, List<LocalizationData> data)
    {
      string fileName = GetLocalFileName(local);
      string fileNameBak = fileName + ".bak";

      if (File.Exists(fileNameBak))
      {
        File.Delete(fileNameBak);
      }

      if (File.Exists(fileName))
      {
        File.Move(fileName, fileNameBak);
      }

      StreamWriter sw = new StreamWriter(fileName, false, System.Text.Encoding.Unicode);

      if (!WithoutLang)
      {
        sw.WriteLine("\"lang\"");
        sw.WriteLine("{");
      }

      sw.WriteLine(String.Format("\"Language\" \"{0}\" ", local));
      sw.WriteLine("\"Tokens\"");
      sw.WriteLine("{");

      foreach (var d in data)
      {
        if (d.ID != null)
        {
          if (DontSaveNotLocalized && d.Origin == d.Localized)
          {
            continue;
          }

          string loc;

          if (!d.UseSlashN)
          {
            loc = d.Localized;
          }
          else
          {
            loc = d.Localized.Replace(Environment.NewLine, "\\n");
          }

          loc = loc.Replace("\"", "\\\"");

          string line = String.Format("{0}\"{1}\"{2}\"{3}\"", d.DelimeterID, d.ID, d.DelimeterLocalized, loc);

          sw.WriteLine(line);

          if (WithOriginText)
          {
            string ori;

            if (!d.OriginTextChanged || d.OriginOld == null)
            {
              ori = d.Origin;
            }
            else
            {
              ori = d.OriginOld;
            }

            if (d.UseSlashN)
            {
              ori = ori.Replace(Environment.NewLine, "\\n");
            }

            ori = ori.Replace("\"", "\\\"");
            line = String.Format("{0}\"[{1}]{2}\"{3}\"{4}\"", d.DelimeterID, this.tokens, d.ID, d.DelimeterOrigin, ori);

            sw.WriteLine(line);
          }
        }
        else
        {
          sw.WriteLine(d.DelimeterID);
        }
      }

      sw.WriteLine("}");

      if (!WithoutLang)
      {
        sw.WriteLine("}");
      }

      sw.Close();
    }
  }
}
