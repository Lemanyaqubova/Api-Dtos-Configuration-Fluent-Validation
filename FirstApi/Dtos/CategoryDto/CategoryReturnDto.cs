namespace FirstApi.Dtos.CategoryDto
{
    public class CategoryReturnDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
