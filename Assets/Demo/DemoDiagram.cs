using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Book;

public class DemoDiagram : Diagram<DemoPage>
{
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            NextTo("Page1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            NextTo("Page2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            NextTo("Page3");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            NextTo("Page4");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            PreviousTo();
        }
    }
}
