namespace StudentManagementSystem;


public abstract class Menu
{
    
    public static Key ReadKey(params Key[] validKeys)
    {
        while (true)
        {
            var keyOrNull = Keymap.ConsoleKey2Key(Console.ReadKey(true).Key);
            if (keyOrNull is not { } key) continue;
            if (validKeys.Length == 0)
            {
                return key;
            }
            if (validKeys.Contains(key))
            {
                return key;
            }
        }
    }

    public static string ReadLine()
    {
        while (true)
        {
            var line = Console.ReadLine();
            if (line != null)
            {
                return line;
            }
        }
    }

    public static uint ReadUInt()
    {
        while (true)
        {
            var line = Console.ReadLine();
            if (uint.TryParse(line, out var number))
            {
                return number;
            }
            
        }
    }
    
    protected abstract bool IsEmpty();
    protected abstract IDictionary<uint, INamed> Named_Dictionary();

    protected abstract string ObjectName();

    protected virtual string ElementName(uint id)
    {
        return Named_Dictionary()[id].Name();
    }
    public override string ToString()
    {
        var ret = "";
        if (!IsEmpty())
        {
            foreach (var (id, _) in Named_Dictionary())
            {
                ret += $"- ({id}) {ElementName(id)}\n";
            }
        }
        else
        {
            ret = "- " + Strings.Empty;
        }

        return ObjectName() + ":\n" + ret;
    }
}