using System.Runtime.Serialization;

namespace Mailings.Resources.API.Exceptions;
[Serializable]
public class UnknownResponseTypeException : Exception
{
    public UnknownResponseTypeException()
        : base()
    {
    }
    public UnknownResponseTypeException(string message)
        : base(message)
    {
    }
    public UnknownResponseTypeException(string? message, Exception? inner)
        : base(message, inner)
    {
    }
    protected UnknownResponseTypeException(StreamingContext context, SerializationInfo info)
        : base(info, context)
    {
    }

}