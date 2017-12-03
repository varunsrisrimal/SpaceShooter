using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BaseShip
{
    float time = 0.0f;

    public GameObject player;
    // Use this for initialization
    void Start ()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
        }

	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        time += Time.deltaTime;

        if(time > 2.0f)
        {
            LaunchMissile();
            time = 0.0f;
        }
    }

    protected override void LaunchMissile()
    {
        Vector2 pos = transform.position;
        pos.y -= 0.5f;

        GameObject obj = Instantiate(Resources.Load("Prefabs/EnemyMissile"), pos, transform.rotation) as GameObject;
        obj.GetComponent<Missile>().SetOwner(this.gameObject);
        obj.GetComponent<Missile>().target = player;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Missile"))
        {
            if (other.gameObject.GetComponent<Missile>().GetOwner().name == "Player")
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
