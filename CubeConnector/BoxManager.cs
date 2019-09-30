using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    public int totalNumber;
    public bool puzzleDone = false;
    public bool puzzleActive = false;
    private BoxController[] myscripts;
    private BoxController clicked;
    private GameObject[] linesDrawn;
    private int lineOn = 0;
    private bool wasClicked = false;
    public int maxNumber;
    private LineRenderer lRend;
    private Vector3 startPoint;
    private Vector3 endPoint;
    private BoxController number;
    private int boxesFinished = 0;

    // Start is called before the first frame update
    void Start()
    {
        myscripts = gameObject.GetComponentsInChildren<BoxController>();
        maxNumber = myscripts.Length;

        linesDrawn = new GameObject[40];

        drawLines();
    }

    void FixedUpdate()
    {
        getSelected();
    }

    // Update is called once per frame
    void Update()
    {
        getNumber();
        checkFinished();
    }

    private void checkFinished()
    {
        foreach (BoxController myscript in myscripts)
        {
            if (!myscript.boxFinished)
            {
                puzzleDone = false;
            }
            else
            {
                boxesFinished += 1;
            }
        }

        if (boxesFinished == myscripts.Length)
        {
            foreach (BoxController myscript in myscripts)
            {
                myscript.puzzleFinished = true;
            }
            foreach (GameObject line in linesDrawn)
            {
                Destroy(line);
            }
        }
        boxesFinished = 0;
    }

    private void getNumber()
    {
        if (wasClicked)
        {
            for (int i = 1; i <= maxNumber; i++)
            {
                if (Input.GetKeyDown(i.ToString()))
                {
                    foreach (BoxController myscript in myscripts)
                    {
                        if (myscript.boxNumber == i)
                        {
                            myscript.boxNumber = 400;
                        }
                    }
                    clicked.boxNumber = i;
                }
            }
        }
    }

    private void getSelected()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

            if (hit)
            {
                if (hitInfo.transform.gameObject.GetComponent<BoxController>() == null)
                {
                    if (clicked != null)
                    {
                        clicked.clickedOn = false;
                        wasClicked = false;
                    }
                }

                else
                {
                    if (clicked != null)
                    {
                        clicked.clickedOn = false;
                    }
                    clicked = hitInfo.transform.gameObject.GetComponent<BoxController>();
                    clicked.clickedOn = true;
                    wasClicked = true;
                }
            }
            else
            {
                if (clicked != null)
                {
                    clicked.clickedOn = false;
                    wasClicked = false;
                }
            }
        }

        if (clicked != null)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (clicked.topSide != null)
                {
                    clicked.clickedOn = false;
                    clicked = clicked.topSide.GetComponent<BoxController>();
                    clicked.clickedOn = true;
                }
                else
                {
                    if (clicked.topLeftSide != null & clicked.topRightSide == null)
                    {
                        clicked.clickedOn = false;
                        clicked = clicked.topLeftSide.GetComponent<BoxController>();
                        clicked.clickedOn = true;
                    }
                    if (clicked.topLeftSide == null & clicked.topRightSide != null)
                    {
                        clicked.clickedOn = false;
                        clicked = clicked.topRightSide.GetComponent<BoxController>();
                        clicked.clickedOn = true;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (clicked.bottomSide != null)
                {
                    clicked.clickedOn = false;
                    clicked = clicked.bottomSide.GetComponent<BoxController>();
                    clicked.clickedOn = true;
                }
                else
                {
                    if (clicked.bottomLeftSide != null & clicked.bottomRightSide == null)
                    {
                        clicked.clickedOn = false;
                        clicked = clicked.bottomLeftSide.GetComponent<BoxController>();
                        clicked.clickedOn = true;
                    }
                    if (clicked.bottomLeftSide == null & clicked.bottomRightSide != null)
                    {
                        clicked.clickedOn = false;
                        clicked = clicked.bottomRightSide.GetComponent<BoxController>();
                        clicked.clickedOn = true;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (clicked.leftSide != null)
                {
                    clicked.clickedOn = false;
                    clicked = clicked.leftSide.GetComponent<BoxController>();
                    clicked.clickedOn = true;
                }
                else
                {
                    if (clicked.topLeftSide != null & clicked.bottomLeftSide == null)
                    {
                        clicked.clickedOn = false;
                        clicked = clicked.topLeftSide.GetComponent<BoxController>();
                        clicked.clickedOn = true;
                    }
                    if (clicked.topLeftSide == null & clicked.bottomLeftSide != null)
                    {
                        clicked.clickedOn = false;
                        clicked = clicked.bottomLeftSide.GetComponent<BoxController>();
                        clicked.clickedOn = true;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (clicked.rightSide != null)
                {
                    clicked.clickedOn = false;
                    clicked = clicked.rightSide.GetComponent<BoxController>();
                    clicked.clickedOn = true;
                }
                else
                {
                    if (clicked.topRightSide != null & clicked.bottomRightSide == null)
                    {
                        clicked.clickedOn = false;
                        clicked = clicked.topRightSide.GetComponent<BoxController>();
                        clicked.clickedOn = true;
                    }
                    if (clicked.topRightSide == null & clicked.bottomRightSide != null)
                    {
                        clicked.clickedOn = false;
                        clicked = clicked.bottomRightSide.GetComponent<BoxController>();
                        clicked.clickedOn = true;
                    }
                }
            }
        }
    }

    private void drawLines()
    {
        foreach (BoxController myscript in myscripts)
        {
            if (myscript.leftSide == null & myscript.rightSide == null)
            {
                myscript.horizontalLine = false;
            }

            if (myscript.topSide == null & myscript.bottomSide == null)
            {
                myscript.verticalLine = false;
            }

            if (myscript.topRightSide == null & myscript.bottomLeftSide == null)
            {
                myscript.forwardLine = false;
            }

            if (myscript.bottomRightSide == null & myscript.topLeftSide == null)
            {
                myscript.backwardLine = false;
            }

            if (myscript.horizontalLine)
            {
                number = myscript;
                while (number.rightSide != null)
                {
                    number = number.rightSide.GetComponent<BoxController>();
                    number.horizontalLine = false;
                }
                startPoint = number.transform.position;

                number = myscript;
                while (number.leftSide != null)
                {
                    number = number.leftSide.GetComponent<BoxController>();
                    number.horizontalLine = false;
                }
                endPoint = number.transform.position;

                createCylinder((startPoint + endPoint)/2, new Vector3(0, 0, 90), new Vector3(1, (startPoint.x - endPoint.x), 1));

                myscript.horizontalLine = false;
            }

            if (myscript.verticalLine)
            {
                number = myscript;
                while (number.topSide != null)
                {
                    number = number.topSide.GetComponent<BoxController>();
                    number.verticalLine = false;
                }
                startPoint = number.transform.position;

                number = myscript;
                while (number.bottomSide != null)
                {
                    number = number.bottomSide.GetComponent<BoxController>();
                    number.verticalLine = false;
                }
                endPoint = number.transform.position;

                createCylinder((startPoint + endPoint) / 2, new Vector3(0, 0, 0), new Vector3(1, (startPoint.y - endPoint.y), 1));

                myscript.verticalLine = false;

            }

            if (myscript.forwardLine)
            {
                number = myscript;
                while (number.topRightSide != null)
                {
                    number = number.topRightSide.GetComponent<BoxController>();
                    number.forwardLine = false;
                }
                startPoint = number.transform.position;

                number = myscript;
                while (number.bottomLeftSide != null)
                {
                    number = number.bottomLeftSide.GetComponent<BoxController>();
                    number.forwardLine = false;
                }
                endPoint = number.transform.position;

                createCylinder((startPoint + endPoint) / 2, new Vector3(0, 0, 45), new Vector3(1, Mathf.Sqrt((startPoint.x - endPoint.x) * (startPoint.x - endPoint.x) + (startPoint.y - endPoint.y) * (startPoint.y - endPoint.y)), 1));

                myscript.forwardLine = false;
            }

            if (myscript.backwardLine)
            {
                number = myscript;
                while (number.bottomRightSide != null)
                {
                    number = number.bottomRightSide.GetComponent<BoxController>();
                    number.backwardLine = false;
                }
                startPoint = number.transform.position;

                number = myscript;
                while (number.topLeftSide != null)
                {
                    number = number.topLeftSide.GetComponent<BoxController>();
                    number.backwardLine = false;
                }
                endPoint = number.transform.position;

                createCylinder((startPoint + endPoint) / 2, new Vector3(0, 0, -45), new Vector3(1, Mathf.Sqrt((startPoint.x - endPoint.x) * (startPoint.x - endPoint.x) + (startPoint.y - endPoint.y) * (startPoint.y - endPoint.y)), 1));

                myscript.backwardLine = false;
            }
        }
    }

    private void createCylinder(Vector3 position, Vector3 angle, Vector3 scale)
    {
        linesDrawn[lineOn] = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        linesDrawn[lineOn].transform.position = position;
        linesDrawn[lineOn].transform.eulerAngles = angle;
        linesDrawn[lineOn].transform.localScale = scale / 2;

        lineOn += 1;
    }
}
