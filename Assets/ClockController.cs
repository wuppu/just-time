using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;

public class ClockController : MonoBehaviour {

    public float speed = 0.5f;

    // Clock Items
    public GameObject hourFirst;
    public GameObject hourSecond;
    public GameObject minFirst;
    public GameObject minSecond;
    public GameObject secFirst;
    public GameObject secSecond;
    public GameObject am_pm;
    public GameObject day;

    private string strDateTime;
    private List<GameObject> clockItems = new List<GameObject>();
    private CultureInfo culture;

    private void Start() {
        culture = CultureInfo.CreateSpecificCulture("en-US");
        clockItems.Add(hourFirst);
        clockItems.Add(hourSecond);
        clockItems.Add(minFirst);
        clockItems.Add(minSecond);
        clockItems.Add(secFirst);
        clockItems.Add(secSecond);
        clockItems.Add(am_pm);
        clockItems.Add(day);
    }

    // Update is called once per frame
    private void Update() {

        // Datetime parsing
        strDateTime = DateTime.Now.ToString("hhmmsst", culture);
        strDateTime = strDateTime.Substring(0, 7);
        strDateTime += DateTime.Now.ToString("ddd", culture);

        // 시계 작동
        NumberMoving();

    }

    /// <summary>
    /// 숫자 움직이는 함수
    /// </summary>
    public void NumberMoving() {
        for (int i = 0; i < clockItems.Count; i++) {

            SpriteRenderer targetSprite;
            float originX = clockItems[i].transform.position.x;
            int targetIdx;

            if (i == clockItems.Count - 1) {
                switch (strDateTime.Substring(i, 3)) {
                    case "Mon": targetIdx = 0; break;
                    case "Tue": targetIdx = 1; break;
                    case "Wed": targetIdx = 2; break;
                    case "Thu": targetIdx = 3; break;
                    case "Fri": targetIdx = 4; break;
                    case "Sat": targetIdx = 5; break;
                    case "Sun": targetIdx = 6; break;
                    default: targetIdx = 0; break;
                }
                clockItems[i].transform.position = Vector2.Lerp(
                    clockItems[i].transform.position,
                    new Vector2(originX, targetIdx * 5),
                    speed
                );
            }
            else if (strDateTime[i] == 'A' || strDateTime[i] == 'P') {
                targetIdx = strDateTime[i] - 'A' == 0 ? 0 : 1;
                clockItems[i].transform.position = Vector2.Lerp(
                    clockItems[i].transform.position,
                    new Vector2(originX, targetIdx * 5),
                    speed
                );
            }
            else {
                targetIdx = strDateTime[i] - '0';
                clockItems[i].transform.position = Vector2.Lerp(
                    clockItems[i].transform.position,
                    new Vector2(originX, targetIdx * 5),
                    speed
                );
            }

            // 지금 숫자를 선명하게 만들고, 이전의 숫자는 흐리게 만든다.
            for (int j = 0; j < clockItems[i].transform.childCount; j++) {
                int dist = Mathf.Abs(targetIdx - j);
                float alpha = dist == 0 ? 1f : (40 - dist * 8) / 255f;

                targetSprite = clockItems[i].transform.GetChild(j).GetComponent<SpriteRenderer>();
                targetSprite.color = Color.Lerp(targetSprite.color, new Color(1f, 1f, 1f, alpha), speed);
            }
        }
    }
}
