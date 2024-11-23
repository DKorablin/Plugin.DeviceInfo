using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using AlphaOmega.Debug;
using SAL.Windows;

namespace Plugin.DeviceInfo
{
	internal partial class PanelDevice : UserControl
	{
		private enum ListDiscGroupType
		{
			General,
			Geometry,
			Partition,
			Detection,
			Smart,
			Udma,
			Dma,
			Pio,
		}

		private const String Caption = "Device Info";

		private PluginWindows Plugin => (PluginWindows)this.Window.Plugin;

		private IWindow Window => (IWindow)base.Parent;

		public PanelDevice()
			=> InitializeComponent();

		protected override void OnCreateControl()
		{
			this.Window.Caption = Caption;

			lvStorage.Plugin = this.Plugin;
			lvDisc.Plugin = this.Plugin;
			this.FillDeviceList();
			base.OnCreateControl();
		}

		protected override void WndProc(ref Message m)
		{
			switch(m.Msg)
			{
			case Constant.WM.DEVICECHANGE:
				if((Constant.DBT)m.WParam.ToInt32() == Constant.DBT.DEVNODES_CHANGED)
					this.FillDeviceList();
				break;
			}
			base.WndProc(ref m);
		}

		private void FillDeviceList()
		{
			Int32? selectedDevice = null;
			if(ddlDrive.Items.Count > 0)
			{
				if(ddlDrive.SelectedIndex > -1)
					selectedDevice = ddlDrive.SelectedIndex;

				ddlDrive.Items.Clear();
			}
			foreach(LogicalDrive drive in DeviceIoControl.GetLogicalDrives())
				switch(drive.Type)
				{
				case WinApi.DRIVE.FIXED:
				case WinApi.DRIVE.RAMDISK:
				case WinApi.DRIVE.REMOVABLE:
					ddlDrive.Items.Add(drive.Name);
					break;
				}
			if(ddlDrive.Items.Count > selectedDevice)
				ddlDrive.SelectedIndex = selectedDevice.Value;
		}

		private void tsbnRefresh_Click(Object sender, EventArgs e)
			=> this.FillDeviceList();

		private void ddlDrive_SelectedIndexChanged(Object sender, EventArgs e)
		{
			String device = ddlDrive.SelectedItem.ToString();
			Boolean isFirstFill = lvDisc.Items.Count == 0;
			lvDisc.SuspendLayout();
			lvStorage.SuspendLayout();
			lvSmart.SuspendLayout();

			lvDisc.Items.Clear();
			lvStorage.Items.Clear();
			lvSmart.Items.Clear();
			error.SetError(tsMain, null);

			try
			{
				using(AlphaOmega.Debug.DeviceIoControl info = new DeviceIoControl(device))
				{
					List<ListViewItem> itemsToAdd = new List<ListViewItem>();
					itemsToAdd.Add(lvDisc.CreateListItem(null, "Power", info.IsDeviceOn ? "ON" : "OFF", "General"));
					//itemsToAdd.Add(this.AddDiscListItem("Power", info.IsDeviceOn ? "ON" : "OFF", ListDiscGroupType.General));

					AlphaOmega.Debug.Native.DiscApi.GETVERSIONINPARAMS? version = info.Disc.Version;
					if(version.HasValue)
					{
						itemsToAdd.Add(lvDisc.CreateListItem(null, "Capabilities", version.Value.fCapabilities, "General"));
						itemsToAdd.Add(lvDisc.CreateListItem(null, "Channel", String.Format("{0}/{1}",
							version.Value.IsPrimaryChannel ? "Primary" : "Secondary",
							version.Value.IsMaster ? "Master" : "Subordinate"), "General"));
					}

					itemsToAdd.Add(lvDisc.CreateListItem(null, "Capacity", info.Disc.DriveGeometryEx.ToString(), "General"));
					lvDisc.AddItem(info.Disc.DriveGeometryEx.Geometry);

					AlphaOmega.Debug.Native.DiscApi.DISK_PARTITION_INFO partition = info.Disc.DriveGeometryEx.GetDiscPartitionInfo();
					lvDisc.AddItem(partition.Gpt);
					lvDisc.AddItem(partition.Mbr);

					lvStorage.AddItem(info.Storage.DeviceNumber);
					lvDisc.AddItem(info.FileSystem.VolumeData);

					AlphaOmega.Debug.Native.DiscApi.DISK_DETECTION_INFO detection = info.Disc.DriveGeometryEx.GetDiscDetectionInfo();
					if(detection.DetectionType != AlphaOmega.Debug.Native.DiscApi.DETECTION_TYPE.DetectNone)
					{
						switch(detection.DetectionType)
						{
						case AlphaOmega.Debug.Native.DiscApi.DETECTION_TYPE.DetectExInt13:
							lvDisc.AddItem(detection.ExInt13);
							break;
						case AlphaOmega.Debug.Native.DiscApi.DETECTION_TYPE.DetectInt13:
							lvDisc.AddItem(detection.Int13);
							break;
						default:
							this.Plugin.Trace.TraceEvent(TraceEventType.Warning, 1, "Unknown detection type");
							break;
						}
					}

					if(info.Disc.Smart != null)
					{
						itemsToAdd.Add(lvDisc.CreateListItem(null, "Type", info.Disc.Smart.SystemParams.Type.ToString(), "S.M.A.R.T."));
						itemsToAdd.Add(lvDisc.CreateListItem(null, "Serial Number", info.Disc.Smart.SystemParams.SerialNumber, "S.M.A.R.T."));
						itemsToAdd.Add(lvDisc.CreateListItem(null, "Firmware Version", info.Disc.Smart.SystemParams.FirmwareRev, "S.M.A.R.T."));
						itemsToAdd.Add(lvDisc.CreateListItem(null, "Model Number", info.Disc.Smart.SystemParams.ModelNumber, "S.M.A.R.T."));
						itemsToAdd.Add(lvDisc.CreateListItem(null, "Capabilities", info.Disc.Smart.SystemParams.wCapabilities.ToString("X"), "S.M.A.R.T."));
						itemsToAdd.Add(lvDisc.CreateListItem(null, "Total Addressable sectors", info.Disc.Smart.SystemParams.ulTotalAddressableSectors.ToString("n0") + " sectors (LBA mode only)", "S.M.A.R.T."));

						itemsToAdd.Add(lvDisc.CreateListItem(null, "Cylinders", info.Disc.Smart.SystemParams.wNumCyls, "S.M.A.R.T."));
						itemsToAdd.Add(lvDisc.CreateListItem(null, "Number of heads", info.Disc.Smart.SystemParams.wNumHeads, "S.M.A.R.T."));
						itemsToAdd.Add(lvDisc.CreateListItem(null, "Current number of cylinders", info.Disc.Smart.SystemParams.wNumCurrentCyls, "S.M.A.R.T."));
						itemsToAdd.Add(lvDisc.CreateListItem(null, "Current number of heads", info.Disc.Smart.SystemParams.wNumCurrentHeads, "S.M.A.R.T."));
						itemsToAdd.Add(lvDisc.CreateListItem(null, "Current sectors per track", info.Disc.Smart.SystemParams.wNumCurrentSectorsPerTrack, "S.M.A.R.T."));
						itemsToAdd.Add(lvDisc.CreateListItem(null, "Current sector capacity", info.Disc.Smart.SystemParams.ulCurrentSectorCapacity, "S.M.A.R.T."));

						lvDisc.Items.AddRange(itemsToAdd.ToArray());
						itemsToAdd.Clear();
						foreach(var attr in info.Disc.Smart)
							if(attr.Attribute.bAttrID > 0)
								itemsToAdd.Add(this.AddSmartListItem(
									attr.Attribute.bAttrID.ToString("X"),
									attr.Attribute.AttributeName,
									attr.Attribute.bAttrValue.ToString(),
									attr.Attribute.bWorstValue.ToString(),
									attr.Threshold.bWarrantyThreshold.ToString(),
									String.Format("{0} ({1:n0})", TypeExtender.FormatValue(attr.Attribute.bRawValue, true), attr.Attribute.RawValue)));
						lvSmart.Items.AddRange(itemsToAdd.ToArray());
					}
				}
				this.Window.Caption = String.Join(" - ", new String[] { device, Caption, });

				if(isFirstFill)
				{
					lvDisc.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
					lvSmart.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
				} else
					lvDisc.AutoResizeValueColumn();
			} catch(Win32Exception exc)
			{//Раньше это надо было чтобы не отчищался список. Но теперь он полюбому отчищается
				this.Plugin.Trace.TraceData(TraceEventType.Error, 10, exc);
				error.SetError(tsMain, exc.Message);
			} finally
			{
				lvDisc.ResumeLayout(false);
				lvStorage.ResumeLayout(false);
				lvSmart.ResumeLayout(false);
			}
		}

