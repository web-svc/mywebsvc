using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace PortalWeb.Models;

public partial class App
{
    public int Id { get; set; }

    public string Secret { get; set; } = null!;

    public int UserId { get; set; }

    public string? Name { get; set; }

    public string? TagLine { get; set; }

    public string? LogoUrl { get; set; }

    public string? RedirectUrl { get; set; }

    public virtual ICollection<AuthServer> AuthServers { get; set; } = new List<AuthServer>();

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
    [ValidateNever]
    public virtual User User { get; set; } = null!;

    public virtual ICollection<UserInApp> UserInApps { get; set; } = new List<UserInApp>();
}
