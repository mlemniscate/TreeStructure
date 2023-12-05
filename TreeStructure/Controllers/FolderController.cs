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
        var folders = await context.Folders.Include(f => f.Parent).ToListAsync();
        var virtualRootNode = folders.ToTree((parent, child) => child.ParentId == parent.Id);

        return tree.Children;
    }
}