// *****************************************************************************
// 
//  Copyright 2003, Weifen Luo
//  All rights reserved. The software and associated documentation 
//  supplied hereunder are the proprietary information of Weifen Luo
//  and are supplied subject to licence terms.
// 
//  DockSample Application 1.0
// *****************************************************************************

using System;
using WeifenLuo.WinFormsUI;
using System.Threading;
using SQLObjects;
using System.Collections;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;


namespace SQLInsert
{
	/// <summary>
	/// Summary description for DummyPropertyGrid.
	/// </summary>
	public class DBConnection : BaseForm
	{
		delegate void BooleanCallback(Boolean enable);
		BooleanCallback enableControls=null;

        //delegate void ListCallback(SqlAdmin.SqlDatabaseCollection dbs);
        //ListCallback setList=null;

        delegate void ListCallback1( DatabaseCollection dbs);
        ListCallback1 setList1 = null;

		delegate void StringCallback(string msg);
		StringCallback setMsg=null;

		public delegate void ChangedDBHandler(object sender, ChangedDBArgs oa);
		public event ChangedDBHandler ChangedDBEvent;

		public delegate void ChangedServerHandler(object sender, ChangedDBArgs oa);
		public event ChangedServerHandler ChangedServerEvent;

		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtUser;
		private System.Windows.Forms.CheckBox chkSQLLogin;
		private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cmbDatabases;
		private System.Windows.Forms.Label lblDatabases;
		private System.Windows.Forms.Label lblServer;
		private System.Windows.Forms.ComboBox cmbServer;
		//protected System.Windows.Forms.StatusBar statBar;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox grpLogin;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		public enum ControlMode{DataBaseConnection,CompareControl};
		

		public ControlMode ControlModeDBConnection;

