using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ball_movement : MonoBehaviour
{
    public GameObject ball_obj;
    private Rigidbody ball_rigid;

    public game_manager game_Manager;

    

    public joy_stick joy_Stick;

    public GameObject joy_Stick1;
    public GameObject joy_Stick2;

    public float ball_force = 25f;

    public Slider ball_move_force;

    // Start is called before the first frame update
    void Start()
    {
        ball_rigid = ball_obj.GetComponent<Rigidbody>();
        ball_move_force.value = 25f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y < -3 && game_Manager.game_on == true)
        {
            gameObject.transform.SetPositionAndRotation(new Vector3(0,1.67f,0), Quaternion.identity);
            game_Manager.game_on = false;
            game_Manager.game_quit();
        }

        ball_force = ball_move_force.value;

        if(ball_move_force.value == 0)
        {
            ball_move_force.value = 0.75f;
        }

        if(game_Manager.joystick.activeSelf == true)
        {
            joy_Stick = joy_Stick1.GetComponent<joy_stick>();
        }
        else
        {
            joy_Stick = joy_Stick2.GetComponent<joy_stick>();
        }
    }

    private void FixedUpdate()
    {
        ball_rigid.AddForce(joy_Stick.ball_angle/ball_force);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.other.tag == "raser")
        {
            game_Manager.game_on = false;
            game_Manager.game_quit();
        }
    }

    public void jump()
    {
        ball_rigid.AddForce(new Vector3(0, 500f, 0));
    }
}
