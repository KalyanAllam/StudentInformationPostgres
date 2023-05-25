using System;
using System.Collections.Generic;

namespace SISPostgres.Models;

public partial class Student
{
    public int Studentid { get; set; }

    public int? Classid { get; set; }

    public string? Lastname { get; set; }

    public string? Firstname { get; set; }

    public int? Phoneno { get; set; }

    public string? Email { get; set; }

    public int? Sectionid { get; set; }

    public int? Rollno { get; set; }

    public virtual Class? Class { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual Section? Section { get; set; }
}
