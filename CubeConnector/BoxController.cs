using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BoxController : MonoBehaviour
{
    // Finished Variable
    public bool puzzleFinished = false;
    public bool boxFinished = false;

    // Box Sides
    public GameObject topSide;
    public GameObject topRightSide;
    public GameObject rightSide;
    public GameObject bottomRightSide;
    public GameObject bottomSide;
    public GameObject bottomLeftSide;
    public GameObject leftSide;
    public GameObject topLeftSide;
    private GameObject originalTopSide;
    private GameObject originalTopRightSide;
    private GameObject originalRightSide;
    private GameObject originalBottomRightSide;
    private GameObject originalBottomSide;
    private GameObject originalBottomLeftSide;
    private GameObject originalLeftSide;
    private GameObject originalTopLeftSide;

    // Lines Drawn
    public bool horizontalLine = true;
    public bool verticalLine = true;
    public bool forwardLine = true;
    public bool backwardLine = true;

    // Box Characteristics
    public int boxNumber;
    private int lastBoxNumber;
    public int valueChanger = 0;
    private Renderer rend;
    public bool clickedOn = false;
    public GameObject textObject;
    private TextMeshPro txt;

    // Total to Check
    private int lineTotal;
    private bool horizontalDone = false;
    private bool verticalDone = false;
    private bool forwardDone = false;
    private bool backwardDone = false;
    private BoxController number;
    private BoxManager parent;

    // Start is called before the first frame update
    void Start()
    {
        transform.gameObject.SetActive(true);
        rend = GetComponent<Renderer>();
        parent = gameObject.GetComponentInParent<BoxManager>();
        txt = textObject.GetComponent<TextMeshPro>();
        boxNumber = 400;

        setOriginals();
    }

    void FixedUpdate()
    {
        updateColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (boxNumber < 300)
        {
            txt.text = boxNumber.ToString();
        }
        else
        {
            txt.text = "";
        }

        checkFinished();
    }

    private void horizontalCheck()
    {
        while (rightSide != null)
        {
            number = rightSide.GetComponent<BoxController>();
            lineTotal += number.boxNumber + number.valueChanger;
            rightSide = number.rightSide;
        }

        while (leftSide != null)
        {
            number = leftSide.GetComponent<BoxController>();
            lineTotal += number.boxNumber + number.valueChanger;
            leftSide = number.leftSide;
        }

        if (lineTotal == parent.totalNumber)
        {
            horizontalDone = true;
        }

        lineTotal = boxNumber + valueChanger;

        leftSide = originalLeftSide;
        rightSide = originalRightSide;
    }

    private void verticalCheck()
    {
        while (topSide != null)
        {
            number = topSide.GetComponent<BoxController>();
            lineTotal += number.boxNumber + number.valueChanger;
            topSide = number.topSide;
        }

        while (bottomSide != null)
        {
            number = bottomSide.GetComponent<BoxController>();
            lineTotal += number.boxNumber + number.valueChanger;
            bottomSide = number.bottomSide;
        }

        if (lineTotal == parent.totalNumber)
        {
            verticalDone = true;
        }

        lineTotal = boxNumber + valueChanger;

        topSide = originalTopSide;
        bottomSide = originalBottomSide;
    }

    private void forwardDiagonalCheck()
    {
        while (topRightSide != null)
        {
            number = topRightSide.GetComponent<BoxController>();
            lineTotal += number.boxNumber + number.valueChanger;
            topRightSide = number.topRightSide;
        }

        while (bottomLeftSide != null)
        {
            number = bottomLeftSide.GetComponent<BoxController>();
            lineTotal += number.boxNumber + number.valueChanger;
            bottomLeftSide = number.bottomLeftSide;
        }

        if (lineTotal == parent.totalNumber)
        {
            forwardDone = true;
        }

        lineTotal = boxNumber + valueChanger;

        topRightSide = originalTopRightSide;
        bottomLeftSide = originalBottomLeftSide;
    }

    private void backwardDiagonalCheck()
    {
        while (bottomRightSide != null)
        {
            number = bottomRightSide.GetComponent<BoxController>();
            lineTotal += number.boxNumber + number.valueChanger;
            bottomRightSide = number.bottomRightSide;
        }

        while (topLeftSide != null)
        {
            number = topLeftSide.GetComponent<BoxController>();
            lineTotal += number.boxNumber + number.valueChanger;
            topLeftSide = number.topLeftSide;
        }

        if (lineTotal == parent.totalNumber)
        {
            backwardDone = true;
        }

        lineTotal = boxNumber + valueChanger;

        topLeftSide = originalTopLeftSide;
        bottomRightSide = originalBottomRightSide;
    }

    private void updateColor()
    {
        if (clickedOn)
        {
            rend.material.color = Color.white;
        }

        else
        {
            if (valueChanger != 0)
            {
                if (valueChanger < 0)
                {
                    rend.material.color = Color.red;
                }
                else
                {
                    rend.material.color = Color.green;
                }
            }
            else
            {
                rend.material.color = Color.cyan;
            }
        }
    }

    private void setOriginals()
    {
        if (leftSide == null & rightSide == null)
        {
            horizontalDone = true;
        }
        else
        {
            originalRightSide = rightSide;
            originalLeftSide = leftSide;
        }

        if (topSide == null & bottomSide == null)
        {
            verticalDone = true;
        }
        else
        {
            originalTopSide = topSide;
            originalBottomSide = bottomSide;

        }

        if (topRightSide == null & bottomLeftSide == null)
        {
            forwardDone = true;
        }
        else
        {
            originalBottomLeftSide = bottomLeftSide;
            originalTopRightSide = topRightSide;
        }

        if (bottomRightSide == null & topLeftSide == null)
        {
            backwardDone = true;
        }
        else
        {
            originalBottomRightSide = bottomRightSide;
            originalTopLeftSide = topLeftSide;
        }
    }

    private void checkFinished()
    {
        // Math checks
        if (!horizontalDone)
        {
            horizontalCheck();
        }

        if (!verticalDone)
        {
            verticalCheck();
        }

        if (!forwardDone)
        {
            forwardDiagonalCheck();
        }

        if (!backwardDone)
        {
            backwardDiagonalCheck();
        }

        if (horizontalDone & forwardDone & backwardDone & verticalDone)
        {
            boxFinished = true;
        }

        // Finished puzzle animation
        if (puzzleFinished & transform.gameObject.activeSelf)
        {
            txt.text = "";
            rend.material.color = Color.yellow;
            transform.eulerAngles += new Vector3(0.0f, 3.0f, 3.0f);
            transform.localScale -= new Vector3(0.03f, 0.03f, 0.03f);

            // Disappear boxes
            if (transform.localScale.x < 0.1 & transform.localScale.y < 0.1 & transform.localScale.z < 0.1)
            {
                transform.gameObject.SetActive(false);
                parent.puzzleDone = true;
            }
        }
    }
}

