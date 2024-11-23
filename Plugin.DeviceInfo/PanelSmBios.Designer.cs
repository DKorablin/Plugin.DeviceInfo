namespace Plugin.DeviceInfo
{
	partial class PanelSmBios
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
			this.tsMain = new System.Windows.Forms.ToolStrip();
			this.ddlTypes = new System.Windows.Forms.ToolStripComboBox();
			this.tsbnFileLoad = new System.Windows.Forms.ToolStripButton();
			this.tsbnFileSave = new System.Windows.Forms.ToolStripButton();
			this.lvTables = new Plugin.DeviceInfo.Controls.BiosTypeListView();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// tsMain
			// 
			this.tsMain.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ddlTypes,
            toolStripSeparator1,
            this.tsbnFileLoad,
            this.tsbnFileSave});
			this.tsMain.Location = new System.Drawing.Point(0, 0);
			this.tsMain.Name = "tsMain";
			this.tsMain.Size = new System.Drawing.Size(300, 28);
			this.tsMain.TabIndex = 0;
			// 
			// ddlTypes
			// 
			this.ddlTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlTypes.DropDownWidth = 300;
			this.ddlTypes.Name = "ddlTypes";
			this.ddlTypes.Size = new System.Drawing.Size(160, 28);
			this.ddlTypes.SelectedIndexChanged += new System.EventHandler(this.ddlTypes_SelectedIndexChanged);
			// 
			// toolStripSeparator1
			// 
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
			// 
			// tsbnFileLoad
			// 
			this.tsbnFileLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbnFileLoad.Image = global::Plugin.DeviceInfo.Properties.Resources.iconOpen;
			this.tsbnFileLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbnFileLoad.Name = "tsbnFileLoad";
			this.tsbnFileLoad.Size = new System.Drawing.Size(29, 25);
			this.tsbnFileLoad.Text = "Load";
			this.tsbnFileLoad.Click += new System.EventHandler(this.tsbnFileLoad_Click);
			// 
			// tsbnFileSave
			// 
			this.tsbnFileSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbnFileSave.Image = global::Plugin.DeviceInfo.Properties.Resources.FileSave;
			this.tsbnFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbnFileSave.Name = "tsbnFileSave";
			this.tsbnFileSave.Size = new System.Drawing.Size(29, 25);
			this.tsbnFileSave.Text = "Save";
			this.tsbnFileSave.Click += new System.EventHandler(this.tsbnFileSave_Click);
			// 
			// lvTables
			// 
			this.lvTables.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvTables.FullRowSelect = true;
			this.lvTables.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvTables.HideSelection = false;
			this.lvTables.Location = new System.Drawing.Point(0, 28);
			this.lvTables.Margin = new System.Windows.Forms.Padding(4);
			this.lvTables.Name = "lvTables";
			this.lvTables.Plugin = null;
			this.lvTables.Size = new System.Drawing.Size(300, 318);
			this.lvTables.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvTables.TabIndex = 1;
			this.lvTables.UseCompatibleStateImageBehavior = false;
			this.lvTables.View = System.Windows.Forms.View.Details;
			// 
			// PanelSmBios
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lvTables);
			this.Controls.Add(this.tsMain);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "PanelSmBios";
			this.Size = new System.Drawing.Size(300, 346);
			this.tsMain.ResumeLayout(false);
			this.tsMain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip tsMain;
		private Controls.BiosTypeListView lvTables;
		private System.Windows.Forms.ToolStripComboBox ddlTypes;
		private System.Windows.Forms.ToolStripButton tsbnFileLoad;
		private System.Windows.Forms.ToolStripButton tsbnFileSave;
	}
}
