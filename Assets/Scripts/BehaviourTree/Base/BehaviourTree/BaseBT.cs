using mikealpha;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mikealpha
{
    public abstract class BaseBT : MonoBehaviour
    {
        private NodeBT mRootNode;

        protected float Tick = 0.5f;

        private void Awake()
        {
            mRootNode = CreateNode();
        }

        public virtual void Update()
        {
            if (mRootNode != null)
                mRootNode.UpdateStatus(Tick);
        }

        protected abstract NodeBT CreateNode();
    }
}
