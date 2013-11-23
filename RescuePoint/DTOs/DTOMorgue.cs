using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DTOMorgue
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    public int TotalBodies { get; set; }
    public int TotalMales { get; set; }
    public int TotalFemales { get; set; }
    public int TotalTeens { get; set; }
    public int TotalAdults { get; set; }
    public int TotalChilds { get; set; }
    public int TotalIdentified { get; set; }
    public int TotalUnidentified { get; set; }
    public string ContactNumber { get; set; }
    public DTOPersonList PersonList { get; set; }

}

public class DTOMorgueList : List<DTOMorgue>, IDisposable
{
    public void Dispose()
    {
    }
}
