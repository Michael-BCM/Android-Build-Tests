using UnityEngine;
[ExecuteInEditMode]
public class Counter : Number
{
    public int num, defaultFontSize = 300, fontSizeModifier = 850;

    private void Start()
    {
        num = -1;
        dotLimit = 1;
        resetCount = justReset = false;
        justLaunched = true;
        timeLimit = 1.5F;
    }

    private void Update()
    {
        if (BCM_Apps.touchCheck()) //All GetTouch 'if' statements must be encased inside this block. 
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                justReset = justLaunched = false;
                resetCount = true;
                num += 1;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Ended)
                justReset = resetCount = false;
        }

        if (resetCount)
            resetTimer += Time.deltaTime;
        else if (!resetCount)
            resetTimer = 0;

        if (resetTimer > timeLimit)
        {
            resetTimer = num = 0;
            justReset = true;
        }

        _textMesh.text = display("Touch the screen\nto count up by 1.\n\nTouch and hold to reset.\n\n\nTap anywhere to begin.", "...", num.ToString());

        if(num.ToString().Length < 4)
            _textMesh.fontSize = fontSizer(40, defaultFontSize);
        else 
            _textMesh.fontSize = fontSizeModifier/num.ToString().Length;        
    }
}