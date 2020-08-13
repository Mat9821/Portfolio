using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraModeEnum
{
    ThirdPerson,
    FirstPerson,
    TopDown
}

public class CameraController : MonoBehaviour
{

    public GameObject FollowObject;
    public GameObject PlayerObject;
    private float CameraSpeed = 100.0f;
    private float FirstPersonClampAngle = 90.0f;
    private float ThirdPersonClampAngle = 45.0f;
    private Vector3 SpringArm;
    private float CameraSensitivity = 100.0f;
    private Vector2 Mouse;
    private Vector3 OriginalPosition;
    private Quaternion OriginalRotation;
    private Vector3 CameraRotation;
    private CameraModeEnum CameraMode;
    private Vector3 ThirdPersonDefaultSpringArm = new Vector3(0, 0, 2);
    private Vector3 FirstPersonDefaultSpringArm = new Vector3(0, 0, -0.5f);


    // Start is called before the first frame update
    void Start()
    {
        //sets camera mode to third person camera on game start.
        CameraMode = CameraModeEnum.ThirdPerson;

        //locks and hides cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        //gets camera position & rotation and sets initial camera p & r offset, for further camera functionality, i.e. first person, zoom, etc.
        CameraRotation = transform.localRotation.eulerAngles;
        OriginalRotation = transform.localRotation;
        OriginalPosition = transform.position;

        if (CameraMode == CameraModeEnum.ThirdPerson)
        {
            SpringArm = ThirdPersonDefaultSpringArm;
        }
        else if (CameraMode == CameraModeEnum.FirstPerson)
        {
            SpringArm = FirstPersonDefaultSpringArm;
        }
        else
        {
            //setup top down later!
        }

        loopstage = 0;

    }

    // Update is called once per frame
    void Update()
    {
        // camera target is being weirdly ofset on start, place holder while i figure that out.
        FollowObject.transform.position = PlayerObject.transform.position;
        //************************************************************************************//

        //make shift zoom, make into better function later
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            if (loopstage >= 0 && loopstage < 3)
            {
                transform.Translate(0, 0, loopfactor);
                loopstage++;
                
            }
            else if(loopstage >= 3)
            {
                transform.Translate(0, 0, -loopfactor*loopstage);
                loopstage = 0;
            }
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            FirstPersonCamera();
        }
        else
        {
            ThirdPersonCamera();
        }

    }

    void FirstPersonCamera()
    {
        //future controller support!
        //--------------------------------------
        //--------------------------------------

        CameraMode = CameraModeEnum.FirstPerson;

        //sets appropriate spring arm
        //SpringArm = FirstPersonDefaultSpringArm;
        

        //gets mouse axis
        Mouse.x = Input.GetAxis("Mouse X");
        Mouse.y = Input.GetAxis("Mouse Y");

        //applies movement based on input and sensitivity
        CameraRotation.x += Mouse.x* CameraSensitivity * Time.deltaTime;
        CameraRotation.y += Mouse.y* CameraSensitivity * Time.deltaTime;
        CameraRotation.y = Mathf.Clamp(CameraRotation.y, -FirstPersonClampAngle, FirstPersonClampAngle);

        Quaternion LocalRotation = Quaternion.Euler(-CameraRotation.y, CameraRotation.x, 0.0f);

        transform.rotation = LocalRotation;
    }

    void ThirdPersonCamera()
    {
        //future controller support!
        //--------------------------------------
        //--------------------------------------
        if(CameraMode == CameraModeEnum.FirstPerson)
        {
            transform.Translate(0, 0, 8);
            transform.position = OriginalPosition;
            transform.rotation = OriginalRotation;
        }

        CameraMode = CameraModeEnum.ThirdPerson;

        //sets appropriate spring arm
        SpringArm = ThirdPersonDefaultSpringArm;
        

        //gets mouse axis
        Mouse.x += Input.GetAxis("Mouse X");
        Mouse.y += Input.GetAxis("Mouse Y");
        Mouse.y = Mathf.Clamp(Mouse.y, -ThirdPersonClampAngle/5, ThirdPersonClampAngle);

        transform.LookAt(FollowObject.transform);
        FollowObject.transform.rotation = Quaternion.Euler(Mouse.y, Mouse.x, 0);
    }

    public void SetCameraMode(CameraModeEnum cameramodeenum)
    {
        CameraMode = cameramodeenum;
    }

    void LateUpdate()
    {
        /* //applies follow taget, can be repurposed for "switching" camera target
 if (CameraMode == CameraModeEnum.ThirdPerson)
 {
   /*  Transform FollowObjectTransform = FollowObject.transform;
     float Step = CameraSpeed * Time.deltaTime;
     Vector3 FollowObjectPosition = FollowObjectTransform.position - SpringArm;
     transform.position = Vector3.MoveTowards(transform.position, FollowObjectPosition, Step);*/
        //   }
        if (CameraMode == CameraModeEnum.FirstPerson)
        {
            Transform FollowObjectTransform = FollowObject.transform;
            float Step = CameraSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, FollowObjectTransform.position, Step);
        }

    }

    public int loopstage = 0;
    public float loopfactor = 1.0f;

}


