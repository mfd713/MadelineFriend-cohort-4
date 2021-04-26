using Ninject;

namespace SustainableForaging.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //I'm too paranoid to delete this
            //ConsoleIO io = new ConsoleIO();
            //View view = new View(io);

            //string forageFileDirectory = Path.Combine(projectDirectory, "data", "forage_data");
            //string foragerFilePath = Path.Combine(projectDirectory, "data", "foragers.csv");
            //string itemFilePath = Path.Combine(projectDirectory, "data", "items.txt");

            //ForageFileRepository forageFileRepository = new ForageFileRepository(forageFileDirectory);
            //ForagerFileRepository foragerFileRepository = new ForagerFileRepository(foragerFilePath);
            //ItemFileRepository itemFileRepository = new ItemFileRepository(itemFilePath);

            //ForagerService foragerService = new ForagerService(foragerFileRepository);
            //ForageService forageService = new ForageService(forageFileRepository, foragerFileRepository, itemFileRepository);
            //ItemService itemService = new ItemService(itemFileRepository);

            //Report report = new Report(forageFileRepository, itemFileRepository, foragerFileRepository);

            //Controller controller = new Controller(foragerService, forageService, itemService, view, report);

            NinjectContainer.Configure();
            var controller = NinjectContainer.Kernel.Get<Controller>();
            controller.Run();
        }

    }
}
