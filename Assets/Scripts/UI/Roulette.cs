using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Roulette : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private int visibleElements;
    [Space] [SerializeField] private float startScrollingSpeed;
    [SerializeField] private float scrollingTime = 10f;
    [SerializeField] private float scrollStopSpeed = 0.002f;

    // local variables
    private Scrollbar _scrollbar;
    private readonly List<Transform> _contentList = new List<Transform>();
    private Transform _objectToMove;
    private int _elementsAmount;
    private float _scrollingSpeed;
    private bool _needsToScroll = false;
    private Card _prizeCard;

    private void Awake()
    {
        _scrollbar = GetComponentInChildren<Scrollbar>();
    }

    public void StartScrolling()
    {
        StartCoroutine(scrollingIEnumerator());

        IEnumerator scrollingIEnumerator()
        {
            _contentList.Clear();
            
            yield return new WaitForSeconds(0.05f); // we`ll wait till cards be generated
            
            foreach (Transform child in content.transform)
                _contentList.Add(child);

            _elementsAmount = _contentList.Count;
            _scrollingSpeed = startScrollingSpeed;

            _needsToScroll = true;
        }
    }
    
    private void Update()
    {
        if (_needsToScroll == false)
            return;

        if (_scrollingSpeed > 0)
        {
            _scrollbar.value += _scrollingSpeed; // scrolling
            if (_scrollingSpeed > scrollStopSpeed)
            {
                _scrollingSpeed -= startScrollingSpeed / scrollingTime * Time.deltaTime;
            }
            else if (_scrollbar.value < 0.001 &&
                     _scrollbar.value > -0.001)
            {
                _scrollingSpeed = 0;
                _scrollbar.value = 0;

                _prizeCard = content.transform.GetChild(1).GetComponent<Card>();
                SingleExtractedOresCounter.ores[_prizeCard.index] += _prizeCard.amount;
            }
        }
        else
        {
            _needsToScroll = false;
        }

        MoveElements();


        void MoveElements()
        {
            if (_scrollbar.value > 1f / (_contentList.Count - visibleElements) == false)
                return;

            _contentList[0].SetSiblingIndex(_elementsAmount - 1);
            _scrollbar.value -= 1f / (_elementsAmount - visibleElements);

            _objectToMove = _contentList[0];
            _contentList.RemoveAt(0);
            _contentList.Add(_objectToMove);
        }
    }
}