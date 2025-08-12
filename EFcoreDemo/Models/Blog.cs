
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFcoreDemo.Models
{
    public class Blog 
    {
        [Key]
        public int BlogId { get; set; }

        [InverseProperty("Blog")]

        public List<Post> Posts { get; set; } = new();

        public string? Url { get; set; }
        //add True OR False working below
         //public bool IsDeleted { get; set; } = false;
        //1-m
        //public virtual ICollection<Post> Posts { get; set; } = [];
    }
    public class RssBlog : Blog
    {
        public string RssUrl { get; set; } = null!;
    }
}

