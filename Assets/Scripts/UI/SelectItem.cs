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

        public Image img1;

        public Image img2;

        public Image img3;

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
            switch (data.level)
            {
                case 1:
                    img1.gameObject.SetActive(true);
                    img2.gameObject.SetActive(false);
                    img3.gameObject.SetActive(false);
                    break;
                case 2:
                    img1.gameObject.SetActive(false);
                    img2.gameObject.SetActive(true);
                    img3.gameObject.SetActive(false);
                    break;
                case 3:
                    img1.gameObject.SetActive(false);
                    img2.gameObject.SetActive(false);
                    img3.gameObject.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        private void InitButtonEvent()
        {
            btn.onClick.AddListener(delegate () {
                Time.timeScale = 1.0f;
                MissionManager.Instance.UpdateMissionLevel(data.level, data.subLevel);
                SceneManager.LoadSceneAsync("Level" + data.level + "-" + "1");               
            });
           
        }
    }
}

