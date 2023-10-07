namespace StudentManagementSystem;

/// <summary>
/// Класс группы, хранит в себе объекты студентов и информацию о старостах
/// </summary>
public sealed class Group : Menu, INamed
{
    /// <summary>
    /// Свободный ID, так как каждая группа должна обладать уникальным ID (чтобы мы могли гарантирванно упаковывать их
    /// в словари без колизий), мы используем статическое поле, которое хранит следующий свободный ID 
    /// </summary>
    public static uint FreeId;

    /// <summary>
    /// Имя группы
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// Словарь ID-Студент, хранящий студентов
    /// </summary>
    public IDictionary<uint, Student> Students { get; } = new Dictionary<uint, Student>();
    
    /// <summary>
    /// Список ID старост
    /// </summary>
    public readonly IList<uint> HeadStudents = new List<uint>();
    
    /// <summary>
    /// ID Группы
    /// </summary>
    public uint Id { get; }

    /// <summary>
    /// Конструктор группы
    /// </summary>
    /// <param name="name">Имя группы</param>
    public Group(string name)
    {
        Name = name;
        Id = FreeId;
        FreeId += 1; // Обновляем свободный ID для следующей группы
    }

    /// <summary>
    /// Добавляет/Убирает слудента из списка старост
    /// </summary>
    /// <param name="id">ID студента</param>
    /// <param name="isHeadStudent">Добавить или убрать студента в/из список(ка)</param>
    private void SetHeadStudent(uint id, bool isHeadStudent)
    {
        switch (isHeadStudent)
        {
            case true when Students.ContainsKey(id) && !HeadStudents.Contains(id):
                HeadStudents.Add(id);
                break;
            case false when Students.ContainsKey(id):
                HeadStudents.Remove(id);
                break;
        }
    }

    /// <summary>
    /// Добавить студента в группу
    /// </summary>
    /// <param name="student">Студент для добавления</param>
    private void AddStudent(Student student)
    {
        Students.Add(student.Id, student);
    }
    
    /// <summary>
    /// Добавить студента в группу, сделать его старостой
    /// </summary>
    /// <param name="student"></param>
    /// <returns></returns>
    public Group AddHeadStudent(Student student)
    {
        if (!Students.ContainsKey(student.Id))
        {
            AddStudent(student);
        }

        if (!HeadStudents.Contains(student.Id))
        {
            SetHeadStudent(student.Id, true);
        }

        return this;

    }

    /// <summary>
    /// Вернуть имя группы
    /// </summary>
    /// <returns>Имя группы</returns>
    string INamed.Name()
    {
        return Name;
    }
    
    /// <summary>
    /// Является группа пустой
    /// </summary>
    /// <returns>`true` если группа пуста, иначе `false`</returns>
    protected override bool IsEmpty()
    {
        return Students.Count == 0;
    }

    /// <summary>
    /// Вернуть словать &lt;uint, INamed&gt;, необходимый для вывода списка студентов
    /// </summary>
    /// <returns>Словать &amp;lt;uint, INamed&amp;gt;</returns>
    protected override IDictionary<uint, INamed> Named_Dictionary()
    {
        return Students.ToDictionary(
            v => v.Key, 
            v => (INamed) v.Value
        );
    }

    /// <summary>
    /// Имя объекта
    /// </summary>
    /// <returns>Имя объекта</returns>
    protected override string ObjectName()
    {
        return Name;
    }

    /// <summary>
    /// Вернуть имя студента по ID с его статусом старосты (если имеется)
    /// </summary>
    /// <param name="id">ID студента</param>
    /// <returns>Имя студента</returns>
    protected override string ElementName(uint id)
    {
        if (HeadStudents.Contains(id))
        {
            return $"{Students[id].Name()} {Strings.HeadStudent}";
        }
        else
        {
                return Students[id].Name();
        }
    }

    /// <summary>
    /// Открыть меню группы
    /// </summary>
    /// <param name="readOnly">Референс на переменную отвечающую за режим просмотра</param>
    public void OpenUi(ref bool readOnly)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine(ToString());
            Console.WriteLine(Strings.SelectTool);
            Console.WriteLine(Strings.BasicTools);
            if (readOnly)
            {
                Console.WriteLine(Strings.ReadModeTools);
            }
            else
            {
                Console.WriteLine(Strings.EditModeTool);
                Console.WriteLine(Strings.AddRemoveTools);
                Console.WriteLine(Strings.ChangeTypeTool);
            }

            uint id;

            var a = readOnly 
                ? ReadKey(Key.Select, Key.Mode, Key.Exit) 
                : ReadKey( Key.Select, Key.Add, Key.Remove, Key.Type, Key.Mode, Key.Exit);
            Student? selStudent;
            switch (a)
            {
                case Key.Exit:
                    return;
                case Key.Mode:
                    readOnly = !readOnly;
                    break;
                case Key.Add:
                    Console.WriteLine(Strings.NewStudent);
                    Console.Write(Strings.NameSemicolon);
                    var firstName = ReadLine();
                    Console.Write(Strings.SecondNameSemicolon);
                    var secondName = ReadLine();
                    Console.Write(Strings.AgeSemicolon);
                    var age = ReadUInt();
                    
                    var student = new Student(firstName, secondName, age);
                    AddStudent(student);
                    break;
                case Key.Remove:
                    Console.Write(Strings.StudentIDToRemove);
                    id = ReadUInt();
                    Students.Remove(id);
                    break;
                case Key.Type:
                    Console.Write(Strings.StudentIDToChangeType);
                    id = ReadUInt();
                    if (Students.TryGetValue(id, out selStudent))
                    {
                        Console.Write(Strings.PromoteDemoteFirstPart);
                        var isHeadStudent = HeadStudents.Contains(id);
                        Console.Write(isHeadStudent ? Strings.Demote : Strings.Promote);
                        Console.WriteLine(Strings.PromoteDemoteSecondPart);
                        bool exit = false;
                        while (!exit)
                        {
                            switch (ReadKey(Key.Yes, Key.No))
                            {
                                case Key.Yes:
                                    SetHeadStudent(selStudent.Id, !isHeadStudent);
                                    exit = true;
                                    break;
                            }
                        }
                        
                    }
                    break;
                case Key.Select:
                    Console.Write(Strings.StudentIDToOpen);
                    id = ReadUInt();
                    
                    if (Students.TryGetValue(id, out selStudent))
                    {
                        selStudent.StudentMenu(readOnly: ref readOnly);
                    }
                    break;
            }
        }
    }
}