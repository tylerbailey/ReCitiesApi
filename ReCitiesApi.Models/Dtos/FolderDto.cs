using System;
using System.Collections.Generic;
using System.Text;

namespace ReCitiesApi.Models.Dtos
{
    public class FolderDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int? ParentId { get; set; }

        public List<FolderDto> SubFolders { get; set; } = [];

        public List<PageDto> Pages { get; set; } = [];
    }
}
