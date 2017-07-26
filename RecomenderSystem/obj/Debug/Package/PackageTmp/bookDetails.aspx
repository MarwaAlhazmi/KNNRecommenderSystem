<%@ Page Title="" Language="C#" MasterPageFile="~/Second.Master" AutoEventWireup="true" CodeBehind="bookDetails.aspx.cs" Inherits="RecomenderSystem.bookDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 119px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function setToRed() {
            document.getElementById("myDivID").style.visibility = "visible";

            setTimeout("setToBlack()", 2000);
        }

        function setToBlack() {
            document.getElementById("myDivID").style.visibility = "hidden";

        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
  
<table width="100%">
<tr>
<td style="width:310px">
    <asp:Image ID="img" runat="server" Height="300px" Width="300px" 
        BorderColor="Silver" BorderStyle="Solid" />
</td>
<td>
<table width="100%">
<tr>
<td class="style1">Title</td>
<td>
    <asp:Label ID="lblTitle" runat="server" Text="Label"></asp:Label>
    </td>
</tr>
<tr>
<td class="style1">Author</td>
<td>
    <asp:Label ID="lblAuthor" runat="server" Text="Label"></asp:Label>
    </td>
</tr>
<tr>
<td class="style1">Publisher</td>
<td>
    <asp:Label ID="lblPub" runat="server" Text="Label"></asp:Label>
    </td>
</tr>
<tr>
<td class="style1">Year Of publication</td>
<td>
    <asp:Label ID="lblYear" runat="server" Text="Label"></asp:Label>
    </td>
</tr>
<tr>
<td class="style1">Category</td>
<td>
    <asp:Label ID="lblCat" runat="server" Text="Label"></asp:Label>
    </td>
</tr>
    <tr>
        <td class="style1">
            Users Rates</td>
        <td>
            <asp:Image ID="imgRate" runat="server" Height="23px" Width="96px" />
        </td>
    </tr>
</table>
</td>
</tr>

</table>

<table width="100%">
<tr>
<td style="width:310px">
    Your Rate<asp:RadioButtonList ID="rbRate" runat="server" AutoPostBack="True" 
        onselectedindexchanged="rbRate_SelectedIndexChanged" 
        RepeatDirection="Horizontal">
        <asp:ListItem>1</asp:ListItem>
        <asp:ListItem>2</asp:ListItem>
        <asp:ListItem>3</asp:ListItem>
        <asp:ListItem>4</asp:ListItem>
        <asp:ListItem>5</asp:ListItem>
    </asp:RadioButtonList>
    <asp:Label ID="lblRate" runat="server" Text="Rate Submitted Successfully" 
        Visible="False"></asp:Label>
    </td>
    <td>
    <asp:Panel ID="pnlRec" runat="server" Visible="false">
        <asp:Label ID="lblRec" runat="server" Text="Do you find this book interesting?"></asp:Label>
        <asp:RadioButtonList ID="rbRec" runat="server" AutoPostBack="True" 
            RepeatDirection="Horizontal" 
            onselectedindexchanged="rbRec_SelectedIndexChanged">
            <asp:ListItem>Yes</asp:ListItem>
            <asp:ListItem>No</asp:ListItem>
        </asp:RadioButtonList>
    </asp:Panel>
    <asp:Label ID="lblFeed" runat="server" Text="Thank you for your feedback" 
            Visible="False"></asp:Label>
    </td>
</tr>
</table>
<table width="100%">
<tr>
<td style="width:310px; margin-left: 80px;">
    <hr />
    Description</td>
</tr>
<tr>
<td style="width:310px">
    <asp:Label ID="lblDesc" runat="server" Text="Label"></asp:Label>
    </td>
</tr>
    <tr>
        <td style="width:310px">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <img id="img1" alt="" src="images/ajax-loader(4).gif" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </td>
    </tr>
</table>
  </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
