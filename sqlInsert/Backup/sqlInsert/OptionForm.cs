using System;
using System.Collections;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Data;
using SQLObjects;
using WeifenLuo.WinFormsUI;
using log4net;


namespace SQLInsert
{
	/// <summary>
	/// Summary description for DummyPropertyGrid.
	/// </summary>
	public class OptionForm : BaseForm
    {
		private System.Windows.Forms.CheckBox chkDelete;
		private System.Windows.Forms.Label lblIdentityInsert;
		private System.Windows.Forms.CheckBox chkIdentity;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkScriptToFile;
        private System.Windows.Forms.TextBox txtFile;
		private System.Windows.Forms.Label label2;

		private System.Windows.Forms.GroupBox grpOptions;
        private System.ComponentModel.Container components = null;
		private static readonly ILog log = LogManager.GetLogger(typeof(OptionForm));

		public OptionForm()
		{

			InitializeComponent();


		}



		public void SetUIFromSettings(Global.RefreshMode mode)
		{
			chkIdentity.Checked = Global.DBSetting.IdentityInsert;
			chkDelete.Checked = Global.DBSetting.DeleteBeforeInsert;
			this.chkScriptToFile.Checked = Global.DBSetting.ScriptToFile;
			this.txtFile.Text = Global.DBSetting.FileNameResult;
		}



		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.chkScriptToFile = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkIdentity = new System.Windows.Forms.CheckBox();
            this.lblIdentityInsert = new System.Windows.Forms.Label();
            this.chkDelete = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.msgPanel)).BeginInit();
            this.grpOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // msgPanel
            // 
            this.msgPanel.Width = 1016;
            // 
            // statBar
            // 
            this.statBar.Location = new System.Drawing.Point(0, 622);
            this.statBar.Size = new System.Drawing.Size(1032, 22);
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.label2);
            this.grpOptions.Controls.Add(this.txtFile);
            this.grpOptions.Controls.Add(this.chkScriptToFile);
            this.grpOptions.Controls.Add(this.label1);
            this.grpOptions.Controls.Add(this.chkIdentity);
            this.grpOptions.Controls.Add(this.lblIdentityInsert);
            this.grpOptions.Controls.Add(this.chkDelete);
            this.grpOptions.Location = new System.Drawing.Point(5, 5);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(683, 355);
            this.grpOptions.TabIndex = 28;
            this.grpOptions.TabStop = false;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 23);
            this.label2.TabIndex = 39;
            this.label2.Text = "Include Delete Statement ";
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(176, 115);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(408, 20);
            this.txtFile.TabIndex = 38;
            this.txtFile.TextChanged += new System.EventHandler(this.txtFile_TextChanged);
            // 
            // chkScriptToFile
            // 
            this.chkScriptToFile.Location = new System.Drawing.Point(152, 115);
            this.chkScriptToFile.Name = "chkScriptToFile";
            this.chkScriptToFile.Size = new System.Drawing.Size(16, 24);
            this.chkScriptToFile.TabIndex = 37;
            this.chkScriptToFile.CheckedChanged += new System.EventHandler(this.chkScriptToFile_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 23);
            this.label1.TabIndex = 36;
            this.label1.Text = "Result to File";
            // 
            // chkIdentity
            // 
            this.chkIdentity.Location = new System.Drawing.Point(152, 81);
            this.chkIdentity.Name = "chkIdentity";
            this.chkIdentity.Size = new System.Drawing.Size(16, 24);
            this.chkIdentity.TabIndex = 35;
            this.chkIdentity.CheckedChanged += new System.EventHandler(this.chkIdentity_CheckedChanged);
            // 
            // lblIdentityInsert
            // 
            this.lblIdentityInsert.Location = new System.Drawing.Point(8, 81);
            this.lblIdentityInsert.Name = "lblIdentityInsert";
            this.lblIdentityInsert.Size = new System.Drawing.Size(136, 23);
            this.lblIdentityInsert.TabIndex = 34;
            this.lblIdentityInsert.Text = "set identity_insert on";
            // 
            // chkDelete
            // 
            this.chkDelete.Location = new System.Drawing.Point(152, 47);
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.Size = new System.Drawing.Size(24, 24);
            this.chkDelete.TabIndex = 33;
            this.chkDelete.CheckedChanged += new System.EventHandler(this.chkDelete_CheckedChanged);
            // 
            // OptionForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1032, 646);
            this.Controls.Add(this.grpOptions);
            this.DockableAreas = ((WeifenLuo.WinFormsUI.DockAreas)(((((WeifenLuo.WinFormsUI.DockAreas.Float | WeifenLuo.WinFormsUI.DockAreas.DockLeft)
                        | WeifenLuo.WinFormsUI.DockAreas.DockRight)
                        | WeifenLuo.WinFormsUI.DockAreas.DockTop)
                        | WeifenLuo.WinFormsUI.DockAreas.DockBottom)));
            this.HideOnClose = true;
            this.Name = "OptionForm";
            this.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.ShowHint = WeifenLuo.WinFormsUI.DockState.DockTopAutoHide;
            this.Text = "OptionForm";
            this.ToolTipText = "Choose which Tables and Columns to script";
            this.Resize += new System.EventHandler(this.Options_Resize);
            this.Controls.SetChildIndex(this.grpOptions, 0);
            this.Controls.SetChildIndex(this.statBar, 0);
            ((System.ComponentModel.ISupportInitialize)(this.msgPanel)).EndInit();
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion


	



		private void Options_Resize(object sender, System.EventArgs e)
		{
			this.grpOptions.Height =  this.Height -40;
			this.grpOptions.Width =  this.Width -10;

		}



		private void chkDelete_CheckedChanged(object sender, System.EventArgs e)
		{
			try
			{
				Global.DBSetting.DeleteBeforeInsert = chkDelete.Checked;
				this.statBar.Text = "";
			}
			catch(System.Exception ex )
			{
				this.statBar.ForeColor = Color.Red;
				this.statBar.Text = "Error setting Property";
				log.Error("Error in chkDelete_CheckedChanged",ex);
			}
		}

		private void chkIdentity_CheckedChanged(object sender, System.EventArgs e)
		{
			try
			{
				Global.DBSetting.IdentityInsert = chkIdentity.Checked;
				this.statBar.Text = "";
			}
			catch(System.Exception ex )
			{
				this.statBar.ForeColor = Color.Red;
				this.statBar.Text = "Error setting Property";
				log.Error("Error in chkIdentity_CheckedChanged",ex);
			}
		}


		private void chkScriptToFile_CheckedChanged(object sender, System.EventArgs e)
		{
			log.Debug("chkScriptToFile_CheckedChanged: chkScriptToFile.Checked=" + chkScriptToFile.Checked.ToString());
			Global.DBSetting.ScriptToFile = chkScriptToFile.Checked;
			if(chkScriptToFile.Checked)
			{
				txtFile.Text = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "sqlinsert.sql";
			}
		}

		private void txtFile_TextChanged(object sender, System.EventArgs e)
		{
			Global.DBSetting.FileNameResult = txtFile.Text;
		}



	}
}
