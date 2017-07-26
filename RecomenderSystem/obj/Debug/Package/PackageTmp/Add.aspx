<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="RecomenderSystem.Add" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<h3>Add New...</h3>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:RadioButtonList runat="server" ID="RB" 
            onselectedindexchanged="RB_SelectedIndexChanged" AutoPostBack="True">
        <asp:ListItem>Category</asp:ListItem>
        <asp:ListItem>Book</asp:ListItem>
        </asp:RadioButtonList>
        <hr />

        <asp:Panel ID="pnlCat" runat="server" Visible = "false">
        <table>
        <tr>
        <td> 
            <asp:Label ID="lblCatName" runat="server" Text="Name" Width="120px"></asp:Label></td>
        <td>
            <asp:TextBox ID="tbCatName" runat="server" Width="200px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="tbCatName" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td>
                    &nbsp;</td>
        </tr>
            <tr>
            <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnCatSave" runat="server" Text="Save" CssClass="btn" 
                        onclick="btnCatSave_Click" /></td>
            </tr>
        </table>
        </asp:Panel>


        <asp:Panel ID="pnlBook" runat="server" Visible="false">
               <table>
        <tr>
        <td> 
            <asp:Label ID="lblISBN" runat="server" Text="ISBN" Width="120px"></asp:Label></td>
        <td>
            <asp:TextBox ID="tbisbn" runat="server" Width="200px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="tbisbn" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td>
                    &nbsp;</td>
        </tr>

              <tr>
        <td> 
            <asp:Label ID="lblTitle" runat="server" Text="Title" Width="120px"></asp:Label></td>
        <td>
            <asp:TextBox ID="tbTitle" runat="server" Width="200px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ControlToValidate="tbTitle" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td>
                    &nbsp;</td>
        </tr>

              <tr>
        <td> 
            <asp:Label ID="lblAuthor" runat="server" Text="Author" Width="120px"></asp:Label></td>
        <td>
            <asp:TextBox ID="tbAuthor" runat="server" Width="200px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" ControlToValidate="tbAuthor" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td>
                    &nbsp;</td>
        </tr>

              <tr>
        <td> 
            <asp:Label ID="lblYear" runat="server" Text="Year Of Publication" Width="120px"></asp:Label></td>
        <td>
            <asp:TextBox ID="tbYear" runat="server" Width="200px"></asp:TextBox>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                     ControlToValidate="tbYear" ErrorMessage="*" ForeColor="Red" 
                                     ValidationExpression="^\d{1,10}$"></asp:RegularExpressionValidator>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" ControlToValidate="tbYear" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td>
                    &nbsp;</td>
        </tr>

              <tr>
        <td> 
            <asp:Label ID="lblPub" runat="server" Text="Publisher" Width="120px"></asp:Label></td>
        <td>
            <asp:TextBox ID="tbPub" runat="server" Width="200px"></asp:TextBox>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" ControlToValidate="tbPub" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <td>
                    &nbsp;</td>
        </tr>
              <tr>
        <td> 
            <asp:Label ID="lblDesc" runat="server" Text="Description" Width="120px"></asp:Label></td>
        <td>
            <asp:TextBox ID="tbDesc" runat="server" Width="200px" TextMode="MultiLine" 
                Height="51px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" ControlToValidate="tbDesc" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            <td>
                    &nbsp;</td>
        </tr>
            <tr>
            <td>
                <asp:Label ID="lblCat" runat="server" Text="Category" Width="120px"></asp:Label>
            </td>
                <td>
                    <asp:DropDownList ID="ddlCat" runat="server" Width="200px">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
                   <tr>
                       <td>
                           &nbsp;</td>
                       <td>
                           &nbsp;</td>
                       <td>
                           <asp:Button ID="btnBookSave" runat="server" CssClass="btn" 
                               onclick="btnBookSave_Click" Text="Save" />
                       </td>
                   </tr>
        </table>
        </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
