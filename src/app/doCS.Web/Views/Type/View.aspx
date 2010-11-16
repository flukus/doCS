<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Project.Master" Inherits="doCS.Web.ViewHelpers.ViewPage<TypeView>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%: Model.Name %>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<script type="text/javascript">
		$(document).ready(function () {
			$("#members").displayOptions({});
		});
	</script>

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

		<fieldset id="members" class="collapsable">
			<legend>Members</legend>
			<% Html.RenderPartial("_DisplayOptions", DisplayOptions); %>
			<ul>
				<% foreach (var property in Model.Properties) { %>
					<li class="<%: (property.GetAccessor == "unknown") ? property.SetAccessor : property.GetAccessor  %> <%: (property.DeclaringTypeId != Model.Id) ? "inherited" : "" %>" >
						<div class="quickInfo" >
							<%: (property.GetAccessor == "unknown") ? property.SetAccessor : property.GetAccessor %>
							<%: property.IsStatic ? "static" : "" %>
							<%: property.IsVirtual ? "virtual" : "" %>
							<%: property.IsAbstract ? "abstract" : "" %>
							<%: Html.ActionLink(property.TypeName, "View", new { id = property.TypeId }) %>
							<a href="#" ><%: property.Name %></a>
							<% if (property.IsProperty) { %>
								<% var setAccessor = (property.SetAccessor != property.GetAccessor && property.GetAccessor != "unknown") ? property.SetAccessor : ""; %>
								<% setAccessor = setAccessor == "unknown" ? "" : setAccessor + " set; "; %>
								<%: string.Format("{{ {0} {1} }}", (property.GetAccessor != "unknown") ? "get;" : "", setAccessor) %>
							<% } %>
						</div>
						<div class="extendedInfo" >
						</div>
						<div class="documentation" >
						</div>
					</li>
				<% } %>
			</ul>
		</fieldset>

		<fieldset>
			<legend>Methods</legend>
			<!-- extension methods should be shown here as well -->
			<ul>
				<% foreach (var member in Model.Methods) { %>
					<li></li>
				<% } %>
			</ul>
		</fieldset>

</asp:Content>
