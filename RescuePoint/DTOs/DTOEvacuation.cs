using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DTOEvacuation
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Longtitude { get; set; }
    public string Latitude { get; set; }
    public int CurrentPeople { get; set; }
    public int MaxPeople { get; set; }
    public int Food { get; set; }
    public int Water { get; set; }
    public int Medicine { get; set; }

}

public class DTOEvacuationList : List<DTOEvacuation>, IDisposable
{
    public void Dispose()
    {

    }
}
