using UnityEngine;
using System.Collections;

namespace MiniGame
{
    public class TestDraw : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject go = GameObject.Instantiate(Resources.Load("Prefabs/LineEff")) as GameObject;
                go.transform.SetParent(this.transform, true);
                go.transform.position = new Vector3(-2, -1, 0);
                TrailAutoMoveComp t = go.AddComponent<TrailAutoMoveComp>();
                t.Init(new Vector3(1, -1, 0), 2.0f, 0.0f);
            }

        }
    }

}
