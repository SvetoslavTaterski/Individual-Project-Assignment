﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static GepardOOD.Common.EntityValidationConstants.Wine;

namespace GepardOOD.Data.Models
{
    public class Wine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(ManufacturerMaxLength)]
        public string Manufacturer { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(ImageUrlMaxLength)]
        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

		[ForeignKey(nameof(WineCategory))]
        public int WineCategoryId { get; set; }

        [Required]
        public virtual WineCategory WineCategory { get; set; } = null!;

        [ForeignKey(nameof(Associate))]
        public Guid AssociateId { get; set; }

        [Required]
        public virtual Associate Associate { get; set; } = null!;

        [ForeignKey(nameof(Client))]
        public Guid? ClientId { get; set; }

        public virtual ApplicationUser? Client { get; set; }
    }
}
