using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_script : MonoBehaviour
{

    public game_manager gamemanager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("go");

        if (collision.gameObject.tag.Equals("ball"))
        {
            Debug.Log("destroy");
            if (this.gameObject.tag.Equals("vanish"))
            {
                gamemanager.ball_itemv = true;
                gamemanager.vanish_item = false;
            }else if (this.gameObject.tag.Equals("jump"))
            {

                gamemanager.ball_itemj = true;
                gamemanager.jump_item = false;
            }
            
            
            Destroy(this.gameObject);


        }
    }
}
