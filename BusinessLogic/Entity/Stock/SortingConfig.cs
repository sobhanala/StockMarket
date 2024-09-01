namespace api.BusinessLogic.Entity.Stock
{
    public class SortingConfig
    {
        public bool IsDescending { get; set; } = false;
        public SortingField? SortingField { get; set; } = null;
    }
}