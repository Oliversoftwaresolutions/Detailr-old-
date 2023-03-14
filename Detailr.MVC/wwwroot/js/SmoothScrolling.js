if (!Util) function Util() { };

Util.scrollTo = function (final, duration, cb, scrollEl) {
    var element = scrollEl || window;
    var start = element.scrollTo || document.documentElement.scrollTop,
        currentTime = null;

    if (!scrollEl) start = window.scrollY || document.documentElement.scrollTop;

    var animateScroll = function (timestamp) {
        if (!currentTime) currentTime = timestamp;
        var progress = timestamp - currentTime;
        if (progress > duration) progress = duration;

        var val = Math.easeInOutQuad(progress, start, final - start, duration);
        element.scrollTo(0, val);
        if (progress < duration) window.requestAnimationFrame(animateScroll);
        else cb && cb();
    };

    window.requestAnimationFrame(animateScroll);
};

Util.moveFocus = function (element) {
    if (!element) element = document.getElementsByTagName('body')[0];
    element.focus();
    if (document.activeElement !== element) {
        element.setAttribute('tabindex', '-1');
        element.focus();
    }
};

Util.cssSupports = function (property, value) {
    return CSS.supports(property, value);
};

Math.easeInOutQuad = function (t, b, c, d) {
    t /= d / 2;
    if (t < 1) return c / 2 * t * t + b;
    t--;
    return -c / 2 * (t * (t - 2) - 1) + b;
};

(function () {
    var SmoothScroll = function (element) {
        if (!('CSS' in window) || !CSS.supports('color', 'var(--color-var)')) return;
        this.element = element;
        this.scrollDuration = parseInt(this.element.getAttribute('data-duration') || 300);
        this.dataElementY = this.element.getAttribute('data-scrollable-element-y') || this.element.getAttribute('data-scrollable-element') || this.element.getAttribute('data-element');
        this.scrollElementY = this.dataElementY ? document.querySelector(this.dataElementY) : window;
        this.dataElementX = this.element.getAttribute('data-scrollable-element-x');
        this.scrollElementX = this.dataElementY ? document.querySelector(this.dataElementX) : window;

        this.initScroll();
    };

    SmoothScroll.prototype.initScroll = function () {
        var self = this;

        // Detect click on link
        this.element.addEventListener('click', function (event) {
            event.preventDefault();

            var targetId = event.target.closest('.js-smooth-scroll').getAttribute('href').replace('#', ''),
                target = document.getElementById(targetId),
                targetTabIndex = target.getAttribute('tabindex'),
                windowScrollTop = self.scrollElementY.scrollTop || document.documentElement.scrollTop;

            // Scroll vertically
            if (!self.dataElementY) windowScrollTop = window.scrollY || document.documentElement.scrollTop;

            var scrollElementY = self.dataElementY ? self.scrollElementY : false;

            // Check if there's a fixed element on the page
            var fixedHeight = self.getFixedElementHeight();

            Util.scrollTop(target.getBoundingClientRect().top + windowScrollTop - fixedHeight, self.scrollDuration, function () {
                self.scrollHorizontally(target, fixedHeight);

                // Move the focus to the target element - don't break keyboard navigation
                Util.moveFocus(target);
                history.pushState(false, false, '#' + targetId);
                self.resetTarget(target, targetTabIndex);
            }, scrollElementY);
        });
    };

    SmoothScroll.prototype.scrollHorizontally = function (target, delta) {
        var scrollEl = this.dataElementX ? this.scrollElementX : false;
        var windowScrollLeft = this.scrollElementX ? this.scrollElementX : document.documentElement.scrollLeft;
        var final = target.getBoundingClientRect().left + windowScrollLeft - delta,
            duration = this.scrollDuration;

        var element = scrollEl || window;
        var start = element.scrollLeft || document.documentElement.scrollLeft,
            currentTime = null;

        if (!scrollEl) start = window.scrollX || document.documentElement.scrollLeft;

        // Return if there is no need to scroll
        if (Math.abs(start - final) < 5) return;

        var animateScroll = function (timestamp) {
            if (!currentTime) currentTime = timestamp;
            var progress = timestamp - currentTime;
            if (progress > duration) progress = duration;
            var val = Math.easeInOutQuad(progress, start, final - start, duration);
            element.scrollTop({ left: val });
            if (progress < duration) window.requestAnimationFrame(animateScroll);
        };
        window.requestAnimationFrame(animateScroll);
    };

    SmoothScroll.prototype.resetTarge = function (target, tabIndex) {
        if (parseInt(target.getAttribute('tabindex')) < 0) {
            target.style.outline = "none";
            !tabIndex && target.removeAttribute('tabindex');
        }
    };

    SmoothScroll.prototype.getFixedHeightElement = function () {
        var scrollElementY = this.dataElementY ? this.scrollElementY : document.documentElement;
        var fixedElementDelta = parseInt(getComputedStyle(scrollElementY).getPropertyValue('scroll-padding'));
        if (isNaN(fixedElementDelta)) {
            fixedElementDelta = 0;
            var fixedElement = document.querySelector(this.element.getAttribute('data-fixed-element'));
            if (fixedElement) fixedElementDelta = parseInt(fixedElement.getBoundingClientRect().height);
        }
        return fixedElementDelta;
    };

    var smoothScrollLinks = document.getElementsByClassName('js-smooth-scroll');
    if (smoothScrollLinks.length > 0 && !Util.cssSupports('scroll-behavior', 'smooth') && window.requestAnimationFrame) {
        // you need javascript only if css scroll-behavior is not supported
        for (var i = 0; i < smoothScrollLinks.length; i++) {
            (function (i) { new SmoothScroll(smoothScrollLinks[i]); })(i);
        }
    }

}());