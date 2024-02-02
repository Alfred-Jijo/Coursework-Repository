using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

public class HitCountStore : MonoBehaviour
{
    [SerializeField] private int hitCountUnmodified = 0;
    [SerializeField] private int hitCountShift = 0;
    [SerializeField] private int hitCountControl = 0;
    private KeyCode modifier = default;

    void Start()
    {
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
        dbCommandReadValues.CommandText = "SELECT * FROM HitCountTableExtended";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

        while (dataReader.Read())
        {
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

        dbConnection.Close();
    }

    private void OnMouseDown()
    {
        var hitCount = GetHitCount();
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommandInsertValue = dbConnection.CreateCommand();
        dbCommandInsertValue.CommandText = "INSERT OR REPLACE INTO HitCountTableExtended (id, hits) VALUES (" + (int)modifier + ", " + hitCount + ")";
        dbCommandInsertValue.ExecuteNonQuery();

        dbConnection.Close();
    }

    private int GetHitCount()
    {
        return modifier switch
        {
            KeyCode.LeftShift => ++hitCountShift,
            KeyCode.LeftControl => ++hitCountControl,
            _ => ++hitCountUnmodified,
        };
    }

    private IDbConnection CreateAndOpenDatabase()
    {
        string dbUri = "URI=file:MyDatabase.sqlite";
        IDbConnection dbConnection = new SqliteConnection(dbUri);
        dbConnection.Open();

        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand();
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS HitCountTableExtended (id INTEGER PRIMARY KEY, hits INTEGER)";
        dbCommandCreateTable.ExecuteReader();

        return dbConnection;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            modifier = KeyCode.LeftShift;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            modifier = KeyCode.LeftControl;
        }
        else
        {
            modifier = default;
        }
    }
}
