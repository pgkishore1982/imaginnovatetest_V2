using System;
using System.Collections.Generic;

namespace imaginnovatetest.Models;

public partial class TblEmployee
{
    public int Empid { get; set; }

    public string? Firstname { get; set; }

    public string? Lastname { get; set; }

    public string? Email { get; set; }

    public string? Phonenumber { get; set; }

    public DateTime? Doj { get; set; }

    public decimal? Salary { get; set; }
}
