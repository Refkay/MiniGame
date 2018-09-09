using UnityEngine;
using System.Collections;
using MiniGameComm;

namespace MiniGame
{
    public class MainMenu : MonoSingleton<MainMenu>
    {
        public GameObject StartMenu;

        private void Awake()
        {
            //StartMenu.SetActive(false);
        }

        private void Start()
        {
            StartCoroutine(StartGame(2.5f));
        }

        IEnumerator StartGame(float time)
        {
            yield return new WaitForSeconds(time - 1.0f);
            StartMenu.GetComponent<Animator>().SetTrigger("play");

            yield return new WaitForSeconds(1.0f);
            StartMenu.SetActive(false);                   
        }
    }

}
