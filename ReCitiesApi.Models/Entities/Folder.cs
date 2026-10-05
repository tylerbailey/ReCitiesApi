using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ReCitiesApi.Models.Entities
{
    public class Folder: BaseEntity, IEntity
    {
        public string UserId { get; set; } = string.Empty;

        [ForeignKey("Parent")]
        public int? ParentId { get; set; }

        public string Name { get; set; } = string.Empty;

        public virtual Folder? Parent { get; set; }

        public virtual ICollection<Page> Pages { get; set; } = [];

        public virtual ICollection<Folder> Folders { get; set; } = [];
    
    }
}
