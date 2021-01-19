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



[System.Serializable]
public class CityJ
{
    public string name;
    public string state;
    public string country;
    public int id;
}
[System.Serializable]
public class Citysj
{
    public CityJ[] citys;
}


public class city : MonoBehaviour
{


    List<string> countries = new List<string>();
    List<int> id = new List<int>();
    public Button x1;
    public Button x2;

    static public int CityId;
    public bool isFirst = true;
    static public string CityName = "";
    static public string CountryName = "";
    static public string StateName = "";
    static public bool cityInputted = false;
    public List<Dropdown.OptionData> dropdownOptions;
    public TextAsset jsonFile;
    public Citysj CityInJson;
    // Start is called before the first frame update
    void Start()
    {
        Button btnss = x1.GetComponent<Button>();
        btnss.onClick.AddListener(TaskOnExit);
        
        Button btnsss = x2.GetComponent<Button>();
        btnsss.onClick.AddListener(TaskOnExit);
       
        //StartCoroutine(Setup());
    }
    void DropdownValueChanged(Dropdown change)
    {
 
    }

    void TaskOnExit()
    {
        if (MainMenu.lastOption !=4)
        {
            MainMenu.inpOption = MainMenu.lastOption;
        }
        else
        {
            MainMenu.inpOption = 0;
        }
        Destroy(GameObject.Find("Canvas"));
        SceneManager.LoadScene(sceneName: "MainMenu");
    }
    void TaskOnDrop()
    {
    }
    public void ValueChangeCheck(string inputText)
    { 

        
    }
    // Update is called once per frame
    IEnumerator Setup()
    {
        CityInJson = JsonUtility.FromJson<Citysj>("{\"citys\":" + jsonFile.text + "}");
        foreach (CityJ cit in CityInJson.citys)
        {
            if (string.Compare(cit.state, "") != 0)
            {
                string temp = cit.name + "," + cit.country + "," + cit.state;
                id.Add(cit.id);
                countries.Add(temp);
            }
            else if (string.Compare(cit.state, "") == 0)
            {
                string temp = cit.name + "," + cit.country;
                id.Add(cit.id);
                countries.Add(temp);
            }
        }
        Debug.Log("done");
        yield return null;
    }

    void Update()
    {
        if(isFirst)
        {
            //StartCoroutine(Setup());
            isFirst = false;
        }
    }


}
