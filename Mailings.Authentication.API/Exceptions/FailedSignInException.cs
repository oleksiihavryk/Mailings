using System.Collections;
using System.Runtime.Serialization;
using Mailings.Authentication.Shared;
using Mailings.Authentication.Shared.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Mailings.Authentication.API.Exceptions;
[Serializable]
public sealed class FailedSignInException : Exception
{
    private readonly User _user;
    private readonly SignInResult _signInResult;

    public override string Message => base.Message ??
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

    private FailedSignInException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
        _user = info.GetObject<User>(nameof(_user));
        _signInResult = info.GetObject<SignInResult>(nameof(_signInResult));
    }
}