using System;
using DWMH.Core.Repos;
using DWMH.DAL;
using System.IO;
using DWMH.Core.Loggers;
using Ninject;

namespace DWMH
{
    public static class NinjectContainer
    {
        public static StandardKernel Kernel { get; private set; }

        public static void Configure()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.
                Parent.Parent.FullName;

            Kernel = new StandardKernel();
            int loggerChoice = ConsoleIO.PromptInt("What kind of logging do you want?" +
                "\n1. None\n2. Console\n3. File\n", 1, 3);

            switch (loggerChoice)
            {
                case 2:
                    Kernel.Bind<ILogger>().To<ConsoleLogger>();
                    break;
                case 3:
                    Kernel.Bind<ILogger>().To<FileLogger>().WithConstructorArgument("_filePath","log.txt");
                    break;
                default:
                    Kernel.Bind<ILogger>().To<NullLogger>();
                    break;
            }

            Kernel.Bind<IReservationRepository>().To<ReservationFileRepository>()
                .WithConstructorArgument("directory", projectDirectory + "\\reservations");
            Kernel.Bind<IGuestRepository>().To<GuestFileRepository>()
                .WithConstructorArgument("file", Path.Combine(projectDirectory, "guests.csv"));
            Kernel.Bind<IHostRepository>().To<HostFileRepository>()
                .WithConstructorArgument("file", Path.Combine(projectDirectory, "hosts.csv"));
        }
    }
}
