using System;
using System.Collections.Generic;

namespace ChickenEfDal.Model;

public partial class Chicken
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public double Weight { get; set; }

    public List<Egg> Eggs { get; set; } = new List<Egg>();
}
