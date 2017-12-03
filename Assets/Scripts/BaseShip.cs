using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShip : MonoBehaviour
{
    public bool useKeyBoard;
    public float accelerateMax = 100.0f;
    public float accelerateDelta = 5.0f;

    public float xMovement = 5.0f;
    public float yMovement = 5.0f;

    public float horizontalSpeed = 0.25f;
    public float verticalSpeed = 0.25f;

    //this is for the boundary check
    protected Vector2 minPos, maxPos;

    //if sprite is changed we might need to update this
    protected float spriteWidth = 0.35f;
    protected float spriteHeight = 0.3f;

    protected float currentAcceleration = 0.0f;
    protected float currentDeceleration = 0.0f;
    protected bool accelerating = false;

    // Use this for initialization
    void Start()
    {
        minPos = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxPos = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        minPos.x += spriteWidth;
        minPos.y += spriteHeight;
        maxPos.x -= spriteWidth;
        maxPos.y -= spriteHeight;
    }

    // Update is called once per frame
  
       
    protected virtual void Update()
    {
        float x = 0;
        float y = 0;
        float h = 0;
        float v = 0;

        h = horizontalSpeed * Input.GetAxis("Mouse X");
        v = verticalSpeed * Input.GetAxis("Mouse Y");

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            x -= xMovement * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            x += xMovement * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            y += yMovement * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            y -= yMovement * Time.deltaTime;
        }

        //Accelerate
        if (Input.GetMouseButtonDown(0) && !accelerating)
        {
            //we had more decelation to do but started accelerating again missing the needed decelation to go back to original pos
            currentAcceleration = currentDeceleration;
            accelerating = true;
            currentDeceleration = 0.0f;
        }

        //Decelerate 
        if (Input.GetMouseButtonUp(0) && accelerating)
        {
            accelerating = false;

            currentDeceleration = currentAcceleration;
            currentAcceleration = 0.0f;
        }

        if (currentDeceleration > 0.0f && !accelerating)
        {
            y -= accelerateDelta * Time.deltaTime;
            currentDeceleration -= accelerateDelta;
        }

        if (accelerating)
        {
            if (currentAcceleration < accelerateMax)
            {
                y += accelerateDelta * Time.deltaTime;
                currentAcceleration += accelerateDelta;
            }
        }

        Vector2 pos = transform.position;

        if(useKeyBoard)
        {
            pos += new Vector2(x, y);
        }
        else
        {
            pos += new Vector2(h, v);
        }

        CheckBoundary(ref pos);

        transform.position = pos;

        //Fire
        if (Input.GetMouseButtonDown(1) || (Input.GetKeyDown(KeyCode.Space) && useKeyBoard))
        {
            LaunchMissile();
        }
    }

    protected virtual void CheckBoundary(ref Vector2 pos)
    {
        pos.x = Mathf.Clamp(pos.x, minPos.x, maxPos.x);
        pos.y = Mathf.Clamp(pos.y, minPos.y, maxPos.y);
    }

    protected virtual void LaunchMissile()
    {
        Vector2 pos = transform.position;
        pos.y += 0.5f;

        GameObject obj = Instantiate(Resources.Load("Prefabs/Missile"), pos, transform.rotation) as GameObject;
        obj.GetComponent<Missile>().SetOwner(this.gameObject);
    }
}
