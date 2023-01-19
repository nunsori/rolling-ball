using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raser_sript : MonoBehaviour
{
    private string[] direction_sample = { "z+", "z-", "x+", "x-" };
    public string direction;

    public game_manager game_Manager;

    //public int direction = 0;
    private float speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.025f;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (game_Manager.game_on == false)
        {
            Destroy(this.gameObject);
        }*/

        if(gameObject.transform.position.y >2 || gameObject.transform.position.y < -2)
        {
            Destroy(this.gameObject);
        }

        if(game_Manager.difficulty == 0)
        {
            speed = 0.025f;
        }
        else if(game_Manager.difficulty == 1)
        {
            speed = 0.05f;
        }
        else if(game_Manager.difficulty == 2)
        {
            speed = 0.07f;
        }
        else if(game_Manager.difficulty == 3)
        {
            speed = 0.09f;
        }
    }


    private void FixedUpdate()
    {
        if (gameObject.transform.position.z > -300&& direction == direction_sample[1])//z-规氢
        {
            gameObject.transform.Translate(new Vector3(0, 0, (-1)*speed));
        }else if(gameObject.transform.position.z < 300 && direction == direction_sample[0]) //z+规氢
        {
            gameObject.transform.Translate(new Vector3(0, 0, speed));
        }else if(gameObject.transform.position.x > -300 && direction == direction_sample[3])//x-规氢
        {
            gameObject.transform.Translate(new Vector3((-1)*speed, 0, 0));
        }else if(gameObject.transform.position.x < 300 && direction == direction_sample[2])//x+规氢
        {
            gameObject.transform.Translate(new Vector3(speed, 0, 0));
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("go");

        if(collision.gameObject.tag.Equals("d_wall"))
        {
            Debug.Log("destroy");
            Destroy(this.gameObject);
        }
    }
}
