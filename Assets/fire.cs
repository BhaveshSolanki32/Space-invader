using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    public GameObject bullet;
    GameObject firedbullet;
    public Vector2 fireforce = new Vector2(0, 10);
    public List<GameObject> frdlst = new List<GameObject>();
    public bool check = false;
    public int listpost = 0;
    public int post;
   public bool found = false;
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            firedbullet = Instantiate(bullet, transform.position, transform.rotation);
            firedbullet.GetComponent<Rigidbody2D>().AddForce(fireforce);
        }
    }

}
