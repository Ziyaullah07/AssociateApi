using System;
using System.Collections.Generic;

namespace AssociateApi.Models;

public partial class TblLocation
{
    public int LocationId { get; set; }

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public virtual ICollection<TblAssociate> TblAssociates { get; set; } = new List<TblAssociate>();
}
