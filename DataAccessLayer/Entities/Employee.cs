using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class Employee
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public int? Age { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }
}
