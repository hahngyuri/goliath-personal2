//using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

public class MapIntroduce : MonoBehaviour
{

    public GameObject goToMain//create button and connect the button and GotoMainScene()
    private int currentStage;
    private int openedStage;
    public int clearedStage;
    public GameObject IntroMap;
    public GameObject GoliathName0;
    public GameObject GoliathName1;
    public GameObject GoliathName2;
    public GameObject Story0;
    public GameObject Story1;
    public GameObject Story2;
    public GameObject stage0;
    public GameObject stage1;
    public GameObject stage2;
    public GameObject Btn_GoToBattle;
    public GameObject GoliathStory;
    public GameObject loading;
    private void Start()
    {
        IntroMap.SetActive(false);
        GoliathStory.SetActive(false);
        Btn_GoToBattle.SetActive(false);
        GoliathName0.SetActive(false);
        GoliathName1.SetActive(false);
        GoliathName2.SetActive(false);
        Story0.SetActive(false);
        Story1.SetActive(false);
        Story2.SetActive(false);

        Btn_GoToBattle.GetComponent<Button>().onClick.AddListener(OnGoToBattleButtonClicked);
        stage0.GetComponent<Button>().onClick.AddListener(() => OnStageButtonClicked(0));
        stage1.GetComponent<Button>().onClick.AddListener(() => OnStageButtonClicked(1));
        stage2.GetComponent<Button>().onClick.AddListener(() => OnStageButtonClicked(2));
    }
    void LoadBattle()
    {
        SceneManager.LoadScene("BattleScene");
    }
    public void GotoBattleScene()
    {
        loading.SetActive(true);
        Invoke("LoadBattle", 1.0f);
    }
    void GotoMainScene()
    {
        SceneManager.LoadScene("Main");
    }
    //Create button and connect ResetStageData()
    public void ResetStageData()
    {
        if (PlayerPrefs.HasKey("OpenedStage"))
        {
            PlayerPrefs.DeleteKey("OpenedStage");
        }
    }


    private void OnGoToBattleButtonClicked()
    {
        //check Saved data_ if story has been opened and the stage has been cleared 
        if (PlayerPrefs.HasKey("OpenedStage"))
        {
            openedStage = PlayerPrefs.GetInt("OpenedStage");
        }
        else
        {
            openedStage = -1;
        }
        if (PlayerPrefs.HasKey("ClearedStage"))
        {
            clearedStage = PlayerPrefs.GetInt("ClearedStage");
        }
        else
        {
            clearedStage = -1;
        }


        //Move to proper Scene
        if (currentStage == 0)
        {
            if (openedStage == -1)
            {
                Debug.Log("Move to Story Scene of 0");
                openedStage = 0;
            }
            else if (openedStage >= 0)
            {
                GotoBattleScene();
            }
        }
        else if (currentStage == 1)
        {
            if (clearedStage == 0)
            {
                Debug.Log("Move to Story Scene of 1");
                openedStage = 1;
            }
            else if (openedStage >= 1)
            {
                Debug.Log("Move to Battle Scene of 1");
                GotoBattleScene();
            }
        }
        else if (currentStage == 2)
        {
            if (clearedStage == 1)
            {
                Debug.Log("Move to Story Scene of 2");
                openedStage = 2;
            }
            else if (openedStage >= 2)
            {
                Debug.Log("Move to Battle Scene of 2");
                GotoBattleScene();
            }
        }


        PlayerPrefs.SetInt("OpenedStage", openedStage);
        PlayerPrefs.Save();
        Debug.Log("Saved : max opened stage is " + openedStage);
        Debug.Log("Saved : max cleared stage is " + clearedStage);
    }

    private void OnStageButtonClicked(int stage)
    {
        Btn_GoToBattle.SetActive(true);
        IntroMap.SetActive(true);
        GoliathStory.SetActive(true);
        if (stage == 0)
        {
            GoliathName0.SetActive(true);
            Story0.SetActive(true);
            GoliathName1.SetActive(false);
            Story1.SetActive(false);
            GoliathName2.SetActive(false);
            Story2.SetActive(false);
        }
        else if (stage == 1)
        {
            GoliathName0.SetActive(false);
            Story0.SetActive(false);
            GoliathName1.SetActive(true);
            Story1.SetActive(true);
            GoliathName2.SetActive(false);
            Story2.SetActive(false);
        }
        else if (stage == 2)
        {
            GoliathName0.SetActive(false);
            Story0.SetActive(false);
            GoliathName1.SetActive(false);
            Story1.SetActive(false);
            GoliathName2.SetActive(true);
            Story2.SetActive(true);
        }
        currentStage = stage;
        TurnSystem.stage = stage;
    }
}
