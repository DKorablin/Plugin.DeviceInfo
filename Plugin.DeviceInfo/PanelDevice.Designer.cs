namespace Plugin.DeviceInfo
{
	partial class PanelDevice
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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ToolStripButton tsbnRefresh;
			System.Windows.Forms.SplitContainer splitSmart;
			AlphaOmega.Windows.Forms.ContextMenuStripCopy cmsSmartCopy;
			this.lvSmart = new System.Windows.Forms.ListView();
			this.colSmartId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colSmartName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colSmartValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colSmartWorst = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colSmartThreshold = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colSmartRaw = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tsMain = new System.Windows.Forms.ToolStrip();
			this.ddlDrive = new System.Windows.Forms.ToolStripComboBox();
			this.tabMain = new System.Windows.Forms.TabControl();
			this.tabDisc = new System.Windows.Forms.TabPage();
			this.lvDisc = new Plugin.DeviceInfo.Controls.ReflectionListView();
			this.tabStorage = new System.Windows.Forms.TabPage();
			this.lvStorage = new Plugin.DeviceInfo.Controls.ReflectionListView();
			this.tabSmart = new System.Windows.Forms.TabPage();
			this.error = new System.Windows.Forms.ErrorProvider(this.components);
			tsbnRefresh = new System.Windows.Forms.ToolStripButton();
			splitSmart = new System.Windows.Forms.SplitContainer();
			cmsSmartCopy = new AlphaOmega.Windows.Forms.ContextMenuStripCopy();
			splitSmart.Panel1.SuspendLayout();
			splitSmart.SuspendLayout();
			this.tsMain.SuspendLayout();
			this.tabMain.SuspendLayout();
			this.tabDisc.SuspendLayout();
			this.tabStorage.SuspendLayout();
			this.tabSmart.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.error)).BeginInit();
			this.SuspendLayout();
			// 
			// tsbnRefresh
			// 
			tsbnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			tsbnRefresh.Image = global::Plugin.DeviceInfo.Properties.Resources.iconRefresh;
			tsbnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			tsbnRefresh.Name = "tsbnRefresh";
			tsbnRefresh.Size = new System.Drawing.Size(23, 30);
			tsbnRefresh.Text = "Refresh";
			tsbnRefresh.Click += new System.EventHandler(this.tsbnRefresh_Click);
			// 
			// splitSmart
			// 
			splitSmart.Dock = System.Windows.Forms.DockStyle.Fill;
			splitSmart.Location = new System.Drawing.Point(4, 5);
			splitSmart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			splitSmart.Name = "splitSmart";
			splitSmart.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitSmart.Panel1
			// 
			splitSmart.Panel1.AutoScroll = true;
			splitSmart.Panel1.Controls.Add(this.lvSmart);
			splitSmart.Panel2Collapsed = true;
			splitSmart.Size = new System.Drawing.Size(322, 356);
			splitSmart.SplitterDistance = 157;
			splitSmart.SplitterWidth = 6;
			splitSmart.TabIndex = 1;
			// 
			// lvSmart
			// 
			this.lvSmart.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSmartId,
            this.colSmartName,
            this.colSmartValue,
            this.colSmartWorst,
            this.colSmartThreshold,
            this.colSmartRaw});
			this.lvSmart.ContextMenuStrip = cmsSmartCopy;
			this.lvSmart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvSmart.FullRowSelect = true;
			this.lvSmart.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lvSmart.Location = new System.Drawing.Point(0, 0);
			this.lvSmart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.lvSmart.Name = "lvSmart";
			this.lvSmart.Size = new System.Drawing.Size(322, 356);
			this.lvSmart.TabIndex = 0;
			this.lvSmart.UseCompatibleStateImageBehavior = false;
			this.lvSmart.View = System.Windows.Forms.View.Details;
			// 
			// colSmartId
			// 
			this.colSmartId.Text = "ID";
			// 
			// colSmartName
			// 
			this.colSmartName.Text = "Name";
			// 
			// colSmartValue
			// 
			this.colSmartValue.Text = "Value";
			// 
			// colSmartWorst
			// 
			this.colSmartWorst.Text = "Worst";
			// 
			// colSmartThreshold
			// 
			this.colSmartThreshold.Text = "Threshold";
			// 
			// colSmartRaw
			// 
			this.colSmartRaw.Text = "Raw";
			// 
			// cmsSmartCopy
			// 
			cmsSmartCopy.Name = "cmsSmartCopy";
			cmsSmartCopy.Size = new System.Drawing.Size(127, 34);
			// 
			// tsMain
			// 
			this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ddlDrive,
            tsbnRefresh});
			this.tsMain.Location = new System.Drawing.Point(0, 0);
			this.tsMain.Name = "tsMain";
			this.tsMain.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
			this.tsMain.Size = new System.Drawing.Size(338, 33);
			this.tsMain.TabIndex = 0;
			// 
			// ddlDrive
			// 
			this.ddlDrive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlDrive.Name = "ddlDrive";
			this.ddlDrive.Size = new System.Drawing.Size(180, 33);
			this.ddlDrive.SelectedIndexChanged += new System.EventHandler(this.ddlDrive_SelectedIndexChanged);
			// 
			// tabMain
			// 
			this.tabMain.Controls.Add(this.tabDisc);
			this.tabMain.Controls.Add(this.tabStorage);
			this.tabMain.Controls.Add(this.tabSmart);
			this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabMain.Location = new System.Drawing.Point(0, 33);
			this.tabMain.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tabMain.Name = "tabMain";
			this.tabMain.SelectedIndex = 0;
			this.tabMain.Size = new System.Drawing.Size(338, 399);
			this.tabMain.TabIndex = 2;
			// 
			// tabDisc
			// 
			this.tabDisc.Controls.Add(this.lvDisc);
			this.tabDisc.Location = new System.Drawing.Point(4, 29);
			this.tabDisc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tabDisc.Name = "tabDisc";
			this.tabDisc.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tabDisc.Size = new System.Drawing.Size(330, 366);
			this.tabDisc.TabIndex = 1;
			this.tabDisc.Text = "Disc";
			this.tabDisc.UseVisualStyleBackColor = true;
			// 
			// lvDisc
			// 
			this.lvDisc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvDisc.FullRowSelect = true;
			this.lvDisc.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvDisc.HideSelection = false;
			this.lvDisc.Location = new System.Drawing.Point(4, 5);
			this.lvDisc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.lvDisc.Name = "lvDisc";
			this.lvDisc.Plugin = null;
			this.lvDisc.Size = new System.Drawing.Size(322, 356);
			this.lvDisc.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvDisc.TabIndex = 1;
			this.lvDisc.UseCompatibleStateImageBehavior = false;
			this.lvDisc.View = System.Windows.Forms.View.Details;
			// 
			// tabStorage
			// 
			this.tabStorage.Controls.Add(this.lvStorage);
			this.tabStorage.Location = new System.Drawing.Point(4, 29);
			this.tabStorage.Name = "tabStorage";
			this.tabStorage.Size = new System.Drawing.Size(330, 366);
			this.tabStorage.TabIndex = 2;
			this.tabStorage.Text = "Storage";
			this.tabStorage.UseVisualStyleBackColor = true;
			// 
			// lvStorage
			// 
			this.lvStorage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvStorage.FullRowSelect = true;
			this.lvStorage.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvStorage.HideSelection = false;
			this.lvStorage.Location = new System.Drawing.Point(0, 0);
			this.lvStorage.Name = "lvStorage";
			this.lvStorage.Plugin = null;
			this.lvStorage.Size = new System.Drawing.Size(330, 366);
			this.lvStorage.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvStorage.TabIndex = 0;
			this.lvStorage.UseCompatibleStateImageBehavior = false;
			this.lvStorage.View = System.Windows.Forms.View.Details;
			// 
			// tabSmart
			// 
			this.tabSmart.Controls.Add(splitSmart);
			this.tabSmart.Location = new System.Drawing.Point(4, 29);
			this.tabSmart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tabSmart.Name = "tabSmart";
			this.tabSmart.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tabSmart.Size = new System.Drawing.Size(330, 366);
			this.tabSmart.TabIndex = 0;
			this.tabSmart.Text = "SMART";
			this.tabSmart.UseVisualStyleBackColor = true;
			// 
			// error
			// 
			this.error.ContainerControl = this;
			// 
			// PanelDevice
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabMain);
			this.Controls.Add(this.tsMain);
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "PanelDevice";
			this.Size = new System.Drawing.Size(338, 432);
			splitSmart.Panel1.ResumeLayout(false);
			splitSmart.ResumeLayout(false);
			this.tsMain.ResumeLayout(false);
			this.tsMain.PerformLayout();
			this.tabMain.ResumeLayout(false);
			this.tabDisc.ResumeLayout(false);
			this.tabStorage.ResumeLayout(false);
			this.tabSmart.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.error)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStripComboBox ddlDrive;
		private Plugin.DeviceInfo.Controls.ReflectionListView lvDisc;
		private System.Windows.Forms.TabControl tabMain;
		private System.Windows.Forms.TabPage tabSmart;
		private System.Windows.Forms.TabPage tabDisc;
		private System.Windows.Forms.ListView lvSmart;
		private System.Windows.Forms.ColumnHeader colSmartId;
		private System.Windows.Forms.ColumnHeader colSmartName;
		private System.Windows.Forms.ColumnHeader colSmartValue;
		private System.Windows.Forms.ColumnHeader colSmartWorst;
		private System.Windows.Forms.ColumnHeader colSmartThreshold;
		private System.Windows.Forms.ColumnHeader colSmartRaw;
		private System.Windows.Forms.ErrorProvider error;
		private System.Windows.Forms.ToolStrip tsMain;
		private System.Windows.Forms.TabPage tabStorage;
		private Plugin.DeviceInfo.Controls.ReflectionListView lvStorage;
	}
}
