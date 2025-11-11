using System;
using System.Collections.Generic;
using System.Diagnostics;
using Plugin.DeviceInfo.Bll;
using SAL.Flatbed;
using SAL.Windows;

namespace Plugin.DeviceInfo
{
	public class PluginWindows : IPlugin
	{
		private readonly IHost _host;
		private TraceSource _trace;
		private Dictionary<String, DockState> _documentTypes;
		private IMenuItem _menuWinApi;
		private IMenuItem _menuDevice;
		private IMenuItem _menuFwSmb;

		internal TraceSource Trace => this._trace ?? (this._trace = PluginWindows.CreateTraceSource<PluginWindows>());

		private IHostWindows HostWindows => this._host as IHostWindows;
		private Dictionary<String, DockState> DocumentTypes
		{
			get
			{
				if(this._documentTypes == null)
					this._documentTypes = new Dictionary<String, DockState>()
					{
						{ typeof(PanelDevice).ToString(), DockState.DockRightAutoHide },
						{ typeof(PanelSmBios).ToString(), DockState.DockLeftAutoHide },
					};
				return this._documentTypes;
			}
		}

		public PluginWindows(IHost host)
			=> this._host = host ?? throw new ArgumentNullException(nameof(host));

		Boolean IPlugin.OnConnection(ConnectMode mode)
		{
			IHostWindows host = this.HostWindows;
			if(host == null)
			{
				this.Trace.TraceEvent(TraceEventType.Error, 10, "Plugin {0} requires {1}", this, typeof(IHostWindows));
				return false;
			} else
			{
				IMenuItem menuTools = host.MainMenu.FindMenuItem("Tools");
				if(menuTools == null)
				{
					this.Trace.TraceEvent(TraceEventType.Error, 10, "Menu item 'Tools' not found");
					return false;
				}

				this._menuWinApi = menuTools.FindMenuItem("WinAPI");
				if(this._menuWinApi == null)
				{
					this._menuWinApi = menuTools.Create("WinAPI");
					this._menuWinApi.Name = "Tools.WinAPI";
					menuTools.Items.Add(this._menuWinApi);
				}
				this._menuDevice = this._menuWinApi.Create("&Device Info");
				this._menuDevice.Name = "Tools.WinAPI.DeviceInfo";
				this._menuDevice.Click += (sender, e) => this.CreateWindow(typeof(PanelDevice).ToString(), true);

				this._menuFwSmb = this._menuWinApi.Create("&SMBIOS");
				this._menuFwSmb.Name = "Tools.WinAPI.Firmware.SMBIOS";
				this._menuFwSmb.Click += (sender, e) => this.CreateWindow(typeof(PanelSmBios).ToString(), true);

				this._menuWinApi.Items.AddRange(new IMenuItem[] { this._menuDevice, this._menuFwSmb });
				return true;
			}
		}

		Boolean IPlugin.OnDisconnection(DisconnectMode mode)
		{
			if(this._menuDevice != null)
				this.HostWindows.MainMenu.Items.Remove(this._menuDevice);
			if(this._menuFwSmb != null)
				this.HostWindows.MainMenu.Items.Remove(this._menuFwSmb);
			if(this._menuWinApi != null && this._menuWinApi.Items.Count == 0)
				this.HostWindows.MainMenu.Items.Remove(this._menuWinApi);

			NodeExtender.DisposeFonts();
			return true;
		}

		public IWindow GetPluginControl(String typeName, Object args)
			=> this.CreateWindow(typeName, false, args);

		private IWindow CreateWindow(String typeName, Boolean searchForOpened, Object args = null)
			=> this.DocumentTypes.TryGetValue(typeName, out DockState state)
				? this.HostWindows.Windows.CreateWindow(this, typeName, searchForOpened, state, args)
				: null;

		private static TraceSource CreateTraceSource<T>(String name = null) where T : IPlugin
		{
			TraceSource result = new TraceSource(typeof(T).Assembly.GetName().Name + name);
			result.Switch.Level = SourceLevels.All;
			result.Listeners.Remove("Default");
			result.Listeners.AddRange(System.Diagnostics.Trace.Listeners);
			return result;
		}
	}
}