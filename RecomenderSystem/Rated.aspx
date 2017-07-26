<%@ Page Title="" Language="C#" MasterPageFile="~/Second.Master" AutoEventWireup="true" CodeBehind="Rated.aspx.cs" Inherits="RecomenderSystem.Rated" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h5>Rated books</h5>
<asp:Panel ID="pnlRated" runat="server" BorderStyle="Solid" BorderColor="Silver">
        <asp:ListView ID="lvRated" runat="server" DataKeyNames="ISBN" 
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
</asp:Content>
