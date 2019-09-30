using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasController : MonoBehaviour
{
    public TextMeshProUGUI number;
    public TextMeshProUGUI total;
    public TextMeshProUGUI level;
    public GameObject puzzles;
    private PuzzleManager puzzleManager;

    // Start is called before the first frame update
    void Start()
    {
        puzzleManager = puzzles.GetComponent<PuzzleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        total.text = "Total: " + puzzleManager.currentTotal.ToString();
        number.text = "#1-" + puzzleManager.currentNumber.ToString();
        level.text = "4-" + (puzzleManager.puzzleOn + 1).ToString();
    }
}
