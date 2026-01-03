using System;
using TMPro;
using UnityEngine;

public class Journal : MonoBehaviour
{
    [SerializeField]
    TMP_Text contentsTextBox, titleTextBox, pageNumberBox;
    [SerializeField]
    Page[] pages;
    public void loadPageByNumber(int number)

    {
        foreach (Page page in pages)
        {
            if(page.number == number)
            {
                LoadPage(page);
            }
        }
    }

    public void LoadPageByIndex(int index)
    {
        LoadPage(pages[index]);
    }

    void LoadPage(Page page)
    {
        contentsTextBox.text = page.contents;
        titleTextBox.text = page.title;
        pageNumberBox.text = page.number.ToString();
    }
}
[Serializable]
public class Page
{
    public int number;
    public String title;
    public String contents;
}
