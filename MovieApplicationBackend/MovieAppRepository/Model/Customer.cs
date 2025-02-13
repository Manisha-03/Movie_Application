using System;
using System.Collections.Generic;

namespace MovieApplicationBackend.Models;

public partial class Customer
{
    public short CustomerId { get; set; }

    public byte? StoreId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public short? AddressId { get; set; }

    public bool? Active { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? LastUpdate { get; set; }

    public virtual Address? Address { get; set; }
}
