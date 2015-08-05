using System;

namespace FreeRoo.Developer
{
	public interface ICommandParser
	{
		ICommand Parser (string cmdLine);
	}
}

