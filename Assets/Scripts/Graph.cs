﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
    public Transform pointPrefab;
	[Range(10, 100)]
	public int resolution = 10;

	Transform[] points;
	const float pi = Mathf.PI;

	// [Range(0, 1)]
	// public int function;

	static GraphFunction[] functions = {
		SineFunction, Sine2DFunction, MultiSineFunction,MultiSine2DFunction,
		Ripple
	};

	public GraphFunctionName function;

	void Awake()
	{
		float step = 2f / resolution;
		Vector3 scale = Vector3.one * step;
		points = new Transform[resolution * resolution];
	}

	void Update()
	{
		float t = Time.time;
		GraphFunction f = functions[(int)function];
		float step = 2f / resolution;
		for (int i = 0, z = 0; z < resolution; z++)
		{
			float v = (z + 0.5f) * step - 1f;
			for (int x = 0; x < resolution; x++, i++)
			{
				float u = (x + 0.5f) * step - 1f;
				points[i].localPosition = f(u, v, t);
			}
		}
	}

	static Vector3 SineFunction(float x, float z, float t)
    {
		Vector3 p;
		p.x = x;
		p.y = Mathf.Sin(pi * (x + t));
		p.z = z;
		return p;
	}

	static Vector3 Sine2DFunction(float x, float z, float t)
	{
		Vector3 p;
		p.x = x;
		p.y = Mathf.Sin(pi * (x + t));
		p.y += Mathf.Sin(pi * (z + t));
		p.y *= 0.5f;
		p.z = z;
		return p;
	}

	static Vector3 MultiSine2DFunction(float x, float z, float t)
	{
		Vector3 p;
		p.x = x;
		p.y = 4f * Mathf.Sin(pi * (x + z + t / 2f));
		p.y += Mathf.Sin(pi * (x + t));
		p.y += Mathf.Sin(2f * pi * (z + 2f * t)) * 0.5f;
		p.y *= 1f / 5.5f;
		p.z = z;
		return p;
	}

	static Vector3 MultiSineFunction(float x, float z, float t)
	{
		Vector3 p;
		p.x = x;
		p.y = Mathf.Sin(pi * (x + t));
		p.y += Mathf.Sin(2f * pi * (x + 2f * t)) / 2f;
		p.y *= 2f / 3f;
		p.z = z;
		return p;
	}

	static Vector3 Ripple(float x, float z, float t)
	{
		Vector3 p;
		float d = Mathf.Sqrt(x * x + z * z);
		p.x = x;
		p.y = Mathf.Sin(pi * (4f * d - t));
		p.y /= 1f + 10f * d;
		p.z = z;
		return p;
	}

}
