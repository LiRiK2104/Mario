using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PointsDisplayer : MonoBehaviour
{
    [SerializeField] private PointsCollector _pointsCollector;
    
    private Text _label;
    private int _minLabelLength = 5;

    private void Awake()
    {
        _label = GetComponent<Text>();
    }

    private void OnEnable()
    {
        _pointsCollector.PointsCollected += DisplayPoints;
        DisplayPoints();
    }

    private void OnDisable()
    {
        _pointsCollector.PointsCollected -= DisplayPoints;
    }

    private void DisplayPoints()
    {
        _label.text = _pointsCollector.Points.ToString();
        UpdateLabel();
    }

    private void UpdateLabel()
    {
        if (_label.text.Length >= _minLabelLength)
            return;
        
        char fillChar = '0';
        int charCount = _minLabelLength - _label.text.Length;
        string filler = new string(fillChar, charCount);

        _label.text = filler + _label.text;
    }
}
