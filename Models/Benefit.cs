using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TheStartupBuddyV3.Models
{
    public partial class Benefit
    {
        [Key]
        public int Id { get; set; }
        public string? BenefitName { get; set; } = null!;
        public string? Logo { get; set; } = null!;
        public string Worth { get; set; } = null!;
        public byte PackType { get; set; }
        public string? BenefitDescription { get; set; } = null!;
        public string? BenefitInfo { get; set; }
        public byte? ApplyTo { get; set; }
        public byte? ActionType { get; set; }
        public int? PriceBenefit{ get; set; }
        public Boolean IsPublished { get; set; }
        public int AssignToProgram { get; set; }
        public string? tags { get; set; }
        public int? AssignToLevel { get; set; }
        public int? AssignToCategory { get; set; }
        public DateTime? CreateDate { get; set; }
        public int ProgramGroupId { get; set; }
    }
}
