namespace StudentManagementSystem;

public enum Key
{
    Add,
    Remove,
    Select,
    Zero,
    One,
    Two,
    Three,
    Mode,
    Exit,
    Type,
    Yes,
    No,
}

public static class Keymap
{
    public static Key? ConsoleKey2Key(ConsoleKey consoleKey)
    {
        switch (consoleKey)
        {
            case ConsoleKey.A:
                return Key.Add;
            case ConsoleKey.R:
                return Key.Remove;
            case ConsoleKey.S:
                return Key.Select;
            case ConsoleKey.D0:
                return Key.Zero;
            case ConsoleKey.D1:
                return Key.One;
            case ConsoleKey.D2:
                return Key.Two;
            case ConsoleKey.D3:
                return Key.Three;
            case ConsoleKey.M:
                return Key.Mode;
            case ConsoleKey.E:
                return Key.Exit;
            case ConsoleKey.T:
                return Key.Type;
            case ConsoleKey.Y:
                return Key.Yes;
            case ConsoleKey.N:
                return Key.No;
            default:
                return null;
        }
    }
}