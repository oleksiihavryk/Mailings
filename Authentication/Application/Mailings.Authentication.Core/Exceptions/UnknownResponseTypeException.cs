using System.Runtime.Serialization;

namespace Mailings.Authentication.Core.Exceptions;
/// <summary>
///     Unknown response type exception model
/// </summary>
[Serializable]
internal sealed class UnknownResponseTypeException : Exception
{
    /// <summary>
    ///     Message of exception
    /// </summary>
    public override string Message => base.Message ??
                                      "Unknown response type of ResponseDto." +
                                      "Check what exactly you choose as a response type " +
                                      "of your ResponseDto";

    public UnknownResponseTypeException()
        : base()
    {
    }
    public UnknownResponseTypeException(string message)
        : base(message)
    {
    }
    public UnknownResponseTypeException(
        string? message, 
        Exception? inner)
        : base(message, inner)
    {
    }

    private UnknownResponseTypeException(
        StreamingContext context, 
        SerializationInfo info)
        : base(info, context)
    {
    }
}