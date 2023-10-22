using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrometheusMVPProvider.Data;
internal class Junkyard
{
    public int JunkyardId { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }

    public List<Junk> Junks { get; set; }
}

internal class Junk
{
    public int JunkId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int JunkyardId { get; set; }
    public Junkyard Junkyard { get; set; }
}
