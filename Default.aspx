<%@ Page Title="Connections" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div id="divConnectionWarning">
        <img id="imgWarning" src="Images/warning25.png"/>
        <span>One or more connections could not be mapped to the stored database information. Please consider updating the database.</span>
        <div id="divUnmatchedConnections"></div>
    </div>

    <asp:Repeater ID="rptCustomers" runat="server">
    <HeaderTemplate>
        <table id="tblConnections">
            <tr>
                <th scope="col" id="thStatus">
                    Status
                </th>
                <th scope="col" id="thLabName">
                    Lab Name
                </th>
                <th scope="col" id="thIPAddress">
                    IP Address
                </th>
                <th scope="col" id="thLabID">
                    Lab ID
                </th>
                <th scope="col" id="thUserConnected">
                    User Connected
                </th>
                <th scope="col" id="thDuration">
                    Duration
                </th>
            </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <tr>
            <td>
                <asp:Image ID="imgStatus" runat="server" Text='' />
            </td>
            <td>
                <asp:Label ID="lblLabName" runat="server" Text='<%# Eval("labName") %>' />
            </td>
            <td>
                <asp:Label ID="lblIPAddress" runat="server" Text='<%# Eval("ipAddress") %>' />
            </td>
            <td>
                <asp:Label ID="lblLabID" runat="server" Text='<%# Eval("labID") %>' />
            </td>
            <td>
                <asp:Label ID="lblUserConnected" runat="server" Text='' />
            </td>
            <td>
                <asp:Label ID="lblDuration" runat="server" Text='' />
            </td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>

    <script src="Scripts/default.js?v=1.1ac" type="text/javascript" ></script>
    <link rel="stylesheet" href="Style.css?v1.0c" type="text/css">
    <link rel="stylesheet" href="Content/bootstrap.css?v1.0a" type="text/css">

</asp:Content>


