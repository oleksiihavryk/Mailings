using Mailings.Authentication.Data.ClaimProvider;
using Mailings.Authentication.Domain;
using Mailings.Authentication.Shared.IdentityData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Mailings.Authentication.Data.DbInitializer;
/// <summary>
///     Implementation of database initializer service
/// </summary>
public class IdentityDbInitializer : IDbInitializer
{
    /// <summary>
    ///     Credentials of administrator user (what provides by default)
    /// </summary>
    protected readonly IOptions<AdminCredentials> _credentials;
    /// <summary>
    ///     Users manager service
    /// </summary>
    protected readonly UserManager<User> _userManager;
    /// <summary>
    ///     Roles manager service
    /// </summary>
    protected readonly RoleManager<IdentityRole> _roleManager;
    /// <summary>
    ///     Service that provides state of application environment
    /// </summary>
    protected readonly IHostingEnvironment _env;
    /// <summary>
    ///     Claim provider service
    /// </summary>
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

    /// <summary>
    ///     Implementation of database initilizer service
    ///     Initialization of database in async mode
    /// </summary>
    /// <returns>
    ///     Task of async operation by database initialization 
    /// </returns>
    public virtual async Task InitializeAsync()
    {
        await PopulateRolesIfItsPossibleAsync();
        await PopulateAdminsIfItsPossibleAsync();
    }

    /// <summary>
    ///     Populate administrator users in database
    /// </summary>
    /// <returns>
    ///     Task of async operation by admin users populating
    /// </returns>
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
    /// <summary>
    ///     Populate default set of roles in async mode
    /// </summary>
    /// <returns>
    ///     Task of async operation by role set populating
    /// </returns>
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