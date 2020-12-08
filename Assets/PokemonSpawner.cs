using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using System.Threading.Tasks;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class PokemonSpawner : MonoBehaviour
{
    public GameObject playerController;
    static FirebaseDatabase database = FirebaseDatabase.DefaultInstance;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static async Task GetPokemon(string pokeName)
    {
        await database.RootReference.OrderByChild("Tier").EqualTo("1").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                // Do something with snapshot...

                foreach (var item in (Dictionary<string, object>)snapshot.Value)
                {
                    object value;
                    ((Dictionary<string, object>)(item.Value)).TryGetValue("Name", out value);
                    Debug.Log(value.ToString());
                }
            }
            
        });
    }
    public PokemonSpawner(string json)
    {
        //JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        //PokeInfo pokeList =
        //       (PokeInfo)jsonSerializer.DeserializeObject(json);
        //Debug.Log(pokeList);
    }
}

