using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class bought_skin_sort : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] skins;
    public Button back;
    void Start()
    {
        for(int i =0; i < skins.Length; i++)
        {
            GameObject a = Instantiate(skins[i]);
        }
        back.transform.position = Camera.main.WorldToScreenPoint(new Vector2(1.75f,4.25f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        public void back_btn()
    {
        SceneManager.LoadScene("start");
    }
}
