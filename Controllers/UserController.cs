using System.Diagnostics;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SeriesBoxd.Models;

namespace SeriesBoxd.Controllers;
[Authorize(Roles = "Admin")]
public class UserController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public IActionResult Index()
    {
        var users = _userManager.Users.ToList();
        return View(users);
    }


    public async Task<IActionResult> Edit(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        var userVM = new UserEditVM();
        userVM.UserName = user.UserName ?? string.Empty;
        userVM.Email = user.Email ?? string.Empty;
        userVM.Roles = new SelectList(_roleManager.Roles.ToList());
        return View(userVM);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(UserEditVM userEditVM)
    {
        var user = await _userManager.FindByNameAsync(userEditVM.UserName);
        if (user != null)
        {
            await _userManager.AddToRoleAsync(user, userEditVM.Role);
        }

        return RedirectToAction("Index");
    }
}
