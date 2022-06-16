using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDecision : MonoBehaviour
{
    [SerializeField]
    private RequestManager requestManager;
    public void SelectYes()
    {
        if (requestManager.correct)
            requestManager.UpdateScore(1);
        else
            requestManager.UpdateScore(-1);

        requestManager.VisitGuest();

    }

    public void SelectNo()
    {
        requestManager.VisitGuest();
    }
}
