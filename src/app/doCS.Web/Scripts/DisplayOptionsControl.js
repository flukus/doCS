(function ($) {
	//$.vari = "$.vari";
	//$.fn.vari = "$.fn.vari";

	// $.fn is the object we add our custom function
	//$.fn.DoSomethingLocal = function()
	//{
	//return this.each(function()
	//{
	//alert(this.vari);    // would output `undefined`
	//alert($(this).vari); // would output `$.fn.vari`
	//});
	//};

	$(document).ready(function () {
		$("div.collapsable").each(function (index, item) {
			update(item);
		});
		$("div.collapsable input").change(function (eventObj) {
			$("div.collapsable").each(function (index, item) {
				update(item);
			});
		});
	});

	function update(display) {
		var form = $("form", display);
		var options = {};
		$(form.serializeArray()).each(function (i, n) {
			options[n.name] = (n.value == "true") ? true : false;
		});
		$("ul li", display).each(function (index, element) {
			element = $(element);
			var showItem = true;
			if (!options.ShowPublic && element.hasClass("public")) {
				showItem = false;
			}
			if (!options.ShowProtected && element.hasClass("protected")) {
				showItem = false;
			}
			if (!options.ShowPrivate && element.hasClass("private")) {
				showItem = false;
			}
			if (!options.ShowInherited && element.hasClass("inherited")) {
				showItem = false;
			}
			if (showItem) {
				element.show("slide");
			} else {
				element.hide("slide");
			}
		});
	}

	function showOption() {
	}

})(jQuery);