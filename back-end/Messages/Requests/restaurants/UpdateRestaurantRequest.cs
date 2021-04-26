namespace WebApi.Controllers
{
    public class UpdateRestaurantRequest
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string RestaurantAddress { get; set; }
        public string RestaurantEmail { get; set; }
        public string RestaurantPhone { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public string Base64Value { get; set; }
        public string ImageName { get; set; }
        public string ContentType { get; set; }
    }
}