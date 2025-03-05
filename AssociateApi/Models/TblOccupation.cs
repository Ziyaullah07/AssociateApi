using System;
using System.Collections.Generic;

namespace AssociateApi.Models;

public partial class TblOccupation
{
    public int OccupationId { get; set; }

    public string Occupation { get; set; } = null!;

    public string Department { get; set; } = null!;

    public virtual ICollection<TblAssociate> TblAssociates { get; set; } = new List<TblAssociate>();
}
