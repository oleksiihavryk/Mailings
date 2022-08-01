using AutoMapper;
using Mailings.Resources.Domen.MailingService;
using Mailings.Resources.Domen.Models;
using Mailings.Resources.Shared.Dto;
using Mailings.Resources.Shared.Extensions;

namespace Mailings.Resources.API.Configurations;
public static class AutoMapperConfiguration
{
    public static MapperConfiguration CreateConfiguration()
        => new MapperConfiguration(config =>
        {
            config.CreateDoubleLinkedMap<MailingGroup, MailingGroupDto>();
            config.CreateDoubleLinkedMap<TextMail, TextMailDto>();
            config.CreateDoubleLinkedMap<HtmlMail, HtmlMailDto>();
            config.CreateDoubleLinkedMap<Mail, MailDto>();
            config.CreateDoubleLinkedMap<EmailAddress, EmailAddressDto>();
            config.CreateDoubleLinkedMap<EmailAddressTo, EmailAddressToDto>();
            config.CreateDoubleLinkedMap<EmailAddressFrom, EmailAddressFromDto>();
            config.CreateDoubleLinkedMap<HistoryNoteMailingGroup, HistoryNoteMailingGroupDto>();
        });
}