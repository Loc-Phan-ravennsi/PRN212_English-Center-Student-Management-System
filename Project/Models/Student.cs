using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class Student
{
    public int StudentsId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int ClassId { get; set; }

    public virtual Class Class { get; set; } = null!;

    public virtual ICollection<Score> Scores { get; set; } = new List<Score>();
}
