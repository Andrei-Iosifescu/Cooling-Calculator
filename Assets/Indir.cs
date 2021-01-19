using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using System.Globalization;

public class Indir : MonoBehaviour
{
    //indirect media inputs
    public InputField effectiveness1;
    public InputField air1;
    public InputField drybulb1;
    public InputField elevation1;
    public InputField wetbulb1;
    public InputField rh1;
    public InputField hr1;
    public InputField enthalpy1;
    public InputField humidity1;
    public InputField dewpoint1;

    //direct media inputs
    public InputField effectiveness2;
    public InputField air2;
    public InputField drybulb2;
    public InputField elevation2;
    public InputField wetbulb2;
    public InputField rh2;
    public InputField hr2;
    public InputField enthalpy2;
    public InputField humidity2;
    public InputField dewpoint2;

    //toggles
    public Toggle indirect;
    string indirectHold = "";

    public Toggle direct;
    string directHold = "";
    public Toggle changes;

    //static
    public static bool isOn1;
    public static bool isOn2;
    public static bool isFirst;

    //buttons
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
        //init
        Calculations.EFFECTIVENESS1MM = "80";
        Calculations.EFFECTIVENESS2MM = "95";
        effectiveness1.text = Calculations.EFFECTIVENESS1MM;
        effectiveness2.text = Calculations.EFFECTIVENESS2MM;

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
        //listeners
        Toggle tog = indirect.GetComponent<Toggle>();
        tog.onValueChanged.AddListener(delegate {
            indirectToggle(tog);
        });
        Toggle togs = direct.GetComponent<Toggle>();
        togs.onValueChanged.AddListener(delegate {
            directToggle(togs);
        });
        
        if (isFirst)
        {
            indirect.isOn = isOn1;
            direct.isOn = isOn2;
        }
        else
        {
            isFirst = true;
            indirect.isOn = true;
            direct.isOn = true;
        }

        Button btns = switch1.GetComponent<Button>();
        btns.onClick.AddListener(switcher1);

        Button btnss = switch2.GetComponent<Button>();
        btnss.onClick.AddListener(switcher2);

        Button btnsss = switch3.GetComponent<Button>();
        btnsss.onClick.AddListener(switcher3);

