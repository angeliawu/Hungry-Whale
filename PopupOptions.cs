using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupOptions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check if player pressed escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Pop up the menu
            Resources.FindObjectsOfTypeAll<OptionsMenu>()[0].gameObject.SetActive(true);
        }
    }
}
