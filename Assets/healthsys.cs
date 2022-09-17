using UnityEngine;

public class healthsys : MonoBehaviour
{
    fire fire;
    masterscript ms;
    void Start()
    {
        fire = fire.FindObjectOfType<fire>();

        ms = FindObjectOfType<masterscript>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Animation 1 (1)(Clone)")
        {
            other.gameObject.SetActive(false);
            PlayerPrefs.SetInt("enemy", PlayerPrefs.GetInt("enemy") - 1);
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 10);
            gameObject.SetActive(false);
            ms.alienlist.Remove(other.gameObject);
        }
        if (other.gameObject.name == "Animation 1 (3)(Clone)")
        {
            if (fire.check == true)
            {
                Debug.Log(fire.check);
                for (int i = fire.frdlst.Count -1 ; i > 0 && fire.found == false; i--)
                {
                    Debug.Log(i);
                    if (other.gameObject == fire.frdlst[i])
                    {
                        Debug.Log("fired");
                        other.gameObject.SetActive(false);
                        PlayerPrefs.SetInt("enemy", PlayerPrefs.GetInt("enemy") - 1);
                        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 10);
                        fire.found = true;
                        ms.alienlist.Remove(other.gameObject);
                    }
                }
            }
            if (fire.found == false)
            {
                fire.frdlst.Insert(fire.listpost, other.gameObject);
                fire.listpost += 1;
                fire.check = true;
            }
            fire.found = false;
            gameObject.SetActive(false);
        }
    }
}
