using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Global
/// </summary>
public class Global
{
    public Global()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static List<KeyValuePair<string, Connection>> _data;

    /// <summary>
    /// Get or set the static important data.
    /// </summary>
    public static List<KeyValuePair<string, Connection>> Data
    {
        get
        {
            return _data;
        }
        set
        {
            _data = value;
        }
    }
}