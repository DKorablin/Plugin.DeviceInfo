using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using AlphaOmega.Debug.Smb;
using AlphaOmega.Windows.Forms;
using Plugin.DeviceInfo.Bll;
using Plugin.DeviceInfo.Properties;

namespace Plugin.DeviceInfo.Controls
{
	class BiosTypeListView : ListView
	{
		private const Int32 ColumnNameIndex = 0;

		private const Int32 ColumnValueIndex = 1;

		public PluginWindows Plugin { get; set; }

		public BiosTypeListView()
		{
			base.View = View.Details;
			base.FullRowSelect = true;
			base.HeaderStyle = ColumnHeaderStyle.None;
			base.MultiSelect = true;
			base.Sorting = SortOrder.Ascending;
			base.UseCompatibleStateImageBehavior = false;
			base.HideSelection = false;

			base.ContextMenuStrip = new ContextMenuStripCopy();
		}

		public void AddItem(TypeBase row, String groupName)
		{
			_ = this.Plugin ?? throw new InvalidOperationException("Plugin is null");

			this.DataBindI(row, groupName);

			//Такой код использовать нельзя. Т.к. изредка класс инкапсулирует дочерний массив
			/*IEnumerable ienum = row as IEnumerable;
			if(ienum != null)
			{
				Int32 index = 0;
				foreach(Object item in ienum)
					this.DataBindI(item, this.Plugin.FormatValue(index++));
			} else
				this.DataBindI(row, null);*/
		}

		public void AutoResizeValueColumn()
			=> base.AutoResizeColumn(ColumnValueIndex, ColumnHeaderAutoResizeStyle.ColumnContent);

		private void DataBindI(TypeBase row, String groupName)
		{
			List<ListViewItem> items = new List<ListViewItem>();
			items.AddRange(this.CreateListItems(row, groupName));

			base.Items.AddRange(items.ToArray());
		}

		internal ListViewItem CreateReflectedListItem(Object row, MemberInfo member, String groupName)
		{
			if(groupName == null)
				groupName = member.MemberType.ToString();

			String value;
			Boolean isException = false;
			try
			{
				value = member.FormatValue(member.GetMemberValue(row), false);
			} catch(TargetInvocationException exc)
			{
				isException = true;
				value = exc.InnerException.Message;
			} catch(Exception exc)
			{
				isException = true;
				value = exc.Message;
			}
			return this.CreateListItem(row, DdlSmBiosType.XmlReader.FindDocumentation(member) ?? member.Name, value, groupName, isException);
		}

		private ListViewGroup GetGroup(String groupName)
			=> base.Groups[groupName]
				?? base.Groups.Add(groupName, groupName);

		public IEnumerable<ListViewItem> CreateListItems(TypeBase row, String groupName)
		{
			if(row != null)
			{
				Type rowType = row.GetType();
				if(groupName == null)
					groupName = DdlSmBiosType.XmlReader.FindEnumDocumentation(row.Header.Type);

				Object typeObject = null;
				PropertyInfo typeType = null;
				List<MemberInfo> members = new List<MemberInfo>();
				foreach(MemberInfo member in rowType.GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy).Where(p => p.MemberType == MemberTypes.Property))
				{
					switch(member.Name)
					{
					case "Type":
						typeType = (PropertyInfo)member;//HACK: Грязный хак поиска структуры...
						typeObject = typeType.GetValue(row, null);
						break;
					case "Strings":
					case "ExData":
						break;
					case "Header":
						members.Add(member);
						break;
					default:
						members.Add(member);
						yield return this.CreateReflectedListItem(row, member, groupName);
						break;
					}
				}

				if(typeType != null)
					foreach(MemberInfo member in typeType.PropertyType.GetMembers(BindingFlags.Public | BindingFlags.Instance).Where(p => p.MemberType == MemberTypes.Property || p.MemberType == MemberTypes.Field))
						if(!members.Any(p => p.Name == member.Name))
							yield return this.CreateReflectedListItem(typeObject, member, groupName);
			}
		}

		public ListViewItem CreateListItem(Object row, String name, Object value, String groupName)
			=> this.CreateListItem(
				row,
				name,
				TypeExtender.FormatValue(value, false),
				groupName,
				false);

		public ListViewItem CreateListItem(Object row, String name, String value, String groupName, Boolean exception)
		{
			ListViewItem result = new ListViewItem() { Tag = row, };
			if(!String.IsNullOrEmpty(groupName))
				result.Group = this.GetGroup(groupName);

			if(base.Columns.Count == 0)
				base.Columns.AddRange(new ColumnHeader[]
			{
				new ColumnHeader(){ Text = "Name", },
				new ColumnHeader(){ Text = "Value", },
			});

			String[] subItems = Array.ConvertAll<String, String>(new String[base.Columns.Count], delegate (String a) { return String.Empty; });
			result.SubItems.AddRange(subItems);

			result.SubItems[BiosTypeListView.ColumnNameIndex].Text = name;
			if(value == null)
			{
				result.SetNull();
				value = Resources.NullString;
			} else if(exception)
				result.SetException();

			result.SubItems[BiosTypeListView.ColumnValueIndex].Text = value;
			return result;
		}
	}
}