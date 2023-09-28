namespace StudentManagmentSystem;

public class HeadStudent : Student
{
    public HeadStudent(string firstName, string secondName, uint age, uint id) : base(firstName, secondName, age, id)
    {
        
    }
    
    public override string ToString()
    {
        return String.Format("Староста {0} {1} (ID: {3}, Возраст: {2})", FirstName, SecondName, Age, ID);
    }  
}