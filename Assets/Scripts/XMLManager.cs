using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.SceneManagement;

public class XMLManager : MonoBehaviour
{
[System.Serializable]
public struct GameDataObject{
    public int currentLevel;
    public GameDataObject(int currentLevel){
        this.currentLevel = currentLevel;
    }
}



    private const string saveFileLocation = "Assets/Saves/saveData.xml";
    public static XMLManager SaveDataHandler;


    // Start is called before the first frame update
    void Start()
    {
        SaveDataHandler = this;    
    }

    // Update is called once per frame
    public void Save(int levelID){
        XmlDocument xmlDocument = new XmlDocument();
        GameDataObject gameState = new GameDataObject(levelID);
        XmlSerializer serializer = new XmlSerializer(typeof(GameDataObject));
        using(MemoryStream stream = new MemoryStream()){
            serializer.Serialize(stream, gameState);
            stream.Position = 0;
            xmlDocument.Load(stream);
            xmlDocument.Save(saveFileLocation);
        }
        Debug.Log("Saved game");
    }

    public void Load(){
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load(saveFileLocation);
        string xmlString = xmlDocument.OuterXml;

        GameDataObject gameState;

        using (StringReader read = new StringReader(xmlString)){
            XmlSerializer serializer = new XmlSerializer(typeof(GameDataObject));
            using (XmlReader reader = new XmlTextReader(read))
                {
                gameState = (GameDataObject) serializer.Deserialize(reader);
                }
        }
        Debug.Log("load game");
        int levelToLoad = gameState.currentLevel;
        Debug.Log(levelToLoad);
        SceneManager.LoadSceneAsync(levelToLoad);
    }
}


