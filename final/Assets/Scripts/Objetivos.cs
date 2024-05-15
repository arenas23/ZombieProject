using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objetivos : MonoBehaviour
{
    // public static Objetivos Instance { get; private set; }

    Image[] images;
    public List<Objective> objectives = new();
    public List<Objective> activeObjectives = new();
    public int currentObjective = 0;
    private Vector3 positionObjective1 = new(0, -280, 0);
    private Vector3 positionObjective2 = new(0, -370, 0);

    public enum ObjectivesEnum
    {
        GO_CONTROL_ROOM = 0,
        SEARCH_KEYCARD = 1,
        TURN_ON_GENERATORS = 2,
        ESCAPE = 3
    }



    // private void Awake()
    // {
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }

    // }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.objetivos = this;
        images = GetComponentsInChildren<Image>();

        foreach (ObjectivesEnum obj in Enum.GetValues(typeof(ObjectivesEnum)))
        {
            int imageIndex = (int)obj * 2;
            Objective newObjective = new Objective(obj, images[imageIndex], images[imageIndex + 1]);
            objectives.Add(newObjective);
            images[imageIndex].gameObject.SetActive(false); 
            images[imageIndex + 1].gameObject.SetActive(false);

        }

        StartCoroutine(ShowFirstObjective());
    }

    public IEnumerator ShowFirstObjective()
    {
        yield return new WaitForSeconds(27);
        // yield return new WaitForSeconds(2);
        objectives[0].imageObjective.gameObject.SetActive(true);
        objectives[0].imageObjective.rectTransform.anchoredPosition = positionObjective1;
        activeObjectives.Add(objectives[0]);
    }

    public void ActivateObjective(ObjectivesEnum objective)
    {

        currentObjective = (int)objective;
        activeObjectives.Add(objectives[currentObjective]);

        activeObjectives[^1].imageObjective.gameObject.SetActive(true);
        Debug.Log(activeObjectives.Count);
        activeObjectives[^1].imageObjective.rectTransform.anchoredPosition = objective == ObjectivesEnum.SEARCH_KEYCARD ? positionObjective2  : positionObjective1;


    }

    public void CompleteObjective(ObjectivesEnum objective)
    {
        objectives[(int)objective].imageObjective.gameObject.SetActive(false);
        StartCoroutine(ShowCompleted(objective));
    }

    IEnumerator ShowCompleted(ObjectivesEnum objective)
    {
        objectives[(int)objective].objectiveCompleted.gameObject.SetActive(true);
        objectives[(int)objective].imageObjective.rectTransform.anchoredPosition = objective == ObjectivesEnum.SEARCH_KEYCARD ? positionObjective2 : positionObjective1;
        activeObjectives.Remove(objectives[(int)objective]);
        yield return new WaitForSeconds(3);
        objectives[(int)objective].objectiveCompleted.gameObject.SetActive(false);
    }

}





