﻿namespace Mailings.Web.API.ViewModels;

public class MailingResponseViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsSuccess { get; set; }
}