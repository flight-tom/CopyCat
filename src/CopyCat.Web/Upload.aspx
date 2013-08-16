<%@ Page Language="C#" MasterPageFile="BT.Master" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="BTTrackerWeb.Upload" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style4
        {
            font-family: Arial, Helvetica, sans-serif;
            font-style: italic;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <p class="style4">
        Choose the your local torrent file which you are going to upload:</p>
    <p>
        <asp:FileUpload ID="TorrentUploader" runat="server" Width="100%" />
    </p>
    <p>
        <asp:Button ID="btnUpload" runat="server" Font-Names="Arial" 
            onclick="btnUpload_Click" Text="Upload it!" />
        <asp:Label ID="lblResult" runat="server" EnableViewState="False" 
            Font-Italic="True" Font-Names="Arial" ForeColor="Red" Visible="False"></asp:Label>
    </p>
</asp:Content>
