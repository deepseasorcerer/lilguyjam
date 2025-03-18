using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class uiButtonMainMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Button button;

    [SerializeField] private Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1f);
    [SerializeField] private Vector3 normalScale = new Vector3(1f, 1f, 1f);
    [SerializeField] private float hoverSpeed = 0.2f;
    [SerializeField] private float clickSpeed = 0.1f;


    void Start()
    {
        button = GetComponent<Button>();
        // Optionally, you can add an onClick event here as well:
        button.onClick.AddListener(OnClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Animate scale on hover enter
        LeanTween.scale(gameObject, hoverScale, hoverSpeed).setEase(LeanTweenType.easeOutElastic);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Animate back to normal scale on hover exit
        LeanTween.scale(gameObject, normalScale, hoverSpeed).setEase(LeanTweenType.easeOutElastic);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Click animation - scale down slightly and then return to normal
        LeanTween.scale(gameObject, new Vector3(0.95f, 0.95f, 1f), clickSpeed).setEase(LeanTweenType.easeOutQuad).setOnComplete(() =>
        {
            LeanTween.scale(gameObject, normalScale, clickSpeed).setEase(LeanTweenType.easeOutBounce);
        });
    }

    void OnClick()
    {
        // You can define more actions to happen after a click here
        Debug.Log("Button Clicked!");
    }
}
