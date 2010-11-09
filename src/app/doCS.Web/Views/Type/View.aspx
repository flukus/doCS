<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Project.Master" Inherits="doCS.Web.ViewHelpers.ViewPage<TypeView>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: Model.Name %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>public class <%: Model.Name %></h1>
		<h3>Namespace: <%: Model.NamespaceName %></h3>
		<h3>Assembly: <%: Model.AssemblyName %></h3>

		<!-- This should be an svg image -->
		<h3>Inheritence Heirarchy</h3>
		<ul>
				<li>
					<ul>
						<% foreach (var interfac in Model.Interfaces) { %>
							<li><%: Html.ActionLink(interfac.Name, "View", new { id = interfac.Id })%></li>
						<% } %>
					</ul>
				</li>
			<% foreach (var baseClass in Model.BaseClasses) { %>
				<li>
					<ul>
						<li><%: Html.ActionLink(baseClass.Name, "View", new { id = baseClass.Id }) %></li>
						<% foreach (var interfac in baseClass.Interfaces) { %>
							<li><%: Html.ActionLink(interfac.Name, "View", new { id = interfac.Id }) %></li>
						<% } %>
					</ul>
				</li>
			<% } %>
		</ul>

		<p>
			<% if (XmlDocumentationHelper.HasSummary(Model.XmlDocumentation)) { %>
				<%: XmlDocumentationHelper.GetSummary(Model.XmlDocumentation) %>
			<% } %>
		</p>

		<h3>Members</h3>
		<ul>
			<% foreach (var member in Model.Members) { %>
				<li></li>
			<% } %>
		</ul>

		<h3>Properties</h3>
		<ul>
			<% foreach (var member in Model.Properties) { %>
				<li></li>
			<% } %>
		</ul>

		<h3>Methods</h3>
		<ul>
			<% foreach (var member in Model.Methods) { %>
				<li></li>
			<% } %>
		</ul>

</asp:Content>
