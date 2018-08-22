using UnityEngine;
using System.Collections;

public class BlackHole : MonoBehaviour {

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 gravity = (transform.position - collision.gameObject.transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(gravity * 20);
        }
    }
}
