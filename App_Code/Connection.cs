using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

public class Connection
{
    public string protocol { get; set; }
    public string hostIP { get; set; }
    public string targetIP { get; set; }
    public string status { get; set; }
    public string labName { get; set; }
    public string serviceHost { get; set; }
    public Stopwatch duration { get; set; }

    public Connection()
    {
        //Start a stopwatch to caluclate duration of connection
        duration = new Stopwatch();
    }

    ~Connection()
    {
        duration.Stop();
    }
 }