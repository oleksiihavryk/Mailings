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
    }

    private async Task PopulateAdminsIfItsPossibleAsync()
    {
        var userName = _credentials.Value.Login;

        if ((await _userManager.FindByNameAsync(userName)) == null)
        {
            var admin = new User()
            {
                FirstName = "Main",
                LastName = "Admin",
                Email = "mailer.admin@gmail.com",
                EmailConfirmed = true,
                UserName = _credentials.Value.Login ?? "admin123",
            };
            var pass = _credentials.Value.Password;

            await _userManager.CreateAsync(admin, pass);
            await _userManager.AddToRoleAsync(admin, Roles.Administrator.ToString());
            await _claimProvider.ProvideClaimsAsync(admin, Roles.Administrator);
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