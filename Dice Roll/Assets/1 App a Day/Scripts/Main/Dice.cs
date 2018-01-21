using UnityEngine;
[ExecuteInEditMode]

public class Dice : Number
{
    public bool roll, thrown, justThrown;
    public float _timer;
    public int rollCount, numberOfSides;

    int randomNumber() { return Random.Range(1, numberOfSides + 1); }

    private void Start()
    {
        _textMesh.text = "Touch and hold to roll the die.\n\nRelease to throw the die.\n\n\nTouch anywhere to begin.";

        rollCount = 0;
        _timer = 0;
        timeLimit = 0.1F;
        roll = thrown = false;
        justLaunched = true;
        instructionFontSize = 40;
        defaultFontSize = 300;
        numberOfSides = 6;
    }

    private void Update()
    {
        _textMesh.fontSize = fontSizer(instructionFontSize, defaultFontSize); //Sets the font size. 

        if (roll)
            _textMesh.text = randomNumber().ToString(); //Continually sets the number's value, as long as the dice is rolling.

        if(BCM_Apps.touchCheck() && !thrown)
        {
            #region Touching the screen.
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (!justLaunched)
                {
                    justThrown = false;
                    roll = true;
                }
            }
            #endregion

            #region Releasing the screen. 
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (justLaunched)
                {
                    justLaunched = false;
                    _textMesh.text = "0";
                }
                else if (!justLaunched)
                {
                    if (justThrown)
                        justThrown = false;
                    else
                    {
                        roll = false;
                        thrown = true;
                    }
                }                
            }
            #endregion
        }

        #region Dice throw
        if (thrown)
        {
            _timer += Time.deltaTime;

            if (_timer >= timeLimit)
            {
                _textMesh.text = randomNumber().ToString();
                rollCount += 1;
                if (rollCount > 5)
                {
                    rollCount = 0;
                    timeLimit = 0F;
                    thrown = false;
                    justThrown = true;
                }
                timeLimit += 0.08F;
                _timer = 0;
            }
        }
        #endregion
    }    
}