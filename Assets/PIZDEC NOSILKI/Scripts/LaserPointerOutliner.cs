using TMPro;
using UnityEngine;
using Valve.VR.Extras;

public class LaserPointerOutliner : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    private TMP_Text textPanel;
    private SteamVR_LaserPointer laserPointer;
    private OutlinableManager currentOutlinableManager;
    private Outlinable lastChosenDetail;

    private void Start()
    {
        panel.SetActive(false);
    }

    public void OnEnable()
    {
        textPanel = panel.GetComponentInChildren<TMP_Text>();
        laserPointer = GetComponent<SteamVR_LaserPointer>();

        laserPointer.PointerIn += SelectDetailWithPointer;
        laserPointer.PointerOut += DeselectDetailWithPointer;
        laserPointer.PointerClick += ChooseDetailWithPointer;
    }

    public void OnDisable()
    {
        laserPointer.PointerIn -= SelectDetailWithPointer;
        laserPointer.PointerOut -= DeselectDetailWithPointer;
        laserPointer.PointerClick -= ChooseDetailWithPointer;
    }

    private void UpdateOutlinableManager(Transform target)
    {
        // Получаем OutlinableManager через родителя объекта (или другим способом)
        var manager = target.GetComponentInParent<OutlinableManager>();
        if (manager != null)
        {
            currentOutlinableManager = manager;
        }
    }

    public void SelectDetailWithPointer(object obj, PointerEventArgs e)
    {
        if (e.target.CompareTag("Outlinable"))
        {
            UpdateOutlinableManager(e.target.transform);

            var selectedDetail = currentOutlinableManager.FindOutlinableByName(e.target.name);
            currentOutlinableManager.SwitchOutline(selectedDetail);
        }
    }

    public void DeselectDetailWithPointer(object obj, PointerEventArgs e)
    {
        if (e.target.CompareTag("Outlinable"))
        {
            UpdateOutlinableManager(e.target.transform);

            var selectedDetail = currentOutlinableManager.FindOutlinableByName(e.target.name);
            currentOutlinableManager.SwitchOutline(selectedDetail);
        }
    }

    public void ChooseDetailWithPointer(object obj, PointerEventArgs e)
    {
        if (e.target.CompareTag("Outlinable"))
        {
            UpdateOutlinableManager(e.target.transform);

            var selectedDetail = currentOutlinableManager.FindOutlinableByName(e.target.name);

            if (selectedDetail != null)
            {
                selectedDetail.outline.OutlineColor = Color.green;
                selectedDetail.isChangable = false;
                selectedDetail.outline.enabled = true;

                if (lastChosenDetail != null)
                {
                    lastChosenDetail.outline.OutlineColor = lastChosenDetail.color;
                    lastChosenDetail.isChangable = true;
                    lastChosenDetail.outline.enabled = false;

                    if (lastChosenDetail == selectedDetail)
                    {
                        lastChosenDetail.isOnceUnchangable = true;
                        lastChosenDetail = null;
                        panel.SetActive(false);

                        return;
                    }
                }
                textPanel.text = selectedDetail.info;
                lastChosenDetail = selectedDetail;
                panel.SetActive(true);
            }
        }
        else
        {
            if (lastChosenDetail != null)
            {
                lastChosenDetail.outline.OutlineColor = lastChosenDetail.color;
                lastChosenDetail.isChangable = true;
                lastChosenDetail.outline.enabled = false;

                lastChosenDetail = null;
                panel.SetActive(false);
            }
        }
    }
}