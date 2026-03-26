using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ERPAssessmentApp.Models
{
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Please enter an item name")]
        public string ItemName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Weight is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Weight must be greater than zero")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Weight { get; set; }

        public int? ParentItemId { get; set; }

        public bool IsProcessed { get; set; } = false;

        [ForeignKey("ParentItemId")]
        public virtual Item? ParentItem { get; set; } 

        public virtual ICollection<Item>? ChildItems { get; set; }
    }
}