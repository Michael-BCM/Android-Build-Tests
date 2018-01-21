using UnityEngine;

public class Dice : Number
{
    public bool roll, thrown, justThrown;
    public float _timer;
    public int rollCount;

    private void Start()
    {
        _textMesh.text = "Press and hold to roll the die.\n\nRelease to throw the die.\n\nTap to start.";

        rollCount = 0;
        _timer = 0;
        roll = thrown = false;
        justLaunched = true;
    }

    private void Update()
    {
        Throw();
        _textMesh.fontSize = fontSizer(40, 100);

        if (roll)
            _textMesh.text = randomNumber().ToString();

        if(BCM_Apps.touchCheck() && !thrown)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (!justLaunched)
                {
                    justThrown = false;
                    roll = true;
                }
            }
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
                }
                else if (!justThrown)
                {
                    roll = false;
                    thrown = true;
                }
            }
        }
    }

    void Throw()
    {
        if (thrown)
        {
            _timer += Time.deltaTime;

            if (_timer >= 0.2)
            {
                _textMesh.text = randomNumber().ToString();
                rollCount += 1;
                if (rollCount > 5)
                {
                    rollCount = 0;
                    thrown = false;
                    justThrown = true;
                }
                _timer = 0;
            }
        }
    }

    int randomNumber()
    {
        return Random.Range(1, 7);
    }
}