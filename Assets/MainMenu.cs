using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using System.Globalization;


public class MainMenu : MonoBehaviour
{

    //inputs
    public Dropdown inputDropdown;
    public Dropdown inpConds;
    public Text topName;
    public Text bottomName;
    public Text topUnit;
    public Text bottomUnit;

    public Text errormsg;
    public bool showError = false;
    public int time = 0;

    public InputField air;
    public InputField drybulb;
    public InputField elevation;
    public InputField wetbulb;
    
    public Button calc;
    public Button reset;
    
    public InputField rh;
    public InputField hr;
    public InputField enthalpy;
    public InputField humidity;
    public InputField dewpoint;
    public double ar = 0;
    public double db = 0;
    public double wb = 0;
    public double ElevInFt = 0;

    static public int condOption; 
    static public int inpOption;
    static public int metOption;
    static public int lastOption=4;
    static public int ErrMsg=0;

    //units
    public Dropdown unitDropdown;
    public bool isImp = true;
    public Text airU;
    public Text drybulbU;
    public Text elevationU;
    public Text wetbulbU;
    public Text rhU;
    public Text hrU;
    public Text enthalpyU;
    public Text humidityU;
    public Text dewpointU;


    //buttons at bottom
    public Button switch1;
    public Button switch2;
    public Button switch3;

    //toggles
    public Toggle changes;


