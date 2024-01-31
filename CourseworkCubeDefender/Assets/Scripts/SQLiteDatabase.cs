using Mono.Data.Sqlite; 
using System.Data; 
using UnityEngine;

public class SqliteExampleSimple : MonoBehaviour
{


    [SerializeField] private int hitCountUnmodified = 0;
    [SerializeField] private int hitCountShift = 0;
    [SerializeField] private int hitCountControl = 0;
    private KeyCode modifier = default;

    void Start()
    {
        // Read all values from the table.
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand(); 
        dbCommandReadValues.CommandText = "SELECT * FROM HitCountTable";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

        while (dataReader.Read())
        {
            // The `id` has index 0, our `value` has the index 1.
            var id = dataReader.GetInt32(0);  
            var hits = dataReader.GetInt32(1); 
            if (id == (int)KeyCode.LeftShift) 
            {
                hitCountShift = hits; 
            }
            else if (id == (int)KeyCode.LeftControl) 
            {
                hitCountControl = hits;
            }
            else
            {
                hitCountUnmodified = hits;
            }
        }

        // Remember to always close the connection at the end.
        dbConnection.Close();
    }

    private void OnMouseDown()
    {
        var hitCount = 0;
        switch (modifier)
        {
            case KeyCode.LeftShift:
                // Increment the hit count and set it to PlayerPrefs.
                hitCount = ++hitCountShift;
                break;
            case KeyCode.LeftControl:
                // Increment the hit count and set it to PlayerPrefs.
                hitCount = ++hitCountControl;
                break;
            default:
                // Increment the hit count and set it to PlayerPrefs.
                hitCount = ++hitCountUnmodified; 
                break;
        }

        // Insert a value into the table.
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommandInsertValue = dbConnection.CreateCommand();
        dbCommandInsertValue.CommandText = "INSERT OR REPLACE INTO HitCountTable (id, hits) VALUES (" + (int)modifier + ", " + hitCount + ")";
        dbCommandInsertValue.ExecuteNonQuery();

        // Remember to always close the connection at the end.
        dbConnection.Close();
    }

    private IDbConnection CreateAndOpenDatabase() 
    {
        // Open a connection to the database.
        string dbUri = "URI=file:TestDatabase.db"; 
        IDbConnection dbConnection = new SqliteConnection(dbUri); 
        dbConnection.Open(); 

        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); 
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS HitCountTable (id INTEGER PRIMARY KEY, hits INTEGER )"; 
        dbCommandCreateTable.ExecuteReader(); 

        return dbConnection;
    }

    private void Update()
{
    // Check if a key was pressed.
    if (Input.GetKey(KeyCode.LeftShift)) 
    {
        // Set the LeftShift key.
        modifier = KeyCode.LeftShift; 
    }
    else if (Input.GetKey(KeyCode.LeftControl)) 
    {
        // Set the LeftControl key.
        modifier = KeyCode.LeftControl;
    }
    else // 3
    {
        // In any other case reset to default and consider it unmodified.
        modifier = default; 
    }
}
}