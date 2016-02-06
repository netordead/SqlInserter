using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;
using SQLObjects;
using System.Collections;


namespace SqlInserterWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


        }

        private void SaveAppSettings()
        {

            string filename = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"/matinsert.xml";

            try
            {
                Utility.SaveAppSettings(filename, Global.AppSettings);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        public void SetUIFromSettings(SQLObjects.Global.RefreshMode mode)
        {
            // select db from setting
            if (mode == Global.RefreshMode.Application)
            {
                //names of servers
                List<ServerSetting> servers = Global.AppSettings.GetSettings();
                cmbServer.DataContext = servers;
                if (Global.AppSettings.SelectedDB != null)
                {
                    cmbServer.SelectedValue = Global.AppSettings.SelectedDB;
                }
            }

            //// no db selected in setting
            if (cmbServer.SelectedValue=="") return;
            else
            {
                Global.AppSettings.SelectedDB = cmbServer.SelectedValue.ToString();
            }

            //if settings exist for db apply them
            Global.DBSetting = Global.ServSettings.GetDBSettings(Global.SelectedDB, true);

        }

        /// <summary>
        /// Load Settings for all servers and dbs
        /// </summary>
        private void LoadAppSettings()
        {


            string filename = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location )+ @"/matinsert.xml";

            if (!File.Exists(filename))
            {
                Global.AppSettings = new ApplicationSetting();
                SaveAppSettings();
            }

            try
            {
                Global.AppSettings = Utility.LoadAppSettings(filename);
                SetUIFromSettings(Global.RefreshMode.Application);
            }
            catch (System.Exception ex)
            {
                ///TODO Fehler anzeigen
                Global.AppSettings = new ApplicationSetting();
            }

        }


   

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            base.DataContext = new DataBaseList();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAppSettings();
        }



 
    }
}
