namespace StudentManagementSystem;

public sealed class Storage : Menu
{
    private readonly IDictionary<uint, Group> _groups = new Dictionary<uint, Group>();

    protected override bool IsEmpty()
    {
        return _groups.Count == 0;
    }

    protected override IDictionary<uint, INamed> Named_Dictionary()
    {
        return _groups.ToDictionary(
            v => v.Key,
            v => (INamed) v.Value
            );
    }

    protected override string ObjectName()
    {
        return Strings.Storage;
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
            }

            uint id;

            var a = readOnly 
                ? ReadKey(Key.Select, Key.Mode, Key.Exit) 
                : ReadKey(Key.Add, Key.Remove, Key.Select, Key.Mode, Key.Exit);

            switch (a)
            {
                case Key.Exit:
                    return;
                case Key.Mode:
                    readOnly = !readOnly;
                    break;
                case Key.Add:
                    Console.Write(Strings.NewGroup);
                    var group = new Group(ReadLine());
                    _groups.Add(group.Id, group);
                    break;
                case Key.Remove:
                    Console.Write(Strings.GroupIdToRemove);
                    id = ReadUInt();
                    _groups.Remove(id);
                    break;
                case Key.Select:
                    Console.Write(Strings.GroupIDToOpen);
                    id = ReadUInt();
                    if (_groups.TryGetValue(id, out var selGroup))
                    {
                        selGroup.OpenUi(readOnly: ref readOnly);
                    }
                    break;
            }

            
        }
    }
}