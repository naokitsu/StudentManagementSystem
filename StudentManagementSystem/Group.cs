namespace StudentManagementSystem;

public class Group
{
    public string Name { get; set; }
    public IDictionary<uint, Student> Students { get; } = new Dictionary<uint, Student>();
    public IList<uint> HeadStudents = new List<uint>();
    public uint ID { get; set; }

    public Group(String name, uint id)
    {
        Name = name;
        ID = id;
    }
    
    public Group SetHeadStudent(uint id, bool isHeadStudent)
    {
        if (isHeadStudent && Students.ContainsKey(id) && !HeadStudents.Contains(id))
        {
            HeadStudents.Add(id);
        } else if (!isHeadStudent && Students.ContainsKey(id))
        {
           HeadStudents.Remove(id);
        }

        return this;
    }
    
    public Group AddStudent(Student student)
    {
        Students.Add(student.ID, student);
        return this;
    }
    
    public Group AddHeadStudent(Student student)
    {
        if (!Students.ContainsKey(student.ID))
        {
            AddStudent(student);
        }

        if (!HeadStudents.Contains(student.ID))
        {
            SetHeadStudent(student.ID, true);
        }

        return this;

    }
    
    public override string ToString()
    {
        String ret = "";
        foreach (var (id, student) in Students)
        {
            if (HeadStudents.Contains(id))
            {
                ret += $"\n- {student} (Староста)";
            }
            else
            {
                ret += $"\n- {student}";
            }
        }
        return $"Группа {Name} (ID: {ID}):{ret}";
    }  
}