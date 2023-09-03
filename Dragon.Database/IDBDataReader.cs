﻿namespace Dragon.Database;

public interface IDBDataReader {
    void Close();
    bool Read();
    object GetData(string column);
}