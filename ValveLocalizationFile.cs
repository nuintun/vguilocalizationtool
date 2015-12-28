using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace VGUILocalizationTool
{
  class ValveLocalizationFile
  {
    private int Pos { set; get; }
    private string FilePath { set; get; }
    private string FileName { set; get; }
    private string Language { set; get; }
    public bool WithOriginText { set; get; }
    public bool DontSaveNotLocalized { set; get; }
    private Valve.ValveAST AST { set; get; }
    private static Valve.ValveParser valveParser = new Valve.ValveParser();

    // 去处语言中的占位变量
    private static string LOCALTRIM = @"%[A-z]\d*|%[\w_]+%";

    // VGUI 解析和写入类
    public ValveLocalizationFile(string path)
    {
      StreamReader streamReader = new StreamReader(path);

      WithOriginText = false;

      this.FilePath = path;
      this.FileName = Path.GetFileNameWithoutExtension(path);
      this.Pos = this.FileName.LastIndexOf("_");
      this.AST = valveParser.Parse(streamReader.ReadToEnd());

      streamReader.Close();

      if (this.Pos >= 0)
      {
        this.Language = this.FileName.Substring(Pos + 1);
      }
      else
      {
        this.Language = string.Empty;
      }
    }

    /// <summary>
    /// 获取本地化文件名
    /// </summary>
    /// <param name="local"></param>
    /// <returns></returns>
    public string GetLocalFilePath(string local)
    {
      string ext = Path.GetExtension(this.FilePath);
      string localFile = (this.Pos >= 0 ? this.FileName.Remove(this.Pos) : this.FileName) + "_" + local + ext;

      return Path.GetDirectoryName(this.FilePath) + "\\" + localFile;
    }

    /// <summary>
    /// 是否已经进行本地化
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="local"></param>
    /// <returns></returns>
    public static bool Locolaized(string origin, string local)
    {
      if (origin == null || local == null)
      {
        return origin != local;
      }
      else
      {
        origin = Regex.Replace(origin.Trim(), LOCALTRIM, "");
        local = Regex.Replace(local.Trim(), LOCALTRIM, "");

        if (origin == "" && local == "")
        {
          return true;
        }
        else
        {
          return origin != local;
        }
      }
    }

    /// <summary>
    /// 数据读取
    /// </summary>
    /// <param name="local"></param>
    /// <returns></returns>
    public ValveLocalizationData ReadData(string local)
    {
      Valve.ValveAST localAST = null;
      string localPath = GetLocalFilePath(local);

      ValveLocalizationData data = new ValveLocalizationData();
      List<LocalizationData> list = new List<LocalizationData>();

      if (File.Exists(localPath))
      {
        StreamReader streamReader = new StreamReader(localPath);

        localAST = valveParser.Parse(streamReader.ReadToEnd(), this.Language);

        streamReader.Close();
      }
      else
      {
        localAST = new Valve.ValveAST();
      }

      this.WithOriginText = localAST.WithOriginText;

      for (int i = 0, len = this.AST.Tokens.Count; i < len; i++)
      {
        Valve.ValveToken localToken = null;
        Valve.ValveToken originToken = this.AST.Tokens[i];

        if (originToken.ID == null)
        {
          continue;
        }

        if (localAST.Tokens != null)
        {
          localToken = (
            from item in localAST.Tokens
            where originToken.ID == item.ID && originToken.Platform == item.Platform
            select item
          ).SingleOrDefault();
        }

        LocalizationData listItem = new LocalizationData();

        listItem.ID = originToken.ID;
        listItem.Origin = originToken.Localized;
        listItem.Platform = originToken.Platform;

        if (localToken == null)
        {
          listItem.Localized = originToken.Localized;
        }
        else
        {
          listItem.OriginOld = localToken.Origin;
          listItem.Localized = localToken.Localized != null ? localToken.Localized : originToken.Localized;
          listItem.OriginTextChanged = localToken.Origin == null ? false : localToken.Origin != originToken.Localized;
        }

        list.Add(listItem);
      }

      data.List = list;
      data.Language = local;
      data.WithOriginText = localAST.WithOriginText;

      return data;
    }

    /// <summary>
    /// 数据写入
    /// </summary>
    /// <param name="data"></param>
    public void WriteData(ValveLocalizationData data)
    {
      string localPath = GetLocalFilePath(data.Language);
      string localPathBak = localPath + ".bak";

      if (File.Exists(localPath))
      {
        if (File.Exists(localPathBak))
        {
          File.Delete(localPathBak);
        }

        File.Move(localPath, localPathBak);
      }


      Valve.ValveAST localAST = new Valve.ValveAST();
      List<Valve.ValveToken> tokens = new List<Valve.ValveToken>();
      StreamWriter streamWriter = new StreamWriter(localPath, false, System.Text.Encoding.Unicode);

      localAST.Language = data.Language;
      localAST.OriginLanguage = this.Language;
      localAST.Frame = this.AST.Frame;
      localAST.Tokens = tokens;
      localAST.WithOriginText = data.WithOriginText;

      for (int i = 0, len = this.AST.Tokens.Count; i < len; i++)
      {
        LocalizationData localData = null;
        Valve.ValveToken token = new Valve.ValveToken();
        Valve.ValveToken originToken = this.AST.Tokens[i];

        if (originToken.ID != null)
        {
          localData = (
            from item in data.List
            where originToken.ID == item.ID && originToken.Platform == item.Platform
            select item
          ).SingleOrDefault();
        }

        token.BOF = originToken.BOF;
        token.DelimeterLocalized = originToken.DelimeterLocalized;
        token.DelimeterPlatform = originToken.DelimeterPlatform;
        token.EOF = originToken.EOF;

        if (localData == null)
        {
          token.ID = originToken.ID;
          token.Platform = originToken.Platform;
          token.Origin = originToken.Origin;
          token.Localized = originToken.Localized;
        }
        else
        {
          token.ID = localData.ID;
          token.Platform = localData.Platform;
          token.Origin = localData.OriginTextChanged ? localData.OriginOld : localData.Origin;
          token.Localized = localData.Localized;
        }

        tokens.Add(token);
      }

      streamWriter.Write(valveParser.Stringify(localAST));
      streamWriter.Close();
    }
  }
}
