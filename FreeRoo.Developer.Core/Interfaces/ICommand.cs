using System;

namespace FreeRoo.Developer
{
	public interface ICommand
	{
		void SetArgs(string[] args);
		void Excute();
	}
}

