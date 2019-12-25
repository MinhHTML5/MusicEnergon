using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Helper : MonoBehaviour {
	public static float RAD_TO_DEG = 57.29577951308231f;
	public static float DEG_TO_RAD = 0.0174532925199433f;

	public static float DistanceBetweenTwoPoint (float x1, float y1, float x2, float y2) {
		// ====================================
		// Self explanatory right?
		// ====================================
		return Mathf.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
	}
	
	public static float AngleBetweenTwoPoint (float x1, float y1, float x2, float y2) {
		// ====================================
		// This function get the angle between
		// 2 points, aka, how much you need to
		// rotate (clockwise) to face x2;y2 if 
		// you're at x1;y1, and face directly up.
		// ====================================
		float angle = 0;
		if (y2 == y1) {
			if (x2 < x1)
				angle = 90;
			else if (x2 > x1)
				angle = 270;
		}
		else {
			angle = Mathf.Atan((x1 - x2) / (y2 - y1)) * RAD_TO_DEG;
			if (y2 < y1) {
				angle += 180;
			}
			if (angle < 0) angle += 360;
		}

		return angle;
	}
	
	public static float AngleBetweenAngle (float a1, float a2) {
		// ====================================
		// This function get the difference 
		// between 2 angle, aka, how much you
		// need to rotate to reach a2 from a1.
		// ====================================
		a1 = NormalizeAngle(a1);
		a2 = NormalizeAngle(a2);
		
		/*
		if (Mathf.Abs(a1 - a2) < 180) {
			return a1 - a2;
		}
		else if (a1 > 180) {
			return a1 - a2 - 360;
		}
		else {
			return a1 - a2 + 360;
		}
		*/
		float a = a1 - a2;
		if (a < 0) {
			a = -a - 360;
		}
		return a;
	}
	
	public static float NormalizeAngle (float angle) {
		// ====================================
		// This function convert any angle
		// to an equal angle between 0->360
		// ====================================
		angle = angle % 360;
		if (angle < 0) angle += 360;
		return angle;
	}

	public static float Sin (float angle) {
		// ====================================
		// Sin but in Degree, not Radiant
		// ====================================
		return Mathf.Sin(angle * DEG_TO_RAD);
	}

	public static float Cos (float angle) {
		// ====================================
		// Cos but in Degree, not Radiant
		// ====================================
		return Mathf.Cos(angle * DEG_TO_RAD);
	}
	
	public static Color GetRandomBrightColor () {
		// ====================================
		// This function return a random color
		// with maximum saturation and brightness
		// ====================================
		float hue = Random.Range (0, 360);
		return GetRGBColorFromHSV(hue, 1, 1);
	}
	
	public static Color GetRGBColorFromHSV (float h, float s, float v) {
		// ====================================
		// This function convert HSV color 
		// to RGB format. I got this on Google.
		// ====================================
		float i;
		float f, p, q, t;
		float r, g, b;
		if (s == 0) {
			r = v;
			b = v;
			g = v;
		}
		else {
			h = h / 60;
			i = Mathf.Floor(h);
			f = h - i;
			p = v * ( 1 - s );
			q = v * ( 1 - s * f );
			t = v * ( 1 - s * ( 1 - f ) );
			
			switch( i ) {
				case 0:
					r = v;
					g = t;
					b = p;
					break;
				case 1:
					r = q;
					g = v;
					b = p;
					break;
				case 2:
					r = p;
					g = v;
					b = t;
					break;
				case 3:
					r = p;
					g = q;
					b = v;
					break;
				case 4:
					r = t;
					g = p;
					b = v;
					break;
				default:
					r = v;
					g = p;
					b = q;
					break;
			}
		}
		return new Color(r, g, b, 1);
	}
	
	public static float NumberRunTowardTarget (float val, float target) {
		if (target - val > 1000) {
			val += 57;
		}
		else if (target - val > 100) {
			val += 13;
		}
		else if (target - val > 10) {
			val += 7;
		}
		else if (target - val > 0) {
			val += 1;
		}
		
		if (target - val < -1000) {
			val -= 57;
		}
		else if (target - val < -100) {
			val -= 13;
		}
		else if (target - val < -10) {
			val -= 7;
		}
		else if (target - val < 0) {
			val -= 1;
		}
		
		return val;
	}
	
	public static Color ColorTween (Color start, Color end, float amount) {
		Color result = new Color();
		result.r = start.r + (end.r - start.r) * amount;
		result.g = start.g + (end.g - start.g) * amount;
		result.b = start.b + (end.b - start.b) * amount;
		result.a = start.a + (end.a - start.a) * amount;
		return result;
	}
}
