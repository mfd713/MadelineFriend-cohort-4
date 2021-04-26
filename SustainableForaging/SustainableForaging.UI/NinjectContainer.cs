using SustainableForaging.BLL;
using SustainableForaging.DAL;
using System;
using System.IO;
using Ninject;
using SustainableForaging.Core.Repositories;

namespace SustainableForaging.UI
{
    public static class NinjectContainer
    {
        public static StandardKernel Kernel { get; private set; }


        public static void Configure()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            Kernel = new StandardKernel();
            Kernel.Bind<IForageRepository>().To<ForageFileRepository>()
                .WithConstructorArgument("directory",
                Path.Combine(projectDirectory, "data", "forage_data"));
            Kernel.Bind<IItemRepository>().To<ItemFileRepository>()
                .WithConstructorArgument("filePath",
                Path.Combine(projectDirectory, "data", "items.txt"));
            Kernel.Bind<IForagerRepository>().To<ForagerFileRepository>()
                .WithConstructorArgument("filePath",
                Path.Combine(projectDirectory, "data", "foragers.csv"));

        }
    }
}
