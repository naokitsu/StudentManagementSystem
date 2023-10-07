namespace StudentManagementSystem;

/// <summary>
/// Интерфейс для обектов, которые могут храниться в Menu
/// </summary>
public interface INamed
{
    /// <summary>
    /// Вернуть имя объекта
    /// </summary>
    /// <returns>Имя объекта</returns>
    public string Name();
}