﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GepardOOD.Common.EntityValidationConstants.Beer;

namespace GepardOOD.Data.Models
{
    public class Beer
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

        [ForeignKey(nameof(BeerCategory))]
        public int BeerCategoryId { get; set; }

        [Required]
        public virtual BeerCategory BeerCategory { get; set; } = null!;

        [ForeignKey(nameof(Associate))]
        public Guid AssociateId { get; set; }

        [Required]
        public virtual Associate Associate { get; set; } = null!;

        [ForeignKey(nameof(Client))]
        public Guid? ClientId { get; set; }

        public virtual ApplicationUser? Client { get; set; }
    }
}
