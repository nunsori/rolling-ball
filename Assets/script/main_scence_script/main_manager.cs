using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class main_manager : MonoBehaviour
{
    public GameObject main_title;
    public GameObject start_button;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //main_title.GetComponent<RectTransform>().localScale = new Vector2(Screen.width*10/9,Screen.width/3.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //main_bg.GetComponent<RectTransform>().localScale = new Vector2(Screen.width,Screen.height);
    }

    public void btn_clicked(string type)
    {
        switch (type) {
            case "play":
                SceneManager.LoadScene("SampleScene");
                break;
            default: break;
        }

    }
}
