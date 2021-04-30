using DWMH.Core;
using DWMH.BLL;
using Ninject;

namespace DWMH
{
    public class Program
    {
        static void Main(string[] args)
        { 

            NinjectContainer.Configure();
            var controller = NinjectContainer.Kernel.Get<Controller>();

            controller.Run();
        }
    }
}
