using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform startPosition;
    [SerializeField] Transform endPosition;
    [SerializeField] Text remainDistanceText;
    [SerializeField] List<Text> nameHorse;
    [SerializeField] List<Horse> horses;
    public Camera mainCamera;
    int remainDistance;
    private static GameManager _instance;
    private float totalRace;
    public static GameManager Instance => _instance;
    [SerializeField] AudioSource audio;
    public int RemainDistance { get => remainDistance; private set => remainDistance = value; }

    private Horse firstHorse;

    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        totalRace = endPosition.position.z - startPosition.position.z;
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        sortingRanking();
        showRemainDistance();
        slowDownCamera();
    }


    private void sortingRanking()
    {
        var listHorse = horses.OrderByDescending(o => o.transform.position.z).ToList();
        Vector3 currentPositionCamera = mainCamera.transform.position;
        firstHorse = listHorse[0];
        mainCamera.transform.position = new Vector3(currentPositionCamera.x, currentPositionCamera.y, firstHorse.transform.position.z);
        displayRanking(listHorse);
    }

    private void showRemainDistance()
    {
        remainDistance = (int)Math.Round(totalRace - firstHorse.transform.position.z);

        if (remainDistance >= 0f)
        {
            remainDistanceText.text = firstHorse.Name + ":" + remainDistance + Common.Meter;
            audio.Stop();
        }
        else
        {
            remainDistanceText.text = Common.Zero + "" + Common.Meter;
        }

    }

    private void slowDownCamera()
    {
        if (remainDistance <= 100)
        {
            Time.timeScale = 0.5f;
        }
    }

    public void displayRanking(List<Horse> horses)
    {
        for (int i = 0; i < horses.Count; i++)
        {
            nameHorse[i].text = horses[i].Name;
        }
    }
}
