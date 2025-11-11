using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using AlphaOmega.Windows.Forms;
using Plugin.DeviceInfo.Bll;
using Plugin.DeviceInfo.Properties;

namespace Plugin.DeviceInfo.Controls
{
	internal class ReflectionListView : ListView
	{
		private const Int32 ColumnNameIndex = 0;

		private const Int32 ColumnValueIndex = 1;

		public PluginWindows Plugin { get; set; }

		public ReflectionListView()
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

		public void AddItem(Object row)
		{
			_ = this.Plugin ?? throw new InvalidOperationException("Plugin is null");

			if(row != null)
			{
				Type rowType = row.GetType();
				if(rowType.BaseType == typeof(Array))
				{
					Int32 index = 0;
					foreach(Object item in (Array)row)
						this.DataBindI(item, TypeExtender.FormatValue(index++, false));
				} else
					this.DataBindI(row, rowType.Name);

				//This code cannot be used, because a class rarely encapsulates a child array.
				/*IEnumerable ienum = row as IEnumerable;
				if(ienum != null)
				{
					Int32 index = 0;
					foreach(Object item in ienum)
						this.DataBindI(item, this.Plugin.FormatValue(index++));
				} else
					this.DataBindI(row, null);*/
				base.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			}
		}
		public void AutoResizeValueColumn()
			=> base.AutoResizeColumn(ColumnValueIndex, ColumnHeaderAutoResizeStyle.ColumnContent);

		private void DataBindI(Object row, String groupName)
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
			return this.CreateListItem(row, member.Name, value, groupName, isException);
		}

		private ListViewGroup GetGroup(String groupName)
			=> base.Groups[groupName]
				?? base.Groups.Add(groupName, groupName);

		public IEnumerable<ListViewItem> CreateListItems(Object row, String groupName)
		{
			if(row != null)
			{
				Type rowType = row.GetType();
				if(groupName == null)
					groupName = rowType.Name;

				foreach(MemberInfo member in rowType.GetMembers().Where(p => p.MemberType == MemberTypes.Field || p.MemberType == MemberTypes.Property))
					yield return this.CreateReflectedListItem(
						row,
						member,
						groupName);
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

			String[] subItems = Array.ConvertAll<String, String>(new String[base.Columns.Count], a => String.Empty);
			result.SubItems.AddRange(subItems);

			result.SubItems[ReflectionListView.ColumnNameIndex].Text = name;
			if(value == null)
			{
				result.SetNull();
				value = Resources.NullString;
			} else if(exception)
				result.SetException();

			result.SubItems[ReflectionListView.ColumnValueIndex].Text = value;
			return result;
		}
	}
}