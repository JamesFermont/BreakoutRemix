using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockSelection : MonoBehaviour {
    public string currentBlock;
    public Object ButtonPrefab;
    public Transform ContentTransform;
    public List<Button> myButtons = new List<Button>();
    public Sprite eraseSprite;
    private EditorNavigation editorNavigation;


    public void Start() {
        editorNavigation = transform.parent.GetComponent<EditorNavigation>();

        ((RectTransform)ContentTransform).sizeDelta = new Vector2((BlockDictionary.instance.dict.Count - 2) * 170, 0);

        CreateEraseButton();


        for (int i = 0; i < BlockDictionary.instance.dict.Count; i++) {
            if (i < 3)
                continue;
            CreateBlockButton(BlockDictionary.instance.dict[i].key, i);
        }
    }
    private void CreateEraseButton() {
        GameObject currentBlockGameObject = (GameObject)Instantiate(ButtonPrefab, ContentTransform);
        currentBlockGameObject.name = "ERASE";
        currentBlockGameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "ERASE";
        currentBlockGameObject.transform.localPosition = new Vector3(120, currentBlockGameObject.transform.position.y, currentBlockGameObject.transform.position.z);
        currentBlockGameObject.GetComponent<Button>().image.sprite = eraseSprite;
        myButtons.Add(currentBlockGameObject.GetComponent<Button>());
        currentBlockGameObject.GetComponent<Button>().onClick.AddListener(delegate { SelectButton("ERASE"); });
    }
    private void CreateBlockButton(string buttonName, int yIndex) {
        GameObject currentBlockGameObject = (GameObject)Instantiate(ButtonPrefab, ContentTransform);
        currentBlockGameObject.name = BlockDictionary.instance.dict[yIndex].key;
        currentBlockGameObject.transform.localPosition = new Vector3(120 + 170 * (yIndex - 2), currentBlockGameObject.transform.position.y, currentBlockGameObject.transform.position.z);
        currentBlockGameObject.GetComponent<Button>().image.sprite = BlockDictionary.instance.dict[yIndex].myGameObject.GetComponent<SpriteRenderer>().sprite;
        myButtons.Add(currentBlockGameObject.GetComponent<Button>());
        currentBlockGameObject.GetComponent<Button>().onClick.AddListener(delegate { SelectButton(buttonName); });
    }

    private void SelectButton(string buttonName) {
        foreach (Button b in myButtons) {
            if (b.gameObject.name == buttonName) {
                currentBlock = buttonName;
                b.interactable = false;
                editorNavigation.previewCursor.GetComponent<SpriteRenderer>().sprite = b.image.sprite;

            } else {
                b.interactable = true;
            }
        }
    }

}
