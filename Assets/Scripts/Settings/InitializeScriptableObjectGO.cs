using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBOB
{
    public class InitializeScriptableObjectGO : MonoBehaviour
    {
        public SO_GameObject sO_GameObject;
        public GameObject assignWithThisGO;

        private void Awake()
        {
            sO_GameObject.gameObject = assignWithThisGO;
        }
    }
}
