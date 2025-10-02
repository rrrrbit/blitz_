using UnityEngine;

public static class GLOBAL
{
    public static float SQRT2OVER2 = Mathf.Sqrt(2) / 2;
    
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
	public static Vector3 Lerpd(Vector3 a, Vector3 b, float k, float t, float d)
	{
		return Vector3.Lerp(
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
	public static Vector3 Lerpd(Vector3 a, Vector3 b, float f, float d)
	{
		return Vector3.Lerp(
			a, b,
			1 - Mathf.Pow(f, d));
	}
	public static float LerpdF(float k, float t)
	{
		return Mathf.Pow(1 - k, 1 / t);
	}
	public static void DrawCross(Vector3 pos, float size = 10, Color? color = null)
	{
		var c = color ?? Color.white;

		Debug.DrawLine(pos + Vector3.left * size / 2, pos + Vector3.right * size / 2, c);
		Debug.DrawLine(pos + Vector3.up * size / 2, pos + Vector3.down * size / 2, c);
	}
}
