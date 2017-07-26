<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Register.aspx.cs" Inherits="RecomenderSystem.Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:CreateUserWizard ID="RegisterUser" runat="server" EnableViewState="false" 
    OnCreatedUser="RegisterUser_CreatedUser" 
    ContinueDestinationPageUrl="~/Default.aspx" 
        oncreatinguser="RegisterUser_CreatingUser">
        <LayoutTemplate>
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <WizardSteps>
            <asp:CreateUserWizardStep ID="RegisterUserWizardStep" runat="server">
                <ContentTemplate>
                    <h2>
                        Create a New Account
                    </h2>
                    <p>
                        Use the form below to create a new account.
                    </p>
                    <p>
                        Passwords are required to be a minimum of <%= Membership.MinRequiredPasswordLength %> characters in length.
                    </p>
                    <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="RegisterUserValidationGroup"/>
                    <div class="accountInfo">
                        <fieldset class="register">
                            <legend>Account Information</legend>
                            <p>
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName" 
                                    Width="200px">User Name:</asp:Label><br />
                                <asp:TextBox ID="UserName" runat="server" CssClass="textEntry" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                                     CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required." 
                                     ValidationGroup="RegisterUserValidationGroup" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email" 
                                    Width="200px">E-mail:</asp:Label><br />
                                <asp:TextBox ID="Email" runat="server" CssClass="textEntry" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" 
                                     CssClass="failureNotification" ErrorMessage="E-mail is required." ToolTip="E-mail is required." 
                                     ValidationGroup="RegisterUserValidationGroup" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password" 
                                    Width="200px">Password:</asp:Label><br />
                                <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" 
                                    TextMode="Password" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                                     CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required." 
                                     ValidationGroup="RegisterUserValidationGroup" ForeColor="Red">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" 
                                    AssociatedControlID="ConfirmPassword" Width="200px">Confirm Password:</asp:Label><br />
                                <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="passwordEntry" 
                                    TextMode="Password" Width="200px"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" 
                                    CssClass="failureNotification" Display="Dynamic" 
                                     ErrorMessage="Confirm Password is required." ID="ConfirmPasswordRequired" runat="server" 
                                     ToolTip="Confirm Password is required." 
                                    ValidationGroup="RegisterUserValidationGroup" ForeColor="Red">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="PasswordCompare" runat="server" 
                                    ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                                     CssClass="failureNotification" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match."
                                     ValidationGroup="RegisterUserValidationGroup" ForeColor="Red">*</asp:CompareValidator>
                            </p>
                            <hr />
                             <p>
                                <asp:Label ID="lblName" runat="server" AssociatedControlID="tbName" 
                                     Width="200px">Name:</asp:Label><br />
                                <asp:TextBox ID="tbName" runat="server" CssClass="textEntry" Width="200px"></asp:TextBox>
                             
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                     ControlToValidate="tbName" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                             
                            </p>
                             <p>
                                <asp:Label ID="lblLocation" runat="server" AssociatedControlID="tbLocation" 
                                     Width="200px">Location:</asp:Label><br />
                                <asp:TextBox ID="tbLocation" runat="server" CssClass="textEntry" Width="200px"></asp:TextBox>
                              
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                     ControlToValidate="tbLocation" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                              
                            </p>
                             <p>
                                <asp:Label ID="lblAge" runat="server" AssociatedControlID="tbAge" Width="200px">Age:</asp:Label><br />
                                <asp:TextBox ID="tbAge" runat="server" CssClass="textEntry" Width="200px"></asp:TextBox>
                              
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                     ControlToValidate="tbAge" ErrorMessage="*" ForeColor="Red" 
                                     ValidationExpression="^\d{1,10}$"></asp:RegularExpressionValidator>
                              
                            </p>
                             <p>
                                <asp:Label ID="lblCat" runat="server" AssociatedControlID="ddlCat" Width="200px">Preferred Category:</asp:Label><br />
                                 <asp:DropDownList ID="ddlCat" runat="server" Width="200px" 
                                     DataSourceID="EntityDataSource1" DataTextField="Name" DataValueField="ID">
                                 </asp:DropDownList>
                                
                              
                            </p>
                        </fieldset>
                        <p class="submitButton">
                            <asp:Button ID="CreateUserButton" runat="server" CommandName="MoveNext" Text="Create User" 
                                 ValidationGroup="RegisterUserValidationGroup" CssClass="btn" 
                                onclick="CreateUserButton_Click"/>
                        </p>
                    </div>
                </ContentTemplate>
                <CustomNavigationTemplate>
                </CustomNavigationTemplate>
            </asp:CreateUserWizardStep>
<asp:CompleteWizardStep runat="server"></asp:CompleteWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
    <asp:EntityDataSource ID="EntityDataSource1" runat="server" 
        ConnectionString="name=BooksEntities" DefaultContainerName="BooksEntities" 
        EnableFlattening="False" EntitySetName="Categories">
    </asp:EntityDataSource>
</asp:Content>
