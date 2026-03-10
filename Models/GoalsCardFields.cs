using System.ComponentModel.DataAnnotations;

namespace TheStartupBuddyV3.Models
{
    public class GoalsCardFields
    {
        [Key]
        public int IdCard { get; set; }
        public int? IdStep { get; set; }
        public string? help_text { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Byte FieldCode { get; set; }
        public Byte FieldCodeType { get; set; }
        public Boolean HasOption { get; set; }
        public String? Option { get; set; }
        public Byte DisplayOrder { get; set; }
        public String? SuccessMessage { get; set; }
        public string? Placeholder { get; set; }
        public string? TextButton { get; set; }
        public string? Type { get; set; }
        public string? TypeRule { get; set; }
        public string? Label { get; set; }
        public string? Value { get; set; }
        public bool IsRequired { get; set; }
        public bool IsDisabled { get; set; }
        public Byte MaxData { get; set; }
        public bool AllowMultiple { get; set; }
        public String? FilesType { get; set; }
        public int QuestionID { get; set; }
        public int Score { get; set; }
        public Boolean Visible { get; set; }
    }
}
