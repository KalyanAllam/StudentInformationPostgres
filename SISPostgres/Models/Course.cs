using System;
using System.Collections.Generic;

namespace SISPostgres.Models;

public partial class Course
{
    public int Courseid { get; set; }

    public string? Title { get; set; }

    public int? Facultyid { get; set; }

    public string? Name { get; set; }

    public string? Credits { get; set; }

    public int? Classid { get; set; }

    public virtual Class? Class { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Faculty? Faculty { get; set; }
}
