using System;
using System.Collections.Generic;

namespace SISPostgres.Models;

public partial class Faculty
{
    public int Facultyid { get; set; }

    public string? Facultyfirstname { get; set; }

    public string? Facultylastname { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
