<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.min.js"></script>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Deposit.aspx.cs" Inherits="Deposit.Webforms.Deposit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Deposit</title>
    <style type="text/css">
        .auto-style1 {
            width: 157px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblPrincipal" runat="server" Text="Principal"/>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPrincipal" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RangeValidator ID="txtPrincipalRangeValidator" runat="server" MinimumValue="3000000" MaximumValue="5000000" ControlToValidate="txtPrincipal" ErrorMessage="Principal amount should be between 3,000,000 and 5,000,000" ForeColor="Red"></asp:RangeValidator>
                        <asp:RegularExpressionValidator ID="txtPrincipalValidator" runat="server" ControlToValidate="txtPrincipal" ErrorMessage="Please enter the numeric value." ForeColor="Red" ValidationExpression="([0-9])[0-9]*[.]?[0-9]*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblStartDate" runat="server" Text="Start Date"/>
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date"  TabIndex="1" ></asp:TextBox>
                    </td>
                    <td><asp:RequiredFieldValidator ID="txtStartDateValidator" runat="server" ControlToValidate="txtStartDate" ErrorMessage="please select start date." Display="Dynamic" ForeColor="red"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblEndDate" runat="server" Text="End Date"/>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" TabIndex="2" ></asp:TextBox>
                    </td>
                    <td><asp:RequiredFieldValidator ID="txtEndDateValidator" runat="server" ControlToValidate="txtEndDate" ErrorMessage="please select end date." Display="Dynamic" ForeColor="red"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblInterestRate" runat="server" Text="Interest Rate"/>
                    </td>
                    <td>
                        <asp:TextBox ID="txtInterestRate" runat="server" TabIndex="3" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="txtInterestRateValidator" runat="server" ControlToValidate="txtInterestRate" ErrorMessage="Please enter the numeric value." ForeColor="Red" ValidationExpression="([0-9])[0-9]*[.]?[0-9]*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblterm" runat="server" Text="Term (In Years)"/>
                    </td>
                    <td>
                        <asp:TextBox ID="txtTerm" runat="server" TabIndex="4" ></asp:TextBox>
                    </td>
                    <td>
                        <asp:RegularExpressionValidator ID="txtTermValidator" runat="server" ControlToValidate="txtTerm" ErrorMessage="Please enter the valid value." ForeColor="Red" ValidationExpression="([0-9])[0-9]*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblMaturityAmount" runat="server" Text="Maturity Amount"/>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMaturityAmount" runat="server" ReadOnly="true" >0.00</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lbltotalMaturityAmount" runat="server" Text="Total Maturity Amount"/>
                    </td>
                    <td>
                        <asp:TextBox ID="txttotalMaturityAmount" runat="server" ReadOnly="true" >0.00</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblOptions" runat="server" Text="Select Option"/>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlOptions" runat="server" Height="17px" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged" Width="133px" TabIndex="5">
                            <asp:ListItem Selected="True">Hold</asp:ListItem>
                            <asp:ListItem>Buy</asp:ListItem>
                            <asp:ListItem>Sell</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
