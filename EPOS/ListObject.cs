using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPOS
{
    class ListObject
    {
    public ListObject(string name, int id)
        {
        Name = name;
        ID = id;
    }
 
    public string Name { get; set; }
    public int ID { get; set; }
}
}
