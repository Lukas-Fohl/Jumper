using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class bought_skin : MonoBehaviour
{
    public int id;
    public Button sele;
    public GameObject sprite;
    public Vector2 position;
    public int pos;
    public bool chosen;
    public Text child_text;
    public new AudioSource audio;
    void Start()
    {
        //sele.GetComponentInChildren<Text>().text = $"select";
        audio = GetComponent<AudioSource>();
        pos = 100;
        chosen = false;
        get_pos();
        posToPos();
        set_pos();
    }
    void get_pos()
    {
        //PlayerPrefs.SetString("own","-0-1-2-3-4-5-6-7-8");
        if(PlayerPrefs.GetString("own") != null || PlayerPrefs.GetString("own") !="")
        {
            string[] own_array_sting = PlayerPrefs.GetString("own").Split('-');
            int minus =0;
            foreach (string item in own_array_sting)
            {
                if(string.IsNullOrEmpty(item))
                    minus++;
            }
            for (int i = 0; i < own_array_sting.Length; i++)
            {
                int x;
                int.TryParse(own_array_sting[i],out x);
                if(id == x && !string.IsNullOrEmpty(own_array_sting[i]))
                {
                    pos = i-minus;
                }
            }
        }
    }
    void posToPos()
    {
        float x,y;
        switch(pos%3)
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
        y = Mathf.Abs(10-((pos-(pos%3))/3)*2f); //METH
        position = new Vector2(x,y-7.5f);
    }
    void Update()
    {
        if(PlayerPrefs.GetInt("skin")==id)
            chosen = true;
        else if(string.IsNullOrEmpty(PlayerPrefs.GetInt("skin").ToString()))
            PlayerPrefs.SetInt("skin",0);
        else
            chosen = false;
        if(chosen == true)
        {
            sele.enabled = false;
            //sele.GetComponent<Image>().color = Color.grey;
            child_text.text = "selected";
            //sele.GetComponentInChildren<Text>().text = $"selected";
        }else
        {
            sele.enabled = true;
            child_text.text = "select";
            sele.GetComponent<Image>().color = Color.white;
            //sele.GetComponentInChildren<Text>().text = $"select";
        }
    }   
    public void chose_click()
    {
        PlayerPrefs.SetInt("skin",id);
        audio.Play();
    }
    IEnumerator stop_sound()
    {
        yield return new WaitForSeconds(.5f);
        audio.Stop();
    }
    void set_pos()
    {
        transform.position = new Vector2(position.x,position.y);
        sele.transform.position = Camera.main.WorldToScreenPoint (new Vector2(position.x+.01f,position.y-.5f));
        child_text.transform.position = Camera.main.WorldToScreenPoint (new Vector2(position.x+.01f,position.y-.5f));
        sprite.transform.position = new Vector2(position.x,position.y+.3f);
    }
}
