using Mailings.Web.Shared.Dto;

namespace Mailings.Web.Services;
public interface IMailingsSenderResourceService
{
    Task<MailingResponseDto> Send(MailingRequestDto requestDto);
}