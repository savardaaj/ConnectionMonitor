$(document).ready(function () {

    checkConnection();
    setInterval(function () {
        checkConnection() // this will run after every 5 seconds
    }, 5000);

    var missingConnections = new Array();

    function Connection(labname, status, hostIP, targetIP, protocol) {
        this.labname = labname;
        this.status = status;
        this.hostIP = hostIP;
        this.targetIP = targetIP;
        this.protocol = protocol;
    }    
    
    function checkConnection() {
        $.ajax({
            url: "//dev02/Connections/Handler.ashx?action=getConnections",
            cache: false,
            success: function (data) {

                clearMissingConnection();
                //data should be list of active connections
                markConnection(data);
                
            },
            fail: function (jqXHR, textStatus, errorThrown) {
                alert("bad thing");
            }
        });
    }

    function markConnection(data) {
        
        var table = document.getElementById("tblConnections");
        var col;
        var row;

        var imgStatus;
        var userConnected;
        var duration;

        //Mark everything as good then only mark active connections later
        for (var i = 1; i < table.rows.length; i++) {
            imgStatus = "#MainContent_rptCustomers_imgStatus_" + (i - 1);
            userConnected = "#MainContent_rptCustomers_lblUserConnected_" + (i - 1);
            duration = "#MainContent_rptCustomers_lblDuration_" + (i - 1);
            $(imgStatus)[0].src = "Images/ready25.png";
            $(userConnected)[0].innerText = "";
            $(duration)[0].innerText = "";
        }

        if (data !== "" && data !== null && data.length > 0) {
            //Iterate through all of the data
            for (var k = 0; k < data.length; k++) {
                var dataMatch = false;
                for (var i = 1; i < table.rows.length; i++) {
                    imgStatus = "#MainContent_rptCustomers_imgStatus_" + (i - 1);
                    userConnected = "#MainContent_rptCustomers_lblUserConnected_" + (i - 1);
                    duration = "#MainContent_rptCustomers_lblDuration_" + (i - 1);

                    if (table.rows[i] !== null && table.rows[i].cells[0].localName !== 'th') {
                        //iterate through columns
                        row = table.rows[i];
                        col = row.cells[2].innerText;
                        //columns would be accessed using the "col" variable assigned in the for loop
                        if (col === data[k].Value.targetIP) {
                            $(imgStatus)[0].src = "Images/busy25.png";
                            $(userConnected)[0].innerText = data[k].Value.serviceHost;
                            displayDuration(duration, data[k].Value.duration.Elapsed);
                            dataMatch = true;
                        }
                    }
                }
                //Data went through table and found no matches
                if (dataMatch == false) {
                    displayMissingConnections(data[k]);
                }
            }
        }
        
    }

    function displayMissingConnections(dataElement) {
        var addMissingConnection = true;
        $('#divConnectionWarning')[0].style.display = "inline";
        $('#imgWarning')[0].style.display = "inline";

        //Push data if the table is empty.
        if (missingConnections.length < 1) {
            addMissingConnection = true;
        }

        for (var i = 0; i < missingConnections.length; i++) {
            if (dataElement.Value.targetIP === missingConnections[i].Value.targetIP) {
                //dont add if it finds it
                addMissingConnection = false;
            }
        }

        if (addMissingConnection) {
            missingConnections.push(dataElement);
            $('#divUnmatchedConnections')[0].innerText += "  " + dataElement.Value.serviceHost + " : " + dataElement.Value.targetIP + " ;   ";
        }
    }

    function clearMissingConnection() {
        missingConnections = [];
        $('#divUnmatchedConnections')[0].innerText = "";
    }
    
    function displayDuration(id, elapsed) {
        var elapsedTime = "";

        if (elapsed.Days > 0) {
            elapsedTime += elapsed.Days + "d ";
        }
        if (elapsed.Hours > 0) {
            elapsedTime += elapsed.Hours + "h ";
        }
        if (elapsed.Minutes > 0) {
            elapsedTime += elapsed.Minutes + "m ";
        }
        if (elapsed.Seconds > 0) {
            elapsedTime += elapsed.Seconds + "s ";
        }
        $(id)[0].innerText = elapsedTime;
    }

    function uuidv4() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c === 'x' ? r : r & 0x3 | 0x8;
            return v.toString(16);
        }).replace(/-/g, '');
    }

    
});
