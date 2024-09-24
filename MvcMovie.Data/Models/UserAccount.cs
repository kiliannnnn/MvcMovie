using System;
using System.Collections.Generic;

namespace MvcMovie.Data.Models;

public partial class UserAccount
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public bool Deleted { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? DeleteDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
