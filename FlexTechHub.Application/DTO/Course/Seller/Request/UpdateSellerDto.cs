namespace InventoryManagement.Application.DTO.Customer.Seller.Request
{
    public class UpdateSellerDto
    {
        public Guid SellerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string BusinessName { get; set; }
        public string DomainAddress { get; set; }
        public string BusinessType { get; set; }
        public string BusinessAddress { get; set; }
        public string BusinessCategory { get; set; }
        public string ContactEmail { get; set; }
        public string SocialMediaUrl { get; set; }
        public string WebsiteUrl { get; set; }
        public string AdminUserName { get; set; }
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
        public string AdminConfirmPassword { get; set; }
        public string AdminAddress { get; set; }
    }

}