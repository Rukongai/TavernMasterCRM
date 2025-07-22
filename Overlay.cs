using RukonMods.TavCRM;
using UnityEngine;

public class CustomerStatsOverlay : MonoBehaviour
{
    private Rect windowRect = new Rect(100, 100, 220, 130);
    private bool showWindow = true;
    private bool isResizing = false;
    private Vector2 resizeStartMouse;
    private Vector2 resizeStartSize;
    private readonly float resizeHandleSize = 20f;

    void OnGUI()
    {
        if (!showWindow) return;

        GUI.color = new Color(1, 1, 1, 0.8f);
        windowRect = GUI.Window(12345, windowRect, DrawWindowContents, "Customer Stats");
        GUI.color = Color.white;

        // Draw resize handle (bottom-right corner)
        var handleRect = new Rect(windowRect.xMax - resizeHandleSize, windowRect.yMax - resizeHandleSize, resizeHandleSize, resizeHandleSize);
        GUI.Box(handleRect, "");

        // Mouse handling
        var e = Event.current;
        if (e.type == EventType.MouseDown && handleRect.Contains(e.mousePosition))
        {
            isResizing = true;
            resizeStartMouse = e.mousePosition;
            resizeStartSize = windowRect.size;
            e.Use();
        }
        if (isResizing)
        {
            if (e.type == EventType.MouseDrag)
            {
                var delta = e.mousePosition - resizeStartMouse;
                windowRect.width = Mathf.Max(100, resizeStartSize.x + delta.x);
                windowRect.height = Mathf.Max(50, resizeStartSize.y + delta.y);
                e.Use();
            }
            if (e.type == EventType.MouseUp)
            {
                isResizing = false;
                e.Use();
            }
        }
    }

    void DrawWindowContents(int windowID)
    {
        GUILayout.Label($"Common: {Stats.CustomerStatsCache.Common}");
        GUILayout.Label($"Rare:   {Stats.CustomerStatsCache.Rare}");
        GUILayout.Label($"Gold:   {Stats.CustomerStatsCache.Gold}");
        GUILayout.Label($"Royal:  {Stats.CustomerStatsCache.Royal}");
        GUILayout.Label($"Customer's Using Tavern:  {Stats.CustomerStatsCache.Total.Count}");
        GUILayout.Label($"customerCounter:  {Stats.CustomerStatsCache.customerCounter}");
        GUILayout.Label("--------------------------");
        GUILayout.Label($"Waiting to be Seated: {Stats.CustomerStatsCache.Seated}");
        GUILayout.Label("--------------------------");
        GUILayout.Label($"Drinks Ordered: {Stats.CustomerStatsCache.DrinksOrdered}");
        GUILayout.Label($"Drinks Filled: {Stats.CustomerStatsCache.DrinksFilled}");
        GUILayout.Label($"Drinks Served - {Stats.CustomerStatsCache.DrinksServed}");
        GUILayout.Label("--------------------------");
        GUILayout.Label($"Food Orders - {Stats.CustomerStatsCache.FoodOrdered}");
        GUILayout.Label($"Food Prepared - {Stats.CustomerStatsCache.FoodPrepared}");
        GUILayout.Label($"Food Served - {Stats.CustomerStatsCache.FoodServed}");
        GUILayout.Label("--------------------------");
        GUILayout.Label($"Hotel Customers:  {Stats.CustomerStatsCache.HotelCustomer}");
        GUILayout.Space(10);

        if (GUILayout.Button("Close")) showWindow = false;

        // Make the window draggable
        GUI.DragWindow(new Rect(0, 0, 10000, 20));
    }
}