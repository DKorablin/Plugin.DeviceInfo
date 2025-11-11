using System;
using AlphaOmega.Debug.Native;
using Plugin.DeviceInfo.Bll;

namespace Plugin.DeviceInfo.Controls
{
	internal class DdlSmBiosType
	{
		private static XmlReflectionReader _XmlReader;
		internal static XmlReflectionReader XmlReader
		{
			get
			{
				return DdlSmBiosType._XmlReader
					?? (DdlSmBiosType._XmlReader = new XmlReflectionReader(typeof(DdlSmBiosType).Assembly.GetManifestResourceStream("Plugin.DeviceInfo.SystemFirmware.xml")));
			}
		}

		public SmBios.Type? Type { get; }

		public DdlSmBiosType()
		{ }

		public DdlSmBiosType(SmBios.Type type)
			=> this.Type = type;

		public override String ToString()
			=> this.Type == null
				? String.Empty
				: DdlSmBiosType.XmlReader.FindEnumDocumentation(this.Type.Value)
				?? $"Unknown ({this.Type.Value})";
	}
}