using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UpdateMashin : MonoBehaviour
{
    GameObject player;
    public GameObject CanvasGetIntoUpdates;
    public GameObject CanvasUpdates;
    bool ChekForButton = false;
    bool ChekForExet = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ChekForButton)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                ActivateUpdates();
            }
        }
        else if (ChekForExet)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                DeactivateUpdates();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            CanvasGetIntoUpdates.SetActive(true);
            ChekForButton = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CanvasGetIntoUpdates.SetActive(false);
            ChekForButton = false;
        }
    }

    public void ActivateUpdates()
    {
        Cursor.lockState = CursorLockMode.None;
        CanvasUpdates.SetActive(true);
        CanvasGetIntoUpdates.SetActive(false);
        ChekForButton = false;
        ChekForExet = true;
        player.GetComponent<FirstPersonController>().enabled = false;

    }

    public void DeactivateUpdates()
    {
        Cursor.lockState = CursorLockMode.Locked;
        CanvasUpdates.SetActive(false);
        CanvasGetIntoUpdates.SetActive(true);
        ChekForButton = true;
        ChekForExet = false;
        player.GetComponent<FirstPersonController>().enabled = true;
    }


}
