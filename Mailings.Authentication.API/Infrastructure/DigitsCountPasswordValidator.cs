using Mailings.Authentication.Shared;
using Microsoft.AspNetCore.Identity;

namespace Mailings.Authentication.API.Infrastructure;
public class DigitsCountPasswordValidator : PasswordValidator<User>
{
    public const int DefaultMinimalDigitCount = 2;

    public int MinimalDigitCount { get; }

    public DigitsCountPasswordValidator(int minimalDigitCount)
        : base()
    {
        MinimalDigitCount = minimalDigitCount;
    }
    public DigitsCountPasswordValidator()
        :this (DefaultMinimalDigitCount)
    {
    }

    public override async Task<IdentityResult> ValidateAsync(
        UserManager<User> manager, 
        User user, 
        string password)
    {
        var result = await base.ValidateAsync(manager, user, password);

        CheckOnDigitCount(password, MinimalDigitCount, ref result);

        return result;
    }

    protected virtual void CheckOnDigitCount(
        string password, 
        int minDigitCount, 
        ref IdentityResult result)
    {
        IdentityResult? newResult = null;

        int digitCount = 0;
        
        foreach (var letter in password)
            if (char.IsDigit(letter))
                digitCount++;

        if (digitCount < minDigitCount)
            newResult = CreateFailedIdentityResult(result.Errors, new IdentityError() 
                { 
                    Code = "", 
                    Description = 
                        $"Minimal digit count in password always need to be higher than {minDigitCount}"
                });
        else if (!result.Succeeded)
            newResult = CreateFailedIdentityResult(result.Errors);
        else 
            newResult = IdentityResult.Success;

        result = newResult ?? 
                 throw new InvalidOperationException(
                     "Impossible error in checking on count of digits in password");
    }
    protected IdentityResult CreateFailedIdentityResult(
        IEnumerable<IdentityError> errors, 
        IdentityError? newError = null)
    {
        if (newError != null)
        {
            var newErrors = new List<IdentityError>(errors);
            newErrors.Add(newError);
            return IdentityResult.Failed(newErrors.ToArray());
        }
        
        return IdentityResult.Failed(errors.ToArray());
    }
}
