using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Drag3D : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private bool isActive = true;
    public bool IsActive
    {
        get => isActive;
        set
        {
            if (useDelay)
                transform.DOKill();
            isActive = value;
        }
    }

    [SerializeField] private bool isHorizontal;
    [SerializeField] private bool isVertical;
    [SerializeField] private bool isAxisZ;

    [SerializeField] private bool useDelay;

    [SerializeField] private float InputAxisYOffset = -1f;

    [SerializeField] private MoveConstraint constraintAxisX;
    [SerializeField] private MoveConstraint constraintAxisY;
    [SerializeField] public MoveConstraint constraintAxisZ;

    public UnityEvent OnDrag = new UnityEvent();

    void OnMouseDrag()
    {
        if (!isActive)
            return;

        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        var temp = transform.position.z;

        var myPos = transform.position;
        var targetPos = Camera.main.ScreenToWorldPoint(mousePoint);

        var result = new Vector3(isHorizontal ? Mathf.Clamp(targetPos.x, constraintAxisX.minPoint, constraintAxisX.maxPoint) : myPos.x,
                isVertical ? Mathf.Clamp(targetPos.y + InputAxisYOffset, constraintAxisY.minPoint, constraintAxisY.maxPoint) : myPos.y,
                isAxisZ ? Mathf.Clamp((targetPos.z + targetPos.y + InputAxisYOffset), constraintAxisZ.minPoint, constraintAxisZ.maxPoint) : temp);
        //Debug.Log("targetposZ = " + targetPos.z + "targetPosY = " + targetPos.y + "InputAxisYOffset = " + InputAxisYOffset + "MİN POİNT" + constraintAxisZ.minPoint + " max point " + constraintAxisZ.maxPoint);

        if (useDelay)
        {
            transform.DOKill();
            transform.DOMove(result, 2f);
        }
        else
            transform.position = result;

        OnDrag.Invoke();
    }
    private void OnMouseUp()
    {
        transform.position = _target.position;
    }

    [System.Serializable]
    public class MoveConstraint
    {
        public float minPoint;
        public float maxPoint;
    }

}
