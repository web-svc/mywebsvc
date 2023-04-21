using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace PortalWeb.Models;

public partial class Role
{
    public int Id { get; set; }

    public int AppId { get; set; }

    public string InnerName { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;
    [ValidateNever]
    public virtual App App { get; set; } = null!;

    public virtual ICollection<UserInRole> UserInRoles { get; set; } = new List<UserInRole>();
}
