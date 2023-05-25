using System;
using System.Collections.Generic;

namespace SISPostgres.Models;

public partial class Enrollment
{
    public int Enrollmentid { get; set; }

    public int? Courseid { get; set; }

    public int? Studentid { get; set; }

    public int? Termid { get; set; }

    public int? Marks { get; set; }

    public int? Marksobtained { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Student? Student { get; set; }

    public virtual Term? Term { get; set; }

    public string? Grade { get; set; }


}
