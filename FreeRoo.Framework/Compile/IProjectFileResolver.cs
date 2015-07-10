using System;

namespace FreeRoo.Framework
{
	public interface IProjectFileResolver
	{
		IProject Resolver (string projectFilePath);
	}
}

