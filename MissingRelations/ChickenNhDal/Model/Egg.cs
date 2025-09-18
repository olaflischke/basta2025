using System;
using System.Collections.Generic;

namespace ChickenNhDal;

public partial class Egg
{
    public virtual int Id { get; set; }
    public virtual double Weight { get; set; }
    public virtual int Color { get; set; }
    public virtual int ChickenId { get; set; }
    public virtual Chicken Mother { get; set; }
}
