using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skin : MonoBehaviour
{
    public int id;
    public Button buy;
    public GameObject sprite;
    public Vector2 position;
    public int price;
    public bool bought;
    public new AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        buy.GetComponentInChildren<Text>().text = $"{price}";
        bought = false;
        IdToPos();
        set_pos();
        get_own();
        if(bought == true)
        {
            buy.enabled = false;
            buy.GetComponentInChildren<Text>().text = $"bought";
        }
    }
    void get_own()
    {
        if(PlayerPrefs.GetString("own") != null || PlayerPrefs.GetString("own") !="")
        {
            string[] own_array_sting = PlayerPrefs.GetString("own").Split('-');
            for (int i = 0; i < own_array_sting.Length; i++)
            {
                int x;
                int.TryParse(own_array_sting[i],out x);
                if(id == x)
                {
                    bought=true;
                    break;
                }
            }   
        }
    }
    void Update()
    {
        if(string.IsNullOrEmpty(PlayerPrefs.GetInt("points").ToString()))
        {
            PlayerPrefs.SetInt("points",0);
        }
        if(bought == true)
        {
            buy.enabled = false;
            buy.GetComponentInChildren<Text>().text = $"bought";
        }
    }
    void IdToPos()
    {
        /*  
            0 0 0
            0 0 0
            0 0 0
            0 0 0
            oder so
        */
        float x,y;
        switch(id%3)
        {
            default:
                x = -1.5f;
            break;
            case 1:
                x = 0;
            break;
            case 2:
                x = 1.5f;
            break;
        }
        y = Mathf.Abs(10-((id-(id%3))/3)*2f); //METH
        position = new Vector2(x,y-7.5f);
    }
    public void buy_click()
    {
        if(PlayerPrefs.GetInt("points")>= price)
        {
            PlayerPrefs.SetString("own",PlayerPrefs.GetString("own")+$"-{id}");
            PlayerPrefs.SetInt("points",PlayerPrefs.GetInt("points")-price);
            bought = true;
        }
            audio.Play();
            StartCoroutine("stop_sound");
    }
    IEnumerator stop_sound()
    {
        yield return new WaitForSeconds(.5f);
        audio.Stop();

    }
    void set_pos()
    {
        transform.position = new Vector2(position.x,position.y);
        buy.transform.position = Camera.main.WorldToScreenPoint (new Vector2(position.x+.01f,position.y-.5f));
        sprite.transform.position = new Vector2(position.x,position.y+.3f);
    }
}
