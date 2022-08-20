using System.Collections;
using Microsoft.AspNetCore.Identity;

namespace Mailings.Authentication.Shared.Exceptions;
[Serializable]
public sealed class FailedSignUpException : Exception
{
    private readonly User _user;
    private readonly IdentityResult _identityResult;

    public override string Message =>
        $"Failed sign-up operation for user with username {_user.UserName}. " +
        $"Identity result: {_identityResult}";
    public override IDictionary Data => new Dictionary<string, object>()
    {
        ["user"] = _user,
        ["identity"] = _identityResult
    };

    public FailedSignUpException(
        User user,
        IdentityResult identityResult,
        Exception? inner = null)
        : base(string.Empty, inner)
    {
        _user = user;
        _identityResult = identityResult;
    }
}
