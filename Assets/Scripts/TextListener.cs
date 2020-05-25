using UnityEngine;
using UnityEngine.UI;

public class TextListener : MonoBehaviour
{
    public ObservableProperty<int> Property = new ObservableProperty<int>();
    public Text text;

    void Start()
    {
        Property.Subscribe(UpdateText);
        // Property.Value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Property.Value++;
        }
    }

    private void UpdateText(int value)
    {
        text.text = value.ToString();
    }
}
