<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucReportViewer.ascx.cs" Inherits="MVC_CRUD_Application_Demo.Report.ucReportViewer" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<div style="width: 921px">
    <asp:ScriptManager ID="ScriptManagesdsr1" runat="server"></asp:ScriptManager>
    <rsweb:ReportViewer ID="rvReportView" runat="server" Height="640px" Width="920px" AsyncRendering="true" PageCountMode="Actual" ShowToolBar="true">
        <LocalReport EnableExternalImages="True">
        </LocalReport>
    </rsweb:ReportViewer>
</div>