		public DBConnection()
		{
			enableControls = new BooleanCallback(EnableControls);
			//setList = new ListCallback(SetList);
			setMsg = new StringCallback(SetMsg);
            setList1 = new ListCallback1(SetList1);
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();


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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DBConnection));
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtUser = new System.Windows.Forms.TextBox();
			this.chkSQLLogin = new System.Windows.Forms.CheckBox();
			this.btnConnect = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbDatabases = new System.Windows.Forms.ComboBox();
			this.lblDatabases = new System.Windows.Forms.Label();
			this.lblServer = new System.Windows.Forms.Label();
			this.cmbServer = new System.Windows.Forms.ComboBox();
			this.grpLogin = new System.Windows.Forms.GroupBox();
			//this.statBar = new System.Windows.Forms.StatusBar();
			this.label3 = new System.Windows.Forms.Label();
			this.grpLogin.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(96, 56);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(192, 20);
			this.txtPassword.TabIndex = 20;
			this.txtPassword.Text = "";
			// 
			// txtUser
			// 
			this.txtUser.Location = new System.Drawing.Point(96, 24);
			this.txtUser.Name = "txtUser";
			this.txtUser.Size = new System.Drawing.Size(192, 20);
			this.txtUser.TabIndex = 19;
			this.txtUser.Text = "";
			// 
			// chkSQLLogin
			// 
			this.chkSQLLogin.Location = new System.Drawing.Point(96, 80);
			this.chkSQLLogin.Name = "chkSQLLogin";
			this.chkSQLLogin.Size = new System.Drawing.Size(136, 24);
			this.chkSQLLogin.TabIndex = 24;
			this.chkSQLLogin.Text = "Use SQL Login";
			// 
			// btnConnect
			// 
			this.btnConnect.Location = new System.Drawing.Point(8, 248);
			this.btnConnect.Name = "btnConnect";
			this.btnConnect.Size = new System.Drawing.Size(148, 23);
			this.btnConnect.TabIndex = 23;
			this.btnConnect.Text = "Connect";
			this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(76, 24);
			this.label1.TabIndex = 22;
			this.label1.Text = "Password";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(76, 24);
			this.label2.TabIndex = 21;
			this.label2.Text = "User";
			// 
			// cmbDatabases
			// 
			this.cmbDatabases.Location = new System.Drawing.Point(8, 304);
			this.cmbDatabases.Name = "cmbDatabases";
			this.cmbDatabases.Size = new System.Drawing.Size(248, 21);
			this.cmbDatabases.TabIndex = 18;
			this.cmbDatabases.SelectedIndexChanged += new System.EventHandler(this.cmbDatabases_SelectedIndexChanged);
			// 
			// lblDatabases
			// 
			this.lblDatabases.Location = new System.Drawing.Point(8, 280);
			this.lblDatabases.Name = "lblDatabases";
			this.lblDatabases.Size = new System.Drawing.Size(76, 24);
			this.lblDatabases.TabIndex = 17;
			this.lblDatabases.Text = "Databases";
			// 
			// lblServer
			// 
			this.lblServer.Location = new System.Drawing.Point(8, 16);
			this.lblServer.Name = "lblServer";
			this.lblServer.Size = new System.Drawing.Size(112, 16);
			this.lblServer.TabIndex = 16;
			this.lblServer.Text = "Server";
			// 
			// cmbServer
			// 
			this.cmbServer.Location = new System.Drawing.Point(8, 40);
			this.cmbServer.Name = "cmbServer";
			this.cmbServer.Size = new System.Drawing.Size(256, 21);
			this.cmbServer.TabIndex = 15;
			this.cmbServer.SelectedIndexChanged += new System.EventHandler(this.cmbServer_SelectedIndexChanged);
			// 
			// grpLogin
			// 
			this.grpLogin.Controls.Add(this.label1);
			this.grpLogin.Controls.Add(this.txtPassword);
			this.grpLogin.Controls.Add(this.txtUser);
			this.grpLogin.Controls.Add(this.chkSQLLogin);
			this.grpLogin.Controls.Add(this.label2);
			this.grpLogin.Location = new System.Drawing.Point(8, 80);
			this.grpLogin.Name = "grpLogin";
			this.grpLogin.Size = new System.Drawing.Size(304, 136);
			this.grpLogin.TabIndex = 25;
			this.grpLogin.TabStop = false;
			this.grpLogin.Text = "Login - optional";
			// 
			// statBar
			// 
//			this.statBar.Location = new System.Drawing.Point(0, 414);
//			this.statBar.Name = "statBar";
//			this.statBar.Size = new System.Drawing.Size(904, 22);
//			this.statBar.TabIndex = 26;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 224);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(184, 24);
			this.label3.TabIndex = 27;
			this.label3.Text = "Connect via SQL login or Windows";
			// 
			// DBConnection
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(904, 438);
			this.Controls.Add(this.label3);
			//this.Controls.Add(this.statBar);
			this.Controls.Add(this.grpLogin);
			this.Controls.Add(this.btnConnect);
			this.Controls.Add(this.cmbDatabases);
			this.Controls.Add(this.lblDatabases);
			this.Controls.Add(this.lblServer);
			this.Controls.Add(this.cmbServer);
			this.DockableAreas = ((WeifenLuo.WinFormsUI.DockAreas)(((((WeifenLuo.WinFormsUI.DockAreas.Float | WeifenLuo.WinFormsUI.DockAreas.DockLeft) 
				| WeifenLuo.WinFormsUI.DockAreas.DockRight) 
				| WeifenLuo.WinFormsUI.DockAreas.DockTop) 
				| WeifenLuo.WinFormsUI.DockAreas.DockBottom)));
			this.DockPadding.Bottom = 2;
			this.DockPadding.Top = 2;
			this.HideOnClose = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(200, 450);
			this.Name = "DBConnection";
			this.ShowHint = WeifenLuo.WinFormsUI.DockState.DockTopAutoHide;
			this.Text = "Database Connection";
			this.Resize += new System.EventHandler(this.DBConnection_Resize);
			this.Load += new System.EventHandler(this.DBConnection_Load);
			this.grpLogin.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion



		private void btnConnect_Click(object sender, System.EventArgs e)
		{
			
			if(cmbServer.Text=="") 
			{
				this.WriteError( "No Server Selected");
				return;
			}
			
			if(Global.AppSettings.HasSetting(cmbServer.Text))
			{
				Global.ServSettings = Global.AppSettings.GetServerSetting(cmbServer.Text.ToLower(),false);
			}
			else
			{
				Global.ServSettings = new SQLObjects.ServerSetting(cmbServer.Text.ToLower());
				Global.AppSettings.AddServerSetting(cmbServer.Text.ToLower());
			}



            EnableControls(false);


            WaitCallback async = new WaitCallback(Connect);

            Connection conn = new Connection();

            if (this.ControlModeDBConnection == ControlMode.DataBaseConnection)
            {
                conn.IsCompare = false;
            }
            else
            {
                conn.IsCompare = true;
            }

            conn.Server = this.cmbServer.Text;
            conn.DB = this.cmbDatabases.Text;
            conn.SqlPwd = this.txtPassword.Text;
            conn.SqlUser = this.txtUser.Text;
            conn.UseSQL = this.chkSQLLogin.Checked;

            ThreadPool.QueueUserWorkItem(async, conn);
		}


