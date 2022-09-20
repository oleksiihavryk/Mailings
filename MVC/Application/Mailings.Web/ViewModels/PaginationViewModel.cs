namespace Mailings.Web.ViewModels;
/// <summary>
///     Pagination view model
/// </summary>
public sealed class PaginationViewModel
{
    /// <summary>
    ///     Current page index of pagination model
    /// </summary>
    public int PageIndex { get; set; }
    /// <summary>
    ///     Total pages of pagination
    /// </summary>
    public int TotalPages { get; set; }
}