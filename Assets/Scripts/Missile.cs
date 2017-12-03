using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    public bool enableTopSpeed;
    GameObject owner;
    public GameObject target;

    Vector2 targetPosition;

    float time = 0.0f;
    float speed;

    //this is for the boundary check
    Vector2 minPos, maxPos;

    bool homing;

    public Missile(GameObject obj)
    {
        owner = obj;
    }

	// Use this for initialization
	void Start ()
    {
        //speed = 20.0f;
        homing = true;

        time = Time.time;

        minPos = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxPos = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //later I might have to change this to base class of player and enemy
        if (owner != null && enableTopSpeed)
        {
            speed = owner.GetComponent<BaseShip>().accelerateDelta * 4;
        }
        else
        {
            speed = 5.0f;
        }
        
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (Time.time - time > 1.0f && target != null)
        {
            homing = false;
        }

        if (target != null && homing)
        {
            targetPosition = target.transform.position;
        }

        if(homing && target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);

            transform.rotation = Quaternion.LookRotation(Vector3.forward, targetPosition);
        }
        else
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }


        if (transform.position.x < minPos.x || transform.position.x > maxPos.x ||
            transform.position.y < minPos.y || transform.position.y > maxPos.y)
        {
            Destroy(this.gameObject);
        }
	}


    public void SetOwner(GameObject _owner)
    {
        owner = _owner;
    }

    public GameObject GetOwner()
    {
        return owner;
    }
}
