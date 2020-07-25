using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject FollowObject;
    public float CameraSpeed = 100.0f;
    public float ClampAngle = 90.0f;
    public Vector3 SpringArm;
    public float CameraSensitivity = 100.0f;
    public Vector2 Mouse;
    public Vector3 CameraPosition;
    public Vector3 CameraRotation;

    // Start is called before the first frame update
    void Start()
    {
        //locks and hides cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        //gets camera position and sets initial camera position offset, for further camera functionality, i.e. first person, zoom, etc.
        CameraRotation = transform.localRotation.eulerAngles;
        SpringArm = new Vector3(0, 0, 2);

    }

    // Update is called once per frame
    void Update()
    {
        //gets mouse axis
        Mouse.x = Input.GetAxis("Mouse X");
        Mouse.y = Input.GetAxis("Mouse Y");

        //future controller support!
        //--------------------------------------
        //--------------------------------------

        //applies movement based on input and sensitivity
        CameraRotation.x += Mouse.x * CameraSensitivity * Time.deltaTime;
        CameraRotation.y += Mouse.y * CameraSensitivity * Time.deltaTime;
        CameraRotation.y = Mathf.Clamp(CameraRotation.y, -ClampAngle, ClampAngle);

        Quaternion LocalRotation = Quaternion.Euler(-CameraRotation.y, CameraRotation.x, 0.0f);
        transform.rotation = LocalRotation;
    }

    void LateUpdate()
    {
        //applies follow taget, can be repurposed for "switching" camera target
        Transform FollowObjectTransform = FollowObject.transform;
        float Step = CameraSpeed * Time.deltaTime;
        Vector3 FollowObjectPosition = FollowObjectTransform.position - SpringArm;
        transform.position = Vector3.MoveTowards(transform.position, FollowObjectPosition, Step); 
    }
}
