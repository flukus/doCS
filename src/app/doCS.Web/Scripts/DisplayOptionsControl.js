(function ($) {
	//$.vari = "$.vari";
	//$.fn.vari = "$.fn.vari";

	// $.fn is the object we add our custom function
	$.fn.displayOptions = function (options) {
		var defaults = {};
		$this = $(this[0]);
		var options = $.extend(defaults, options);
		update();

		$("input", $this).click(function (eventObj) {
			update();
		});
	};

	$(document).ready(function () {
		$("fieldset.collapsable").each(function (index, item) {
			//update(item);
		});
		$("fieldset.collapsable input").change(function (eventObj) {
			$("fieldset.collapsable").each(function (index, item) {
				//update(item);
			});
		});
	});

	function update() {
		var form = $("form", $this);
		var options = {};
		$(form.serializeArray()).each(function (i, n) {
			options[n.name] = (n.value == "true") ? true : false;
		});
		$("ul li", $this).each(function (index, element) {
			var showItem = true;
			element = $(element);
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