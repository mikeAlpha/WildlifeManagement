using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace mikealpha
{

    public enum status
    {
        success,
        running,
        failure
    }


    public abstract class NodeBT
    {
        protected List<NodeBT> childNodes = new List<NodeBT>();
        public virtual status UpdateStatus(float tick) { return status.failure; }
    }
}
