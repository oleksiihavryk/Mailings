using System.Collections;
using Mailings.Resources.Domen.MailingService;

namespace Mailings.Resources.Domen.Exceptions;

[Serializable]
public class MailingRequestException : Exception
{
    private readonly MailingSendRequest _sendRequest;

    public override string Message 
        => base.Message ?? 
           "Mailing send request exception." + Environment.NewLine + 
           $"Send request: {_sendRequest}";
    public override IDictionary Data => 
        new Dictionary<string, object>()
        {
            ["request"] = _sendRequest
        };

    public MailingRequestException(MailingSendRequest sendRequest)
        :this (sendRequest, null, null)
    {
    }
    public MailingRequestException(
        MailingSendRequest sendRequest, 
        string message) 
        : this(sendRequest, message, null)
    {
    }
    public MailingRequestException(
        MailingSendRequest sendRequest,
        string? message, 
        Exception? inner)
        : base(message, inner)
    {
        _sendRequest = sendRequest;
    }
}