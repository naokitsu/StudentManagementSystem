using System.Runtime.InteropServices.ComTypes;

namespace StudentManagementSystem;

public class Student : INamed
{
    public static uint FreeId;
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public uint Age { get; set; }

    public uint ID { get; }
    
    public Student(string firstName, string secondName, uint age)
    {
        FirstName = firstName;
        SecondName = secondName;
        Age = age;
        ID = FreeId;
        FreeId += 1;
    }
    public string Name()
    {
        return $"{FirstName} {SecondName} (Age: {Age})";
    }

    public void StudentMenu(ref bool readOnly)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(ToString());
            Console.WriteLine(Strings.BasicTools);
            Console.WriteLine(Strings.SelectTool);
            
            if (readOnly)
            {
                Console.WriteLine(Strings.ReadModeTools);
            }
            else
            {
                Console.WriteLine(Strings.EditModeTool);
            }

            Key a; 
            a = readOnly 
                ? Menu.ReadKey(Key.Mode, Key.Exit) 
                : Menu.ReadKey(Key.Select, Key.Mode, Key.Exit);
            switch (a)
            {
                case Key.Exit:
                    return;
                case Key.Mode:
                    readOnly = !readOnly;
                    break;
                case Key.Select:
                    Console.WriteLine(Strings.SelectFieldToEdit);
                    bool exit = false;
                    while (!exit)
                    {
                        switch (Menu.ReadKey())
                        {
                            case Key.Zero:
                                Console.Write(Strings.NameSemicolon);
                                FirstName = Menu.ReadLine();
                                exit = true;
                                break;
                            case Key.One:
                                Console.Write(Strings.SecondNameSemicolon);
                                SecondName = Menu.ReadLine();
                                exit = true;
                                break;
                            case Key.Two:
                                Console.Write(Strings.AgeSemicolon);
                                Age = Menu.ReadUInt();
                                exit = true;
                                break;
                            case Key.Exit:
                                exit = true;
                                break;
                        }
                    }
                    
                    
                    break;
            }
        }
    }
    
    public override string ToString()
    {
        return $"0: {Strings.NameSemicolon}{FirstName}\n1: {Strings.SecondNameSemicolon}{SecondName}\n2: {Strings.AgeSemicolon}{Age}";
    }
    
    
}

