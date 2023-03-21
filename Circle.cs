using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Circle : MonoBehaviour
{
    private TMP_Text valueText;
    private RectTransform rectTransform;
    private bool transformation;
    Animation animation;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Start metódus");
        transformation = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updatePosition()
    {
        transform.position = transform.position + Vector3.right * (float) 1;
    }

    public TMP_Text ValueText
    {
        get
        {
            if (valueText) return valueText;

            valueText = GetComponentInChildren<TMP_Text>();

            return valueText;
        }
    }

    public RectTransform RectTransform
    {
        get
        {
            if (rectTransform) return rectTransform;

            rectTransform = GetComponent<RectTransform>();

            return rectTransform;
        }
    }

    public void destroyCircle()
    {
        Destroy(this);
    }
}
