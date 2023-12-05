namespace TreeStructure;

public class Folder
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Folder Parent { get; set; }
    public Guid? ParentId { get; set; }
    public ICollection<Folder> SubFolders { set; get; } = new List<Folder>();
}