using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class game_manager : MonoBehaviour
{

    public GameObject[] d_wall = new GameObject[4];
    private Transform[] d_wall_tr = new Transform[4];

    public bool game_on = false;

    public GameObject play_menu;
    public GameObject start_button;
    public Text timer_text;
    public GameObject game_over_menu;
    public Text total_time_text;
    public GameObject ground_panel;//땅바닥 오브젝트
    public GameObject setting_menu;
    public Toggle reverse_button;
    public GameObject joystick;
    public GameObject item_button;
    public joy_stick joy_Stick;
    public GameObject lever;
    public GameObject joystick2;

    private bool timer_on = false; // 타이머 관련 함수
    public float timer_time = 0f;
    // Start is called before the first frame update

    public bool vanish_item = false;
    public bool jump_item = false;
    public bool ball_itemv = false;
    public bool ball_itemj = false;
    public GameObject vanish_obj;
    public GameObject jump_obj;
    private bool item_create_cor = false;


    public Button item_active_btn;
    public GameObject ball_obj;
    public ball_movement ball_Movement;
    public Rigidbody ball_rigid;

    public GameObject vanishobj;
    public vanish_active_script vanish_Active_Script;

    public int difficulty = 0;

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            if(d_wall[i] !=null)
            {
                d_wall_tr[i] = d_wall[i].transform;
            }
            
        }

        play_menu.SetActive(false);
        start_button.SetActive(true);
        game_over_menu.SetActive(false);
        vanishobj.SetActive(false);
        setting_menu.SetActive(false);
        joystick2.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width - joystick2.GetComponent<RectTransform>().sizeDelta.x/2, joystick2.GetComponent<RectTransform>().sizeDelta.y / 2);
        joystick2.SetActive(false);
        difficulty = 0;
        Time.timeScale = 0;
        joy_Stick = joystick.GetComponent<joy_stick>();

        reverse_button.onValueChanged.AddListener(delegate
        {
            reverse_button_chaged(reverse_button);
        });

        reverse_button.isOn = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        string temp_str;
        temp_str = timer_time.ToString();
        
        int index = temp_str.IndexOf(".");
        timer_text.text = timer_time.ToString().Substring(0, index + 3);

        if(game_on == false)
        {
            timer_on = false;
        }

        if (ball_itemv == false && ball_itemj == false) // 이거 공이 아이템 얻었을때로 수정하기
        {
            item_active_btn.interactable = false;
            item_button.GetComponent<Image>().color = Color.black;
        }
        else
        {
            item_active_btn.interactable = true;
            if(ball_itemv == true)
            {
                item_button.GetComponent<Image>().color = Color.blue;
            }
            else
            {
                item_button.GetComponent<Image>().color = Color.red;
            }
        }

        if(timer_time >= 5&&timer_time<=20)
        {
            difficulty = 1;
        }
        else if(timer_time >20 && timer_time <= 35)
        {
            difficulty = 2;
        }
        else if(timer_time >35 && timer_time <= 50)
        {
            difficulty = 3;
        }
        else
        {
            difficulty = 0;
        }

    }


    public void button_down(string type)
    {
        switch (type)
        {
            case "play_menu_on":
                if(play_menu.active == false)
                {
                    play_menu.SetActive(true); //나중에 타이머 멈추는거 구현
                    Time.timeScale = 0;
                    timer_on = false;
                    item_create_cor = false;
                }
                break;
            case "continue":
                if(play_menu.active == true)
                {
                    Time.timeScale = 1;
                    play_menu.SetActive(false); //타이머 제가동 구현해놓기
                    timer_restart();
                    item_create_cor = true;
                    StartCoroutine("Item_create");
                    
                }
                break;
            case "main":
                StopAllCoroutines();
                SceneManager.LoadScene("main_scene");
                //메인 씬 이동 구현해놓기
                break;
            case "settings":
                setting_menu.SetActive(true);
                //play_menu.SetActive(false);
                //setting 메뉴 구현
                break;
            case "start":
                game_on = true;
                Time.timeScale = 1;
                start_button.SetActive(false);
                timer_restart();
                item_create_cor = true;
                StartCoroutine("item_create");

                //타이머 시작 구현
                break;
            case "restart":
                game_on = false;
                StopCoroutine("item_create");
                game_over_menu.SetActive(false);
                start_button.SetActive(true);
                vanishobj.SetActive(true);
                vanish_Active_Script.vanish();
                break;
            case "item_active":
                if(ball_itemv == true)
                {
                    //vanish 실행코드
                    
                    vanishobj.SetActive(true);
                    vanishobj.transform.Translate(new Vector3(ball_obj.transform.position.x,ball_obj.transform.position.y,ball_obj.transform.position.z));
                    vanish_Active_Script.vanish();
                    ball_itemv = false;
                    vanish_item = false;
                }
                else if(ball_itemj == true)
                {
                    //ball_obj.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0));
                    ball_Movement.jump();
                    //ball_rigid.AddForce(new Vector3(0, 10, 0));
                    ball_itemj = false;
                    jump_item = false;
                }
                break;
            case "back":
                setting_menu.SetActive(false);
                play_menu.SetActive(true);
                break;
            default: break;
        }

    }

    public void timer_restart()
    {
        timer_on = true;
        StartCoroutine("timer");
    }

    public void set_game_on_false()
    {
        game_on = false;
    }

    public void game_quit()//game_over_menu
    {
        Debug.Log("end");
        StopCoroutine("timer");
        game_on = false;
        Time.timeScale = 0;
        //outmenu on 하게 만들기
        total_time_text.text = timer_text.text;
        timer_time = 0f;
        game_over_menu.SetActive(true);


    }

    void reverse_button_chaged(Toggle chage)
    {
        if(chage.isOn == true)
        {
            //joystick.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
            //joystick.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0);
            //joystick.GetComponent<RectTransform>().anchoredPosition = new Vector3(-joystick.GetComponent<RectTransform>().sizeDelta.x, joystick.GetComponent<RectTransform>().sizeDelta.y, 0);/////////////

            // joystick.GetComponent<joy_stick>().rectTransform = joystick.GetComponent<RectTransform>();
            // joystick.GetComponent<joy_stick>().lever = lever.GetComponent<RectTransform>();
            joystick.SetActive(false);
            joystick2.SetActive(true);


            item_button.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            item_button.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
            item_button.GetComponent<RectTransform>().anchoredPosition = new Vector3(item_button.GetComponent<RectTransform>().sizeDelta.x/2, item_button.GetComponent<RectTransform>().sizeDelta.y / 2, 0);
            //joystick.GetComponent<RectTransform>().Translate(new Vector3(-75, 75, 0));
            //item_button.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
            //item_button.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0);
            
        }
        else if(chage.isOn == false)
        {
            //joystick.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            //joystick.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
            //joystick.GetComponent<RectTransform>().anchoredPosition = new Vector3(-joystick.GetComponent<RectTransform>().sizeDelta.x, joystick.GetComponent<RectTransform>().sizeDelta.y, 0);//////////
            //joystick.GetComponent<RectTransform>().anchoredPosition = new Vector3(75, 75, 0);

            // joystick.GetComponent<joy_stick>().rectTransform = joystick.GetComponent<RectTransform>();
            // joystick.GetComponent<joy_stick>().lever = lever.GetComponent<RectTransform>();
            joystick.SetActive(true);
            joystick2.SetActive(false);

            item_button.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
            item_button.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0);
            //item_button.GetComponent<RectTransform>().anchoredPosition = new Vector3(-50, 50, 0);
            item_button.GetComponent<RectTransform>().anchoredPosition = new Vector3(-item_button.GetComponent<RectTransform>().sizeDelta.x / 2, item_button.GetComponent<RectTransform>().sizeDelta.y / 2, 0);

            //item_button.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            //item_button.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0);
            
        }
    }

    IEnumerator timer()
    {
        while(timer_on == true)
        {
            yield return new WaitForSeconds(0.01f);
            timer_time += 0.01f;
        }
        StopCoroutine("timer");
    }

    IEnumerator item_create()
    {
        while(item_create_cor == true)
        {
            
            if(jump_item == false && vanish_item == false && ball_itemj == false && ball_itemv == false)
            {
                float time = Random.Range(1f, 2f);
                float item_temp = Random.Range(0f, 1f);
                float randomx = Random.Range(-2f, 2f);
                float randomz = Random.Range(-2.5f, 2.5f);
                yield return new WaitForSeconds(time);
                Instantiate((item_temp >= 0.5f) ? vanish_obj : jump_obj, new Vector3(ground_panel.transform.position.x+randomx,ground_panel.transform.position.y+0.2f,ground_panel.transform.position.z+randomz),Quaternion.identity);
                //ground_panel.transform.position.x   //ground panel 기준으로 아이템 생성한다는 명령어
                if(item_temp >= 0.5f)
                {
                    vanish_item = true;
                }
                else
                {
                    jump_item = true;
                }
            }
            else
            {
                yield return new WaitForSeconds(0.01f);
            }
            

        }
        StopCoroutine("item_create");
    }

}
