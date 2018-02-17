using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour {

    Transform cameraTransform;
    public Transform target;
    public float distance;

    void Start()
    {
        cameraTransform = this.GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        cameraTransform.position = new Vector3(target.position.x, target.position.y, target.position.z - distance);
    }
}
