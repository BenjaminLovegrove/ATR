using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MemoryFlash : MonoBehaviour
{
    public CanvasGroup myCG;
    private bool flash = false;

    void FixedUpdate()
    {
        if (flash)
        {
            myCG.alpha = myCG.alpha - Time.deltaTime;
            if (myCG.alpha <= 0)
            {
                myCG.alpha = 0;
                flash = false;
            }
        }
    }

    public void ActivateFlash()
    {
        flash = true;
        myCG.alpha = 1;
    }
}