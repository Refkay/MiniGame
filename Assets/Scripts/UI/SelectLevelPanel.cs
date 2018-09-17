using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MiniGame
{
    public class SelectLevelPanel : MonoBehaviour
    {

        public SelectLevelView selectLevelView;
      

        public Color Blue;

        private void Awake()
        {
       

        }

        // Use this for initialization
        void Start()
        {
            InitEvent();
         
        }

        private void InitEvent()
        {         
            selectLevelView.SetMessages(GetLevelsList());
        }

        private int[] levelData = new int[] { 4, 5, 7 };

        private List<SelectItemMessage> GetLevelsList()
        {
            var list = new List<SelectItemMessage>();
            int mainLv = PlayerProgress.Instance.HighestMainLevel;
            int subLv = PlayerProgress.Instance.HighestSubLevel;
            for (int i = 1; i <= levelData.Length; i++)
            {
                for (int j = 1; j <= levelData[i - 1]; j++)
                {
                    list.Add(new SelectItemMessage(string.Format("Level{0}-{1}", i, j), i, j));
                    if (mainLv == i && subLv == j)
                    {
                        break;
                    }
                }
                if (mainLv == i)
                {
                    break;
                }
            }
            return list;
        }

    }

}
