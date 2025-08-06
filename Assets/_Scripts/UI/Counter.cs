using UnityEngine;
using DG.Tweening;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Counter : MonoBehaviour
{
    [SerializeField] protected string textToAdd;
    protected TextMeshProUGUI counterText;

    protected void Awake()
    {
        counterText = GetComponent<TextMeshProUGUI>();
    }
    public void UpdateCounter(string counterTextValue)
    {
        counterText.text = textToAdd + counterTextValue;
        Vector3 punchScale = new Vector3(0.1f, 0.1f, 0.1f);
        float punchDuration = 0.25f;
        counterText.transform.DOPunchScale(punchScale, punchDuration);
    }
}