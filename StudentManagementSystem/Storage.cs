namespace StudentManagementSystem;

public sealed class Storage : Menu
{
    public readonly IDictionary<uint, Group> Groups = new Dictionary<uint, Group>();

    protected override bool IsEmpty()
    {
        return Groups.Count == 0;
    }

    protected override IDictionary<uint, INamed> Named_Dictionary()
    {
        return Groups.ToDictionary(
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
                    Groups.Add(group.Id, group);
                    break;
                case Key.Remove:
                    Console.Write(Strings.GroupIdToRemove);
                    id = ReadUInt();
                    Groups.Remove(id);
                    break;
                case Key.Select:
                    Console.Write(Strings.GroupIDToOpen);
                    id = ReadUInt();
                    if (Groups.TryGetValue(id, out var selGroup))
                    {
                        selGroup.OpenUi(readOnly: ref readOnly);
                    }
                    break;
            }

            
        }
    }
}