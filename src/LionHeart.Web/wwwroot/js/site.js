function setupContentLoader(buttonSelector, contentSelector, url) {
	$(document).ready(function () {
		$(buttonSelector).click(function () {
            $(contentSelector).empty();
			$(contentSelector).load(url);
		});
	});
}