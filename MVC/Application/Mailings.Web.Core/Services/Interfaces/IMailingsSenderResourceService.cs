using Mailings.Web.Domain.Dto;

namespace Mailings.Web.Core.Services.Interfaces;
public interface IMailingsSenderResourceService
{
    Task<MailingResponseDto> Send(MailingRequestDto requestDto);
}