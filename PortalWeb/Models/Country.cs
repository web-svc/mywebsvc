using System;
using System.Collections.Generic;

namespace PortalWeb.Models;

public partial class Country
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<State> States { get; set; } = new List<State>();
}
