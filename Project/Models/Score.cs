using System;
using System.Collections.Generic;

namespace Project.Models;

public partial class Score
{
    public int ScoresId { get; set; }

    public int StudentId { get; set; }

    public double Listening { get; set; }

    public double Speaking { get; set; }

    public double Reading { get; set; }

    public double Writing { get; set; }

    public int WeekNumber { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Student Student { get; set; } = null!;
}
