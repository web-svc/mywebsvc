using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace PortalWeb.Models;

public partial class AuthServer
{
    public int Id { get; set; }

    public int AppId { get; set; }

    public byte RouterId { get; set; }

    public string Key { get; set; } = null!;

    public string Secret { get; set; } = null!;
    [ValidateNever]
    public virtual App App { get; set; } = null!;
    [ValidateNever]
    public virtual Router Router { get; set; } = null!;
}
