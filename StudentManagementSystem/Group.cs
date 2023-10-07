namespace StudentManagementSystem;

public sealed class Group : Menu, INamed
{
    public static uint FreeId;

    public string Name { get; }
    public IDictionary<uint, Student> Students { get; } = new Dictionary<uint, Student>();


    public readonly IList<uint> _headStudents = new List<uint>();
    public uint Id { get; }

    public Group(string name)
    {
        Name = name;
        Id = FreeId;
        FreeId += 1;
    }

    private void SetHeadStudent(uint id, bool isHeadStudent)
    {
        switch (isHeadStudent)
        {
            case true when Students.ContainsKey(id) && !_headStudents.Contains(id):
                _headStudents.Add(id);
                break;
            case false when Students.ContainsKey(id):
                _headStudents.Remove(id);
                break;
        }
    }

    private void AddStudent(Student student)
    {
        Students.Add(student.ID, student);
    }
    
    public Group AddHeadStudent(Student student)
    {
        if (!Students.ContainsKey(student.ID))
        {
            AddStudent(student);
        }

        if (!_headStudents.Contains(student.ID))
        {
            SetHeadStudent(student.ID, true);
        }

        return this;

    }

    string INamed.Name()
    {
        return Name;
    }
    
    protected override bool IsEmpty()
    {
        return Students.Count == 0;
    }

    protected override IDictionary<uint, INamed> Named_Dictionary()
    {
        return Students.ToDictionary(
            v => v.Key, 
            v => (INamed) v.Value
        );
    }

    protected override string ObjectName()
    {
        return Name;
    }

    protected override string ElementName(uint id)
    {
        if (_headStudents.Contains(id))
        {
            return $"{Students[id].Name()} {Strings.HeadStudent}";
        }
        else
        {
                return Students[id].Name();
        }
    }

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
                        var isHeadStudent = _headStudents.Contains(id);
                        Console.Write(isHeadStudent ? Strings.Demote : Strings.Promote);
                        Console.WriteLine(Strings.PromoteDemoteSecondPart);
                        bool exit = false;
                        while (!exit)
                        {
                            switch (ReadKey(Key.Yes, Key.No))
                            {
                                case Key.Yes:
                                    SetHeadStudent(selStudent.ID, !isHeadStudent);
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