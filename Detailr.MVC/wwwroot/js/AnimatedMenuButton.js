if (!Util) function Util() { };

Util.hasClass = function (el, className) {
	return el.classList.contains(className);
};

Util.addClass = function (el, className) {
	var classList = className.split(' ');
	el.classList.add(classList[0]);
	if (classList.length > 1) Util.addClass(el, classList.slice(1).join(' '));
};

Util.removeClass = function (el, className) {
	var classList = className.split(' ');
	el.classList.remove(classList[0]);
	if (classList.length > 1) Util.removeClass(el, classList.slice(1).join(' '));
};

Util.toggleClass = function (el, className, bool) {
	if (bool) Util.addClass(el, className);
	else Util.removeClass(el, className);
};

(function () {
	var menuBtns = document.getElementsByClassName('js-anim-menu-btn');
	if (menuBtns.length > 0) {
		for (var i = 0; i < menuBtns.length; i++) {
			(function (i) {
				initMenuBtn(menuBtns[i]);
			})(i);
		}

		function initMenuBtn(btn) {
			btn.addEventListener('click', function (event) {
				event.preventDefault();
				var status = !Util.hasClass(btn, 'anim-menu-btn--state-b');
				Util.toggleClass(btn, 'anim-menu-btn--state-b', status);
				// emit custom event
				var event = new CustomEvent('anim-menu-btn-clicked', { detail: status });
				btn.dispatchEvent(event);
			});
		};
	}
}());