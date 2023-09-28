using System.Runtime.InteropServices.ComTypes;

namespace StudentManagmentSystem;

public class Student
{
    public String FirstName { get; set; }
    public String SecondName { get; set; }
    public uint Age { get; set; }

    public uint ID { get; set; }
    
    public Student(String firstName, String secondName, uint age, uint id)
    {
        FirstName = firstName;
        SecondName = secondName;
        Age = age;
        ID = id;
    }

    public override string ToString()
    {
        return String.Format("{0} {1} (ID: {3}, Возраст: {2})", FirstName, SecondName, Age, ID);
    }  
    
}