using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class Teacher
{
    public int TeachersId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Class> Classes { get; set; } = new List<Class>();
}
