<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<doCS.Web.Models.Type.DisplayOptions>" %>
<form action="#" >
	<fieldset>
		<legend>Display:</legend>
		<label for="ShowPublic">Public: </label>
		<input type="checkbox" name="ShowPublic" <%= Model.ShowPublic ? "checked='checked'" : "" %> value="true" />

		<label for="ShowProtected">Protected: </label>
		<input type="checkbox" name="ShowProtected" <%= Model.ShowProtected ? "checked='checked'" : "" %> value="true" />

		<label for="ShowPrivate">Private: </label>
		<input type="checkbox" name="ShowPrivate" <%= Model.ShowPrivate ? "checked='checked'" : "" %> value="true" />

		<label for="ShowInherited">Inherited: </label>
		<input type="checkbox" name="ShowInherited" <%= Model.ShowInherited ? "checked='checked'" : "" %> value="true" />

	</fieldset>
</form>

