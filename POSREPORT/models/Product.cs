namespace POSREPORT.models
{
    public class Product
    {
        private int id;
        private string code;
        private string name;
        private string description;
        private bool isService;
        private int brandId;
        private int categoryId;
        private int unitMeasureId;
        private bool isAvailable;
        private string createdAt;

        public int Id { get => id; set => id = value; }
        public string Code { get => code; set => code = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public bool IsService { get => isService; set => isService = value; }
        public int BrandId { get => brandId; set => brandId = value; }
        public int CategoryId { get => categoryId; set => categoryId = value; }
        public int UnitMeasureId { get => unitMeasureId; set => unitMeasureId = value; }
        public bool IsAvailable { get => isAvailable; set => isAvailable = value; }
        public string CreatedAt { get => createdAt; set => createdAt = value; }

        public Product(int id, string code, string name, string description, bool isService, int brandId, int categoryId, int unitMeasureId, bool isAvailable, string createdAt)
        {
            this.Id = id;
            this.Code = code;
            this.Name = name;
            this.Description = description;
            this.IsService = isService;
            this.BrandId = brandId;
            this.CategoryId = categoryId;
            this.UnitMeasureId = unitMeasureId;
            this.IsAvailable = isAvailable;
            this.CreatedAt = createdAt;
        }
    }
}
