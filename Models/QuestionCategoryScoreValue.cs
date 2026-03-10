using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class QuestionCategoryScoreValue
    {
        public int Questionid { get; set; }
        public string Category { get; set; } = null!;
        public bool Eligiblity { get; set; }
        public decimal Weight { get; set; }
        public decimal Value { get; set; }
        public string Valuetext { get; set; } = null!;
        public decimal Valuefrom { get; set; }
        public decimal Valueto { get; set; }
        public int Scorevalueid { get; set; }
    }
}
