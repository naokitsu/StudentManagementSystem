namespace StudentManagementSystem;

/// <summary>
/// Клавиши управления пользователя
/// </summary>
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

/// <summary>
/// Класс отвечающий за маппинг клавиш к действиям
/// </summary>
public static class Keymap
{
    /// <summary>
    /// Получить клавишу управления по ConsoleKey
    /// </summary>
    /// <param name="consoleKey">Клавиша пользователя</param>
    /// <returns>Клавиша действия</returns>
    public static Key? ConsoleKey2Key(ConsoleKey consoleKey)
    {
        return consoleKey switch
        {
            ConsoleKey.A => Key.Add,
            ConsoleKey.R => Key.Remove,
            ConsoleKey.S => Key.Select,
            ConsoleKey.D0 => Key.Zero,
            ConsoleKey.D1 => Key.One,
            ConsoleKey.D2 => Key.Two,
            ConsoleKey.D3 => Key.Three,
            ConsoleKey.M => Key.Mode,
            ConsoleKey.E => Key.Exit,
            ConsoleKey.T => Key.Type,
            ConsoleKey.Y => Key.Yes,
            ConsoleKey.N => Key.No,
            _ => null
        };
    }
}