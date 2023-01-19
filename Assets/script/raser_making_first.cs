using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class raser_making_first : MonoBehaviour
{

    public GameObject raser_obj;
    public GameObject gamemanager;
    public game_manager gamemanagers;

    private string[] direction_sample = {"z+","z-","x+","x-" };
    public string direction;


    private bool shoot_cor = false;

   // public GameObject [] making = new GameObject[6];
    private float cycle_length = 0f;
    private bool cycle_reverse = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shoot_cor == false)
        {
            StartCoroutine("shoot");
        }

        
    }

    private void FixedUpdate()
    {
        if(direction == "z")
        {
            if (cycle_length < 5)
            {
                cycle_length += 0.1f;
                if (cycle_reverse == false)
                {
                    gameObject.transform.Translate(new Vector3(0.1f, 0, 0));
                }
                else
                {
                    gameObject.transform.Translate(new Vector3(-0.1f, 0, 0));
                }

            }
            else
            {
                cycle_length = 0f;
                cycle_reverse = !cycle_reverse;
            }
        }else if(direction == "x")
        {
            if (cycle_length < 6)
            {
                cycle_length += 0.1f;
                if (cycle_reverse == false)
                {
                    gameObject.transform.Translate(new Vector3(-0.1f, 0, 0));
                }
                else
                {
                    gameObject.transform.Translate(new Vector3(0.1f, 0, 0));
                }

            }
            else
            {
                cycle_length = 0f;
                cycle_reverse = !cycle_reverse;
            }
        }
        

        //Debug.Log(cycle_length);
    }

    IEnumerator shoot()
    {
        shoot_cor = true;

        while (gamemanagers.game_on == true)
        {
            float time = Random.Range(0.5f, 3f);
            if(gamemanagers.difficulty == 0)
            {
                time = Random.Range(2f, 3f);
            }
            else if (gamemanagers.difficulty == 1)
            {
                time = Random.Range(1f, 3f);
            }
            else if(gamemanagers.difficulty == 2)
            {
                time = Random.Range(0.5f, 3f);
            }
            else if(gamemanagers.difficulty == 3)
            {
                time = Random.Range(0.25f, 3f);
            }

            yield return new WaitForSeconds(time);
            if(direction == "z")
            {
                Instantiate(raser_obj, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            }
            else if(direction == "x")
            {
                Instantiate(raser_obj, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.Euler(new Vector3(0,0,0)));
            }
            
        }
        

        StopCoroutine("shoot");
    }
}
