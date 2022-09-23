using System.Runtime.Serialization;

namespace Mailings.Resources.Exceptions;

[Serializable]
internal sealed class UnknownRequestMailTypeException : Exception
{
    public UnknownRequestMailTypeException()
        : base()
    {
    }
    public UnknownRequestMailTypeException(string message, Exception? inner = null)
        : base(message, inner)
    {
    }

    protected UnknownRequestMailTypeException(
        StreamingContext context,
        SerializationInfo info)
        : base(info, context)
    {
    }

}