using System;
using System.Windows.Forms;

namespace Plugin.DeviceInfo.Controls
{
	internal partial class NameValueLabel : UserControl
	{
		public String Title
		{
			get => lblTitle.Text;
			set => lblTitle.Text = value;
		}

		public String Value
		{
			get => txtText.Text;
			set => txtText.Text = value;
		}

		public NameValueLabel()
			=> InitializeComponent();

		public void SetValue(String title, String value)
		{
			this.Title = title;
			this.Value = value;
		}
	}
}