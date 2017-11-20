public class ConnectionData
{
    public string labName { get; set; }
    public string ipAddress{ get; set; }
    public string labID { get; set; }
    public string serviceHost { get; set; }

    public ConnectionData(string labName, string ipAddress, string labID, string serviceHost) {
        this.labName = labName;
        this.ipAddress = ipAddress;
        this.labID = labID;
        this.serviceHost = serviceHost;
    }
}