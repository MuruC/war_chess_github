﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openRecruitMenu() {
        UIManager.Instance.recruitMenu.SetActive(true);
    }

    public void exitRecruitMenu() {
        UIManager.Instance.recruitMenu.SetActive(false);
    }
}
