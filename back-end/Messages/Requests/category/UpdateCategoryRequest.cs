namespace WebApi.Controllers
{
    public class UpdateCategoryRequest
    {
        public int CategoryId { get; set; }
        public int? ParentId { get; set; }
        public string CategoryName { get; set; }
        
    }
}