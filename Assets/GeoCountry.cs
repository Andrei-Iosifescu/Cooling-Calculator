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
public class GeoData
{
	public string countryCode;
	public string city;
	public string region;
	public string lat;
	public string lon;
}
[System.Serializable]
public class GeoElevation
{
	public string elevation;
}
[System.Serializable]
public class WeatherData
{
	public string weather;
}

public class GeoCountry : MonoBehaviour
{
	public InputField hr;
	public InputField enthalpy;
	public InputField humidity;
	public InputField dewpoint;
	public InputField wetbulb;
	public InputField air;


	static public Boolean firstPass;
	static public Boolean twoBlocked;
	static public Boolean threeBlocked;
	static public String inf;

	public Dropdown inputDropdown;
	public Dropdown metricDropdown;

	public InputField db;
	public InputField rh;
	public InputField eleva;
	public Text info;
	public string data = null;
	public string dat = null;
	public GeoData tempo = new GeoData();
	public GeoElevation tem = new GeoElevation();
	public WeatherData temps = new WeatherData();
	public Toggle changes;

	public string lat;
	public string lon;

	public  CanvasGroup makeInvis;
	// Use this for initialization
	void Start()
	{
		
		MainMenu.ErrMsg = 0;
		Dropdown inputs = metricDropdown.GetComponent<Dropdown>();
		inputs.onValueChanged.AddListener(delegate
		{
			metricValueChanged(metricDropdown);
		});
		Dropdown inputss = inputDropdown.GetComponent<Dropdown>();
		inputss.onValueChanged.AddListener(delegate
		{
			DropdownValueChanged(inputDropdown);
		});
		if(inputDropdown.value==2)
        {
			makeInvis.alpha = 1;
			makeInvis.blocksRaycasts = true;
		}
		else
        {
			makeInvis.alpha = 0;
			makeInvis.blocksRaycasts = false;
		}
		if (city.cityInputted && MainMenu.inpOption == 1)
		{
			StartCoroutine(GetWeather("api.openweathermap.org/data/2.5/weather?id=" + city.CityId + "&appid={API Key}&units=imperial"));
			inf = "Currently getting weather information from " + city.CityName;
			//StartCoroutine(GetWeather("api.openweathermap.org/data/2.5/weather?q=" + city.CityName +","+ city.StateName +","+city.CountryName+"&appid={API Key}&units=imperial"));
		}
		if (!firstPass)
		{

			if (!city.cityInputted && MainMenu.inpOption == 0)
			{
				StartCoroutine(GetLocation());
			}

			if (MainMenu.inpOption == 2)
			{
				info.text = "";
			}
			if (MainMenu.ErrMsg == 1 || MainMenu.ErrMsg == 2)
			{
				firstPass = true;
				DontDestroyOnLoad(this);
				SceneManager.LoadScene(sceneName: "message");
			}
		}
		info.text = inf;

		if (twoBlocked)
		{
			wetbulb.interactable = false;
			db.interactable = false;
		}
		if (threeBlocked)
		{
			eleva.interactable = false;
		}
		firstPass = true;
	}
	void metricValueChanged(Dropdown change)
    {
		if (!city.cityInputted && MainMenu.inpOption == 0)
		{
			StartCoroutine(GetLocation());
		}
		if (city.cityInputted && MainMenu.inpOption == 1 && MainMenu.metOption==1)
		{
			inf = "Currently getting weather information from " + city.CityName;
			info.text = inf;
			StartCoroutine(GetWeather("api.openweathermap.org/data/2.5/weather?id=" + city.CityId + "&appid={API Key}&units=metric"));
			eleva.text = "";
			//StartCoroutine(GetWeather("api.openweathermap.org/data/2.5/weather?q=" + city.CityName +","+ city.StateName +","+city.CountryName+"&appid={API Key}&units=imperial"));
		}
		else if (city.cityInputted && MainMenu.inpOption == 1 && MainMenu.metOption == 0)
		{
			inf = "Currently getting weather information from " + city.CityName;
			info.text = inf;
			StartCoroutine(GetWeather("api.openweathermap.org/data/2.5/weather?id=" + city.CityId + "&appid={API Key}&units=imperial"));
			eleva.text = "";
			//StartCoroutine(GetWeather("api.openweathermap.org/data/2.5/weather?q=" + city.CityName +","+ city.StateName +","+city.CountryName+"&appid={API Key}&units=imperial"));
		}
	}
	void DropdownValueChanged(Dropdown change)
	{
		if (!city.cityInputted && MainMenu.inpOption == 0)
		{
			wetbulb.interactable = false;
			db.interactable = false;
			twoBlocked = true;
			StartCoroutine(GetLocation());
		}
		if (city.cityInputted && MainMenu.inpOption == 1)
		{
			wetbulb.interactable = false;
			db.interactable = false;
			twoBlocked = true;
			StartCoroutine(GetWeather("api.openweathermap.org/data/2.5/weather?id="+ city.CityId+"&appid={API Key}&units=imperial"));
			inf = "Currently getting weather information from " + city.CityName;
			info.text = inf;
			eleva.text = "";
			//StartCoroutine(GetWeather("api.openweathermap.org/data/2.5/weather?q=" + city.CityName +","+ city.StateName +","+city.CountryName+"&appid={API Key}&units=imperial"));
		}
		if (MainMenu.inpOption == 2)
		{
			if (inputDropdown.value == 2)
			{
				makeInvis.alpha = 1;
				makeInvis.blocksRaycasts = true;
			}
			twoBlocked = false;
			threeBlocked = false;
			info.text = "";
			eleva.interactable = true;
			wetbulb.interactable = true;
			db.interactable = true;

			air.text = "";
			db.text = "";
			eleva.text = "";
			rh.text = "";
			eleva.text ="0";
			wetbulb.text = "";
			hr.text = "";
			enthalpy.text = "";
			humidity.text = "";
			dewpoint.text = "";
		}
		else
        {
			makeInvis.alpha = 0;
			makeInvis.blocksRaycasts = false;
			info.text = "";
			wetbulb.interactable = false;
			db.interactable = false;
			twoBlocked = true;

		}

	}
	IEnumerator GetRequest(string uri)
	{
		using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
		{
			yield return webRequest.SendWebRequest();
			string[] pages = uri.Split('/');
			int page = pages.Length - 1;
			if (webRequest.isNetworkError)
			{
				inputDropdown.value = 2;
				Debug.Log(pages[page] + ": Error: " + webRequest.error);
				if (!firstPass)
				{
					MainMenu.ErrMsg = 2;
				}
			}
			else
			{
				Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
				lon = tempo.lon;
				lat = tempo.lat;
				data = webRequest.downloadHandler.text;
				tempo = JsonUtility.FromJson<GeoData>(data);
				if (MainMenu.metOption == 0)
				{
					inf = "Currently getting weather information from " + tempo.city + "," + tempo.region + "," + tempo.countryCode;
					info.text = inf;
					lon = tempo.lon;
					lat = tempo.lat;
					string temp = "https://elevation-api.io/api/elevation?points=(" + lat + "," + lon + ")&key=JBcy2dZ8l7Kq8znbw1M40Vo0bte95j";
					StartCoroutine(GetLocationbyIp(temp));
					StartCoroutine(GetWeather("api.openweathermap.org/data/2.5/weather?q=" + tempo.city + "," + tempo.region + "," + tempo.countryCode + "&appid={API Key}&units=imperial"));

				}
				else
                {
					inf = "Currently getting weather information from " + tempo.city + "," + tempo.region + "," + tempo.countryCode;
					info.text = inf;
					lon = tempo.lon;
					lat = tempo.lat;
					string temp = "https://elevation-api.io/api/elevation?points=(" + lat + "," + lon + ")&key=JBcy2dZ8l7Kq8znbw1M40Vo0bte95j";
					StartCoroutine(GetLocationbyIp(temp));
					StartCoroutine(GetWeather("api.openweathermap.org/data/2.5/weather?q=" + tempo.city + "," + tempo.region + "," + tempo.countryCode + "&appid={API Key}&units=metric"));
				}
				inf = "Currently getting weather information from " + tempo.city + "," + tempo.region + "," + tempo.countryCode;
			}
		}
	}
	IEnumerator GetWeather(string uri)
	{
		using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
		{
			yield return webRequest.SendWebRequest();
			string[] pages = uri.Split('/');
			int page = pages.Length - 1;
			if (webRequest.isNetworkError)
			{
				inputDropdown.value = 2;
				Debug.Log(pages[page] + ": Error: " + webRequest.error);
				if (!firstPass)
				{
					MainMenu.ErrMsg = 2;
				}
			}
			else
			{
				Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
				string json = webRequest.downloadHandler.text;
				int firstStringPosition = json.IndexOf("temp");
				json = json.Substring(firstStringPosition);
				firstStringPosition = json.IndexOf("f");
				json = json.Substring(6, firstStringPosition-8) ;
				db.text = json;
				
				json = webRequest.downloadHandler.text;
				firstStringPosition = json.IndexOf("humidity");
				json = json.Substring(firstStringPosition);


				json=json.Substring(10);
				firstStringPosition = json.IndexOf(",");
				json = json.Substring(0, firstStringPosition);

				if (!json.Contains("}"))
				{
					rh.text = json;
				}
				else
                {
					firstStringPosition = json.IndexOf("}");
					json = json.Substring(0, firstStringPosition);
					rh.text = json;
				}

				Calculations.AIRMM = air.text;
				Calculations.DRYBULBMM = db.text;
				Calculations.ELEVATIONMM = eleva.text;
				Calculations.RHMM = rh.text;
				if (changes.isOn)
				{
					changes.isOn = false;
				}
				else
				{
					changes.isOn = true;
				}
				eleva.text = Calculations.ELEVATIONMM;
				wetbulb.text = Calculations.WETBULBMM;
				hr.text = Calculations.HRMM;
				enthalpy.text = Calculations.ENTHALPYMM;
				humidity.text = Calculations.HUMIDITYMM;
				dewpoint.text = Calculations.DEWPOINTMM;

				twoBlocked = true;


			}
		}
	}
	IEnumerator GetLocation()
	{
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser)
		{
			if(!firstPass)
            {
				MainMenu.ErrMsg = 1;
            }
			StartCoroutine(GetRequest("http://ip-api.com/json"));
			yield break;
		}

