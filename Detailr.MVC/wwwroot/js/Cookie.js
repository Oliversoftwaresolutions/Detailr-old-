(function () {
    function initNoticeEvents(notice) {
        notice.addEventListener('click', function (event) {
            if (event.target.closest('.js-notice__hide-control')) {
                event.preventDefault();
                notice.classList.add('notice--hide');
            }
        });
    };

    var noticeElements = document.getElementsByClassName('js-notice');
    if (noticeElements.length > 0) {
        for (var i = 0; i < noticeElements.length; i++) {
            (function (i) {
                initNoticeEvents(noticeElements[i]);
            })(i);
        }
    }
}());