namespace StudentManagementSystem;

/// <summary>
/// Приватный интерфейс меню
/// </summary>
public abstract class Menu
{
    /// <summary>
    /// Получить клавишу действия пользователя
    /// </summary>
    /// <param name="validKeys">Список валидных клавиш</param>
    /// <returns>Если список валидных клавиш не пустой, гарантированно возвращает одну их клавиш из списка, в противном
    /// случае может вернуть null, если маппинг для клавиши не назначен</returns>
    public static Key ReadKey(params Key[] validKeys)
    {
        while (true)
        {
            // TextReader не удастся здесь использовать, так как через него невозможно блокировать испольнение кода
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

    /// <summary>
    /// Обертка над Console.ReadLine(), гарантирующая не null String
    /// </summary>
    /// <returns>Строку введенную пользователем</returns>
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

    /// <summary>
    ///  Обертка над Console.ReadLine() & uint.TryParse, гарантирующая не null uint
    /// </summary>
    /// <returns>Число введенное пользователем</returns>
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
    
    /// <summary>
    /// Является ли меню пустым
    /// </summary>
    /// <returns>`true` если в меню нет элементов, иначе `false`</returns>
    protected abstract bool IsEmpty();
    
    /// <summary>
    /// Вернуть словарь INamed объектов меню
    /// </summary>
    /// <returns>Словарь INamed объектов меню</returns>
    protected abstract IDictionary<uint, INamed> Named_Dictionary();

    /// <summary>
    /// Вернуть имя объекта для отображения в меню
    /// </summary>
    /// <returns>Имя объекта для отображения в меню</returns>
    protected abstract string ObjectName();

    /// <summary>
    /// Вернуть имя элемента для отображения в меню по ID
    /// </summary>
    /// <param name="id">ID элемента</param>
    /// <returns>Имя элемента для отображения в мен</returns>
    protected virtual string ElementName(uint id)
    {
        return Named_Dictionary()[id].Name();
    }
    
    /// <summary>
    /// Перевод меню в строку
    /// </summary>
    /// <returns>Меню в виде строки</returns>
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