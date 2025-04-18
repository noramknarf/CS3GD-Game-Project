using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;

[Serializable]
public class HighScoreResult : IComparable{
    public int Score;
    public string code;
    public string message;
    //public string updated;
    //public string created;
    public string objectId;
    

    public int CompareTo(object obj){
        if (obj == null){return 1;}
        
        HighScoreResult otherScore = obj as HighScoreResult;
        if (otherScore != null){
            return this.Score.CompareTo(otherScore.Score);
        }
        else throw new ArgumentException("object is not another HighScoreResult");
    }
}
[Serializable]
public class HighScoreDataset{
    public HighScoreResult[] highScores;
}

public class RemoteHighScoreManager : MonoBehaviour {

    public static RemoteHighScoreManager Instance { get; private set; }

    private IEnumerator coroutineSend;
    private IEnumerator coroutineReceive;
    public List<HighScoreResult> highScoresFromDB; 

    void Awake() {
        // force singleton instance
        if (Instance == null) { Instance = this; } else { Destroy(gameObject); }

        // don't destroy this object when we load scene
        DontDestroyOnLoad(gameObject);
    }

    public void GetHighScore(Action onCompleteCallback) {
        coroutineReceive = GetHighScoreCR(onCompleteCallback);
        StartCoroutine(coroutineReceive);

    }

    public void SetHighScore(Action OncompleteCallback, int score) {
        coroutineSend = SetHighScoreCR(OncompleteCallback, score, "");
        StartCoroutine(coroutineSend);

    }
    public void SetHighScore(Action OncompleteCallback, int score, string id) {
        coroutineSend = SetHighScoreCR(OncompleteCallback, score, id);
        StartCoroutine(coroutineSend);

    }


    public IEnumerator GetHighScoreCR(Action onCompleteCallback) {
        Debug.Log("Begin GetHighScoreCR");

        string strTableName = "HighScores";

        // TODO #2 - construct the url for our request, including objectid!
        const string objectID = "A05204F7-51D6-4736-BD52-DB7619ED1137";
        string url = /*"https://api.backendless.com/022A73B8-8106-45AB-8A3E-13C35A048C67/3523EAD7-33DC-4F93-9443-CAD91086CEDA/data/HighScores/A05204F7-51D6-4736-BD52-DB7619ED1137" ;*/"https://api.backendless.com/" +
                    PersistentDataHandler.APPLICATION_ID + "/" +
                    PersistentDataHandler.REST_SECRET_KEY +
                    "/data/" +
                    strTableName;/* +
                    "/" +
                    objectID;*/




        // TODO #3 - create a GET UnityWebRequest, passing in our URL
        UnityWebRequest webreq = UnityWebRequest.Get(url);


        // TODO #4 - set the request headers as dictated by the backendless documentation (3 headers)
        webreq.SetRequestHeader("application-id", PersistentDataHandler.APPLICATION_ID);
        webreq.SetRequestHeader("secret-key", PersistentDataHandler.REST_SECRET_KEY);
        webreq.SetRequestHeader("application-type", "REST");

        // TODO #5 - Send the webrequest and yield (so the script waits until it returns with a result)
        yield return webreq.SendWebRequest();

        // TODO #6 - check for webrequest errors
        if (webreq.result == UnityWebRequest.Result.ConnectionError) {
            Debug.Log("ConnectionError");
        } else if (webreq.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("ProtocolError");
            Debug.Log("Code: " + webreq.responseCode);
        } else if (webreq.result == UnityWebRequest.Result.DataProcessingError) {
            Debug.Log("DataProcessingError");
        } else if (webreq.result == UnityWebRequest.Result.Success) {
            Debug.Log("Success");
        
            // TODO #7 - Convert the downloadHandler.text property to HighScoreResult (currently JSON)
            Debug.Log(webreq.downloadHandler.text);
            Debug.Log(webreq.downloadHandler.text.GetType());
           // foreach(){}

            string json = webreq.downloadHandler.text;
            HighScoreDataset dbRows = JsonUtility.FromJson<HighScoreDataset>("{\"highScores\":" + json + "}");
            //HighScoreResult highScoreData = JsonUtility.FromJson<HighScoreResult>(webreq.downloadHandler.text);
            HighScoreResult highScoreData = dbRows.highScores[0];
            Debug.Log("dbRows: " + dbRows);
            if(dbRows.highScores == null){
                Debug.Log("is null");
            }
            Debug.Log("First entry = " + dbRows.highScores[0]);

            // TODO #8 - check that there are no backendless errors
            if (!string.IsNullOrEmpty(highScoreData.code)) {
                Debug.Log("Error:" + highScoreData.code + " " + highScoreData.message);
            }
            else{
                Debug.Log("Gets here");
                RemoteHighScoreManager.Instance.highScoresFromDB = new List<HighScoreResult>();
                foreach(HighScoreResult highScore in dbRows.highScores){
                    highScoresFromDB.Add(highScore);
                }
        
                List<int> topFive = new List<int>();
                foreach(HighScoreResult highScore in dbRows.highScores){
                    topFive.Add(highScore.Score);
                }
                topFive.Sort();
                topFive.Reverse();
                onCompleteCallback(); //returns all the scores in the DB in descending order
            }
        }
    }

