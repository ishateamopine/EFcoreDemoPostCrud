using EFcoreDemo.Models.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFcoreDemo.Models.Domain
{
    public class Blog : AuditBase
    {
        [Key]
        public int BlogId { get; set; }

        [InverseProperty("Blog")]

        public List<Post> Posts { get; set; } = new();

        public string? Url { get; set; }
    }
}

