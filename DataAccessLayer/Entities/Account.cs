using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class Account
{
    public int Id { get; set; }

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public string Description { get; set; } = null!;

    public int? Role { get; set; }
}
