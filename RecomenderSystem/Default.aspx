<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Second.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="RecomenderSystem._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    </asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

    <asp:Panel ID="pnlNew" runat="server" BorderStyle="Solid" BorderColor="Silver" 
        Visible="False">
<h5>Recommended books: New User</h5>
        <asp:ListView ID="lvNew" runat="server" DataKeyNames="ISBN" 
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
                <a href="bookDetails.aspx?rec=1&isbn=<%# Eval("ISBN") %>">
                <img alt="" src="images/<%# Eval("ISBN") %>.jpg" width="103" height="135" style="border: thin solid #C0C0C0" /></a>
                    
                    <br />
                    <a href="bookDetails.aspx?Rec=1&isbn=<%# Eval("ISBN") %>"><%# Eval("Title") %></a>
                    
                  
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


        
<asp:Panel ID="pnlknn" runat="server" BorderStyle="Solid" BorderColor="Silver" 
        Visible="False">
<h5>Collaborative Filtering using KNN</h5>
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
                <a href="bookDetails.aspx?Rec=knn&isbn=<%# Eval("ISBN") %>">
                <img alt="" src="images/<%# Eval("ISBN") %>.jpg" width="103" height="135" style="border: thin solid #C0C0C0;" /></a>
                    
                    <br />
                    <a href="bookDetails.aspx?Rec=1&isbn=<%# Eval("ISBN") %>"><%# Eval("Title") %></a>
                    
                  
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
<br />

<asp:Panel ID="pnlvsm" runat="server" BorderStyle="Solid" BorderColor="Silver" 
        Visible="False">
<h5>Content based using VSM</h5>
        <asp:ListView ID="lvvsm" runat="server" DataKeyNames="ISBN" 
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
                <a href="bookDetails.aspx?Rec=vsm&isbn=<%# Eval("ISBN") %>">
                <img alt="" src="images/<%# Eval("ISBN") %>.jpg" width="103" height="135" style="border: thin solid #C0C0C0; vertical-align: top;" /></a>
                    
                    <br />
                    <a href="bookDetails.aspx?Rec=1&isbn=<%# Eval("ISBN") %>"><%# Eval("Title") %></a>
                    
                  
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
