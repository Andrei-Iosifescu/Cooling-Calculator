using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using System.Globalization;

public class Calculations : MonoBehaviour
{
    //Calculate bool
    static public bool isCalc;
    static public bool isDone;
    public Toggle changes;
    //main menu
    static public string AIRMM;
    static public string DRYBULBMM;
    static public string ELEVATIONMM;
    static public string WETBULBMM;
    static public string RHMM;
    static public string HRMM;
    static public string ENTHALPYMM;
    static public string HUMIDITYMM;
    static public string DEWPOINTMM;

    public double ar = 0;
    public double db = 0;
    public double wb = 0;
    public double RH = 0;
    public double HR = 0;
    public double ElevInFt = 0;
    public double Dewpoint = 0;
    public double Enthalpy = 0;
    public double Humidity = 0;

    //indi
    static public string AIR1MM;
    static public string DRYBULB1MM;
    static public string ELEVATION1MM;
    static public string WETBULB1MM;
    static public string RH1MM;
    static public string HR1MM;
    static public string ENTHALPY1MM;
    static public string HUMIDITY1MM;
    static public string DEWPOINT1MM;
    static public string EFFECTIVENESS1MM;
    
    static public string EFFECTIVENESS2MM;
    static public string AIR2MM;
    static public string DRYBULB2MM;
    static public string ELEVATION2MM;
    static public string WETBULB2MM;
    static public string RH2MM;
    static public string HR2MM;
    static public string ENTHALPY2MM;
    static public string HUMIDITY2MM;
    static public string DEWPOINT2MM;

    //COOLING
    static public string AIR3MM;
    static public string DRYBULB3MM;
    static public string ELEVATION3MM;
    static public string WETBULB3MM;
    static public string RH3MM;
    static public string HR3MM;
    static public string ENTHALPY3MM;
    static public string HUMIDITY3MM;
    static public string DEWPOINT3MM;
    static public string CONDMM;
    static public string CAPACITYMM;
    static public string SENSIBLEHEATMM;
    static public string LATENTHEATMM;

    //OVERALL
    static public string WETBULBALLMM;
    static public string DEWPOINTALLMM;
    static public string ALL1MM;
    static public string ALL2MM;
    static public string ALL3MM;
    static public string ALL4MM;
    static public string ALL5MM;
    static public string ALL6MM;
    static public string ALL7MM;

