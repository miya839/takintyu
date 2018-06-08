using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    public void FinishAnim()
    {
        GetComponent<Animation>().Play("AxeIdle");
    }
}
