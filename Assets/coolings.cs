using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using System.Globalization;


public class coolings : MonoBehaviour
{
    //cooling coil
    public InputField capacity;
    public InputField sensibleheat;
    public InputField latentheat;
    public InputField air3;
    public InputField drybulb3;
    public InputField elevation3;
    public InputField wetbulb3;
    public InputField rh3;
    public InputField hr3;
    public InputField enthalpy3;
    public InputField humidity3;
    public InputField dewpoint3;
    public InputField cond;

    //toggles
    public Toggle cooling;
    string coolingHold = "";
    public Toggle changes;

    public static bool isOn1;


    //button
    public Button switch1;
    public Button switch2;
    public Button switch3;

    //units
    public Text airU;
    public Text drybulbU;
    public Text elevationU;
    public Text wetbulbU;
    public Text rhU;
    public Text hrU;
    public Text enthalpyU;
    public Text humidityU;
    public Text dewpointU;

    // Start is called before the first frame update
    void Start()
    {
        //INIT
        capacity.text = Calculations.CAPACITYMM;
        if (MainMenu.metOption == 1)
        {
            airU.text = "CMM";
            drybulbU.text = "C";
            elevationU.text = "M";
            wetbulbU.text = "C";
            hrU.text = "kgw/kga";
            enthalpyU.text = "btu/kg";
            humidityU.text = "grain/kg";
            dewpointU.text = "C";
        }
        else if (MainMenu.metOption == 0)
        {
            airU.text = "CFM";
            drybulbU.text = "F";
            elevationU.text = "FT";
            wetbulbU.text = "F";
            hrU.text = "lbw/\nlba";
            enthalpyU.text = "btu/lb";
            humidityU.text = "grain/lb";
            dewpointU.text = "F";
        }
        TaskOnClick();
        //toggles
        Toggle togss = cooling.GetComponent<Toggle>();
        togss.onValueChanged.AddListener(delegate
        {
            coolingToggle(togss);
        });

        cooling.isOn = isOn1 ;
       
        Button btns = switch1.GetComponent<Button>();
        btns.onClick.AddListener(switcher1);

        Button btnss = switch2.GetComponent<Button>();
        btnss.onClick.AddListener(switcher2);

        Button btnsss = switch3.GetComponent<Button>();
        btnsss.onClick.AddListener(switcher3);

        capacity.onValueChanged.AddListener(delegate { ValueChangeCheck(); });


    }
    void TaskOnClick()
    {
        Calculations.CAPACITYMM = capacity.text;
        Calculations.isDone = true;
        if (changes.isOn)
        {
            changes.isOn = false;
        }
        else
        {
            changes.isOn = true;
        }

        air3.text = Calculations.AIRMM;
        drybulb3.text = Calculations.DRYBULB3MM;
        elevation3.text = Calculations.ELEVATION1MM;
        wetbulb3.text = Calculations.WETBULB3MM;
        rh3.text = Calculations.RH3MM;
        hr3.text = Calculations.HR3MM;
        enthalpy3.text = Calculations.ENTHALPY3MM;
        humidity3.text = Calculations.HUMIDITY3MM;
        dewpoint3.text = Calculations.DEWPOINT3MM;
        sensibleheat.text = Calculations.SENSIBLEHEATMM;
        cond.text = Calculations.CONDMM;
        latentheat.text = Calculations.LATENTHEATMM;

    }
    // Update is called once per frame
    void Update()
    {

    }

    void recalculate()
    {
        TaskOnClick();
    }
    //toggles
    public void ValueChangeCheck()
    {
        recalculate();
    }
    void coolingToggle(Toggle change)
    {
        if (change.isOn)
        {
            capacity.text = coolingHold;
            capacity.interactable = !capacity.interactable;
            Image img = sensibleheat.GetComponent<Image>();
            img.color = Color.green;
            sensibleheat.interactable = !sensibleheat.interactable;
            img = latentheat.GetComponent<Image>();
            img.color = Color.green;
            latentheat.interactable = !latentheat.interactable;
            img = air3.GetComponent<Image>();
            img.color = Color.green;
            air3.interactable = !air3.interactable;
            img = drybulb3.GetComponent<Image>();
            img.color = Color.green;
            drybulb3.interactable = !drybulb3.interactable;
            img = elevation3.GetComponent<Image>();
            img.color = Color.green;
            elevation3.interactable = !elevation3.interactable;
            img = wetbulb3.GetComponent<Image>();
            img.color = Color.green;
            wetbulb3.interactable = !wetbulb3.interactable;
            img = rh3.GetComponent<Image>();
            img.color = Color.green;
            rh3.interactable = !rh3.interactable;
            img = hr3.GetComponent<Image>();
            img.color = Color.green;
            hr3.interactable = !hr3.interactable;
            img = cond.GetComponent<Image>();
            img.color = Color.green;
            cond.interactable = !cond.interactable;
            img = enthalpy3.GetComponent<Image>();
            img.color = Color.green;
            enthalpy3.interactable = !enthalpy3.interactable;
            img = humidity3.GetComponent<Image>();
            img.color = Color.green;
            humidity3.interactable = !humidity3.interactable;
            img = dewpoint3.GetComponent<Image>();
            img.color = Color.green;
            dewpoint3.interactable = !dewpoint3.interactable;
            recalculate();
        }
        else
        {
            coolingHold = capacity.text;
            capacity.text = "0";
            capacity.interactable = !capacity.interactable;
            Image img = cond.GetComponent<Image>();
            img.color = Color.black;
            cond.interactable = !cond.interactable;
            img = sensibleheat.GetComponent<Image>();
            img.color = Color.black;
            sensibleheat.interactable = !sensibleheat.interactable;
            img = latentheat.GetComponent<Image>();
            img.color = Color.black;
            latentheat.interactable = !latentheat.interactable;
            img = air3.GetComponent<Image>();
            img.color = Color.black;
            air3.interactable = !air3.interactable;
            img = drybulb3.GetComponent<Image>();
            img.color = Color.black;
            drybulb3.interactable = !drybulb3.interactable;
            img = elevation3.GetComponent<Image>();
            img.color = Color.black;
            elevation3.interactable = !elevation3.interactable;
            img = wetbulb3.GetComponent<Image>();
            img.color = Color.black;
            wetbulb3.interactable = !wetbulb3.interactable;
            img = rh3.GetComponent<Image>();
            img.color = Color.black;
            rh3.interactable = !rh3.interactable;
            img = hr3.GetComponent<Image>();
            img.color = Color.black;
            hr3.interactable = !hr3.interactable;
            img = enthalpy3.GetComponent<Image>();
            img.color = Color.black;
            enthalpy3.interactable = !enthalpy3.interactable;
            img = humidity3.GetComponent<Image>();
            img.color = Color.black;
            humidity3.interactable = !humidity3.interactable;
            img = dewpoint3.GetComponent<Image>();
            img.color = Color.black;
            dewpoint3.interactable = !dewpoint3.interactable;
            recalculate();
        }
    }

    //button functions
    void switcher1()
    {
        isOn1 = cooling.isOn;
        SceneManager.LoadScene(sceneName: "indi");
    }
    void switcher2()
    {
        isOn1 = cooling.isOn;
        SceneManager.LoadScene(sceneName: "MainMenu");
    }
    void switcher3()
    {
        isOn1 = cooling.isOn;
        SceneManager.LoadScene(sceneName: "overall");
    }
}
