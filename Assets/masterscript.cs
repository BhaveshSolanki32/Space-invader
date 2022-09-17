using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class masterscript : MonoBehaviour
{
    public GameObject alien;
    public GameObject alien2;
    float diff = 1.38f;
    public Vector2 ai = new Vector2(-2.72f, 3.65f);
    public float speed;
    public Animator anim;
    public GameObject start;
    public GameObject player;
    Transform trans;
    Vector2 motion;
    PlayerPrefs levelcheck;
    bool spawned = false;
    public GameObject leveltxt;
    public GameObject scoretxt;
    public List<GameObject> alienlist = new List<GameObject>();
     int listpost;
    Vector2 fallalienvect;
    Vector2 fallalien2vect;
    private GameObject randomalien1;
    private GameObject randomalien2;
    public float fallspeed;
    bool fallbool = false;
    int randomint;
    public GameObject gameovertxt;
    void Start()
    {
        listpost = 0;
        PlayerPrefs.SetInt("enemy", 1);
        spawned = false;
        if (PlayerPrefs.GetInt("levelcheck") == 1)
        {
            PlayerPrefs.SetInt("hardness", 1);
        }
        else
        {
            if (PlayerPrefs.GetInt("levelcheck") <= 1)
            { PlayerPrefs.SetInt("levelcheck", 1); }
        }
        if (PlayerPrefs.GetInt("levelcheck") <= 1)
        {
            start.SetActive(true);
        }
        else
        {
            start.SetActive(false);
            btnstart();
        }

        trans = player.GetComponent<Transform>();
        motion = trans.position;
        if (PlayerPrefs.GetInt("levelcheck") == 1)
        {
            PlayerPrefs.SetInt("score", 0);
            PlayerPrefs.SetInt("level", 0);
        }
    }
    public void btnstart()
    {
        if (spawned == false)
        {
            anim.SetBool("STARTPRESS", true);
            Invoke("startfalse", 0.5f);
            for (int i = 1; i <= 5; i = i + 1)
            {
                spawnenymies();
                ai.x += diff;
            }
            ai.x = -2.72f;

            for (int j = 1; j <= PlayerPrefs.GetInt("hardness"); j = j + 1)
            {
                ai.y -= diff;
                spawnenymies();
                for (int r = 1; r <= 4; r = r + 1)
                {
                    ai.x += diff;
                    spawnenymies();
                }
                if (j < 4)
                { ai.x = -2.72f; }
            }

            anim.SetBool("loadendd", true);
            FindObjectOfType<fire>().enabled = true;
        }
        if (PlayerPrefs.GetInt("levelcheck") <= 10)
        { Invoke("enemyfall", 20 / PlayerPrefs.GetInt("levelcheck")); }
        else
        { Invoke("enemyfall", 2); }


    }

    void spawnenymies()
    {
        PlayerPrefs.SetInt("enemy", PlayerPrefs.GetInt("enemy") + 1);
        PlayerPrefs.SetInt("spawn", Random.Range(0, 2));
        if (PlayerPrefs.GetInt("spawn") == 0)
        {
            alienlist.Insert(listpost, Instantiate(alien, ai, transform.rotation));
        }
        else
        {
            alienlist.Insert(listpost, Instantiate(alien2, ai, transform.rotation));
        }
        listpost = listpost + 1;

    }
    void startfalse()
    {
        start.SetActive(false);
    }
    public void restart()
    {
        if (PlayerPrefs.GetInt("hardness") < 3)
        { PlayerPrefs.SetInt("hardness", PlayerPrefs.GetInt("hardness") + 1); }
        PlayerPrefs.SetInt("enemy", 1);
        PlayerPrefs.SetInt("levelcheck", PlayerPrefs.GetInt("levelcheck") + 1);
        spawned = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void FixedUpdate()
    {
        trans.position = motion;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            motion.x -= speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            motion.x += speed;
        }

        if (spawned == true)
        {
            if (PlayerPrefs.GetInt("enemy") <= 1)
            {
                restart();
            }
        }
        if (spawned == true && fallbool == true)
        {
            randomalien1.GetComponent<Transform>().position = fallalienvect;
            randomalien2.GetComponent<Transform>().position = fallalien2vect;
        }


    }
    void Update()
    {
        if (PlayerPrefs.GetInt("enemy") <= 21 && PlayerPrefs.GetInt("enemy") != 1)
        {
            spawned = true;
        }
        scoretxt.GetComponent<Text>().text = "SCORE : " + PlayerPrefs.GetInt("score");
        leveltxt.GetComponent<Text>().text = "LEVEL : " + PlayerPrefs.GetInt("levelcheck");
        if (fallbool == true)
        {
            if (randomint == 0)
            {
                fallalienvect.y -= fallspeed;
                fallalien2vect.y -= fallspeed;
            }
            else
            {
                fallalien2vect.y -= fallspeed;
            }
        }
        if (fallalienvect.y < -5.22f || fallalien2vect.y < -5.22f)
        {
            gameovertxt.SetActive(true);
            FindObjectOfType<fire>().enabled = false;
            if (PlayerPrefs.GetInt("score") > PlayerPrefs.GetInt("highscore"))
            { PlayerPrefs.SetInt("highscore", PlayerPrefs.GetInt("score")); }
            PlayerPrefs.SetInt("levelcheck", 1);
            PlayerPrefs.SetInt("hardness", 1);
            PlayerPrefs.SetInt("score", 0);
            Invoke("restart", 3);
        }
    }
    void enemyfall()
    {
        fallbool = false;
        randomint = Random.Range(0, 2);
        randomalien1 = alienlist[Random.Range(0, listpost )];
        randomalien2 = alienlist[Random.Range(0, listpost )];
        if (randomint == 0)
        {
            fallalienvect = randomalien1.GetComponent<Transform>().position;
            fallalien2vect = randomalien2.GetComponent<Transform>().position;
        }
        else
        {
            fallalien2vect = randomalien1.GetComponent<Transform>().position;
        }
        fallbool = true;

    }
}
