using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreeStructure.Tree;

namespace TreeStructure.Controllers;

[ApiController]
[Route("[controller]")]
public class FolderController : Controller
{
    private readonly AppDbContext context;

    public FolderController(AppDbContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Folder>>> GetAllSimple()
    {
        var folders = await context.Folders.ToListAsync();
        folders.ForEach(f => Console.WriteLine(f.GetHashCode()));
        Console.WriteLine("______________________________________________");
        folders.First(f => f.ParentId == null).SubFolders.ToList().ForEach(f => Console.WriteLine(f.GetHashCode()));
        return folders;
    }

    [HttpGet("{id}/parents")]
    public async Task<ActionResult<ICollection<Folder>>> GetParents(Guid id)
    {
        var folders = await context.Folders.Include(f => f.Parent).ToListAsync();
        var rootFolder = folders.First(f => f.Parent == null);
        var flattenSubFolders = rootFolder.FlattenSubFolders();
        var folder = flattenSubFolders.First(fsf => fsf.Id == id);
        var parents = folder.GetParents();
        var parrentsDto = FoldersToDto(parents);
        return Ok(parrentsDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var folder = await context.Folders.FirstAsync(f => f.Id == id);
        context.Folders.Remove(folder);
        await context.SaveChangesAsync();
        return Ok();
    }

    private List<FolderDto> FoldersToDto(ICollection<Folder> folders)
    {
        var result = new List<FolderDto>();
        foreach (var folder in folders)
        {
            result.Add(new FolderDto
            {
                Id = folder.Id,
                Name = folder.Name,
                ParentId = folder.ParentId,
            });
        }
        return result;
    }
}

internal class FolderDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Folder Parent { get; set; }
    public Guid? ParentId { get; set; }
    public ICollection<Folder> SubFolders { get; } = new List<Folder>();
}