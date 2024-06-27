using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace expr2.Pages.Models;

public partial class Item
{
    [Display(Name = "物品编号")]
    [Required(ErrorMessage = "请输入物品编号")]
    public int ItemId { get; set; }

    [Display(Name = "物品名称")]
    [Required(ErrorMessage = "请输入物品名称")]
    public string ItemName { get; set; } = null!;

    [Display(Name = "物品类别")]
    public string? CategoryId { get; set; }

    [Display(Name = "产地")]
    public string? Origin { get; set; }

    [Display(Name = "规格")]
    public string? Specification { get; set; }

    [Display(Name = "型号")]
    public string? Model { get; set; }

    [Display(Name = "物品图片")]
    public string? ItemImage { get; set; }

    [Display(Name = "数量")]
    [Required(ErrorMessage = "请输入物品数量")]
    [Range(0, 100000000, ErrorMessage = "数量必须在 0 到 1000000 之间")]
    public int Amount { get; set; }

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    public virtual ICollection<UsageRequest> UsageRequests { get; set; } = new List<UsageRequest>();
}
