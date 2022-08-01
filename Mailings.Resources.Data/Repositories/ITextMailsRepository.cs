using Mailings.Resources.Shared.Dto;

namespace Mailings.Resources.Data.Repositories;

public interface ITextMailsRepository : IRepository<TextMailDto, Guid>
{
}