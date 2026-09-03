using System;
using System.Collections.Generic;
using System.Text;

namespace ReCitiesApi.Models.Entities
{
    public class Folder: BaseEntity, IEntity
    {
        public string UserId { get; set; } = string.Empty;

        public int? ParentId { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<Page> Pages { get; set; } = [];

        public ICollection<Folder> Folders { get; set; } = [];
    
    }
}
