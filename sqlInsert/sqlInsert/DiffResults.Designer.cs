namespace SQLInsert
{
    partial class DiffResults
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.lvSource = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.lvDestination = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // lvSource
            // 
            this.lvSource.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvSource.FullRowSelect = true;
            this.lvSource.HideSelection = false;
            this.lvSource.Location = new System.Drawing.Point(12, 17);
            this.lvSource.MultiSelect = false;
            this.lvSource.Name = "lvSource";
            this.lvSource.Size = new System.Drawing.Size(216, 278);
            this.lvSource.TabIndex = 0;
            this.lvSource.UseCompatibleStateImageBehavior = false;
            this.lvSource.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Line";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Text (Source)";
            this.columnHeader2.Width = 147;
            // 
            // lvDestination
            // 
            this.lvDestination.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4});
            this.lvDestination.FullRowSelect = true;
            this.lvDestination.HideSelection = false;
            this.lvDestination.Location = new System.Drawing.Point(249, 17);
            this.lvDestination.MultiSelect = false;
            this.lvDestination.Name = "lvDestination";
            this.lvDestination.Size = new System.Drawing.Size(272, 278);
            this.lvDestination.TabIndex = 2;
            this.lvDestination.UseCompatibleStateImageBehavior = false;
            this.lvDestination.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Line";
            this.columnHeader3.Width = 50;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Text (Destination)";
            this.columnHeader4.Width = 198;
            // 
            // DiffResults
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(533, 440);
            this.Controls.Add(this.lvDestination);
            this.Controls.Add(this.lvSource);
            this.Name = "DiffResults";
            this.Text = "Results";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvSource;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ListView lvDestination;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}