    void Start()
    {
        Toggle togs = changes.GetComponent<Toggle>();
        togs.onValueChanged.AddListener(delegate {
            Recalculate();
        });
    }
    // Update is called once per frame
    void Update()
    {
    }
    void Recalculate()
    {
        if (MainMenu.metOption == 0)
        {
            //main manu
            if (MainMenu.inpOption == 1 || MainMenu.inpOption == 0)
            {
                if (string.IsNullOrEmpty(AIRMM))
                {
                    AIRMM = "0";
                }
                ar = double.Parse(AIRMM);
                db = double.Parse(DRYBULBMM);
                if (string.IsNullOrEmpty(ELEVATIONMM))
                {
                    ELEVATIONMM = "0";
                }
                ElevInFt = double.Parse(ELEVATIONMM);
                RH = double.Parse(RHMM);
                wb = FWetbulb(db, ElevInFt, RH);
                WETBULBMM = wb.ToString();
                //HR
                HR = HumRat(db, wb, ElevInFt);
                HRMM = HR.ToString();
                //Enthalpy
                Enthalpy = getEnthalpy(HR, db);
                ENTHALPYMM = Enthalpy.ToString();
                //Humidity
                Humidity = getHumidity(HR);
                HUMIDITYMM = Humidity.ToString();
                //Dewpoint
                Dewpoint = DewPoint(db, RH);
                DEWPOINTMM = Dewpoint.ToString();
            }
            else
            {
                if (MainMenu.condOption == 0)
                {
                    ar = double.Parse(AIRMM);
                    db = double.Parse(DRYBULBMM);
                    wb = double.Parse(WETBULBMM);
                    if (string.IsNullOrEmpty(ELEVATIONMM))
                    {
                        ELEVATIONMM = "0";
                    }
                    ElevInFt = double.Parse(ELEVATIONMM);
                    //RH%
                    RH = RelHum(db, wb, ElevInFt);
                    RHMM = RH.ToString();
                    //HR
                    HR = HumRat(db, wb, ElevInFt);
                    HRMM = HR.ToString();
                    //Enthalpy
                    Enthalpy = getEnthalpy(HR, db);
                    ENTHALPYMM = Enthalpy.ToString();
                    //Humidity
                    Humidity = getHumidity(HR);
                    HUMIDITYMM = Humidity.ToString();
                    //Dewpoint
                    Dewpoint = DewPointAl(HR, ElevInFt);
                    DEWPOINTMM = Dewpoint.ToString();
                }
                else
                { 
                    ar = double.Parse(AIRMM);
                    db = double.Parse(DRYBULBMM);
                    RH = double.Parse(WETBULBMM);
                    if (string.IsNullOrEmpty(ELEVATIONMM))
                    {
                        ELEVATIONMM = "0";
                    }
                    ElevInFt = double.Parse(ELEVATIONMM);
                    //RH%
                    RHMM = RH.ToString();
                    //wetbulb
                    //tesry
                    Debug.Log(db);
                    Debug.Log(ElevInFt);
                    Debug.Log(RH);

                    wb = WetBulb(db, ElevInFt, RH);
                    WETBULBMM = wb.ToString();
                    //HR
                    HR = HumRat(db, wb, ElevInFt);
                    HRMM = HR.ToString();
                    //Enthalpy
                    Enthalpy = getEnthalpy(HR, db);
                    ENTHALPYMM = Enthalpy.ToString();
                    //Humidity
                    Humidity = getHumidity(HR);
                    HUMIDITYMM = Humidity.ToString();
                    //Dewpoint
                    Dewpoint = DewPoint(db, RH);
                    DEWPOINTMM = Dewpoint.ToString();
                }
            }
            //indi
            
            if (String.IsNullOrEmpty(EFFECTIVENESS1MM))
            {
                EFFECTIVENESS1MM = "80";
            }
            if (String.IsNullOrEmpty(EFFECTIVENESS2MM))
            {
                EFFECTIVENESS2MM = "95";
            }
            

            //indirect media
            //air1
            AIR1MM = AIRMM;
            //db1
            double db1 = getdb1(db, wb);

            DRYBULB1MM = db1.ToString();
            //elevation1
            ELEVATION1MM = ElevInFt.ToString();
            //dewpoint1
            DEWPOINT1MM = Dewpoint.ToString();
            //HR1
            HR1MM = HR.ToString();
            //rh1
            double RH1 = getRH(db1, Dewpoint);
            RH1MM = RH1.ToString();
            //enthalpy
            double Enthalpy1 = getEnthalpy1(db1, HR);
            ENTHALPY1MM = Enthalpy1.ToString();
            //humidity
            double Humidity1 = getHumidity(HR);
            HUMIDITY1MM = Humidity1.ToString();
            //Wet Bulb
            double Wetbulb1 = WetBulb(db1, ElevInFt, RH1);
            WETBULB1MM = Wetbulb1.ToString();

            //direct media
            //air2
            AIR2MM = ar.ToString();
            //db2
            double db2 = getdb2(db1, Wetbulb1);
            DRYBULB2MM = db2.ToString();
            //elevation2
            ELEVATION2MM = ElevInFt.ToString();
            //enthalpy2
            ENTHALPY2MM = Enthalpy1.ToString();
            //Wet Bulb2
            WETBULB2MM = Wetbulb1.ToString();
            //rh2
            double test = double.Parse(EFFECTIVENESS2MM);
            if (test == 0)
            {
                RH2MM = RH1.ToString();
            }
            else
            {
                double RH2 = RelHum(db2, Wetbulb1, ElevInFt);
                RH2MM = RH2.ToString();
            }
            //hr2
            if (test == 0)
            {
                HR2MM = HR.ToString();
            }
            else
            {
                double HR2 = HumRat(db2, Wetbulb1, ElevInFt);
                HR2MM = HR2.ToString();
            }
            //humidity
            double Humidity2 = getHumidity(double.Parse(HR2MM));
            HUMIDITY2MM = Humidity2.ToString();
            //dewpoint2
            Double Dewpoint2 = DewPoint(db2, double.Parse(RH2MM));
            DEWPOINT2MM = Dewpoint2.ToString();

            //cooling coil
            Double hr2 = double.Parse(HR2MM);
            if (String.IsNullOrEmpty(CAPACITYMM))
            {
                CAPACITYMM = "0.0";
            }
            double cap = double.Parse(CAPACITYMM);
            double factorCorrection = (1.08 - (0.0000345454545454546 * ElevInFt));
            double max = ((db2 - Dewpoint2) * ar * factorCorrection) / 12000;
            double sensible = db2 - ((cap * 12000) / (factorCorrection * ar));
            //double splusl=
            //air3
            AIR3MM = ar.ToString();
            //elevation3
            ELEVATION3MM = ElevInFt.ToString();
            //enthalpy3
            double Enthalpy3 = Enthalpy1 - (cap * 12000) / ((4.42 + 0.0000715 * ElevInFt) * ar);
            ENTHALPY3MM = Enthalpy3.ToString();
            //db3
            if (cap <= max)
            {
                DRYBULB3MM = sensible.ToString();
            }
            else
            {
                double DB3 = st(hr2, ElevInFt, Dewpoint2, Enthalpy3);
                DRYBULB3MM = DB3.ToString();
            }
            //rh3
            if (cap <= max)
            {
                double temp = getRH(sensible, Dewpoint2);
                RH3MM = temp.ToString();
            }
            else
            {
                double RH3 = 100;
                RH3MM = RH3.ToString();
            }
            //sensible heat
            double Sensibleheat = factorCorrection * (db2 - double.Parse(DRYBULB3MM)) * ar / 12000;
            SENSIBLEHEATMM = Sensibleheat.ToString();
            //latent heat
            double Latentheat = cap - Sensibleheat;
            LATENTHEATMM = Latentheat.ToString();
            //dewpoint3
            if (Latentheat == 0)
            {
                DEWPOINT3MM = Dewpoint2.ToString();
            }
            else
            {
                double Dewpoint3 = DewPoint(double.Parse(DRYBULB3MM), double.Parse(RH3MM));
                DEWPOINT3MM = Dewpoint3.ToString();
            }
            //WetBulb3
            double Wetbulb3 = WetBulb(double.Parse(DRYBULB3MM), ElevInFt, double.Parse(RH3MM));
            WETBULB3MM = Wetbulb3.ToString();
            //hr3
            if (Latentheat == 0)
            {
                HR3MM = hr2.ToString();
            }
            else
            {
                double HR3 = HumRat(double.Parse(DRYBULB3MM), Wetbulb3, ElevInFt);
                HR3MM = HR3.ToString();
            }
            //humidity3
            double Humidity3 = double.Parse(HR3MM) * 7000;
            HUMIDITY3MM = Humidity3.ToString();
            //cond
            double Cond = (ar * 0.0807 * (453.59237 / 1000) * (hr2
                - double.Parse(HR3MM))) * 0.264172 * 60;
            CONDMM = Cond.ToString();

            //overall
            Double db3 = double.Parse(DRYBULB3MM);
            Double Enthalpy2 = double.Parse(ENTHALPY2MM);
            //wet bulb overall
            double WetBulbAll = ((db - db3) /
                (db - wb)) * 100;
            WETBULBALLMM = WetBulbAll.ToString();
            //dew point overall
            double DewPointAll = ((db - db3) /
                (db - Dewpoint)) * 100;
            DEWPOINTALLMM = DewPointAll.ToString();
            //all1
            double All1 = (ar * (1.08 - (0.0000345454545454546 * ElevInFt)) *
                (db - db2)) / 12000;
            ALL1MM = All1.ToString();
            //all2
            double All2 = (ar * (4.45 - (0.00014234 * ElevInFt)) * (Enthalpy -
                Enthalpy2)) / 12000;
            ALL2MM = All2.ToString();
            //all3
            double All3 = (ar * (4.45 - (0.00014234 * ElevInFt)) * (Enthalpy
                - Enthalpy3)) / 12000;
            ALL3MM = All3.ToString();
            //all4
            double All4 = (ar * (1.08 - (0.0000345454545454546 * ElevInFt)) * (db -
                db3)) / 12000;
            ALL4MM = All4.ToString();
            //all5
            double All5 = (All3 * 12000) / ((1.35 * ar) + (cap * 700));
            ALL5MM = All5.ToString();
            //all7
            double All7 = (All3 * 12000) / ((0.9 * ar) + (cap * 700));
            ALL7MM = All7.ToString();
            //all6
            double All6 = (1 - (11 / All7)) * 100;
            ALL6MM = All6.ToString();
            roundAllValues();

            isDone = false;
        }
        else
        {
            //main manu
            if (MainMenu.inpOption == 1 || MainMenu.inpOption == 0)
            {
                db = double.Parse(DRYBULBMM);
                if (string.IsNullOrEmpty(ELEVATIONMM))
                {
                    ELEVATIONMM = "0";
                }
                if (string.IsNullOrEmpty(AIRMM))
                {
                    AIRMM = "0";
                }
                ar = double.Parse(AIRMM);
                ElevInFt = double.Parse(ELEVATIONMM);
                RH = double.Parse(RHMM);
                wb = CWetbulb(db, ElevInFt, RH);
                WETBULBMM = wb.ToString();
                //HR
                HR = CHumRat(db, wb, ElevInFt);
                HRMM = HR.ToString();
                //Enthalpy
                Enthalpy = Cen(db, HR);
                ENTHALPYMM = Enthalpy.ToString();
                //Humidity
                Humidity = CgetHumidity(HR);
                HUMIDITYMM = Humidity.ToString();
                //Dewpoint
                Dewpoint = CDewPoint(db,RH);
                DEWPOINTMM = Dewpoint.ToString();
            }
            else
            {
                if (MainMenu.condOption==0)
                {
                    ar = double.Parse(AIRMM);
                    db = double.Parse(DRYBULBMM);
                    wb = double.Parse(WETBULBMM);
                    if (string.IsNullOrEmpty(ELEVATIONMM))
                    {
                        ELEVATIONMM = "0";
                    }
                    ElevInFt = double.Parse(ELEVATIONMM);
                    //RH%
                    RH = CRelHum(db, wb, ElevInFt);
                    Debug.Log(RH);
                    Debug.Log("db" + db+ "wb" + wb + "ele" + ElevInFt+"ducks");
                    RHMM = RH.ToString();
                    //HR
                    HR = CHumRat(db, wb, ElevInFt);
                    HRMM = HR.ToString();
                    //Enthalpy
                    Enthalpy = Cen(db, HR);
                    ENTHALPYMM = Enthalpy.ToString();
                    //Humidity
                    Humidity = getHumidity(HR);
                    HUMIDITYMM = Humidity.ToString();
                    //Dewpoint
                    Dewpoint = CDewPointAl(ElevInFt, HR);
                    DEWPOINTMM = Dewpoint.ToString();
                }
                else
                {
                    ar = double.Parse(AIRMM);
                    db = double.Parse(DRYBULBMM);
                    RH = double.Parse(WETBULBMM);
                    if (string.IsNullOrEmpty(ELEVATIONMM))
                    {
                        ELEVATIONMM = "0";
                    }
                    ElevInFt = double.Parse(ELEVATIONMM);
                    //RH%
                    RHMM = RH.ToString();
                    //wetbulb
                    wb = CWetbulb(db, ElevInFt, RH);
                    //HR
                    HR = CHumRat(db, wb, ElevInFt);
                    HRMM = HR.ToString();
                    //Enthalpy
                    Enthalpy = Cen(db, HR);
                    ENTHALPYMM = Enthalpy.ToString();
                    //Humidity
                    Humidity = getHumidity(HR);
                    HUMIDITYMM = Humidity.ToString();
                    //Dewpoint
                    Dewpoint = CDewPoint(db, RH);
                    DEWPOINTMM = Dewpoint.ToString();
                }
            }
            //indi
            
            if (String.IsNullOrEmpty(EFFECTIVENESS1MM))
            {
                EFFECTIVENESS1MM = "80";
            }
            if (String.IsNullOrEmpty(EFFECTIVENESS2MM))
            {
                EFFECTIVENESS2MM = "95";
            }

         
            //indirect media

            //air1
            AIR1MM = ar.ToString();
            //db1
            double db1 = getdb1(db, wb);

            DRYBULB1MM = db1.ToString();
            //elevation1
            ELEVATION1MM = ElevInFt.ToString();
            //dewpoint1
            DEWPOINT1MM = Dewpoint.ToString();
            //HR1
            HR1MM = HR.ToString();
            //rh1
            double RH1 = CRH(db1, Dewpoint);
            RH1MM = RH1.ToString();
            //enthalpy
            double Enthalpy1 = Cen(db1, HR);
            ENTHALPY1MM = Enthalpy1.ToString();
            //humidity
            double Humidity1 = getHumidity(HR);
            HUMIDITY1MM = Humidity1.ToString();
            //Wet Bulb
            double Wetbulb1 = CWetbulb(db1, ElevInFt, RH1);
            WETBULB1MM = Wetbulb1.ToString();

            //direct media
            //air2
            AIR2MM = ar.ToString();
            //db2
            double db2 = getdb2(db1, Wetbulb1);
            DRYBULB2MM = db2.ToString();
            //elevation2
            ELEVATION2MM = ElevInFt.ToString();
            //enthalpy2
            ENTHALPY2MM = Enthalpy1.ToString();
            //Wet Bulb2
            WETBULB2MM = Wetbulb1.ToString();
            //rh2
            double test = double.Parse(EFFECTIVENESS2MM);
            if (test == 0)
            {
                RH2MM = RH1.ToString();
            }
            else
            {
                double RH2 = CRelHum(db2, Wetbulb1, ElevInFt);
                RH2MM = RH2.ToString();
            }
            //hr2
            if (test == 0)
            {
                HR2MM = HR.ToString();
            }
            else
            {
                double HR2 = CHumRat(db2, Wetbulb1, ElevInFt);
                HR2MM = HR2.ToString();
            }
            //humidity
            double Humidity2 = getHumidity(double.Parse(HR2MM));
            HUMIDITY2MM = Humidity2.ToString();
            //dewpoint2
            Double Dewpoint2 = CDewPoint(db2, double.Parse(RH2MM));
            DEWPOINT2MM = Dewpoint2.ToString();
         
            //cooling coil
            Double hr2 = double.Parse(HR2MM);
            if (String.IsNullOrEmpty(CAPACITYMM))
            {
                CAPACITYMM = "0.0";
            }


            double cap = double.Parse(CAPACITYMM);
            double TempEle = toFeet(ElevInFt);
            double Tempdb2 = toFarenheit(db2);
            double Tempcap = fromkws(cap);
            double TempDewpoint2 = toFarenheit(Dewpoint2);
            double TempEnthalpy1 = toEnpthalpy(Enthalpy1);
            double Tempar = toCFM(ar);

            double factorCorrection = (1.08 - (0.0000345454545454546 * TempEle));
            double max = ((Tempdb2 - TempDewpoint2) * Tempar * factorCorrection) / 12000;
            double sensible = Tempdb2 - ((Tempcap * 12000) / (factorCorrection * Tempar));
            //double splusl=
            //air3
            AIR3MM = ar.ToString();
            //elevation3
            ELEVATION3MM = ElevInFt.ToString();
            //enthalpy3
            double Enthalpy3 = TempEnthalpy1 - (Tempcap * 12000) / ((4.42 + 0.0000715 * TempEle) * Tempar);
            ENTHALPY3MM = ((Enthalpy3 * 2.3347) -18.06).ToString();
            //db3
            if (Tempcap <= max)
            {
                DRYBULB3MM = toCelsius(sensible).ToString();
            }
            else
            {
                double DB3 = st(hr2, TempEle, TempDewpoint2, toEnpthalpy(Enthalpy3));
                DRYBULB3MM = toCelsius(DB3).ToString();
            }
            //rh3
            if (Tempcap <= max)
            {
                double temp = getRH(sensible, TempDewpoint2);
                RH3MM = temp.ToString();
            }
            else
            {
                double RH3 = 100;
                RH3MM = RH3.ToString();
            }
            //sensible heat
            double Sensibleheat = factorCorrection * (Tempdb2 - toFarenheit(double.Parse(DRYBULB3MM))) * Tempar / 12000;
            SENSIBLEHEATMM = tokws(Sensibleheat).ToString();
            //latent heat
            double Latentheat = Tempcap - Sensibleheat;
            LATENTHEATMM = tokws(Latentheat).ToString();
            //dewpoint3
            if (Latentheat == 0)
            {
                DEWPOINT3MM = Dewpoint2.ToString();
            }
            else 
            {
                double Dewpoint3 = DewPoint(toFarenheit(double.Parse(DRYBULB3MM)), toFarenheit(double.Parse(RH3MM)));
                DEWPOINT3MM = toCelsius(Dewpoint3).ToString();
            } 
            //WetBulb3
            double Wetbulb3 = WetBulb(toFarenheit(double.Parse(DRYBULB3MM)), TempEle, toFarenheit(double.Parse(RH3MM)));
            WETBULB3MM = toCelsius(Wetbulb3).ToString();
            //hr3
            if (Latentheat == 0)
            {
                HR3MM = hr2.ToString();
            }
            else
            {
                double HR3 = CHumRat(double.Parse(DRYBULB3MM), double.Parse(WETBULB3MM), ElevInFt);
                HR3MM = HR3.ToString();
            }
            //humidity3
            double Humidity3 = double.Parse(HR3MM) * 1000;
            HUMIDITY3MM = Humidity3.ToString();
            //cond
            double Cond = (Tempar * 0.0807 * (453.59237 / 1000) * (hr2
                - double.Parse(HR3MM))) * 0.264172 * 60;
            CONDMM = Cond.ToString();

            //overall
            Double db3 = double.Parse(DRYBULB3MM);
            Double Enthalpy2 = double.Parse(ENTHALPY2MM);
            //wet bulb overall

            double WetBulbAll = ((db - db3) /
                (db - wb)) * 100;
            WETBULBALLMM = WetBulbAll.ToString();
            //dew point overall
            double DewPointAll = ((db - db3) /
                (db - Dewpoint)) * 100;
            DEWPOINTALLMM = DewPointAll.ToString();
            //all1
            ar = double.Parse(AIRMM);
            ar = toCFM(ar);
            ElevInFt = toFeet(ElevInFt);
            db = toFarenheit(db);
            db2 = toFarenheit(db2);
            db3 = toFarenheit(db3);
            Enthalpy = toEnpthalpy(Enthalpy);
            Enthalpy2 = toEnpthalpy(Enthalpy2);

            double All1 = (ar * (1.08 - (0.0000345454545454546 * ElevInFt)) *
                (db - db2)) / 12000;
            ALL1MM = All1.ToString();
            //all2
            double All2 = (ar * (4.45 - (0.00014234 * ElevInFt)) * (Enthalpy -
                Enthalpy2)) / 12000;
            ALL2MM = All2.ToString();
            //all3
            double All3 = (ar * (4.45 - (0.00014234 * ElevInFt)) * (Enthalpy
                - Enthalpy3)) / 12000;
            ALL3MM = All3.ToString();
            //all4
            double All4 = (ar * (1.08 - (0.0000345454545454546 * ElevInFt)) * (db -
                db3)) / 12000;
            ALL4MM = All4.ToString();
            //all5
            double All5 = (All3 * 12000) / ((1.35 * ar) + (Tempcap * 700));
            ALL5MM = All5.ToString();
            //all7
            double All7 = (All3 * 12000) / ((0.9 * ar) + (Tempcap * 700));
            ALL7MM = All7.ToString();
            //all6
            double All6 = (1 - (11 / All7)) * 100;
            ALL6MM = All6.ToString();
            roundAllValues();
            isDone = false;
        }
    }
    //calculations
    public double RelHum(double db, double wb, double ElevInFt)
    {
        double RT = wb + 459.67;
        double pt = 14.696 * Math.Pow((1 - 0.0000068753 * ElevInFt), 5.2559);
        double c8 = -10440.4;
        double c9 = -11.29465;
        double c10 = -0.027022355;
        double c11 = 0.00001289036;
        double c12 = -0.000000002478068;
        double c13 = 6.5459673;
        double pws = Math.Exp(c8 / RT + c9 + c10 * RT + c11 *
            Math.Pow(RT, 2) + c12 * Math.Pow(RT, 3) + c13 * Math.Log(RT));
        double wsat = (pws * 0.62198) / (pt - pws);
        double wnom = (1093 - 0.556 * wb) * wsat - 0.24 * (db - wb);
        double wdenom = 1093 + 0.444 * db - wb;
        double W = wnom / wdenom;
        double pw = (W * pt) / (0.62198 + W);
        RT = db + 459.67;
        double ps = Math.Exp(c8 / RT + c9 + c10 * RT + c11 * Math.Pow(RT, 2)
            + c12 * Math.Pow(RT, 3) + c13 * Math.Log(RT));
        double pa = pt - pw;
        double relHum = 100 * (W * pa) / (0.62198 * ps);
        return relHum;
    }
    public double HumRat(double db, double wb, double ElevInFt)
    {
        double RT = wb + 459.67;
        double pt = 14.696 * Math.Pow((1 - 0.0000068753 * ElevInFt), 5.2559);
        double c8 = -10440.4;
        double c9 = -11.29465;
        double c10 = -0.027022355;
        double c11 = 0.00001289036;
        double c12 = -0.000000002478068;
        double c13 = 6.5459673;
        double pws = Math.Exp(c8 / RT + c9 + c10 * RT + c11 * Math.Pow(RT, 2)
            + c12 * Math.Pow(RT, 3) + c13 * Math.Log(RT));
        double wsat = (pws * 0.62198) / (pt - pws);
        double wnom = (1093 - 0.556 * wb) * wsat - 0.24 * (db - wb);
        double wdenom = 1093 + 0.444 * db - wb;
        double humRat = wnom / wdenom;
        return humRat;
    }
    public double getEnthalpy(double HR, double db)
    {
        double e = 0.24 * db + HR * (1061 + 0.444 * db);
        return e;
    }
    public double DewPointAl(double HR, double ElevInFt)
    {
        double p = 14.696 * Math.Pow((1 - 0.0000068753 * ElevInFt), 5.2559);
        double pw = (p * HR) / (0.62198 + HR);
        double alpha = Math.Log(pw);
        double C14 = 100.45;
        double C15 = 33.193;
        double C16 = 2.319;
        double C17 = 0.17074;
        double C18 = 1.2063;
        double DewPointAl = C14 + C15 * alpha + C16 * Math.Pow(alpha, 2) + C17 * Math.Pow(alpha, 3) + C18 * Math.Pow((pw), 0.1984);
        return DewPointAl;
    }
    public double getHumidity(double HR)
    {
        double h = HR * 7000;
        return h;
    }
    public double DewPoint(double db, double Rh)
    {
        double es = 6.11 * Math.Pow(10, ((7.5 * ((db - 32) * 5 / 9)) / (237.7 + ((db - 32) * 5 / 9))));
        double x = Math.Log(es * Rh / 611);
        double y = Math.Log(10);
        double Z = x / y;
        double dw = (237.7 * Z) / (7.5 - Z);
        dw = (dw * 9 / 5) + 32;
        return dw;
    }
    public double getdb1(double db, double wb)
    {
        double percent = double.Parse(EFFECTIVENESS1MM);
        double d = db - (db - wb) * (percent / 100);
        return d;
    }
    public double getRH(double db, double dew)
    {
        //6.11 * 10 ^ ((7.5 * ((db - 32) * 5 / 9)) / (237.7 + ((db - 32) * 5 / 9)))
        double es = 6.11 * Math.Pow(10, ((7.5 * ((db - 32) * 5 / 9)) / (237.7 + ((db - 32) * 5 / 9))));
        dew = (dew - 32) * 5 / 9;
        double Rh = (Math.Pow(10, ((dew * 7.5) / (237.7 + dew)))) * 611 / es;
        return Rh;
    }
    public double getEnthalpy1(double db1, double HR1)
    {
        double e = 0.24 * db1 + HR1 * (1061 + 0.444 * db1);
        return e;
    }
    public double getdb2(double db, double wb)
    {
        double percent = double.Parse(EFFECTIVENESS2MM);
        double d = db - (db - wb) * (percent / 100);
        return d;
    }
    public double WetBulb(double db, double ElevInFT, double Rh)
    {
        double C1 = 6.112;
        double c2 = 17.67;
        double c3 = 243.5;
        double c4 = 0.00066;
        double c5 = 0.000000759;
        double e = 2.71828;
        double DEW = 0;

        DEW = (DEW - 32) * 5 / 9;
        db = (db - 32) * 5 / 9;
        double ASUM2 = db;
        double wb = ASUM2;
        double pt = (14.696 * Math.Pow((1 - 0.0000068753 * ElevInFt), 5.2559)) * 68.9476;
        double es = C1 * Math.Pow(e, ((c2 * db) / (db + c3)));
        double et1 = es * Rh / 100;
        double et = et1;
        double Z = 100000000;
        while (Z >= et)
        {
            double z1 = C1 * Math.Pow(e, ((c2 * wb) / (wb + c3)));
            double z2 = pt * (c4 + (c5 * wb)) * (db - wb);
            double z3 = z1 - z2;
            Z = z3;
            wb = wb - 1;
            //Loop Until Z < et
        }
        double y = 0;
        while (y < et)
        {
            double y1 = C1 * Math.Pow(e, ((c2 * wb) / (wb + c3)));
            double y2 = pt * (c4 + (c5 * wb)) * (db - wb);
            double y3 = y1 - y2;
            y = y3;
            wb = wb + 0.01;
            //Loop Until y >= et
        }
        wb = (wb * 9 / 5) + 32;
        wb = Math.Round(wb, 8);
        double Wetbulb = wb;
        return Wetbulb;
    }
    public double st(double hr, double elev, double stemp, double eh)
    {
        double e1 = 0.24 * stemp + hr * (1061 + 0.444 * stemp);
        while (e1 > eh)
        {
            stemp = stemp - 0.001;
            hr = HumRat(stemp, stemp, elev);
            e1 = 0.24 * stemp + hr * (1061 + 0.444 * stemp);
        }
        double St = Math.Round(stemp, 3);
        return St;
    }

