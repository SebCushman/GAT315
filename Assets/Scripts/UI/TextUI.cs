using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextUI : MonoBehaviour
{
    public TMP_Text txtValue = null;

    //public FloatData data = null;
    public float counter = 0;
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    void Update()
    {
        counter += Time.deltaTime;
        if(counter >= 0.5f)
        {
            txtValue.text = (1.0f / Time.deltaTime).ToString();
            counter = 0;
        }
        
    }
}
