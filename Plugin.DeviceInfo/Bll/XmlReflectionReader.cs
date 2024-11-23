using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace Plugin.DeviceInfo.Bll
{
	internal class XmlReflectionReader
	{
		private readonly XmlDocument _document;
		private readonly String _assemblyName;
		private readonly Dictionary<String, String> _documentationCache = new Dictionary<String, String>();

		public XmlReflectionReader(String xml)
		{
			XmlDocument document = new XmlDocument();
			document.LoadXml(xml);
			this._document = document;
			this._assemblyName = this.QuerySingleNode("/doc/assembly/name").Trim();
		}

		private String QuerySingleNode(String xPath)
		{
			var navigator = this._document.CreateNavigator();
			var xmlResult = navigator.SelectSingleNode(xPath);
			return xmlResult?.InnerXml;
		}

		public String FindDocumentation(MemberInfo type)
		{
			Assembly asm = type.Module.Assembly;
			if(asm.GetName().Name != this._assemblyName)
				return null;

			String memberName = XmlReflectionReader.GetMemberName(type);
			return this.FindDocumentationI(memberName);
		}

		private String FindDocumentationI(String memberName)
		{
			if(!this._documentationCache.TryGetValue(memberName, out String result))
			{
				String xmlResult = this.QuerySingleNode($"/doc/members/member[@name=\"{memberName}\"]/summary");
				result = xmlResult?.Trim().Replace("  ", " ");

				this._documentationCache.Add(memberName, result);
			}

			return result;
		}

		public String FindEnumDocumentation<TEnum>(TEnum value) where TEnum : struct
		{
			String fullName = value.GetType().ToString();
			//TODO: Add FlagsAttribute
			String memberName = "F:" + fullName.Replace('+', '.') + "." + value;//Nested types declared as Namespace.Class+InnerClass.Field
			return this.FindDocumentationI(memberName);
		}

		private static String GetMemberName(MemberInfo type)
		{
			Char prefix;
			switch(type.MemberType)
			{
			case MemberTypes.Field:
				prefix = 'F';
				break;
			case MemberTypes.Property:
				prefix = 'P';
				break;
			case MemberTypes.TypeInfo:
			case MemberTypes.NestedType://<- enum
				Type elementType = ((Type)type).GetRealType();
				if(elementType == type)
					prefix = 'T';
				else//Array[MyType]
					return GetMemberName(elementType);
				break;
			case MemberTypes.Method:
				prefix = 'M';
				break;
			/*case MemberTypes.NestedType:
				if(type.Ise)*/
			default: throw new NotImplementedException();
			}

			String fullName = type.DeclaringType == null
				? type.ToString()
				: type.DeclaringType.FullName;
			return prefix
				+ ":"
				+ fullName.Replace('+', '.')//Nested types declares as Namespace.Class+InnerClass.Field
				+ "."
				+ type.Name;
		}
	}
}