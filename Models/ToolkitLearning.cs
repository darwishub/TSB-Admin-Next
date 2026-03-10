using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class ToolkitLearning
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? Type { get; set; }
        public string? Tag { get; set; }
        public byte[]? Image { get; set; }
        public bool Exclusive { get; set; }
        public int ProgramId { get; set; }
        public byte ContentType { get; set; }
        public string? ContentArticle { get; set; }
        public int? IdLevel { get; set; }
        public string? Logo { get; set; }
        public bool IsPublished { get; set; }
        public int Rewards { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? IdCategory { get; set; }
        public int ProgramGroupId { get; set; }
    }

    public partial class ToolkitLearningModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? Type { get; set; }
        public string? Tag { get; set; }
        public byte[]? Image { get; set; }
        public bool Exclusive { get; set; }
        public int ProgramId { get; set; }
        public byte ContentType { get; set; }
        public string? ContentArticle { get; set; }
        public int? IdLevel { get; set; }
        public string? Logo { get; set; }
        public bool IsPublished { get; set; }
        public int Rewards { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? IdCategory { get; set; }
        public string? CategoryLogo { get; set; }
        public int ProgramGroupId { get; set; }
    }
}
