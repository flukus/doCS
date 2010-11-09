<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Project.Master" Inherits="doCS.Web.ViewHelpers.ViewPage<IList<NamespaceListItem>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>List</h2>

		<ul>
			<% foreach (var ns in Model) { %>
				<li><%: Html.ActionLink(ns.Name, "View", new { Id = ns.Id }) %></li>
			<% } %>
		</ul>

</asp:Content>
