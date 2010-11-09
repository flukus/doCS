<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ProjectEdit>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

		<% using (var form = Html.BeginForm("Edit", "Project", FormMethod.Post)) { %>
			<%: Html.ValidationSummary() %>
			<fieldset>
				<legend>Edit</legend>

				<%: Html.HiddenFor(model => model.Id) %>

				<div class="editor-label">
					<%: Html.LabelFor(model => model.Name) %>
				</div>
				<div class="editor-field">
					<%: Html.TextBoxFor(model => model.Name) %>
					<%: Html.ValidationMessageFor(model => model.Name) %>
				</div>

				<div class="editor-label">
					<%: Html.LabelFor(model => model.IncludedAssemblies) %>
				</div>
				<div class="editor-field">
					<%: Html.TextAreaFor(model => model.IncludedAssemblies) %>
					<%: Html.ValidationMessageFor(model => model.IncludedAssemblies) %>
				</div>

				<div class="editor-label">
					<%: Html.LabelFor(model => model.IncludedXmlFiles) %>
				</div>
				<div class="editor-field">
					<%: Html.TextAreaFor(model => model.IncludedXmlFiles) %>
					<%: Html.ValidationMessageFor(model => model.IncludedXmlFiles) %>
				</div>

				<input type="submit" />

			</fieldset>
		<% } %>

</asp:Content>
