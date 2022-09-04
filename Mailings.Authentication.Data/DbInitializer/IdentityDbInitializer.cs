using Mailings.Authentication.Shared;
using Mailings.Authentication.Shared.ClaimProvider;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Mailings.Authentication.Data.DbInitializer;
public class IdentityDbInitializer : IDbInitializer
{
    private readonly IOptions<AdminCredentials> _credentials;
    protected readonly UserManager<User> _userManager;
    protected readonly RoleManager<IdentityRole> _roleManager;
    protected readonly IHostingEnvironment _env;
    protected readonly IClaimProvider<User> _claimProvider;

    protected virtual User Admin =>
        new User()
        {
            FirstName = "Main",
            LastName = "Admin",
            Email = "mailer.admin@gmail.com",
            EmailConfirmed = true,
            UserName = "admin123",
        };
    protected virtual IDictionary<User, string> DefaultUsers => new Dictionary<User, string>()
    {
        [new User()
        {
            FirstName = "Ordinary",
            LastName = "Man",
            Email = "test.email2@gmail.com",
            EmailConfirmed = true,
            UserName = "defaultUser",
        }] = "kof94sjo3v",
    };

    public IdentityDbInitializer(
        UserManager<User> userManager, 
        RoleManager<IdentityRole> roleManager,
        IHostingEnvironment env,
        IClaimProvider<User> claimProvider, 
        IOptions<AdminCredentials> credentials)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _env = env;
        _claimProvider = claimProvider;
        _credentials = credentials;
    }

    public virtual async Task InitializeAsync()
    {
        await PopulateRolesIfItsPossibleAsync();
        await PopulateAdminsIfItsPossibleAsync();

        if (_env.IsDevelopment())
        {
            await PopulateDefaultUsersIfItsPossibleAsync();
        }
    }
    private async Task PopulateDefaultUsersIfItsPossibleAsync()
    {
        foreach (var user in DefaultUsers)
        {
            if ((await _userManager.FindByNameAsync(user.Key.UserName)) == null)
            {
                await _userManager.CreateAsync(user.Key, user.Value);
                await _userManager.AddToRoleAsync(user.Key, Roles.Default.ToString());
                await _claimProvider.ProvideClaimsAsync(user.Key, Roles.Default);
            }
        }
    }
    private async Task PopulateAdminsIfItsPossibleAsync()
    {
        Admin.UserName = _credentials.Value.Login;
        var pass = _credentials.Value.Password;

        if ((await _userManager.FindByNameAsync(Admin.UserName)) == null)
        {
            await _userManager.CreateAsync(Admin, pass);
            await _userManager.AddToRoleAsync(Admin, Roles.Administrator.ToString());
            await _claimProvider.ProvideClaimsAsync(Admin, Roles.Administrator);
        }
    }
    private async Task PopulateRolesIfItsPossibleAsync()
    {
        foreach (var role in Enum.GetNames<Roles>())
        {
            if ((await _roleManager.FindByNameAsync(role)) == null)
            {
                var identityRole = new IdentityRole(role);
                await _roleManager.CreateAsync(identityRole);
            }
        }
    }
}