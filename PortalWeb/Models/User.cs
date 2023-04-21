using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace PortalWeb.Models;

public partial class User
{
    public int Id { get; set; }

    public string WebGuid { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string? Password { get; set; }

    public bool Status { get; set; }

    public byte? OauthRouter { get; set; }

    public byte LoginAttempt { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime UpdateDate { get; set; }

    public virtual ICollection<App> Apps { get; set; } = new List<App>();

    public virtual Customer? Customer { get; set; }

    public virtual Router? OauthRouterNavigation { get; set; }

    public virtual ICollection<UserInApp> UserInApps { get; set; } = new List<UserInApp>();

    public virtual ICollection<UserInRole> UserInRoles { get; set; } = new List<UserInRole>();
}
