using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace expr2.Pages.Models;

public partial class UsageRequest
{
    [Display(Name = "领用流水")]
    public int RequestId { get; set; }

    [Display(Name = "物品名称")]
    [Required(ErrorMessage = "请输入物品名称")]
    public int? ItemId { get; set; }

    [Display(Name = "领用数量")]
    [Required(ErrorMessage = "请输入领用数量")]
    [Range(1, 100000000, ErrorMessage = "数量必须在 0 到 1000000 之间")]
    public int? Quantity { get; set; }

    [Display(Name = "领用人")]
    [Required(ErrorMessage = "请输入领用人")]
    public string? RequestedBy { get; set; }

    [Display(Name = "领用日期")]
    [DataType(DataType.Date)]
    public DateTime? RequestDate { get; set; }

    [Display(Name = "状态")]
    public string? Status { get; set; }

    public virtual Item? Item { get; set; }
}
