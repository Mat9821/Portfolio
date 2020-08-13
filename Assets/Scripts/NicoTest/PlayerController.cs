using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //replace this with whatever Camera object will be created
    public CameraController theGameCamera;
    public Weapon equiptWeapon;
    public Bullet equiptAmmo;
    public Ability activeAbility;
    public BagPack activeBagPack;


    private Rigidbody Rb_pBody;
    Vector3 applyForce;
    Vector3 rotationVector;

    Vector2 playerRotation;

    public float forceModifier = 5.5f;
    void Awake()
    {
        Rigidbody tempRB = GetComponent<Rigidbody>();
        if (!tempRB)
        {
            tempRB = new Rigidbody();
            tempRB = gameObject.AddComponent<Rigidbody>();
        }
        Rb_pBody = tempRB;

        if(!theGameCamera)
        {
            //clunky, but works until a camera object prefab is created
            CameraController tempCamera = new CameraController();
            tempCamera = gameObject.AddComponent<CameraController>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        applyForce = Vector3.zero;
        rotationVector = Vector3.zero;

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        //if there is any significant force to be applied, we will apply it to the player Rigid body
        if (applyForce.magnitude > 0.1)
        {
            //apply the force
            Rb_pBody.AddForce(applyForce);
            //Slowly decrease the force being applied from whatever number it is, to 0, so the slowdown seems natural
            applyForce = Vector3.MoveTowards(applyForce, Vector3.zero, forceModifier);

        }
        if (rotationVector.magnitude > 0.1)
        {
            Debug.Log("rotationVector" + rotationVector);
            //apply the force
            transform.Rotate(rotationVector);
            //Slowly decrease the force being applied from whatever number it is, to 0, so the slowdown seems natural
            rotationVector = Vector3.MoveTowards(rotationVector, Vector3.zero, forceModifier);

        }
        //Get the input as how much are we suppose to move the player for
        applyForce = Movement();
        rotationVector += RotatePlayer();
        RotatePlayer();
    }
    private Vector3 Movement()
    {
        //Save the axis and then multiplied by our Force modifier
        //this will ensure that we are going to move in the right direction by the right amount
        Vector3 dirVector = new Vector3(Input.GetAxis("Horizontal") * forceModifier, 0, Input.GetAxis("Vertical") * forceModifier);
        //For jumping, we apply a default amount multiplied by our modifier, so all the movement is affected by this variable uniformly
        if (Input.GetKeyDown("space"))
        {
            dirVector.y += 10.0f * forceModifier;
        }
        return dirVector;
    }
    private Vector3 RotatePlayer1()
    {
        //Temp function
        Vector3 rotateLeft = new Vector3(0, 1, 0);
        Vector3 rotateRight = new Vector3(0, -1, 0);

        while (Input.GetKeyDown("q"))
        {
            return  rotateLeft;
        }
        while (Input.GetKeyDown("e"))
        {
            return rotateRight;
        }
        //Becase the game is going to phisics based, movement will be based on forces applied to the player
        return Vector3.zero;
    }

    private Vector3 RotatePlayer()
    {
        //gets mouse axis
        playerRotation.x = Input.GetAxis("Mouse X");
        playerRotation.y = Input.GetAxis("Mouse Y");
        theGameCamera.RotateCamera(playerRotation);

        //Becase the game is going to phisics based, movement will be based on forces applied to the player
        return Vector3.zero;
    }
    bool IsGrounded()
    {
        //for now it will return true until we implement a correct system to know if the character is grounded

        return true;
    }

}


