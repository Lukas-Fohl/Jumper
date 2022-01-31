using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class btn : MonoBehaviour
{
    // Start is called before the first frame update
    public Text info;
    public int x =1;
    void Start()
    {
        if(PlayerPrefs.GetString("own") ==""||PlayerPrefs.GetString("own") ==null)
        {
            PlayerPrefs.SetString("own","-0");
        }
        if(string.IsNullOrEmpty(PlayerPrefs.GetInt("skin").ToString()))
        {
            PlayerPrefs.SetInt("skin",0);
        }
        if(string.IsNullOrEmpty(PlayerPrefs.GetInt("points").ToString()))
        {
            PlayerPrefs.SetInt("points",0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void click()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void home()
    {
        SceneManager.LoadScene("start");
    }
    public void shop()
    {
        SceneManager.LoadScene("shop");
    }
    public void skins()
    {
        SceneManager.LoadScene("skins");
    }
    public void info_txt()
    {
        x++;
        if(x%2==0)
        {
            info.transform.position = Camera.main.WorldToScreenPoint(new Vector2(0,2.25f));
        }else
        {
            info.transform.position = Camera.main.WorldToScreenPoint(new Vector2(0,30));
        }
    }
}
