using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalWeb.Models;

public partial class State
{
    public int Id { get; set; }

    public int StateId { get; set; }

    public string Name { get; set; } = null!;
    [Required]
    [DisplayName("Country")]

    public int CountryId { get; set; }
    [ForeignKey("CountryId")]
    [ValidateNever]
    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
