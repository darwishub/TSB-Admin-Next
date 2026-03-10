using System;
using System.ComponentModel.DataAnnotations;

public class CompanyGoalsCardFields
{
    [Key]
    public int IdCompanyGoalsCard { get; set; }
    public int IdCard { get; set; }
    public int? StartupId { get; set; }
    public string? IdUser { get; set; }
    public string? Value { get; set; }
    public DateTime DateCreated { get; set; }
    public int? QuestionID { get; set; }
    public int Score { get; set; }
    public bool IsCorrected { get; set; }
}

public class ScoreCorrectionInput
{
    public int CardId { get; set; }
    public int StartupId { get; set; }
    public int Score { get; set; }
}
