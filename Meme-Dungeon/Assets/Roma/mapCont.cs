using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mapCont : MonoBehaviour
{
    public GameObject clickButton;

    public Image miniMapImage1;
    public Image miniMapImage2;
    public RawImage miniMapImage3;
    public Button miniMapButton;

    public Image BigMapImage1;
    public Image BigMapImage2;
    public RawImage BigMapImage3;
    public GameObject BigMapButton;

    public void ChangeMap()
    {
        miniMapImage1.enabled = !miniMapImage1.enabled;
        miniMapImage2.enabled = !miniMapImage2.enabled;
        miniMapImage3.enabled = !miniMapImage3.enabled;
        miniMapButton.enabled = !miniMapButton.enabled;

        BigMapImage1.enabled = !BigMapImage1.enabled;
        BigMapImage2.enabled = !BigMapImage2.enabled;
        BigMapImage3.enabled = !BigMapImage3.enabled;
        BigMapButton.SetActive(!BigMapButton.activeSelf);
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ChangeMap();
        }
    }
}
