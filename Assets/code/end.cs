using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class end : MonoBehaviour
{
    // Start is called before the first frame update
    public Text score_;
    void Start()
    {
        PlayerPrefs.SetInt("points",PlayerPrefs.GetInt("points")+PlayerPrefs.GetInt("score"));
        int hscore;
        if(PlayerPrefs.GetInt("score")>PlayerPrefs.GetInt("hscore"))
        {
            hscore = PlayerPrefs.GetInt("score");
            PlayerPrefs.SetInt("hscore",PlayerPrefs.GetInt("score"));
        }else
        {
            hscore = PlayerPrefs.GetInt("hscore");
        }
        score_.text = $"score:\t\t{PlayerPrefs.GetInt("score")}\nHight score:\t{PlayerPrefs.GetInt("hscore")}\nPoints:\t\t{PlayerPrefs.GetInt("points")}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
