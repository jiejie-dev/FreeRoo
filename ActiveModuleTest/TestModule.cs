using System;
using Nancy;

namespace ActiveModuleTest
{
	public class TestModule:NancyModule
	{
		public TestModule ()
		{
			Get ["/test"] = _ => "test";
		}
	}
}

