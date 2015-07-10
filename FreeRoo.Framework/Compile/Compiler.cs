using System;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.IO;

namespace FreeRoo.Framework
{
	public class Compiler:ICompiler
	{
		
		public CompilerResults Compile (string[] files, string assemblyName, bool generateExecutable, bool generateInMemory)
		{
			CSharpCodeProvider provider = new CSharpCodeProvider ();

			CompilerParameters cp = new CompilerParameters ();
			cp.GenerateExecutable = generateExecutable;
			cp.GenerateInMemory = generateInMemory;
			cp.OutputAssembly = assemblyName;
			cp.ReferencedAssemblies.Add ("System.dll");

			CompilerResults cr = provider.CompileAssemblyFromFile (cp, files);
			//cr.PathToAssembly = Path.Combine (AppDomain.CurrentDomain.BaseDirectory, "test.dll");
			return cr;
		}

		public Compiler ()
		{
		}
	}
}

