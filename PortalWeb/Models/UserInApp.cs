using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace PortalWeb.Models;

public partial class UserInApp
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int AppId { get; set; }
    [ValidateNever]
    public virtual App App { get; set; } = null!;
    [ValidateNever]

    public virtual User User { get; set; } = null!;
}
