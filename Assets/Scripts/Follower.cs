using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Follower : MonoBehaviour {

    [SerializeField]
    public float speed;

    Vector3 facingDirection;

    SpriteRenderer spriteRender;
    Sprite[] sprites;

    Player p;

    public Queue<Vector3> waypoints;

	// Use this for initialization
	void Start () {
        facingDirection = Vector3.right;

        spriteRender = gameObject.GetComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("Sprites");

        p = GameObject.Find("Player").GetComponent<Player>();

        waypoints = new Queue<Vector3>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void move()
    {
        if (waypoints.Count != 0)
        {
            Vector3 newPos = transform.position + (facingDirection * speed * Time.deltaTime);
            float deltaWaypoint = Mathf.Abs(Vector3.Distance(transform.position, waypoints.Peek()));
            float deltaNewPos = Mathf.Abs(Vector3.Distance(transform.position, newPos));

            //will pass waypoint in next move
            if (deltaWaypoint < deltaNewPos)
            {
                deltaNewPos -= deltaWaypoint;
                transform.position = waypoints.Peek();
                waypoints.Dequeue();
                if (waypoints.Count == 0)
                    face(p.transform.position);
                else
                    face(waypoints.Peek());

                transform.Translate(facingDirection * deltaNewPos);
            }
            else//keep moving in direction of waypoint
            {
                transform.Translate(facingDirection * speed * Time.deltaTime);
            }
        }
        else
        {
            //already facing player, continue moving in direction
            transform.Translate(facingDirection * speed * Time.deltaTime);
        }
        
    }

    private void face(Vector3 point)
    {
        if (point.x == transform.position.x)
        {
            if (point.y > transform.position.y)
                facingDirection = Vector3.up;
            else if (point.y < transform.position.y)
                facingDirection = Vector3.down;
        }
        else if (point.x > transform.position.x)
        {
            if (point.y > transform.position.y)
                facingDirection = new Vector3(1, 1, 0);
            else if (point.y == transform.position.y)
                facingDirection = Vector3.right;
            else
                facingDirection = new Vector3(1, -1, 0);
        }
        else if (point.x < transform.position.x)
        {
            if (point.y > transform.position.y)
                facingDirection = new Vector3(-1, 1, 0);
            else if (point.y == transform.position.y)
                facingDirection = Vector3.left;
            else
                facingDirection = new Vector3(-1, -1, 0);
        }

        changeSprite();
    }

    private void changeSprite()
    {
        if (facingDirection.x == 0)
        {
            if (facingDirection.y == 1)
                spriteRender.sprite = sprites[1];
            else if (facingDirection.y == -1)
                spriteRender.sprite = sprites[6];
        }
        else if (facingDirection.x == 1)
        {
            if (facingDirection.y == 1)
                spriteRender.sprite = sprites[2];
            else if (facingDirection.y == 0)
                spriteRender.sprite = sprites[4];
            else if (facingDirection.y == -1)
                spriteRender.sprite = sprites[7];
        }
        else if (facingDirection.x == -1)
        {
            if (facingDirection.y == 1)
                spriteRender.sprite = sprites[0];
            else if (facingDirection.y == 0)
                spriteRender.sprite = sprites[3];
            else if (facingDirection.y == -1)
                spriteRender.sprite = sprites[5];
        }
    }
}