        void SetList1(Microsoft.SqlServer.Management.Smo.DatabaseCollection dbs)
        {

            if (InvokeRequired)
            {
                BeginInvoke(setList1, new Object[] { dbs });
                return;
            }
            ChangedDBArgs arg = new ChangedDBArgs();
            arg.ServerName = Global.ServSettings.Name;
            if (ChangedServerEvent != null)
            {
                ChangedServerEvent(this, arg);
            }
            foreach (Database db in dbs)
            {
                this.cmbDatabases.Items.Add(db.Name);
            }

        }

		void SetMsg(string msg) 
		{
			// EnableControls makes sure that it is being called on 
			// the GUI thread using InvokeRequired and BeginInvoke
			if (InvokeRequired) 
			{ 
				BeginInvoke( setMsg , new Object[]{msg});  
				return;
			}
			if(msg.StartsWith("ERROR:"))
			{
				this.WriteError(msg.Replace("ERROR:",""));	
			}
			else
			{
				this.WriteSuccess( msg);
			}
		}

        /// <summary>
        /// Connects to Server
        /// </summary>
        /// <param name="param"></param>
		void Connect(Object param) 
		{

			Connection conn = (Connection)param;
			
			if(conn.UseSQL)
			{
                ServerConnection servConn = new ServerConnection();

                servConn.DatabaseName = conn.DB;
                servConn.ServerInstance = conn.Server;
                servConn.LoginSecure = false;
                servConn.Login = conn.SqlUser;
                servConn.Password = conn.SqlPwd;
                Server server = new Server(servConn);
                
				if(!conn.IsCompare)
				{
                    Global.Serv1 = server;
				}
				else
				{
                    Global.ServCompare1 = server;
				}
			}
			else
			{
                ServerConnection servConn = new ServerConnection();
                servConn.ServerInstance = conn.Server;

				if(!conn.IsCompare)
				{
                    Global.Serv1 = new Server(servConn);	
				}
				else
				{
                    Global.ServCompare1 = new Server(servConn);	
				}
			}
			try
			{
				//SqlAdmin.SqlDatabaseCollection dbs = null;
				if(!conn.IsCompare)
				{
					Global.AppSettings.SelectedDB = conn.DB;
				}
				else
				{
					Global.AppSettings.SelectedDBCompare = conn.DB;
				}
			

                SetList1(Global.Serv1.Databases);
				SetMsg("Connected");
			}
			catch(System.Exception ex)
			{
				SetMsg("ERROR:"+ ex.Message);
				return;
			}
			finally
			{

				EnableControls(true);
			}
		}
		void EnableControls(Boolean enable) 
		{
			
			// EnableControls makes sure that it is being called on 
			// the GUI thread using InvokeRequired and BeginInvoke
			if (InvokeRequired) 
			{ 
				BeginInvoke(enableControls, new Object[]{enable});      
				return;
			}
			btnConnect.Enabled = enable;  

		}
		private void cmbServer_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}

		public void SetUIFromSettings(Global.RefreshMode mode)
		{
			// select db from setting
			if(mode == Global.RefreshMode.Application)
			{
				//names of servers
				ArrayList _db = Global.AppSettings.GetSettings();
				cmbServer.DataSource = _db;
				if(Global.AppSettings.SelectedDB!=null)
				{
					int _index = cmbServer.FindStringExact(Global.AppSettings.SelectedDB);
					if(_index > -1)
					{
						cmbServer.SelectedIndex = _index;
					}
				}
			}
			
			// no db selected in setting
			if(cmbDatabases.Text=="") return;
			else
			{
				Global.AppSettings.SelectedDB = cmbDatabases.Text;
			}

			//if settings exist for db apply them
			Global.DBSetting = Global.ServSettings.GetDBSettings(Global.SelectedDB,true);

		}

        /// <summary>
        /// User selected DEB from Combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void cmbDatabases_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.ControlModeDBConnection == ControlMode.DataBaseConnection)
			{
				Global.SelectedDB = cmbDatabases.Items[cmbDatabases.SelectedIndex].ToString() ;
				try
				{
					ChangedDBEvent(this , new ChangedDBArgs(Global.SelectedDB));
					Global.IsConnected = true;
				}
				catch(System.Exception ex)
				{
					this.WriteError( ex.Message);	
				}
			}
			else
			{
				Global.SelectedDBCompare = cmbDatabases.Items[cmbDatabases.SelectedIndex].ToString() ;
			}
		}

		private void DBConnection_Load(object sender, System.EventArgs e)
		{
			
		}

		private void DBConnection_Resize(object sender, System.EventArgs e)
		{
			this.grpLogin.Width = this.Width - 20;

			this.txtPassword.Width = grpLogin.Width - 110 ;
			this.txtUser.Width = txtPassword.Width  ;

			cmbDatabases.Width = this.Width - 20;
			this.cmbServer.Width =  cmbDatabases.Width  ;
			
		}





	}
}
