using System;
using System.Collections.Generic;

namespace MovieApplicationBackend.Models;

public class Country
{
    public short CountryId { get; set; }

    public string? Country1 { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();
}
