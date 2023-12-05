namespace TreeStructure;

public static class Seed
{
    public static async Task SeedFolderTree(AppDbContext context)
    {
        if (context.Folders.Any()) return;
        var folders = new List<Folder>();

        var rootFolder = new Folder
        {
            Id = Guid.NewGuid(),
            Name = "Root"
        };

        var folder1 = new Folder
        {
            Id = Guid.NewGuid(),
            Name = "Folder1",
            ParentId = rootFolder.Id
        };
        rootFolder.SubFolders.Add(folder1);

        var folder1_1 = new Folder
        {
            Id = Guid.NewGuid(),
            Name = "Folder1.1",
            ParentId = folder1.Id
        };
        folder1.SubFolders.Add(folder1_1);

        var folder1_1_1 = new Folder
        {
            Id = Guid.NewGuid(),
            Name = "Folder1.1.1",
            ParentId = folder1_1.Id
        };
        folder1_1.SubFolders.Add(folder1_1_1);

        var folder1_1_1_1 = new Folder
        {
            Id = Guid.NewGuid(),
            Name = "Folder1.1.1.1",
            ParentId = folder1_1_1.Id
        };
        folder1_1_1.SubFolders.Add(folder1_1_1_1);

        var folder1_1_1_2 = new Folder
        {
            Id = Guid.NewGuid(),
            Name = "Folder1.1.1.2",
            ParentId = folder1_1_1.Id
        };
        folder1_1_1.SubFolders.Add(folder1_1_1_2);

        var folder1_1_2 = new Folder
        {
            Id = Guid.NewGuid(),
            Name = "Folder1.1.2",
            ParentId = folder1_1.Id
        };
        folder1_1.SubFolders.Add(folder1_1_2);

        var folder1_2 = new Folder
        {
            Id = Guid.NewGuid(),
            Name = "Folder1.2",
            ParentId = folder1.Id
        };
        folder1.SubFolders.Add(folder1_2);

        var folder2 = new Folder
        {
            Id = Guid.NewGuid(),
            Name = "Folder2",
            ParentId = rootFolder.Id
        };
        rootFolder.SubFolders.Add(folder2);
        
        folders.AddRange(rootFolder.FlattenSubFolders());

        context.Folders.AddRange(folders);
        await context.SaveChangesAsync();
    }

    // The FlattenSubFolders method is an extension method to flatten the hierarchy.
    public static IEnumerable<Folder> FlattenSubFolders(this Folder folder)
    {
        yield return folder;
        foreach (var subFolder in folder.SubFolders.SelectMany(f => f.FlattenSubFolders()))
        {
            yield return subFolder;
        }
    }
}