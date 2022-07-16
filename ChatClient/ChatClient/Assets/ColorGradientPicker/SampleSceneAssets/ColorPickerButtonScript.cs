using UnityEngine;

public class ColorPickerButtonScript : MonoBehaviour
{
    private UnityEngine.UI.Image r;
    void Start()
    {
        r = GetComponent<UnityEngine.UI.Image>();
        r.color = r.color;
    }
    public void ChooseColorButtonClick() => ColorPicker.Create(r.color, "Select Player color", SetColor, ColorFinished, true);
    private void SetColor(Color currentColor) => r.color = currentColor;

    private void ColorFinished(Color finishedColor) { }
}
