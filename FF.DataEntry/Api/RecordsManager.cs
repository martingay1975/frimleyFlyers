using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.DataEntry.Api
{
    public class RecordsManager
    {
        public RecordsManager(List<Record> records)
        {
            Records = records;
        }
        public List<Record> Records { get; }
    }
}
