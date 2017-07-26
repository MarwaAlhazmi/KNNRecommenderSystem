<%@ Page Title="" Language="C#" MasterPageFile="~/Second.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="RecomenderSystem.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    
<table width="100%">
<tr>
<td></td>
<td>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
    <ProgressTemplate>
                <img id="img1" alt="" src="images/ajax-loader(4).gif" style="float:right" />
    </ProgressTemplate>
    </asp:UpdateProgress>
</td>
</tr>
<tr>
<td><h5>Search</h5></td>
<td></td>
</tr>
<tr>
<td>
    <asp:TextBox ID="tbKeyword" runat="server" Width="200px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="tbKeyword" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
    <asp:Label ID="lblError" runat="server" ForeColor="Red" Text="Numbers only" 
        Visible="False"></asp:Label>
    </td>
<td>
    <asp:Button ID="btnSearch" runat="server" CssClass="btn" 
        onclick="btnSearch_Click" Text="Search" />
</td>
</tr>
<tr>
<td>
<p>Search By:</p>
    <asp:RadioButtonList ID="rbType" runat="server">
        <asp:ListItem Selected="True" Value="0">Keyword</asp:ListItem>
        <asp:ListItem Value="1">Author</asp:ListItem>
        <asp:ListItem Value="2">Puplisher</asp:ListItem>
        <asp:ListItem Value="3">Year of puplication</asp:ListItem>
    </asp:RadioButtonList>
    </td>
<td>
    &nbsp;</td>
</tr>
</table>
<hr />
<asp:Panel ID="pnlResult" runat="server">
<asp:ListView ID="lvknn" runat="server" DataKeyNames="ISBN" 
            GroupItemCount="3">
            
            <EmptyDataTemplate>
                <table id="Table1" runat="server" style="">
                    <tr>
                        <td>
                            No data was returned.</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <EmptyItemTemplate>
                <td id="Td1" runat="server" />
            </EmptyItemTemplate>
            <GroupTemplate>
                <tr ID="itemPlaceholderContainer" runat="server">
                    <td ID="itemPlaceholder" runat="server">
                    </td>
                </tr>
            </GroupTemplate>
            
            <ItemTemplate>
                <td id="Td2" runat="server" style="width:33%; vertical-align: top;">
                <a href="bookDetails.aspx?isbn=<%# Eval("ISBN") %>">
                <img alt="" src="images/<%# Eval("ISBN") %>.jpg" width="103" height="135" style="border: thin solid #C0C0C0" /></a>
                    
                    <br />
                    <a href="bookDetails.aspx?isbn=<%# Eval("ISBN") %>"><%# Eval("Title") %></a>
                    
                  
                    <div><%# Eval("Auther") %></div>
                    
                  
                    <div><%# Eval("Year") %></div>
                    
                  
                    <div><%# Eval("Publisher") %></div>
                    
                    <br />
                </td>
            </ItemTemplate>
            <LayoutTemplate>
                <table id="Table2" runat="server">
                    <tr id="Tr1" runat="server">
                        <td id="Td3" runat="server">
                            <table ID="groupPlaceholderContainer" runat="server" border="0" style="">
                                <tr ID="groupPlaceholder" runat="server">
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="Tr2" runat="server">
                        <td id="Td4" runat="server" style="">
                            <asp:DataPager ID="DataPager1" runat="server" PageSize="12" OnPreRender="DataPager1_PreRender">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" 
                                        ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                    <asp:NumericPagerField />
                                    <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" 
                                        ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                </Fields>
                            </asp:DataPager>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            
        </asp:ListView>
</asp:Panel>
</ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
