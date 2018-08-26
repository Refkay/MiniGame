using UnityEngine;
using System.Collections;

namespace MiniGame
{
    public class StarLineTrigger : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Line" )
            {
                //隐藏没点亮之前的星星
                gameObject.transform.Find("hide").gameObject.SetActive(false);
                gameObject.transform.Find("show").gameObject.SetActive(true);
            }
        }
    }
}


