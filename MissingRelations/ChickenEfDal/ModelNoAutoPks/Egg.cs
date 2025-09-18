using System;
using System.Collections.Generic;

namespace ChickenEfDal.ModelNoAutoPks;

public partial class Egg
{
    public int Id { get; set; }

    public double Weight { get; set; }

    public int Color { get; set; }

    public int ChickenId { get; set; }
}
