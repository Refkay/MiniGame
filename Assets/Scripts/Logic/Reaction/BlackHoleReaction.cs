using UnityEngine;
using System.Collections;

public class BlackHoleReaction : MonoBehaviour {

    public float gravityValue = 20.0f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Vector3 gravity = (transform.position - collision.gameObject.transform.position).normalized;
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(gravity * gravityValue);
        }
    }
}
