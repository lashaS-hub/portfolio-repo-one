using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoxValue : ScriptableObject
{
    [Range(1, 100)]
    public int weight = 100;
    public bool appearOnStart = true;

    [Range(1, 15)]
    public int minimumLevelAppear = 1;
    public Sprite image_Sprite;
	public Sprite tier_Sprite;
    public Color tierColor = Color.white;

	
    public AudioClip triggerSound;
    public AudioClip blockSound;


    public abstract bool Contact(BoxBehaviour from, BoxBehaviour to);

    public virtual void Init(BoxBehaviour box) {}

}