using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Production.UI
{
    public class State : MonoBehaviour
    {
        public virtual void OnStatePreShown()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnStateReady()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnStatePreHidden()
        {

        }

        public virtual void OnStateHidden()
        {
            gameObject.SetActive(false);
        }

        public virtual void HideImmediately()
        {
            gameObject.SetActive(false);
        }
    }
}
