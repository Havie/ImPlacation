using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public int _gold = 1;
    public int _victoryPoints;
    public TextMeshProUGUI _VictoryText;
    public TextMeshProUGUI _GoldText;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameManager();
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            //if not, set instance to this
            _instance = this;
        }
        //If instance already exists and it's not this:
        else if (_instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("TownCenter").GetComponent<bTownCenter>().StartingBuildComplete();
        _gold = 1;
        _victoryPoints = 0;
        UpdateVictoryPoint();
        UpdateGold();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void incrementGold(int amnt)
    {
        _gold += amnt;
        UpdateGold();
    }

    public void incrementVictoryPoints(int amnt)
    {
        _victoryPoints += amnt;
        if (_victoryPoints > 5)
            youWin();
    }
    public void UpdateVictoryPoint()
    {
        if(_VictoryText!=null)
        {
            _VictoryText.text = _victoryPoints.ToString();
        }
    }
    public void UpdateGold()
    {
        if(_GoldText)
        {
            _GoldText.text = _gold.ToString();
        }
    }
    public void youWin()
    {
        Debug.Log("You WOn!");
    }
}
