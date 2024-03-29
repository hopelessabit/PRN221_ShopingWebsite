﻿using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string? CompanyName { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
