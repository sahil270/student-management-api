using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class PageFilterParams
    {
        [Range(1, int.MaxValue, ErrorMessage = $"{nameof(Page)} Must be grater than zero.")]
        [DefaultValue(1)]
        public int Page { get; set; } = 1;

        [Range(0, 100, ErrorMessage = $"{nameof(PageSize)} Must be a positive Number less than 100.")]
        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;
        public string? SearchKeyword { get; set; }
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; } = false;
    }
}
