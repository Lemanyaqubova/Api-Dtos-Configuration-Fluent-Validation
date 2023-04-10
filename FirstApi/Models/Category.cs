namespace FirstApi.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDelete { get; set; }
    }
}
