using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour
{
    private int turn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        turn = GameManager.Instance.getTurn();
        showInformation();
    }

    void showInformation() {
        if (turn == 0)
        {
            UIManager.Instance.alignImage.sprite = UIManager.Instance.AlignImages[0];
        }
        else {
            UIManager.Instance.alignImage.sprite = UIManager.Instance.AlignImages[1];
        }
    }
}
