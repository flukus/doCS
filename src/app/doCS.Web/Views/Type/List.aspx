<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Project.Master" Inherits="doCS.Web.ViewHelpers.ViewPage<List<TypeListItem>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>List</h2>

		<ul>
			<% foreach (var type in Model) { %>
				<li><%: Html.ActionLink(type.Name, "View", "Type", new { id = type.Id }, null)%> (<%: Html.ActionLink(type.NamespaceName, "View", "Namespace", new { id = type.NamespaceId }, null)%>)</li>
			<% } %>
		</ul>

</asp:Content>
