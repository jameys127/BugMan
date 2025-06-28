using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] tabs;
    [SerializeField] private GameObject[] pages;
    // Start is called before the first frame update
    void Start()
    {
        ActivatePage(0);
    }

    public void ActivatePage(int tabNum){
        for(int i = 0; i < pages.Length; i++){
            pages[i].SetActive(false);
            tabs[i].color = Color.grey;
        }
        pages[tabNum].SetActive(true);
        tabs[tabNum].color = Color.white;
    }
}
