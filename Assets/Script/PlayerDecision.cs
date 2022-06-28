using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDecision : MonoBehaviour
{
    [SerializeField]
    private RequestManager requestManager;
    public void SelectYes()
    {
        requestManager.DecisionComplete(true);
    }

    public void SelectNo()
    {
        requestManager.DecisionComplete(false);
    }
}
