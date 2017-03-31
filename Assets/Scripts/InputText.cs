using UnityEngine;
using System.Collections;

public class InputText : MonoBehaviour {

	public void TextChangedName(string newText)
    {
        DataControl.control.playerName = newText;
    }

    public void TextChangedAge(string newText)
    {
        DataControl.control.playerAge = newText;
    }
}
