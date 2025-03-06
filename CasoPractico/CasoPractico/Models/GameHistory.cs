using System;
using System.Collections.Generic;

namespace CasoPractico.Models;

public partial class GameHistory
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public int? Score { get; set; }

    public int? Duration { get; set; }

    public DateTime? GameDate { get; set; }
}