        effectiveness1.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        effectiveness2.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }
    void TaskOnClick()
    {
        Calculations.EFFECTIVENESS1MM = effectiveness1.text;
        Calculations.EFFECTIVENESS2MM = effectiveness2.text;
        if (changes.isOn)
        {
            changes.isOn = false;
        }
        else
        {
            changes.isOn = true;
        }
        //effectiveness1.text = Calculations.EFFECTIVENESS1MM;
        air1.text = Calculations.AIRMM;
        drybulb1.text = Calculations.DRYBULB1MM;
        elevation1.text = Calculations.ELEVATION1MM;
        wetbulb1.text = Calculations.WETBULB1MM;
        rh1.text = Calculations.RH1MM;
        hr1.text = Calculations.HR1MM;
        enthalpy1.text = Calculations.ENTHALPY1MM;
        humidity1.text = Calculations.HUMIDITY1MM;
        dewpoint1.text = Calculations.DEWPOINT1MM;
        //effectiveness1.text = Calculations.EFFECTIVENESS1MM;
        air2.text = Calculations.AIRMM;
        drybulb2.text = Calculations.DRYBULB2MM;
        elevation2.text = Calculations.ELEVATION2MM;
        wetbulb2.text = Calculations.WETBULB2MM;
        rh2.text = Calculations.RH2MM;
        hr2.text = Calculations.HR2MM;
        enthalpy2.text = Calculations.ENTHALPY2MM;
        humidity2.text = Calculations.HUMIDITY2MM;
        dewpoint2.text = Calculations.DEWPOINT2MM;

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
    void indirectToggle(Toggle change)
    {
        byte r = 114;
        byte g = 255;
        byte b = 122;
        byte a = 255;
        if (change.isOn)
        {
            effectiveness1.text = indirectHold;
            effectiveness1.interactable = !effectiveness1.interactable;
            Image img = air1.GetComponent<Image>();
            img.color = new Color32(r, g, b, a);
            air1.interactable = !air1.interactable;
            img = drybulb1.GetComponent<Image>();
            img.color = new Color32(r, g, b, a);
            drybulb1.interactable = !drybulb1.interactable;
            img = elevation1.GetComponent<Image>();
            img.color = new Color32(r, g, b, a);
            elevation1.interactable = !elevation1.interactable;
            img = wetbulb1.GetComponent<Image>();
            img.color = new Color32(r, g, b, a);
            wetbulb1.interactable = !wetbulb1.interactable;
            img = rh1.GetComponent<Image>();
            img.color = new Color32(r, g, b, a);
            rh1.interactable = !rh1.interactable;
            img = hr1.GetComponent<Image>();
            img.color = new Color32(r, g, b, a);
            hr1.interactable = !hr1.interactable;
            img = enthalpy1.GetComponent<Image>();
            img.color = new Color32(r, g, b, a);
            enthalpy1.interactable = !enthalpy1.interactable;
            img = humidity1.GetComponent<Image>();
            img.color = new Color32(r, g, b, a);
            humidity1.interactable = !humidity1.interactable;
            img = dewpoint1.GetComponent<Image>();
            img.color = new Color32(r, g, b, a);
            dewpoint1.interactable = !dewpoint1.interactable;
            recalculate();
        }
        else
        {
            indirectHold = effectiveness1.text;
            effectiveness1.text = "0";
            effectiveness1.interactable = !effectiveness1.interactable;
            Image img = air1.GetComponent<Image>();
            img.color = Color.black;
            air1.interactable = !air1.interactable;
            img = drybulb1.GetComponent<Image>();
            img.color = Color.black;
            drybulb1.interactable = !drybulb1.interactable;
            img = elevation1.GetComponent<Image>();
            img.color = Color.black;
            elevation1.interactable = !elevation1.interactable;
            img = wetbulb1.GetComponent<Image>();
            img.color = Color.black;
            wetbulb1.interactable = !wetbulb1.interactable;
            img = rh1.GetComponent<Image>();
            img.color = Color.black;
            rh1.interactable = !rh1.interactable;
            img = hr1.GetComponent<Image>();
            img.color = Color.black;
            hr1.interactable = !hr1.interactable;
            img = enthalpy1.GetComponent<Image>();
            img.color = Color.black;
            enthalpy1.interactable = !enthalpy1.interactable;
            img = humidity1.GetComponent<Image>();
            img.color = Color.black;
            humidity1.interactable = !humidity1.interactable;
            img = dewpoint1.GetComponent<Image>();
            img.color = Color.black;
            dewpoint1.interactable = !dewpoint1.interactable;
            recalculate();
        }

    }
    void directToggle(Toggle change)
    {
        byte r = 153;
        byte g = 181;
        byte b = 255;
        byte a = 255;
        if (change.isOn)
        {
            effectiveness2.text = directHold;
            effectiveness2.interactable = !effectiveness2.interactable;
            Image img = air2.GetComponent<Image>();
            img.color = new Color32(r, g, b, a);
            air2.interactable = !air2.interactable;
            img = drybulb2.GetComponent<Image>();
            img.color = new Color32(r, g, b, a);
            drybulb2.interactable = !drybulb2.interactable;
            img = elevation2.GetComponent<Image>();
            img.color = new Color32(r, g, b, a);
            elevation2.interactable = !elevation2.interactable;
            img = wetbulb2.GetComponent<Image>();
            img.color = new Color32(r, g, b, a);
            wetbulb2.interactable = !wetbulb2.interactable;
            img = rh2.GetComponent<Image>();
            img.color = new Color32(r, g, b, a);
            rh2.interactable = !rh2.interactable;
            img = hr2.GetComponent<Image>();
            img.color = new Color32(r, g, b, a);
            hr2.interactable = !hr2.interactable;
            img = enthalpy2.GetComponent<Image>();
            img.color = new Color32(r, g, b, a);
            enthalpy2.interactable = !enthalpy2.interactable;
            img = humidity2.GetComponent<Image>();
            img.color = new Color32(r, g, b, a);
            humidity2.interactable = !humidity2.interactable;
            img = dewpoint2.GetComponent<Image>();
            img.color = new Color32(r, g, b, a);
            dewpoint2.interactable = !dewpoint2.interactable;
            recalculate();

        }
        else
        {
            directHold = effectiveness2.text;
            effectiveness2.text = "0";
            effectiveness2.interactable = !effectiveness2.interactable;
            Image img = air2.GetComponent<Image>();
            img.color = Color.black;
            air2.interactable = !air2.interactable;
            img = drybulb2.GetComponent<Image>();
            img.color = Color.black;
            drybulb2.interactable = !drybulb2.interactable;
            img = elevation2.GetComponent<Image>();
            img.color = Color.black;
            elevation2.interactable = !elevation2.interactable;
            img = wetbulb2.GetComponent<Image>();
            img.color = Color.black;
            wetbulb2.interactable = !wetbulb2.interactable;
            img = rh2.GetComponent<Image>();
            img.color = Color.black;
            rh2.interactable = !rh2.interactable;
            img = hr2.GetComponent<Image>();
            img.color = Color.black;
            hr2.interactable = !hr2.interactable;
            img = enthalpy2.GetComponent<Image>();
            img.color = Color.black;
            enthalpy2.interactable = !enthalpy2.interactable;
            img = humidity2.GetComponent<Image>();
            img.color = Color.black;
            humidity2.interactable = !humidity2.interactable;
            img = dewpoint2.GetComponent<Image>();
            img.color = Color.black;
            dewpoint2.interactable = !dewpoint2.interactable;
            recalculate();
        }
    }
    public void ValueChangeCheck()
    {
        recalculate();
    }
    //button functions
    void switcher1()
    {
        isOn1 = indirect.isOn;
        isOn2 = direct.isOn;
        SceneManager.LoadScene(sceneName: "MainMenu");
    }
    void switcher2()
    {
        isOn1 = indirect.isOn;
        isOn2 = direct.isOn;
        SceneManager.LoadScene(sceneName: "cool");
    }
    void switcher3()
    {
        isOn1 = indirect.isOn;
        isOn2 = direct.isOn;
        SceneManager.LoadScene(sceneName: "overall");
    }
}