    // Start is called before the first frame update
    void Start()
    {
        if(!string.IsNullOrEmpty(Calculations.DEWPOINTMM))
        {
            air.text=Calculations.AIRMM;
            drybulb.text=Calculations.DRYBULBMM;
            elevation.text= Calculations.ELEVATIONMM;
            wetbulb.text=Calculations.WETBULBMM;
            rh.text = Calculations.RHMM;
            hr.text = Calculations.HRMM;
            enthalpy.text = Calculations.ENTHALPYMM;
            humidity.text = Calculations.HUMIDITYMM;
            dewpoint.text = Calculations.DEWPOINTMM;
        }


        //buttons listeners
        Button btn = calc.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

        Button btnr = reset.GetComponent<Button>();
        btnr.onClick.AddListener(ResetClick);

        Button btns = switch1.GetComponent<Button>();
        btns.onClick.AddListener(switcher1);

        Button btnss = switch2.GetComponent<Button>();
        btnss.onClick.AddListener(switcher2);

        Button btnsss = switch3.GetComponent<Button>();
        btnsss.onClick.AddListener(switcher3);

        //dropdown listeners
        unitDropdown.value = metOption;
        Dropdown unitDropdowns = unitDropdown.GetComponent<Dropdown>();
        unitDropdowns.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(unitDropdown);
        });

        inputDropdown.value = inpOption;
        Dropdown unitDropdownss = inputDropdown.GetComponent<Dropdown>();
        unitDropdownss.onValueChanged.AddListener(delegate
        {
            DropdownsValueChanged(unitDropdown);
        });

        inpConds.value = condOption;
        Dropdown unitDropdownsss = inpConds.GetComponent<Dropdown>();
        unitDropdownsss.onValueChanged.AddListener(delegate
        {
            conditionChanged(unitDropdown);
        });
    }
    void TaskOnClick()
    {
        if(inpOption==2)
        {
            Calculations.AIRMM = air.text;
            Calculations.DRYBULBMM = drybulb.text;
            Calculations.ELEVATIONMM = elevation.text;
            Calculations.WETBULBMM = wetbulb.text;
            if (changes.isOn)
            {
                changes.isOn = false;
            }
            else
            {
                changes.isOn = true;
            }
            elevation.text = Calculations.ELEVATIONMM;
            if (inpConds.value==0)
            {
                rh.text = Calculations.RHMM;
            }
            else
            {
                rh.text = Calculations.WETBULBMM;
            }
            hr.text = Calculations.HRMM;
            enthalpy.text = Calculations.ENTHALPYMM;
            humidity.text = Calculations.HUMIDITYMM;
            dewpoint.text = Calculations.DEWPOINTMM;
        }
        else
        {
            Calculations.AIRMM = air.text;
            Calculations.DRYBULBMM = drybulb.text;
            Calculations.ELEVATIONMM = elevation.text;
            Calculations.RHMM = rh.text;
            if (changes.isOn)
            {
                changes.isOn = false;
            }
            else
            {
                changes.isOn = true;
            }
            elevation.text = Calculations.ELEVATIONMM;
            wetbulb.text = Calculations.WETBULBMM;
            hr.text = Calculations.HRMM;
            enthalpy.text = Calculations.ENTHALPYMM;
            humidity.text = Calculations.HUMIDITYMM;
            dewpoint.text = Calculations.DEWPOINTMM;
        }

        //changes.isOn = true;

    }
    // Update is called once per frame
    void ResetClick()
    {
        if(inpOption==2)
        {
            air.text = "0";
            drybulb.text = "0";
            elevation.text = "0";
            wetbulb.text = "0";
        }
        else
        {
            air.text = "0";
            elevation.text = "0";
        }
        TaskOnClick();
    }
    void Update()
    {
        if(showError)
        {
            time++;
            if(time>1000)
            {
                errormsg.text = "";
                showError = false;
            }
        }
    }
    //dropdown functions
    void conditionChanged(Dropdown change)
    {
        if (inpConds.value==0)
        {
            condOption = 0;
            topName.text = "Wet Bulb";
            bottomName.text = "RH%";
            topUnit.text = "F";
            bottomUnit.text = "%";
        }
        else
        {
            condOption = 1;
            topName.text = "RH%";
            bottomName.text = "Wet Bulb";
            topUnit.text = "%";
            bottomUnit.text = "F";
        }
    }
    void DropdownsValueChanged(Dropdown change)
    {
        inpOption = inputDropdown.value;
        if (inputDropdown.value == 1)
        {
            DontDestroyOnLoad(this);
            SceneManager.LoadScene(sceneName: "citySelect");

        }
        else
        {
            lastOption = inputDropdown.value;
            city.cityInputted = false;
        }
    }
    void DropdownValueChanged(Dropdown change)
    {
        metOption = unitDropdown.value;
        //TaskOnClick();
        if (unitDropdown.value == 1)
        {  
            isImp = false;
            airU.text = "CMM";
            drybulbU.text = "C";
            elevationU.text = "M";
            wetbulbU.text = "C";
            hrU.text = "kgw/kga";
            enthalpyU.text = "btu/kg";
            humidityU.text = "grain/kg";
            dewpointU.text = "C";
        }
        else if (unitDropdown.value == 0)
        {
            isImp = true;
            airU.text = "CFM";
            drybulbU.text = "F";
            elevationU.text = "FT";
            wetbulbU.text = "F";
            hrU.text = "lbw/\nlba";
            enthalpyU.text = "btu/lb";
            humidityU.text = "grain/lb";
            dewpointU.text = "F";
        }
    }
    //button functions
    void switcher1()
    {
        double hold = Double.Parse(rh.text);
        if (String.Compare(air.text, "0") == 0)
        {
            errormsg.text = "*Please input valid CFM*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(air.text))
        {
            errormsg.text = "*Please input valid CFM*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(drybulb.text))
        {
            errormsg.text = "*Please input valid DryBulb*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(elevation.text))
        {
            errormsg.text = "*Please input valid Elevation*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(air.text))
        {
            errormsg.text = "*Please input valid Wetbulb*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(rh.text))
        {
            errormsg.text = "*Please input valid Rh*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(hr.text))
        {
            errormsg.text = "*Please input valid Hr*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(enthalpy.text))
        {
            errormsg.text = "*Please input valid Enthalpy*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(humidity.text))
        {
            errormsg.text = "*Please input valid Humidity*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(dewpoint.text))
        {
            errormsg.text = "*Please input valid Dewpoint*";
            showError = true;
            time = 0;
        }
        else if (hold < 0 && hold > 100)
        {
            errormsg.text = "*Please input valid RH*";
            showError = true;
            time = 0;
        }
        else
        {
            TaskOnClick();
            SceneManager.LoadScene(sceneName: "indi");
        }
    }
    void switcher2()
    {
       
        double hold = Double.Parse(rh.text);
        if (String.Compare(air.text, "0") == 0)
        {
            errormsg.text = "*Please input valid CFM*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(air.text))
        {
            errormsg.text = "*Please input valid CFM*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(drybulb.text))
        {
            errormsg.text = "*Please input valid DryBulb*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(elevation.text))
        {
            errormsg.text = "*Please input valid Elevation*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(air.text))
        {
            errormsg.text = "*Please input valid Wetbulb*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(rh.text))
        {
            errormsg.text = "*Please input valid Rh*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(hr.text))
        {
            errormsg.text = "*Please input valid Hr*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(enthalpy.text))
        {
            errormsg.text = "*Please input valid Enthalpy*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(humidity.text))
        {
            errormsg.text = "*Please input valid Humidity*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(dewpoint.text))
        {
            errormsg.text = "*Please input valid Dewpoint*";
            showError = true;
            time = 0;
        }
        else if(hold < 0 && hold > 100)
        {
            errormsg.text = "*Please input valid RH*";
            showError = true;
            time = 0;
        }
        else
        {
            TaskOnClick();
            SceneManager.LoadScene(sceneName: "cool");
        }
    }
    void switcher3()
    {
        double hold = Double.Parse(rh.text);
        if (String.Compare(air.text, "0") == 0)
        {
            errormsg.text = "*Please input valid CFM*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(air.text))
        {
            errormsg.text = "*Please input valid CFM*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(drybulb.text))
        {
            errormsg.text = "*Please input valid DryBulb*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(elevation.text))
        {
            errormsg.text = "*Please input valid Elevation*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(air.text))
        {
            errormsg.text = "*Please input valid Wetbulb*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(rh.text))
        {
            errormsg.text = "*Please input valid Rh*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(hr.text))
        {
            errormsg.text = "*Please input valid Hr*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(enthalpy.text))
        {
            errormsg.text = "*Please input valid Enthalpy*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(humidity.text))
        {
            errormsg.text = "*Please input valid Humidity*";
            showError = true;
            time = 0;
        }
        else if (string.IsNullOrEmpty(dewpoint.text))
        {
            errormsg.text = "*Please input valid Dewpoint*";
            showError = true;
            time = 0;
        }
        else if (hold < 0 && hold > 100)
        {
            errormsg.text = "*Please input valid RH*";
            showError = true;
            time = 0;
        }
        else
        {
            TaskOnClick();
            SceneManager.LoadScene(sceneName: "overall");
        }
    }
}