namespace ReCitiesApi.Models.Entities
{
    public class Neighborhood : BaseEntity, IEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}