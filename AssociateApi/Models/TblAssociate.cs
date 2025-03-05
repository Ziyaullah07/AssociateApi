using System;
using System.Collections.Generic;

namespace AssociateApi.Models;

public partial class TblAssociate
{
    public int AssociateId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public int? LocationId { get; set; }

    public int? OccupationId { get; set; }

    public virtual TblLocation? Location { get; set; }

    public virtual TblOccupation? Occupation { get; set; }

    public virtual TblContactInfo? TblContactInfo { get; set; }
}
