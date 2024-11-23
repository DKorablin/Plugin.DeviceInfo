using System;

namespace Plugin.DeviceInfo
{
	internal static class Constant
	{
		public static class WM
		{
			/// <summary>Notifies an application of a change to the hardware configuration of a device or the computer.</summary>
			/// <remarks>http://msdn.microsoft.com/en-us/library/windows/desktop/aa363480%28v=vs.85%29.aspx</remarks>
			public const Int32 DEVICECHANGE = 0x0219;
		}

		/// <summary>Device event type</summary>
		public enum DBT : Int32
		{
			/// <summary>A request to change the current configuration (dock or undock) has been canceled.</summary>
			CONFIGCHANGECANCELED = 0x0019,
			/// <summary>The current configuration has changed, due to a dock or undock.</summary>
			CONFIGCHANGED = 0x0018,
			/// <summary>A custom event has occurred.</summary>
			CUSTOMEVENT = 0x8006,
			/// <summary>A device or piece of media has been inserted and is now available.</summary>
			DEVICEARRIVAL = 0x8000,
			/// <summary>Permission is requested to remove a device or piece of media.</summary>
			/// <remarks>Any application can deny this request and cancel the removal.</remarks>
			DEVICEQUERYREMOVE = 0x8001,
			/// <summary>A request to remove a device or piece of media has been canceled.</summary>
			DEVICEQUERYREMOVEFAILED = 0x8002,
			/// <summary>A device or piece of media has been removed.</summary>
			DEVICEREMOVECOMPLETE = 0x8004,
			/// <summary>A device or piece of media is about to be removed. Cannot be denied.</summary>
			DEVICEREMOVEPENDING = 0x8003,
			/// <summary>A device-specific event has occurred.</summary>
			DEVICETYPESPECIFIC = 0x8005,
			/// <summary>A device has been added to or removed from the system.</summary>
			DEVNODES_CHANGED = 0x0007,
			/// <summary>Permission is requested to change the current configuration (dock or undock).</summary>
			QUERYCHANGECONFIG = 0x0017,
			/// <summary>The meaning of this message is user-defined.</summary>
			USERDEFINED = 0xffff,
		}
	}
}