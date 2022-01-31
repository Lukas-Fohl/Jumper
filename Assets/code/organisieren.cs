using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class organisieren : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject cherry;
    public GameObject points_obj;
    public float scale_y;
    public float scale_x;
    public GameObject player_obj;
    public Rigidbody2D	rb;
    public new AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        GameObject instance = Instantiate(cherry);
        instance.transform.position = new Vector2(Random.Range(-2f,2f), Random.Range(-2f,2f));
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("place");
        handle_points();
    }
    IEnumerator place()
    {
        GameObject[] cherrys = GameObject.FindGameObjectsWithTag("up");
        if(cherrys.Length <1 || cherrys == null)
        {
            GameObject instance = Instantiate(cherry);
            instance.transform.position = new Vector2(100,100);
            audio.Play();
            StartCoroutine("stop_sound");
            yield return new WaitForSeconds(.5f);
            while(true)
            {
                Vector2 place = new Vector2(Random.Range(-2.01f,2.01f),Random.Range(-2.01f,2.01f));
                float site_a = Mathf.Abs(player_obj.transform.position.x-place.x);
                float site_b = Mathf.Abs(player_obj.transform.position.y-place.y);
                float dif = Mathf.Sqrt(Mathf.Pow(site_a,2)+Mathf.Pow(site_b,2));
                Vector2 next_pos = new Vector2(player_obj.transform.position.x + rb.velocity.x ,player_obj.transform.position.y + rb.velocity.y);
                float site_a_2 = Mathf.Abs(place.x-next_pos.x);
                float site_b_2 = Mathf.Abs(place.y-next_pos.y);
                float dif_2 = Mathf.Sqrt(Mathf.Pow(site_a_2,2)+Mathf.Pow(site_b_2,2));
                if(dif >1.75 && dif_2 >1.75)
                {
                    if(Mathf.RoundToInt(player_obj.transform.position.x) != Mathf.RoundToInt(place.x))
                    {
                        instance.transform.position = place;
                        break;
                    }
                }
            }
        }
    }
    IEnumerator stop_sound()
    {
        yield return new WaitForSeconds(.1f);
        audio.Stop();
    }
    void handle_points()
    {
 
        GameObject thePlayer = GameObject.FindGameObjectWithTag("Player");
        player playerScript = thePlayer.GetComponent<player>();
        int idk = playerScript.jump_points;
        GameObject[] obj = GameObject.FindGameObjectsWithTag("Finish");
        foreach(GameObject g in obj)
        {
            if(g.gameObject.ToString().Contains("Clone"))
            {
                g.gameObject.SetActive(false);
                Destroy(g.gameObject);
            }
        }
        for (int i = 0; i < idk; i++)
        {
            float x = (i%10)/scale_x;
            float y = (i-(i%10)/scale_y);
            GameObject instance2 = Instantiate(points_obj);
            instance2.transform.position = new Vector2(x/3 -2.5f +0.5f,(y/10 +4.5f) - y/10*1.5f);
        }
    }
}
