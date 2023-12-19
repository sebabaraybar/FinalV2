using System.Diagnostics;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SeriesBoxd.Models;

namespace SeriesBoxd.Controllers;
[Authorize(Roles = "Admin")]
public class RoleController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    public RoleController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        var roles = _roleManager.Roles.ToList();
        return View(roles);
    }

    //Get
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(RoleCreateVM roleCreateVM)
    {
        if (string.IsNullOrEmpty(roleCreateVM.RoleName))
        {
            return View();
        }
        var role = new IdentityRole(roleCreateVM.RoleName);
        _roleManager.CreateAsync(role);

        return RedirectToAction("Index");
    }
}
