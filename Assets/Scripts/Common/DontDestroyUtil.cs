using UnityEngine;
using System.Collections;
using UnityEngine;

namespace MiniGameComm
{
    public class DontDestroyUtil 
    {
        public static void DontDestroyOnLoadWrapper(GameObject obj)
        {
            if (obj  != null)
            {
                Object.DontDestroyOnLoad(obj);
            }
        }        
    }
}

