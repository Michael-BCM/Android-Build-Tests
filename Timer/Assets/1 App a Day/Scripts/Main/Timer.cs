using UnityEngine;

public class Timer : Number
{    
    public bool count;
    public float elapsedTime, secondCount;
    public int minuteCount, hourCount, dayCount;
    
    private void Start()
    {
        elapsedTime = secondCount = resetTimer = minuteCount = hourCount = 0;
        dotLimit = 1;
        count = resetCount = justReset = false;
        justLaunched = true;
        timeLimit = 1.5F;
        defaultFontSize = 300;
        instructionFontSize = 40;
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
            elapsedTime += Time.deltaTime;           

        if (resetCount)
            resetTimer += Time.deltaTime;
        else
            resetTimer = 0;            

        if(resetTimer > timeLimit)
        {
            resetCount = false;
            justReset = true;
            secondCount = minuteCount = hourCount = dayCount = 0;
        }

        _textMesh.text = display("Touch to start\nor pause the clock.\n\nTap and hold to reset.\n\n\nTouch anywhere to begin.", "...", timeDisplay());

        if(justLaunched)
            _textMesh.fontSize = fontSizer(instructionFontSize, defaultFontSize);
        else
            _textMesh.fontSize = fontSizeModifier / _textMesh.text.Length;
        
        if (secondCount >= 60)
            minuteCount += 1;

        //secondCount = setClock(secondCount, 60); //For debugging, replace the below line with this one. For accuracy, keep the line below intact.

        secondCount = elapsedTime - (minuteCount * 60) - (hourCount * 3600) - (dayCount * 3600 * 24);
        
        if (minuteCount >= 60)
            hourCount += 1;

        minuteCount = setClock(minuteCount, 60);

        if (hourCount >= 24)
            dayCount += 1;

        hourCount = setClock( hourCount, 24);

        dayCount = setClock(dayCount, int.MaxValue);        
    }

    string timeDisplay()
    {
        if(dayCount >= 1)
            return dayCount.ToString("0") + ":" + hourCount.ToString("00") + ":" + minuteCount.ToString("00") + ":" + secondCount.ToString("00.00");
        if(hourCount >= 1)
            return hourCount.ToString("00") + ":" + minuteCount.ToString("00") + ":" + secondCount.ToString("00.00");
        if(minuteCount >= 1)
            return minuteCount.ToString("00") + ":" + secondCount.ToString("00.00");

        return secondCount.ToString("00.00");
    }

    int setClock (int measurement, int limit)
    {
        if (measurement >= limit || measurement < 0)
            return 0;
        return measurement;
    }

    float setClock(float measurement, int limit)
    {
        if (measurement >= limit || measurement < 0)
            return 0;
        return measurement;
    }
}