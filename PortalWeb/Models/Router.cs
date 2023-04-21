using System;
using System.Collections.Generic;

namespace PortalWeb.Models;

public partial class Router
{
    public byte Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<AuthServer> AuthServers { get; set; } = new List<AuthServer>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
