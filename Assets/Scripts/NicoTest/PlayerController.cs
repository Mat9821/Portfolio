using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody Rb_pBody;
    Vector3 applyForce;
    public float forceModifier = 5.5f;
    void Awake()
    {
        Rigidbody tempRB = GetComponent<Rigidbody>();
        if(!tempRB)
        {
            tempRB = new Rigidbody();
            tempRB = gameObject.AddComponent<Rigidbody>();
        }
        Rb_pBody = tempRB;

    }
    // Start is called before the first frame update
    void Start()
    {
        applyForce = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        //if there is any significant force to be applied, we will apply it to the player Rigid body
        if(applyForce.magnitude>0.1)
        {
            //apply the force
            Rb_pBody.AddForce(applyForce);
            //Slowly decrease the force being applied from whatever number it is, to 0, so the slowdown seems natural
            applyForce = Vector3.MoveTowards(applyForce, Vector3.zero, forceModifier);

        }
        //Get the input as how much are we suppose to move the player for
        applyForce = Movement();
    }
    private Vector3 Movement()
    {
        //Save the axis and then multiplied by our Force modifier
        //this will ensure that we are going to move in the right direction by the right amount
        Vector3 dirVector = new Vector3(Input.GetAxis("Horizontal")* forceModifier, 0 , Input.GetAxis("Vertical")* forceModifier);
        //For jumping, we apply a default amount multiplied by our modifier, so all the movement is affected by this variable uniformly
        if(Input.GetKeyDown("space"))
        {
            dirVector.y += 10.0f*forceModifier;
        }
        return dirVector;
    }
    //Becase the game is going to phisics based, movement will be based on forces applied to the player

}