    //f calculations
    public double FWetbulb(double db,double elev,double rh)
    {
        double C1 = 6.112;
        double c2 = 17.67;
        double c3 = 243.5;
        double c4 = 0.00066;
        double c5 = 0.000000759;
        double e = 2.71828;
        double DEW = DewPoint(db,rh);

        DEW = (DEW - 32) * 5 / 9;
        db = (db - 32) * 5 / 9;
        double ASUM2 = DEW;
        double wb = ASUM2;
        double pt = (14.696 * Math.Pow((1 - 0.0000068753 * elev), 5.2559)) * 68.9476;
        double es = C1 * Math.Pow(e,((c2 * db) / (db + c3)));
        double et1 = es * rh / 100;
        double et = et1;
        double z = 0;
        while (z <= et)
        {
            double z1 = C1 * Math.Pow(e,((c2 * wb) / (wb + c3)));
            double z2 = pt * (c4 + (c5 * wb)) * (db - wb);
            double z3 = z1 - z2;
            z = z3;
            wb = wb + 1;
        }
        double y = 1000000;
        while (y > et)
        {
            double y1 = C1 * Math.Pow(e,((c2 * wb) / (wb + c3)));
            double y2 = pt * (c4 + (c5 * wb)) * (db - wb);
            double y3 = y1 - y2;
            y = y3;
            wb = wb - 0.01;
        }

        wb = (wb * 9 / 5) + 32;
        wb = Math.Round(wb, 8);
        double fwetbulb;
        if (rh >= 100)
        {
            fwetbulb = ((db * 9 / 5) + 32); 
        }
        else
        {
            fwetbulb = wb;
        }
        return fwetbulb;
    }
    //c calculations
    public double CWetbulb(double db, double elev, double rh)
    {
        double C1 = 6.112;
        double c2 = 17.67;
        double c3 = 243.5;
        double c4 = 0.00066;
        double c5 = 0.000000759;
        double e = 2.71828;
        elev = elev / 0.305;
        double ASUM2 = db;
        double wb = ASUM2;
        double pt = (14.696 * Math.Pow((1 - 0.0000068753 * elev), 5.2559)) * 68.9476;
        double es = C1 * Math.Pow(e, ((c2 * db) / (db + c3)));
        double et1 = es * rh / 100;
        double et = et1;
        double Z = 100000;
        while (Z >= et)
        {
            double z1 = C1 * Math.Pow(e, ((c2 * wb) / (wb + c3)));
            double z2 = pt * (c4 + (c5 * wb)) * (db - wb);
            double z3 = z1 - z2;
            Z = z3;
            wb = wb - 1;
        }
        double y = 0;
        while (y < et)
        {
            double y1 = C1 * Math.Pow(e, ((c2 * wb) / (wb + c3)));
            double y2 = pt * (c4 + (c5 * wb)) * (db - wb);
            double y3 = y1 - y2;
            y = y3;
            wb = wb + 0.01;
        }
        //wb = wb;
        wb = Math.Round(wb, 8);
        double value;
        if (rh >= 100)
        {
            value = db;
        }
        else
        {
            value = wb;
        }
        return value;
    }
    public double CDewPointAl(double elev, double hr)
    {
        elev = elev / 0.305;
        double p = 14.696 * Math.Pow((1 - 0.0000068753 * elev) , 5.2559);
        double pw = (p * hr) / (0.62198 + hr);
        double alpha = Math.Log(pw);
        double C14 = 100.45;
        double C15 = 33.193;
        double C16 = 2.319;
        double C17 = 0.17074;
        double C18 = 1.2063;
        //what is cr
        double cr = 0;
        double assump1 = C14 + C15 * alpha + C16 * Math.Pow(alpha, 2) +
            C17 * Math.Pow(alpha, 3) + C18 * Math.Pow((pw), 0.198) - cr;
        double value = (assump1 - 32) * 5 / 9;
        return value;
    }
    public double CDewPoint(double db,double rh)
    {
        db = (db * 9 / 5) + 32;
        double es = 6.11 * Math.Pow(10 , ((7.5 * ((db - 32) * 5 / 9)) / (237.7 + ((db - 32) * 5 / 9))));
        double x = Math.Log(es * rh / 611);
        double y = Math.Log(10);
        double Z = x / y;
        double dw = (237.7 * Z) / (7.5 - Z);
        return dw;
    }
    public double CRH(double db,double dew)
    {
        db = (db * 9 / 5) + 32;
        dew = (dew * 9 / 5) + 32;
        double es = 6.11 * Math.Pow(10 , ((7.5 * ((db - 32) * 5 / 9)) / (237.7 + ((db - 32) * 5 / 9))));
        dew = (dew - 32) * 5 / 9;
        double cRh = (Math.Pow(10 ,((dew * 7.5) / (237.7 + dew)))) * 611 / es;
        return cRh;
    }
    public double CRelHum(double db,double wb,double ElevInm)
    {
        db = (db * 9 / 5) + 32;
        wb = (wb * 9 / 5) + 32;
        ElevInm = ElevInm * 3.281;
        Debug.Log("db" + db + "wb" + wb + "ele" + ElevInm+"fucks");
        double RT = wb + 459.67;
        double pt = 14.696 * Math.Pow((1 - 0.0000068753 * ElevInm) , 5.2559);
        double c8 = -10440.4;
        double c9 = -11.29465;
        double c10 = -0.027022355;
        double c11 = 0.00001289036;
        double c12 = -0.000000002478068;
        double c13 = 6.5459673;
        double pws = Math.Exp(c8 / RT + c9 + c10 * RT + c11 * Math.Pow(RT, 2) + c12 * Math.Pow(RT, 3) + c13 * Math.Log(RT));
        double wsat = (pws * 0.62198) / (pt - pws);
        double wnom = (1093 - 0.556 * wb) * wsat - 0.24 * (db - wb);
        double wdenom = 1093 + 0.444 * db - wb;
        double W = wnom / wdenom;
        double pw = (W * pt) / (0.62198 + W);
        RT = db + 459.67;
        double ps = Math.Exp(c8 / RT + c9 + c10 * RT + c11 * Math.Pow(RT, 2) + c12 * Math.Pow(RT, 3) + c13 * Math.Log(RT));
        double pa = pt - pw;
        double value = 100 * (W * pa) / (0.62198 * ps);
        return value;
    }
    public double CHumRat(double db, double wb, double ElevInm)
    {
        db = (db * 9 / 5) + 32;
        wb = (wb * 9 / 5) + 32;
        ElevInm = ElevInm / 0.305;
        double RT = wb + 459.67;
        double pt = 14.696 * Math.Pow((1 - 0.0000068753 * ElevInm), 5.2559);
        double c8 = -10440.4;
        double c9 = -11.29465;
        double c10 = -0.027022355;
        double c11 = 0.00001289036;
        double c12 = -0.000000002478068;
        double c13 = 6.5459673;
        double pws = Math.Exp(c8 / RT + c9 + c10 * RT + c11 * Math.Pow(RT, 2) + c12 * Math.Pow(RT, 3) + c13 * Math.Log(RT));
        double wsat = (pws * 0.62198) / (pt - pws);
        double wnom = (1093 - 0.556 * wb) * wsat - 0.24 * (db - wb);
        double wdenom = 1093 + 0.444 * db - wb;
        double value = wnom / wdenom;
        return value;
    }
    public double Cen(double db, double hr)
    {
        db = (db * 9 / 5) + 32;
        double value = (0.24 * db) + (hr * (1061 + 0.444 * db));
        value = (value * 2.3347) - 18.06;
        return value;
    }
    public double CHR(double db,double en)
    {
        en = (en + 18.06) / 2.3347;
        db = (db * 9 / 5) + 32;
        double value = (en - (0.24 * db)) / (1061 + 0.444 * db);
        return value;
    }
    public double CgetHumidity(double HR)
    {
        double h = HR * 1000;
        return h;
    }
    public double CWetbulbdew(double db,double elev,double dew)
    {
        double rh = CRH(db, dew);
        db = (db - 32) * 5 / 9;
        double value = CWetbulb(db, elev, rh);
        return value;
    }
    public void roundAllValues()
    {
        double temp = Math.Round(Double.Parse(AIRMM), 2);
        AIRMM = temp.ToString();
        temp = Math.Round(Double.Parse(DRYBULBMM), 2);
        DRYBULBMM = temp.ToString();
        temp = Math.Round(Double.Parse(ELEVATIONMM), 2);
        ELEVATIONMM = temp.ToString();
        temp = Math.Round(Double.Parse(WETBULBMM), 2);
        WETBULBMM = temp.ToString();
        temp = Math.Round(Double.Parse(RHMM), 2);
        RHMM = temp.ToString();
        temp = Math.Round(Double.Parse(HRMM), 3);
        HRMM = temp.ToString();
        temp = Math.Round(Double.Parse(ENTHALPYMM), 2);
        ENTHALPYMM = temp.ToString();
        temp = Math.Round(Double.Parse(HUMIDITYMM), 2);
        HUMIDITYMM = temp.ToString();
        temp = Math.Round(Double.Parse(DEWPOINTMM), 2);
        DEWPOINTMM = temp.ToString();
        temp = Math.Round(Double.Parse(AIR1MM), 2);
        AIR1MM = temp.ToString();
        temp = Math.Round(Double.Parse(DRYBULB1MM), 2);
        DRYBULB1MM = temp.ToString();
        temp = Math.Round(Double.Parse(ELEVATION1MM), 2);
        ELEVATION1MM = temp.ToString();
        temp = Math.Round(Double.Parse(WETBULB1MM), 2);
        WETBULB1MM = temp.ToString();
        temp = Math.Round(Double.Parse(RH1MM), 2);
        RH1MM = temp.ToString();
        temp = Math.Round(Double.Parse(HR1MM), 2);
        HR1MM = temp.ToString();
        temp = Math.Round(Double.Parse(ENTHALPY1MM), 2);
        ENTHALPY1MM = temp.ToString();
        temp = Math.Round(Double.Parse(HUMIDITY1MM), 2);
        HUMIDITY1MM = temp.ToString();
        temp = Math.Round(Double.Parse(DEWPOINT1MM), 2);
        DEWPOINT1MM = temp.ToString();
        temp = Math.Round(Double.Parse(EFFECTIVENESS1MM), 2);
        EFFECTIVENESS1MM = temp.ToString();
        temp = Math.Round(Double.Parse(EFFECTIVENESS2MM), 2);
        EFFECTIVENESS2MM = temp.ToString();
        temp = Math.Round(Double.Parse(AIR2MM), 2);
        AIR2MM = temp.ToString();
        temp = Math.Round(Double.Parse(DRYBULB2MM), 2);
        DRYBULB2MM = temp.ToString();
        temp = Math.Round(Double.Parse(ELEVATION2MM), 2);
        ELEVATION2MM = temp.ToString();
        temp = Math.Round(Double.Parse(WETBULB2MM), 2);
        WETBULB2MM = temp.ToString();
        temp = Math.Round(Double.Parse(RH2MM), 2);
        RH2MM = temp.ToString();
        temp = Math.Round(Double.Parse(HR2MM), 2);
        HR2MM = temp.ToString();
        temp = Math.Round(Double.Parse(ENTHALPY2MM), 2);
        ENTHALPY2MM = temp.ToString();
        temp = Math.Round(Double.Parse(HUMIDITY2MM), 2);
        HUMIDITY2MM = temp.ToString();
        temp = Math.Round(Double.Parse(DEWPOINT2MM), 2);
        DEWPOINT2MM = temp.ToString();
        temp = Math.Round(Double.Parse(AIR3MM), 2);
        AIR3MM = temp.ToString();
        temp = Math.Round(Double.Parse(DRYBULB3MM), 2);
        DRYBULB3MM = temp.ToString();
        temp = Math.Round(Double.Parse(ELEVATION3MM), 2);
        ELEVATION3MM = temp.ToString();
        temp = Math.Round(Double.Parse(WETBULB3MM), 2);
        WETBULB3MM = temp.ToString();
        temp = Math.Round(Double.Parse(RH3MM), 2);
        RH3MM = temp.ToString();
        temp = Math.Round(Double.Parse(HR3MM), 2);
        HR3MM = temp.ToString();
        temp = Math.Round(Double.Parse(ENTHALPY3MM), 2);
        ENTHALPY3MM = temp.ToString();
        temp = Math.Round(Double.Parse(HUMIDITY3MM), 2);
        HUMIDITY3MM = temp.ToString();
        temp = Math.Round(Double.Parse(DEWPOINT3MM), 2);
        DEWPOINT3MM = temp.ToString();
        temp = Math.Round(Double.Parse(CONDMM), 2);
        CONDMM = temp.ToString();
        temp = Math.Round(Double.Parse(CAPACITYMM), 2);
        CAPACITYMM = temp.ToString();
        temp = Math.Round(Double.Parse(SENSIBLEHEATMM), 2);
        SENSIBLEHEATMM = temp.ToString();
        temp = Math.Round(Double.Parse(LATENTHEATMM), 2);
        LATENTHEATMM = temp.ToString();
        temp = Math.Round(Double.Parse(WETBULBALLMM), 2);
        WETBULBALLMM = temp.ToString();
        temp = Math.Round(Double.Parse(DEWPOINTALLMM), 2);
        DEWPOINTALLMM = temp.ToString();
        temp = Math.Round(Double.Parse(ALL1MM), 2);
        ALL1MM = temp.ToString();
        temp = Math.Round(Double.Parse(ALL2MM), 2);
        ALL2MM = temp.ToString();
        temp = Math.Round(Double.Parse(ALL3MM), 2);
        ALL3MM = temp.ToString();
        temp = Math.Round(Double.Parse(ALL4MM), 2);
        ALL4MM = temp.ToString();
        temp = Math.Round(Double.Parse(ALL5MM), 2);
        ALL5MM = temp.ToString();
        temp = Math.Round(Double.Parse(ALL6MM), 2);
        ALL6MM = temp.ToString();
        temp = Math.Round(Double.Parse(ALL7MM), 2);
        ALL7MM = temp.ToString();
    }
    
    public double toFarenheit(double t)
    {
        double x = (t * 9 / 5) + 32;
        return x;
    }
    public double toFeet(double t)
    {
        double x = t * 3.28084;
        return x;
    }
    public double toEnpthalpy(double t)
    {
        double x = (t + 18.06)/2.3347;
        return x;
    }
    public double fromkws(double t)
    {
        double x = t / 3.5168525;
        return x;
    }
    public double tokws(double t)
    {
        double x = t * 3.5168525;
        return x;
    }
    public double toCelsius(double t)
    {
        double x = (t -32) * 5/9;
        return x;
    }
    public double toCFM(double t)
    {
        double x = (t* 35.314666212661);
        return x;
    }
    public double fromCFM(double t)
    {
        double x = (t * 0.028316847000000252);
        return x;
    }

}
