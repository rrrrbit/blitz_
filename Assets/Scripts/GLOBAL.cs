using UnityEngine;

public static class GLOBAL
{
    public static float Lerpd(float a, float b, float k, float t, float d)
    {
        return Mathf.Lerp(
            a, b,
            1 - Mathf.Pow(
                1 - k,
                d / t));
    }
    public static Vector2 Lerpd(Vector2 a, Vector2 b, float k, float t, float d)
    {
        return Vector2.Lerp(
            a, b,
            1 - Mathf.Pow(
                1 - k,
                d / t));
    }
    public static float Lerpd(float a, float b, float f, float d)
    {
        return Mathf.Lerp(
            a, b,
            1 - Mathf.Pow(f, d));
    }
    public static Vector2 Lerpd(Vector2 a, Vector2 b, float f, float d)
    {
        return Vector2.Lerp(
            a, b,
            1 - Mathf.Pow(f, d));
    }
    public static float LerpdF(float k, float t)
    {
        return Mathf.Pow(1 - k, 1 / t);
    }
}