		// Start service before querying location
		Input.location.Start();
		//Input.compass.enabled = true;

		// Wait until service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}
		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
			eleva.text = "Timed out";
			StartCoroutine(GetRequest("http://ip-api.com/json"));
			if (!firstPass)
			{
				MainMenu.ErrMsg = 1;
			}
			yield break;
		}
		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			if (!firstPass)
			{
				MainMenu.ErrMsg = 1;
			}
			StartCoroutine(GetRequest("http://ip-api.com/json"));
			yield break;
		}
		else
		{
			// Access granted and location value could be retrieved
			int sec = 0;
			if (Input.location.lastData.altitude != 0)
			{
				eleva.text = "" + Input.location.lastData.altitude;
				threeBlocked = true;
				if (MainMenu.metOption == 0)
				{
					StartCoroutine(GetWeather("api.openweathermap.org/data/2.5/weather?lat=" + Input.location.lastData.latitude + "&lon=" + Input.location.lastData.longitude + "&appid={API Key}&units=imperial"));
				}
				else
                {
					StartCoroutine(GetWeather("api.openweathermap.org/data/2.5/weather?lat=" + Input.location.lastData.latitude + "&lon=" + Input.location.lastData.longitude + "&appid={API Key}&units=metric"));
				}
			}
			//test.text = sec + "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
			sec++;
		}
		// Stop service if there is no need to query location updates continuously
		Input.location.Stop();
	}
	IEnumerator GetLocationbyIp(string ur)
	{
		using (UnityWebRequest webRequest = UnityWebRequest.Get(ur))
		{
			yield return webRequest.SendWebRequest();
			string[] pages = ur.Split('/');
			int page = pages.Length - 1;
			if (webRequest.isNetworkError)
			{
				inputDropdown.value = 2;
				Debug.Log(pages[page] + ": Error: " + webRequest.error);
				if (!firstPass)
				{
					MainMenu.ErrMsg = 2;
				}
			}
			else
			{
				Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
				data = webRequest.downloadHandler.text;
			}
			dat = webRequest.downloadHandler.text;
			dat = dat.Substring(15,dat.Length-38);
			tem = JsonUtility.FromJson<GeoElevation>(dat);
			Debug.Log(tem.elevation);
			double d;
			if (MainMenu.metOption==0)
            {
				d = double.Parse(tem.elevation) * 3.281;
			}
			else
            {
				d = double.Parse(tem.elevation);
			}
			Calculations.ELEVATIONMM = d.ToString();
			eleva.text = d.ToString();
		}

	}
		// Update is called once per frame
		void Update()
	{
		
	}
}