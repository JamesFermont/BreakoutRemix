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


        //Create A Button for every Block in the Dictionary
        GameObject currentBlockGameObject;
        ((RectTransform)ContentTransform).sizeDelta = new Vector2((BlockDictionary.instance.dict.Count - 2) * 170, 0);


        currentBlockGameObject = (GameObject)Instantiate(ButtonPrefab, ContentTransform);
        currentBlockGameObject.name = "ERASE";
        currentBlockGameObject.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "ERASE";
        currentBlockGameObject.transform.localPosition = new Vector3(120, currentBlockGameObject.transform.position.y, currentBlockGameObject.transform.position.z);
        currentBlockGameObject.GetComponent<Button>().image.sprite = eraseSprite;
        myButtons.Add(currentBlockGameObject.GetComponent<Button>());
        currentBlockGameObject.GetComponent<Button>().onClick.AddListener(delegate { SelectButton("ERASE"); });

        for (int i = 0; i < BlockDictionary.instance.dict.Count; i++) {
            if (i < 3)
                continue;
            currentBlockGameObject = (GameObject)Instantiate(ButtonPrefab, ContentTransform);
            currentBlockGameObject.name = BlockDictionary.instance.dict[i].key;
            currentBlockGameObject.transform.localPosition = new Vector3(120 + 170 * (i - 2), currentBlockGameObject.transform.position.y, currentBlockGameObject.transform.position.z);
            currentBlockGameObject.GetComponent<Button>().image.sprite = BlockDictionary.instance.dict[i].myGameObject.GetComponent<SpriteRenderer>().sprite;
            myButtons.Add(currentBlockGameObject.GetComponent<Button>());
            string nameCopy = currentBlockGameObject.name;
            currentBlockGameObject.GetComponent<Button>().onClick.AddListener(delegate { SelectButton(nameCopy); });
        }
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
