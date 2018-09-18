using UnityEngine;
using System.Collections;

namespace MiniGame
{
    public class DelayHide : MonoBehaviour
    {

        public float delayTime;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartDelayHide()
        {
            StartCoroutine(DelayHideGameObject());
        }

        private IEnumerator DelayHideGameObject()
        {
            yield return new WaitForSeconds(delayTime);
            gameObject.SetActive(false);
        }
    }
}
