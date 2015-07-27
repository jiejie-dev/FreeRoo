using Owin;
using Nancy;

namespace FreeRoo.Core
{
    /// <summary>
    /// 支持NancyFx的OWIN启动类
    /// <para>MS OWIN标准的宿主都需要一个启动类</para>
    /// </summary>
    public class Startup
    {
        public void Configuration(IAppBuilder builder)
        {
            //将 Nancy(中间件)添加到Microsoft.Owin处理环节中
            ////////////////////////////////////////////////////
            builder.UseNancy();

			//启动FreeRoo
			FreeRooAppStarter start=new FreeRooAppStarter();
        }
    }
}