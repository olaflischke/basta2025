using System;
using System.Collections.Generic;

namespace ChickenNhDalNoAutoPks;

public partial class Chicken
{
    public virtual int Id { get; set; }
    public virtual string Name { get; set; } = null!;
    public virtual double Weight { get; set; }
    public virtual IList<Egg> Eggs { get; set; } = new List<Egg>();
}
