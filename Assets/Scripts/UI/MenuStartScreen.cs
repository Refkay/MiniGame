using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace MiniGame
{
    public class MenuStartScreen : MonoBehaviour
    {

        public Text levelName;
        // Use this for initialization
        void Start()
        {
            levelName.text = "Level " + MissionManager.Instance.mCurLevel;                      
         }
      
    }

}
