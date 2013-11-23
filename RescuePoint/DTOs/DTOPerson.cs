using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DTOPerson
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public string AgeBracket { get; set; }
    public bool Identified { get; set; }
}

public class DTOPersonList : List<DTOPerson>, IDisposable
{
    public void Dispose()
    {
    }
}
