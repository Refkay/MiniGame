using UnityEngine;

namespace MiniGame
{
    public class TrailAutoMoveComp : MonoBehaviour
    {
        private Vector3 targetVector3;
        private Vector3 speed;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endPoint"></param>
        /// <param name="time">运动花费的秒数</param>
        public void Init(Vector3 endPoint,float time,float fixSpeed = 0.0f)
        {
            if (fixSpeed == 0.0f)
            {
                speed = (endPoint - this.transform.position) / time;
            }
            else
            {
                speed = (endPoint - this.transform.position).normalized*fixSpeed;
            }
            
            targetVector3 = endPoint;
        }

        private void Update()
        {
            float sqrtDistance = (targetVector3 - this.transform.position).sqrMagnitude;
            if (sqrtDistance > 0.01f)
            {
                Vector3 nextPos = this.speed * Time.deltaTime + this.transform.position;
                if ((nextPos - this.transform.position).sqrMagnitude < sqrtDistance)
                {
                    this.transform.position = nextPos;
                }
                else
                {
                    this.transform.position = targetVector3;
                }
            }
        }
    }
}