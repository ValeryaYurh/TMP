using LR4.Contracts;
using LR4.Entities;

namespace LR4.Services
{
    public class FileService : IFileService<Product>
    {
        public IEnumerable<Product> ReadFile(string fileName)
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (BinaryReader reader = new BinaryReader(stream))
            {
                while (stream.Position < stream.Length)
                {
                    Product? product = null;

                    try
                    {
                        string name = reader.ReadString();
                        int price = reader.ReadInt32();
                        int quantity = reader.ReadInt32();
                        bool inStock = reader.ReadBoolean();

                        product = new Product(name, price, quantity, inStock);
                    }
                    catch (EndOfStreamException ex)
                    {
                        Console.WriteLine($"Файл \"{fileName}\" оборван, данные неполные: {ex.Message}");
                    }
                    catch (IOException ex)
                    {
                        Console.WriteLine($"Ошибка ввода-вывода при чтении файла: {ex.Message}");
                    }
                    catch (ObjectDisposedException ex)
                    {
                        Console.WriteLine($"Попытка чтения из закрытого потока: {ex.Message}");
                    }

                    if (product == null)
                        break;

                    yield return product;
                }
            }
        }

        public void SaveData(IEnumerable<Product> data, string fileName)
        {
            if (File.Exists(fileName))
                File.Delete(fileName);

            using (FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            using (BinaryWriter writer = new BinaryWriter(stream))
            {
                try
                {
                    foreach (Product product in data)
                    {
                        writer.Write(product.Name);
                        writer.Write(product.Price);
                        writer.Write(product.Quantity);
                        writer.Write(product.InStock);
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine($"Ошибка ввода-вывода при записи файла: {ex.Message}");
                }
                catch (ObjectDisposedException ex)
                {
                    Console.WriteLine($"Попытка записи в закрытый поток: {ex.Message}");
                }
            }
        }
    }
}
