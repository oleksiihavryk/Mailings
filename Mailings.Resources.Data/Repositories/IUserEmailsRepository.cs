﻿using Mailings.Resources.Domain.MainModels;

namespace Mailings.Resources.Data.Repositories;
public interface IUserEmailsRepository : IRepository<UserMails, Guid>
{
}