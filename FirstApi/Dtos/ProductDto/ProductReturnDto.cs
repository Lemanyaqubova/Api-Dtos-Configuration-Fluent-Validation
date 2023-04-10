namespace FirstApi.Dtos.ProductDto
{
    public class ProductReturnDto
    {

        public string Name { get; set; }
        public double SalePrice { get; set; }
        public double CostPrice { get; set; }


        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
