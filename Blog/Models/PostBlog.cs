using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models
{
    public class PostBlog
    {
        public int Id { get; set; }
        public string Tilte { get; set; }
        public string SubTilte { get; set; }
        public string Body { get; set; }
        public DateTime CreationTime { get; set; }
        public string ImagePath { get; set; }
        public string AuthorId { get; set; }
        

        [ForeignKey("AuthorId")]
        public virtual IdentityUser User { get; set; }

    }
}
