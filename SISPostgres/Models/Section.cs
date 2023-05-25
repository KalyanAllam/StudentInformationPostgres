using System;
using System.Collections.Generic;

namespace SISPostgres.Models;

public partial class Section
{
    public int Sectionid { get; set; }

    public string? Classsectionname { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
