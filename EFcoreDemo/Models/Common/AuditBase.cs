﻿using System.ComponentModel.DataAnnotations;

namespace EFcoreDemo.Models.Common
{
    public class AuditBase
    {
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }
    }
}
