using UnityEngine;
using System.Collections;

namespace MiniGame
{
    public class DesReaction : MonoBehaviour
    {
        GameObject desObj;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {      
                //展示到达终点的动画
                desObj = GameObject.Instantiate(Resources.Load("Prefabs/ArriveDes")) as GameObject;
                desObj.transform.position = gameObject.transform.position;
                StartCoroutine(DestoryDesObj());            
            }
        }        

        private IEnumerator DestoryDesObj()
        {
            yield return new WaitForSeconds(1.2f);
            if (desObj != null)
            {
                Destroy(desObj);
            }
        }
    }

}
