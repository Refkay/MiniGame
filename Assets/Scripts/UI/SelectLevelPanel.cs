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
            List<SelectItemMessage> list = new List<SelectItemMessage>();
            list.Add(new SelectItemMessage("Level1-1", 1, 1));
            list.Add(new SelectItemMessage("Level1-2", 1, 2));
            list.Add(new SelectItemMessage("Level1-3", 1, 3));
            list.Add(new SelectItemMessage("Level2-1", 2, 1));
            list.Add(new SelectItemMessage("Level2-2", 2, 2));
            list.Add(new SelectItemMessage("Level2-3", 2, 3));
            list.Add(new SelectItemMessage("Level2-4", 2, 4));
            list.Add(new SelectItemMessage("Level2-5", 2, 5));
            selectLevelView.SetMessages(list);
        }

    }

}
