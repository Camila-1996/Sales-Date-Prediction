﻿// ---------------------------------------
// ---
// ---
// ---
// ---------------------------------------

namespace WEB_APIv1.Server.ViewModels.Shop
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public decimal BuyingPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public int UnitsInStock { get; set; }
        public bool IsActive { get; set; }
        public bool IsDiscontinued { get; set; }
        public string? ProductCategoryName { get; set; }
    }
}
