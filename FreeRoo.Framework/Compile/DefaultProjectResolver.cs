using System;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FreeRoo.Framework
{
	public class DefaultProjectResolver:IProjectFileResolver
	{
		#region IProjectFileResolver implementation

		public IProject Resolver (string projectFilePath)
		{
			FileInfo projectFileInfo = new FileInfo (projectFilePath);

			DefaultProject project = new DefaultProject ();
			XmlDocument doc = new XmlDocument ();

			var content = File.ReadAllText (projectFilePath, Encoding.Default);
			Regex reg = new Regex ("<Project.+?>");
			var match = reg.Match (content);
			content = content.Replace (match.Value, "<Project>");
			doc.LoadXml (content);

			var nodeOutPutType = doc.SelectSingleNode ("Project/PropertyGroup/OutputType");
			var stringOutPutType = (nodeOutPutType as XmlElement).InnerText;
			var o = Enum.Parse (typeof(ProjectOutPutType), stringOutPutType);
			project.OutPutType = (ProjectOutPutType)o;
			project.AssemblyName = (doc.SelectNodes ("Project/PropertyGroup/AssemblyName") [0] as XmlElement).InnerText;
			var nodeListReferences = doc.SelectNodes ("Project/ItemGroup/Reference");
			List<string> references = new List<string> ();
			foreach (var item in nodeListReferences) {
				var current = item as XmlElement;
				references.Add (current.GetAttribute ("Include"));
			}
			project.References = references.ToArray ();
			var nodeListFiles = doc.SelectNodes ("Project/ItemGroup/Compile");
			List<string> files = new List<string> ();
			foreach (var item in nodeListFiles) {
				var current = item as XmlElement;
				files.Add (Path.Combine (projectFileInfo.DirectoryName, current.GetAttribute ("Include").Replace ("\\", "/")));
			}
			project.CSFiles = files.ToArray ();
			return project;
		}

		#endregion

		public DefaultProjectResolver ()
		{
		}
	}
}

