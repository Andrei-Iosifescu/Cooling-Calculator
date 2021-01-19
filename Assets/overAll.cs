using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using System.Globalization;

public class overAll : MonoBehaviour
{
    //Overall
    public InputField wetbulball;
    public InputField dewpointall;
    public InputField all1;
    public InputField all2;
    public InputField all3;
    public InputField all4;
    public InputField all5;
    public InputField all6;
    public InputField all7;

    //buttons
    public Button switch1;
    public Button switch2;
    public Button switch3;

    //toggle
    public Toggle changes;

    //Overall
    public InputField air1;
    public InputField elevation1;
    public InputField drybulb1;
    public InputField wetbulb1;
    public InputField rh1;
    public InputField hr1;
    public InputField enthalpy1;
    public InputField humidity1;
    public InputField dewpoint1;
    public InputField air2;
    public InputField elevation2;
    public InputField drybulb2;
    public InputField wetbulb2;
    public InputField rh2;
    public InputField hr2;
    public InputField enthalpy2;
    public InputField humidity2;
    public InputField dewpoint2;

    public Text Conds;
    public Text ar;
    public Text ele;
    public Text db;
    public Text wb;
    public Text rh;
    public Text hr;
    public Text enth;
    public Text humi;
    public Text dp;

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

    public Text c1;
    public Text c2;
    public Text c3;
    public Text c4;


    // Start is called before the first frame update
    void Start()
    {
        TaskOnClick();

        //listeners
        Button btns = switch1.GetComponent<Button>();
        btns.onClick.AddListener(switcher1);

        Button btnss = switch2.GetComponent<Button>();
        btnss.onClick.AddListener(switcher2);

        Button btnsss = switch3.GetComponent<Button>();
        btnsss.onClick.AddListener(switcher3);

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
            c1.text = "Sensible Cooling from the Outdoor (KW)";
            c2.text = "Actual Cooling from the Outdoor (KW)";
            c3.text = "Total Sensible Cooling from the Outdoor (KW)";
            c4.text = "Total Cooling from the Outdoor (KW)";
            all1.text = (double.Parse(all1.text) * 3.5168525).ToString();
            all2.text = (double.Parse(all2.text) * 3.5168525).ToString();
            all3.text = (double.Parse(all3.text) * 3.5168525).ToString();
            all4.text = (double.Parse(all4.text) * 3.5168525).ToString();
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

    }
    void TaskOnClick()
    {
        Calculations.isCalc = true;
        if (changes.isOn)
        {
            changes.isOn = false;
        }
        else
        {
            changes.isOn = true;
        }
        wetbulball.text = Calculations.WETBULBALLMM;
        dewpointall.text = Calculations.DEWPOINTALLMM;
        all1.text = Calculations.ALL1MM;
        all2.text = Calculations.ALL2MM;
        all3.text = Calculations.ALL3MM;
        all4.text = Calculations.ALL4MM;
        all5.text = Calculations.ALL5MM;
        all6.text = Calculations.ALL6MM;
        all7.text = Calculations.ALL7MM;

        air1.text = Calculations.AIRMM;
        elevation1.text = Calculations.ELEVATIONMM;
        drybulb1.text = Calculations.DRYBULBMM;
        wetbulb1.text = Calculations.WETBULBMM;
        rh1.text = Calculations.RHMM;
        hr1.text = Calculations.HRMM;
        enthalpy1.text = Calculations.ENTHALPYMM;
        humidity1.text = Calculations.HUMIDITYMM;
        dewpoint1.text = Calculations.DEWPOINTMM;

        air2.text = Calculations.AIRMM;
        elevation2.text = Calculations.ELEVATION2MM;
        drybulb2.text = Calculations.DRYBULB2MM;
        wetbulb2.text = Calculations.WETBULB2MM;
        rh2.text = Calculations.RH2MM;
        hr2.text = Calculations.HR2MM;
        enthalpy2.text = Calculations.ENTHALPY2MM;
        humidity2.text = Calculations.HUMIDITY2MM;
        dewpoint2.text = Calculations.DEWPOINT2MM;

        string a1 = "Off";
        if(Indir.isOn1)
        {
            a1 = "On";
        }
        string a2 = "Off";
        if (Indir.isOn2)
        {
            a2 = "On";
        }
        string a3 = "Off";
        if (coolings.isOn1)
        {
            a3 = "On";
        }
        Conds.text = "Indirect Media: " +a1+"  Direct Media: "+a2+"\n"+"    Additional Coil: "+a3;
        ar.text = (Math.Round((Double.Parse(air1.text) - Double.Parse(air2.text)),2)).ToString();
        ele.text = (Math.Round((Double.Parse(elevation1.text) - Double.Parse(elevation2.text)), 2)).ToString();
        db.text = (Math.Round((Double.Parse(drybulb1.text) - Double.Parse(drybulb2.text)), 2)).ToString();
        wb.text = (Math.Round((Double.Parse(wetbulb1.text) - Double.Parse(wetbulb2.text)), 2)).ToString();
        rh.text = (Math.Round((Double.Parse(rh1.text) - Double.Parse(rh2.text)), 2)).ToString();
        hr.text = (Math.Round((Double.Parse(hr1.text) - Double.Parse(hr2.text)), 2)).ToString();
        enth.text = (Math.Round((Double.Parse(enthalpy1.text) - Double.Parse(enthalpy2.text)), 2)).ToString();
        humi.text = (Math.Round((Double.Parse(humidity1.text) - Double.Parse(humidity2.text)), 2)).ToString();
        dp.text = (Math.Round((Double.Parse(dewpoint1.text) - Double.Parse(dewpoint1.text)), 2)).ToString();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    //button functions
    void switcher1()
    {
        SceneManager.LoadScene(sceneName: "MainMenu");

    }
    void switcher2()
    {
        SceneManager.LoadScene(sceneName: "indi");
    }
    void switcher3()
    {
        SceneManager.LoadScene(sceneName: "cool");
    }

}
