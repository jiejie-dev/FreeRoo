using System;
using System.Security.Cryptography.X509Certificates;
using System.Reflection;
using System.CodeDom.Compiler;

namespace FreeRoo.Framework
{
	public class CompileEngine:ICompileEngine
	{
		public CompileEngine ()
		{
			
		}

		public CompilerResults CompileProject (string projectFilePath)
		{
			DefaultProjectResolver resolver = new DefaultProjectResolver ();
			DefaultProject project = (DefaultProject)resolver.Resolver (projectFilePath);
			Compiler compiler = new Compiler ();
			bool ifExe = project.OutPutType == ProjectOutPutType.Exe ? true : false;
			return compiler.Compile (project.CSFiles, project.AssemblyName + "." + project.OutPutType.ToString ().ToLower (), ifExe, true);
		}
	}
}

