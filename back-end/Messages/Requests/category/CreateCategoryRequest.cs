namespace WebApi.Controllers
{
    public class CreateCategoryRequest
    {
        public int? ParentId { get; set; }
        public string CategoryName { get; set; }
    }
}