(function () {
    var WizardForm = function (element, formValidator) {
        this.element = element;
        this.steps = this.element.getElementsByClassName('js-wiz-form__step');
        this.currentIndex = 0;
        this.stepPrevClass = 'wiz-form__step--prev';
        this.stepNextClass = 'wiz-form__step--next';
        this.currentClass = 'js-wiz-form__step--current';
        // navigation
        this.navPrev = this.element.getElementsByClassName('js-wiz-form__prev');
        this.navNext = this.element.getElementsByClassName('js-wiz-form__next');
        this.navSummary = this.element.getElementsByClassName('js-wiz-form__summary');
        this.formSubmit = this.element.getElementsByClassName('js-wiz-form__submit');

        // Contact Details Summary
        this.UserNameSummary = this.element.getElementsByClassName("js-list-v2__contact-details_name")[0];
        this.emailSummary = this.element.getElementsByClassName("js-list-v2__contact-details_email")[0];
        this.phoneNumberSummary = this.element.getElementsByClassName("js-list-v2__contact-details_phoneNumber")[0];

        // Vehicle type details Summary
        this.vehicleTypeSummary = this.element.getElementsByClassName("js-list-v2__vehicle-type")[0];

        // Custom service summary
        this.customServiceSummary = this.element.getElementsByClassName("js-list-v2__custom-serivces")[0];

        // Vehicles choices Details
        this.vehicleChoices = this.element.getElementsByClassName("js-choice-img");

        // Custom selected services
        this.selectedCustomServices = this.element.getElementsByClassName("choice-tag--checked");

        // Contact Details
        this.firstName = this.element.getElementsByClassName('js-input__firstName')[0];
        this.lastName = this.element.getElementsByClassName('js-input__lastname')[0];
        this.email = this.element.getElementsByClassName('js-input__email')[0];
        this.phoneNumber = this.element.getElementsByClassName('js-input__phoneNumber')[0];

        // step bar
        this.stepsBar = this.element.getElementsByClassName('js-wiz-form__step-indicator');
        if (this.stepsBar.length > 0) {
            this.stepsBarCurrent = this.stepsBar[0].getElementsByClassName('js-wiz-form__current-step');
            this.stepsBarTot = this.stepsBar[0].getElementsByClassName('js-wiz-form__tot-steps');
        }
        // form validator
        this.formValidator = formValidator;
        this.formValidatorSteps = [];

        this.customServiceCheckedClass = 'choice-tag--checked';

        initWizardForm(this);
        initWizardFormEvents(this);
    };

    WizardForm.prototype.showStep = function (index) {
        this.currentIndex = index - 1;
        updateForm(this)
    };

    function initWizardForm(form) {
        // get selected step
        getSelectedStep(form);
        // reset navigation
        resetNav(form);
        setBarTotalSteps(form);
        resetStepBar(form);

        // init form validator
        if (form.formValidator) initFormValidator(form);
    };

    function changeStep(form, event) {
        if (event.target.closest('.js-wiz-form__next') || event.target.closest(".js-wiz-form__summary")) updateFormStep(form, 'next');
        if (event.target.closest('.js-wiz-form__prev')) updateFormStep(form, 'prev');
    };

    function getSelectedStep(form) {
        var selectedStep = form.element.getElementsByClassName(form.currentClass);
        form.currentIndex = (selectedStep.length > 0) ? Util.getIndexInArray(form.steps, selectedStep[0]) : 0;
        setStepsClass(form);
    };

    function setStepsClass(form) {
        for (var i = 0; i < form.steps.length; i++) {
            if (i < form.currentIndex) {
                Util.addClass(form.steps[i], form.stepPrevClass);
                Util.removeClass(form.steps[i], form.stepNextClass + ' ' + form.currentClass);
            } else if (i > form.currentIndex) {
                Util.addClass(form.steps[i], form.stepNextClass);
                Util.removeClass(form.steps[i], form.stepPrevClass + ' ' + form.currentClass);
            } else {
                Util.addClass(form.steps[i], form.currentClass);
                Util.removeClass(form.steps[i], form.stepNextClass + ' ' + form.stepPrevClass);
            }
        }
    };

    function resetNav(form) {
        if (form.navPrev.length > 0) {
            form.currentIndex > 0 ? Util.removeClass(form.navPrev[0], 'is-hidden') : Util.addClass(form.navPrev[0], 'is-hidden');
        }

        if (form.navNext.length > 0 && form.navSummary.length > 0) {
            if (form.currentIndex == (form.steps.length - 2)) {
                Util.addClass(form.navNext[0], 'is-hidden');
                Util.removeClass(form.navSummary[0], 'is-hidden');
            } else {
                Util.removeClass(form.navNext[0], 'is-hidden');
                Util.addClass(form.navSummary[0], 'is-hidden');
            }
        }

        if (form.navNext.length > 0 && form.formSubmit.length > 0) {
            if (form.currentIndex == (form.steps.length - 1)) {
                Util.addClass(form.navNext[0], 'is-hidden');
                Util.removeClass(form.formSubmit[0], 'is-hidden');
            }
            else if (form.currentIndex != (form.steps.length - 2)) {
                Util.removeClass(form.navNext[0], 'is-hidden');
                Util.addClass(form.formSubmit[0], 'is-hidden');
            }
            else {
                Util.addClass(form.formSubmit[0], 'is-hidden');
            }
        }
    };

    function setBarTotalSteps(form) {
        if (form.stepsBarTot && form.stepsBarTot.length > 0) {
            form.stepsBarTot[0].textContent = form.steps.length;
            form.stepsBar[0].style.setProperty('--steps-v2-steps-nr', form.steps.length);
        }
    };

    function resetStepBar(form) {
        if (form.stepsBarCurrent && form.stepsBarCurrent.length > 0) {
            form.stepsBar[0].style.setProperty('--step-v2-current-step', form.currentIndex + 1);
            form.stepsBarCurrent[0].textContent = form.currentIndex + 1;
        }
    };

    function updateFormStep(form, direction) {
        // update current step
        if (direction == 'next') form.currentIndex = form.currentIndex + 1;
        else form.currentIndex = form.currentIndex - 1;
        updateForm(form);
    };

    function updateForm(form) {
        if (form.currentIndex < 0) form.currentIndex = 0;
        if (form.currentIndex > form.steps.length) form.currentIndex = form.steps.length;
        if (form.currentIndex < form.steps.length) {
            setStepsClass(form); // update form visible step
            resetNav(form);
            resetStepBar(form);
        } else {
            form.currentIndex = form.steps.length - 1;
            // form will be submitted here
        }
    };

    function initFormValidator(form) {
        var opts = form.formValidator;
        for (var i = 0; i < form.steps.length; i++) {
            opts.element = form.steps[i];
            form.formValidatorSteps.push(new FormValidator(opts));
        }
    };

    function initWizardFormEvents(form) {
        form.element.addEventListener('click', function (event) {
            if (form.formValidator && event.target.closest('.js-wiz-form__summary')) {
                // change step only if no errors are found
                form.formValidatorSteps[form.currentIndex].validate(function (errors) {

                    if (errors.length == 0) {
                        changeStep(form, event);
                        populateSummary(form);
                    }
                });
            } else {
                changeStep(form, event);
            }
        });
    };

    function populateSummary(form) {
        form.UserNameSummary.textContent = form.firstName.value + " " + form.lastName.value;
        form.emailSummary.textContent = form.email.value;
        form.phoneNumberSummary.textContent = form.phoneNumber.value;

        form.customServiceSummary.innerHTML = "";

        for (var i = 0; i < form.selectedCustomServices.length; i++) {

            var li = document.createElement("li");

            li.textContent = form.selectedCustomServices[i].children[2].innerHTML

            form.customServiceSummary.append(li);
        }

        for (var i = 0; i < form.vehicleChoices.length; i++) {
            if (form.vehicleChoices[i].ariaChecked == 'true') {
                form.vehicleTypeSummary.textContent = form.vehicleChoices[i].textContent;
            }
        }

    }


    window.WizardForm = WizardForm;

    var wizardForm = document.getElementsByClassName('js-wiz-form');
    new WizardForm(wizardForm[0], {
        inputErrorClass: 'form-control--error',
        customValidate: {
            isValidEmail: function (input, cb) {

                var validEmail = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
                cb(validEmail.test(input.value));
            },

            isValidPhoneNumber: function (input, cb) {

                var validPhoneNumber = /^\d{11}$/;
                cb(validPhoneNumber.test(input.value));
            }
        }
    });

}());