using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

public class QRCodeScanner : MonoBehaviour
{
    public RawImage targetImage;
    public Texture2D encodedTexture;

    private WebCamTexture camTexture;
    private Thread qrThread;

    private Color32[] color;
    private int width;
    private int height;
    private Rect screenRect;

    private bool isQuit;
    private bool shouldEncodeNow;
    private bool shouldDecodeNow;

    public string result;

    private void OnEnable()
    {
        //Reset();
    }


    //private void OnDestroy()
    //{
    //    qrThread.Abort();
    //    camTexture.Stop();
    //}

    //private void Start()
    //{
    //    encodedTexture = new Texture2D(256, 256);
    //    result = "";
    //    shouldEncodeNow = true;

    //    screenRect = new Rect(0, 0, Screen.width, Screen.height);
    //    camTexture = new WebCamTexture();
    //    camTexture.requestedHeight = Screen.height;
    //    camTexture.requestedWidth = Screen.width;

    //    Reset();

    //    color = new Color32[width * height];
    //    qrThread = new Thread(DecodeQR);
    //    qrThread.Start();
    //}

    //private void Update()
    //{
    //    if (camTexture.isPlaying)
    //    {
    //        if (!shouldDecodeNow)
    //        {
    //            camTexture.GetPixels32(color);
    //            shouldDecodeNow = !shouldDecodeNow;
    //        }

    //        string textForEncoding = result;
    //        if (shouldEncodeNow && textForEncoding != null)
    //        {
    //            Color32[] color32 = Encode(textForEncoding, encodedTexture.width, encodedTexture.height);
    //            encodedTexture.SetPixels32(color32);
    //            encodedTexture.Apply();
    //            shouldEncodeNow = false;
    //        }
    //    }
    //}


    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 32;
        style.normal.textColor = Color.grey;
        GUI.Box(new Rect(Screen.width / 2 - 125, Screen.height / 2 - 25, 250, 50), "cam islaying : " + camTexture.updateCount);
    }

    private void OnApplicationQuit()
    {
        isQuit = true;
    }

    private void Reset()
    {
        
        if (camTexture)
        {
            camTexture.Play();
            width = camTexture.width;
            height = camTexture.height;
        }

    }

    private void DecodeQR()
    {
        BarcodeReader barcodeReader = new BarcodeReader
        {
            AutoRotate = false,
            Options = new DecodingOptions { TryHarder = false }
        };

        while (true)
        {
            if (isQuit) break;

            try
            {
                Result result = barcodeReader.Decode(color, width, height);
                if (result != null)
                {
                    this.result = result.Text;
                    shouldEncodeNow = true;
                    Debug.Log(result.Text + " " + result.BarcodeFormat);
                }

                Thread.Sleep(200);
                shouldDecodeNow = false;
            }
            catch (Exception exception)
            {
                Debug.LogError("err : " + exception.Message);
            }
        }
    }

    private Color32[] Encode(string textForEncoding, int width, int height)
    {
        BarcodeWriter writer = new BarcodeWriter
        {
            Format = BarcodeFormat.QR_CODE,
            Options = new QrCodeEncodingOptions { Height = height, Width = width  }
        };

        return writer.Write(textForEncoding);
    }
}
