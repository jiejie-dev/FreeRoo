using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;

namespace FreeRoo.Developer
{
	public class CmdContainer:ICmdContainer
	{
		private List<Type> _cmdTypes;

		public CmdContainer ()
		{
			InitCmdsTypes ();	
		}

		private void InitCmdsTypes ()
		{
			_cmdTypes = new List<Type> ();

			AddAssemblyTypes (Assembly.GetExecutingAssembly ());
			DirectoryInfo dirInfo = new DirectoryInfo (AppDomain.CurrentDomain.BaseDirectory);
			FileInfo[] files = dirInfo.GetFiles ("\"*.frd\"",SearchOption.AllDirectories);
			foreach (var item in files) {
				Assembly assembly = Assembly.LoadFrom (item.Name);
				AddAssemblyTypes (assembly);
			}
		}
		private void AddAssemblyTypes(Assembly assembly)
		{
			var types = assembly.GetTypes ();
			foreach (var item in types.ToList ()) {
				if (typeof(ICommand).IsAssignableFrom (item)) {
					_cmdTypes.Add (item);
				}
			}
		}

		public void AddCmdType (Type type)
		{
			this._cmdTypes.Add (type);
		}

		public List<Type> GetAllCmdTypes ()
		{
			return _cmdTypes;
		}

		public string[] GetAllCmdNameList ()
		{
			List<string> list = new List<string> ();
			foreach (var item in this.GetAllCmdTypes ()) {
				list.Add (item.Name.Replace ("Command","").ToLower ());
			}
			return list.ToArray ();
		}
	}
}

