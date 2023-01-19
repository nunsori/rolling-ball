using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class joy_stick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    public RectTransform lever;
    public RectTransform rectTransform;

    

    [SerializeField, Range(10f,150f)]
    private float leverRange;

    public Vector3 ball_angle;

    public Transform ball_transform;

    public Toggle reverse_btn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var inputDir = eventData.position - rectTransform.anchoredPosition/2;

        var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;

        lever.anchoredPosition = clampedDir;//- new Vector2(lever.sizeDelta.x / 2, lever.sizeDelta.y / 2);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var inputDir = eventData.position - rectTransform.anchoredPosition/2;

        var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;

        lever.anchoredPosition = clampedDir;// - new Vector2(lever.sizeDelta.x/2, lever.sizeDelta.y/2) ;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        caculation_angle();
    }


    public void caculation_angle()
    {
        Vector2 v1 = lever.position - rectTransform.position;
        

        float angle = Mathf.Atan2(v1.y, v1.x);

        Debug.Log(v1.y);
        Debug.Log(v1.x);

        //Hook_dir_transform.position.y = v1.y + player_transform.position.y;

        ball_angle = new Vector3(v1.x, 0, v1.y);

       // ball_angle = new Vector3(v1.x+ball_transform.position.x, 0, v1.y+ball_transform.position.y);

        //ball_angle = new Vector3(v1.x + player_transform.position.x,0, v1.y + player_transform.position.y);

        //Hook_dir_transform = new Vector3(v1.x + player_transform.position.x, v1.y + player_transform.position.y, 0);
    }

    public void toggle_chaged()
    {

    }
}
