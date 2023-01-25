(function () {
    var MorphNav = function (element) {
        this.element = element;
        this.menuToggle = this.element.getElementsByClassName('js-morph-nav__sm-menu-toggle');
        this.nav = this.element.getElementsByClassName('js-morph-nav__sm-dropdown');
        this.morphDropown = this.element.getElementsByClassName('js-morph-nav__lg-subnav');
        this.morphDpWrapper = this.element.getElementsByClassName('js-morph-nav__lg-dropdown-wrapper');
        this.morphBg = this.element.getElementsByClassName('js-morph-nav__lg-dropdown-bg');
        this.navItems = this.element.getElementsByClassName('js-morph-nav__lg-nav-item');
        this.navDropdowns = this.element.getElementsByClassName('js-morph-nav__lg-dropdown-item');
        if (this.nav.length < 1 || this.menuToggle.length < 1 || this.morphBg.length < 1) return false;

        // variables to navigate dropdown with keyboard
        this.navFirstFocusable = navFirstFocusable(this);
        this.indexKeyDropdown = -1;
        this.dropdownFocusableItems = [];

        initMorphNav(this);
    };

    function navFirstFocusable(obj) {
        var focusableEle = obj.nav[0].querySelectorAll('[href], input:not([disabled]), select:not([disabled]), textarea:not([disabled]), button:not([disabled]), iframe, object, embed, [tabindex]:not([tabindex="-1"]), [contenteditable], audio[controls], video[controls], summary'),
            firstFocusable = false;

        for (var i = 0; i < focusableEle.length; i++) {
            if (focusableEle[i].offsetWidth || focusableEle[i].offsetHeight || focusableEle[i].getClientRects().length) {
                firstFocusable = focusableEle[i];
                break;
            }
        }

        return firstFocusable;
    };

    function initMorphNav(obj) {
        // get focusable items in dropdown
        initDrobdownFocusable(obj);

        // on small devices - show navigation when clicking on menu toggle button
        obj.menuToggle[0].addEventListener('click', function (event) {
            toggleMenuNavigation(obj);
        });

        // init key events
        initKeyEvents(obj);

        // on bigger devices - show morphing dropdown on hover/click
        for (var i = 0; i < obj.navItems.length; i++) {
            (function (i) {
                detectHover(obj, i);
                detectClick(obj, i);
            })(i);
        }
    };

    function toggleMenuNavigation(obj) { // toggle menu visibility on small devices
        var expandedStatus = obj.nav[0].classList.toggle('morph-nav__sm-dropdown--is-visible');
        obj.menuToggle[0].setAttribute('aria-expanded', expandedStatus);
        if (expandedStatus && obj.navFirstFocusable) obj.navFirstFocusable.focus(); // move focus to first focusable element
    };

    function detectHover(obj, index) {
        obj.navItems[index].addEventListener('mouseenter', function (event) {
            showMorphBg(obj, index);
        });

        obj.navItems[index].addEventListener('mouseleave', function (event) {
            setTimeout(function () {
                if (isDropdownHovered(obj, index)) return; // the mouse moved to the dropdown
                isDropdownHovered(obj) ? hideDropdown(obj, index) : hideMorphBg(obj, index);
            }, 50);
        });

        obj.navDropdowns[index].addEventListener('mouseleave', function (event) {
            setTimeout(function () {
                if (isDropdownHovered(obj, index)) return; // the mouse moved to the dropdown
                isDropdownHovered(obj) ? hideDropdown(obj, index) : hideMorphBg(obj, index);
            }, 50);
        });
    };

    function detectClick(obj, index) {
        obj.navItems[index].addEventListener('click', function (event) {
            if (!obj.navDropdowns[index].classList.contains('morph-nav__lg-dropdown-item--is-visible')) {
                showMorphBg(obj, index, function () {
                    if (obj.dropdownFocusableItems[index].length > 0) obj.dropdownFocusableItems[index][0].focus();
                });
            } else {
                hideMorphBg(obj, index);
            }
        });
    };

    function isDropdownHovered(obj, index) {
        var hovered = false;
        if (typeof index !== 'undefined') {
            // check if a specific dropdown is hovered
            if (obj.navItems[index].matches(':hover') || obj.navDropdowns[index].matches(':hover')) return true;
            return false;
        }
        for (var i = 0; i < obj.navItems.length; i++) {
            // check if one of the dropdowns is hovered
            if (obj.navItems[i].matches(':hover') || obj.navDropdowns[i].matches(':hover')) {
                hovered = true;
                break;
            }
        }
        return hovered;
    };

    function showMorphBg(obj, index, cb) {
        var timeout = 0,
            duration = 300;
        if (!obj.morphBg[0].classList.contains('morph-nav__lg-dropdown-bg--is-visible')) {
            timeout = 50;
            duration = 0;
        }
        resetBgStyle(obj, index, duration); // set dropdown position/size

        // add visibility classes
        setTimeout(function () {
            obj.morphDropown[0].classList.add('morph-nav__lg-subnav--is-visible');
            obj.morphBg[0].classList.add('morph-nav__lg-dropdown-bg--is-visible');
            obj.navDropdowns[index].classList.add('morph-nav__lg-dropdown-item--is-visible');
            setAriaAttribute(obj, index, 'true');
            if (cb) cb();
        }, timeout);
    };

    function hideMorphBg(obj, index) {
        obj.morphDropown[0].classList.remove('morph-nav__lg-subnav--is-visible');
        obj.morphBg[0].classList.remove('morph-nav__lg-dropdown-bg--is-visible');
        obj.navDropdowns[index].classList.remove('morph-nav__lg-dropdown-item--is-visible');
        setAriaAttribute(obj, index, 'false');
    };

    function hideDropdown(obj, index) {
        obj.navDropdowns[index].classList.remove('morph-nav__lg-dropdown-item--is-visible');
        setAriaAttribute(obj, index, 'false');
    };

    function resetBgStyle(obj, index, duration) {
        var height = 0, width = 0, left = 0, top = 0;
        if (obj.navItems[index]) {
            var boundingRectItem = obj.navItems[index].getBoundingClientRect(),
                left = boundingRectItem.left,
                itemWidth = boundingRectItem.width;
        }

        if (obj.navDropdowns[index]) {
            var boundingRectDrop = obj.navDropdowns[index].getBoundingClientRect(),
                height = boundingRectDrop.height,
                width = boundingRectDrop.width;
        }

        animateElement(obj, height, width, left - width / 2 + itemWidth / 2, duration, 'easeOutQuart');
    };

    function setAriaAttribute(obj, index, value) {
        var controller = obj.navItems[index].querySelector('button');
        if (controller) controller.setAttribute('aria-exanded', value);
    };

    function initKeyEvents(obj) {
        // key navigation - small devices
        window.addEventListener('keyup', function (event) {
            // listen for esc key - close navigation if open
            if (event.key && event.key.toLowerCase() == 'escape' && !shouldShowMorphBg(obj)) {
                if (obj.menuToggle[0].getAttribute('aria-expanded') == 'true' && document.activeElement.closest('.js-morph-nav')) {
                    obj.menuToggle[0].click();
                    obj.menuToggle[0].focus();
                }
            }
            // listen for tab key - close navigation when it loses focus 
            if (event.key && event.key.toLowerCase() == 'tab' && !shouldShowMorphBg(obj)) {
                if (obj.menuToggle[0].getAttribute('aria-expanded') == 'true' && !document.activeElement.closest('.js-morph-nav')) obj.menuToggle[0].click();
            }
        });

        // key navigation - bigger devices
        window.addEventListener('keydown', function (event) {
            if (event.key && event.key.toLowerCase() == 'tab' && shouldShowMorphBg(obj)) {
                obj.indexKeyDropdown = getFocusedDropdown(obj);
                var direction = event.shiftKey ? 'prev' : 'next';
                if (obj.indexKeyDropdown > -1) navigateDropdown(obj, direction, event);
            }
        });
    };

    function getFocusedDropdown(obj) {
        var index = -1;
        // return the index of the dropdown in focus (if any)
        for (var i = 0; i < obj.navDropdowns.length; i++) {
            if (obj.navItems[i].matches(':focus-within') || obj.navDropdowns[i].matches(':focus-within')) {
                index = i;
                break;
            }
        }
        return index;
    };

    function navigateDropdown(obj, direction, event) {
        if (obj.dropdownFocusableItems[obj.indexKeyDropdown][0] == document.activeElement && direction == 'prev') {
            // first focusable item in dropdown is in focus -> move focus to trigger
            moveFocusToTrigger(obj, event, obj.indexKeyDropdown);
        }
        if (obj.dropdownFocusableItems[obj.indexKeyDropdown][obj.dropdownFocusableItems[obj.indexKeyDropdown].length - 1] == document.activeElement && direction == 'next') {
            // last focusable item in dropdown is in focus -> move focus to trigger
            moveFocusToTrigger(obj, event, obj.indexKeyDropdown);
        }
    };

    function moveFocusToTrigger(obj, event, index) {
        event.preventDefault();
        var focusable = obj.navItems[index].querySelector('button, a');
        if (focusable) focusable.focus();
        hideMorphBg(obj, index);
    };

    function initDrobdownFocusable(obj) {
        for (var i = 0; i < obj.navDropdowns.length; i++) {
            obj.dropdownFocusableItems[i] = obj.navDropdowns[i].querySelectorAll(focusableElString);
        }
    };

    function shouldShowMorphBg(obj) {
        // true on bigger devices
        return !(obj.menuToggle[0].offsetWidth || obj.menuToggle[0].offsetHeight || obj.menuToggle[0].getClientRects().length);
    };

    function animateElement(obj, toH, toW, toT, duration) {
        // loop function to animate height/width/transform
        if (duration == 0) {
            obj.morphDpWrapper[0].style.height = toH + "px";
            obj.morphDpWrapper[0].style.width = toW + "px";
            obj.morphDpWrapper[0].style.transform = 'translateX(' + toT + 'px)';
            return;
        }
        var startW = obj.morphDpWrapper[0].offsetWidth,
            startH = obj.morphDpWrapper[0].offsetHeight,
            startT = getTranslateX();

        var currentTime = null;

        var animateLoop = function (timestamp) {
            if (!currentTime) currentTime = timestamp;
            var progress = timestamp - currentTime;
            if (progress > duration) progress = duration;
            // get new style values
            var valH = Math.easeOutQuart(progress, startH, toH - startH, duration);
            var valW = Math.easeOutQuart(progress, startW, toW - startW, duration);
            var valT = Math.easeOutQuart(progress, startT, toT - startT, duration);

            obj.morphDpWrapper[0].style.height = valH + "px";
            obj.morphDpWrapper[0].style.width = valW + "px";
            obj.morphDpWrapper[0].style.transform = 'translateX(' + valT + 'px)';
            if (progress < duration) {
                window.requestAnimationFrame(animateLoop);
            }
        };

        function getTranslateX() {
            var style = window.getComputedStyle(obj.morphDpWrapper[0]);
            var matrix = new WebKitCSSMatrix(style.transform);
            return matrix.m41;
        }

        obj.morphDpWrapper[0].style.height = startH + "px";
        obj.morphDpWrapper[0].style.width = startW + "px";
        window.requestAnimationFrame(animateLoop);
    };

    var focusableElString = '[href], input:not([disabled]), select:not([disabled]), textarea:not([disabled]), button:not([disabled]), iframe, object, embed, [tabindex]:not([tabindex="-1"]), [contenteditable], audio[controls], video[controls], summary';

    window.MorphNav = MorphNav;

    var morphNav = document.getElementsByClassName('js-morph-nav');
    if (morphNav.length > 0) {
        for (var i = 0; i < morphNav.length; i++) {
            (function (i) { new MorphNav(morphNav[i]); })(i);
        }
    }
}());