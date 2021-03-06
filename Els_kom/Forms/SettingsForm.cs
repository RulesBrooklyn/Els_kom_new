// Copyright (c) 2014-2020, Els_kom org.
// https://github.com/Elskom/
// All rights reserved.
// license: MIT, see LICENSE for more details.

namespace Els_kom.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Els_kom.Controls;
    using Elskom.Generic.Libs;

    internal partial class SettingsForm : /*Form*/ThemedForm
    {
        private string curvalue;
        private string curvalue2;
        private string curvalue3;
        private int curvalue4;
        private int curvalue5;
        private string curvalue6;
        private string label4 = "...";
        private string label5 = "...";
        private string label8 = "...";

        internal SettingsForm() => this.InitializeComponent();

        internal static string Label9 { get; private set; } = "0";

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            SettingsFile.Settingsxml?.ReopenFile();
            this.curvalue3 = SettingsFile.Settingsxml?.TryRead("ElsDir");
            this.curvalue = SettingsFile.Settingsxml?.TryRead("IconWhileElsNotRunning");
            this.curvalue2 = SettingsFile.Settingsxml?.TryRead("IconWhileElsRunning");
            this.curvalue6 = SettingsFile.Settingsxml?.TryRead("WindowIcon");
            if (!int.TryParse(SettingsFile.Settingsxml?.TryRead("LoadPDB"), out this.curvalue4))
            {
                // do nothing to silence a compile error.
            }

            if (!int.TryParse(SettingsFile.Settingsxml?.TryRead("SaveToZip"), out this.curvalue5))
            {
                // do nothing to silence a compile error.
            }

            var sources = SettingsFile.Settingsxml?.TryRead("Sources", "Source", null);
            foreach (var source in sources)
            {
                _ = this.ListView2.Items.Add(source);
            }

            this.TextBox1.Text = this.curvalue3;

            // set these to the values read above only if they are not empty.
            this.label4 = string.IsNullOrEmpty(this.curvalue) ? this.label4 : this.curvalue;
            this.label5 = string.IsNullOrEmpty(this.curvalue2) ? this.label5 : this.curvalue2;
            this.label8 = string.IsNullOrEmpty(this.curvalue6) ? this.label8 : this.curvalue6;
            this.CheckBox1.Checked = Convert.ToBoolean(this.curvalue5);
            this.CheckBox2.Checked = Convert.ToBoolean(this.curvalue4);
            var entries = new List<ListViewItem>();
            foreach (var callbackplugin in KOMManager.Callbackplugins)
            {
                entries.Add(new ListViewItem(
                    new string[]
                    {
                        callbackplugin.PluginName,
                    }, -1));
            }

            foreach (var komplugin in KOMManager.Komplugins)
            {
                entries.Add(new ListViewItem(
                    new string[]
                    {
                    komplugin.PluginName,
                    }, -1));
            }

            if (entries.Count > 3)
            {
                this.ColumnHeader1.Width -= 17;
            }

            this.ListView1.Items.AddRange(entries.ToArray());
            if (this.label4 == "0")
            {
                this.RadioButton1.Checked = true;
            }
            else if (this.label4 == "1")
            {
                this.RadioButton2.Checked = true;
            }
            else if (this.label4 == "2" || this.label4 == "...")
            {
                this.RadioButton3.Checked = true;
            }

            if (this.label5 == "0")
            {
                this.RadioButton4.Checked = true;
            }
            else if (this.label5 == "1" || this.label5 == "...")
            {
                this.RadioButton5.Checked = true;
            }
            else if (this.label5 == "2")
            {
                this.RadioButton6.Checked = true;
            }

            if (this.label8 == "0" || this.label8 == "...")
            {
                this.RadioButton7.Checked = true;
            }
            else if (this.label8 == "1")
            {
                this.RadioButton8.Checked = true;
            }
            else if (this.label8 == "2")
            {
                this.RadioButton9.Checked = true;
            }

            this.TreeView1.SelectedNode = this.TreeView1.Nodes[0];
            Label9 = "1";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog1 = new FolderBrowserDialog
            {
                Description = "Select the Folder that Your Elsword Install is in (Must be the one that either elsword.exe or voidels.exe is in).",
                RootFolder = Environment.SpecialFolder.MyComputer,
                ShowNewFolderButton = false,
            })
            {
                var res = folderBrowserDialog1.ShowDialog();
                if (res == DialogResult.OK)
                {
                    if (folderBrowserDialog1.SelectedPath.Length > 0)
                    {
                        this.TextBox1.Text = folderBrowserDialog1.SelectedPath;
                    }
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e) => this.Close();

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RadioButton1.Checked)
            {
                this.label4 = "0";
            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RadioButton2.Checked)
            {
                this.label4 = "1";
            }
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RadioButton3.Checked)
            {
                this.label4 = "2";
            }
        }

        private void RadioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RadioButton4.Checked)
            {
                this.label5 = "0";
            }
        }

        private void RadioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RadioButton5.Checked)
            {
                this.label5 = "1";
            }
        }

        private void RadioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RadioButton6.Checked)
            {
                this.label5 = "2";
            }
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.TreeView1.SelectedNode.Index == 0)
            {
                this.Panel3.Visible = true;
                this.Panel5.Visible = false;
                this.Panel6.Visible = false;
                _ = this.TreeView1.Focus();
            }
            else if (this.TreeView1.SelectedNode.Index == 1)
            {
                this.Panel3.Visible = false;
                this.Panel5.Visible = true;
                this.Panel6.Visible = false;
                _ = this.TreeView1.Focus();
            }
            else if (this.TreeView1.SelectedNode.Index == 2)
            {
                this.Panel3.Visible = false;
                this.Panel5.Visible = false;
                this.Panel6.Visible = true;
                _ = this.TreeView1.Focus();
            }
        }

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Label9 = "0";
            SettingsFile.Settingsxml?.ReopenFile();
            this.curvalue3 = SettingsFile.Settingsxml?.TryRead("ElsDir");
            this.curvalue = SettingsFile.Settingsxml?.TryRead("IconWhileElsNotRunning");
            this.curvalue2 = SettingsFile.Settingsxml?.TryRead("IconWhileElsRunning");
            if (!string.Equals(this.TextBox1.Text, this.curvalue3))
            {
                if (this.TextBox1.Text.Length > 0)
                {
                    SettingsFile.Settingsxml?.Write("ElsDir", this.TextBox1.Text);
                }
                else
                {
                    _ = MessageManager.ShowWarning("You Should Set a Working Elsword Directory.", "Warning!", Convert.ToBoolean(Convert.ToInt32(!string.IsNullOrEmpty(SettingsFile.Settingsxml?.TryRead("UseNotifications")) ? SettingsFile.Settingsxml?.TryRead("UseNotifications") : "0")));
                }
            }

            if (!string.Equals(this.label4, this.curvalue))
            {
                if (this.label5 == "...")
                {
                    SettingsFile.Settingsxml?.Write("IconWhileElsNotRunning", "2");
                }
                else
                {
                    SettingsFile.Settingsxml?.Write("IconWhileElsNotRunning", this.label4);
                }
            }

            if (!string.Equals(this.label5, this.curvalue2))
            {
                if (this.label5 == "...")
                {
                    SettingsFile.Settingsxml?.Write("IconWhileElsRunning", "1");
                }
                else
                {
                    SettingsFile.Settingsxml?.Write("IconWhileElsRunning", this.label5);
                }
            }

            if (!string.Equals(this.label8, this.curvalue6))
            {
                if (this.label8 == "...")
                {
                    SettingsFile.Settingsxml?.Write("WindowIcon", "0");
                }
                else
                {
                    SettingsFile.Settingsxml?.Write("WindowIcon", this.label8);
                }
            }

            SettingsFile.Settingsxml?.Write("LoadPDB", this.curvalue4.ToString());
            SettingsFile.Settingsxml?.Write("SaveToZip", this.curvalue5.ToString());
            var sources = new List<string>();
            for (var i = 0; i < this.ListView2.Items.Count; i++)
            {
                sources.Add(this.ListView2.Items[i].Text);
            }

            SettingsFile.Settingsxml?.Write("Sources", "Source", sources.ToArray());
            sources.Clear();

            // write to file.
            SettingsFile.Settingsxml?.Save();
        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (var i = 0; i < this.ListView1.SelectedItems.Count; i++)
            {
                var selitem = this.ListView1.SelectedItems[i];
                var found = false;
                foreach (var callbackplugin in KOMManager.Callbackplugins)
                {
                    if (callbackplugin.PluginName.Equals(selitem.Text))
                    {
                        if (callbackplugin.SupportsSettings)
                        {
                            found = true;
                        }
                    }
                }

                this.Button3.Enabled = !found ? false : true;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < this.ListView1.SelectedItems.Count; i++)
            {
                var selitem = this.ListView1.SelectedItems[i];
                foreach (var callbackplugin in KOMManager.Callbackplugins)
                {
                    if (callbackplugin.PluginName.Equals(selitem.Text))
                    {
                        using (var plugsettingfrm = (Form)callbackplugin.SettingsWindow)
                        {
                            plugsettingfrm.Icon = Icons.FormIcon;
                            _ = plugsettingfrm.ShowDialog(callbackplugin.ShowModal ? this : null);
                        }
                    }
                }
            }
        }

        private void Button5_Click(object sender, EventArgs e) => this.ListView2.Items.Add("Enter plugin source url here.");

        private void Button6_Click(object sender, EventArgs e)
        {
            if (this.ListView2.SelectedItems.Count > 0)
            {
                this.ListView2.SelectedItems[0].Remove();
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            using (var pluginsForm = new PluginsForm())
            {
                _ = pluginsForm.ShowDialog();
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e) => this.curvalue5 = Convert.ToInt32(this.CheckBox1.Checked);

        private void CheckBox2_CheckedChanged(object sender, EventArgs e) => this.curvalue4 = Convert.ToInt32(this.CheckBox2.Checked);

        // people say use a DataGridView because they cant hack together a solution.
        // well they were too stupid to hack a elegant soluion like this.
        private void ListView2_DoubleClick(object sender, EventArgs e)
        {
            var textBox = new TextBox
            {
                Bounds = this.ListView2.SelectedItems[0].Bounds,
                Text = this.ListView2.SelectedItems[0].Text,
                Visible = true,
            };
            textBox.Enter += (s, s1) =>
            {
                // so the user can replace all the text in the listview item.
                textBox.SelectAll();
            };
            textBox.Leave += (s, e1) =>
            {
                this.ListView2.SelectedItems[0].Text = textBox.Text;

                // do not forget to dispose this control.
                this.ListView2.Controls.Remove(textBox);
                textBox.Dispose();
            };
            textBox.KeyDown += (s, s1) =>
            {
                s1.Handled = true;
                if (s1.KeyData == Keys.Enter)
                {
                    this.ListView2.SelectedItems[0].Text = textBox.Text;

                    // do not forget to dispose this control.
                    this.ListView2.Controls.Remove(textBox);
                    textBox.Dispose();
                }
                else if (s1.KeyData == Keys.Right)
                {
                    // remove selection.
                    textBox.Select(0, 0);
                }
                else if (s1.KeyData == Keys.Left)
                {
                    // remove selection.
                    textBox.Select(0, 0);
                }
            };
            ShareXResources.ApplyDarkThemeToControl(textBox);
            this.ListView2.Controls.Add(textBox);
            this.ActiveControl = textBox;
        }

        private void RadioButton7_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RadioButton7.Checked)
            {
                this.label8 = "0";
            }
        }

        private void RadioButton8_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RadioButton8.Checked)
            {
                this.label8 = "1";
            }
        }

        private void RadioButton9_CheckedChanged(object sender, EventArgs e)
        {
            if (this.RadioButton9.Checked)
            {
                this.label8 = "2";
            }
        }
    }
}
