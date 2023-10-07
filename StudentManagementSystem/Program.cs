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

        Console.WriteLine(Strings.SelectLanguage);
        Strings.Culture = Menu.ReadKey(Key.One, Key.Zero) switch
        {
            Key.Zero => CultureInfo.GetCultureInfo("en"),
            Key.One => CultureInfo.GetCultureInfo("ru"),
            _ => Strings.Culture
        };

        var file = "save.json";
        string payload;
        Storage? data = null;
        if (File.Exists(file))
        {
            using var inputFile = new StreamReader(file);
            payload = inputFile.ReadToEnd();
            data = JsonConvert.DeserializeObject<Storage>(payload);
        }
        
        
        storage = data ?? new Storage();
        
        Console.Clear();
        Console.WriteLine(Strings.Welcome_Message);
        
        storage.OpenUi(readOnly: ref readOnly);
        
        var saved = JsonConvert.SerializeObject(storage, Formatting.Indented);
        
        using (StreamWriter outputFile = new StreamWriter("save1.json"))
        {
            outputFile.Write(saved);
        }
        
    }
}
