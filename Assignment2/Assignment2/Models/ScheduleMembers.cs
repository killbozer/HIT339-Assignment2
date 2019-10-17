using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment2.Models
{
    public partial class ScheduleMembers
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("ScheduleID")]
        public int ScheduleId { get; set; }
        public string MemberEmail { get; set; }


    }
}
