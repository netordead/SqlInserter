using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using log4net.Config;
using WeifenLuo.WinFormsUI;
using SQLObjects;
using log4net;


namespace SQLInsert
{
 
	/// <summary>
	/// Summary description for DataCreator.
	/// </summary>
	public class DataCreator : System.Windows.Forms.Form
	{
		private DeserializeDockContent m_deserializeDockContent;
		private TableForm _frmTables = new TableForm();
		private DBConnection _frmConnection = new DBConnection();
		private OptionForm _frmOption = new OptionForm();
		private DBCompare _frmConnectionCompare = new DBCompare();
		private DocForm _frmDoc = new DocForm();
		private WeifenLuo.WinFormsUI.DockPanel dockPanel;

		private System.Windows.Forms.StatusBar statBar;
		private System.Windows.Forms.ToolBar toolBar;
		private System.Windows.Forms.ToolBarButton toolBarButtonTables;
		private System.Windows.Forms.ToolBarButton toolBarButtonConnection;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ToolBarButton toolBarButtonDocForm;
		private System.Windows.Forms.ToolBarButton toolBarButtonCompare;
		private System.Windows.Forms.ToolBarButton toolBarButtonOption;
		private System.ComponentModel.IContainer components;

		Pen statusPen = new Pen(Color.Red);
		Pen okPen = new Pen(Color.Red);

		Pen bgPen = new Pen(Color.Silver);
		SolidBrush statusBrushFontBrush = new SolidBrush(Color.Red);
		SolidBrush okBrushFontBrush = new SolidBrush(Color.Green);
		
		SolidBrush bgBrush = new SolidBrush(Color.Silver);
		private System.Windows.Forms.StatusBarPanel msgPanel;
		private System.Windows.Forms.StatusBarPanel statDatabase;
		private System.Windows.Forms.StatusBarPanel statServer;

		private static readonly ILog log = LogManager.GetLogger(typeof(DataCreator));

		public static void Main()
		{

			Application.Run(new DataCreator());
		}
		private DockContent GetContentFromPersistString(string persistString)
		{

			if (persistString == typeof(TableForm).ToString())
				return _frmTables;
			else if (persistString == typeof(DBConnection).ToString())
				return _frmConnection;
			else if (persistString == typeof(DBCompare).ToString())
				return _frmConnectionCompare;
			else if (persistString == typeof(OptionForm).ToString())
				return _frmOption;
			else
				return null;
		}

