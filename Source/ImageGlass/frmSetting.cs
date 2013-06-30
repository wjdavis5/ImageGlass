﻿/*
ImageGlass Project - Image viewer for Windows
Copyright (C) 2013 DUONG DIEU PHAP
Project homepage: http://imageglass.org

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Principal;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ImageGlass
{
    public partial class frmSetting : Form
    {
        public frmSetting()
        {
            InitializeComponent();
        }

        private Color M_COLOR_MENU_ACTIVE = Color.FromArgb(255, 0, 123, 176);
        private Color M_COLOR_MENU_HOVER = Color.FromArgb(255, 0, 160, 220);
        private Color M_COLOR_MENU_NORMAL = Color.Silver;
        

        #region MOUSE ENTER - HOVER - DOWN MENU
        private void lblMenu_MouseDown(object sender, MouseEventArgs e)
        {
            Label lbl = (Label)sender;
            lbl.BackColor = M_COLOR_MENU_ACTIVE;
        }

        private void lblMenu_MouseUp(object sender, MouseEventArgs e)
        {
            Label lbl = (Label)sender;

            if (int.Parse(lbl.Tag.ToString()) == 1)
            {
                lbl.BackColor = Color.FromArgb(255, M_COLOR_MENU_ACTIVE.R + 20,
                                                M_COLOR_MENU_ACTIVE.G + 20,
                                                M_COLOR_MENU_ACTIVE.B + 20);
            }
            else
            {
                lbl.BackColor = M_COLOR_MENU_HOVER;
            }
        }

        private void lblMenu_MouseEnter(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;

            if (int.Parse(lbl.Tag.ToString()) == 1)
            {
                lbl.BackColor = Color.FromArgb(255, M_COLOR_MENU_ACTIVE.R + 20,
                                                M_COLOR_MENU_ACTIVE.G + 20,
                                                M_COLOR_MENU_ACTIVE.B + 20);
            }
            else
            {
                lbl.BackColor = M_COLOR_MENU_HOVER;
            }

        }

        private void lblMenu_MouseLeave(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            if (int.Parse(lbl.Tag.ToString()) == 1)
            {
                lbl.BackColor = M_COLOR_MENU_ACTIVE;
            }
            else
            {
                lbl.BackColor = M_COLOR_MENU_NORMAL;
            }
        }
        #endregion

        #region MOUSE ENTER - HOVER - DOWN BUTTON
        private void lblButton_MouseDown(object sender, MouseEventArgs e)
        {
            Label lbl = (Label)sender;
            lbl.BackColor = M_COLOR_MENU_ACTIVE;            
        }

        private void lblButton_MouseUp(object sender, MouseEventArgs e)
        {
            Label lbl = (Label)sender;
            lbl.BackColor = M_COLOR_MENU_HOVER;
        }

        private void lblButton_MouseEnter(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;
            lbl.BackColor = M_COLOR_MENU_HOVER;
        }

        private void lblButton_MouseLeave(object sender, EventArgs e)
        {
            Label lbl = (Label)sender; 
            lbl.BackColor = M_COLOR_MENU_NORMAL;            
        }
        #endregion


        private void frmSetting_Load(object sender, EventArgs e)
        {
            lblMenu_Click(lblGeneral, EventArgs.Empty);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSetting_SizeChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void lblMenu_Click(object sender, EventArgs e)
        {
            Label lbl = (Label)sender;

            lblGeneral.Tag = 0;
            lblContextMenu.Tag = 0;
            lblLanguage.Tag = 0;
            lblExtension.Tag = 0;

            lblGeneral.BackColor = M_COLOR_MENU_NORMAL;
            lblContextMenu.BackColor = M_COLOR_MENU_NORMAL;
            lblLanguage.BackColor = M_COLOR_MENU_NORMAL;
            lblExtension.BackColor = M_COLOR_MENU_NORMAL;

            lbl.Tag = 1;
            lbl.BackColor = M_COLOR_MENU_ACTIVE;

            if (lbl.Name == "lblGeneral")
            {
                tab1.SelectedTab = tabGeneral;
                LoadTabGeneralConfig();
            }
            else if (lbl.Name == "lblContextMenu")
            {
                tab1.SelectedTab = tabContextMenu;
                txtExtensions.Text = Setting.ContextMenuExtensions;
            }
            else if (lbl.Name == "lblLanguage")
            {
                tab1.SelectedTab = tabLanguage;
            }
            else if (lbl.Name == "lblExtension")
            {
                tab1.SelectedTab = tabExtension;
            }
        }


        #region TAB GENERAL
        private void chkLockWorkspace_CheckedChanged(object sender, EventArgs e)
        {
            Setting.IsLockWorkspaceEdges = chkLockWorkspace.Checked;
        }

        private void chkAutoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoUpdate.Checked)
            {
                Setting.SetConfig("AutoUpdate", chkAutoUpdate.Checked.ToString());
            }
            else
            {
                Setting.SetConfig("AutoUpdate", "0");
            }
        }

        private void chkFindChildFolder_CheckedChanged(object sender, EventArgs e)
        {
            Setting.SetConfig("Recursive", chkFindChildFolder.Checked.ToString());
        }

        private void chkHideToolBar_CheckedChanged(object sender, EventArgs e)
        {
            Setting.IsHideToolBar = chkHideToolBar.Checked;
            Setting.SetConfig("IsHideToolbar", Setting.IsHideToolBar.ToString());
        }

        private void cmbZoomOptimization_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbZoomOptimization.SelectedIndex == 1)
            {
                Setting.ZoomOptimizationMethod = ZoomOptimizationValue.SmoothPixels;
            }
            else if (cmbZoomOptimization.SelectedIndex == 2)
            {
                Setting.ZoomOptimizationMethod = ZoomOptimizationValue.ClearPixels;
            }
            else
            {
                Setting.ZoomOptimizationMethod = ZoomOptimizationValue.Auto;
            }
        }

        private void chkWelcomePicture_CheckedChanged(object sender, EventArgs e)
        {
            Setting.IsWelcomePicture = chkWelcomePicture.Checked;
        }

        private void barInterval_Scroll(object sender, EventArgs e)
        {
            Setting.SetConfig("Interval", barInterval.Value.ToString());
            lblSlideshowInterval.Text = "Slide show interval: " + barInterval.Value.ToString();
        }

        private void numMaxThumbSize_ValueChanged(object sender, EventArgs e)
        {
            Setting.SetConfig("MaxThumbnailFileSize", numMaxThumbSize.Value.ToString());
        }

        private void cmbImageOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            Setting.SetConfig("ImageLoadingOrder", cmbImageOrder.SelectedIndex.ToString());
            Setting.LoadImageOrderConfig();
        }

        /// <summary>
        /// Get and load value of General tab
        /// </summary>
        private void LoadTabGeneralConfig()
        {
            //Get value of chkLockWorkspace
            chkLockWorkspace.Checked = bool.Parse(Setting.GetConfig("LockToEdge", "true"));

            //Get value of chkFindChildFolder
            chkFindChildFolder.Checked = bool.Parse(Setting.GetConfig("Recursive", "false"));

            //Get value of cmbAutoUpdate
            string s = Setting.GetConfig("AutoUpdate", "true");
            if (s != "0")
            {
                chkAutoUpdate.Checked = true;
            }
            else
            {
                chkAutoUpdate.Checked = false;
            }

            //Get value of chkWelcomePicture
            chkWelcomePicture.Checked = bool.Parse(Setting.GetConfig("Welcome", "true"));

            //Get value of chkHideToolBar
            chkHideToolBar.Checked = bool.Parse(Setting.GetConfig("IsHideToolbar", "false"));

            //Get value of cmbZoomOptimization
            s = Setting.GetConfig("ZoomOptimize", "0");
            int i = 0;
            if (int.TryParse(s, out i))
            {
                if (-1 < i && i < cmbZoomOptimization.Items.Count)
                {}
                else
                {
                    i = 0;
                }
            }
            cmbZoomOptimization.SelectedIndex = i;

            //Get value of barInterval
            i = int.Parse(Setting.GetConfig("Interval", "5"));
            if (0 < i && i < 61)
            {
                barInterval.Value = i;
            }
            else
            {
                barInterval.Value = 5;
            }

            lblSlideshowInterval.Text = "Slide show interval: " + barInterval.Value.ToString();

            //Get value of numMaxThumbSize
            s = Setting.GetConfig("MaxThumbnailFileSize", "1");
            i = 1;
            if (int.TryParse(s, out i))
            {}
            numMaxThumbSize.Value = i;

            //Get value of cmbImageOrder
            s = Setting.GetConfig("ImageLoadingOrder", "0");
            i = 0;
            if (int.TryParse(s, out i))
            {
                if (-1 < i && i < cmbImageOrder.Items.Count)
                { }
                else
                {
                    i = 0;
                }
            }
            cmbImageOrder.SelectedIndex = i;

            //Get background color
            picBackgroundColor.BackColor = Setting.BackgroundColor;
        }
        #endregion


        #region TAB CONTEXT MENU
        private void lblAddDefaultContextMenu_Click(object sender, EventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = Setting.StartUpDir + "igtasks.exe";
            p.StartInfo.Arguments = "addext " + //name of param
                                    "\"" + Application.ExecutablePath + "\" " + //arg 1
                                    "\"" + Setting.SupportedExtensions + "\" "; //arg 2
            p.EnableRaisingEvents = true;
            p.Exited += p_Exited;

            try
            {
                p.Start();
            }
            catch { }

        }

        private void lblUpdateContextMenu_Click(object sender, EventArgs e)
        {
            //Update context menu
            Process p = new Process();
            p.StartInfo.FileName = Setting.StartUpDir + "igtasks.exe";
            p.StartInfo.Arguments = "updateext " + //name of param
                                    "\"" + Application.ExecutablePath + "\" " + //arg 1
                                    "\"" + txtExtensions.Text.Trim() + "\" "; //arg 2
            p.EnableRaisingEvents = true;
            p.Exited += p_Exited;

            try
            {
                p.Start();
            }
            catch { }
        }

        private void lblRemoveAllContextMenu_Click(object sender, EventArgs e)
        {
            //Remove all context menu
            Process p = new Process();
            p.StartInfo.FileName = Setting.StartUpDir + "igtasks.exe";
            p.StartInfo.Arguments = "removeext ";
            p.EnableRaisingEvents = true;
            p.Exited += p_Exited;

            try
            {
                p.Start();
            }
            catch { }

            txtExtensions.Text = Setting.ContextMenuExtensions;
        }

        void p_Exited(object sender, EventArgs e)
        {
            txtExtensions.Text = Setting.ContextMenuExtensions;
        }

        #endregion





        private void frmSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            Setting.IsForcedActive = true;
        }

        private void lnkGetMoreLanguage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.imageglass.org/languages.php" +
                    "?utm_source=imageglass&utm_medium=language_click&utm_campaign=from_app_" +
                    Application.ProductVersion.Replace(".", "_"));
            }
            catch { }
        }

        private void picBackgroundColor_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            c.AllowFullOpen = true;

            if (c.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                picBackgroundColor.BackColor = c.Color;
                Setting.BackgroundColor = c.Color;

                //Luu background color
                Setting.SetConfig("BackgroundColor", Setting.BackgroundColor.ToArgb().ToString());
            }
        }

        

        

        

        





    }
}