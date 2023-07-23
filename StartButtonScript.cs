using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void ClickStartButton()
    {
        GameObject.Find("TurnSystem").GetComponent<TurnSystem>().StartBattlePhase();
    }
}