		private ListViewItem AddSmartListItem(String id, String name, String value, String worst, String threshold, String rawValue)
		{
			ListViewItem result = null;

			/*foreach(ListViewItem item in lvSmart.Items)
				if(item.SubItems[colSmartId.Index].Text == id)
				{
					result = item;
					break;
				}*/
			if(result == null)
			{
				result = new ListViewItem();
				String[] subItems = Array.ConvertAll<String, String>(new String[lvSmart.Columns.Count], delegate(String a) { return String.Empty; });
				result.SubItems.AddRange(subItems);
				result.SubItems[colSmartId.Index].Text = id;
				result.SubItems[colSmartName.Index].Text = name;
				//lvSmart.Items.Add(result);
			}

			result.SubItems[colSmartValue.Index].Text = value;
			result.SubItems[colSmartWorst.Index].Text = worst;
			result.SubItems[colSmartThreshold.Index].Text = threshold;
			result.SubItems[colSmartRaw.Index].Text = rawValue;

			return result;
		}

		/*private IEnumerable<ListViewItem> AddDiscListItems(Object item)
		{
			if(item != null)
			{
				Type itemType = item.GetType();
				foreach(FieldInfo field in itemType.GetFields())
					yield return this.AddDiscListItem(field.Name, field.GetValue(item), itemType.Name);
			}
		}

		private ListViewItem AddDiscListItem(String name, Object value, ListDiscGroupType group)
		{
			return this.AddDiscListItem(name, value, group.ToString());
		}

		private ListViewItem AddDiscListItem(String name, Object value, String groupName)
		{
			ListViewGroup lvg = GetListGroup(lvDisc, groupName);
			ListViewItem result = null;

			if(result == null)
			{
				result = new ListViewItem(lvg);
				while(result.SubItems.Count < lvDisc.Columns.Count)
					result.SubItems.Add(String.Empty);
				result.SubItems[colDiscName.Index].Text = name;
			}

			result.SubItems[colDiscValue.Index].Text = TypeExtender.FormatValue(value, false);
			return result;
		}

		private static ListViewGroup GetListGroup(ListView lv, String groupName)
		{
			ListViewGroup result = lv.Groups[groupName];
			if(result == null)
				result = lv.Groups.Add(groupName, groupName);
			return result;
		}*/
	}
}