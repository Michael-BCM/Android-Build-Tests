using UnityEngine;

public class Timer : Number
{
    public float _timer;
    public bool count;
    
    private void Start()
    {
        _timer = resetTimer = 0;
        dotLimit = 1;
        count = resetCount = justReset = false;
        justLaunched = true;
        timeLimit = 1.5F;
    }

    private void Update()
    {
        if(BCM_Apps.touchCheck())
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began && !count && !justLaunched)
                resetCount = true;

            if(Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (justLaunched)
                    justLaunched = false;
                else if (!justLaunched)
                {
                    if (justReset)
                        justReset = false;
                    else if (!justReset)
                    {
                        resetCount = false;
                        count = !count;
                    }
                }                               
            }
        }
        if (count)
            _timer += Time.deltaTime;

        if (resetCount)
            resetTimer += Time.deltaTime;
        else
            resetTimer = 0;            

        if(resetTimer > timeLimit)
        {
            resetCount = false;
            justReset = true;
            _timer = 0;
        }

        _textMesh.text = display("Tap to start/pause/resume.\n\nHold to reset.\n\n\nTap to begin.", "...", _timer.ToString("F" + 1));
        _textMesh.fontSize = fontSizer(30, 100);
    }
}