    // TODO #1 - change the signature to be a Coroutine, add callback parameter
    public IEnumerator SetHighScoreCR(Action OnCompleteCallback,int score, string id) {
        string strTableName = "HighScores";
        string url = "";
        if (String.IsNullOrEmpty(id)){
        
            // TODO #2 - construct the url for our request, including objectid!
            url = "https://api.backendless.com/" +
                    PersistentDataHandler.APPLICATION_ID + "/" +
                    PersistentDataHandler.REST_SECRET_KEY +
                    "/data/" +
                    strTableName;
        }
        else{
            url = "https://api.backendless.com/" +
                    PersistentDataHandler.APPLICATION_ID + "/" +
                    PersistentDataHandler.REST_SECRET_KEY +
                    "/data/" +
                    strTableName +
                    "/" +
                    id;
        }

        


        // TODO #3 - construct JSON string for data we want to send
        string data = JsonUtility.ToJson(new HighScoreResult { Score = score });

        // TODO #4 - create PUT UnityWebRequest passing our url and data
        UnityWebRequest webreq = UnityWebRequest.Put(url, data);

        // TODO #5 set the request headers as dictated by the backendless documentation (4 headers)
        webreq.SetRequestHeader("Content-Type", "application/json");
        webreq.SetRequestHeader("application-id", PersistentDataHandler.APPLICATION_ID);
        webreq.SetRequestHeader("secret-key", PersistentDataHandler.REST_SECRET_KEY);
        webreq.SetRequestHeader("application-type", "REST");

        // TODO #6 - Send the webrequest and yield (so the script waits until it returns with a result)
        yield return webreq.SendWebRequest();

        // TODO #7 - check for webrequest errors
        if (webreq.result == UnityWebRequest.Result.ConnectionError) {
            Debug.Log("ConnectionError");
        } else if (webreq.result == UnityWebRequest.Result.ProtocolError) {
            Debug.Log("ProtocolError");
        } else if (webreq.result == UnityWebRequest.Result.DataProcessingError) {
            Debug.Log("DataProcessingError");
        } else if (webreq.result == UnityWebRequest.Result.Success) {
            Debug.Log("Successful put request");
        
            // TODO #7 - Convert the downloadHandler.text property to HighScoreResult (currently JSON)
            HighScoreResult highScoreData = JsonUtility.FromJson<HighScoreResult>(webreq.downloadHandler.text);

            // TODO #8 - check that there are no backendless errors
            if (!string.IsNullOrEmpty(highScoreData.code)) {
                Debug.Log("Error:" + highScoreData.code + " " + highScoreData.message);
            }
            else{
                // string json = webreq.downloadHandler.text;
                // HighScoreDataset dbRows = JsonUtility.FromJson<HighScoreDataset>("{\"highScores\":" + json + "}");
                // RemoteHighScoreManager.Instance.highScoresFromDB = new List<HighScoreResult>();
                // foreach(HighScoreResult highScore in dbRows.highScores){
                //     highScoresFromDB.Add(highScore);
                // }
                OnCompleteCallback();
            }
        }
    }

}

