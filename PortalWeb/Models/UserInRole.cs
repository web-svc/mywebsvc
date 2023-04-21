using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace PortalWeb.Models;

public partial class UserInRole
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int RoleId { get; set; }
    [ValidateNever]
    public virtual Role Role { get; set; } = null!;
    [ValidateNever]

    public virtual User User { get; set; } = null!;
}
