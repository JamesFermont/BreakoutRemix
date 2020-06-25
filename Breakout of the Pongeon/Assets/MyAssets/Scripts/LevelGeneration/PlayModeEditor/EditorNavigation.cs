using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorNavigation : MonoBehaviour {

    public EditorMode currentMode = EditorMode.PLAY;
    public GameObject previewCursor;
    private GameObject volatiles;
    private BlockSelection blockSelection;

    public Button playButton;

    public Button createButton;
    public Button saveButton;
    public Button loadButton;

    public GameObject createDialog;
    public GameObject saveDialog;
    public GameObject loadDialog;

    private Camera camera;
    private Vector3 mousePosition;

    void Start() {
        blockSelection = transform.GetComponentInChildren<BlockSelection>();
        volatiles = GameObject.Find("Volatiles");
        camera = Camera.main;

        createButton.onClick.AddListener(delegate { createDialog.SetActive(true); });
        saveButton.onClick.AddListener(delegate { saveDialog.SetActive(true); });
        loadButton.onClick.AddListener(delegate { loadDialog.SetActive(true); });

        playButton.onClick.AddListener(delegate { switchModes(); });

        SwitchToEdit();
    }

    // Update is called once per frame
    void Update() {
        mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        SetPreviewCursorPosition();
        if (currentMode == EditorMode.EDIT) {
            if (!loadDialog.activeSelf && !saveDialog.activeSelf && !createDialog.activeSelf) {
                if (Input.GetMouseButtonDown(0)) {
                    if (blockSelection.currentBlock != "ERASE") {
                        if (GridEditor.TryPlaceObjectInGrid(blockSelection.currentBlock, Grid.toGridPosition(mousePosition), true))
                            LevelManager.ResetCurrentLevel();
                    } else if (blockSelection.currentBlock == "ERASE") {
                        if (GridEditor.TryDeleteObjectAtPosition(Grid.toGridPosition(mousePosition)))
                            LevelManager.ResetCurrentLevel();
                    }

                }
            }

        }
        if (Grid.toGridPosition(mousePosition).y < Constants.PROTECTED_ROWS)
            HidePreviewCursor(true);
        else if (!createDialog.activeSelf && !saveDialog.activeSelf && !loadDialog.activeSelf && currentMode == EditorMode.EDIT)
            HidePreviewCursor(false);

    }

    private void SwitchToEdit() {
        if (currentMode == EditorMode.EDIT)
            return;
        currentMode = EditorMode.EDIT;
        playButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Play";
        //Deactivate Volatiles
        if (volatiles != null) {
            SetBallAndPaddle(true);
        }
        SetEditorUI(false);

        LevelStatistics.instance.ResetTracker();

        if (LevelManager.currentLevelGO != null)
            LevelManager.ResetCurrentLevel();
    }

    private void SwitchToPlay() {
        if (currentMode == EditorMode.PLAY)
            return;
        currentMode = EditorMode.PLAY;
        playButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Stop";

        if (volatiles != null) {
            SetBallAndPaddle(false);
        }
        SetEditorUI(true);
        volatiles.transform.GetChild(2).GetComponent<TargetManager>().FindTargetAreas();
    }

    private void SetEditorUI(bool hidden) {
        transform.GetChild(0).gameObject.SetActive(!hidden);
        transform.GetChild(1).gameObject.SetActive(!hidden);
        previewCursor.SetActive(!hidden);
    }

    private void SetBallAndPaddle(bool hidden) {
        volatiles.transform.GetChild(0).GetComponent<MouseMovement>().isHidden = hidden;
        volatiles.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = !hidden;
        volatiles.transform.GetChild(1).GetComponent<BallStart>().isBallStart = true;
        volatiles.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        volatiles.transform.GetChild(1).GetComponent<BallStart>().isHidden = hidden;
        volatiles.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = !hidden;

    }

    private void SetPreviewCursorPosition() {
        if (LevelManager.currentLevel == null)
            LevelManager.CreateNewLevel();
        previewCursor.transform.position = LevelManager.currentLevel.grid.toWorldPosition(Grid.toGridPosition(mousePosition));
    }

    private void switchModes() {
        if (currentMode == EditorMode.EDIT)
            SwitchToPlay();
        else
            SwitchToEdit();
    }

    public void HidePreviewCursor(bool hidden) {
        previewCursor.SetActive(!hidden);
    }

}
