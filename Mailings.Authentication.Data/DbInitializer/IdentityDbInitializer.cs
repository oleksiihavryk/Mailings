using System.Security.Claims;
using IdentityModel;
using Mailings.Authentication.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace Mailings.Authentication.Data.DbInitializer;
public class IdentityDbInitializer : IDbInitializer
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IHostingEnvironment _env;

    protected virtual IDictionary<User, string> Admins => new Dictionary<User, string>()
    {
        [new User()
        {
            FirstName = "Main",
            LastName = "Admin",
            Email = "mailer.admin@gmail.com",
            EmailConfirmed = true,
            UserName = "admin123",
        }] = "admin123",
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
    protected virtual IDictionary<User, string> BetaTesters
        => new Dictionary<User, string>()
        {
            [new User()
            {
                FirstName = "First",
                LastName = "Beta tester",
                Email = "test.email3@gmail.com",
                EmailConfirmed = true,
                UserName = "betatester1",
            }] = "dSfo19fjFA",
            [new User()
            {
                FirstName = "Second",
                LastName = "Beta tester",
                Email = "test.email4@gmail.com",
                EmailConfirmed = true,
                UserName = "betatester2",
            }] = "Ksa92S37f0",
            [new User()
            {
                FirstName = "Thirst",
                LastName = "Beta tester",
                Email = "test.email5@gmail.com",
                EmailConfirmed = true,
                UserName = "betatester3",
            }] = "20SJ72SNn8",
        };

    public IdentityDbInitializer(
        UserManager<User> userManager, 
        RoleManager<IdentityRole> roleManager,
        IHostingEnvironment env)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _env = env;
    }


    public virtual async Task InitializeAsync()
    {
        await PopulateRolesIfItsPossibleAsync();
        await PopulateAdminsIfItsPossibleAsync();
        await PopulateBetaTestersIfItsPossibleAsync();

        if (_env.IsDevelopment())
        {
            await PopulateDefaultUsersIfItsPossibleAsync();
        }
    }

    private async Task PopulateBetaTestersIfItsPossibleAsync()
    {
        foreach (var user in BetaTesters)
        {
            if ((await _userManager.FindByNameAsync(user.Key.UserName)) == null)
            {
                await _userManager.CreateAsync(user.Key, user.Value);
                await _userManager.AddToRoleAsync(user.Key, Roles.BetaTester.ToString());
                await PopulateClaimsToUserAsync(user.Key, Roles.BetaTester);
            }
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
                await PopulateClaimsToUserAsync(user.Key, Roles.Default);
            }
        }
    }
    private async Task PopulateAdminsIfItsPossibleAsync()
    {
        foreach (var user in Admins)
        {
            if ((await _userManager.FindByNameAsync(user.Key.UserName)) == null)
            {
                await _userManager.CreateAsync(user.Key, user.Value);
                await _userManager.AddToRoleAsync(user.Key, Roles.Administrator.ToString());
                await PopulateClaimsToUserAsync(user.Key, Roles.Administrator);
            }
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
    private async Task PopulateClaimsToUserAsync(User user, Roles role)
    {
        await _userManager.AddClaimAsync(
            user: user,
            claim: new Claim(
                type: JwtClaimTypes.GivenName,
                value: user.FirstName));
        await _userManager.AddClaimAsync(
            user: user,
            claim: new Claim(
                type: JwtClaimTypes.FamilyName,
                value: user.LastName));
        await _userManager.AddClaimAsync(
            user: user,
            claim: new Claim(
                type: JwtClaimTypes.Email,
                value: user.Email));
        await _userManager.AddClaimAsync(
            user: user,
            claim: new Claim(
                type: JwtClaimTypes.EmailVerified,
                value: user.EmailConfirmed.ToString()));
        await _userManager.AddClaimAsync(
            user: user,
            claim: new Claim(
                type: JwtClaimTypes.Id,
                value: user.Id));
        await _userManager.AddClaimAsync(
            user: user,
            claim: new Claim(
                type: JwtClaimTypes.PreferredUserName,
                value: user.UserName));
        await _userManager.AddClaimAsync(
            user: user,
            claim: new Claim(
                type: JwtClaimTypes.Role,
                value: role.ToString()));
    }
}