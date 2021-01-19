using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Threading;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using System.Globalization;
using UnityEngine.Networking;


public class AutoCompleteScrollRect : MonoBehaviour
{
    private Boolean isSelected;
    public TextAsset jsonFile;
    public Citysj CityInJson;
    List<string> countries = new List<string>();
    List<int> id = new List<int>();
    public Button dd;
    string hold;

   // References
   [SerializeField] InputField m_InputField;
    [SerializeField] ScrollRect m_ScrollRect;
    private RectTransform m_ScrollRectTransform;

    // Prefab for available options
    [SerializeField] OptionButton m_OptionPrefab;

    // The transform (parent) to spawn options into
    [SerializeField] Transform m_OptionsParent;

    [SerializeField] List<string> m_Options;

    private Dictionary<string, GameObject> m_OptionObjectSpawnedDict;       // Store a list of options in a dictionary, essentially object pooling the buttons

    private float m_OriginalOffsetMinY;

    [SerializeField] int m_ComponentsHeight = 50;                           // The size of the option buttons
    [SerializeField] int m_OptionsOnDisplay = 4;                            // How many options the scrollview can display at one time

    private void Start()
    {
        Button btnss =dd.GetComponent<Button>();
        btnss.onClick.AddListener(TaskOnClick);

        m_OriginalOffsetMinY = m_ScrollRect.gameObject.GetComponent<RectTransform>().offsetMin.y;

        m_ScrollRectTransform = m_ScrollRect.gameObject.GetComponent<RectTransform>();

        m_ScrollRect.gameObject.SetActive(false);   // By default, we don't need to show the scroll view.
        StartCoroutine(Setup());


    }
    IEnumerator Setup()
    {
        CityInJson = JsonUtility.FromJson<Citysj>("{\"citys\":" + jsonFile.text + "}");
        Debug.Log("done");
        yield return null;
    }

    public void SetAndCloseScrollView(string optionLabel)
    {
        m_InputField.text = optionLabel;
        m_ScrollRect.gameObject.SetActive(false);
    }

    /// <summary>
    /// Spawns a list of all the available options into the scene, deactivates them, and adds them to the pool
    /// </summary>
    /// <param name="options"></param>
    private void SpawnClickableOptions(List<string> options)
    {
        ResetDictionaryAndCleanupSceneObjects();
        if(m_Options == null || m_Options.Count == 0)
        {
            Debug.LogError("Options lists is null or the list is == 0, please ensure it has something in it!");
            return;
        }
        for (int i = 0; i < m_Options.Count; i++)
        {
            GameObject obj = Instantiate(m_OptionPrefab.gameObject, m_OptionsParent);
            obj.transform.localScale = Vector3.one;

            m_OptionObjectSpawnedDict.Add(m_Options[i], obj);

            string opt = m_Options[i];

            obj.GetComponent<OptionButton>().Setup(m_Options[i], m_ComponentsHeight, () =>
            {
                hold = opt;
                Debug.Log("Clicked option " + opt);
                OnValueSelected();
                SetAndCloseScrollView(opt);
            });
        }

    }

    /// <summary>
    /// Cleans up the scrollview
    /// </summary>
    private void ResetDictionaryAndCleanupSceneObjects()
    {
        if (m_OptionObjectSpawnedDict == null)
        {
            m_OptionObjectSpawnedDict = new Dictionary<string, GameObject>();
            return;
        }

        if (m_OptionObjectSpawnedDict.Count == 0)
            return;

        foreach (KeyValuePair<string, GameObject> options in m_OptionObjectSpawnedDict)
            Destroy(options.Value);

        m_OptionObjectSpawnedDict.Clear();
    }

