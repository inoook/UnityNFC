using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NfcPcSc;

public class UnityNfcTest : MonoBehaviour
{
    [SerializeField] UnityNfc nfcMain = null;

    // Start is called before the first frame update
    void Start()
    {
        nfcMain.eventConnectDevice += (string deviceName) => {
            Debug.Log("【接続】リーダー名: " + deviceName);
        };
        nfcMain.eventNfc += NfcMain_eventNfc;
        nfcMain.eventNfcDetectId += NfcMain_eventNfcDetectId;
    }

    private void NfcMain_eventNfcDetectId(string idstr)
    {
        Debug.LogWarning("ID: " + idstr);
        nfcId = idstr;
    }

    [SerializeField] UnityNfc.NfcState _nfcState = UnityNfc.NfcState.None;
    [SerializeField] string nfcId = "";
    private void NfcMain_eventNfc(UnityNfc.NfcState nfcState)
    {
        Debug.LogWarning("nfcState: "+ nfcState);
        _nfcState = nfcState;
        if(nfcState == UnityNfc.NfcState.Release)
        {
            nfcId = "";
        }
    }

    //
    //
    [SerializeField] Rect drawRect = new Rect(10, 10, 100, 100);

    private void OnGUI()
    {
        GUILayout.BeginArea(drawRect);
        GUILayout.Label("nfcState: "+ _nfcState.ToString() );
        GUILayout.Label("ID: "+ nfcId);

        if (GUILayout.Button("GetUid"))
        {
            string uid = "";
            if (nfcMain.TryGetNfcUID(out uid))
            {
                Debug.Log("ID: " + uid);
            }
            else {
                Debug.Log("認識出来ません");
            }
        }
        GUILayout.EndArea();
    }
}
