using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mikealpha
{
    public class Sequence : NodeBT
    {
        public Sequence(List<NodeBT> nodes)
        {
            childNodes = nodes;
        }

        public override status UpdateStatus(float tick)
        {
            for(int i = 0; i<childNodes.Count;i++)
            {
                var status = childNodes[i].UpdateStatus(tick);
                if (status == status.running)
                    return status.running;
                else if (status == status.failure)
                    return status.failure;
            }
           return status.success;
        }
    }
}
