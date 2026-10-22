using LR4.Comparers;
using LR4.Entities;
using LR4.Services;

namespace LR4
{
    internal class Program
    {
        static void Main()
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "YURHILEVICH_Lab4");

            bool folderExisted = PrepareFolder(folderPath);
            if (folderExisted)
                Console.WriteLine($"Папка \"{folderPath}\" уже существует, её содержимое удалено");
            else
                Console.WriteLine($"Папка \"{folderPath}\" создана");

            FillFolderWithRandomFiles(folderPath);

            Console.WriteLine("\nСодержимое папки:");
            foreach (string file in Directory.GetFiles(folderPath))
                Console.WriteLine($"Файл: {Path.GetFileName(file)} имеет расширение {Path.GetExtension(file)}");

            List<Product> products = CreateProducts();
            FileService fileService = new FileService();

            string filePath = Path.Combine(folderPath, "products.txt");
            string newFilePath = Path.Combine(folderPath, "products_renamed.txt");

            fileService.SaveData(products, filePath);
            Console.WriteLine($"\nКоллекция записана в файл \"{Path.GetFileName(filePath)}\"");

            RenameFile(filePath, newFilePath);
            Console.WriteLine($"Файл переименован в \"{Path.GetFileName(newFilePath)}\"");

            List<Product> loadedProducts = new List<Product>();
            loadedProducts.AddRange(fileService.ReadFile(newFilePath));

            List<Product> sortedByName = loadedProducts.Order(new MyCustomComparer<Product>()).ToList();

            PrintProducts("Исходная коллекция", products);
            PrintProducts("Коллекция из файла, отсортированная по Name", sortedByName);

            List<Product> sortedByPrice = loadedProducts
                .Order(Comparer<Product>.Create((first, second) => first.Price.CompareTo(second.Price)))
                .ToList();

            PrintProducts("Коллекция из файла, отсортированная по Price", sortedByPrice);
        }

        static bool PrepareFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                DirectoryInfo directory = new DirectoryInfo(folderPath);

                foreach (FileInfo file in directory.GetFiles())
                    file.Delete();

                foreach (DirectoryInfo subDirectory in directory.GetDirectories())
                    subDirectory.Delete(true);

                return true;
            }

            Directory.CreateDirectory(folderPath);
            return false;
        }

        static void FillFolderWithRandomFiles(string folderPath)
        {
            string[] extensions = { ".txt", ".rtf", ".dat", ".inf" };
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                string extension = extensions[random.Next(extensions.Length)];
                string fileName = Path.ChangeExtension(Path.GetRandomFileName(), extension);
                string filePath = Path.Combine(folderPath, fileName);

                using (File.Create(filePath))
                {
                }
            }
        }

        static List<Product> CreateProducts()
        {
            List<Product> products = new List<Product>();

            products.Add(new Product("Молоко", 3, 40, true));
            products.Add(new Product("Хлеб", 2, 100, true));
            products.Add(new Product("Сахар", 4, 0, false));
            products.Add(new Product("Чай", 5, 25, true));
            products.Add(new Product("Кофе", 12, 0, false));
            products.Add(new Product("Масло", 8, 15, true));

            return products;
        }

        static void RenameFile(string oldPath, string newPath)
        {
            File.Move(oldPath, newPath);
        }

        static void PrintProducts(string title, IEnumerable<Product> products)
        {
            Console.WriteLine($"\n{title}:");

            foreach (Product product in products)
            {
                string availability = "нет";
                if (product.InStock)
                    availability = "да";

                Console.WriteLine($"  {product.Name}: цена {product.Price}, количество {product.Quantity}, в наличии: {availability}");
            }
        }
    }
}
