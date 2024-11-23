using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Plugin.DeviceInfo.Properties;

namespace Plugin.DeviceInfo
{
	internal static class TypeExtender
	{
		public static Type GetMemberType(this MemberInfo member)
		{
			switch(member.MemberType)
			{
			case MemberTypes.Field:
				return ((FieldInfo)member).FieldType;
			case MemberTypes.Property:
				return ((PropertyInfo)member).PropertyType;
			case MemberTypes.Method:
				return ((MethodInfo)member).ReturnType;
			case MemberTypes.TypeInfo:
			case MemberTypes.NestedType:
				return (Type)member;
			default:
				throw new NotImplementedException();
			}
		}

		public static String FormatValue(Object value, Boolean showAsHex)
			=> value == null
				? null
				: TypeExtender.FormatValue(value.GetType(), value, showAsHex);

		public static Object GetMemberValue(this MemberInfo member, Object obj)
		{
			switch(member.MemberType)
			{
			case MemberTypes.Field:
				return ((FieldInfo)member).GetValue(obj);
			case MemberTypes.Property:
				return ((PropertyInfo)member).GetValue(obj, null);
			default:
				throw new NotImplementedException();
			}
		}

		public static String FormatValue(this MemberInfo info, Object value, Boolean showAsHex)
		{
			if(value == null)
				return null;

			Type type = info.GetMemberType();

			if(type.IsEnum)
				return value.ToString();
			else if(type==typeof(Char))
				switch((Char)value)
					{
					case '\'':	return "\\\'";
					case '\"':	return "\\\"";
					case '\0':	return "\\0";
					case '\a':	return "\\a";
					case '\b':	return "\\b";
					case '\f':	return "\\b";
					case '\t':	return "\\t";
					case '\n':	return "\\n";
					case '\r':	return "\\r";
					case '\v':	return "\\v";
					default:	return value.ToString();
					}
			else if(value is IFormattable)
			{
				switch(Convert.GetTypeCode(value))
				{
				case TypeCode.Byte:
				case TypeCode.SByte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
					if(showAsHex)
						return "0x" + ((IFormattable)value).ToString("X", CultureInfo.CurrentCulture);
					else
						return ((IFormattable)value).ToString("n0", CultureInfo.CurrentCulture);
				default:
					return value.ToString();
				}
			}
			else
			{
				Type elementType = type.HasElementType ? type.GetElementType() : null;
				if(elementType != null && type.BaseType == typeof(Array) && (elementType.IsPrimitive || elementType==typeof(String)))
				{
					Int32 index = 0;
					Array arr = (Array)value;
					StringBuilder values = new StringBuilder(String.Format("{0}[{1}]", elementType, TypeExtender.FormatValue(arr.Length, showAsHex)));
					values.Append(" { ");
					foreach(Object item in arr)
					{
						if(index++ > 10)
						{
							values.Append("...");
							break;
						}

						values.AppendFormat("{0}, ", TypeExtender.FormatValue(item, showAsHex) ?? Resources.NullString);
					}
					values.Append("}");
					return values.ToString();
				}else
					return value.ToString();
			}
		}

		public static Type GetRealType(this Type type)
		{
			if(type.IsGenericType)
			{
				Type genericType = type.GetGenericTypeDefinition();
				if(genericType == typeof(Nullable<>)
					|| genericType == typeof(IEnumerator<>)
					|| genericType == typeof(IEnumerable<>)
					/*|| genericType == typeof(System.Collections.Generic.SortedList<,>)*/)
					return type.GetGenericArguments()[0].GetRealType();
			}
			if(type.HasElementType)
				//if(type.BaseType == typeof(Array))//+Для out и ref параметров
				return type.GetElementType().GetRealType();
			return type;
		}
	}
}