using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RequestManager : MonoBehaviour
{
    private Guest guest;
    public bool correct;
    private int score;
    public TextMeshProUGUI scoreText, guestNameText, guestPartyText, guestSpeciesText, guestProfessionText;

    private float trueRatio = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        UpdateScore(0);
        VisitGuest();
    }

    public void VisitGuest()
    {
        correct = new System.Random().NextDouble() < trueRatio ? true : false;
        guest = GuestDB.CreateGuest(correct);

        guestNameText.text = guest.GetName();
        guestPartyText.text = guest.GetParty();
        guestSpeciesText.text = GuestDB.GetSpeciesText(guest.GetSpecies());
        guestProfessionText.text = GuestDB.GetProfessiosText(guest.GetProfession());
    }

    public void UpdateScore(int num)
    {
        score += num;
        scoreText.text = score.ToString();
    }
}
