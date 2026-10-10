using CampusEats.Api.Dtos;
using CampusEats.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CampusEats.Api.Controllers;

[ApiController]                    // Automatically validates requests and returns 400 for invalid models
[Route("api/[controller]")]        // Base route: /api/menu
public class MenuController : ControllerBase
{
    // Dependency Injection (DI)
    private readonly IMenuService _svc;

    // Constructor
    public MenuController(IMenuService svc)
    {
        _svc = svc;    // Service is injected by ASP.NET Core DI container
    }

    // --------------------------------------------------------------------
    // GET: /api/menu
    // Returns all menu items
    // --------------------------------------------------------------------
    [HttpGet]
    public ActionResult<IEnumerable<MenuItemDto>> GetAll()
    {
        return Ok(_svc.GetAll());
    }

    // --------------------------------------------------------------------
    // GET: /api/menu/{id}
    // Returns a single menu item by ID
    // Example: GET /api/menu/1
    // --------------------------------------------------------------------
    [HttpGet("{id}")]
    public ActionResult<MenuItemDto> GetById(int id)
    {
        var item = _svc.GetById(id);

        if (item == null)
        {
            return NotFound();   // HTTP 404
        }

        return Ok(item);         // HTTP 200
    }

    // --------------------------------------------------------------------
    // POST: /api/menu
    // Creates a new menu item
    // --------------------------------------------------------------------
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public ActionResult<MenuItemDto> Create([FromBody] CreateMenuItemDto dto)
    {
        var created = _svc.Create(dto);

        return CreatedAtAction(
            nameof(GetById),      // Returns the location of the new resource
            new { id = created.Id },
            created
        ); // HTTP 201 Created
    }

    // --------------------------------------------------------------------
// PUT: /api/menu/{id}
// Updates an existing menu item
// Example: PUT /api/menu/1
// --------------------------------------------------------------------
[HttpPut("{id}")]
[Authorize(Roles = "Admin")]
public IActionResult Update(
    int id,
    [FromBody] CreateMenuItemDto dto)
{
    var ok = _svc.Update(id, dto);

    if (!ok)
    {
        return NotFound();    // HTTP 404
    }

    return NoContent();       // HTTP 204 (Updated successfully)
}

// --------------------------------------------------------------------
// DELETE: /api/menu/{id}
// Deletes a menu item by ID
// Example: DELETE /api/menu/1
// --------------------------------------------------------------------
[HttpDelete("{id}")]
[Authorize(Roles = "Admin")]
public IActionResult Delete(int id)
{
    var ok = _svc.Delete(id);

    if (!ok)
    {
        return NotFound();    // HTTP 404
    }

    return NoContent();       // HTTP 204 (Deleted successfully)
}

    
}