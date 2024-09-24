using System;
using System.Collections.Generic;

namespace MvcMovie.Data.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<UserAccount> Users { get; set; } = new List<UserAccount>();
}