		public DataCreator()
		{
			DOMConfigurator.Configure();

			m_deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
			InitializeComponent();
			_frmConnection.ChangedDBEvent+=new DBConnection.ChangedDBHandler( this.ChangeDB );
			_frmConnection.ChangedServerEvent += new DBConnection.ChangedServerHandler(this.ChangeServer);
			_frmTables.ScriptEvent+= new TableForm.ScriptHandler(HandleScript);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{

			if( disposing )
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			statusPen.Dispose();
			okPen.Dispose();
			this.statusBrushFontBrush.Dispose();
			this.okBrushFontBrush.Dispose();
			this.bgBrush.Dispose();
			this.bgPen.Dispose();

			base.Dispose( disposing );

		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DataCreator));
			this.statBar = new System.Windows.Forms.StatusBar();
			this.dockPanel = new WeifenLuo.WinFormsUI.DockPanel();
			this.toolBar = new System.Windows.Forms.ToolBar();
			this.toolBarButtonTables = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonConnection = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonDocForm = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonCompare = new System.Windows.Forms.ToolBarButton();
			this.toolBarButtonOption = new System.Windows.Forms.ToolBarButton();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.msgPanel = new System.Windows.Forms.StatusBarPanel();
			this.statDatabase = new System.Windows.Forms.StatusBarPanel();
			this.statServer = new System.Windows.Forms.StatusBarPanel();
			((System.ComponentModel.ISupportInitialize)(this.msgPanel)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statDatabase)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statServer)).BeginInit();
			this.SuspendLayout();
			// 
			// statBar
			// 
			this.statBar.Location = new System.Drawing.Point(0, 400);
			this.statBar.Name = "statBar";
			this.statBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																					   this.msgPanel,
																					   this.statDatabase,
																					   this.statServer});
			this.statBar.ShowPanels = true;
			this.statBar.Size = new System.Drawing.Size(536, 22);
			this.statBar.TabIndex = 0;
			this.statBar.DrawItem += new System.Windows.Forms.StatusBarDrawItemEventHandler(this.statBar_DrawItem);
			// 
			// dockPanel
			// 
			this.dockPanel.ActiveAutoHideContent = null;
			this.dockPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dockPanel.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World, ((System.Byte)(0)));
			this.dockPanel.Location = new System.Drawing.Point(0, 0);
			this.dockPanel.Name = "dockPanel";
			this.dockPanel.Size = new System.Drawing.Size(536, 400);
			this.dockPanel.TabIndex = 0;
			// 
			// toolBar
			// 
			this.toolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																					   this.toolBarButtonTables,
																					   this.toolBarButtonConnection,
																					   this.toolBarButtonDocForm,
																					   this.toolBarButtonCompare,
																					   this.toolBarButtonOption});
			this.toolBar.DropDownArrows = true;
			this.toolBar.ImageList = this.imageList;
			this.toolBar.Location = new System.Drawing.Point(0, 0);
			this.toolBar.Name = "toolBar";
			this.toolBar.ShowToolTips = true;
			this.toolBar.Size = new System.Drawing.Size(536, 42);
			this.toolBar.TabIndex = 7;
			this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
			// 
			// toolBarButtonTables
			// 
			this.toolBarButtonTables.ImageIndex = 2;
			this.toolBarButtonTables.Text = "Tables";
			this.toolBarButtonTables.ToolTipText = "Choose Tables";
			// 
			// toolBarButtonConnection
			// 
			this.toolBarButtonConnection.ImageIndex = 4;
			this.toolBarButtonConnection.Text = "Connect";
			this.toolBarButtonConnection.ToolTipText = "Connect to Database";
			// 
			// toolBarButtonDocForm
			// 
			this.toolBarButtonDocForm.ImageIndex = 0;
			this.toolBarButtonDocForm.Text = "SQL";
			this.toolBarButtonDocForm.ToolTipText = "SQL Script";
			// 
			// toolBarButtonCompare
			// 
			this.toolBarButtonCompare.ImageIndex = 9;
			this.toolBarButtonCompare.Text = "Compare";
			this.toolBarButtonCompare.ToolTipText = "Compare with second Database";
			// 
			// toolBarButtonOption
			// 
			this.toolBarButtonOption.ImageIndex = 5;
			this.toolBarButtonOption.Text = "Options";
			// 
			// imageList
			// 
			this.imageList.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// msgPanel
			// 
			this.msgPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.msgPanel.Style = System.Windows.Forms.StatusBarPanelStyle.OwnerDraw;
			this.msgPanel.Width = 341;
			// 
			// statDatabase
			// 
			this.statDatabase.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.statDatabase.Text = "Select Database";
			this.statDatabase.Width = 97;
			// 
			// statServer
			// 
			this.statServer.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Contents;
			this.statServer.Text = "Select Server";
			this.statServer.Width = 82;
			// 
			// DataCreator
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(536, 422);
			this.Controls.Add(this.toolBar);
			this.Controls.Add(this.dockPanel);
			this.Controls.Add(this.statBar);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "DataCreator";
			this.Text = "SQL Insert Scripter";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.DataCreator_Load);
			this.Closed += new System.EventHandler(this.DataCreator_Closed);
			((System.ComponentModel.ISupportInitialize)(this.msgPanel)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statDatabase)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statServer)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion



		private void SaveAppSettings()
		{

			string filename=System.Reflection.Assembly.GetExecutingAssembly().Location + @"\matinsert.xml";
			try
			{
				Utility.SaveAppSettings(filename , Global.AppSettings);
			}
			catch(System.Exception ex)
			{
				throw ex;
			}
	
		}
		/// <summary>
		/// Load Settings for all servers and dbs
		/// </summary>
		private void LoadAppSettings()
		{
			string filename = Application.StartupPath + @"/matinsert.xml";
			
			if(!File.Exists(filename))
			{
				Global.AppSettings = new ApplicationSetting();
				SaveAppSettings();
			}
			
			try
			{
				Global.AppSettings = Utility.LoadAppSettings(filename);
				this._frmConnection.SetUIFromSettings(Global.RefreshMode.Application);
			}
			catch(System.Exception ex)
			{
				this.statBar.Text = ex.ToString();
				Global.AppSettings = new ApplicationSetting();
			}

		}

		private void DataCreator_Load(object sender, System.EventArgs e)
		{
			//Loads serialized settings data
			try
			{

				LoadAppSettings();

				string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");

				if (File.Exists(configFile))
					dockPanel.LoadFromXml(configFile, m_deserializeDockContent);
				_frmTables.Show(dockPanel);

				_frmConnection.ControlModeDBConnection = DBConnection.ControlMode.DataBaseConnection;
				_frmConnection.Show(dockPanel);
				_frmConnectionCompare.ControlModeDBConnection = DBConnection.ControlMode.CompareControl;

				EnableButtons(false);
				ShowForms(false);
				
				this.toolBarButtonConnection.Enabled = true;
				this._frmConnection.Show();

				this.WriteSuccess("Succesfully Loaded Application. You are lucky today Dude! Please connect to a Database now.");

			}
			catch(System.Exception ex)
			{
				log.Error(ex);
				this.WriteError("Could not Load Application");
			}
		}


		private void menuItemToolbox_Click()
		{
			
			if( _frmConnection.IsHidden)
			{
				_frmConnection.Show(dockPanel ,  DockState.Float);
			}
			else
			{
				_frmConnection.Hide();	
			}
		}
		private void menuItemDocForm_Click()
		{
			
			if( _frmDoc.IsHidden)
			{
				_frmDoc.Show(dockPanel ,  DockState.Float);
			}
			else
			{
				_frmDoc.Hide();	
			}
		}
		private void menuItemCompare_Click()
		{
			if( _frmConnectionCompare.IsHidden)
			{
				_frmConnectionCompare.Show(dockPanel ,  DockState.Float);
			}
			else
			{
				_frmConnectionCompare.Hide();	
			}
		}
		private void menuItemOption_Click()
		{
			///HACK no idea why it is disposed when closed
			if( _frmOption.IsDisposed )
			{
				_frmOption = new OptionForm();
				_frmOption.Show(dockPanel , DockState.Float);
				return;
			}

			if( _frmOption.IsHidden)
			{
				
				_frmOption.Show(dockPanel ,  DockState.Float);
			}
			else
			{
				_frmOption.Hide();	
			}
		}

		private void EnableButtons(bool enable )
		{
			toolBarButtonOption.Enabled = enable;
			toolBarButtonConnection.Enabled = enable;
			toolBarButtonDocForm.Enabled = enable;
			toolBarButtonCompare.Enabled= enable;
			toolBarButtonOption.Enabled= enable;
			toolBarButtonTables.Enabled = enable;
		}

		private void ShowForms(bool show )
		{
			if(show)
			{
				this._frmConnection.Show();
				this._frmConnectionCompare.Show();
				this._frmDoc.Show();
				this._frmOption.Show();
				this._frmTables.Show();
			}
			else
			{
				this._frmConnection.Hide();
				this._frmConnectionCompare.Hide();
				this._frmDoc.Hide();
				this._frmOption.Hide();
				this._frmTables.Hide();
			}


		}

		private void menuItemSolutionExplorer_Click()
		{
			
			if( _frmTables.IsHidden)
			{
				_frmTables.Show(dockPanel ,  DockState.Float);
			}
			else
			{
				_frmTables.Hide();	
			}
		}


		private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{

			if (e.Button == toolBarButtonConnection)
				menuItemToolbox_Click();
			else if (e.Button == toolBarButtonTables)
				menuItemSolutionExplorer_Click();
			else if (e.Button == toolBarButtonDocForm)
				menuItemDocForm_Click();
			else if (e.Button == toolBarButtonCompare)
				menuItemCompare_Click();
			else if (e.Button == toolBarButtonOption)
				menuItemOption_Click();

		}

		private void DataCreator_Closed(object sender, System.EventArgs e)
		{
			SaveAppSettings();
			string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
			dockPanel.SaveAsXml(configFile);
		}




		private void ChangeServer(object sender,  ChangedDBArgs e)
		{
			this.statBar.Panels[2].Text = e.ServerName;
			this.statBar.Invalidate();
		}

		private void ChangeDB(object sender,  ChangedDBArgs e)
		{
			try
			{
				Global.DBSetting = Global.ServSettings.GetDBSettings(e.DBName,true);

				this._frmOption.Show();
				this._frmOption.SetUIFromSettings(Global.RefreshMode.DataBase);
				this._frmOption.statBar.Text = "Database set to " + e.DBName;

				this._frmTables.Show();
				this._frmTables.SetUIFromSettings(Global.RefreshMode.DataBase);
				this._frmTables.statBar.Text = "Database set to " + e.DBName;
				this.EnableButtons(true);
				this.WriteSuccess("You are connected to Database " + e.DBName );
				this.statBar.Panels[1].Text = e.DBName;
			}
			catch(System.Exception ex)
			{
				log.Error(ex);
				this.WriteError("There was an error trying to slect the Database");
			}
		}
		private void HandleScript(object sender,  ScriptArgs e)
		{
			try
			{
				_frmDoc.Show(dockPanel ,  DockState.Float);
				this._frmDoc.HandleScript(e.Script);
			}
			catch(System.Exception ex)
			{
				this.statBar.Text = ex.Message;	
			}
		}
		
		/// <summary>
		/// inform user about sucesfull operation
		/// </summary>
		/// <param name="msg"></param>
		private void WriteSuccess( string msg)
		{
			this.statBar.Text = msg;
			this.statBar.ForeColor = Color.Green;
			statBar.Invalidate();
		}

		/// <summary>
		/// inform user about errot
		/// </summary>
		/// <param name="msg"></param>
		private void WriteError( string msg)
		{
			this.statBar.Text = msg;
			this.statBar.ForeColor = Color.Red;
			statBar.Invalidate();
		}

		public void statBar_DrawItem(object sender, StatusBarDrawItemEventArgs sbdevent)
		{
			Graphics g = sbdevent.Graphics;
			StatusBar sb = (StatusBar)sender;
			RectangleF rectf = new RectangleF(sbdevent.Bounds.X, sbdevent.Bounds.Y, sbdevent.Bounds.Width, sbdevent.Bounds.Height);
			bgBrush.Color = sb.BackColor;
			statusPen.Color = sb.BackColor ;
			g.DrawRectangle(this.statusPen, sbdevent.Bounds);
			sbdevent.Graphics.FillRectangle(this.bgBrush , sbdevent.Bounds);
			statusBrushFontBrush.Color = sb.ForeColor;
			g.DrawString( sb.Text , sb.Font, this.statusBrushFontBrush, rectf);
		}
	}
}
