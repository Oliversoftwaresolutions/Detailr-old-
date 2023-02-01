(function () {
    var floatingLabels = document.getElementsByClassName('js-floating-label');
    if (floatingLabels.length > 0) {
        for (var i = 0; i < floatingLabels.length; i++) {
            (function (i) { initFloatingLabel(floatingLabels[i]) })(i);
        }

        function initFloatingLabel(element) {
            var input = element.getElementsByClassName('js-form-control')[0];
            input.addEventListener('input', function (event) {
                resetFloatingLabel(element, input);
            });
        };

        function resetFloatingLabel(element, input) { // show label if input is not empty
            element.getElementsByClassName('js-form-label')[0].classList.toggle('form-label--floating', input.value.length > 0);
        };
    }
}());