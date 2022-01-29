using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class skins_sort : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] skins;
    public Button back;
    public Text points;
    void Start()
    {
        for(int i =0; i < skins.Length; i++)
        {
            GameObject a = Instantiate(skins[i]);
        }
        back.transform.position = Camera.main.WorldToScreenPoint(new Vector2(1.75f,4.25f));
        points.transform.position = Camera.main.WorldToScreenPoint(new Vector2(-1.5f,4.25f));
        if(!string.IsNullOrEmpty(PlayerPrefs.GetInt("points").ToString()))
        {
            points.text = $"POINTS: {PlayerPrefs.GetInt("points")}";
        }else
        {
            PlayerPrefs.SetInt("points",0);
            points.text = $"POINTS: {PlayerPrefs.GetInt("points")}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        points.text = $"POINTS: {PlayerPrefs.GetInt("points")}";
    }
        public void back_btn()
    {
        SceneManager.LoadScene("start");
    }
}
