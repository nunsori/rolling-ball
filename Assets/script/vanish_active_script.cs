using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class vanish_active_script : MonoBehaviour
{
    public Transform ball_trans;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void vanish()
    {
        gameObject.transform.SetPositionAndRotation(new Vector3(ball_trans.position.x,ball_trans.position.y,ball_trans.position.z), Quaternion.identity);
        StartCoroutine("vanish_cor");
    }

    IEnumerator vanish_cor()
    {
        for(int i = 0; i<10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            gameObject.transform.localScale += new Vector3(1, 1, 1);
        }
        yield return new WaitForSeconds(0.01f);
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        this.gameObject.SetActive(false);
        StopCoroutine("vanish_cor");
    }
}
