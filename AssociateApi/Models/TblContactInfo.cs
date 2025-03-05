using System;
using System.Collections.Generic;

namespace AssociateApi.Models;

public partial class TblContactInfo
{
    public int ContactId { get; set; }

    public int? AssociateId { get; set; }

    public string Email { get; set; } = null!;

    public string? ContactNo { get; set; }

    public string? Address { get; set; }

    public virtual TblAssociate? Associate { get; set; }
}
