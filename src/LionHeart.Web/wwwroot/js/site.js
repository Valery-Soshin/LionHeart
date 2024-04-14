function setupContentLoader(buttonSelector, contentSelector, url) {
	$(document).ready(function () {
		$(buttonSelector).click(function () {
			$(contentSelector).empty();
			jQuery('html,body').animate({ scrollTop: 100 }, 0);
			$(contentSelector).load(url);
			
		});
	});
}