using System.ComponentModel.DataAnnotations.Schema;

namespace ReCitiesApi.Models.Entities
{
    public class Page : BaseEntity, IEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        [ForeignKey("Folder")]
        public int FolderId { get; set; }

        public virtual Folder? Folder { get; set; }

    }
}
