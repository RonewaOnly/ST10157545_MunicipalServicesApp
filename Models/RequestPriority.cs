using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10157545_MunicipalServicesApp.Models
{
    /**
     * <summary>
            Urgency level of a service request. The integer value doubles as the
            sort/heap key (higher value = more urgent) and is derived automatically
            from the issue's category when it is first reported, so it never
            changes afterwards - which keeps the priority-keyed Red-Black Tree and
            the urgency heap valid without ever needing to re-key a node.
        </summary>
     * **/
    public enum RequestPriority
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Critical = 4
    }
}
