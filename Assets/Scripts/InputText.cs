using UnityEngine;
using System.Collections;

public class InputText : MonoBehaviour {

	public void TextChanged(string newText)
    {
        DataControl.control.playerName = newText;
    }
}
