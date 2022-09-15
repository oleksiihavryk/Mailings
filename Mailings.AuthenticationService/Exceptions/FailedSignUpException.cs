using System.Collections;
using System.Runtime.Serialization;
using Mailings.AuthenticationService.Domain;
using Mailings.AuthenticationService.Shared.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Mailings.AuthenticationService.Exceptions;
/// <summary>
///     Failed sign up exception model
/// </summary>
[Serializable]
public sealed class FailedSignUpException : Exception
{
    /// <summary>
    ///     Data of user which occurred exception
    /// </summary>
    private readonly User _user;
    /// <summary>
    ///     Identity result of exception
    /// </summary>
    private readonly IdentityResult _identityResult;

    /// <summary>
    ///     Message of exception
    /// </summary>
    public override string Message => base.Message ??
        $"Failed sign-up operation for user with username {_user.UserName}. " +
        $"Identity result: {_identityResult}";
    /// <summary>
    ///     Data of exception
    /// </summary>
    public override IDictionary Data => new Dictionary<string, object>()
    {
        ["user"] = _user,
        ["identity"] = _identityResult
    };

    public FailedSignUpException(
        User user,
        IdentityResult identityResult,
        Exception? inner = null)
        : base(null, inner)
    {
        _user = user;
        _identityResult = identityResult;
    }

    private FailedSignUpException(
        SerializationInfo info,
        StreamingContext context)
        : base(info, context)
    {
        _user = info.GetObject<User>(nameof(_user));
        _identityResult = info.GetObject<IdentityResult>(nameof(_identityResult));
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(_user), _user);
        info.AddValue(nameof(_identityResult), _identityResult);
    }
}