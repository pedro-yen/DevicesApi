using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevicesApi.Common.Utils
{
    public class KeysetPagedResult<T>
    {
        public required List<T> Items { get; set; }
        public Guid? LastSeenId { get; set; }
        public int PageSize { get; set; }
        public bool HasMore { get; set; }
    }
}
