using System.Runtime.InteropServices.ComTypes;

namespace StudentManagementSystem;

/// <summary>
/// Класс студента, хранит в себе информацию о студентах
/// </summary>
public class Student : INamed
{
    /// <summary>
    /// Свободный ID, так как каждый студент должен обладать уникальным ID (чтобы мы могли гарантирванно упаковывать их
    /// в словари без колизий), мы используем статическое поле, которое хранит следующий свободный ID 
    /// </summary>
    public static uint FreeId;
    
    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Фамилия
    /// </summary>
    public string SecondName { get; set; }
    
    /// <summary>
    /// Возраст
    /// </summary>
    public uint Age { get; set; }

    /// <summary>
    /// ID Студента
    /// </summary>
    public uint Id { get; }
    
    /// <summary>
    /// Конструктор студента
    /// </summary>
    /// <param name="firstName">Имя</param>
    /// <param name="secondName">Фамилия</param>
    /// <param name="age">Возраст</param>
    public Student(string firstName, string secondName, uint age)
    {
        FirstName = firstName;
        SecondName = secondName;
        Age = age;
        Id = FreeId;
        FreeId += 1;// Обновляем свободный ID для следующего студента
    }
    
    /// <summary>
    /// Вернуть имя студента
    /// </summary>
    /// <returns>Имя студента</returns>
    public string Name()
    {
        return $"{FirstName} {SecondName} (Age: {Age})";
    }

    /// <summary>
    /// Открыть меню студента, нам не нужна реализация ToString() от Menu, поэтому не наследуем
    /// </summary>
    /// <param name="readOnly"></param>
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
    
    /// <summary>
    /// Перевод студента в строку
    /// </summary>
    /// <returns>Текстовая репрезентация студента</returns>
    public override string ToString()
    {
        return $"0: {Strings.NameSemicolon}{FirstName}\n1: {Strings.SecondNameSemicolon}{SecondName}\n2: {Strings.AgeSemicolon}{Age}";
    }
    
    
}

