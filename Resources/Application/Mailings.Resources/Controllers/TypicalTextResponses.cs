namespace Mailings.Resources.Controllers;
internal static class TypicalTextResponses
{
    public static string EntityNotFoundById 
        => "Entity by current id is not found in system";
    public static string UnknownUserIdOrMissingContentByUserId
        => "User id is does not exist on system or " +
           "user with this id is not does not have any entities on system";
    public static string IncorrectClientInput => "Incorrect client input. " +
                                                 "Value which has been received from " +
                                                 "client is incorrect.";
}