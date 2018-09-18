using UnityEngine;
using System.Collections;
using MiniGameComm;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MiniGame
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        public GameObject gamePanel;

        public Button pauseBtn;

        public Button continueBtn;

        public Button QuitBtn;

        public GameObject PauseAndTurnCountPanel;

        public Text turnCountText;

        public GameObject newGuide1;

        public GameObject newGuide2;

        public GameObject newGuide3;

        public GameObject newGuide4;

        public GameObject newGuide5;

        public GameObject newGuide6;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            AddButtonListener();
        }

        private void AddButtonListener()
        {
            pauseBtn.onClick.AddListener(OnPauseBtnClick);
            continueBtn.onClick.AddListener(OnContinueBtnClick);
            QuitBtn.onClick.AddListener(OnQuitBtnClick);
        }
       
        public void OnPauseBtnClick()
        {
            Time.timeScale = 0.0f;
            gamePanel.SetActive(true);
            PauseAndTurnCountPanel.gameObject.SetActive(false);
        }

        public void OnContinueBtnClick()
        {
            Time.timeScale = 1.0f;
            gamePanel.SetActive(false);
            PauseAndTurnCountPanel.gameObject.SetActive(true);
        }

        public void OnQuitBtnClick()
        {
            gamePanel.SetActive(false);
            PauseAndTurnCountPanel.gameObject.SetActive(false);
            SceneManager.LoadSceneAsync("MainMenu");
        }

        public void GoToGame()
        {
            gamePanel.SetActive(false);
            PauseAndTurnCountPanel.gameObject.SetActive(true);
        }

        public void HideAll()
        {
            gamePanel.SetActive(false);
            PauseAndTurnCountPanel.gameObject.SetActive(false); 
        }

        public void SetTurnCountText(int count)
        {
            turnCountText.text = "x " + count;
        }
    }
}

