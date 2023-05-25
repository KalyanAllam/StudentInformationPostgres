using System;
using System.Collections.Generic;

namespace SISPostgres.Models;

public partial class Term
{
    public int Termid { get; set; }

    public string? Termname { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
