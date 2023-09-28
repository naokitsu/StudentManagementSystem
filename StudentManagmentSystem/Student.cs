namespace StudentManagmentSystem;

public class Student
{
    public String FirstName { get; set; }
    public String SecondName { get; set; }
    public uint Age { get; set; }

    public Student(String firstName, String secondName, uint age)
    {
        FirstName = firstName;
        SecondName = secondName;
        Age = age;
    }
    
}