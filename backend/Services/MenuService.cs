using CampusEats.Api.Data;
using CampusEats.Api.Dtos;
using CampusEats.Api.Models;

namespace CampusEats.Api.Services;

public class MenuService : IMenuService
{
    private readonly AppDbContext _db;

    public MenuService(AppDbContext db)
    {
        _db = db;
    }

    private static MenuItemDto ToDto(MenuItem menuItem) =>
        new(
            menuItem.Id,
            menuItem.Name,
            menuItem.Price,
            menuItem.Category,
            menuItem.Available
        );

    public IEnumerable<MenuItemDto> GetAll()
    {
        return _db.MenuItems
            .OrderBy(menuItem => menuItem.Id)
            .ToList()
            .Select(ToDto);
    }

    public MenuItemDto? GetById(int id)
    {
        var menuItem = _db.MenuItems.Find(id);

        return menuItem is null
            ? null
            : ToDto(menuItem);
    }

    public MenuItemDto Create(CreateMenuItemDto dto)
    {
        var menuItem = new MenuItem
        {
            Name = dto.Name,
            Price = dto.Price,
            Category = dto.Category,
            Available = true
        };

        _db.MenuItems.Add(menuItem);
        _db.SaveChanges();

        return ToDto(menuItem);
    }

    public bool Update(int id, CreateMenuItemDto dto)
    {
        var menuItem = _db.MenuItems.Find(id);

        if (menuItem is null)
        {
            return false;
        }

        menuItem.Name = dto.Name;
        menuItem.Price = dto.Price;
        menuItem.Category = dto.Category;

        _db.SaveChanges();

        return true;
    }

    public bool Delete(int id)
    {
        var menuItem = _db.MenuItems.Find(id);

        if (menuItem is null)
        {
            return false;
        }

        _db.MenuItems.Remove(menuItem);
        _db.SaveChanges();

        return true;
    }
}