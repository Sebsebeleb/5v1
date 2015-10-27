using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof (LayoutElement))]
public class ExpandableElement : MonoBehaviour, IPointerClickHandler
{

    private bool _isCollapsed = false;
    private bool _needsUpdate = false;

    // The height to use for layout element when the gameobject is expanded
    public float CollapsedHeight;
    private LayoutElement _element;
    public ILayoutElement SizeController; // The component to use for getting the real wanted size when expanded

    public bool CollapsedByDefault;
        // If this is true, the box will be collapsed by default when it is enabled from being deactivated


    private void Awake()
    {
        _element = GetComponent<LayoutElement>();
    }

    // Ensure the size is correct (could have been changed, like child text changing meaning the expanded size is wrong
    public void OnEnable()
    {
        if (CollapsedByDefault)
        {
            _isCollapsed = true;
        }
        // Preferred size isnt updated untill the next frame so we need to wait a bit
        _needsUpdate = true;
    }

    private void UpdateSizes()
    {
        if (IsCollapsed)
        {
            _element.preferredHeight = CollapsedHeight;
        }
        else
        {
            _element.preferredHeight = -1f;
            Debug.Log(LayoutUtility.GetPreferredHeight(GetComponent<RectTransform>()));
            _element.preferredHeight = LayoutUtility.GetPreferredHeight(GetComponent<RectTransform>());
        }
    }

    public void LateUpdate()
    {
        if (_needsUpdate)
        {
            _needsUpdate = false;

            UpdateSizes();
        }
    }

    public bool IsCollapsed
    {
        get { return _isCollapsed; }
        set { _isCollapsed = value; }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        IsCollapsed = !IsCollapsed;

        if (IsCollapsed)
        {
            _element.DOPreferredSize(new Vector2(-1, CollapsedHeight), 0.3f);
        }
        else
        {
            float oldSize = _element.preferredHeight;

            // Silly way to get the actual preferred (ignoring our own layout element) height.
            _element.preferredHeight = -1f;
            float fullSize = LayoutUtility.GetPreferredHeight(GetComponent<RectTransform>());
            _element.preferredHeight = oldSize;
            _element.DOPreferredSize(new Vector2(-1, fullSize), 0.3f);
        }
    }
}