using System;
using System.Collections.Generic;
using System.Text;

namespace ReCitiesApi.Models.Dtos
{
    public class PageDto
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public int FolderId { get; set; }
    }
}
