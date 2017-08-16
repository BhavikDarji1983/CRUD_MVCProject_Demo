<%@ Page Title="" Language="C#" MasterPageFile="~/Report/Report.Master" AutoEventWireup="true"
    CodeBehind="EmployeeList.aspx.cs" Inherits="MVC_CRUD_Application_Demo.Report.EmployeeList" %>

<%@ Register Src="ucReportViewer.ascx" TagName="ucReportViewer" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ucReportViewer ID="ucrvBondList" runat="server" />
     
</asp:Content>
