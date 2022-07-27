using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordInfo : MonoBehaviour
{
    //W - wrong, C - correct

    [SerializeField]
    private Image background;
    [SerializeField]
    private TMP_Text CCText;
    [SerializeField]
    private TMP_Text WCText;
    [SerializeField]
    private TMP_Text WWText;

    [SerializeField]
    private Color Wcolor;
    [SerializeField]
    private Color Ccolor;

    public void W(string w, string c)
    {
        background.color = Wcolor;

        CCText.gameObject.SetActive(false);
        WCText.gameObject.SetActive(true);
        WWText.gameObject.SetActive(true);

        WCText.text = c;
        WWText.text = w;
    }

    public void C(string c)
    {
        background.color = Ccolor;

        CCText.gameObject.SetActive(true);
        WCText.gameObject.SetActive(false);
        WWText.gameObject.SetActive(false);

        CCText.text = c;
    }
}
