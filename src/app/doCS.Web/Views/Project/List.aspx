<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<ProjectListItem>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	All Projects
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>All Projects</h2>
		<%= Html.ActionLink("Add Project", "Edit") %>
		<ul>
			<% foreach (ProjectListItem project in Model) { %>
					<li><%: Html.ActionLink(project.ProjectName, "View", new { project = project.ProjectName } )%> (<%: Html.ActionLink("Edit", "Edit", new { Id = project.ProjectId }) %>)  (<%: Html.ActionLink("Extract", "Extract", new { Id = project.ProjectId } )%>)</li>
			<% } %>
		</ul>

</asp:Content>
