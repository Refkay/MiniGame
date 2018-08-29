using UnityEngine;
using System.Collections;

public class SelfRotateMovement : MonoBehaviour {

    public float rotateSpeed = 30;
    private Transform cachedTransform;

    private void Awake()
    {
        cachedTransform = gameObject.transform;
    }

    // Update is called once per frame
    void Update () {
        cachedTransform.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
    }
}
