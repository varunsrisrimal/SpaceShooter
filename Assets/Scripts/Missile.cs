using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {

    GameObject owner;
    public GameObject target;

    Vector2 targetPosition;

    float time = 0.0f;
    float speed;

    //this is for the boundary check
    Vector2 minPos, maxPos;

    public Missile(GameObject obj)
    {
        owner = obj;
    }

    bool homing = true;
	// Use this for initialization
	void Start ()
    {
        minPos = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxPos = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //later I might have to change this to base class of player and enemy
        speed = owner.GetComponent<Player>().accelerateDelta;

        if (target != null)
        {
            targetPosition = target.transform.position;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        time += Time.deltaTime;

        if (time > 1.0f && homing)
        {
            homing = false;
        }

        if(homing && target != null)
        {
            transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
        }
        else
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }


        if(transform.position.x < minPos.x || transform.position.x > maxPos.x ||
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
