<%@ Page Language="C#" MasterPageFile="BT.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BTTrackerWeb.WebForm1" Title="Torrent download page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" runat="server">
    <asp:GridView ID="TorrentGrid" runat="server" AutoGenerateColumns="False" 
        BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
        CellPadding="3" Font-Names="Arial" GridLines="Vertical" Width="100%">
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <Columns>
            <asp:HyperLinkField DataNavigateUrlFields="URL" DataTextField="FileName" 
                HeaderText="File Name" />
            <asp:BoundField DataField="UploadedDate" HeaderText="Last Date" />
        </Columns>
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
    </asp:GridView>
</asp:Content>
