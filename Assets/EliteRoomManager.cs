using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class EliteRoomManager : MonoBehaviour
{
    [Header("Player")]
    public PlayerController player;
    public BoolVariable playerCanControl;

    [Header("Elite")]
    public EliteController elite;

    [Header("Elite Kill Button")]
    public GameObject eliteKillButtonPrefab;
    public Transform eliteKillButtonSpawn;
    public float secondsBeforeKillButtonSpawn = 10.0f;
    public BoolVariable hasTriggerEliteKillButton;
    private GameObject eliteKillButton;
    private PlayableDirector playableDirector;

    // Start is called before the first frame update
    void Start()
    {
        playableDirector = GetComponent<PlayableDirector>();
        StartCoroutine(SpawnEliteKillButton());
    }

    // Update is called once per frame
    void Update()
    {
        if (hasTriggerEliteKillButton.RuntimeValue)
        {
            elite.gameObject.SetActive(false);
        }
    }

    IEnumerator SpawnEliteKillButton()
    {
        yield return new WaitForSeconds(secondsBeforeKillButtonSpawn);

        Debug.Log("Button spawn");

        eliteKillButton = Instantiate(eliteKillButtonPrefab, eliteKillButtonSpawn);
    }
}
