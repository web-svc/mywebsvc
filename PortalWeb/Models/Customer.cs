using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace PortalWeb.Models;

public partial class Customer
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Company { get; set; } = null!;

    public int? CountryId { get; set; }

    public int? StateId { get; set; }

    public string City { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int ZipCode { get; set; }

    public long Phone { get; set; }

    public long Mobile { get; set; }

    public string? Description { get; set; }
    [ValidateNever]
    public virtual Country? Country { get; set; }
    [ValidateNever]
    public virtual State? State { get; set; }
    [ValidateNever]
    public virtual User User { get; set; } = null!;
}
