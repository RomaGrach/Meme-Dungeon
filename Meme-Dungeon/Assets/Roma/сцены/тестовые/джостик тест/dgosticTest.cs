using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dgosticTest : MonoBehaviour
{
    //public FloatingJoystick FJ;
    public GameObject _fj;
    public RectTransform _FjHandal;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            _fj.SetActive(false);
            //FJ.setDef();
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            _fj.SetActive(true);
        }
    }
}