    /// <summary>
    /// Hooked up to the OnValueChanged() event of the inputfield specified, we listen out for changes within the input field.
    /// When the input.text has changed, we search the options dictionary and attempt to find matches, and display them if any.
    /// </summary>
    public void OnValueChanged()
    {
        int isfound = 0;
        if (m_InputField.text == "")
        {
            m_ScrollRect.gameObject.SetActive(false);       // Disable the scrollview if the inputfield is empty
            return;
        }
        else if (m_InputField.text.Length >= 4)
        {
            countries.Clear();
            m_OptionsOnDisplay = 4;
            foreach (CityJ cit in CityInJson.citys)
            {

                string tempw = char.ToUpper(m_InputField.text[0]) + m_InputField.text.Substring(1);
                if (cit.name.Contains(tempw) && string.Compare(cit.state, "") != 0)
                {
                    string temp = cit.name + "," + cit.country + "," + cit.state;
                    id.Add(cit.id);
                    countries.Add(temp);
                    isfound++;
                }
                else if (cit.name.Contains(tempw) && string.Compare(cit.state, "") == 0)
                {
                    string temp = cit.name + "," + cit.country;
                    id.Add(cit.id);
                    countries.Add(temp);
                    isfound++;
                }
            }
            m_Options = countries.Distinct().ToList();
            SpawnClickableOptions(m_Options);

            List<string> optionsThatMatched = m_OptionObjectSpawnedDict.Keys.
               Where(optionKey => optionKey.ToLower().Contains(m_InputField.text.ToLower())).ToList();

            foreach (KeyValuePair<string, GameObject> keyValuePair in m_OptionObjectSpawnedDict)
            {
                if (optionsThatMatched.Contains(keyValuePair.Key))
                    keyValuePair.Value.SetActive(true);
                else
                    keyValuePair.Value.SetActive(false);
            }

            if (optionsThatMatched.Count == 0)
            {
                m_ScrollRect.gameObject.SetActive(false);        // Disable the scrollview if no options
                return;
            }

            isSelected = false;

            // If options is > than the amount of options we can display
            if (optionsThatMatched.Count > m_OptionsOnDisplay)
            {
                // Then scale the height of the rect transform to only show the max amount of items we can show at one time
                m_ScrollRectTransform.offsetMin = new Vector2(
                           m_ScrollRect.GetComponent<RectTransform>().offsetMin.x,
                             m_OriginalOffsetMinY - (m_ComponentsHeight * m_OptionsOnDisplay));

            }
            else
            {
                // Else... just increase the height of the rect transform to display all of options that matched
                m_ScrollRectTransform.offsetMin = new Vector2(
                          m_ScrollRect.GetComponent<RectTransform>().offsetMin.x,
                            m_OriginalOffsetMinY - (m_ComponentsHeight * optionsThatMatched.Count));
            }

            m_ScrollRect.gameObject.SetActive(true);            // If we get here, we can assume that we want to display the options.

        }
    }
    public void OnValueSelected()
    {
        isSelected = true;
    }
    public void TaskOnClick()
    {
        city.StateName = "";
        if (isSelected)
        {
            string temp1 = hold;

            int firstStringPosition = temp1.IndexOf(",");
            string temp = temp1.Substring(0, firstStringPosition);
            city.CityName = temp;

            temp1 = temp1.Substring(firstStringPosition + 1);
            if (temp1.Contains(","))
            {
                firstStringPosition = temp1.IndexOf(",");
                temp = temp1.Substring(0, firstStringPosition);
                city.CountryName = temp;

                Debug.Log(city.CountryName);
                temp1 = temp1.Substring(firstStringPosition + 1);
                city.StateName = temp1;
                Debug.Log(city.StateName);

            }
            else
            {
                city.CountryName = temp1;
            }

            foreach (CityJ cit in CityInJson.citys)
            {
                if (string.Compare(cit.name, city.CityName) == 0 && string.Compare(cit.country, city.CountryName) == 0 && string.Compare(cit.state, city.StateName) == 0)
                {
                    city.CityId = cit.id;
                    Debug.Log(city.CityId);
                }
            }

            city.CityName = hold;
            MainMenu.inpOption = 1;
            city.cityInputted = true;
            Destroy(GameObject.Find("Canvas"));
            SceneManager.LoadScene(sceneName: "MainMenu");
        }
        else
        {
            Debug.Log(city.CityId);

        }
    }
    void TaskOnDrop()
    {
        m_ScrollRect.gameObject.SetActive(true);
    }
}
