using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TabMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject menuScreen;

    private InputAction tabAction;
    // Start is called before the first frame update
    void Start()
    {
        menuScreen.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTab(InputAction.CallbackContext context){
        if(context.started){
            menuScreen.SetActive(!menuScreen.activeSelf);
        }
    }
}
