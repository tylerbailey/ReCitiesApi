namespace ReCitiesApi.Models.Entities
{
    public class Page : BaseEntity, IEntity
    {
        public string UserId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
