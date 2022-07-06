using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;
using UnityEngine;

public class Neural : MonoBehaviour
{
    public CamCore capt;
    bool isRun = false;
    private string path;
    [DllImport("libEmoEngine.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr step();

    [DllImport("libEmoEngine.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int ClearMemory(IntPtr ptr);
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRun)
        {
            StartCoroutine(worker());
        }
    }

    IEnumerator worker()
    {
        isRun = true;
        yield return new WaitForSeconds(1);
        capt.Capture(false);
        yield return new WaitForSeconds(1);
        capt.Capture(true);
        float[] buffer = new float[12];
        IntPtr buf = step();

        Marshal.Copy(buf, buffer, 0, buffer.Length);
        if (buffer[0] > 20.0f | buffer[3] > 20.0f)
        {
            if (buffer[5] > -85.0f)
            {
                anim.Play("Base Layer.W_Walk Left", 0, 0);
            }
            else
            {
                if (buffer[5] > -115.0f)
                {
                    anim.Play("Base Layer.W_Walk", 0, 0);
                }
                else
                {
                    anim.Play("Base Layer.W_Walk Right", 0, 0);
                }
            }
        }
        else
        {
            anim.Play("Base Layer.C_Death1", 0, 0);
        }
        ClearMemory(buf);
        isRun = false;
    }
}
