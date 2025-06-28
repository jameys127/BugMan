using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject menuScreen;
    // Start is called before the first frame update
    void Start()
    {
        menuScreen.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
            menuScreen.SetActive(!menuScreen.activeSelf);
        }
    }
}
