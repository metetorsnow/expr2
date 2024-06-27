using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace expr2.Pages.Models;

public partial class Stock
{
    [Display(Name = "入库流水号")]
    public int StockId { get; set; }

    [Display(Name = "物品名称")]
    [Required(ErrorMessage = "请输入物品名称")]
    public int? ItemId { get; set; }

    [Display(Name = "购买日期")]
    [Required(ErrorMessage = "请选择购买日期")]
    [DataType(DataType.Date)]
    public DateTime? PurchaseDate { get; set; }

    [Display(Name = "购买数量")]
    [Required(ErrorMessage = "请输入购买数量")]
    public int? PurchaseQuantity { get; set; }

    [Display(Name = "单价")]
    [Required(ErrorMessage = "请输入单价")]
    public decimal? UnitPrice { get; set; }

    [Display(Name = "总价")]
    public decimal? TotalPrice { get; set; }

    public virtual Item? Item { get; set; }
}
