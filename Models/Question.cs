using System;
using System.Collections.Generic;

namespace TheStartupBuddyV3.Models
{
    public partial class Question
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Controltype { get; set; } = null!;
        public int Tab { get; set; }
        public string Options { get; set; } = null!;
        public bool Isdisable { get; set; }
        public int Displayorder { get; set; }
        public int Qid { get; set; }
    }
}
