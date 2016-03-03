using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    [SerializeField]
    public float speed;

    public Vector3 facingDirection;

    SpriteRenderer spriteRender;

    Sprite[] sprites;

    Follower[] followers;
    
    // Use this for initialization
	void Start () {
        facingDirection = Vector3.right;

        spriteRender = gameObject.GetComponent<SpriteRenderer>();

        sprites = Resources.LoadAll<Sprite>("Sprites");

        GameObject[] f = GameObject.FindGameObjectsWithTag("Follower");
        followers = new Follower[f.Length];
        for (int i = 0; i < followers.Length; i++)
            followers[i] = f[i].GetComponent<Follower>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 currDir = Vector3.zero;


        if (Input.GetKey(KeyCode.RightArrow))
            currDir.x += 1;

        if (Input.GetKey(KeyCode.LeftArrow))
            currDir.x -= 1;

        if (Input.GetKey(KeyCode.UpArrow))
            currDir.y += 1;

        if (Input.GetKey(KeyCode.DownArrow))
            currDir.y -= 1;

        if (currDir != Vector3.zero)
        {
            if (currDir != facingDirection)
            {
                facingDirection = currDir;
                faceDirection();
                foreach (Follower f in followers)
                    f.waypoints.Enqueue(transform.position);
            }

            transform.Translate(facingDirection * speed * Time.deltaTime);

            foreach (Follower f in followers)
                f.move();
        }
            
	}

    private void faceDirection()
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
