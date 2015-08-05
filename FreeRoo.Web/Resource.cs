using System;
using System.Collections.Generic;
using Nancy.Json;
using System.Linq;

namespace FreeRoo.Web
{
	public class ResourceBase
	{
		public ResourceBase(string name,string path,string cdn)
		{
			this.Name = name;
			this.Path = path;
			this.Cdn = cdn;
		}
		public string Name{get;set;}
		public string Path{get;set;}
		public string Cdn{get;set;}

		public ResourceBase SetName(string name)
		{
			this.Name = name;
			return this;
		}
		public ResourceBase SetPath(string path)
		{
			this.Path = path;
			return this;
		}
		public ResourceBase SetCdn(string cdn)
		{
			this.Cdn = cdn;
			return this;
		}
	}
	public class Style:ResourceBase
	{
		public Style(string name,string path,string cdn)
			:base(name,path,cdn)
		{

		}
	}
	public class Script:ResourceBase
	{
		public Script(string name,string path,string cdn)
			:base(name,path,cdn)
		{

		}
	}
	public class Resource
	{
		public static Resource Current{ get; set; }
		static Resource(){
			Current = new Resource ();
		}
		public List<Style> Styles{ get; set; }
		public List<Script> Scripts{get;set;}
		public Resource ()
		{
			this.Styles = new List<Style> ();
			this.Scripts = new List<Script> ();
		}
		public Style Style(string name,string path,string cdn)
		{
			var s = new Style (name, path, cdn);
			this.Styles.Add (s);
			return s;
		}
		public Style Style(string name)
		{
			return this.Styles.FirstOrDefault(x => x.Name == name);
		}
		public Script Script(string name,string path,string cdn)
		{
			var s = new Script (name, path, cdn);
			this.Scripts.Add (s);
			return s;
		}
		public Script Script(string name)
		{
			return this.Scripts.FirstOrDefault (x => x.Name == name);
		}
	}
}

