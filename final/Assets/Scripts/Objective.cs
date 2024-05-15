using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objective:MonoBehaviour
{

    public Objetivos.ObjectivesEnum objective;
    public Image imageObjective;
    public Image objectiveCompleted;

    public Objective(Objetivos.ObjectivesEnum objective, Image imageObjective, Image objectiveCompleted)
    {
        this.objective = objective;
        this.imageObjective = imageObjective;
        this.objectiveCompleted = objectiveCompleted;
    }

}
