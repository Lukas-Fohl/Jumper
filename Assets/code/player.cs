using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    // Start is called before the first frame update
    public int jump_points;
    public int points;
    public double angel;
    public float dif;
    public float velo;
    public float border;
    public bool can;
    public bool add = true;
    public bool paus = false;
    public Vector2 to;
    public Vector2 veloc;
    public Text txt;
    public Button paus_btn;
    public Button back;
    public Rigidbody2D	rb;
    public ParticleSystem ps;
    public SpriteRenderer sp;
    public Sprite[] skins_player;
    public Sprite paus_sprite;
    public Sprite play_sprite;
    public new AudioSource audio;
    public AudioSource audio2;
    public GameObject border_left;
    public GameObject border_right;
    public GameObject border_top;
    public GameObject border_bottom;
    void Start()
    {
        border_top.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2,Screen.height,1000));      //oben
        border_bottom.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2,0,1000));               //unten
        border_right.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height/2,1000));    //rechts
        border_left.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height/2,1000));                //links 
        paus_btn.transform.position = new Vector2(1.8f,3.5f);
        back.transform.position = new Vector2(1.8f,2.8f);
        sp = gameObject.GetComponent<SpriteRenderer>();
        if(string.IsNullOrEmpty(PlayerPrefs.GetInt("skin").ToString()))
        {
            PlayerPrefs.SetInt("skin",0);
        }
        sp.sprite = skins_player[PlayerPrefs.GetInt("skin")];
        transform.position = new Vector3(0,3f,1);
        rb = GetComponent<Rigidbody2D>();
        points =0;
        jump_points = 3;
        rb.gravityScale =0;
        StartCoroutine("able");
        can = false;
        ps.Stop();
    }

    IEnumerator able()
    {
        yield return new WaitForSeconds(1);
        can = true;
        if(paus == false)
            rb.gravityScale = .15f;
    }
    // Update is called once per frame
    void Update()
    {
        txt.text= points.ToString();
        if (Input.touchCount == 1 && can ==true && paus == false)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began && jump_points >=1 )
            {
                jump_points--;
                Vector2 touch_position = Camera.main.ScreenToWorldPoint(touch.position);
                Vector2 btn_pos = new Vector2(paus_btn.transform.position.x, paus_btn.transform.position.y);
                btn_pos = paus_btn.transform.position;
                if(touch_position.x < btn_pos.x-(border/2) || touch_position.x	> btn_pos.x+border || touch_position.y < btn_pos.y-border || touch_position.y > btn_pos.y+border)
                {
                    audio.Play();
                    to = Camera.main.ScreenToWorldPoint(touch.position);
                    get_angel(touch_position.x, touch_position.y);
                    dif =  get_dif(touch_position.x, touch_position.y);
                    rb.freezeRotation = true;
                    rb.freezeRotation = false;
                    rb.velocity = new Vector2(0,0);
                    rot(touch_position);
                    vel(dif);
                    ps.Play();
                    ps.transform.position = touch_position;
                    StartCoroutine("par");  
                }else
                {
                    jump_points++;                    
                }
            }
        }
    }
    IEnumerator par()
    {
        yield return new WaitForSeconds(.05f);
        ps.Stop();
    }
    void get_angel(float x_in, float y_in)
    {
        float a = Mathf.Abs(transform.position.x-x_in);
        float b = Mathf.Abs(transform.position.y-y_in);
        float hyptinuse = Mathf.Sqrt(Mathf.Pow(a, 2) +Mathf.Pow(b,2));
        float x = Mathf.Asin(a/hyptinuse);
        double y = Mathf.Asin(x);
        angel = (x/ Mathf.PI *180); 
    }
    public float get_dif(float x_in, float y_in)
    {
        float a = Mathf.Abs(transform.position.x-x_in);
        float b = Mathf.Abs(transform.position.y-y_in);
        float hyptinuse = Mathf.Sqrt(Mathf.Pow(a, 2) +Mathf.Pow(b,2));
        return hyptinuse;
    }
    public void vel(float difer)
    {
        float speed = Mathf.Abs(difer);
        rb.velocity = transform.up* (Mathf.Abs(5-speed)+.25f);
        velo = Mathf.Abs(5-speed);
    }
    IEnumerator stop_sound()
    {
        audio.Stop();
        yield return new WaitForSeconds(0.1f);
    }
    public void rot(Vector2 touchpos)
    {
        if(touchpos.x<=transform.position.x)
        {
            if(touchpos.y <= transform.position.y)
            {
                gameObject.transform.eulerAngles = new Vector3(
                gameObject.transform.eulerAngles.x,
                gameObject.transform.eulerAngles.y,
                Mathf.Abs(90-Mathf.RoundToInt((float)angel))+270
                );
            }else if(touchpos.y >= transform.position.y)
            {
                gameObject.transform.eulerAngles = new Vector3(
                gameObject.transform.eulerAngles.x,
                gameObject.transform.eulerAngles.y,
                Mathf.RoundToInt((float)angel)+180
                );
            }
        }
        else if(touchpos.x>=transform.position.x )
        {
            if(touchpos.y <= transform.position.y)
            {
                gameObject.transform.eulerAngles = new Vector3(
                gameObject.transform.eulerAngles.x,
                gameObject.transform.eulerAngles.y,
                Mathf.RoundToInt((float)angel)
                );
            }else if(touchpos.y >= transform.position.y)
            {
                gameObject.transform.eulerAngles = new Vector3(
                gameObject.transform.eulerAngles.x,
                gameObject.transform.eulerAngles.y,
                Mathf.Abs(90-Mathf.RoundToInt((float)angel))+90
                );
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.ToString().Contains("up"))
        {
            if(other.gameObject !=this.gameObject)
            {
                other.gameObject.SetActive(false);
                Destroy(other);
                if(add == true)
                {
                    jump_points=jump_points+1;
                    points++;
                    add = false;
                    StartCoroutine("add_num");
                }
            }
        }else
        {
            audio2.Play();
            StartCoroutine("end_death");
        }
    }
    IEnumerator end_death()
    {
        yield return new WaitForSeconds(0.2f);
        audio2.Stop();
        PlayerPrefs.SetInt("score",points);
        SceneManager.LoadScene("end");
    }
    IEnumerator add_num()
    {
        yield return new WaitForSeconds(.1f);
        add = true;
        
    }
    public void click()
    {
        
        if(paus == false)
        {
            veloc = rb.velocity;
            paus_btn.image.sprite = play_sprite;
            paus = true;
            rb.velocity = transform.up*0;
            rb.velocity = transform.right*0;
            rb.freezeRotation = true;
            rb.gravityScale =0;
        }else
        {
            rb.freezeRotation = false;
            paus_btn.image.sprite = paus_sprite;
            paus = false;
            rb.velocity = veloc;
            rb.gravityScale =0.15f;
        }
    }
}
