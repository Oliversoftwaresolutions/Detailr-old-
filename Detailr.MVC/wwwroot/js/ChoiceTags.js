(function () {
    var ChoiceTags = function (element) {
        this.element = element;
        this.labels = this.element.getElementsByClassName('js-choice-tag');
        this.customChoiceTagInput = document.getElementsByClassName("choice-tag__custom-input")[0];
        this.characterLimit = Number(this.customChoiceTagInput.getAttribute('maxlength')) || 50;
        this.customServiceCount = document.getElementsByClassName("js-choice-tags")[0].getElementsByTagName("li").length;
        this.customServiceRemoveBtn = document.getElementsByClassName("js-custom-service--remove")[0];
        this.counter = document.getElementsByClassName("js-character-count__counter")[0];
        this.customServiceCounter = document.getElementsByClassName("js-customservice-count__counter")[0];
        this.inputs = getChoiceInput(this);
        this.isRadio = this.inputs[0].type.toString() == 'radio';
        this.checkedClass = 'choice-tag--checked';
        this.defaultClass = 'choice-tag--default';

        this.customServiceLimit = 10;
        this.defaultServicesList = 5;

        this.initCount();
        initChoiceTags(this);
        initChoiceTagEvent(this);
        initCustomChoiceTags(this);
    }

    ChoiceTags.prototype.initCount = function () {
        var self = this;

        this.counter.textContent = this.getCount();
        this.customServiceCounter.textContent = this.getCustomServiceCount();
    };

    ChoiceTags.prototype.getCount = function () {
        return this.characterLimit - this.customChoiceTagInput.value.length;
    };

    ChoiceTags.prototype.getCustomServiceCount = function () {
        return this.customServiceLimit - this.customServiceCount + this.defaultServicesList;
    }

    function getChoiceInput(element) {
        var inputs = [];
        for (var i = 0; i < element.labels.length; i++) {
            inputs.push(element.labels[i].getElementsByTagName('input')[0]);
        }
        return inputs;
    };

    function initChoiceTags(element) {
        // if tag is selected by default - add checkedClass to the label element
        for (var i = 0; i < element.inputs.length; i++) {
            element.labels[i].classList.toggle(element.checkedClass, element.inputs[i].checked);
        }
    };

    function initChoiceTagEvent(element) {
        element.element.addEventListener('change', function (event) {
            var inputIndex = Array.prototype.indexOf.call(element.inputs, event.target);
            if (inputIndex < 0) return;
            element.labels[inputIndex].classList.toggle(element.checkedClass, event.target.checked);
            if (element.isRadio && event.target.checked) resetRadioTags(element, inputIndex);
        });

        element.customServiceRemoveBtn.addEventListener("click", function (event) {
            deleteSelectedChoiceTags(element);
        });

        element.customChoiceTagInput.addEventListener("input", function (event) {
            element.counter.textContent = element.getCount();
        });
    };

    function resetRadioTags(element, index) {
        // when a radio input is checked - reset all the others
        for (var i = 0; i < element.labels.length; i++) {
            if (i != index) element.labels[i].classList.remove(element.checkedClass);
        }
    };

    function initCustomChoiceTags(element) {

        element.customChoiceTagInput.addEventListener("keyup", function (event) {

            debugger;

            if (event.keyCode === 13 && element.customChoiceTagInput.value.length != 0) {

                var updatedChoiceTagsCount = updateChoiceTagsCount(element);
                initChoiceTagEvent(element);

                if (updatedChoiceTagsCount.length > 14)
                    return;

                createNewChoiceTag(element);
                updateChoiceTagsCount(element);
                increaseCustomServiceCounter(element);
                resetInputsAndMaximumWordCounter(element);
            }
        });
    };

    function updateChoiceTagsCount(element) {
        var updatedInputs = getChoiceInput(element);
        element.inputs = updatedInputs;
        return updatedInputs;
    };

    function increaseCustomServiceCounter(element) {
        element.customServiceCount++;
        element.customServiceCounter.textContent = element.getCustomServiceCount();
    };

    function decreaseCustomServiceCounter(element) {
        element.customServiceCount--;
        element.customServiceCounter.textContent = element.getCustomServiceCount();
    }

    function resetInputsAndMaximumWordCounter(element) {
        element.customChoiceTagInput.value = "";
        element.counter.textContent = element.getCount();
    };

    function getAllSelectedChoiceTags(element) {

        var selectedChoiceTags = [];

        for (var i = 0; i < element.labels.length; i++) {

            var isDefaultChoiceTag = Util.hasClass(element.labels[i], element.defaultClass);
            var hasChoiceTagBeenSelected = Util.hasClass(element.labels[i], element.checkedClass);


            if (!isDefaultChoiceTag && hasChoiceTagBeenSelected)
                selectedChoiceTags.push(element.labels[i].getElementsByTagName('input')[0])
        }

        return selectedChoiceTags;

    };

    function deleteSelectedChoiceTags(element) {
        var choiceTagsToDelete = getAllSelectedChoiceTags(element);

        for (var i = 0; i < choiceTagsToDelete.length; i++) {
            choiceTagsToDelete[i].labels[i].parentElement.remove();
            decreaseCustomServiceCounter(element);
        }       
    };

    function createNewChoiceTag(element) {

        var choiceTagContainer = document.getElementsByClassName("choice-tags")[0];
        var newListItem = document.createElement("li");

        // Create label and add classes 
        var label = document.createElement("label");
        label.className = "choice-tag choice-tag--checkbox text-sm js-choice-tag";

        // Add input and add classes 
        var input = document.createElement("input");
        input.className = "sr-only";
        input.type = "checkbox";
        input.id = "checkbox-tag-" + element.customChoiceTagInput.value;

        // Add svg (icon)
        var svg = '<svg class="choice-tag__icon icon margin-right-xxs" viewBox="0 0 16 16" aria-hidden="true"><g class="choice-tag__icon-group" fill="none" stroke="currentColor" stroke-linecap="round" stroke-miterlimit="10" stroke-width="2"><line x1="-6" y1="8" x2="8" y2="8"></line><line x1="8" y1="8" x2="22" y2="8"></line><line x1="8" y1="2" x2="8" y2="14"></line></g></svg>';

        // Create span using user input
        var span = document.createElement("span");
        span.textContent = element.customChoiceTagInput.value;
        label.appendChild(input);
        label.appendChild(span);
        input.insertAdjacentHTML("afterend", svg);
        newListItem.appendChild(label);
        choiceTagContainer.appendChild(newListItem);


    };

    //initialize the ChoiceTags objects
    var choiceTags = document.getElementsByClassName('js-choice-tags');
    if (choiceTags.length > 0) {
        for (var i = 0; i < choiceTags.length; i++) {
            (function (i) { new ChoiceTags(choiceTags[i]); })(i);
        }
    };
}());