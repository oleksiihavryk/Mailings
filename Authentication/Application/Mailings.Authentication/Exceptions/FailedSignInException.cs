using System.Collections;
using System.Runtime.Serialization;
using Mailings.Authentication.Domain;
using Mailings.Authentication.Shared.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Mailings.Authentication.Exceptions;
/// <summary>
///     Failed sign in exception model
/// </summary>
[Serializable]
public sealed class FailedSignInException : Exception
{
    /// <summary>
    ///     Data of user which occurred exception
    /// </summary>
    private readonly User _user;
    /// <summary>
    ///     Sign in result
    /// </summary>
    private readonly SignInResult _signInResult;

    /// <summary>
    ///     Message of exception
    /// </summary>
    public override string Message => base.Message ??
        $"Failed sign-up operation for user with username {_user.UserName}. " +
        $"Identity result: {_signInResult}";
    /// <summary>
    ///     Data of exception
    /// </summary>
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

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(_user), _user);
        info.AddValue(nameof(_signInResult), _signInResult);
    }
}