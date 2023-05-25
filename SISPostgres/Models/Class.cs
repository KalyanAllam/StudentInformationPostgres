using System;
using System.Collections.Generic;

namespace SISPostgres.Models;

public partial class Class
{
    public int Classid { get; set; }

    public string? Classname { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
