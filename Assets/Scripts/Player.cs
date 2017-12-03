using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseShip
{
    // Use this for initialization
    void Start()
    {
        spriteWidth = 0.35f;
        spriteHeight = 0.3f;

        minPos = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        maxPos = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        minPos.x += spriteWidth;
        minPos.y += spriteHeight;
        maxPos.x -= spriteWidth;
        maxPos.y -= spriteHeight;
    }

    // Update is called once per frame

    protected override void Update()
    {
        base.Update();

        //Fire
        if(Input.GetMouseButtonDown(1))
        {
            LaunchMissile();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Missile"))
        {
            Debug.Log("works");
            if(other.gameObject.GetComponent<Missile>().GetOwner().name == "Enemy")
            {
                Destroy(other.gameObject);
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }

        }
    }

}
