﻿// ---------------------------------------
// ---
// ---
// ---
// ---------------------------------------

using System.ComponentModel.DataAnnotations;

namespace WEB_APIv1.Core.Models
{
    public class BaseEntity : IAuditableEntity
    {
        public int Id { get; set; }

        [MaxLength(40)]
        public string? CreatedBy { get; set; }

        [MaxLength(40)]
        public string? UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
