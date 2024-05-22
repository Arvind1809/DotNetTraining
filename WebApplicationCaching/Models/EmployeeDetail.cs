using System;
using System.Collections.Generic;

namespace WebApplicationCaching.Models;

public partial class EmployeeDetail
{
    public string Name { get; set; } = null!;

    public int? RoleId { get; set; }

    public int Id { get; set; }
}
