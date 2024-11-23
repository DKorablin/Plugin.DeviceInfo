using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AlphaOmega.Debug;
using AlphaOmega.Debug.Native;
using AlphaOmega.Debug.Smb;
using Plugin.DeviceInfo.Controls;
using SAL.Flatbed;
using SAL.Windows;

namespace Plugin.DeviceInfo
{
	public partial class PanelSmBios : UserControl,IPluginSettings<PanelSmBiosSettings>
	{
		private PanelSmBiosSettings _settings;
		private FirmwareT<FirmwareSmBios> _tables;
		private FirmwareSmBios _bios;

		Object IPluginSettings.Settings { get => this.Settings; }

		public PanelSmBiosSettings Settings { get { return this._settings == null ? this._settings = new PanelSmBiosSettings() : this._settings; } }

		private FirmwareT<FirmwareSmBios> Tables
		{
			get => this._tables;
			set
			{
				this._bios = null;
				this._tables = value;
			}
		}
		private FirmwareSmBios Bios
		{
			get => this._bios ?? (this._bios = this.Tables.GetData().First());
		}

		private const String Caption = "System Management BIOS";

		private PluginWindows Plugin { get => (PluginWindows)this.Window.Plugin; }
		private IWindow Window { get => (IWindow)base.Parent; }

		public PanelSmBios()
			=> InitializeComponent();

		protected override void OnCreateControl()
		{
			this.Window.Caption = Caption;
			lvTables.Plugin = this.Plugin;

			base.OnCreateControl();

			this.FillTypes(this.Settings.FilePath);
		}

		private void tsbnFileLoad_Click(Object sender, EventArgs e)
		{
			using(OpenFileDialog dlg = new OpenFileDialog() { Filter = "System Firmware|*.sfw|All Files|*.*", })
				if(dlg.ShowDialog() == DialogResult.OK)
				{
					this.FillTypes(dlg.FileName);
					Baseboard baseboard = this.Bios.GetType<Baseboard>();
					this.Window.Caption = Caption + " - " + baseboard.Manufacturer + " " + baseboard.Product;
				}
		}

		private void tsbnFileSave_Click(Object sender, EventArgs e)
		{
			using(SaveFileDialog dlg = new SaveFileDialog() { Filter = "System Firmware|*.sfw|All Files|*.*", })
				if(dlg.ShowDialog() == DialogResult.OK)
					File.WriteAllBytes(dlg.FileName, this.Tables.Save());
		}

		private void FillTypes(String filePath)
		{
			this.Settings.FilePath = filePath;
			this.Tables = this.Settings.FilePath == null
				? new FirmwareT<FirmwareSmBios>()
				: new FirmwareT<FirmwareSmBios>(File.ReadAllBytes(filePath));

			lvTables.Clear();
			ddlTypes.Items.Clear();

			List<SmBios.Type> types = new List<SmBios.Type>();

			Int32 index = 1;
			lvTables.SuspendLayout();
			foreach(TypeBase type in this.Bios.Types)
			{
				String groupName = DdlSmBiosType.XmlReader.FindEnumDocumentation(type.Header.Type)
					?? $"Unknown ({type.Header.Type})";

				lvTables.AddItem(type, $"{index++}. {groupName}");
				if(!types.Contains(type.Header.Type))
					types.Add(type.Header.Type);
			}
			lvTables.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			lvTables.ResumeLayout(true);

			ddlTypes.Items.Add(new DdlSmBiosType());
			ddlTypes.Items.AddRange(types.Select(p => new DdlSmBiosType(p)).ToArray());
		}

		private void ddlTypes_SelectedIndexChanged(Object sender, EventArgs e)
		{
			lvTables.Items.Clear();
			DdlSmBiosType ddlItem = (DdlSmBiosType)ddlTypes.SelectedItem;

			Int32 index = 1;
			IEnumerable<TypeBase> types = ddlItem.Type == null
				? this.Bios.Types
				: this.Bios.Types.Where(p => p.Header.Type == ddlItem.Type);

			lvTables.SuspendLayout();
			foreach(TypeBase type in types)
			{
				String groupName = DdlSmBiosType.XmlReader.FindEnumDocumentation(type.Header.Type)
					?? $"Unknown ({type.Header.Type})";

				lvTables.AddItem(type, $"{index++}. {groupName}");
			}
			lvTables.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			lvTables.ResumeLayout(true);
		}
	}
}