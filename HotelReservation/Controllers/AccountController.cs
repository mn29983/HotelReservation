using HotelReservation.Models;
using HotelReservation.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<AccountController> _logger;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        ILogger<AccountController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _logger = logger;

        InitializeRoles().Wait();
    }

    private async Task InitializeRoles()
    {
        string[] roleNames = { "Admin", "User" };

        foreach (var roleName in roleNames)
        {
            bool roleExists = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExists)
            {
                try
                {
                    // Create the role
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
                catch (Exception ex)
                {
                    // Log a warning or handle the exception as needed
                    // Duplicate role creation might not be an error, but you should be aware of it
                    _logger.LogWarning($"Failed to create role '{roleName}'. {ex.Message}");
                }
            }
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        var availableRoles = _roleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name });

        var model = new RegisterViewModel
        {
            AvailableRoles = availableRoles
        };

        return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserRole = model.SelectedRole
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.SelectedRole);

                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

        }


        model.AvailableRoles = _roleManager.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name });
        return View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginAsync(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }


    [HttpGet]
    [Authorize(Roles = "Admin, User")]
    public IActionResult ManageRoles()
    {
        if (User.IsInRole("Admin"))
        {
            return View("AllMedicine");
        }
        else if (User.IsInRole("User"))
        {
            return View("UserMedicine");
        }
        return RedirectToAction("Index", "Home");
    }


    // List Users
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ViewUsers()
    {
        var users = await _userManager.Users.ToListAsync();

        var usersWithRoles = new List<UsersViewModel>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var userWithRoles = new UsersViewModel
            {
                UserId = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Roles = roles.ToList()
            };
            usersWithRoles.Add(userWithRoles);
        }

        return View(usersWithRoles);
    }


    // Make User
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> MakeUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user != null)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, "Admin");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return RedirectToAction("ViewUsers");
            }
        }
        return NotFound();
    }


    // Make Admin
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> MakeAdmin(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user != null)
        {

            var result = await _userManager.RemoveFromRoleAsync(user, "User");

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Admin");
                return RedirectToAction("ViewUsers");
            }
        }
        return NotFound();
    }


    // Delete User
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user != null)
        {
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                // Handle success (e.g., redirect to user list)
                return RedirectToAction("ViewUsers");
            }
            else
            {
                // Handle errors
                // You might want to display error messages or log the errors
                return View("Error"); // You should create an error view
            }
        }

        // Handle case where the user was not found
        return NotFound();
    }
}
