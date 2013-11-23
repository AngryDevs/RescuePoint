using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DTOMorgue
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Latitude { get; set; }
    public string Longtitude { get; set; }
    public string TotalBodies { get; set; }
    public string TotalMale { get; set; }
    public string TotalFemale { get; set; }
    public string TotalTeen { get; set; }
    public string TotalAdult { get; set; }
    public string TotalChild { get; set; }
    public string ContactNumber { get; set; }
    public DTOPersonList PersonList { get; set; }

}

public class DTOMorgueList : List<DTOMorgue>, IDisposable
{
    public void Dispose()
    {
    }
}
