<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Web.Script.Serialization;
using System.Linq;

public class Handler : IHttpHandler {

    public JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

    public void ProcessRequest(HttpContext context)
    {
        var request = context.Request;
        var requestBody = new StreamReader(request.InputStream, request.ContentEncoding).ReadToEnd();

        //If it is a post
        if (requestBody != "")
        {
            ProcessNewData(requestBody);
        }
        else
        {
            string action = HttpUtility.ParseQueryString(request.Url.Query).Get("action");
            var data = new object();
            switch (action)
            {
                case "getConnections":
                    data = getConnections();
                    break;
                default:
                    break;
            }

            string jsonRet = jsonSerializer.Serialize(data);
            context.Response.ContentType = "application/json";
            context.Response.Write(jsonRet);
        }

    }

    public void Page_Load(object sender, EventArgs e)
    {

    }

    public List<KeyValuePair<string, Connection>> getConnections()
    {
        if (Global.Data != null)
        {
            return Global.Data;
        }
        else
        {
            return new List<KeyValuePair<string, Connection>>();
        }
    }

    public void ProcessNewData(string requestBody) {
        List<KeyValuePair<string, Connection>> newDataListKVP = new List<KeyValuePair<string, Connection>>();
        List<KeyValuePair<string, Connection>> globalCopy;
        List<Connection> data = new List<Connection>();

        //Initialize global data if it is null
        if (Global.Data == null) {
            Global.Data = new List<KeyValuePair<string, Connection>>();
        }

        KeyValuePair<string, Connection> temp = new KeyValuePair<string, Connection>();
        globalCopy = new List<KeyValuePair<string, Connection>>(Global.Data);

        data = jsonSerializer.Deserialize<List<Connection>>(requestBody);

        //If it is a new connection, add it
        foreach(Connection c in data)
        {
            temp = new KeyValuePair<string, Connection>(c.serviceHost, c);
            newDataListKVP.Add(temp);
            var result = (from kvp in Global.Data where (kvp.Key == temp.Key) && (kvp.Value.targetIP == temp.Value.targetIP) select kvp.Value);
            if(result.Count() < 1)
            {
                temp.Value.duration.Start();
                Global.Data.Add(temp);
            }
        }

        //if we have it in global data but not in the new list, drop it
        foreach (KeyValuePair<string, Connection> gkvp in globalCopy)
        {
            //if global list has stuff that new list doesn't, connection was dropped
            var result = (from kvp in newDataListKVP where (kvp.Key == gkvp.Key) && (kvp.Value.targetIP == gkvp.Value.targetIP) select kvp.Value);
            if (result.Count() < 1)
            {
                Global.Data.Remove(gkvp);
            }
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}