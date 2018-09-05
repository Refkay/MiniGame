using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MiniGameComm;

namespace MiniGame
{
    public class SelectItemMessage
    {
        public string levelName;
        public int level;
        public int subLevel;
        public SelectItemMessage(string name, int level, int subLevel)
        {
            levelName = name;
            this.level = level;
            this.subLevel = subLevel;
        }
    }

    public class SelectItem : ItemRender 
    {

        public Text levelName;

        public Button btn;

        private SelectItemMessage data;

        private void Start()
        {
            InitButtonEvent();
        }

        protected override void UpdateView()
        {
            if (m_Data != null)
            {
                data = m_Data as SelectItemMessage;
                SetData(data);
            }
            base.UpdateView();
        }

        private void SetData(SelectItemMessage data)
        {
            levelName.text = data.levelName;
        }

        private void InitButtonEvent()
        {
            btn.onClick.AddListener(delegate () {
                MissionManager.Instance.UpdateMissionLevel(data.level, data.subLevel);
                SceneManager.LoadSceneAsync("Level" + data.level + "-" + "1");               
            });
           
        }
    }
}

