using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using System.Globalization;
using UnityEngine.Networking;

public class MessageScene : MonoBehaviour
{
    public Text info;

    static public Boolean air;
    static public Boolean db;
    static public Boolean wb;


    public Button ok;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = ok.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnExit);

        if (MainMenu.ErrMsg==1)
        {
            info.text = "We were unable to get your devices location. Please make sure that location services have been enabled and location permission has been enabled for our app" +
                "\n"+"Otherwise we will approximate your location based on the connected network and Elevation will have to be manually inputted.";
        }
        else if(MainMenu.ErrMsg == 2)
        {
            info.text = "No network connection detected, values will have to be inputed manually";
        }
        else if(MainMenu.ErrMsg == 3)
        {
            info.text = "The following values have not been inputted \n please input the values before we can continue:\n";
            if(air)
            {
                info.text = info.text = "Air Volume\n";
            }
            if (db)
            {
                info.text = info.text = "Dry Bulb\n";
            }
            if (wb)
            {
                info.text = info.text = "Wet Bulb\n";
            }

        }
    }
    void TaskOnExit()
    {
        Destroy(GameObject.Find("Canvas"));
        SceneManager.LoadScene(sceneName: "MainMenu");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
