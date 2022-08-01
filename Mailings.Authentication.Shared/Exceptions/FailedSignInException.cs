using System.Collections;
using Microsoft.AspNetCore.Identity;

namespace Mailings.Authentication.Shared.Exceptions;
public class FailedSignInException : Exception
{
    private readonly User _user;
    private readonly SignInResult _signInResult;

    public override string Message =>
        $"Failed sign-up operation for user with username {_user.UserName}. " +
        $"Identity result: {_signInResult}";
    public override IDictionary Data => new Dictionary<string, object>()
    {
        ["user"] = _user,
        ["identity"] = _signInResult
    };

    public FailedSignInException(
        User user,
        SignInResult signInResult, 
        Exception? inner = null)
        : base(string.Empty, inner)
    {
        _user = user;
        _signInResult = signInResult;
    }
}