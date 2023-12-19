using Microsoft.AspNetCore.Mvc.Rendering;

namespace Entities.ViewModels;

public class UserEditVM
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public SelectList Roles { get; set; }
}