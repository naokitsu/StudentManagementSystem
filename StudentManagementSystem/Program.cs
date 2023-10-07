// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;

namespace StudentManagementSystem;

using System;
using System.Globalization;

public static class StudentManagementSystem
{
    public static void Main()
    {
        Storage storage;
        var readOnly = true;

        // Выбор локали
        {
            Console.WriteLine(Strings.SelectLanguage);
            Strings.Culture = Menu.ReadKey(Key.One, Key.Zero) switch
            {
                Key.Zero => CultureInfo.GetCultureInfo("en"),
                Key.One => CultureInfo.GetCultureInfo("ru"),
                _ => Strings.Culture
            };
        }
        
        // Выбор файла
        {
            Console.Clear();
            Console.Write(Strings.EnterTheFileToOpen);
            var filename = Console.ReadLine();
            if (filename == "")
                filename = null;
            storage = DeserializeStorage(filename) ?? new Storage();
        }
        
        // Открытие меню хранилища
        {
            Console.Clear();
            Console.WriteLine(Strings.Welcome_Message);
            storage.OpenUi(readOnly: ref readOnly);
        }
        
        // Сохранение файла
        {
            while (true)
            {
                Console.Clear();
                Console.Write(Strings.EnterTheSaveFile);
                var filename = Console.ReadLine();

                if (filename is null or "")
                {
                    Console.WriteLine(Strings.GoingToDiscard);
                    if (Menu.ReadKey(Key.Yes, Key.No) == Key.No)
                    {
                        continue;
                    }
                    Console.WriteLine(Strings.StorageDiscarded);
                    return;
                }

                if (File.Exists(filename))
                {
                    Console.WriteLine(Strings.GoingToOverwrite);
                    if (Menu.ReadKey(Key.Yes, Key.No) == Key.No)
                    {
                        continue;
                    }
                    Console.WriteLine(Strings.FileOverwritten);
                }

                SerializeStorage(storage, filename);
                Console.WriteLine(Strings.StorageSaved);
                break;
            }
        }
    }

    /// <summary>
    /// Десерилизация хранилища из файла
    /// </summary>
    /// <param name="filename">Путь файла</param>
    /// <returns>Хранилище или null, если файл не существует или структура повреждена</returns>
    private static Storage? DeserializeStorage(string? filename)
    {
        if (filename is null) return null;
        if (!File.Exists(filename)) return null;
        
        using var inputFile = new StreamReader(filename);
        var payload = inputFile.ReadToEnd();
        return JsonConvert.DeserializeObject<Storage>(payload);
    }
    
    /// <summary>
    /// Серилизация хранилища в файл
    /// </summary>
    /// <param name="storage">Хранилище</param>
    /// <param name="filename">Путь файла</param>
    private static void SerializeStorage(Storage storage, string filename)
    {
        var saved = JsonConvert.SerializeObject(storage, Formatting.Indented);
        using var outputFile = new StreamWriter(filename);
        outputFile.Write(saved);
    }
}
