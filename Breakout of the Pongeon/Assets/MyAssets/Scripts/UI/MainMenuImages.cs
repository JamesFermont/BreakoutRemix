using UnityEngine;
using UnityEngine.UI;

public class MainMenuImages : MonoBehaviour
{
    public Sprite[] menus;
    public Sprite[] views;
    public Sprite[] selections;

    private Image menu;
    private Image view;
    private Image selection;

    private void Start() {
        menu = transform.Find("Menu").GetComponent<Image>();
        view = transform.Find("View").GetComponent<Image>();
        selection = transform.Find("Selection").GetComponent<Image>();
    }

    public void SetViewAndSelection(int index) {
        SetView(index);
        SetSelection(index);
        
    }
    public void SetView(int index) {
        view.sprite = views[index];
    }
    private void SetSelection (int index) {
        selection.sprite = selections[index];
    } 


    public void SetMenu(int index) {
        menu.sprite = menus[index];
    }
}
