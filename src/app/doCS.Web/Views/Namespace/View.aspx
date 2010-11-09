<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Project.Master" Inherits="System.Web.Mvc.ViewPage<NamespaceView>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	View
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%: Model.Name  %></h2>

		<h3>Types</h3>
		<ul>
			<% foreach (var type in Model.Types) { %>
				<li><%: Html.ActionLink(type.Name, "View", "Type", new { Id = type.Id }, new { })%></li>
			<% } %>
		</ul>

</asp:Content>
