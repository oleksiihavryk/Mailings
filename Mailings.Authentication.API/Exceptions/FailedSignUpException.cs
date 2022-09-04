using System.Collections;
using System.Runtime.Serialization;
using Mailings.Authentication.Shared;
using Mailings.Authentication.Shared.Extensions;
using Microsoft.AspNetCore.Identity;

namespace Mailings.Authentication.API.Exceptions;
[Serializable]
public sealed class FailedSignUpException : Exception
{
    private readonly User _user;
    private readonly IdentityResult _identityResult;

    public override string Message => base.Message ??
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