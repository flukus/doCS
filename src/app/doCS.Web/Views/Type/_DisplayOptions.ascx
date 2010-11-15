<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<doCS.Web.Models.Type.DisplayOptions>" %>
<form action="#" >
	<fieldset>
		<legend>Display:</legend>
		<%: Html.LabelFor(x=>x.ShowPublic) %>
		<%: Html.CheckBoxFor(x=>x.ShowPublic) %>
		<%: Html.Label("ShowProtected") %>
		<%: Html.CheckBoxFor(x=>x.ShowProtected) %>
		<%: Html.Label("ShowPrivate") %>
		<%: Html.CheckBoxFor(x=>x.ShowPrivate) %>
		<%: Html.Label("ShowInherited") %>
		<%: Html.CheckBoxFor(x=>x.ShowInherited) %>
	</fieldset>
</form>

