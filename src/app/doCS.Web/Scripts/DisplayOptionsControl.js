(function ($) {
	//$.vari = "$.vari";
	//$.fn.vari = "$.fn.vari";

	// $.fn is the object we add our custom function
	$.fn.displayOptions = function (options) {
		var defaults = {};
		$this = $(this[0]);
		var options = $.extend(defaults, options);
		update();

		$(".extendedInfo", $this).hide();
		$(".documentation", $this).hide();
		$(".quickInfo", $this).click(function (event) {
			var target = $(event.target);
			var li = target.parent("li");
			toggleDisplay($(li));
		});
		$("input", $this).click(function (eventObj) {
			update();
		});
	};

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

	function toggleDisplay(element) {
		if ($this.currentDisplay) {
			$(".extendedInfo", $this.currentDisplay).hide();
			$(".documentation", $this.currentDisplay).hide();
			if ($this.currentDisplay[0] == element[0]) {
				$this.currentDisplay = null;
				return;
			}
		}
		$(".extendedInfo", element).show("slide");
		$(".documentation", element).show("slide");
		$this.currentDisplay = element;
	}

	function showOption() {
	}

})(jQuery);