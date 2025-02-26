using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{
    //NOTICE: This code was copied directly from the completed CS3GD Lab 5 to be used as a basis for further behaviours.

    public Transform target;
    public float distFromTarget;
    public bool follow = true;
    public float displacementY = 1.5f;
    //Degree to which the camera lags just a little behind the target's motion
    public float easing = 0.1f;
    [SerializeField]
    private GameObject cameraFocusPoint;

    private Vector3 newpos;



    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            //+y for above, - z for behind.
            newpos = target.position - (target.forward * distFromTarget); //+ (target.up * displacementY);
            newpos.y = target.position.y + displacementY;
            this.transform.position += (newpos - this.transform.position) * easing;
            //this.transform.rotation = target.transform.rotation;
            this.transform.LookAt(cameraFocusPoint.transform);
        }
        else
        {
            this.transform.LookAt(cameraFocusPoint.transform);
        }
    }
}
