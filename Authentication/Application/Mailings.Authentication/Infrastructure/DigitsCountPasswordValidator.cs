using Mailings.Authentication.Domain;
using Microsoft.AspNetCore.Identity;

namespace Mailings.Authentication.Infrastructure;
/// <summary>
///     Service class of password validator used for provide a new rule of password validation,
///     minimal count of digits in password.
/// </summary>
public class DigitsCountPasswordValidator : PasswordValidator<User>
{
    /// <summary>
    ///     Default minimal count of digits.
    /// </summary>
    public const int DefaultMinimalDigitCount = 2;

    /// <summary>
    ///     Minimal digit count in service.
    /// </summary>
    public int MinimalDigitCount { get; }

    public DigitsCountPasswordValidator(int? minimalDigitCount = null)
        : base()
    {
        MinimalDigitCount = minimalDigitCount is null or < DefaultMinimalDigitCount ? 
            DefaultMinimalDigitCount : 
            (int)minimalDigitCount;
    }

    /// <summary>
    ///     Method for password validation.
    /// </summary>
    /// <param name="manager">
    ///     Users manager.
    /// </param>
    /// <param name="user">
    ///     Validated user.
    /// </param>
    /// <param name="password">
    ///     Password which validated.
    /// </param>
    /// <returns>
    ///     Result of validate operation.
    /// </returns>
    public override async Task<IdentityResult> ValidateAsync(
        UserManager<User> manager, 
        User user, 
        string password)
    {
        var result = await base.ValidateAsync(manager, user, password);

        CheckOnDigitCount(password, MinimalDigitCount, ref result);

        return result;
    }

    /// <summary>
    ///     Check password on digit count
    /// </summary>
    /// <param name="password">
    ///     Password which validated.
    /// </param>
    /// <param name="minDigitCount">
    ///     Minimal digit count.
    /// </param>
    /// <param name="result">
    ///     Returned operation result.
    /// </param>
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
        else newResult = IdentityResult.Success;

        result = newResult;
    }

    /// <summary>
    ///     Shortcut method for creating failed identity result
    /// </summary>
    /// <param name="errors">
    ///     All errors in identity result
    /// </param>
    /// <param name="newError">
    ///     New error of some operation while validating password
    /// </param>
    /// <returns>
    ///     New identity result of operation.
    /// </returns>
    private IdentityResult CreateFailedIdentityResult(
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