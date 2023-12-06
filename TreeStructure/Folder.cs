namespace TreeStructure;

public class Folder
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Folder Parent { get; set; }
    public Guid? ParentId { get; set; }
    public ICollection<Folder> SubFolders { get; } = new List<Folder>();

    public ICollection<Folder> GetParents()
    {
        if (Parent == null) return new List<Folder>();

        var collection = Parent.GetParents();
        collection.Add(this.Parent);
        return collection;
    }
}