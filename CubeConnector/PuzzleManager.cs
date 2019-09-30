using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    private BoxManager[] myscripts;
    private bool setComplete = false;
    public int puzzleOn;
    public int currentTotal;
    public int currentNumber;

    // Start is called before the first frame update
    void Start()
    {
        myscripts = gameObject.GetComponentsInChildren<BoxManager>(true);
        puzzleOn = 0;
        myscripts[puzzleOn].gameObject.SetActive(true);
        currentNumber = myscripts[puzzleOn].maxNumber;
        currentTotal = myscripts[puzzleOn].totalNumber;
    }

    // Update is called once per frame
    void Update()
    {
        currentNumber = myscripts[puzzleOn].maxNumber;
        if (myscripts[puzzleOn].puzzleDone)
        {
            myscripts[puzzleOn].gameObject.SetActive(false);

            if (puzzleOn >= myscripts.Length - 1)
            {
                setComplete = true;
                Debug.Log("Set Done");
            }

            if (puzzleOn < myscripts.Length - 1)
            {
                puzzleOn += 1;
            }

            myscripts[puzzleOn].gameObject.SetActive(true);
            currentNumber = myscripts[puzzleOn].maxNumber;
            currentTotal = myscripts[puzzleOn].totalNumber;
        }
    }
}
