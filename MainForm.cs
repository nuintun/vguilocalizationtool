using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace VGUILocalizationTool
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
    }

    private void btnOpen_Click(object sender, EventArgs e)
    {
      if (openOriginFile.ShowDialog() == DialogResult.OK)
      {
        if (cbLocal.Text != "")
        {
          Properties.Settings.Default.DefLang = cbLocal.Text;
        }

        tbOrigin.Text = openOriginFile.FileName;

        string path = Path.GetDirectoryName(tbOrigin.Text);

        openOriginFile.InitialDirectory = path;

        string ext = Path.GetExtension(tbOrigin.Text);
        string originName = Path.GetFileNameWithoutExtension(tbOrigin.Text);
        int pos = originName.LastIndexOf("_");

        pos = pos == -1 ? -1 : pos + 1;

        var allfiles =
            from s in Directory.GetFiles(path + "\\", originName.Remove(pos) + "*" + ext)
            orderby s
            select Path.GetFileNameWithoutExtension(s);

        cbLocal.Items.Clear();

        foreach (string fileName in allfiles)
        {
          string fname = Path.GetFileNameWithoutExtension(fileName);

          if (fname != originName)
          {
            cbLocal.Items.Add(fname.Substring(pos));
          }
        }

        lblPerc.Text = "";
        localizationDataBindingSource.DataSource = new List<LocalizationData>();

        if (Properties.Settings.Default.DefLang != "")
        {
          int i = cbLocal.Items.IndexOf(Properties.Settings.Default.DefLang);

          if (i >= 0)
          {
            cbLocal.SelectedIndex = i;
          }
        }
      }
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      AddLocal local = new AddLocal();

      if (local.ShowDialog() == DialogResult.OK)
      {

        string tokens = local.tbLocal.Text.Trim();

        if (tokens != "")
        {
          cbLocal.Items.Add(tokens);
          cbLocal.SelectedIndex = cbLocal.Items.Count - 1;
        }
      }
    }

    ValveLocalizationFile file;

    bool Locolaized(string or, string lc)
    {
      if (lc == null)
      {
        return false;
      }
      else if (or.Length <= 2)
      {
        return true;
      }
      else if (or.Length == lc.Length)
      {
        if (or != lc)
        {
          return true;
        }
        else
        {
          foreach (char c in or)
          {
            if (Char.IsLetter(c))
            {
              return false;
            }
          }

          // skip text without letters
          return true;
        }
      }
      else
      {
        return true;
      }
    }

    private void cbLocal_SelectedIndexChanged(object sender, EventArgs e)
    {
      file = new ValveLocalizationFile(tbOrigin.Text);

      string originName = Path.GetFileNameWithoutExtension(tbOrigin.Text);
      string origin = originName.Substring(originName.LastIndexOf("_") + 1);

      List<LocalizationData> ori = file.ReadData(origin);
      List<LocalizationData> loc = file.ReadData((string)cbLocal.SelectedItem);

      cbSaveWithOrigin.Checked = file.WithOriginText;

      int tcount = 0;
      int lcount = 0;

      foreach (var or in ori)
      {
        if (or.ID == null)
        {
          continue;
        }

        tcount++;

        var lc = (from l in loc
                  where or.ID == l.ID
                  select l).SingleOrDefault();

        or.Origin = or.Localized;
        or.DelimeterOrigin = or.DelimeterLocalized;

        if (lc != null && lc.Localized != null)
        {
          or.OriginTextChanged = (lc.Origin != null && or.Origin != lc.Origin);
          or.OriginOld = lc.Origin;
          or.Localized = lc.Localized;
          or.UseSlashN = lc.UseSlashN;
          or.DelimeterLocalized = lc.DelimeterLocalized;

          if (Locolaized(or.Origin, lc.Localized))
          {
            lcount++;
          }
        }
      }

      if (tcount == 0)
      {
        tcount = 1;
      }

      lblPerc.Text = String.Format("{0:F}%", (1.0f * lcount / tcount) * 100);
      localizationDataBindingSource.DataSource = ori;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (file == null)
      {
        return;
      }

      List<LocalizationData> loc = (List<LocalizationData>)localizationDataBindingSource.DataSource;

      file.WithOriginText = cbSaveWithOrigin.Checked;
      file.DontSaveNotLocalized = cbDontSaveNotLocalized.Checked;
      file.WriteData((string)cbLocal.SelectedItem, loc);
    }

    private void btnNext_Click(object sender, EventArgs e)
    {
      LocalizationData data;
      do
      {
        localizationDataBindingSource.MoveNext();
        data = (LocalizationData)localizationDataBindingSource.Current;
      } while ((localizationDataBindingSource.Position + 1 < localizationDataBindingSource.Count) &&
          (data.ID == null || (!data.OriginTextChanged && Locolaized(data.Origin, data.Localized))));

      this.Invalidate();
    }

    private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
    {
      Properties.Settings.Default.InitialDir = openOriginFile.InitialDirectory;
      Properties.Settings.Default.FindText = dialog.tbText.Text;
      Properties.Settings.Default.IDDefault = dialog.rbID.Checked;
      Properties.Settings.Default.OriginDefault = dialog.rbEnglish.Checked;
      Properties.Settings.Default.LocalizedDefault = dialog.rbLocalized.Checked;

      if (cbLocal.Text != "")
      {
        Properties.Settings.Default.DefLang = cbLocal.Text;
      }

      Properties.Settings.Default.Save();
    }

    FindTextDialog dialog = new FindTextDialog();

    internal void FindNext()
    {
      string text = dialog.tbText.Text.ToLower();
      int ind = dialog.rbID.Checked ? 0 : dialog.rbEnglish.Checked ? 1 : 2;

      if (text != "")
      {
        LocalizationData data;
        bool gotoNext;

        do
        {
          localizationDataBindingSource.MoveNext();
          data = (LocalizationData)localizationDataBindingSource.Current;
          gotoNext = (localizationDataBindingSource.Position + 1 < localizationDataBindingSource.Count);
          if (gotoNext)
          {
            if (data.ID != null)
            {
              switch (ind)
              {
                case 0:
                  gotoNext = (data.ID.ToLower().IndexOf(text) == -1);
                  break;
                case 1:
                  gotoNext = (data.Origin.ToLower().IndexOf(text) == -1);
                  break;
                case 2:
                  gotoNext = (data.Localized.ToLower().IndexOf(text) == -1);
                  break;
              }
            }
          }
        } while (gotoNext);

        this.Invalidate();
      }
    }

    private void MainForm_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.Control && e.KeyCode == Keys.Q)
      {
        btnNext_Click(null, null);
      }
      else if (e.Control && e.KeyCode == Keys.F)
      {
        btFind_Click(null, null);
      }
      else if (e.KeyCode == Keys.F3)
      {
        FindNext();
      }
    }

    private void btFind_Click(object sender, EventArgs e)
    {
      dialog.mainForm = this;
      dialog.Show();
    }
  }
}
